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
    public  class SpeechRecognizer
    {
        // Initialize a new instance of the SpeechSynthesizer.
        SpeechSynthesizer synth = new SpeechSynthesizer();
        static String next;

        public SpeechRecognizer()
        {
            
        }

       

        public String display()
        {
            try
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
                return next;
            }

            catch
            {
                return next;
            }

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






        internal string getNext()
        {
            return next;
        }
    }
}
