using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ADODB;
using NUnit;
using System.IO;
using System.Net.Mime;
using System.Net.Mail;
using Limilabs.Mail;//email client
using Limilabs.Client.IMAP;
using System.Media;//IMpa client
namespace Voice2
{
    public partial class Email_Sent : Form
    {

        String userEmail;//to store user email
        String password;//user password

        public Email_Sent(String email,String password,String username,int num)
        {
            InitializeComponent();
            userEmail = email;
            this.password = password;
            GetSent();//method to fetch sent items
        }

        //method to get sent emails
        public void GetSent()
        {
            //new Imap object
            Imap imap = new Imap();
            {
                imap.ConnectSSL("imap.gmail.com");
                imap.Login(userEmail, password);
                imap.SelectInbox();
                List<long> uids = imap.Search(Flag.Unseen);
                String[] lines = new String[100];
                int i = 0;

                    
                                imap.Select("[Gmail]/Sent Mail");


                                List<long> uids2 = imap.GetAll();

                                foreach (long uid in uids2)
                                {

                                    IMail email = new MailBuilder()

                                        .CreateFromEml(imap.GetMessageByUID(uid));

                                     lines[i] = email.Subject +"-"+ email.Text;
                                     i++;
                                }

                                System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\Sent.txt", lines);
                imap.Close(true);
            }
        }

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



        private void Import()
        {
            try
            {
                DataTable dt = GetDataTable(@"G:\c users\Desktop\sem 5\SEP\Inbox.txt");
                dataGridView1.DataSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }


        private void Email_Sent_Load(object sender, EventArgs e)
        {
            Import();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SystemSounds.Beep.Play();//play a beep sound
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SystemSounds.Beep.Play();//play a beep sound
        }
    }
}
