//This creates a chatroom, that allows for multiple (unlimmited) number of clients communicating with each other.


namespace WcfChatRoom
{
    using System;
    using System.Collections.Generic;
    using System.ServiceModel;
    using System.Threading;
    using System.Windows.Forms;
    using System.Linq;
    using System.Net.Http;
    using Data;
    using Ozeki.Camera;
    using Ozeki.Media;
    using Ozeki.VoIP;
    using NAudioStreaming;
    using System.Media;
    using System.IO;
    public partial class Chatroom : Form
    {
        #region Properties & Ctor

        private static int SERVICE_PORT = 6274;
        private static string SERVICE_URL = "http://localhost:{0}/Api/ConnectionMgr/";
        string username;
        private static string REGISTER_ACTION = "Register";
        private static string EXIT_ACTION = "Exit?userName={0}";
        private static string GET_CLIENTS_ACTION = "GetConnectedUsers";
        string url = "http://{0}";
      
        private List<string> ConnectedUsers { get; set; }
        private List<string> Connectedports { get; set; }
        private string myListeningUrl { get; set; }
        private object LOCK { get; set; }
        private bool isConnected { get; set; }
        bool videoflag = false;
        Thread receivingThread;
        public SoundPlayer ringing;


       
        string vclient_ipaddress = "";
        string vclient_name = "";
        string auport = "";
        string videoport = "";
        string client_videoport = "";
        string client_auport = "";
        video_chat_room videoroom;
        public string camname = "";
        public string audioinputname = "";
        public string audiooutputname = "";

        public bool IsConnected
        {
            get
            {
                lock (LOCK)
                {
                    return this.isConnected;
                }
            }
            set
            {
                lock (LOCK)
                {
                    this.isConnected = value;
                }
            }
        }
        List<ChannelFactory<IVeryBasicCommunicationService>> SendingFactories { get; set; }



        public Chatroom(string name)
        {
            InitializeComponent();
 
            this.txtListenOn.Text = Ozeki.Network.NetworkAddressHelper.GetLocalIP().ToString() + ":2048";
            this.Video_but.Enabled = false;
            this.user_name.Text = name;
            this.LOCK = new object();
            this.IsConnected = false;
            this.AdjustUi();
            this.txtIpAddress.Text = "500";
            this.audio_port.Text = "555";
            string path = FigureOutFilePath("ringing.wav");
            ringing = new SoundPlayer(path);

            cbCameras.Items.Clear();
            foreach (var info in WebCameraFactory.GetDevices())
            {
                cbCameras.Items.Add(info.Name);
            }

            cbCameras.SelectedIndex = 0;
            Helper.PopulateInputDevicesCombo(this.cmbInputs);
            Helper.PopulateCodecsCombo(Helper.AvailableCodecs, this.cmbCodecs);
        }

        #endregion
        private static string FigureOutFilePath(string fileName)
        {
            string path = fileName;
            if (!File.Exists(path)) { path = @"..\..\" + fileName; }
            if (!File.Exists(path)) { Console.WriteLine("File not found"); throw new Exception("File not found."); }

            path = Path.GetFullPath(path);
            return path;
        }
        #region Form Events

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.ConnectedUsers = new List<string>();
                this.Connectedports = new List<string>();
                receivingThread = new Thread(x => { this.StartAndRunReceiver(); });
                this.myListeningUrl = this.txtListenOn.Text;
                this.username = this.user_name.Text;


                Register(username, string.Format(url, this.myListeningUrl));
                this.IsConnected = true;
                this.AdjustUi();
                this.FigureOutConnectedClients();
                receivingThread.Start();
            }
            catch (Exception)
            {
                Console.WriteLine("ERROR: {0}", e);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            FigureOutConnectedClients();
            this.Send();
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            // stop the listener

            this.IsConnected = false;
            AdjustUi();
            this.Video_but.Enabled = false;
            Exit(this.username);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsConnected = false;
            Exit(this.username);

        }

        #endregion

        #region Private Helpers



        private void StartAndRunReceiver()
        {
            try
            {
                VeryBasicCommunicationService myService = new VeryBasicCommunicationService(this.PostMessage);

                using (ServiceHost host = new ServiceHost(myService, new Uri(string.Format("http://{0}", this.myListeningUrl))))
                {
                    host.AddServiceEndpoint(typeof(IVeryBasicCommunicationService), new BasicHttpBinding(), "Chat");
                    host.Open();


                    this.PostMessage(username + " is listening.. on: " + this.myListeningUrl);

                    while (this.IsConnected)
                    {
                        // this is kinda silly.. but simple enough..
                        Thread.Sleep(1000);

                    }

                    this.PostMessage("RECEIVER IS CLOSING");

                    host.Close();
                }
            }
            catch (Exception)
            {
                this.IsConnected = false;
                this.PostMessage("RECEIVER FAILED. Try running the app as admin");
                /* ..should log here.. */

            }

            this.AdjustUi();
            this.SendingFactories = null;
        }

        private void AdjustUi()
        {
            if (this.InvokeRequired) { this.Invoke(new Action(AdjustUi)); }
            else
            {
                this.btnConnect.Enabled = !this.IsConnected;
                this.btnDisconnect.Enabled = this.IsConnected;
                this.btnSend.Enabled = this.IsConnected;
                this.txtMessage.Enabled = this.IsConnected;
                this.txtListenOn.Enabled = !this.IsConnected;
                this.accept_button.Enabled = this.videoflag;
                this.decline_button.Enabled = this.videoflag;
                this.setting_button.Enabled = this.IsConnected;

            }
        }

        private void Send()
        {
            // THIS SHOULD BE HANDLED BETTER.. FOR NOW THOUGH, WE WILL USE THE URL
            string userName = this.username;

            // send to all connected clients, except to myself..
            string message = string.Format("({0}): {1}", userName, this.txtMessage.Text);

            foreach (ChannelFactory<IVeryBasicCommunicationService> factory in this.SendingFactories)
            {
                try
                {
                    IVeryBasicCommunicationService httpProxy = factory.CreateChannel();

                    httpProxy.Transmit(message);
                }
                catch (EndpointNotFoundException)
                {
                    this.PostMessage(string.Format("FAILED TO SEND TO {0} => nobody listening.", factory.Endpoint.Address));
                }
            }
        }

        private void SetupSenderFactories()
        {
            this.SendingFactories = new List<ChannelFactory<IVeryBasicCommunicationService>>();

            foreach (string connectedClient in this.Connectedports)
            {
                if (connectedClient != string.Format("http://{0}", this.myListeningUrl))
                {
                    this.SendingFactories.Add(new ChannelFactory<IVeryBasicCommunicationService>(new BasicHttpBinding(),
                        new EndpointAddress(string.Format("{0}/Chat", connectedClient))));
                }
            }


        }

        private void PostMessage(string message)
        {
            if (this.InvokeRequired) { this.Invoke(new Action<string>(PostMessage), new object[] { message }); }
            else
            {
                try
                {
                    //httpProxy.Transmit("#videochat#*"+username+"*"+ myListeningUrl+"*"+videoport+"*"+auport);
                    string[] splitmessage = message.Split('*');
                    if (splitmessage[0] == "#videochat#")
                    {
                        ringing.PlayLooping();
                        vclient_ipaddress = splitmessage[2];
                        vclient_name = splitmessage[1];
                        client_videoport = splitmessage[3];
                        client_auport = splitmessage[4];
                        videoflag = true;
                        DialogResult result =
                        MessageBox.Show("Video request from " + vclient_name + ", accept or decline?",
                        "Video Request", MessageBoxButtons.OK);

                        this.AdjustUi();


                    }
                    else if (splitmessage[0] == "#acceptance#")
                    {
                        ringing.Stop();
                        client_videoport = splitmessage[1];
                        client_auport = splitmessage[2];
                        Video_but.Text = "Video Chat";
                        
                        videoroom = new video_chat_room(myListeningUrl, videoport, auport, vclient_name, vclient_ipaddress, client_videoport, client_auport, camname, audioinputname, "");
                        videoroom.Show();

                    }
                    else if (splitmessage[0] == "#decline#")
                    {
                        vclient_ipaddress = "";
                        vclient_name = "";
                        client_videoport = "";
                        client_auport = "";
                        videoflag = false;
                        ringing.Stop();
                        Video_but.Text = "Video Chat";
                        this.AdjustUi();
                    }
                    else
                        this.lblChat.Text = string.Format("({0}) {1}\n", DateTime.Now.ToLongTimeString(),
                            message) + this.lblChat.Text;
                }

                catch (Exception e)
                {

                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void acceptance()
        {
            if (this.InvokeRequired) { this.Invoke(new Action(acceptance), new object[] { }); }
            else
            {
                try
                {
                    this.SendingFactories = new List<ChannelFactory<IVeryBasicCommunicationService>>();
                    this.SendingFactories.Add(new ChannelFactory<IVeryBasicCommunicationService>(new BasicHttpBinding(),
                        new EndpointAddress(string.Format("http://{0}/Chat", vclient_ipaddress))));
                    IVeryBasicCommunicationService httpProxy = SendingFactories[0].CreateChannel();
                    httpProxy.Transmit("#acceptance#*" + videoport + "*" + auport);
                }

                catch (Exception e)
                {

                    Console.WriteLine(e.ToString());
                }
            }
        }

        private void FigureOutConnectedClients()
        {
            // this should be obtained from the server, perphaps? For now, hardcoding it..
            try
            {

                List<ConnectedClient> clientlist = GetConnectedClients();
                ConnectedUsers.Clear();
                Connectedports.Clear();
                //int count = 0;
                foreach (ConnectedClient temp in clientlist)
                {

                    string user = temp.UserName;
                    this.ConnectedUsers.Add(user);
                    this.Connectedports.Add(temp.UrlAddress);


                }


                this.lbConnectedClients.Items.Clear();
                foreach (string client in this.ConnectedUsers)
                {
                    string display = (client == this.username ? "(ME) " : string.Empty) + client;

                    this.lbConnectedClients.Items.Add(display);
                }

                this.SetupSenderFactories();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }


        public bool Register(string userName, string myUrl)
        {
            bool result = false;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format(SERVICE_URL, SERVICE_PORT));

            ConnectedClient me = new ConnectedClient(userName, myUrl);
            try
            {
                HttpResponseMessage response = client.PostAsJsonAsync(REGISTER_ACTION, me).Result;
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("## Connected ##");
                    result = true;
                }
                else
                {
                    Console.WriteLine("ERROR: {0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                }

                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: {0}", e);
                return result;
            }

        }

        public List<ConnectedClient> GetConnectedClients()
        {
            List<ConnectedClient> result = null;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format(SERVICE_URL, SERVICE_PORT));

            HttpResponseMessage response = client.GetAsync(GET_CLIENTS_ACTION).Result;
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsAsync<List<ConnectedClient>>().Result;

                Console.WriteLine("Connected clients:");

                if (!result.Any())
                {
                    Console.WriteLine("   ~~ No Clients ~~");
                }
                else
                {
                    foreach (ConnectedClient aClient in result)
                    {
                        Console.WriteLine("   {0}", aClient);
                    }
                }
            }
            else
            {
                Console.WriteLine("ERROR: {0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            Console.WriteLine();

            return result;
        }

        public bool Exit(string userName)
        {
            bool result = false;

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(String.Format(SERVICE_URL, SERVICE_PORT));

            HttpResponseMessage response = client.DeleteAsync(string.Format(EXIT_ACTION, userName)).Result;

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("## Disconnected ##");
                result = true;
            }
            else
            {
                Console.WriteLine("ERROR: {0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            return result;
        }



        private void Video_but_Click(object sender, EventArgs e)
        {
            try
            {

                if (Video_but.Text == "Cancal")
                {
                    this.SendingFactories = new List<ChannelFactory<IVeryBasicCommunicationService>>();
                    this.SendingFactories.Add(new ChannelFactory<IVeryBasicCommunicationService>(new BasicHttpBinding(),
                            new EndpointAddress(string.Format("{0}/Chat", vclient_ipaddress))));
                    IVeryBasicCommunicationService httpProxy = SendingFactories[0].CreateChannel();


                    httpProxy.Transmit("#decline#*");
                    ringing.Stop();
                    Video_but.Text = "Video Chat";
                    this.AdjustUi();
                }
                else
                {
                    int index = 0;
                    index = lbConnectedClients.SelectedIndex;
                    if (index == -1) { MessageBox.Show("Select a user to start up the video chat"); return; }
                    vclient_name = ConnectedUsers[index];
                    string[] splitmessage = Connectedports[index].Split('/');
                    vclient_ipaddress = splitmessage[2];

                    if (vclient_name == username)
                    {
                        vclient_ipaddress = "";
                        vclient_name = "";
                    }
                    else
                    {
                        this.SendingFactories = new List<ChannelFactory<IVeryBasicCommunicationService>>();
                        this.SendingFactories.Add(new ChannelFactory<IVeryBasicCommunicationService>(new BasicHttpBinding(),
                                new EndpointAddress(string.Format("http://{0}/Chat", vclient_ipaddress))));
                        IVeryBasicCommunicationService httpProxy = SendingFactories[0].CreateChannel();

                        httpProxy.Transmit("#videochat#*" + username + "*" + myListeningUrl + "*" + videoport + "*" + auport);
                        ringing.PlayLooping();
                        Video_but.Text = "Cancal";
                        videoflag = false;
                    }

                }




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }




        private void setting_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtIpAddress.Text)) { MessageBox.Show("Video Port is required"); this.Video_but.Enabled = false; return; }
            if (string.IsNullOrEmpty(this.audio_port.Text)) { MessageBox.Show("Aduio Port is required"); this.Video_but.Enabled = false; return; }
            videoport = this.txtIpAddress.Text;
            auport = this.audio_port.Text;
            camname = (string)cbCameras.SelectedItem;
            audioinputname= cmbInputs.SelectedIndex.ToString();
            //audiooutputname = (string)cmbCodecs.SelectedItem;
            this.Video_but.Enabled = true;
            FigureOutConnectedClients();
        }

        private void accept_button_Click(object sender, EventArgs e)
        {
            auport = (int.Parse(client_auport) % 999 + 555).ToString();
            videoport = (int.Parse(client_videoport) % 999 + 555).ToString();
            this.txtIpAddress.Text = videoport;
            this.audio_port.Text = auport;
            this.SendingFactories = new List<ChannelFactory<IVeryBasicCommunicationService>>();
            this.SendingFactories.Add(new ChannelFactory<IVeryBasicCommunicationService>(new BasicHttpBinding(),
                new EndpointAddress(string.Format("http://{0}/Chat", vclient_ipaddress))));
            IVeryBasicCommunicationService httpProxy = SendingFactories[0].CreateChannel();
            httpProxy.Transmit("#acceptance#*"+videoport + "*" + auport);
            videoflag = false;
            ringing.Stop();
            this.AdjustUi();
            camname = (string)cbCameras.SelectedItem;
            audioinputname = cmbInputs.SelectedIndex.ToString();
            //video_chat_room(string myListening, string videop, string aup, string vclient_n, string vclient_ipadd, string client_videop, string client_aup, string camn, string audioinputn, string audiooutputn)
            videoroom = new video_chat_room(myListeningUrl, videoport, auport, vclient_name, vclient_ipaddress, client_videoport, client_auport, camname, audioinputname, "");
            videoroom.Show();

        }

        private void decline_button_Click(object sender, EventArgs e)
        {
            this.SendingFactories = new List<ChannelFactory<IVeryBasicCommunicationService>>();
            this.SendingFactories.Add(new ChannelFactory<IVeryBasicCommunicationService>(new BasicHttpBinding(),
                new EndpointAddress(string.Format("http://{0}/Chat", vclient_ipaddress))));
            IVeryBasicCommunicationService httpProxy = SendingFactories[0].CreateChannel();
            httpProxy.Transmit("#decline#*");
            vclient_ipaddress = "";
            vclient_name = "";
            client_videoport = "";
            client_auport = "";
            videoflag = false;
            ringing.Stop();
            this.AdjustUi();
        }
    }
    #endregion

}
