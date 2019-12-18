using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ozeki.Camera;
using Ozeki.Media;
using Ozeki.VoIP;
using NAudioStreaming;
using System.Net.Sockets;
using NAudio.Wave;
using System.Net;
using System.Threading;

namespace WcfChatRoom
{
    public partial class video_chat_room : Form
    {
        //server video
        private MediaConnector connector;
        private MJPEGStreamer streamer;
        private IVideoSender videoSender;
        private DrawingImageProvider provider;
        private IWebCamera webCamera;
        private VideoViewerWF videoViewer;

        //client video
        private DrawingImageProvider vprovider;
        private MediaConnector vconnector;
        private MJPEGConnection mjpegConnection;
        private VideoViewerWF vvideoViewer;
        private Zoom zoom;
        //server audio
        private IWavePlayer waveOut { get; set; }
        private BufferedWaveProvider waveProvider { get; set; }
        private UdpClient udpListener { get; set; }
        private INetworkChatCodec codec { get; set; }

        // lock
        private volatile bool connected;



        //server
        
        private NAudio.Wave.WaveIn waveIn { get; set; }
        private UdpClient udpSender { get; set; }
        private INetworkChatCodec selectedCodec { get; set; }
        private bool vconnected { get; set; }


        string myListeningUrl = "";
        string vclient_ipaddress = "";
        string vclient_name = "";
        string auport = "";
        string videoport = "";
        string client_videoport = "";
        string client_auport = "";
        string camName = "";
        string audioinputname = "";
        string audiooutputname = "";

       
        public video_chat_room(string myListening, string videop, string aup, string vclient_n, string vclient_ipadd,string client_videop, string client_aup, string camn, string audioinputn, string audiooutputn)
        {
            try
            {
                InitializeComponent();
            
                string[] splitmessage = myListening.Split(':');
                myListeningUrl = splitmessage[0];
                videoport = videop;
                auport = aup;
                vclient_name = vclient_n;
                splitmessage = vclient_ipadd.Split(':');
                vclient_ipaddress = splitmessage[0];
                client_videoport = client_videop;
                client_auport = client_aup;
                camName = camn;
                audioinputname = audioinputn;
                audiooutputname = audiooutputn;
                close_button.Enabled = false;
                this.client_label.Text = vclient_name;

                //client
                vconnector = new MediaConnector();
                mjpegConnection = new MJPEGConnection();
                vprovider = new DrawingImageProvider();
                this.zoom = new Zoom();

                videoViewer = new VideoViewerWF()
                {
                    Name = "Video Preview",
                    Size = new Size(300, 210),
                    Location = new Point(20, 20),
                    BackColor = Color.Black
                };

                vvideoViewer = new VideoViewerWF()
                {
                    Name = "Video Preview",
                    Size = new Size(300, 210),
                    Location = new Point(20, 20),
                    BackColor = Color.Black
                };



                connector = new MediaConnector();
                provider = new DrawingImageProvider();
                videoViewer.SetImageProvider(provider);




                vvideoViewer.SetImageProvider(vprovider);

                groupBox1.Controls.Add(videoViewer);

                webCamera = null;

                foreach (var info in WebCameraFactory.GetDevices())
                {
                    if (info.Name == camName)
                    {
                        webCamera = WebCameraFactory.GetDevice(info);

                        break;
                    }
                }

                if (webCamera == null)
                {
                    MessageBox.Show("Couldn't connect to the camera");
                    return;
                }
                connector.Connect(webCamera.VideoChannel, provider);
                videoSender = webCamera.VideoChannel;

                webCamera.Start();
                videoViewer.Start();
                streamer = new MJPEGStreamer(myListeningUrl, int.Parse(videoport));


                if (!connector.Connect(videoSender, streamer.VideoChannel))
                {
                    MessageBox.Show("Failed to create connection..");
                }
                streamer.ClientConnected += ClientConnected;
                streamer.ClientDisconnected += ClientDisconnected;

                streamer.Start();





                if (!this.vconnected)
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(this.myListeningUrl), int.Parse(this.auport));
               

                  
                    this.selectedCodec = new AcmMuLawChatCodec();
                   
                    this.ServerConnect(endPoint, int.Parse(audioinputn), selectedCodec);
                   
                }
                else
                {
                    this.ServerDisconnect();
                 
                }


                this.lblStatus.Text = "Server & camera started\n" + lblStatus.Text;
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.ToString() + "\n" + lblStatus.Text;
            }
           
        }

        private void ServerConnect(IPEndPoint endPoint, int inputDeviceNumber, INetworkChatCodec codec)
        {
            // input handling
            this.waveIn = new NAudio.Wave.WaveIn()
            {
                BufferMilliseconds = 50,
                DeviceNumber = inputDeviceNumber,
                WaveFormat = codec.RecordFormat
            };

            this.waveIn.DataAvailable += SoundDataAvailable;
            this.waveIn.StartRecording();

            this.udpSender = new UdpClient();
            this.udpSender.Connect(endPoint);

            this.vconnected = true;
        }

        private void ServerDisconnect()
        {
            if (this.vconnected)
            {
                this.vconnected = false;
                this.waveIn.DataAvailable -= SoundDataAvailable;
                this.waveIn.StopRecording();

                this.udpSender.Close();
                this.waveIn.Dispose();

                
                this.selectedCodec.Dispose();
            }
        }

        private void SoundDataAvailable(object sender, WaveInEventArgs e)
        {
            byte[] encoded = selectedCodec.Encode(e.Buffer, 0, e.BytesRecorded);
            this.udpSender.Send(encoded, encoded.Length);
        }


        void ClientConnected(object sender, VoIPEventArgs<IMJPEGStreamClient> e)
        {
            if (this.InvokeRequired) { this.Invoke(new Action<object, VoIPEventArgs<IMJPEGStreamClient>>(ClientConnected), new object[] { sender, e }); }
            else
            {
                e.Item.StartStreaming();

                lblStatus.Text = ("Client Connected: " + e.Item.RemoteAddress + ":" + e.Item.RemotePort) + "\n" + lblStatus.Text;
            }
        }


        void ClientDisconnected(object sender, VoIPEventArgs<IMJPEGStreamClient> e)
        {
            if (this.InvokeRequired) { this.Invoke(new Action<object, VoIPEventArgs<IMJPEGStreamClient>>(ClientDisconnected), new object[] { sender, e }); }
            else
            {
                e.Item.StopStreaming();

                lblStatus.Text = ("Client Disconnected: " + e.Item.RemoteAddress + ":" + e.Item.RemotePort) + "\n" + lblStatus.Text;
            }
        }




        private void start_button_Click(object sender, EventArgs e)
        {
            try
            {
                if (mjpegConnection != null) { mjpegConnection.Disconnect(); }

                if (!mjpegConnection.Connect("http://" + vclient_ipaddress + ":" + client_videoport, 100000))
                {
                    MessageBox.Show("Connection failed - mjpegConnection.");
                }


                if (!vconnector.Connect(mjpegConnection.VideoChannel, zoom))
                {
                    MessageBox.Show("Connection failed - connector zoom.");
                }

                if (!vconnector.Connect(zoom, vprovider))
                {
                    MessageBox.Show("Connection failed - connector provider.");
                }
                vvideoViewer.Start();
                groupBox.Controls.Add(vvideoViewer);



                //client
                if (!this.connected)
                {
                    IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(vclient_ipaddress), int.Parse(client_auport));

                   
                    this.codec = new AcmMuLawChatCodec();

                    this.ClientConnect(endPoint, codec);
                
                }
                else
                {
                    this.ClientDisconnect();
                
                }


                start_button.Enabled = false;
                close_button.Enabled = true;
            }
            catch (Exception ex)
            {
                lblStatus.Text = ex.ToString() + "\n" + lblStatus.Text;
            }
        }

        private void ClientConnect(IPEndPoint endPoint, INetworkChatCodec codec)
        {
            this.udpListener = new UdpClient();
            this.udpListener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            this.udpListener.Client.Bind(endPoint);

            this.waveOut = new WaveOut();
            this.waveProvider = new BufferedWaveProvider(codec.RecordFormat);
            this.waveOut.Init(waveProvider);
            this.waveOut.Play();

            this.connected = true;
            var state = new AudioListenerThreadData { Codec = codec, EndPoint = endPoint };
            ThreadPool.QueueUserWorkItem(this.ListenerProcessing, state);
        }

        private void ClientDisconnect()
        {
            if (this.connected)
            {
                this.connected = false;
                this.waveOut.Stop();

                this.waveOut.Stop();
                this.udpListener.Close();
                this.waveOut.Dispose();

                // NAudio designed the codecs to support multiple calls to Dispose, recreating their resources if Encode/Decode called again
                this.codec.Dispose();
            }
        }

        private void ListenerProcessing(object state)
        {
            var listenerThreadState = (AudioListenerThreadData)state;
            var endPoint = listenerThreadState.EndPoint;

            try
            {
                while (this.connected)
                {
                    byte[] b = udpListener.Receive(ref endPoint);
                    byte[] decoded = listenerThreadState.Codec.Decode(b, 0, b.Length);
                    waveProvider.AddSamples(decoded, 0, decoded.Length);
                }
            }
            catch (SocketException)
            {
                // usually not a problem - just means we have disconnected
            }
        }

        private void close_button_Click(object sender, EventArgs e)
        {
            this.close();
        


        }
        private void close()
        {
            try
            {
                videoViewer.Stop();

                connector.Disconnect(webCamera.VideoChannel, provider);
                groupBox1.Controls.Clear();

                webCamera.Stop();
                if (mjpegConnection == null) { return; }
                vvideoViewer.Stop();

                vconnector.Disconnect(mjpegConnection.VideoChannel, vprovider);
                mjpegConnection.Disconnect();

                groupBox.Controls.Clear();
                lblStatus.Text = "Server & camera stopped\n" + lblStatus.Text;

                ClientDisconnect();
                ServerDisconnect();
            }
            catch (Exception ex)
            {
                lblStatus.Text=ex.ToString() +"\n"+ lblStatus.Text;
            }
        }

        private void video_chat_room_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.close();
        }
    }
}
