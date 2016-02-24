using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Voice2
{
    public partial class Skype_Contact : Form
    {
        SpeechSynthesizer synth = new SpeechSynthesizer();
        SemanticResultValue[] userNames = new SemanticResultValue[10];//conact names semantic values
        String call;

        string[] lines2;//to store contact names
        string[] lines3;
        String next;
        int position;//temp variables
        String contactSkypeID;//recipients email
        String user; //user name 
        int number;

        String[] contacts=new String[10];

        public Skype_Contact(int num,String username)
        {
            InitializeComponent();
 
            user = username;
            number = num;
  
        }



        private void Skype_Contact_Shown(object sender, EventArgs e)
        {
            //initialize speech enine
            SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();


            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Select a contact name to take the skype call");

            //fetch contact names from file
            lines2 = System.IO.File.ReadAllLines(@"G:\c users\Desktop\sem 5\SEP\SkypeContactNames.txt");


            //adding the contact names to the dictionary
            Dictionary<string, int> names = new Dictionary<string, int>();


            int i = 0;

            //read out each contact name
            foreach (string line in lines2)
            {
                synth.Speak(line);
                userNames[i] = new SemanticResultValue(line, line);   //add the names to semantic values
                textBox1.Text = line;//set text in the text box
                i++;
            }
            textBox1.Text = "";

            synth.Speak("Speak now");

            Choices services2 = new Choices();
            GrammarBuilder[] gb = new GrammarBuilder[i];//new grammar builder array

            //add the user names to the grammar builder array
            for (int j = 0; j < i; j++)
            {
                gb[j] = userNames[j];
            }


            services2.Add(new Choices(gb));

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
                call = e.Result.Text;
                textBox1.Text = e.Result.Text + "\r\n";//set the name on text box

                //identify which contact name is told by user
                for (int i = 0; i < userNames.Length; i++)
                {
                    if (e.Result.Text == lines2[i])
                    {
                        next = lines2[i];
                        position = i;
                        break;
                    }

                }

                //fetch all the contact emails from file
                lines3 = System.IO.File.ReadAllLines(@"G:\c users\Desktop\sem 5\SEP\SkypeContactIDs.txt");
                contactSkypeID = lines3[position];//get the relavant email

                textBox1.Text = contactSkypeID;//set it on text box
                synth.Speak("Detected " + e.Result.Text);
                synth.Speak("Click to take the Skype call");
                synth.Speak("Or press any key to try again");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //message should be recorded
            //directs to the message recording window
            synth.Speak("Calling "+call);
            synth.Speak("Click to cut the call");
            System.Media.SystemSounds.Beep.Play();
           Skype_Call form8 = new Skype_Call( contactSkypeID);//passing the recipients email and user email and password
            this.Hide();//hide this
            form8.Closed += (s, args) => this.Close();
            form8.Show();//show next window
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //user wants to try again
            //directs to the same window
            System.Media.SystemSounds.Beep.Play();
            Skype_Contact form8 = new Skype_Contact(number, user );//passing the recipients email and user email and password
            this.Hide();//hide this
            form8.Closed += (s, args) => this.Close();
            form8.Show();//show next window
        }
    }
}
