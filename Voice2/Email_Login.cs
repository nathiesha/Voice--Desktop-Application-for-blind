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
using System.Speech.Recognition;
using System.Media;//speech recognition

namespace Voice2
{
    //email login window
    public partial class Email_Login : Form
    { 
        //speech synthesizer object for voice output
        SpeechSynthesizer synth = new SpeechSynthesizer();
        String next;//to store next window name

        //semantic vaues array to store usernames
        SemanticResultValue[] userNames = new SemanticResultValue[10];
        string[] lines2;//to store output from textfile
        int position;

        public Email_Login()
        {
            InitializeComponent();
            DataTable dt = GetDataTable(@"G:\c users\Desktop\sem 5\SEP\UserNames1.txt");//displaying user names in data tables
            dataGridView1.DataSource = dt.DefaultView;
          
           
        }

        //fetching data from files
        public static DataTable GetDataTable(string strFileName)
        {
            ADODB.Connection oConn = new ADODB.Connection();//creating a aodb connection
            oConn.Open("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + System.IO.Path.GetDirectoryName(strFileName) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\";", "", "", 0);
            string strQuery = "SELECT * FROM [" + System.IO.Path.GetFileName(strFileName) + "]";
            ADODB.Recordset rs = new ADODB.Recordset();
            System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter();
            DataTable dt = new DataTable();//new data table object
            rs.Open(strQuery, "Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + System.IO.Path.GetDirectoryName(strFileName) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\";",
                ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly, 1);
            adapter.Fill(dt, rs);
            return dt;
        }


        private void Form5_Shown(object sender, EventArgs e)
        {
            
             SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine();
             lines2 = System.IO.File.ReadAllLines(@"G:\c users\Desktop\sem 5\SEP\UserNames.txt");         
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Log In to Email");
            synth.Speak("Select a User name");

            Dictionary<string, int> names = new Dictionary<string, int>();

            
            int i=0;

            foreach (string line in lines2)
            {
                synth.Speak(line);
                userNames[i]=new SemanticResultValue(line,line);   
                i++;
            }

            Choices services2 = new Choices();
            GrammarBuilder[] gb=new GrammarBuilder[i];

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

            synth.Speak("Speak now");

        }


        // Handle the LoadGrammarCompleted event.
        static void recognizer_LoadGrammarCompleted(object sender, LoadGrammarCompletedEventArgs e)
        {
        }


        // Handle the SpeechRecognized event.
        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null)//if recognized input is not null
            {
                textBox1.Text += e.Result.Text + "\r\n";//set the input to text box

                
                //checking whether the input in one of user names
                for (int i = 0; i < userNames.Length; i++)
                {
                    //if the input in one of user names
                    if (e.Result.Text == lines2[i])
                    {
                        next = lines2[i];
                        position = i;
                        break;//stop iteration
                    }


                }
                
                synth.Speak("Detected " + e.Result.Text);
                synth.Speak("Click to proceed");
                synth.Speak("Press any key to try again");
            }
        }


        //if buton clicked
        private void button1_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();//play a beep sound
            //direct to the next window
            Email_Options form6 = new Email_Options(position, next);//new form email options
            this.Hide();//hide this form
            form6.Closed += (s, args) => this.Close();
            form6.Show();//show new form
        }

        //if label clicked
        private void label1_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();//play a beep sound
            //direct to the next window
            Email_Options form6 = new Email_Options(position, next);//new form email options
            this.Hide();//hide this form
            form6.Closed += (s, args) => this.Close();
            form6.Show();//show new form
        }

   


        //if data grid is clicked
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SystemSounds.Beep.Play();//play a beep sound
            //direct to the next window
            Email_Options form6 = new Email_Options(position, next);//new form email options
            this.Hide();//hide this form
            form6.Closed += (s, args) => this.Close();
            form6.Show();//show new form
        }

        //if user wants to try again
        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //direct to the same window
            Email_Login form6 = new Email_Login();//new form email login
            this.Hide();//hide this form
            form6.Closed += (s, args) => this.Close();
            form6.Show();//show new form
        }


    }
}
