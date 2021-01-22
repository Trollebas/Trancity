namespace Trancity
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class TimeBox : UserControl
    {
        private IContainer components = null;
        private int h;
        private int m;
        private int pos;
        private int s;
        private System.Windows.Forms.TextBox text_box;
        private bool view_seconds;

        public event EventHandler TimeChanged;

        public TimeBox()
        {
            this.InitializeComponent();
            base.EnabledChanged += new EventHandler(this.TimeBox_EnabledChanged);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void InitializeComponent()
        {
            this.text_box = new System.Windows.Forms.TextBox();
            base.SuspendLayout();
            this.text_box.BackColor = System.Drawing.SystemColors.Window;
            this.text_box.Dock = System.Windows.Forms.DockStyle.Top;
            this.text_box.Location = new System.Drawing.Point(0, 0);
            this.text_box.Name = "text_box";
            this.text_box.ReadOnly = true;
            this.text_box.Size = new System.Drawing.Size(100, 0x15);
            this.text_box.TabIndex = 0;
            this.text_box.Text = "00:00";
            this.text_box.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_box.DoubleClick += new EventHandler(this.text_box_DoubleClick);
            this.text_box.Click += new EventHandler(this.text_box_Click);
            this.text_box.Leave += new EventHandler(this.text_box_Leave);
            this.text_box.KeyPress += new KeyPressEventHandler(this.text_box_KeyPress);
            this.text_box.KeyDown += new KeyEventHandler(this.text_box_KeyDown);
            base.Controls.Add(this.text_box);
            this.Font = new System.Drawing.Font("Verdana", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0xcc);
            this.MinimumSize = new System.Drawing.Size(40, 0x15);
            base.Name = "TimeBox";
            base.Size = new System.Drawing.Size(100, 0x15);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void Redraw()
        {
            this.text_box.Text = this.h.ToString("00") + ":" + this.m.ToString("00");
            if (this.view_seconds)
            {
                this.text_box.Text = this.text_box.Text + ":" + this.s.ToString("00");
            }
            this.text_box.SelectionStart = (this.pos / 2) * 3;
            this.text_box.SelectionLength = 2;
        }

        public void text_box_Click(object sender, EventArgs e)
        {
            this.pos = (this.text_box.SelectionStart / 3) * 2;
            this.Redraw();
        }

        public void text_box_DoubleClick(object sender, EventArgs e)
        {
            this.Redraw();
        }

        public void text_box_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.Down))
            {
                int num = (e.KeyCode == Keys.Up) ? 1 : -1;
                this.pos = (this.pos / 2) * 2;
                switch (this.pos)
                {
                    case 0:
                        this.Hours += num;
                        break;

                    case 2:
                        this.Minutes += num;
                        break;

                    case 4:
                        this.Seconds += num;
                        break;
                }
                if (this.TimeChanged != null)
                {
                    this.TimeChanged(this, new EventArgs());
                }
            }
            if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Right))
            {
                int num2 = (e.KeyCode == Keys.Right) ? 1 : -1;
                this.pos = ((this.pos / 2) + num2) * 2;
                if ((this.pos >= 6) || ((this.pos >= 4) && !this.view_seconds))
                {
                    this.pos = 0;
                }
                if (this.pos < 0)
                {
                    this.pos = this.view_seconds ? 4 : 2;
                }
            }
            this.Redraw();
        }

        public void text_box_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= '0') && (e.KeyChar <= '9'))
            {
                int num = e.KeyChar - '0';
                switch (this.pos)
                {
                    case 0:
                        this.h = num;
                        break;

                    case 1:
                        this.h = (10 * this.h) + num;
                        if (this.h > 0x17)
                        {
                            this.h = 0x17;
                        }
                        break;

                    case 2:
                        this.m = num;
                        break;

                    case 3:
                        this.m = (10 * this.m) + num;
                        if (this.m > 0x3b)
                        {
                            this.m = 0x3b;
                        }
                        break;

                    case 4:
                        this.s = num;
                        break;

                    case 5:
                        this.s = (10 * this.s) + num;
                        if (this.s > 0x3b)
                        {
                            this.s = 0x3b;
                        }
                        break;
                }
                this.pos++;
                if ((this.pos >= 6) || ((this.pos >= 4) && !this.view_seconds))
                {
                    this.pos = 0;
                }
                this.Redraw();
                if (this.TimeChanged != null)
                {
                    this.TimeChanged(this, new EventArgs());
                }
            }
        }

        public void text_box_Leave(object sender, EventArgs e)
        {
            this.pos = (this.pos / 2) * 2;
        }

        public void TimeBox_EnabledChanged(object sender, EventArgs e)
        {
            this.text_box.Enabled = base.Enabled;
            if (base.Enabled)
            {
                this.text_box.BackColor = SystemColors.Window;
                this.text_box.ForeColor = SystemColors.WindowText;
            }
            else
            {
                this.text_box.BackColor = SystemColors.Control;
                this.text_box.ForeColor = SystemColors.ControlDark;
            }
        }

        public int Hours
        {
            get
            {
                return this.h;
            }
            set
            {
                while (value < 0)
                {
                    value += 0x18;
                }
                this.h = value % 0x18;
                this.Redraw();
            }
        }

        public int Minutes
        {
            get
            {
                return this.m;
            }
            set
            {
                while (value < 0)
                {
                    value += 60;
                }
                this.m = value % 60;
                this.Redraw();
            }
        }

        public int Seconds
        {
            get
            {
                return this.s;
            }
            set
            {
                while (value < 0)
                {
                    value += 60;
                }
                this.s = value % 60;
                this.Redraw();
            }
        }

        public DateTime Time
        {
            get
            {
                return new DateTime(1, 1, 1, this.h, this.m, this.s);
            }
            set
            {
                this.h = value.Hour;
                this.m = value.Minute;
                this.s = value.Second;
                this.Redraw();
            }
        }

        public int Time_Minutes
        {
            get
            {
                return (this.m + (60 * this.h));
            }
            set
            {
                while (value < 0)
                {
                    value += 0x5a0;
                }
                this.s = 0;
                this.m = value % 60;
                this.h = (value / 60) % 0x18;
                this.Redraw();
            }
        }

        public int Time_Seconds
        {
            get
            {
                return (this.s + (60 * (this.m + (60 * this.h))));
            }
            set
            {
                while (value < 0)
                {
                    value += 0x15180;
                }
                this.s = value % 60;
                this.m = (value / 60) % 60;
                this.h = (value / 0xe10) % 0x18;
                this.Redraw();
            }
        }

        public bool ViewSeconds
        {
            get
            {
                return this.view_seconds;
            }
            set
            {
                this.view_seconds = value;
                if (!value && (this.pos >= 4))
                {
                    this.pos = 2;
                }
                this.Redraw();
            }
        }
    }
}

