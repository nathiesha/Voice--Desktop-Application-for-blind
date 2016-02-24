using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Speech.Recognition;//for speech recognizing
using System.Speech.Synthesis;//for audio output
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Voice2
{
    public partial class Email_Options : Form
    {
        String username;//to store username
        String password;//to store password
        String email;//to store email address
        int num;

        //speech output object
        SpeechSynthesizer synth = new SpeechSynthesizer();
        String next;//to store next window name
        SemanticResultValue[] userNames = new SemanticResultValue[10];//user names

        //initializing the speech recognition engine
        SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();

        public Email_Options()
        {
            InitializeComponent();
        }

        //constructor with user name and array position as arguments
        public Email_Options(int number, String line)
        {
            InitializeComponent();
            this.Text += line;//set user name on text box
            username = line;
            num = number;
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            //string arrays to store passwords and emails
            string[] emails;
            string[] pass;

            //read the files and get emails and passwords
            emails = System.IO.File.ReadAllLines(@"G:\c users\Desktop\sem 5\SEP\Emails.txt");
            pass = System.IO.File.ReadAllLines(@"G:\c users\Desktop\sem 5\SEP\Passwords.txt");
            email = emails[num];//get the users email adres and password
            password = pass[num];
        

            synth.SetOutputToDefaultAudioDevice();
        }

        private void Form7_Shown(object sender, EventArgs e)
        {

            synth.Speak("You are logged In as " + username);
            synth.Speak("Select an option");
            synth.Speak("Compose");
            synth.Speak("Inbox");
            synth.Speak("Sent mail");
            synth.Speak("Back");
            synth.Speak("Close");
            synth.Speak("Speak now");

            //creating semantic values for the keywords used in this window
            SemanticResultValue compose = new SemanticResultValue("Compose", "Compose");
            SemanticResultValue inbox = new SemanticResultValue("inbox", "inbox");
            SemanticResultValue Sent = new SemanticResultValue("Sent", "Sent mail");
            SemanticResultValue Back = new SemanticResultValue("Back", "back");
            SemanticResultValue close = new SemanticResultValue("close", "close");

            Choices services = new Choices();
            services.Add(new Choices(new GrammarBuilder[] { compose, inbox, Sent, Back, close }));
            
            GrammarBuilder chooseService = new GrammarBuilder();
            chooseService.Append(new SemanticResultKey("origin", services));
            Grammar ser = new Grammar(chooseService);


            recognizer.SpeechRecognized +=
             new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            // Load the grammar object to the recognizer.
            recognizer.LoadGrammarAsync(ser);

            // Set the input to the recognizer.
            recognizer.SetInputToDefaultAudioDevice();

            // Start recognition.
            recognizer.RecognizeAsync();

        }

    //when speech is recognized

        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null)
            {
                textBox1.Text += e.Result.Text + "\r\n";//set the recoognized value on text box

                synth.Speak("Detected " + e.Result.Text);
                synth.Speak("Click to proceed");
                synth.Speak("Press any key to try again");
                next = e.Result.Text;
            }
        }

        //if button clicked
        private void button1_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();//button click notification

            if (next == "Compose")
            {//new compose window
                Email_Compose form8 = new Email_Compose(email,password,username,num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();
            
            }

            if (next == "inbox")
            {//direxts to the inbox window
                Email_Inbox form8 = new Email_Inbox(email,password,username,num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "Sent mail")
            {//directs to the sent items window
                Email_Sent form8 = new Email_Sent(email,password,username,num);
                this.Hide();//hide this
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "back")
            {
                //direts to the previous window
                Email_Login form8 = new Email_Login();
                this.Hide();
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "close")
            {
                Application.Exit();//close the application
            }
        }

        //if label clicked
        private void label1_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();//button click notification

            if (next == "Compose")
            {//new compose window
                Email_Compose form8 = new Email_Compose(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "inbox")
            {//direxts to the inbox window
                Email_Inbox form8 = new Email_Inbox(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "Sent mail")
            {//directs to the sent items window
                Email_Sent form8 = new Email_Sent(email, password, username, num);
                this.Hide();//hide this
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "back")
            {
                //direts to the previous window
                Email_Login form8 = new Email_Login();
                this.Hide();
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "close")
            {
                Application.Exit();//close the application
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

            SystemSounds.Beep.Play();//button click notification

            if (next == "Compose")
            {//new compose window
                Email_Compose form8 = new Email_Compose(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "inbox")
            {//direxts to the inbox window
                Email_Inbox form8 = new Email_Inbox(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "Sent mail")
            {//directs to the sent items window
                Email_Sent form8 = new Email_Sent(email, password, username, num);
                this.Hide();//hide this
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "back")
            {
                //direts to the previous window
                Email_Login form8 = new Email_Login();
                this.Hide();
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "close")
            {
                Application.Exit();//close the application
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();//button click notification

            if (next == "Compose")
            {//new compose window
                Email_Compose form8 = new Email_Compose(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "inbox")
            {//direxts to the inbox window
                Email_Inbox form8 = new Email_Inbox(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "Sent mail")
            {//directs to the sent items window
                Email_Sent form8 = new Email_Sent(email, password, username, num);
                this.Hide();//hide this
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "back")
            {
                //directs to the previous window
                Email_Login form8 = new Email_Login();
                this.Hide();
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "close")
            {
                Application.Exit();//close the application
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();//button click notification

            if (next == "Compose")
            {//new compose window
                Email_Compose form8 = new Email_Compose(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "inbox")
            {//direxts to the inbox window
                Email_Inbox form8 = new Email_Inbox(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "Sent mail")
            {//directs to the sent items window
                Email_Sent form8 = new Email_Sent(email, password, username, num);
                this.Hide();//hide this
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "back")
            {
                //direts to the previous window
                Email_Login form8 = new Email_Login();
                this.Hide();
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "close")
            {
                Application.Exit();//close the application
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();//button click notification

            if (next == "Compose")
            {//new compose window
                Email_Compose form8 = new Email_Compose(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "inbox")
            {//direxts to the inbox window
                Email_Inbox form8 = new Email_Inbox(email, password, username, num);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }

            if (next == "Sent mail")
            {//directs to the sent items window
                Email_Sent form8 = new Email_Sent(email, password, username, num);
                this.Hide();//hide this
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "back")
            {
                //direts to the previous window
                Email_Login form8 = new Email_Login();
                this.Hide();
                form8.Closed += (s, args) => this.Close();
                form8.Show();

            }
            if (next == "close")
            {
                Application.Exit();//close the application
            }
        }

        //if any key pressed
        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //direts to the same window
            Email_Options form8 = new Email_Options();
            this.Hide();
            form8.Closed += (s, args) => this.Close();
            form8.Show();

        }


    }
}
