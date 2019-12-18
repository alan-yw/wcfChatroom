namespace WcfChatRoom
{
    partial class Chatroom
    {
        
        private System.ComponentModel.IContainer components = null;

        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

       
        private void InitializeComponent()
        {
            this.lblChat = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.user_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtListenOn = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Video_but = new System.Windows.Forms.Button();
            this.lbConnectedClients = new System.Windows.Forms.ListBox();
            this.setting_label = new System.Windows.Forms.Label();
            this.vport_label = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.accept_button = new System.Windows.Forms.Button();
            this.decline_button = new System.Windows.Forms.Button();
            this.cbCameras = new System.Windows.Forms.ComboBox();
            this.audio_port_label = new System.Windows.Forms.Label();
            this.audio_port = new System.Windows.Forms.TextBox();
            this.cmbInputs = new System.Windows.Forms.ComboBox();
            this.cmbCodecs = new System.Windows.Forms.ComboBox();
            this.setting_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblChat
            // 
            this.lblChat.Location = new System.Drawing.Point(6, 7);
            this.lblChat.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblChat.Name = "lblChat";
            this.lblChat.Size = new System.Drawing.Size(434, 562);
            this.lblChat.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.btnConnect.Location = new System.Drawing.Point(446, 98);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(158, 50);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(4, 591);
            this.txtMessage.Margin = new System.Windows.Forms.Padding(2);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(436, 84);
            this.txtMessage.TabIndex = 3;
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.btnSend.Location = new System.Drawing.Point(452, 593);
            this.btnSend.Margin = new System.Windows.Forms.Padding(2);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(158, 82);
            this.btnSend.TabIndex = 4;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 19F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisconnect.ForeColor = System.Drawing.Color.OrangeRed;
            this.btnDisconnect.Location = new System.Drawing.Point(446, 152);
            this.btnDisconnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(158, 50);
            this.btnDisconnect.TabIndex = 6;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // user_name
            // 
            this.user_name.Enabled = false;
            this.user_name.Location = new System.Drawing.Point(446, 37);
            this.user_name.Name = "user_name";
            this.user_name.Size = new System.Drawing.Size(158, 20);
            this.user_name.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(445, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "User Name:";
            // 
            // txtListenOn
            // 
            this.txtListenOn.Location = new System.Drawing.Point(485, 66);
            this.txtListenOn.Margin = new System.Windows.Forms.Padding(2);
            this.txtListenOn.Name = "txtListenOn";
            this.txtListenOn.Size = new System.Drawing.Size(119, 20);
            this.txtListenOn.TabIndex = 10;
            this.txtListenOn.Text = "localhost:2048";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(448, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "URL :";
            // 
            // Video_but
            // 
            this.Video_but.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Video_but.Location = new System.Drawing.Point(446, 207);
            this.Video_but.Name = "Video_but";
            this.Video_but.Size = new System.Drawing.Size(158, 47);
            this.Video_but.TabIndex = 12;
            this.Video_but.Text = "Video Chat";
            this.Video_but.UseVisualStyleBackColor = true;
            this.Video_but.Click += new System.EventHandler(this.Video_but_Click);
            // 
            // lbConnectedClients
            // 
            this.lbConnectedClients.FormattingEnabled = true;
            this.lbConnectedClients.Location = new System.Drawing.Point(448, 294);
            this.lbConnectedClients.Margin = new System.Windows.Forms.Padding(2);
            this.lbConnectedClients.Name = "lbConnectedClients";
            this.lbConnectedClients.Size = new System.Drawing.Size(160, 121);
            this.lbConnectedClients.TabIndex = 13;
            // 
            // setting_label
            // 
            this.setting_label.AutoSize = true;
            this.setting_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setting_label.Location = new System.Drawing.Point(494, 422);
            this.setting_label.Name = "setting_label";
            this.setting_label.Size = new System.Drawing.Size(47, 13);
            this.setting_label.TabIndex = 14;
            this.setting_label.Text = "Setting";
            // 
            // vport_label
            // 
            this.vport_label.AutoSize = true;
            this.vport_label.Location = new System.Drawing.Point(446, 442);
            this.vport_label.Name = "vport_label";
            this.vport_label.Size = new System.Drawing.Size(62, 13);
            this.vport_label.TabIndex = 16;
            this.vport_label.Text = "Video Port :";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(523, 439);
            this.txtIpAddress.Margin = new System.Windows.Forms.Padding(2);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(85, 20);
            this.txtIpAddress.TabIndex = 17;
            // 
            // accept_button
            // 
            this.accept_button.ForeColor = System.Drawing.Color.ForestGreen;
            this.accept_button.Location = new System.Drawing.Point(448, 260);
            this.accept_button.Name = "accept_button";
            this.accept_button.Size = new System.Drawing.Size(75, 23);
            this.accept_button.TabIndex = 21;
            this.accept_button.Text = "Accept";
            this.accept_button.UseVisualStyleBackColor = true;
            this.accept_button.Click += new System.EventHandler(this.accept_button_Click);
            // 
            // decline_button
            // 
            this.decline_button.ForeColor = System.Drawing.Color.Red;
            this.decline_button.Location = new System.Drawing.Point(529, 260);
            this.decline_button.Name = "decline_button";
            this.decline_button.Size = new System.Drawing.Size(75, 23);
            this.decline_button.TabIndex = 22;
            this.decline_button.Text = "Decline";
            this.decline_button.UseVisualStyleBackColor = true;
            this.decline_button.Click += new System.EventHandler(this.decline_button_Click);
            // 
            // cbCameras
            // 
            this.cbCameras.FormattingEnabled = true;
            this.cbCameras.Location = new System.Drawing.Point(451, 463);
            this.cbCameras.Margin = new System.Windows.Forms.Padding(2);
            this.cbCameras.Name = "cbCameras";
            this.cbCameras.Size = new System.Drawing.Size(159, 21);
            this.cbCameras.TabIndex = 23;
            // 
            // audio_port_label
            // 
            this.audio_port_label.AutoSize = true;
            this.audio_port_label.Location = new System.Drawing.Point(448, 491);
            this.audio_port_label.Name = "audio_port_label";
            this.audio_port_label.Size = new System.Drawing.Size(62, 13);
            this.audio_port_label.TabIndex = 24;
            this.audio_port_label.Text = "Audio Port :";
            // 
            // audio_port
            // 
            this.audio_port.Location = new System.Drawing.Point(523, 488);
            this.audio_port.Margin = new System.Windows.Forms.Padding(2);
            this.audio_port.Name = "audio_port";
            this.audio_port.Size = new System.Drawing.Size(85, 20);
            this.audio_port.TabIndex = 25;
            this.audio_port.UseWaitCursor = true;
            // 
            // cmbInputs
            // 
            this.cmbInputs.FormattingEnabled = true;
            this.cmbInputs.Location = new System.Drawing.Point(453, 512);
            this.cmbInputs.Margin = new System.Windows.Forms.Padding(2);
            this.cmbInputs.Name = "cmbInputs";
            this.cmbInputs.Size = new System.Drawing.Size(157, 21);
            this.cmbInputs.TabIndex = 26;
            // 
            // cmbCodecs
            // 
            this.cmbCodecs.FormattingEnabled = true;
            this.cmbCodecs.Location = new System.Drawing.Point(455, 537);
            this.cmbCodecs.Margin = new System.Windows.Forms.Padding(2);
            this.cmbCodecs.Name = "cmbCodecs";
            this.cmbCodecs.Size = new System.Drawing.Size(155, 21);
            this.cmbCodecs.TabIndex = 27;
            // 
            // setting_button
            // 
            this.setting_button.Location = new System.Drawing.Point(497, 563);
            this.setting_button.Name = "setting_button";
            this.setting_button.Size = new System.Drawing.Size(75, 23);
            this.setting_button.TabIndex = 28;
            this.setting_button.Text = "Apply";
            this.setting_button.UseVisualStyleBackColor = true;
            this.setting_button.Click += new System.EventHandler(this.setting_button_Click);
            // 
            // Chatroom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 686);
            this.Controls.Add(this.setting_button);
            this.Controls.Add(this.cmbCodecs);
            this.Controls.Add(this.cmbInputs);
            this.Controls.Add(this.audio_port);
            this.Controls.Add(this.audio_port_label);
            this.Controls.Add(this.cbCameras);
            this.Controls.Add(this.decline_button);
            this.Controls.Add(this.accept_button);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.vport_label);
            this.Controls.Add(this.setting_label);
            this.Controls.Add(this.lbConnectedClients);
            this.Controls.Add(this.Video_but);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtListenOn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.user_name);
            this.Controls.Add(this.btnDisconnect);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblChat);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Chatroom";
            this.Text = "Chatroom";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChat;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.TextBox user_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtListenOn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Video_but;
        private System.Windows.Forms.ListBox lbConnectedClients;
        private System.Windows.Forms.Label setting_label;
        private System.Windows.Forms.Label vport_label;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Button accept_button;
        private System.Windows.Forms.Button decline_button;
        private System.Windows.Forms.ComboBox cbCameras;
        private System.Windows.Forms.Label audio_port_label;
        private System.Windows.Forms.TextBox audio_port;
        private System.Windows.Forms.ComboBox cmbInputs;
        private System.Windows.Forms.ComboBox cmbCodecs;
        private System.Windows.Forms.Button setting_button;
    }
}

