using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADODB;//to fetch data from files
using System.IO;
using Limilabs.Mail;//email
using Limilabs.Client.IMAP;
using System.Speech.Synthesis;
using System.Media;//imap client to connect to email


namespace Voice2
{
    //window that displays inbox
    public partial class Email_Inbox : Form
    {
        //speech synthesizer object for voice output
        SpeechSynthesizer synth = new SpeechSynthesizer();

        String useremail;
        String password;
        String username;
        int number;
        String[] subject = new String[10];//to store subjects of  the messages
        String[] body = new String[10];//to store body of  the messages

        //constructor with email and password
        public Email_Inbox(String email,String password,String uname,int num)
        {
            InitializeComponent();
            useremail = email;//assigning user name and password
            this.password = password;
            number = num;
            username = uname;
            GetInbox(useremail,password);//call get inbox method
        }


        private void Email_Inbox_Shown(object sender, EventArgs e)
        {
            synth.Speak("You are now in the inbox window");
        }
        //method that fetches emails from inbox
        public void GetInbox(String email,String pass)
        {
            //create imap object
            Imap imap = new Imap();
            {
                imap.ConnectSSL("imap.gmail.com");
                imap.Login(email, pass);//user email and password
                imap.SelectInbox();//select the inbox
                List<long> uids = imap.Search(Flag.Unseen);//load unseen emails
                String[] lines=new String[100];//new string array to store emails
                int i=0;

                foreach (long uid in uids)
                {
                    //get each email
                    var eml = imap.GetMessageByUID(uid);
                    IMail message = new MailBuilder()
                        .CreateFromEml(eml);


                   //add the emails to the array
                    subject[i] = message.Subject;//storre subjects of emails
                    body[i] = message.Text;//store text of emails
                    lines[i] = message.Subject+"-"+message.Text;//store message in an array
                    i++;
                }

                //write the emails in to a text file
                System.IO.File.WriteAllLines(@"G:\c users\Desktop\sem 5\SEP\Inbox.txt", lines);//put a path of a folder in your computer


                imap.Close(true);//close imap

                //read out each email subject and body
                for(int j=0;j<i;j++)
                {
                    synth.Speak("Email subject");
                    synth.Speak(subject[j]);
                    synth.Speak("Email Text");
                    synth.Speak(body[j]);

                }

                synth.Speak("Click to read the inbox again");
                synth.Speak("Press any key to go back to the previous window");
            }
        }

        //display emails in the data grid view
        public static DataTable GetDataTable(string strFileName)
        {
            ADODB.Connection oConn = new ADODB.Connection();
            oConn.Open("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + System.IO.Path.GetDirectoryName(strFileName) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\";", "", "", 0);
            string strQuery = "SELECT * FROM [" + System.IO.Path.GetFileName(strFileName) + "]";
            ADODB.Recordset rs = new ADODB.Recordset();
            System.Data.OleDb.OleDbDataAdapter adapter = new System.Data.OleDb.OleDbDataAdapter();
            DataTable dt = new DataTable();
            rs.Open(strQuery, "Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + System.IO.Path.GetDirectoryName(strFileName) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\";",
                ADODB.CursorTypeEnum.adOpenForwardOnly, ADODB.LockTypeEnum.adLockReadOnly, 1);
            adapter.Fill(dt, rs);
            return dt;
        }


//importing emails from the text file
        private void Import()
        {
            try
            {
                DataTable dt = GetDataTable(@"G:\c users\Desktop\sem 5\SEP\Inbox.txt");
                dataGridView1.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                synth.Speak("Error in accessing inbox.Please try again");
                //directs the user to previous window
                Email_Options form8 = new Email_Options(number,username);
                this.Hide();//hide this window
                form8.Closed += (s, args) => this.Close();
                form8.Show();
            }

        }


        private void Email_Inbox_Load(object sender, EventArgs e)
        {
            Import();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //directs the user to the same window
            SystemSounds.Beep.Play();//play a beep sound
            Email_Inbox form8 = new Email_Inbox(useremail,password,username,number);
            this.Hide();//hide this window
            form8.Closed += (s, args) => this.Close();
            form8.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //directs the user to the same window
            SystemSounds.Beep.Play();//play a beep sound
            Email_Inbox form8 = new Email_Inbox(useremail, password,username,number);
            this.Hide();//hide this window
            form8.Closed += (s, args) => this.Close();
            form8.Show();
            
        }

        //if user presses a key
        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            SystemSounds.Beep.Play();//play a beep sound
            //directs the user to the previous window
            Email_Options form8 = new Email_Options(number,username);
            this.Hide();//hide this window
            form8.Closed += (s, args) => this.Close();
            form8.Show();
        }





    }
}
