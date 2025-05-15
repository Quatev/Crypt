using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Windows.Forms;
using Crypt.Controls.Settings;
using Crypt.Helpers.Dll_Comunication;
using Crypt.Controls.LocalPlayer;
using System.Threading.Tasks;
using SharpMonoInjector;
using Crypt.Helpers;

namespace Crypt
{
    public partial class ApplicationForm : Form
    {

        public Guna2Button SelectedTab = null;
        public Guna2Button OldSelectedTab = null;

        public Settings SettingsTab = new Settings();
        public LocalPlayer LocalPlayerTab = new LocalPlayer();

        public Themes.Theme OldTheme = Themes.LightMode;
        public Themes.Theme CurrentTheme = Themes.LightMode;

        public void UpdateSelectedTab(Guna2Button newTab)
        {
            SelectedTab = newTab;

            if (OldSelectedTab != null)
                OldSelectedTab.FillColor = CurrentTheme.Button;

            if (newTab != null)
                newTab.FillColor = CurrentTheme.SelectedTab;

            OldSelectedTab = newTab;
        }

        public ApplicationForm()
        {
            InitializeComponent();

            SelectedTab = guna2Button1;
            UpdateSelectedTab(SelectedTab);

            PipeLineServerThingy.StartPipeServer();

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    // Update inject label
                    if (PipeLineServerThingy.IsConnected && guna2HtmlLabel1.Text != "Crypt | Injected : Yes")
                        guna2HtmlLabel1.Text = "Crypt | Injected : Yes";
                    else if (!PipeLineServerThingy.IsConnected && guna2HtmlLabel1.Text != "Crypt | Injected : No")
                        guna2HtmlLabel1.Text = "Crypt | Injected : No";
                    if (CurrentTheme != OldTheme)
                    {
                        UpdateTheme(this);
                        UpdateTheme(SettingsTab);
                        UpdateTheme(LocalPlayerTab);
                        OldTheme = CurrentTheme;
                    }

                    await Task.Delay(200);
                }
            });

        }

        public void UpdateTheme(Control t)
        {
            if (t == null) return;

            t.BackColor = CurrentTheme.Background;

            if (t is Guna2Button)
                UpdateSelectedTab(SelectedTab);

            foreach (Control control in t.Controls)
            {
                if (control is Guna2Button btn)
                {
                    if (btn != SelectedTab)
                        btn.FillColor = CurrentTheme.Button;
                    else
                        btn.FillColor = CurrentTheme.SelectedTab;
                    btn.ForeColor = CurrentTheme.Foreground;

                    if (CurrentTheme == Themes.DarkMode)
                    {
                        if (!btn.Tag?.ToString()?.Contains("inverted") ?? true)
                        {
                            btn.Image = ImageExtensions.InvertColors(btn.Image);
                            btn.Tag = "inverted";
                        }
                    }
                    else
                    {
                        if (btn.Tag?.ToString() == "inverted")
                        {
                            btn.Image = ImageExtensions.InvertColors(btn.Image);
                            btn.Tag = null;
                        }
                    }
                }
                else if (control is Guna2Panel pnl)
                {
                    pnl.BackColor = CurrentTheme.Background;
                    UpdateTheme(pnl);

                    if (pnl == guna2Panel1)
                        pnl.BackColor = CurrentTheme.TopBar;
                }
                else if (control is UserControl ctrl)
                {
                    ctrl.BackColor = CurrentTheme.Background;
                    UpdateTheme(ctrl);

                    if (ctrl is Settings s)
                    {
                        UpdateTheme(SettingsTab.ApperanceTab);
                        UpdateTheme(SettingsTab.AboutHelpTab);
                        UpdateTheme(SettingsTab.GameTab);
                    }
                    else if (ctrl is LocalPlayer l)
                    {
                        UpdateTheme(l);
                    }
                }
                else if (control is Guna2HtmlLabel lb)
                {
                    lb.ForeColor = CurrentTheme.Foreground;
                }
                else if (control is Guna2ControlBox cbtn)
                {
                    cbtn.IconColor = CurrentTheme.Foreground;
                    cbtn.FillColor = CurrentTheme.TopBar;
                }
            }
        }


        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            if (!TabHolder.Controls.Contains(SettingsTab) || SelectedTab != sender as Guna2Button)
            {
                TabHolder.Controls.Clear();
                TabHolder.Controls.Add(SettingsTab);
                UpdateSelectedTab(sender as Guna2Button);
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (!TabHolder.Controls.Contains(LocalPlayerTab) || SelectedTab != sender as Guna2Button)
            {
                TabHolder.Controls.Clear();
                TabHolder.Controls.Add(LocalPlayerTab);
                UpdateSelectedTab(sender as Guna2Button);
            }
        }

        private void ApplicationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            IntPtr oof = SettingsTab.GameTab.dller;
            Injector oofer = SettingsTab.GameTab.i;

            if (oof != IntPtr.Zero && oofer != null) 
            {
                oofer.Eject(oof, "Crypt.Injection", "Inject", "");
            }
        }
    }
}
