/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 23.08.2013
 * Time: 15:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using SlimDX.DirectInput;
using Common;

namespace Trancity
{
	/// <summary>
	/// UserControlForm - форма для настройки управления
	/// </summary>
	public partial class UserControlForm : Form
	{
		public UserControlForm()
		{
			InitializeComponent();
			Localization.ApplyLocalization(this);
		}
		
		private void UpdateListBox()
		{
			
		}
		
		private void Save_ButtonClick(object sender, EventArgs e)
		{
			/*XmlDocument document = new XmlDocument();
            Xml.document = document;
            XmlElement parent = Xml.AddElement(document, "Control");
            XmlElement parent2 = Xml.AddElement(parent, "Keys");
            Xml.AddElement(parent2, "pause", Key.Escape.ToString());
            Xml.AddElement(parent2, "map", Key.Tab.ToString());
            Xml.AddElement(parent2, "auto_control", Key.A.ToString());
            Xml.AddElement(parent2, "manual_control", Key.M.ToString());
            Xml.AddElement(parent2, "stat_engine", Key.Y.ToString());
            Xml.AddElement(parent2, "drive_doors", Key.S.ToString());
            Xml.AddElement(parent2, "all_doors", Key.D.ToString());
            Xml.AddElement(parent2, "door0", Key.D1.ToString());
            Xml.AddElement(parent2, "door1", Key.D2.ToString());
            Xml.AddElement(parent2, "door2", Key.D3.ToString());
            Xml.AddElement(parent2, "door3", Key.D4.ToString());
            Xml.AddElement(parent2, "door4", Key.D5.ToString());
            Xml.AddElement(parent2, "door5", Key.D6.ToString());
            Xml.AddElement(parent2, "door6", Key.D7.ToString());
            Xml.AddElement(parent2, "door7", Key.D8.ToString());
            Xml.AddElement(parent2, "door8", Key.D9.ToString());
            Xml.AddElement(parent2, "door9", Key.D0.ToString());
            Xml.AddElement(parent2, "lights", Key.F.ToString());
            Xml.AddElement(parent2, "cam0", Key.C.ToString());
            Xml.AddElement(parent2, "cam1", Key.F2.ToString());
            Xml.AddElement(parent2, "cam2", Key.F3.ToString());
            Xml.AddElement(parent2, "cam3", Key.F4.ToString());
            Xml.AddElement(parent2, "pant", Key.T.ToString());
            Xml.AddElement(parent2, "reverse", Key.Backspace.ToString());
            Xml.AddElement(parent2, "gas", Key.UpArrow.ToString());
            Xml.AddElement(parent2, "break", Key.DownArrow.ToString());
            Xml.AddElement(parent2, "st_left", Key.LeftArrow.ToString());
            Xml.AddElement(parent2, "st_right", Key.RightArrow.ToString());
            Xml.AddElement(parent2, "tram_blinker_all", Key.Q.ToString());
            Xml.AddElement(parent2, "tram_blinker_left", Key.LeftArrow.ToString());
            Xml.AddElement(parent2, "tram_blinker_right", Key.RightArrow.ToString());
            Xml.AddElement(parent2, "blinker_all", Key.E.ToString());
            Xml.AddElement(parent2, "blinker_left", Key.Q.ToString());
            Xml.AddElement(parent2, "blinker_right", Key.W.ToString());
            Xml.AddElement(parent2, "gear_up", Key.X.ToString());
            Xml.AddElement(parent2, "gear_down", Key.Z.ToString());
            Xml.AddElement(parent2, "st_motion", Key.O.ToString());
            Xml.AddElement(parent2, "prt_sc", Key.F10.ToString());
            Xml.AddElement(parent2, "debug_info", Key.F1.ToString());
            document.Save(@"Data\Control.xml");*/
			
		}
		
		private void Close_ButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		private void Reset_ButtonClick(object sender, EventArgs e)
		{
			XmlDocument document = new XmlDocument();
            Xml.document = document;
            XmlElement parent = Xml.AddElement(document, "Control");
            XmlElement parent2 = Xml.AddElement(parent, "Keys");
            Xml.AddElement(parent2, "pause", Key.Escape.ToString());
            Xml.AddElement(parent2, "map", Key.Tab.ToString());
            Xml.AddElement(parent2, "auto_control", Key.A.ToString());
            Xml.AddElement(parent2, "manual_control", Key.M.ToString());
            Xml.AddElement(parent2, "start_engine", Key.Y.ToString());
            Xml.AddElement(parent2, "drive_doors", Key.S.ToString());
            Xml.AddElement(parent2, "all_doors", Key.D.ToString());
            Xml.AddElement(parent2, "door0", Key.D1.ToString());
            Xml.AddElement(parent2, "door1", Key.D2.ToString());
            Xml.AddElement(parent2, "door2", Key.D3.ToString());
            Xml.AddElement(parent2, "door3", Key.D4.ToString());
            Xml.AddElement(parent2, "door4", Key.D5.ToString());
            Xml.AddElement(parent2, "door5", Key.D6.ToString());
            Xml.AddElement(parent2, "door6", Key.D7.ToString());
            Xml.AddElement(parent2, "door7", Key.D8.ToString());
            Xml.AddElement(parent2, "door8", Key.D9.ToString());
            Xml.AddElement(parent2, "door9", Key.D0.ToString());
            Xml.AddElement(parent2, "lights", Key.F.ToString());
            Xml.AddElement(parent2, "cam0", Key.C.ToString());
            Xml.AddElement(parent2, "cam1", Key.F2.ToString());
            Xml.AddElement(parent2, "cam2", Key.F3.ToString());
            Xml.AddElement(parent2, "cam3", Key.F4.ToString());
            Xml.AddElement(parent2, "pant", Key.T.ToString());
            Xml.AddElement(parent2, "reverse", Key.Backspace.ToString());
            Xml.AddElement(parent2, "gas", Key.UpArrow.ToString());
            Xml.AddElement(parent2, "break", Key.DownArrow.ToString());
            Xml.AddElement(parent2, "st_left", Key.LeftArrow.ToString());
            Xml.AddElement(parent2, "st_right", Key.RightArrow.ToString());
            Xml.AddElement(parent2, "tram_blinker_all", Key.Q.ToString());
            Xml.AddElement(parent2, "tram_blinker_left", Key.LeftArrow.ToString());
            Xml.AddElement(parent2, "tram_blinker_right", Key.RightArrow.ToString());
            Xml.AddElement(parent2, "blinker_all", Key.E.ToString());
            Xml.AddElement(parent2, "blinker_left", Key.Q.ToString());
            Xml.AddElement(parent2, "blinker_right", Key.W.ToString());
            Xml.AddElement(parent2, "gear_up", Key.X.ToString());
            Xml.AddElement(parent2, "gear_down", Key.Z.ToString());
            Xml.AddElement(parent2, "st_motion", Key.O.ToString());
            Xml.AddElement(parent2, "prt_sc", Key.F10.ToString());
            Xml.AddElement(parent2, "debug_info", Key.F1.ToString());
            document.Save(@"Data\Control.xml");
		}
	}
}
