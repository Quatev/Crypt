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
using Crypt.Controls;

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
            UpdateSelectedTab(SelectedTab);
            SettingsTab.UpdateSelectedTab(SettingsTab.SelectedTab);
            if (t == null) return;

            t.BackColor = CurrentTheme.Background;

            foreach (Control control in t.Controls)
            {
                switch (control)
                {
                    case Guna2Button btn:
                        if (btn != SelectedTab || btn != SettingsTab.SelectedTab)
                            btn.FillColor = CurrentTheme.Button;
                        else
                            UpdateSelectedTab(btn);

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
                        break;

                    case Guna2Panel pnl:
                        pnl.BackColor = CurrentTheme.Background;
                        UpdateTheme(pnl);

                        if (pnl == guna2Panel1)
                            pnl.BackColor = CurrentTheme.TopBar;
                        break;

                    case Guna2TextBox tb:
                        tb.FillColor = CurrentTheme.Button;
                        tb.ForeColor = CurrentTheme.Foreground;
                        tb.PlaceholderForeColor = CurrentTheme.Foreground;
                        tb.BorderColor = CurrentTheme.SelectedTab;

                        tb.DisabledState.FillColor = CurrentTheme.Background;
                        tb.DisabledState.ForeColor = CurrentTheme.Foreground;
                        tb.DisabledState.PlaceholderForeColor = CurrentTheme.Foreground;
                        tb.DisabledState.BorderColor = CurrentTheme.SelectedTab;
                        break;

                    case Guna2HtmlLabel lb:
                        lb.ForeColor = CurrentTheme.Foreground;
                        break;

                    case Guna2ControlBox cbtn:
                        cbtn.IconColor = CurrentTheme.Foreground;
                        cbtn.FillColor = CurrentTheme.TopBar;
                        break;

                    case Guna2ComboBox cbb:
                        cbb.FocusedColor = CurrentTheme.SelectedTab;
                        cbb.FillColor = CurrentTheme.Button;
                        cbb.ForeColor = CurrentTheme.Foreground;
                        cbb.BorderColor = CurrentTheme.SelectedTab;
                        break;

                    case Guna2TrackBar gtb:
                        gtb.ForeColor = CurrentTheme.Foreground;
                        gtb.ThumbColor = CurrentTheme.SelectedTab;
                        break;

                    case UserControl ctrl:
                        if (ctrl is Notification notif)
                        {
                            notif.ProgressBarColor = CurrentTheme.SelectedTab;
                            notif.BackColor = CurrentTheme.Button;
                            break;
                        }

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
                        break;
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
                oofer.Eject(oof, "Crypt.Injection", "Inject", "OnDejection");
            }
        }

        private void ApplicationForm_Load(object sender, EventArgs e)
        {
            if (!TabHolder.Controls.Contains(LocalPlayerTab))
            {
                TabHolder.Controls.Clear();
                TabHolder.Controls.Add(LocalPlayerTab);
                UpdateSelectedTab(guna2Button1);
            }
        }
    }
}
