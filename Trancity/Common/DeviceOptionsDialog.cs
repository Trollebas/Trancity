namespace Common
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using System.Collections.Generic;//
	using System.Runtime.InteropServices;

    public class DeviceOptionsDialog : Form
    {
        private Button Cancel_Button;
        private Label ChooseDevice_Label;
        public string filename;
        private RadioButton Fullscreen_Radio;
//        public int fullscreen_rate = 0x4b;
//        public int fullscreen_x = 640;
//        public int fullscreen_y = 480;
        private Button OK_Button;
        private ComboBox RefreshRate_Box;
        private ComboBox Size_Box;
        private NumericUpDown Size_x_UpDown;
        private NumericUpDown Size_y_UpDown;
//        public bool windowed;
        private RadioButton Windowed_Radio;
//        public int windowed_x = 640;
//        public int windowed_y = 480;
		public DeviceOptions subj;

        public DeviceOptionsDialog(string filename)
        {
            this.InitializeComponent();
            this.filename = filename;
            this.subj = LoadDeviceOptions(filename);
            /*if (File.Exists(filename))
            {
                XmlDocument document = new XmlDocument();
                document.Load(filename);
                XmlElement documentElement = document.DocumentElement;
                if (documentElement["fullscreen_x"] != null)
                {
                    this.fullscreen_x = int.Parse(documentElement["fullscreen_x"].InnerText);
                }
                if (documentElement["fullscreen_y"] != null)
                {
                    this.fullscreen_y = int.Parse(documentElement["fullscreen_y"].InnerText);
                }
                if (documentElement["fullscreen_rate"] != null)
                {
                    this.fullscreen_rate = int.Parse(documentElement["fullscreen_rate"].InnerText);
                }
                if (documentElement["windowed_x"] != null)
                {
                    this.windowed_x = int.Parse(documentElement["windowed_x"].InnerText);
                }
                if (documentElement["windowed_y"] != null)
                {
                    this.windowed_y = int.Parse(documentElement["windowed_y"].InnerText);
                }
                if (documentElement["windowed"] != null)
                {
                    this.windowed = bool.Parse(documentElement["windowed"].InnerText);
                }
            }*/
            this.Size_Box.SelectedIndex = 3;
            this.RefreshRate_Box.SelectedIndex = 0;
            if ((this.subj.fullscreen_x == 320) && (this.subj.fullscreen_y == 240))
            {
                this.Size_Box.SelectedIndex = 0;
            }
            if ((this.subj.fullscreen_x == 640) && (this.subj.fullscreen_y == 480))
            {
                this.Size_Box.SelectedIndex = 1;
            }
            if ((this.subj.fullscreen_x == 800) && (this.subj.fullscreen_y == 600))
            {
                this.Size_Box.SelectedIndex = 2;
            }
            if ((this.subj.fullscreen_x == 0x400) && (this.subj.fullscreen_y == 0x300))
            {
                this.Size_Box.SelectedIndex = 3;
            }
            if ((this.subj.fullscreen_x == 0x480) && (this.subj.fullscreen_y == 0x360))
            {
                this.Size_Box.SelectedIndex = 4;
            }
            if ((this.subj.fullscreen_x == 0x500) && (this.subj.fullscreen_y == 800))
            {
                this.Size_Box.SelectedIndex = 5;
            }
            if ((this.subj.fullscreen_x == 0x500) && (this.subj.fullscreen_y == 960))
            {
                this.Size_Box.SelectedIndex = 6;
            }
            if ((this.subj.fullscreen_x == 0x500) && (this.subj.fullscreen_y == 0x400))
            {
                this.Size_Box.SelectedIndex = 7;
            }
            if ((this.subj.fullscreen_x == 0x640) && (this.subj.fullscreen_y == 0x4b0))
            {
                this.Size_Box.SelectedIndex = 8;
            }
            if ((this.subj.fullscreen_x == 0x690) && (this.subj.fullscreen_y == 0x41a))
            {
                this.Size_Box.SelectedIndex = 9;
            }
            switch (this.subj.fullscreen_rate)
            {
            	case 60:
            	{
            		this.RefreshRate_Box.SelectedIndex = 0;
            		break;
            	}
            	case 0x4b:
            	{
            		this.RefreshRate_Box.SelectedIndex = 1;
            		break;
            	}
            	case 0x55:
            	{
            		this.RefreshRate_Box.SelectedIndex = 2;
            		break;
            	}
            	case 100:
            	{
            		this.RefreshRate_Box.SelectedIndex = 3;
            		break;
            	}
            }
            /*if (this.subj.fullscreen_rate == 60)
            {
                this.RefreshRate_Box.SelectedIndex = 0;
            }
            if (this.subj.fullscreen_rate == 0x4b)
            {
                this.RefreshRate_Box.SelectedIndex = 1;
            }
            if (this.subj.fullscreen_rate == 0x55)
            {
                this.RefreshRate_Box.SelectedIndex = 2;
            }
            if (this.subj.fullscreen_rate == 100)
            {
                this.RefreshRate_Box.SelectedIndex = 3;
            }*/
            this.Size_x_UpDown.Value = this.subj.windowed_x;
            this.Size_y_UpDown.Value = this.subj.windowed_y;
            this.Windowed_Radio.Checked = this.subj.windowed;
            this.Fullscreen_Radio.Checked = !this.subj.windowed;
            this.Box_SelectedIndexChanged(this, new EventArgs());
        }
        
        public static DeviceOptions LoadDeviceOptions(string filename)
        {
        	DeviceOptions options = new DeviceOptions();
        	if (File.Exists(filename))
            {
        		var document = Xml.Connect(filename);
        		var documentElement = document["Options"];//.DocumentElement;
        		options.fullscreen_x = (int)Xml.GetDouble(documentElement["fullscreen_x"], 640);
                options.fullscreen_y = (int)Xml.GetDouble(documentElement["fullscreen_y"], 480);
                options.fullscreen_rate = (int)Xml.GetDouble(documentElement["fullscreen_rate"], 0x4b);
                options.windowed_x = (int)Xml.GetDouble(documentElement["windowed_x"], 640);
                options.windowed_y = (int)Xml.GetDouble(documentElement["windowed_y"], 480);
                options.windowed = Xml.GetDouble(documentElement["windowed"]) != 0.0;
                Xml.Disconnect();
            }
        	else
        	{
        		options.fullscreen_rate = 0x4b;
        		options.fullscreen_x = 640;
        		options.fullscreen_y = 480;
        		options.windowed = false;
        		options.windowed_x = 640;
        		options.windowed_y = 480;
        	}
        	return options;
        }

        private void Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OK_Button.Enabled = !(this.Fullscreen_Radio.Checked && ((this.Size_Box.SelectedIndex < 0) || (this.RefreshRate_Box.SelectedIndex < 0)));
        }

        private void InitializeComponent()
        {
        	this.Fullscreen_Radio = new System.Windows.Forms.RadioButton();
        	this.Windowed_Radio = new System.Windows.Forms.RadioButton();
        	this.ChooseDevice_Label = new System.Windows.Forms.Label();
        	this.Size_Box = new System.Windows.Forms.ComboBox();
        	this.RefreshRate_Box = new System.Windows.Forms.ComboBox();
        	this.Size_x_UpDown = new System.Windows.Forms.NumericUpDown();
        	this.Size_y_UpDown = new System.Windows.Forms.NumericUpDown();
        	this.OK_Button = new System.Windows.Forms.Button();
        	this.Cancel_Button = new System.Windows.Forms.Button();
        	((System.ComponentModel.ISupportInitialize)(this.Size_x_UpDown)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.Size_y_UpDown)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// Fullscreen_Radio
        	// 
        	this.Fullscreen_Radio.Location = new System.Drawing.Point(17, 52);
        	this.Fullscreen_Radio.Name = "Fullscreen_Radio";
        	this.Fullscreen_Radio.Size = new System.Drawing.Size(112, 21);
        	this.Fullscreen_Radio.TabIndex = 0;
        	this.Fullscreen_Radio.Text = "Fullscreen";
        	this.Fullscreen_Radio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
        	// 
        	// Windowed_Radio
        	// 
        	this.Windowed_Radio.Location = new System.Drawing.Point(17, 77);
        	this.Windowed_Radio.Name = "Windowed_Radio";
        	this.Windowed_Radio.Size = new System.Drawing.Size(112, 21);
        	this.Windowed_Radio.TabIndex = 1;
        	this.Windowed_Radio.Text = "Windowed";
        	this.Windowed_Radio.CheckedChanged += new System.EventHandler(this.Radio_CheckedChanged);
        	// 
        	// ChooseDevice_Label
        	// 
        	this.ChooseDevice_Label.AutoSize = true;
        	this.ChooseDevice_Label.Location = new System.Drawing.Point(14, 17);
        	this.ChooseDevice_Label.Name = "ChooseDevice_Label";
        	this.ChooseDevice_Label.Size = new System.Drawing.Size(98, 13);
        	this.ChooseDevice_Label.TabIndex = 2;
        	this.ChooseDevice_Label.Text = "Choose Device:";
        	// 
        	// Size_Box
        	// 
        	this.Size_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.Size_Box.Items.AddRange(new object[] {
        	        	        	"320x240",
        	        	        	"640x480",
        	        	        	"800x600",
        	        	        	"1024x768",
        	        	        	"1152x864",
        	        	        	"1280x800",
        	        	        	"1280x960",
        	        	        	"1280x1024",
        	        	        	"1600x1200",
        	        	        	"1680x1050"});
        	this.Size_Box.Location = new System.Drawing.Point(130, 50);
        	this.Size_Box.Name = "Size_Box";
        	this.Size_Box.Size = new System.Drawing.Size(90, 21);
        	this.Size_Box.TabIndex = 3;
        	this.Size_Box.SelectedIndexChanged += new System.EventHandler(this.Box_SelectedIndexChanged);
        	// 
        	// RefreshRate_Box
        	// 
        	this.RefreshRate_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.RefreshRate_Box.Items.AddRange(new object[] {
        	        	        	"60hz",
        	        	        	"75hz",
        	        	        	"85hz",
        	        	        	"100hz"});
        	this.RefreshRate_Box.Location = new System.Drawing.Point(225, 50);
        	this.RefreshRate_Box.Name = "RefreshRate_Box";
        	this.RefreshRate_Box.Size = new System.Drawing.Size(61, 21);
        	this.RefreshRate_Box.TabIndex = 5;
        	this.RefreshRate_Box.SelectedIndexChanged += new System.EventHandler(this.Box_SelectedIndexChanged);
        	// 
        	// Size_x_UpDown
        	// 
        	this.Size_x_UpDown.Increment = new decimal(new int[] {
        	        	        	40,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.Size_x_UpDown.Location = new System.Drawing.Point(130, 77);
        	this.Size_x_UpDown.Maximum = new decimal(new int[] {
        	        	        	4000,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.Size_x_UpDown.Minimum = new decimal(new int[] {
        	        	        	320,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.Size_x_UpDown.Name = "Size_x_UpDown";
        	this.Size_x_UpDown.Size = new System.Drawing.Size(73, 21);
        	this.Size_x_UpDown.TabIndex = 6;
        	this.Size_x_UpDown.Value = new decimal(new int[] {
        	        	        	320,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	// 
        	// Size_y_UpDown
        	// 
        	this.Size_y_UpDown.Increment = new decimal(new int[] {
        	        	        	30,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.Size_y_UpDown.Location = new System.Drawing.Point(213, 77);
        	this.Size_y_UpDown.Maximum = new decimal(new int[] {
        	        	        	3000,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.Size_y_UpDown.Minimum = new decimal(new int[] {
        	        	        	240,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	this.Size_y_UpDown.Name = "Size_y_UpDown";
        	this.Size_y_UpDown.Size = new System.Drawing.Size(73, 21);
        	this.Size_y_UpDown.TabIndex = 7;
        	this.Size_y_UpDown.Value = new decimal(new int[] {
        	        	        	240,
        	        	        	0,
        	        	        	0,
        	        	        	0});
        	// 
        	// OK_Button
        	// 
        	this.OK_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.OK_Button.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.OK_Button.Location = new System.Drawing.Point(98, 120);
        	this.OK_Button.Name = "OK_Button";
        	this.OK_Button.Size = new System.Drawing.Size(75, 23);
        	this.OK_Button.TabIndex = 8;
        	this.OK_Button.Text = "OK";
        	this.OK_Button.Click += new System.EventHandler(this.OK_Button_Click);
        	// 
        	// Cancel_Button
        	// 
        	this.Cancel_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.Cancel_Button.Location = new System.Drawing.Point(186, 120);
        	this.Cancel_Button.Name = "Cancel_Button";
        	this.Cancel_Button.Size = new System.Drawing.Size(75, 23);
        	this.Cancel_Button.TabIndex = 8;
        	this.Cancel_Button.Text = "Cancel";
        	// 
        	// DeviceOptionsDialog
        	// 
        	this.AcceptButton = this.OK_Button;
        	this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
        	this.CancelButton = this.Cancel_Button;
        	this.ClientSize = new System.Drawing.Size(306, 161);
        	this.ControlBox = false;
        	this.Controls.Add(this.OK_Button);
        	this.Controls.Add(this.Size_y_UpDown);
        	this.Controls.Add(this.Size_x_UpDown);
        	this.Controls.Add(this.RefreshRate_Box);
        	this.Controls.Add(this.Size_Box);
        	this.Controls.Add(this.ChooseDevice_Label);
        	this.Controls.Add(this.Windowed_Radio);
        	this.Controls.Add(this.Fullscreen_Radio);
        	this.Controls.Add(this.Cancel_Button);
        	this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        	this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        	this.Name = "DeviceOptionsDialog";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "Device Options";
        	((System.ComponentModel.ISupportInitialize)(this.Size_x_UpDown)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.Size_y_UpDown)).EndInit();
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }

        private void OK_Button_Click(object sender, EventArgs e)
        {
            switch (this.Size_Box.SelectedIndex)
            {
                case 0:
                    this.subj.fullscreen_x = 320;
                    this.subj.fullscreen_y = 240;
                    break;

                case 1:
                    this.subj.fullscreen_x = 640;
                    this.subj.fullscreen_y = 480;
                    break;

                case 2:
                    this.subj.fullscreen_x = 800;
                    this.subj.fullscreen_y = 600;
                    break;

                case 3:
                    this.subj.fullscreen_x = 0x400;
                    this.subj.fullscreen_y = 0x300;
                    break;

                case 4:
                    this.subj.fullscreen_x = 0x480;
                    this.subj.fullscreen_y = 0x360;
                    break;

                case 5:
                    this.subj.fullscreen_x = 0x500;
                    this.subj.fullscreen_y = 800;
                    break;

                case 6:
                    this.subj.fullscreen_x = 0x500;
                    this.subj.fullscreen_y = 960;
                    break;

                case 7:
                    this.subj.fullscreen_x = 0x500;
                    this.subj.fullscreen_y = 0x400;
                    break;

                case 8:
                    this.subj.fullscreen_x = 0x640;
                    this.subj.fullscreen_y = 0x4b0;
                    break;

                case 9:
                    this.subj.fullscreen_x = 0x690;
                    this.subj.fullscreen_y = 0x41a;
                    break;
            }
            switch (this.RefreshRate_Box.SelectedIndex)
            {
                case 0:
                    this.subj.fullscreen_rate = 60;
                    break;

                case 1:
                    this.subj.fullscreen_rate = 0x4b;
                    break;

                case 2:
                    this.subj.fullscreen_rate = 0x55;
                    break;

                case 3:
                    this.subj.fullscreen_rate = 100;
                    break;
            }
            this.subj.windowed_x = (int) this.Size_x_UpDown.Value;
            this.subj.windowed_y = (int) this.Size_y_UpDown.Value;
            this.subj.windowed = this.Windowed_Radio.Checked;
            XmlDocument document = new XmlDocument();
            Xml.document = document;
            XmlElement parent = Xml.AddElement(document, "Options");
            Xml.AddElement(parent, "fullscreen_x", subj.fullscreen_x);
            Xml.AddElement(parent, "fullscreen_y", subj.fullscreen_y);
            Xml.AddElement(parent, "fullscreen_rate", subj.fullscreen_rate);
            Xml.AddElement(parent, "windowed_x", subj.windowed_x);
            Xml.AddElement(parent, "windowed_y", subj.windowed_y);
            Xml.AddElement(parent, "windowed", subj.windowed ? 1 : 0);//.ToString());
            document.Save(this.filename);
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.Size_Box.Enabled = this.Fullscreen_Radio.Checked;
            this.RefreshRate_Box.Enabled = this.Fullscreen_Radio.Checked;
            this.Size_x_UpDown.Enabled = this.Windowed_Radio.Checked;
            this.Size_y_UpDown.Enabled = this.Windowed_Radio.Checked;
            this.Box_SelectedIndexChanged(sender, e);
        }
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct DeviceOptions
    {
    	public int fullscreen_rate;// = 0x4b;
        public int fullscreen_x;// = 640;
        public int fullscreen_y;// = 480;
        public bool windowed;
        public int windowed_x;// = 640;
        public int windowed_y;// = 480;
    }
}

