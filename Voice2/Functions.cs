using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis; //speech output
using System.Speech.Recognition;
using System.Media;//speech recognition

namespace Voice2
{
    public partial class Functions : Form
    {
        // Initialize a new instance of the SpeechSynthesizer.
        SpeechSynthesizer synth = new SpeechSynthesizer();
        SpeechRecognizer recog = new SpeechRecognizer();
        String next;
            
           
        public Functions()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_shown(object sender, EventArgs e)
        {
             // Configure the audio output. 
                synth.SetOutputToDefaultAudioDevice();
                synth.Speak("Select an option");
                synth.Speak("Email");
                synth.Speak("Skype");
                synth.Speak("Back");
                synth.Speak("Close");
                synth.Speak("Speak now");



                //initialize speech recognizer
                SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
                // Create SemanticResultValue objects that contain keyvalues and their codes.
                SemanticResultValue email = new SemanticResultValue("email", "ORD");
                SemanticResultValue skype = new SemanticResultValue("skype", "BOS");
                SemanticResultValue close = new SemanticResultValue("close", "MIA");
                SemanticResultValue back = new SemanticResultValue("back", "DFW");

                // Create a Choices object and add the SemanticResultValue objects, using
                // implicit conversion from SemanticResultValue to GrammarBuilder
                Choices services = new Choices();
                services.Add(new Choices(new GrammarBuilder[] { email, skype, close, back }));

                // Build the phrase and add SemanticResultKeys.
                GrammarBuilder chooseService = new GrammarBuilder();

                chooseService.Append(new SemanticResultKey("origin", services));

                // Build a Grammar object from the GrammarBuilder.
                Grammar ser = new Grammar(chooseService);
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

                //set the recognized value to text boz
                textBox1.Text += e.Result.Text + "\r\n";

                //audio gudance
                synth.Speak("Detected " + e.Result.Text);
                synth.Speak("Click to proceed");
                synth.Speak("press any key to try again");

                //setting up the next variable to determine next window
                if (e.Result.Text == "email")
                {
                    next = "email";
                }

                if (e.Result.Text == "skype")
                {
                    next = "skype";
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
        

         public String clicker(String next)
         {
             //direct to next window

             SystemSounds.Beep.Play();//play a sound when button is clicked

             //direct to the email window
             if (next == "email")
             {
                 Email form3 = new Email();//new email window
                 this.Hide();//hide this window
                 form3.Closed += (s, args) => this.Close();
                 form3.Show();//show email window
                 return "email";
             }
             //direct to skype window
             if (next == "skype")
             {
                 Skype form4 = new Skype();//new skype window
                 this.Hide();//hide this window
                 form4.Closed += (s, args) => this.Close();
                 form4.Show();//show the skype window
                 return "skype";
             }

             //direct to the previous window
             if (next == "back")
             {
                 welcome form1 = new welcome();//new welcome window
                 this.Hide();//hide this window
                 form1.Closed += (s, args) => this.Close();
                 form1.Show();//show welcome window
                 return "back";
             }
             //close the application
             if (next == "close")
             {
                 
                 Application.Exit();//exit application
                 return "close";
             }

             else
             { return "null"; }
         }
        
        private void button1_Click(object sender, EventArgs e)
         {
             clicker(next);
        }

        private void label2_Click(object sender, EventArgs e)
        {//direct to next window

            clicker(next);
            
        }


        private void label3_Click(object sender, EventArgs e)
        {//direct to next window

            clicker(next);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            //direct to next window

            clicker(next);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            clicker(next);
        }

        //when a key press detected
        public String Key()
        {
            try
            {
                //directs to the same window
                Functions form2 = new Functions();//new welcome window
                this.Hide();//hide this window
                form2.Closed += (s, args) => this.Close();
                form2.Show();//show welcome window
                return "yes";

            }

            catch 
            {
                return "no";
            }
        }

        //if user wants to try again

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            Key();//call the key method
           
        }

        private void Functions_Load(object sender, EventArgs e)
        {

        }

      
    }
}
