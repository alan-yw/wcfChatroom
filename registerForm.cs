using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AuthenticationLib;

namespace WcfChatRoom
{
    public partial class registerForm : Form
    {
        public registerForm()
        {
            InitializeComponent();
            PersistingUserData.SetFileName("userData.xml");
            
        }

        private void reg_button_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterUser(username.Text, pwd.Text);
                message.Text = "Successfully Register";

            }
            catch (Exception)
            {
                message.Text = "Failed to Register";
            }
        }

        private void login_button_Click(object sender, EventArgs e)
        {
            try
            {
                AttemptAuthentication(username.Text, pwd.Text);
                this.Hide();
                Chatroom chat = new Chatroom(username.Text);
                chat.ShowDialog();
                username.Text = "";
                pwd.Text = "";
                message.Text = "";
                this.Show();
            }
            catch (Exception)
            {
                message.Text = "Failed to Login";
            }
            
        }

        private static void AttemptAuthentication(string userName, string nonHashedPassword)
        {
            

            if (Authentication.AuthenticateUser(userName, nonHashedPassword))
            { Console.WriteLine("  ~~~ CORRECT ~~~"); }
            else
            {
                Console.WriteLine("  !!! FAILED !!!");
                Exception e = new Exception("Fail");
                throw e;
            }
        }

        private static void RegisterUser(string userName, string nonHashedPassword)
        {
            Console.WriteLine("### Attempting user registration for user {0} ###", userName);

            if (Authentication.RegisterUser(userName, nonHashedPassword))
            { Console.WriteLine("  ~~~ CORRECT ~~~"); }
            else
            {
                Console.WriteLine("  !!! FAILED !!!");
                Exception e = new Exception("Fail");
                throw e;
            }

        }

       
    }
}
