namespace WcfChatRoom
{
    partial class video_chat_room
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.me_label = new System.Windows.Forms.Label();
            this.client_label = new System.Windows.Forms.Label();
            this.start_button = new System.Windows.Forms.Button();
            this.close_button = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // groupBox
            // 
            this.groupBox.Location = new System.Drawing.Point(497, 56);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(354, 241);
            this.groupBox.TabIndex = 13;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Live camera";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(15, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(354, 241);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Live camera";
            // 
            // me_label
            // 
            this.me_label.AutoSize = true;
            this.me_label.Location = new System.Drawing.Point(12, 21);
            this.me_label.Name = "me_label";
            this.me_label.Size = new System.Drawing.Size(22, 13);
            this.me_label.TabIndex = 15;
            this.me_label.Text = "Me";
            // 
            // client_label
            // 
            this.client_label.AutoSize = true;
            this.client_label.Location = new System.Drawing.Point(494, 21);
            this.client_label.Name = "client_label";
            this.client_label.Size = new System.Drawing.Size(33, 13);
            this.client_label.TabIndex = 16;
            this.client_label.Text = "Client";
            // 
            // start_button
            // 
            this.start_button.Location = new System.Drawing.Point(396, 125);
            this.start_button.Name = "start_button";
            this.start_button.Size = new System.Drawing.Size(75, 23);
            this.start_button.TabIndex = 17;
            this.start_button.Text = "Start";
            this.start_button.UseVisualStyleBackColor = true;
            this.start_button.Click += new System.EventHandler(this.start_button_Click);
            // 
            // close_button
            // 
            this.close_button.Location = new System.Drawing.Point(396, 178);
            this.close_button.Name = "close_button";
            this.close_button.Size = new System.Drawing.Size(75, 23);
            this.close_button.TabIndex = 18;
            this.close_button.Text = "Close";
            this.close_button.UseVisualStyleBackColor = true;
            this.close_button.Click += new System.EventHandler(this.close_button_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 309);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(867, 97);
            this.lblStatus.TabIndex = 19;
            // 
            // video_chat_room
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 406);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.close_button);
            this.Controls.Add(this.start_button);
            this.Controls.Add(this.client_label);
            this.Controls.Add(this.me_label);
            this.Controls.Add(this.groupBox1);
            this.Name = "video_chat_room";
            this.Text = "Video Chat Room";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.video_chat_room_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label me_label;
        private System.Windows.Forms.Label client_label;
        private System.Windows.Forms.Button start_button;
        private System.Windows.Forms.Button close_button;
        private System.Windows.Forms.Label lblStatus;
    }
}