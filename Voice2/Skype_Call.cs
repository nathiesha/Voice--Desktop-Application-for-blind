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
using System.Media;

namespace Voice2
{
    public partial class Skype_Call : Form
    {
        Call call;
        String skypeID;
        public Skype_Call()
        {
            
        }

        public Skype_Call(String ID)
        {
            InitializeComponent();
            skypeID = ID;
            SystemSounds.Beep.Play();//play a beep sound
            InitializeComponent();
            SKYPE4COMLib.Skype skype;
            skype = new SKYPE4COMLib.Skype();
            call = skype.PlaceCall(skypeID);
 }

        private void button1_Click(object sender, EventArgs e)
        {

            call.Finish();
            
        }


    }
}
