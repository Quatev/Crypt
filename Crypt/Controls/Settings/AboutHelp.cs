using Crypt.Helpers.Dll_Comunication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypt.Controls.Settings
{
    public partial class AboutHelp : UserControl
    {
        public AboutHelp()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            PipeLineServerThingy.StartPipeServer();
        }
    }
}
