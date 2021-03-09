/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 23.08.2013
 * Time: 15:54
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using SlimDX.DirectInput;
using System;
using System.Windows.Forms;
using System.Xml;

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
            XmlDocument document = new XmlDocument();
            Common.Xml.document = document;
            XmlElement parent = Common.Xml.AddElement(document, "Control");
            XmlElement parent2 = Common.Xml.AddElement(parent, "Keys");
            Common.Xml.AddElement(parent2, "pause", Key.Escape.ToString("Пауза"));
            Common.Xml.AddElement(parent2, "map", Key.Tab.ToString());
            Common.Xml.AddElement(parent2, "auto_control", Key.A.ToString());
            Common.Xml.AddElement(parent2, "manual_control", Key.M.ToString());
            Common.Xml.AddElement(parent2, "stat_engine", Key.Y.ToString());
            Common.Xml.AddElement(parent2, "drive_doors", Key.G.ToString());
            Common.Xml.AddElement(parent2, "all_doors", Key.H.ToString());
            Common.Xml.AddElement(parent2, "door0", Key.D1.ToString());
            Common.Xml.AddElement(parent2, "door1", Key.D2.ToString());
            Common.Xml.AddElement(parent2, "door2", Key.D3.ToString());
            Common.Xml.AddElement(parent2, "door3", Key.D4.ToString());
            Common.Xml.AddElement(parent2, "door4", Key.D5.ToString());
            Common.Xml.AddElement(parent2, "door5", Key.D6.ToString());
            Common.Xml.AddElement(parent2, "door6", Key.D7.ToString());
            Common.Xml.AddElement(parent2, "door7", Key.D8.ToString());
            Common.Xml.AddElement(parent2, "door8", Key.D9.ToString());
            Common.Xml.AddElement(parent2, "door9", Key.D0.ToString());
            Common.Xml.AddElement(parent2, "lights", Key.F.ToString());
            Common.Xml.AddElement(parent2, "cam0", Key.C.ToString());
            Common.Xml.AddElement(parent2, "cam1", Key.F2.ToString());
            Common.Xml.AddElement(parent2, "cam2", Key.F3.ToString());
            Common.Xml.AddElement(parent2, "cam3", Key.F4.ToString());
            Common.Xml.AddElement(parent2, "pant", Key.T.ToString());
            Common.Xml.AddElement(parent2, "reverse", Key.Backspace.ToString());
            Common.Xml.AddElement(parent2, "gas", Key.W.ToString());
            Common.Xml.AddElement(parent2, "break", Key.S.ToString());
            Common.Xml.AddElement(parent2, "st_left", Key.A.ToString());
            Common.Xml.AddElement(parent2, "st_right", Key.D.ToString());
            Common.Xml.AddElement(parent2, "st_reset", Key.Space.ToString());
            Common.Xml.AddElement(parent2, "resetting_the_pedal_stroke", Key.LeftAlt.ToString());
            Common.Xml.AddElement(parent2, "reset_view", Key.F9.ToString());
            Common.Xml.AddElement(parent2, "tram_blinker_all", Key.O.ToString());
            Common.Xml.AddElement(parent2, "tram_blinker_left", Key.LeftArrow.ToString());
            Common.Xml.AddElement(parent2, "tram_blinker_right", Key.RightArrow.ToString());
            Common.Xml.AddElement(parent2, "blinker_all", Key.E.ToString());
            Common.Xml.AddElement(parent2, "blinker_left", Key.Q.ToString());
            Common.Xml.AddElement(parent2, "blinker_right", Key.E.ToString());
            Common.Xml.AddElement(parent2, "gear_up", Key.X.ToString());
            Common.Xml.AddElement(parent2, "gear_down", Key.Z.ToString());
            Common.Xml.AddElement(parent2, "st_motion", Key.P.ToString());
            Common.Xml.AddElement(parent2, "prt_sc", Key.F10.ToString());
            Common.Xml.AddElement(parent2, "debug_info", Key.F1.ToString());
            document.Save(@"Data\Control.xml");

        }

        private void Close_ButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Reset_ButtonClick(object sender, EventArgs e)
        {
            XmlDocument document = new XmlDocument();
            Common.Xml.document = document;
            XmlElement parent = Common.Xml.AddElement(document, "Control");
            XmlElement parent2 = Common.Xml.AddElement(parent, "Keys");
            Common.Xml.AddElement(parent2, "pause", Key.Escape.ToString());
            Common.Xml.AddElement(parent2, "map", Key.Tab.ToString());
            Common.Xml.AddElement(parent2, "auto_control", Key.A.ToString());
            Common.Xml.AddElement(parent2, "manual_control", Key.M.ToString());
            Common.Xml.AddElement(parent2, "stat_engine", Key.Y.ToString());
            Common.Xml.AddElement(parent2, "drive_doors", Key.G.ToString());
            Common.Xml.AddElement(parent2, "all_doors", Key.H.ToString());
            Common.Xml.AddElement(parent2, "door0", Key.D1.ToString());
            Common.Xml.AddElement(parent2, "door1", Key.D2.ToString());
            Common.Xml.AddElement(parent2, "door2", Key.D3.ToString());
            Common.Xml.AddElement(parent2, "door3", Key.D4.ToString());
            Common.Xml.AddElement(parent2, "door4", Key.D5.ToString());
            Common.Xml.AddElement(parent2, "door5", Key.D6.ToString());
            Common.Xml.AddElement(parent2, "door6", Key.D7.ToString());
            Common.Xml.AddElement(parent2, "door7", Key.D8.ToString());
            Common.Xml.AddElement(parent2, "door8", Key.D9.ToString());
            Common.Xml.AddElement(parent2, "door9", Key.D0.ToString());
            Common.Xml.AddElement(parent2, "lights", Key.F.ToString());
            Common.Xml.AddElement(parent2, "cam0", Key.C.ToString());
            Common.Xml.AddElement(parent2, "cam1", Key.F2.ToString());
            Common.Xml.AddElement(parent2, "cam2", Key.F3.ToString());
            Common.Xml.AddElement(parent2, "cam3", Key.F4.ToString());
            Common.Xml.AddElement(parent2, "pant", Key.T.ToString());
            Common.Xml.AddElement(parent2, "reverse", Key.Backspace.ToString());
            Common.Xml.AddElement(parent2, "gas", Key.W.ToString());
            Common.Xml.AddElement(parent2, "break", Key.S.ToString());
            Common.Xml.AddElement(parent2, "st_left", Key.A.ToString());
            Common.Xml.AddElement(parent2, "st_right", Key.D.ToString());
            Common.Xml.AddElement(parent2, "st_reset", Key.Space.ToString());
            Common.Xml.AddElement(parent2, "resetting_the_pedal_stroke", Key.LeftAlt.ToString());
            Common.Xml.AddElement(parent2, "reset_view", Key.F9.ToString());
            Common.Xml.AddElement(parent2, "tram_blinker_all", Key.O.ToString());
            Common.Xml.AddElement(parent2, "tram_blinker_left", Key.A.ToString());
            Common.Xml.AddElement(parent2, "tram_blinker_right", Key.D.ToString());
            Common.Xml.AddElement(parent2, "blinker_all", Key.E.ToString());
            Common.Xml.AddElement(parent2, "blinker_left", Key.Q.ToString());
            Common.Xml.AddElement(parent2, "blinker_right", Key.E.ToString());
            Common.Xml.AddElement(parent2, "gear_up", Key.X.ToString());
            Common.Xml.AddElement(parent2, "gear_down", Key.Z.ToString());
            Common.Xml.AddElement(parent2, "st_motion", Key.P.ToString());
            Common.Xml.AddElement(parent2, "prt_sc", Key.F10.ToString());
            Common.Xml.AddElement(parent2, "debug_info", Key.F1.ToString());
            document.Save(@"Data\Control.xml");
        }
    }
}
