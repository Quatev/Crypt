using SharpMonoInjector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crypt.Controls.Settings
{
    public partial class Game : UserControl
    {
        public IntPtr dller = IntPtr.Zero;
        public Injector i =null;

        public Game()
        {
            InitializeComponent();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            string dll = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Crypt Instance.dll");
            byte[] byeter = File.ReadAllBytes(dll);

            i = new Injector("Gorilla Tag");
            dller = i.Inject(byeter, "Crypt.Injection", "Inject", "OnInjection");
        }
    }
}
