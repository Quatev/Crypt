using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crypt.Helpers
{
    public static class Themes
    {
        public static Theme LightMode = new Theme
        {
            Background = Color.FromKnownColor(KnownColor.Control),
            Foreground = Color.Black,
            TopBar = Color.FromKnownColor(KnownColor.ControlLight),
            Button = Color.FromKnownColor(KnownColor.ControlLight),
            SelectedTab = Color.FromKnownColor(KnownColor.AppWorkspace)
        };

        public static Theme DarkMode = new Theme
        {
            Background = Color.FromArgb(255, 30, 30, 30),
            Foreground = Color.White,
            TopBar = Color.FromArgb(255, 44, 44, 44),
            Button = Color.FromArgb(255, 42, 42, 42),
            SelectedTab = Color.FromArgb(255, 57, 57, 57)
        };

        public class Theme
        {
            public Color Background { get; set; }
            public Color Foreground { get; set; }
            public Color TopBar { get; set; }
            public Color Button { get; set; }
            public Color SelectedTab { get; set; }
        }
    }
}
