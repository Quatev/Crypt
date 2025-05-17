using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Crypt.Controls
{
    public partial class Notification : UserControl
    {
        [Category("Appearance")]
        public int BorderRadius { get; set; } = 3;

        [Category("Appearance")]
        public Color BorderColor { get; set; } = Color.Black;

        [Category("Appearance")]
        public Color ProgressBarColor { get; set; } = Color.FromKnownColor(KnownColor.Control);

        [Category("Misc")]
        public int Time = 5;
        [Category("Misc")]
        public string TitleText { get; set; } = "PLACEHOLDER TEXT";
        [Category("Misc")]
        public string DescriptionText { get; set; } = "Small Description on the message";

        private int Progress = 100;

        private int Decreasement;

        private Timer Timerer = new Timer();

        public Notification()
        {
            InitializeComponent();
            this.ResizeRedraw = true;
            Timerer = new Timer();
            Timerer.Interval = 50;
            Timerer.Tick += (object sender, EventArgs e) => 
            {
                Progress -= Decreasement;
                if (Progress <= 0)
                {
                    Timerer.Stop();
                    Progress = 0;
                    //this.Hide();
                }
                this.Invalidate();
            };
            Timerer.Start();
            int steps = Time / 50;
            if (steps == 0) steps = 1;
            Decreasement = 100 / steps;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            using (GraphicsPath path = GetRoundedRectanglePath(rect, BorderRadius))
            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }

            int progressWidth = (int)(this.Width * Progress / 100f);
            Rectangle progressRect = new Rectangle(0, this.Height - 5, progressWidth, 5);
            using (SolidBrush pbBrush = new SolidBrush(ProgressBarColor))
            {
                e.Graphics.FillRectangle(pbBrush, progressRect);
            }
        }


        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            if (radius == 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            path.StartFigure();
            path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
