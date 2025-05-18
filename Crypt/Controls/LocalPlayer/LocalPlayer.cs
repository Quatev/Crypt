using Crypt.Helpers.Dll_Comunication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters.Entities;

namespace Crypt.Controls.LocalPlayer
{
    public partial class LocalPlayer : UserControl
    {
        public LocalPlayer()
        {
            InitializeComponent();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {
            string text = guna2TextBox2.Text;

            if (!int.TryParse(text, out _) && text.Length > 0)
            {
                guna2TextBox2.Text = text.Remove(text.Length - 1);
                guna2TextBox2.SelectionStart = guna2TextBox2.Text.Length;
            }
        }

        private void guna2TextBox3_TextChanged(object sender, EventArgs e)
        {
            string text = guna2TextBox3.Text;

            if (!int.TryParse(text, out _) && text.Length > 0)
            {
                guna2TextBox3.Text = text.Remove(text.Length - 1);
                guna2TextBox3.SelectionStart = guna2TextBox3.Text.Length;
            }
        }

        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {
            string text = guna2TextBox4.Text;

            if (!int.TryParse(text, out _) && text.Length > 0)
            {
                guna2TextBox4.Text = text.Remove(text.Length - 1);
                guna2TextBox4.SelectionStart = guna2TextBox4.Text.Length;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            int r = int.Parse(guna2TextBox2.Text);
            int g = int.Parse(guna2TextBox3.Text);
            int b = int.Parse(guna2TextBox4.Text);

            PipeLineServerThingy.SendMessage($"color_change {r} {g} {b}");
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            PipeLineServerThingy.SendMessage($"name_change {guna2TextBox1.Text}");
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            guna2HtmlLabel3.Text = "Changes your player's " + guna2ComboBox1.SelectedItem.ToString();
            guna2Button3.Text = "Set " + guna2ComboBox1.SelectedItem.ToString();
        }

        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            guna2HtmlLabel4.Text = guna2TrackBar1.Value.ToString();
        }
    }
}
