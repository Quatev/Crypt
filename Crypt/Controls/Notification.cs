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
        public int BorderRadius { get; set; } = 5;

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
            this.DoubleBuffered = true;
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
                    this.Hide();
                }
                this.Invalidate();
            };
            Timerer.Start();
            int steps = (Time * 1000) / 50;
            if (steps == 0) steps = 1;
            Decreasement = 100 / steps;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            NotifiText.Text = TitleText;
            label2.Text = DescriptionText;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Rectangle fullRect = new Rectangle(0, 0, this.Width, this.Height);
            using (GraphicsPath path = GetRoundedRectanglePath(fullRect, BorderRadius))
            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillPath(brush, path);
                this.Region = new Region(path);
            }

            int progressHeight = 5;
            int progressWidth = (int)(this.Width * Progress / 100f);
            Rectangle progressRect = new Rectangle(0, this.Height - progressHeight, progressWidth, progressHeight);

            using (GraphicsPath progressPath = GetRoundedRectanglePath(progressRect, BorderRadius))
            using (SolidBrush pbBrush = new SolidBrush(ProgressBarColor))
            {
                e.Graphics.FillPath(pbBrush, progressPath);
            }
        }


        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            if (diameter > rect.Width) diameter = rect.Width;
            if (diameter > rect.Height) diameter = rect.Height;

            path.StartFigure();
            path.AddArc(rect.Left, rect.Top, diameter, diameter, 180, 90); // top-left
            path.AddArc(rect.Right - diameter, rect.Top, diameter, diameter, 270, 90); // top-right
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90); // bottom-right
            path.AddArc(rect.Left, rect.Bottom - diameter, diameter, diameter, 90, 90); // bottom-left
            path.CloseFigure();
            return path;
        }

    }
}
