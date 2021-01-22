using System;
using System.Windows.Forms;
using System.Xml;
using SlimDX.Direct3D9;
using Engine;

namespace Common
{
    public partial class DeviceOptionsDialog : Form
    {
        public string filename;
		public DeviceOptions subj;
		private Direct3D d3d9;

        public DeviceOptionsDialog(string filename)
        {
            this.InitializeComponent();
            this.filename = filename;
            this.subj = LoadDeviceOptions(filename);
            
            // TODO: move that to MyDirect3D:
            d3d9 = new Direct3D();
            var modes = d3d9.Adapters[subj.adapterID].GetDisplayModes(d3d9.Adapters[subj.adapterID].CurrentDisplayMode.Format);
            foreach (DisplayMode mode in modes)
            {
            	this.AdapterMode_ComboBox.Items.Add(string.Format("{0}x{1}, {2}Hz", mode.Width, mode.Height, mode.RefreshRate));
            }
            //
            this.AdapterMode_ComboBox.SelectedIndex = 0;
            this.Size_x_UpDown.Value = this.subj.windowedX;
            this.Size_y_UpDown.Value = this.subj.windowedX;
            this.Windowed_Radio.Checked = this.subj.windowed;
            this.Fullscreen_Radio.Checked = !this.subj.windowed;
            this.VSync_CheckBox.Checked = this.subj.vSync;
            this.Check_OK_Available(this, new EventArgs());
        }
        
        public static DeviceOptions LoadDeviceOptions(string filename)
        {
        	DeviceOptions options = new DeviceOptions();
        	var document = Engine.Xml.TryOpenDocument(filename);
        	var documentElement = Engine.Xml.GetChild(document, "Options");
        	options.adapterID = (int)Xml.GetDouble(Engine.Xml.GetChild(documentElement, "adapter_id"), 0);
        	options.deviceType = (int)Xml.GetDouble(Engine.Xml.GetChild(documentElement, "device_type"), 0);
        	options.vertexProcessingMode = (int)Xml.GetDouble(Engine.Xml.GetChild(documentElement, "vertexProcessingMode"), 0);
        	options.vSync = (int)Xml.GetDouble(Engine.Xml.GetChild(documentElement, "vSync"), 0) != 0.0;
        	options.fullscreenX = (int)Xml.GetDouble(Engine.Xml.GetChild(documentElement, "fullscreen_x"), 640);
            options.fullscreenY = (int)Xml.GetDouble(Engine.Xml.GetChild(documentElement, "fullscreen_y"), 480);
            options.fullscreenRate = (int)Xml.GetDouble(Engine.Xml.GetChild(documentElement, "fullscreen_rate"), 0x4b);
            options.windowedX = (int)Xml.GetDouble(Engine.Xml.GetChild(documentElement, "windowed_x"), 640);
            options.windowedY = (int)Xml.GetDouble(Engine.Xml.GetChild(documentElement, "windowed_y"), 480);
            options.windowed = Xml.GetDouble(Engine.Xml.GetChild(documentElement, "windowed")) != 0.0;
        	return options;
        }
        
        public static void SaveDeviceOptions(DeviceOptions options, string filename)
        {
        	XmlDocument document = new XmlDocument();
            XmlElement parent = Engine.Xml.AddElement(document, "Options");
            Engine.Xml.AddElement(document, parent, "adapter_id", options.adapterID);
            Engine.Xml.AddElement(document, parent, "device_type", options.deviceType);
            Engine.Xml.AddElement(document, parent, "vertexProcessingMode", options.vertexProcessingMode);
            Engine.Xml.AddElement(document, parent, "vSync", options.vSync ? 1 : 0);
            Engine.Xml.AddElement(document, parent, "fullscreen_x", options.fullscreenX);
            Engine.Xml.AddElement(document, parent, "fullscreen_y", options.fullscreenY);
            Engine.Xml.AddElement(document, parent, "fullscreen_rate", options.fullscreenRate);
            Engine.Xml.AddElement(document, parent, "windowed_x", options.windowedX);
            Engine.Xml.AddElement(document, parent, "windowed_y", options.windowedY);
            Engine.Xml.AddElement(document, parent, "windowed", options.windowed ? 1 : 0);
            Engine.Xml.TrySaveDocument(document, filename);
        }

        private void Check_OK_Available(object sender, EventArgs e)
        {
            this.OK_Button.Enabled = !(this.Fullscreen_Radio.Checked && (this.AdapterMode_ComboBox.SelectedIndex < 0));
        }
        
        private void OK_Button_Click(object sender, EventArgs e)
        {
        	DisplayMode mode = d3d9.Adapters[this.subj.adapterID].GetDisplayModes(d3d9.Adapters[this.subj.adapterID].CurrentDisplayMode.Format)[this.AdapterMode_ComboBox.SelectedIndex];
        	this.subj.fullscreenX = mode.Width;
            this.subj.fullscreenY = mode.Height;
            this.subj.fullscreenRate = mode.RefreshRate;
            this.subj.windowedX = (int) this.Size_x_UpDown.Value;
            this.subj.windowedY = (int) this.Size_y_UpDown.Value;
            this.subj.windowed = this.Windowed_Radio.Checked;
            this.subj.vSync = this.VSync_CheckBox.Checked;
            SaveDeviceOptions(subj, this.filename);
        }

        private void Radio_CheckedChanged(object sender, EventArgs e)
        {
            this.AdapterMode_ComboBox.Enabled = this.Fullscreen_Radio.Checked;
            this.Size_x_UpDown.Enabled = this.Windowed_Radio.Checked;
            this.Size_y_UpDown.Enabled = this.Windowed_Radio.Checked;
            this.Check_OK_Available(sender, e);
        }
    }
}

