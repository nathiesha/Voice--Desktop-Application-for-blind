using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;//speech output
using System.Speech.Recognition;//speech recognition
using System.Media;//play sound

namespace Voice2
{
    //the welcome screen
    public partial class welcome : Form
    
    {

           // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer();
           

        public welcome()
        {
            //initialize the form
            InitializeComponent();
            
        }



        private void Form1_Shown(Object sender, EventArgs e)
        {
            //user guidance voice -welcome 
            welcomeSpeeech();
        }

        //speech guidance
        public String welcomeSpeeech()
        {
            try
            { // Configure the audio output. 
                synth.SetOutputToDefaultAudioDevice();
                // Speak a string.
                synth.Speak("Welcome to Easy Connect");
                synth.Speak("Click to get started");
                return "yes";
            }

            catch {
                return "no";
            }
        }


        //if label clicked, directing to the next window
        private void label2_Click(object sender, EventArgs e)
        {
            clicker();
        }

        public int MyTest(int i, int j)
        {
            return i + j;
        }

        //method called when a mouse click detected
        public String clicker()
        {
            try
            {
                //direct to next window
                SystemSounds.Beep.Play();//play a beep sound
                Functions form2 = new Functions(); //create a new form2 object
                this.Hide();//hide this window
                form2.Closed += (s, args) => this.Close();
                form2.Show();//show next window
                return "yes";
            }

            catch 
            {
                return "no";
            }
        
        }

        //when button clicked, directing to the next window
        private void button1_Click(object sender, EventArgs e)
        {
            clicker();
        }


        //if label clicked, directing to the next window
        private void label4_Click(object sender, EventArgs e)
        {

            clicker();
        }

        //if label clicked, directing to the next window
        private void label1_Click_1(object sender, EventArgs e)
        {
            clicker();
        }
    }
}
