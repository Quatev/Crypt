using Crypt.Helpers;
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
    public partial class Apperance : UserControl
    {
        public Apperance()
        {
            InitializeComponent();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplicationForm appl = this.ParentForm as ApplicationForm;
            if (appl == null) return;
            switch (guna2ComboBox1.SelectedIndex)
            {
                case 0:
                    appl.CurrentTheme = Themes.LightMode;
                    break;
                case 1:
                    appl.CurrentTheme = Themes.DarkMode;
                    break;
                case 2:
                    break;
                case 3:
                    break;

            }
        }

        private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            this.ParentForm.TopMost = guna2ToggleSwitch1.Checked;
        }
    }
}
