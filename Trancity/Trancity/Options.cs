using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SlimDX.Direct3D9;
using Common;
using System.IO;

namespace Trancity
{
    public partial class Options : Form
    {
        private Dictionary<string,int> _маршруты = new Dictionary<string,int>();
        private Dictionary<string, int> _транспорт = new Dictionary<string, int>();
        private int _видТранспорта = TypeOfTransport.Tramway;
        private bool city_ready;
        private Button Add_Button;
        private CheckBox AutoControl_Box;
        private Label City_label;
        private Label City_Name_label;
        private int current_player = -1;
        private DeviceOptionsDialog dialog;
        private TabPage DirectX_Page;
        private Button Editor_Button;
        private GroupBox DirectX_Box;
        private Button Exit_Button;
        private Label Transport_label;
        private Label Order_label;
        private Label Route_label;
        private Label Control_label;
        private Button Launch_Buttton;
        private Button LoadCity_Button;
        private OpenFileDialog LoadCity_Dialog;
        private TextBox Name_Box;
        private CheckBox NonExclusiveKeyboard_Box;
        private CheckBox NonExclusiveMouse_Box;
        private CheckBox NoStops_Box;
        private GroupBox Options_Group;
        private TabPage Options_Page;
        private GroupBox Players_Box;
        private CheckedListBox Players_List;
        private TabPage Players_Page;
        private TrackBar Rail_Box;
        private Button Remove_Button;
        private ComboBox Screen_Box;
        private TimeBox StartTime_Box;
        private TabControl Tab_Control;
        private Label Time_label;
        private Label Vertex_label;
        private ComboBox VertexProcessing_Box;
        private ComboBox Маршрут_Box;
        private World world;
        private ComboBox Наряд_Box;
        private ComboBox ПодвижнойСостав_Box;
        private ComboBox Управление_Box;
        private CheckBox EnableShaders_Box;
        private Label Langugage_label;
        private ComboBox Lang_Box;
        private CheckBox InvArrows_Box;
        private CheckBox RotateCamera_Box;
        private Label OnRouteCount_label;
        private Label TransportCount_label;
        private Label InParkCount_label;
        private Label Compute_TCount_label;
        private Label Screen_Size_label;
        private Label Splines_Cond_label;
        private MainForm app = new MainForm { настройки = new MainForm.НастройкиЗапуска() };

        private void Add_Button_Click(object sender, EventArgs e)
        {
            // var avp = MainForm app;
            // var app = new MainForm { настройки = new MainForm.НастройкиЗапуска() };
            var игрокаArray =  app.настройки.игроки;
            app.настройки.игроки = new MainForm.НастройкиЗапускаИгрока[игрокаArray.Length + 1];
            app.настройки.количествоИгроков = игрокаArray.Length + 1;
            for (int i = 0; i < игрокаArray.Length; i++)
            {
                this.app.настройки.игроки[i] = игрокаArray[i];
            }
            app.настройки.игроки[игрокаArray.Length].inputGuid = Guid.Empty;//Microsoft.DirectX.DirectInput.SystemGuid.Keyboard;
            app.настройки.игроки[игрокаArray.Length].вИгре = true;
            app.настройки.игроки[игрокаArray.Length].имя = this.Name_Box.Text;
            app.настройки.игроки[игрокаArray.Length].маршрут = 0;
            app.настройки.игроки[игрокаArray.Length].наряд = 0;
            app.настройки.игроки[игрокаArray.Length].подвижнойСостав = "";
            this.Name_Box.Clear();
            this.Players_List.Items.Clear();
            for (int j = 0; j < app.настройки.количествоИгроков; j++)
            {
                this.Players_List.Items.Add(app.настройки.игроки[j].имя, app.настройки.игроки[j].вИгре);
            }
            this.UpdatePlayers(sender, e);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (this.components != null))
        //    {
        //        this.components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        public Options(MainForm app)
        {
            this.app = app;
            InitializeComponent();
        }

        private void LoadCity_Button_Click(object sender, EventArgs e)
        {
            if (LoadCity_Dialog.ShowDialog(this) == DialogResult.OK)
            {
                app.настройки.cityFilename = LoadCity_Dialog.FileName;
                UpdateCity();
                UpdatePlayers(sender, e);
            }
        }

        private void Name_Box_Enter(object sender, EventArgs e)
        {
            AcceptButton = Add_Button;
        }

        private void Name_Box_Leave(object sender, EventArgs e)
        {
            AcceptButton = Launch_Buttton;
        }

        private void Options_Form_Closing(object sender, CancelEventArgs e)
        {
            switch (Screen_Box.SelectedIndex)
            {
                case 0:
                    app.настройки.размерЭкрана = new Size(640, 480);
                    break;
                case 1:
                    app.настройки.размерЭкрана = new Size(800, 600);
                    break;
                case 2:
                    app.настройки.размерЭкрана = new Size(0x400, 0x300);
                    break;
                case 3:
                    app.настройки.размерЭкрана = new Size(0x480, 0x360);
                    break;
                case 4:
                    app.настройки.размерЭкрана = new Size(0x500, 960);
                    break;
                case 5:
                    app.настройки.размерЭкрана = new Size(0x500, 0x400);
                    break;
                case 6:
                    app.настройки.размерЭкрана = new Size(0x640, 0x4b0);
                    break;
                case 8:
                    app.настройки.размерЭкрана = new Size(0, 0);
                    break;
            }
            this.app.настройки.deviceType = SlimDX.Direct3D9.DeviceType.Hardware;
            if (this.VertexProcessing_Box.SelectedIndex == 0)
            {
                this.app.настройки.createFlags = CreateFlags.HardwareVertexProcessing;
            }
            else if (this.VertexProcessing_Box.SelectedIndex == 1)
            {
                this.app.настройки.createFlags = CreateFlags.SoftwareVertexProcessing;
            }
            else if (this.VertexProcessing_Box.SelectedIndex == 2)
            {
                this.app.настройки.createFlags = CreateFlags.MixedVertexProcessing;
            }
            this.app.настройки.начальноеВремя = this.StartTime_Box.Time_Seconds;
            this.app.настройки.качествоРельсов = (double)((this.Rail_Box.Maximum + this.Rail_Box.Minimum) - this.Rail_Box.Value) / 100.0;
            this.app.настройки.количествоИгроков = this.Players_List.Items.Count;
            this.app.настройки.автоматическоеУправление = this.AutoControl_Box.Checked;
            this.app.настройки.поворачиватьКамеру = this.RotateCamera_Box.Checked;
            this.app.настройки.стрелкиНаоборот = this.InvArrows_Box.Checked;
            this.app.настройки.noSound = !this.EnableSound_Box.Checked;
            this.app.настройки.soundVolume = this.Volume_TrackBar.Value;
            this.app.настройки.noStops = this.NoStops_Box.Checked;
            this.app.настройки.nonExclusiveKeyboard = this.NonExclusiveKeyboard_Box.Checked;
            this.app.настройки.nonExclusiveMouse = this.NonExclusiveMouse_Box.Checked;
            this.app.настройки.enableShaders = this.EnableShaders_Box.Checked;
//            this.app.настройки.deleteFarObject = this.DeleteFarObject_Box.Checked;
            for (int i = 0; i < this.app.настройки.количествоИгроков; i++)
            {
                this.app.настройки.игроки[i].вИгре = this.Players_List.CheckedIndices.Contains(i);
            }
            this.Players_List.SelectedIndex = -1;
            this.UpdatePlayers(sender, new EventArgs());
        }

        private void Options_Form_Load(object sender, EventArgs e)
        {
            this.dialog = new DeviceOptionsDialog(@"Data\DeviceOptions.xml");
            UpdateLocalization();
//            this.RefreshScreenOptions();
            if (this.app.настройки.размерЭкрана == new Size(640, 480))
            {
                this.Screen_Box.SelectedIndex = 0;
            }
            else if (this.app.настройки.размерЭкрана == new Size(800, 600))
            {
                this.Screen_Box.SelectedIndex = 1;
            }
            else if (this.app.настройки.размерЭкрана == new Size(0x400, 0x300))
            {
                this.Screen_Box.SelectedIndex = 2;
            }
            else if (this.app.настройки.размерЭкрана == new Size(0x480, 0x360))
            {
                this.Screen_Box.SelectedIndex = 3;
            }
            else if (this.app.настройки.размерЭкрана == new Size(0x500, 960))
            {
                this.Screen_Box.SelectedIndex = 4;
            }
            else if (this.app.настройки.размерЭкрана == new Size(0x500, 0x400))
            {
                this.Screen_Box.SelectedIndex = 5;
            }
            else if (this.app.настройки.размерЭкрана == new Size(0x640, 0x4b0))
            {
                this.Screen_Box.SelectedIndex = 6;
            }
            else if (this.app.настройки.размерЭкрана == new Size(0, 0))
            {
                this.Screen_Box.SelectedIndex = 8;
            }
            else
            {
                this.Screen_Box.SelectedIndex = 4;
            }
            if (this.app.настройки.createFlags == CreateFlags.HardwareVertexProcessing)
            {
                this.VertexProcessing_Box.SelectedIndex = 0;
            }
            else if (this.app.настройки.createFlags == CreateFlags.SoftwareVertexProcessing)
            {
                this.VertexProcessing_Box.SelectedIndex = 1;
            }
            else if (this.app.настройки.createFlags == CreateFlags.MixedVertexProcessing)
            {
                this.VertexProcessing_Box.SelectedIndex = 2;
            }
            else
            {
                this.VertexProcessing_Box.SelectedIndex = 0;
            }
            this.StartTime_Box.Time_Seconds = this.app.настройки.начальноеВремя;
            if (this.app.настройки.качествоРельсов > 0.0)
            {
                this.Rail_Box.Value = (this.Rail_Box.Maximum + this.Rail_Box.Minimum) - (int)(this.app.настройки.качествоРельсов * 100.0);
            }
//            this.Tram_Box.Value = this.app.настройки.количествоОстальныхТрамваев;
            this.AutoControl_Box.Checked = this.app.настройки.автоматическоеУправление;
            RotateCamera_Box.Checked = app.настройки.поворачиватьКамеру;
            InvArrows_Box.Checked = app.настройки.стрелкиНаоборот;
            EnableSound_Box.Checked = !app.настройки.noSound;
            Volume_TrackBar.Value = app.настройки.soundVolume;
            NoStops_Box.Checked = app.настройки.noStops;
            NonExclusiveKeyboard_Box.Checked = app.настройки.nonExclusiveKeyboard;
            NonExclusiveMouse_Box.Checked = app.настройки.nonExclusiveMouse;
            EnableShaders_Box.Checked = app.настройки.enableShaders;
//            DeleteFarObject_Box.Checked = app.настройки.deleteFarObject;
            Name_Box.Clear();
            Players_List.Items.Clear();
            for (var i = 0; i < app.настройки.количествоИгроков; i++)
            {
                Players_List.Items.Add(app.настройки.игроки[i].имя, app.настройки.игроки[i].вИгре);
            }
            Управление_Box.Items.Clear();
            foreach (var str in MyDirectInput.DeviceNames)
            {
                Управление_Box.Items.Add(str);
            }
            foreach (var трамвай in Модели.Трамваи)
            {
                ПодвижнойСостав_Box.Items.Add(трамвай.name);
                _транспорт[трамвай.name] = TypeOfTransport.Tramway;
            }
            foreach (var троллейбуса in Модели.Троллейбусы)
            {
                ПодвижнойСостав_Box.Items.Add(троллейбуса.name);
                _транспорт[троллейбуса.name] = TypeOfTransport.Trolleybus;
            }
            foreach (var троллейбуса2 in Модели.Автобусы)
            {
                ПодвижнойСостав_Box.Items.Add(троллейбуса2.name);
                _транспорт[троллейбуса2.name] = TypeOfTransport.Bus;
            }
            /*foreach (var рандом in _видТранспорта)
            {
                ПодвижнойСостав_Box.Items.Add(рандом);
                _транспорт[рандом] = TypeOfTransport.Equals;
            }*/
            UpdateCity();
            if ((ПодвижнойСостав_Box.SelectedIndex == -1) && (ПодвижнойСостав_Box.Items.Count > 0))
                ПодвижнойСостав_Box.SelectedIndex = 0;
            UpdatePlayers(sender, e);
        }

        private void RefreshScreenOptions()
        {
            if (dialog.subj.windowed)
            {
            	Screen_Box.Items[8] = string.Format("{0}x{1}, {2}", dialog.subj.windowedX, dialog.subj.windowedY, Localization.current_.windowed);//dialog.subj.windowed_x + "x" + dialog.subj.windowed_y + ", " + Localization.current_.windowed;//в окне";
            }
            else
            {
            	Screen_Box.Items[8] = string.Format("{0}x{1}, {2}Hz", dialog.subj.fullscreenX, dialog.subj.fullscreenY, dialog.subj.fullscreenRate);
            }
        }

        private void Remove_Button_Click(object sender, EventArgs e)
        {
            if (current_player < 0) return;
            var игрокаArray = app.настройки.игроки;
            app.настройки.игроки = new MainForm.НастройкиЗапускаИгрока[игрокаArray.Length - 1];
            app.настройки.количествоИгроков = игрокаArray.Length - 1;
            var index = 0;
            var num2 = 0;
            while (index < игрокаArray.Length)
            {
                if (index != current_player)
                {
                    app.настройки.игроки[num2] = игрокаArray[index];
                    num2++;
                }
                index++;
            }
            Players_List.Items.Clear();
            for (var i = 0; i < app.настройки.количествоИгроков; i++)
            {
                Players_List.Items.Add(app.настройки.игроки[i].имя, app.настройки.игроки[i].вИгре);
            }
            current_player = -1;
            UpdatePlayers(sender, e);
        }

        private void Screen_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Screen_Box.SelectedIndex != 7) return;
            dialog.ShowDialog(this);
            RefreshScreenOptions();
            Screen_Box.SelectedIndex = 8;
        }

        private void StartTime_Box_TimeChanged(object sender, EventArgs e)
        {
            UpdateTramsCount();
        }

        private void UpdateCity()
        {
            world = new World();
            if ((!string.IsNullOrEmpty(app.настройки.cityFilename)) && (File.Exists(app.настройки.cityFilename)))//(app.настройки.cityFilename != null)
            {
                City_Name_label.Text = Path.GetFileNameWithoutExtension(app.настройки.cityFilename);
                world.LoadCitySimple(app.настройки.cityFilename);
                city_ready = true;
            }
            else
            {
            	MessageBox.Show(Localization.current_.mapnotfound, "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            	city_ready = false;
            }
            UpdateRoutes(_видТранспорта);
            UpdateМаршрутBox();
            UpdateTramsCount();
        }

        private void UpdatePlayers(object sender, EventArgs e)
        {
            Remove_Button.Enabled = Players_List.SelectedIndex >= 0;
            Add_Button.Enabled = Name_Box.Text.Length > 0;
            Launch_Buttton.Enabled = (city_ready && !((Players_List.CheckedIndices.Count < 1) || (Players_List.CheckedIndices.Count > 4)));
            if ((Players_List.CheckedIndices.Count < 1) || (Players_List.CheckedIndices.Count > 4))
            {
                Launch_Buttton.Enabled = false;
            }
            else
            {
                Launch_Buttton.Enabled = app.настройки.cityFilename != null;
            }
            if (current_player >= 0)
            {
                var игрокаArray = app.настройки.игроки;
                if (Управление_Box.SelectedIndex >= 0)
                {
                    игрокаArray[current_player].inputGuid = MyDirectInput.DeviceGuids[Управление_Box.SelectedIndex];
                }
                игрокаArray[current_player].подвижнойСостав = (string)ПодвижнойСостав_Box.SelectedItem;
                if (игрокаArray[current_player].подвижнойСостав == null)
                {
                    игрокаArray[current_player].подвижнойСостав = "";
                }
                switch (Маршрут_Box.SelectedIndex)
                {
                    case 0:
                        игрокаArray[current_player].маршрут = 0;
                        break;
                    case 1:
                        игрокаArray[current_player].маршрут = 1;
                        break;
                    default:
                        игрокаArray[current_player].маршрут = _маршруты[(string) Маршрут_Box.SelectedItem] + 2;
                        break;
                }
                игрокаArray[current_player].наряд = Наряд_Box.SelectedIndex;
            }
            current_player = Players_List.SelectedIndex;
            if (current_player < 0)
            {
                Players_Box.Visible = false;
            }
            else
            {
                var игрокаArray2 = app.настройки.игроки;
                Players_Box.Visible = true;
                var num = -1;
                for (var i = 0; i < MyDirectInput.DeviceGuids.Length; i++)
                {
                    if (игрокаArray2[current_player].inputGuid != MyDirectInput.DeviceGuids[i]) continue;
                    num = i;
                    break;
                }
                Управление_Box.SelectedIndex = num;
                if (игрокаArray2[current_player].подвижнойСостав != "")
                {
                    ПодвижнойСостав_Box.SelectedItem = игрокаArray2[current_player].подвижнойСостав;
                }
                //else
                //{
                //    //ПодвижнойСостав_Box.SelectedIndex = -1;
                //}
                Маршрут_Box.SelectedIndex = игрокаArray2[current_player].маршрут < Маршрут_Box.Items.Count ? игрокаArray2[current_player].маршрут : 0;
                Наряд_Box.SelectedIndex = игрокаArray2[current_player].наряд >= Наряд_Box.Items.Count ? 0 : игрокаArray2[current_player].наряд;
            }
        }

        private void UpdateTramsCount()
        {
            if (world == null) return;
            var num = StartTime_Box.Time_Seconds;
            if (num < 0x2a30)
            {
                num += 0x15180;
            }
            var num2 = 0;
            var num3 = 0;
            bool flag;
            foreach (var маршрут in world.маршруты)
            {
                foreach (var наряд in маршрут.orders)
                {
                    if ((наряд.рейсы.Length <= 0) || (наряд.рейсы[наряд.рейсы.Length - 1].время_прибытия <= num))
                        continue;
                    flag = false;
                    for (var i = 0; i < наряд.рейсы.Length; i++)
                    {
                        if (num >= наряд.рейсы[i].время_прибытия) continue;
                        flag = ((num < наряд.рейсы[i].время_отправления) && (наряд.рейсы[i].дорога_отправления == наряд.парк.выезд));
                        break;
                    }
                    if (flag)
                    {
                        num3++;
                    }
                    else
                    {
                        num2++;
                    }
                }
            }
            Compute_TCount_label.Text = num2 + "\n" + num3;
        }

        private void Маршрут_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            Наряд_Box.Items.Clear();
            Наряд_Box.Items.Add(Localization.current_.empty);
            if (Маршрут_Box.SelectedIndex > 0)
            {
                if (Маршрут_Box.SelectedIndex == 1)
                {
                	Наряд_Box.Items.Add(Localization.current_.random);
                }
                else if (world.маршруты[_маршруты[(string) Маршрут_Box.SelectedItem]].orders.Length > 0)
                {
                	Наряд_Box.Items.Add(Localization.current_.random);
                    foreach (var наряд in world.маршруты[_маршруты[(string)Маршрут_Box.SelectedItem]].orders)
                    {
                        var item = наряд.номер + " (";
                        if (наряд.рейсы.Length > 0)
                        {
                            var str2 = item;
                            item = str2 + наряд.рейсы[0].str_время_отправления + " - " + наряд.рейсы[наряд.рейсы.Length - 1].str_время_прибытия + ", ";
                        }
                        item = item + наряд.парк.название + ")";
                        Наряд_Box.Items.Add(item);
                    }
                }
            }
            Наряд_Box.SelectedIndex = 0;
        }

        private void ПодвижнойСостав_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            _видТранспорта = _транспорт[(string)ПодвижнойСостав_Box.SelectedItem];
            UpdateRoutes(_видТранспорта);
            UpdateМаршрутBox();
        }

        private void UpdateМаршрутBox()
        {
            Маршрут_Box.Items.Clear();
            Маршрут_Box.Items.Add(Localization.current_.empty);//"Нет");
            foreach (var s in _маршруты.Keys)
            {
                Маршрут_Box.Items.Add(s);
            }
            if (Маршрут_Box.Items.Count > 1)
            {
            	Маршрут_Box.Items.Insert(1, Localization.current_.random);///"Случайный");
            }
            Маршрут_Box.SelectedIndex = 0;
        }

        private void UpdateRoutes(int видТранспорта)
        {
            _маршруты.Clear();
            for (var i = 0; i < world.маршруты.Length; i++)
            {
                var маршрут = world.маршруты[i];
                if (маршрут.typeOfTransport == видТранспорта)
                {
                    _маршруты[маршрут.number] = i;
                }
            }
        }
        
        private void UpdateLocalization()
        {
        	Lang_Box.Items.Clear();
        	Lang_Box.SelectedIndex = -1;
        	for (int i = 0; i < Localization.localizations.Count; i++)
        	{
        		Lang_Box.Items.Add(Localization.localizations[i].name);
        		if (Localization.localizations[i].name == app.настройки.language) Lang_Box.SelectedIndex = i;
        	}
        	if ((Localization.localizations.Count > 0) && (Lang_Box.SelectedIndex == -1)) Lang_Box.SelectedIndex = 0;
        }

        private void ПодвижнойСостав_Box_TextChanged(object sender, EventArgs e)
        {
            //_видТранспорта = _транспорт[(string)ПодвижнойСостав_Box.SelectedItem];
            //UpdateCity();
        }
        
        private void Lang_BoxSelectedIndexChanged(object sender, EventArgs e)
        {
        	if (Lang_Box.SelectedIndex < 0) return;
        	Localization.current_ = Localization.localizations[Lang_Box.SelectedIndex];
        	app.настройки.language = Localization.current_.name;
        	Localization.ApplyLocalization(this);
        	if (dialog != null)
        	{
        		Localization.ApplyLocalization(dialog);
        		RefreshScreenOptions();
        		Screen_Box.Items[7] = Localization.current_.edit;
        	}
        	if (Маршрут_Box.Items.Count > 0)
        	{
        		Маршрут_Box.Items[0] = Localization.current_.empty;
        		if (Маршрут_Box.Items.Count > 2) Маршрут_Box.Items[1] = Localization.current_.random;
        	}
        }
        
        private void Control_buttonClick(object sender, EventArgs e)
        {
        	var form = new UserControlForm();
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
            	//save table???
            	
            }
            form.Dispose();
        }
        void City_Name_labelClick(object sender, EventArgs e)
        {
          
        }
        void VertexProcessing_BoxSelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
        void Editor_ButtonClick(object sender, EventArgs e)
        {
          
        }
        void StartTime_BoxLoad(object sender, EventArgs e)
        {
          
        }
        void Label1Click(object sender, EventArgs e)
        {
          
        }
    }
}
