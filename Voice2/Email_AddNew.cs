using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voice2
{
    public partial class Email_AddNew : Form
    {
        //initializibg the speech synthesizer
        SpeechSynthesizer synth = new SpeechSynthesizer();


        public Email_AddNew()
        {
            InitializeComponent();
        }


        private void Email_AddNew_Shown(object sender, EventArgs e)
        {
            synth.Speak("Enter new user name, email and password");
        }
  

 

        private void button1_Click(object sender, EventArgs e)
        {
            
            //directs the user to the email  window
            SystemSounds.Beep.Play();//play a beep sound
            Email form8 = new Email();
            this.Hide();//hide this window
            form8.Closed += (s, args) => this.Close();
            form8.Show();//show next
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();//play a beep sound
            String userName;
            String email;
            String password;

            //take data in text boxes
            userName = textBox1.Text;
            email = textBox2.Text;
            password = textBox3.Text;

            //add data to the files
            System.IO.File.WriteAllText(@"G:\c users\Desktop\sem 5\SEP\Usernames.txt", userName);
            System.IO.File.WriteAllText(@"G:\c users\Desktop\sem 5\SEP\Passwords.txt", password);
            System.IO.File.WriteAllText(@"G:\c users\Desktop\sem 5\SEP\Emails.txt", email);

            synth.Speak("User details added to the system. Click to proceed");
        }



    }
}
