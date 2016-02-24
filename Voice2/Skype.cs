using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SKYPE4COMLib;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Media;

namespace Voice2
{
    public partial class Skype : Form
    {
        String next;
        SpeechSynthesizer synth = new SpeechSynthesizer();

        public Skype()
        {
            InitializeComponent();
        }

    

        private void Skype_Shown(object sender, EventArgs e)
        {
            // Configure the audio output. 
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("You are now in Skype window");
            synth.Speak("Select an option");
            synth.Speak("Log in");
            synth.Speak("Add new user");
            synth.Speak("Close");
            synth.Speak("Back");
            synth.Speak("Speak now");

            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
            // Create SemanticResultValue objects that contain keyvalues and their codes.
            SemanticResultValue LogIn = new SemanticResultValue("Log In", "ORD");
            SemanticResultValue user = new SemanticResultValue("Add new User", "BOS");
            SemanticResultValue close = new SemanticResultValue("close", "MIA");
            SemanticResultValue back = new SemanticResultValue("back", "DFW");

            // Create a Choices object and add the SemanticResultValue objects, using
            // implicit conversion from SemanticResultValue to GrammarBuilder
            Choices services2 = new Choices();
            services2.Add(new Choices(new GrammarBuilder[] { LogIn, user, close, back }));

            // Build the phrase and add SemanticResultKeys.
            GrammarBuilder chooseService2 = new GrammarBuilder();

            chooseService2.Append(new SemanticResultKey("origin", services2));

            // Build a Grammar object from the GrammarBuilder.
            Grammar ser = new Grammar(chooseService2);
            ser.Name = "Services";

            // Add a handler for the LoadGrammarCompleted event.
            recognizer.LoadGrammarCompleted +=
              new EventHandler<LoadGrammarCompletedEventArgs>(recognizer_LoadGrammarCompleted);

            // Add a handler for the SpeechRecognized event.
            recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);

            // Load the grammar object to the recognizer.
            recognizer.LoadGrammarAsync(ser);

            // Set the input to the recognizer.
            recognizer.SetInputToDefaultAudioDevice();

            // Start recognition.
            recognizer.RecognizeAsync();
        }

        // Handle the LoadGrammarCompleted event.
        static void recognizer_LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {


        }

        // Handle the SpeechRecognized event.
        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null)
            {
                textBox1.Text += e.Result.Text + "\r\n";

                synth.Speak("Detected " + e.Result.Text);
                synth.Speak("Click to proceed");
                synth.Speak("Press any key to try again");

                if (e.Result.Text == "Log In")
                {
                    next = "Log In";
                }

                if (e.Result.Text == "Add new User")
                {
                    next = "Add new User";
                }

                if (e.Result.Text == "back")
                {
                    next = "back";
                }

                if (e.Result.Text == "close")
                {
                    next = "close";
                }

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            //directs to the next window
            if (next == "Log In")
            {
                //skype log in window
                Skype_LogIn form = new Skype_LogIn();
                this.Hide();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }

            if (next == "Add new User")
            {
                //skype new user
                Skype_AddNew form4 = new Skype_AddNew();
                this.Hide();
                form4.Closed += (s, args) => this.Close();
                form4.Show();
            }

            if (next == "back")
            {
                //go to the previous window
                Functions form1 = new Functions();
                this.Hide();
                form1.Closed += (s, args) => this.Close();
                form1.Show();
            }

            if (next == "close")
            {
                //close the application
                System.Windows.Forms.Application.Exit();
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

            SystemSounds.Beep.Play();
            //directs to the next window
            if (next == "Log In")
            {
                //skype log in window
                Skype_LogIn form = new Skype_LogIn();
                this.Hide();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }

            if (next == "Add new User")
            {
                //skype new user
                Skype_AddNew form4 = new Skype_AddNew();
                this.Hide();
                form4.Closed += (s, args) => this.Close();
                form4.Show();
            }

            if (next == "back")
            {
                //go to the previous window
                Functions form1 = new Functions();
                this.Hide();
                form1.Closed += (s, args) => this.Close();
                form1.Show();
            }

            if (next == "close")
            {
                //close the application
                System.Windows.Forms.Application.Exit();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            //directs to the next window
            if (next == "Log In")
            {
                //skype log in window
                Skype_LogIn form = new Skype_LogIn();
                this.Hide();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }

            if (next == "Add new User")
            {
                //skype new user
                Skype_AddNew form4 = new Skype_AddNew();
                this.Hide();
                form4.Closed += (s, args) => this.Close();
                form4.Show();
            }

            if (next == "back")
            {
                //go to the previous window
                Functions form1 = new Functions();
                this.Hide();
                form1.Closed += (s, args) => this.Close();
                form1.Show();
            }

            if (next == "close")
            {
                //close the application
                System.Windows.Forms.Application.Exit();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            //directs to the next window
            if (next == "Log In")
            {
                //skype log in window
                Skype_LogIn form = new Skype_LogIn();
                this.Hide();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }

            if (next == "Add new User")
            {
                //skype new user
                Skype_AddNew form4 = new Skype_AddNew();
                this.Hide();
                form4.Closed += (s, args) => this.Close();
                form4.Show();
            }

            if (next == "back")
            {
                //go to the previous window
                Functions form1 = new Functions();
                this.Hide();
                form1.Closed += (s, args) => this.Close();
                form1.Show();
            }

            if (next == "close")
            {
                //close the application
                System.Windows.Forms.Application.Exit();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            //directs to the next window
            if (next == "Log In")
            {
                //skype log in window
                Skype_LogIn form = new Skype_LogIn();
                this.Hide();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }

            if (next == "Add new User")
            {
                //skype new user
                Skype_AddNew form4 = new Skype_AddNew();
                this.Hide();
                form4.Closed += (s, args) => this.Close();
                form4.Show();
            }

            if (next == "back")
            {
                //go to the previous window
                Functions form1 = new Functions();
                this.Hide();
                form1.Closed += (s, args) => this.Close();
                form1.Show();
            }

            if (next == "close")
            {
                //close the application
                System.Windows.Forms.Application.Exit();
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();
            //directs to the next window
            if (next == "Log In")
            {
                //skype log in window
                Skype_LogIn form = new Skype_LogIn();
                this.Hide();
                form.Closed += (s, args) => this.Close();
                form.Show();
            }

            if (next == "Add new User")
            {
                //skype new user
                Skype_AddNew form4 = new Skype_AddNew();
                this.Hide();
                form4.Closed += (s, args) => this.Close();
                form4.Show();
            }

            if (next == "back")
            {
                //go to the previous window
                Functions form1 = new Functions();
                this.Hide();
                form1.Closed += (s, args) => this.Close();
                form1.Show();
            }

            if (next == "close")
            {
                //close the application
                System.Windows.Forms.Application.Exit();
            }
        }
    }
}
