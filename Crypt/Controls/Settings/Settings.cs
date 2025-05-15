using Guna.UI2.WinForms;
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
    public partial class Settings : UserControl
    {
        public Guna2Button SelectedTab = null;
        public Guna2Button oldSelectedTab = null;

        public Apperance ApperanceTab = new Apperance();
        public AboutHelp AboutHelpTab = new AboutHelp();
        public Game GameTab = new Game();

        public void UpdateSelectedTab(Guna2Button newTab)
        {
            var appForm = this.ParentForm as ApplicationForm;
            if (appForm == null) return;

            if (oldSelectedTab != null)
                oldSelectedTab.FillColor = appForm.CurrentTheme.Button;

            if (newTab != null)
                newTab.FillColor = appForm.CurrentTheme.SelectedTab;

            oldSelectedTab = newTab;
            SelectedTab = newTab;
        }

        public Settings()
        {
            InitializeComponent();

            SelectedTab = guna2Button1;
            guna2Button1_Click(guna2Button1, null);
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!TabHolder.Controls.Contains(ApperanceTab) || SelectedTab != sender as Guna2Button)
            {
                TabHolder.Controls.Clear();
                TabHolder.Controls.Add(ApperanceTab);
                UpdateSelectedTab(sender as Guna2Button);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (!TabHolder.Controls.Contains(AboutHelpTab) || SelectedTab != sender as Guna2Button)
            {
                TabHolder.Controls.Clear();
                TabHolder.Controls.Add(AboutHelpTab);
                UpdateSelectedTab(sender as Guna2Button);
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (!TabHolder.Controls.Contains(GameTab) || SelectedTab != sender as Guna2Button)
            {
                TabHolder.Controls.Clear();
                TabHolder.Controls.Add(GameTab);
                UpdateSelectedTab(sender as Guna2Button);
            }
        }
    }
}
