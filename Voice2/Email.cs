using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Synthesis;//speech output
using System.Speech.Recognition;//speech recognition
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Voice2
{
    //email window
    public partial class Email : Form
    {
        String next;//string to store nxt window name

        //initializibg the speech synthesizer
        SpeechSynthesizer synth = new SpeechSynthesizer();

        public Email()
        {
            InitializeComponent();
        }



        private void Form3_Shown(object sender, EventArgs e)
        {

            // Configure the audio output. 
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Email service");
            synth.Speak("Select an option");
            synth.Speak("Log in");
            synth.Speak("Add new user");
            synth.Speak("Back");
            synth.Speak("Close");
            synth.Speak("Speak now");


            //new recognition engine
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

        private void button1_Click(object sender, EventArgs e)
        {
            clickMe(next);
        }

        public String clickMe(String next)
        {
            //when the button clicked
            SystemSounds.Beep.Play();//play a beep sound

            //if log in selected
            if (next == "Log In")
            {
                Email_Login form5 = new Email_Login();//new email login window
                this.Hide();//hide this form
                form5.Closed += (s, args) => this.Close();
                form5.Show();//show next form
                return "Log In";
            }
            //add new user
            if (next == "Add new User")
            {
                Email_AddNew form4 = new Email_AddNew();//new user window
                this.Hide();//hide this form
                form4.Closed += (s, args) => this.Close();
                form4.Show();//show next
                return "Add new User";
            }

            if (next == "back")
            {//directs to the previous window
                Functions form1 = new Functions();
                this.Hide();//hide this form
                form1.Closed += (s, args) => this.Close();
                form1.Show();//show previous window
                return "back";
            }

            if (next == "close")
            {

                Application.Exit();//exit the application
                return "close";
            }

            else return "error";
        }
        // Handle the LoadGrammarCompleted event.
        static void recognizer_LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {


        }

        // Handle the SpeechRecognized event.
        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null)//if recgnized speech is not null
            {
                //set that in text box
                textBox1.Text += e.Result.Text + "\r\n";

                //voice output
                synth.Speak("Detected " + e.Result.Text);
                synth.Speak("Click to proceed");
                synth.Speak("Press any key to try again");

                //setting up the next value
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

        private void label1_Click(object sender, EventArgs e)
        {
            clickMe(next);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            clickMe(next);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            clickMe(next);
        }

        private void label4_Click(object sender, EventArgs e)
        {
            clickMe(next);
        }

        //if user wants to try again
        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            key();
        }

        public String key()
        {
            try {
                SystemSounds.Beep.Play();//play a sound when button is clicked
            Email form3 = new Email();//new email window
            this.Hide();//hide this window
            form3.Closed += (s, args) => this.Close();
            form3.Show();//show email window 
            return "yes";
            }
            catch 
            {
                return "no";
            }
        
        
        }



    }
}
