using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;//to save message as a wave file
using System.Speech.Synthesis;//to give speech output
using System.Net.Mail;
using System.Media;//to send mail

namespace Voice2
{
    //this window records the user message, save it and attach it and send to the recipient
    public partial class Email_comRecord : Form
    {
        public WaveIn waveSource = null;//audio recording
        public WaveFileWriter waveFile = null;//wavefile writer
        SpeechSynthesizer synth = new SpeechSynthesizer();

        //variables needed to send the email
        String email;
        String password;
        String rec_email; 
        String recipient;

        public Email_comRecord()
        {
            InitializeComponent();
        }

        //constructor
        public Email_comRecord(String email,String password, String rec_email,String rec)
        {
            InitializeComponent();
            this.rec_email = rec_email;
            this.email=email;
            this.password = password;
            this.recipient = rec;
        }

        private void Email_comRecord_Shown(object sender, EventArgs e)
        {
            synth.Speak("Your message to " + recipient + "is now getting recorded");
            synth.Speak("Click when you finish");

            //new wave source
            waveSource = new WaveIn();
            //convert to wave format
            waveSource.WaveFormat = new WaveFormat(44100, 1);

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            //save the audio file
            waveFile = new WaveFileWriter(@"C:\Temp\Test0005.wav", waveSource.WaveFormat);
            synth.Speak("Start now");
            waveSource.StartRecording();//starts recording the message

        }

     void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }

        // event where the recording has syopped
        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            //dispose the wave source
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }
            //dispose the wave file
           if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
            
            //send the email
            sendEmail(rec_email,email,password);

        }




        public String sendEmail(String recEmail,String email,String password)
        {
            //sending email with the attachment
          
                synth.Speak("Sending the email");
                MailMessage mail = new MailMessage();//mail message object
                //create a smtp client
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(email);
                mail.To.Add(rec_email);
                mail.Subject = "Test Mail - 1";
                mail.Body = "mail with attachment";

                System.Net.Mail.Attachment attachment;
                //attach the audio
                attachment = new System.Net.Mail.Attachment(@"C:\Temp\Test0005.wav");
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                synth.Speak("Email is successfully sent");
                return "yes";
            
  
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //stop recordng when the buton is pressed
            waveSource.StopRecording();
            SystemSounds.Beep.Play();//play a beep sound
        }


        private void label1_Click(object sender, EventArgs e)
        {          
            
            //stop recordng when the buton is pressed
            waveSource.StopRecording();
            SystemSounds.Beep.Play();//play a beep sound

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //stop recordng when the buton is pressed
            waveSource.StopRecording();
            SystemSounds.Beep.Play();//play a beep sound
        }

}}

    

