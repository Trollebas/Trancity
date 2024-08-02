/*
 * Created by SharpDevelop.
 * User: Andrey
 * Date: 11.08.2009
 * Time: 10:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Common;
using System.IO;
using SlimDX.Direct3D9;
using System.Diagnostics;
using System.ComponentModel;

namespace Trancity
{
    /// <summary>
    /// Description of Editor.
    /// </summary>
    public partial class Editor : Form
    {
		private bool alt;
        private StatusBarPanel Angle_Status;
        private StatusBarPanel Angle1_Status;
        private StatusBarPanel Angle2_Status;
        private MenuItem Check_Joints_Item;
        private MenuItem City_Item;
        private MenuItem ComputeAllTime_Item;
        private StatusBarPanel Coord_x1_Status;
        private StatusBarPanel Coord_x2_Status;
        private StatusBarPanel Coord_y1_Status;
        private StatusBarPanel Coord_y2_Status;
        private bool ctrl;
        private StatusBarPanel Cursor_x_Status;
        private StatusBarPanel Cursor_y_Status;
        private Point drag_point;
        private bool dragging;
        private ToolBarButton Edit_Button;
        private Panel edit_panel;
        private MenuItem Exit_Item;
        private string filename;
        private MenuItem Find_MinRadius_Item;
        private StatusBarPanel Height0_Status;
        private StatusBarPanel Height1_Status;
        private ImageList imageList;
        private StatusBarPanel Length_Status;
        private MainMenu mainMenu;
        private bool modified;
        private MouseEventArgs mouse_args = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);
        private Button Narad_Add_Button;
        private ComboBox Narad_Box;
        private Button Narad_ChangeName_Button;
        private Label Narad_label;
        private TextBox Narad_Name_Box;
        private Label Narad_Name_label;
        private Panel narad_panel;
        private ComboBox Narad_Park_Box;
        private Label Narad_Park_label;
        private Button Narad_Remove_Button;
        private Button Narad_Runs_Add_Button;
        private ComboBox Narad_Runs_Box;
        private Label Narad_Runs_label;
        private Button Narad_Runs_Remove_Button;
        private ComboBox Narad_Runs_Run_Box;
        private Label Narad_Runs_Run_label;
        private TimeBox Narad_Runs_Time1_Box;
        private Label Narad_Runs_Time1_label;
        private TimeBox Narad_Runs_Time2_Box;
        private Label Narad_Runs_Time2_label;
        private MenuItem New_Item;
        private Panel old_panel;
        private ToolBarButton New_Button;
        private ToolBarButton Open_Button;
        private MenuItem Open_Item;
        private OpenFileDialog openFileDialog;
        private Panel panel;
        private Button Park_Add_Button;
        private ComboBox Park_Box;
        private ToolBarButton Park_Button;
        private Button Park_ChangeName_Button;
        private ToolBarButton Park_Edit_Button;
        private ToolBarButton Park_In_Button;
        private Label Park_label;
        private TextBox Park_Name_Box;
        private Label Park_Name_label;
        private ToolBarButton Park_Out_Button;
        private Panel park_panel;
        private ToolBarButton Park_Rails_Button;
        private Button Park_Remove_Button;
        private ToolBarButton Play_Button;
        private StatusBarPanel Radius_Status;
        private ToolBarButton Rail_Build_Direct_Button;//Rail_Build_прямые_Button;
        private ToolBarButton Rail_Button;
        private ToolBarButton Rail_Edit_Button;
        private MenuItem Refresh_All_TripStop_Lists_Item;
        private Timer Refresh_Timer;
        private bool refreshing = true;
        private ToolBarButton Road_Button;
        private Button Route_Add_Button;
        private ComboBox Route_Box;
        private ToolBarButton Route_Button;
        private Button Route_ChangeName_Button;
        private Label Route_label;
        private TextBox Route_Name_Box;
        private Label Route_Name_label;
        private Panel route_panel;
        private Button Route_Remove_Button;
        private Button Route_Runs_Add_Button;
        private ComboBox Route_Runs_Box;
        private Button Route_Runs_ComputeTime_Button;
        private Label Route_Runs_label;
        private CheckBox Route_Runs_Park_Box;
        private Button Route_Runs_Remove_Button;
        private TimeBox Route_Runs_Time_Box;
        private Label Route_Runs_Time_label;
        private CheckBox Route_Runs_ToPark_Box;
        private Label Route_Runs_ToParkIndex_label;
        private NumericUpDown Route_Runs_ToParkIndex_UpDown;
        private CheckBox Route_ShowNarads_Box;
        private ComboBox Route_TransportType_Box;
        private Label Route_TransportType_label;
        private ToolBarButton Run_Button;
        private MenuItem Run_Item;
        private ToolBarButton Save_Button;
        private MenuItem Save_Item;
        private MenuItem SaveAs_Item;
        private SaveFileDialog saveFileDialog;
        private ToolBarButton SeparatorButton1;
        private ToolBarButton SeparatorButton2;
        private ToolBarButton SeparatorButton3;
        private ToolBarButton SeparatorButton4;
        private ToolBarButton SeparatorButton5;
        private ToolBarButton SeparatorButton6;
        private MenuItem SeparatorItem1;
        private MenuItem SeparatorItem2;
        private StatusBarPanel SeparatorPanel1;
        private StatusBarPanel SeparatorPanel2;
        private StatusBarPanel SeparatorPanel3;
        private bool shift;
        private Button Signals_Add_Button;
        private Label Signals_Bound_label;
        private NumericUpDown Signals_Bound_UpDown;
        private ComboBox Signals_Box;
        private ToolBarButton Signals_Button;
        private Button Signals_Element_AddContact_Button;
        private Button Signals_Element_AddSignal_Button;
        private ComboBox Signals_Element_Box;
        private Button Signals_Element_EditLocation_Button;
        private Label Signals_Element_label;
        private Label Signals_Element_Location_label;
        private CheckBox Signals_Element_Minus_Box;
        private Button Signals_Element_Remove_Button;
        private Button Signals_Element_ShowLocation_Button;
        private Label Signals_label;
        private Panel signals_panel;
        private Button Signals_Remove_Button;
        private Panel Sizable_Panel;
        private StatusBar statusBar;
        private Button Stops_Add_Button;
        private ComboBox Stops_Box;
        private ToolBarButton Stops_Button;
        private Button Stops_ChangeName_Button;
        private Button Stops_EditLocation_Button;
        private Label Stops_label;
        private Label Stops_Location_label;
        private TextBox Stops_Name_Box;
        private Label Stops_Name_label;
        private Panel stops_panel;
        private Button Stops_Remove_Button;
        private Button Stops_ShowLocation_Button;
        private Button Svetofor_Add_Button;
        private TimeBox Svetofor_Begin_Box;
        private ComboBox Svetofor_Box;
        private ToolBarButton Svetofor_Button;
        private TimeBox Svetofor_Cycle_Box;
        private Label Svetofor_Cycle_label;
        private ComboBox Svetofor_Element_Box;
        private Button Svetofor_Element_EditLocation_Button;
        private Label Svetofor_Element_label;
        private Label Svetofor_Element_Location_label;
        private Button Svetofor_Element_Remove_Button;
        private Button Svetofor_Element_ShowLocation_Button;
        private TimeBox Svetofor_End_Box;
        private Label Svetofor_Green_label;
        private Label Svetofor_label;
        private TimeBox Svetofor_OfGreen_Box;
        private Panel svetofor_panel;
        private Button Svetofor_Remove_Button;
        private Button Svetofor_Signal_Add_Button;
        private Button Svetofor_Svetofor_Add_Button;
        private CheckBox Svetofor_Svetofor_Arrow_Box;
        private ComboBox Svetofor_Svetofor_ArrowGreen_Box;
        private Label Svetofor_Svetofor_ArrowGreen_label;
        private ComboBox Svetofor_Svetofor_ArrowRed_Box;
        private Label Svetofor_Svetofor_ArrowRed_label;
        private ComboBox Svetofor_Svetofor_ArrowYellow_Box;
        private Label Svetofor_Svetofor_ArrowYellow_label;
        private TimeBox Svetofor_ToGreen_Box;
        private Label Svetofor_Work_label;
        private ToolBar toolBar;
        private ToolBarButton Troll_lines_Button;
        private ToolBarButton Troll_lines_Draw_Button;
        private ToolBarButton Troll_lines_Edit_Button;
        private ToolBarButton Troll_lines_Flag_Button;
        private StatusBarPanel Wide0_Status;
        private StatusBarPanel Wide1_Status;
        private Игрок[] без_игроков;
        private Game игра;
        private Игрок игрок;
        private Игрок[] игроки;
        private World мир;
        private bool раскрашены_провода;
        private bool раскрашены_рельсы;
        private Стадия_стоительства строительство_дороги;
        private Road строящаяся_дорога;
        private Stop строящаяся_остановка;
        private Строящиеся_провода строящиеся_провода;
        private Светофор строящийся_светофор;
        private Светофорный_сигнал строящийся_светофорный_сигнал;
        private Сигнальная_система.Контакт строящийся_элемент_сигнальной_системы;
        private Visual_Signal строящийся_сигнал_сигнальной_системы;
        private double угол_поворота;
        private ToolBarButton Object_Button;
        private Объект строящийся_объект;
        private int temp_index = -1;
        private bool splines_aviable;
        private double time_color = 0.0;

        public Editor()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
        }
        
        private void ApplyLocalization()
        {
        	Localization.ApplyLocalization(this);
        	Localization.ApplyLocalizationToolBar(this.toolBar);
        }

        private void Check_стыки_Item_Click(object sender, EventArgs e)
        {
            int num = 0;
            int num2 = 0;
            foreach (Road рельс in this.мир.ВсеДороги)
            {
                рельс.ОбновитьСледующиеДороги(this.мир.ВсеДороги);
                if (рельс.предыдущиеДороги.Length == 0)
                {
                    num++;
                    this.игрок.cameraPosition.xz_point = рельс.концы[0];
                }
                if (рельс.следующиеДороги.Length == 0)
                {
                    num2++;
                    this.игрок.cameraPosition.xz_point = рельс.концы[1];
                }
            }
            if ((num > 0) && (num2 > 0))
            {
            	MessageBox.Show(this, string.Format(Localization.current_.joints_begin_end/*"Найдено {0} путей без начала и {1} путей без продолжения."*/, num.ToString(), num2.ToString()), "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (num > 0)
            {
            	MessageBox.Show(this, string.Format(Localization.current_.joints_begin/*"Найдено {0} путей без начала."*/, num.ToString()), "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (num2 > 0)
            {
            	MessageBox.Show(this, string.Format(Localization.current_.joints_end/*"Найдено {0} путей без продолжения."*/, num2.ToString()), "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show(this, Localization.current_.joints_checked/*"Все стыки проверены."*/, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void ComputeAllTime_Item_Click(object sender, EventArgs e)
        {
            List<Trip> list = new List<Trip>();
            List<int> list2 = new List<int>();
            foreach (Route маршрут in this.мир.маршруты)
            {
                list.AddRange(маршрут.AllTrips);
                for (int j = 0; j < маршрут.AllTrips.Count; j++)
                {
                    list2.Add(маршрут.typeOfTransport);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].время_прибытия != 0.0)
                {
                    list.RemoveAt(i);
                    list2.RemoveAt(i);
                    i--;
                }
            }
            if (list.Count == 0)
            {
                MessageBox.Show(this, Localization.current_.routes_computed/*"Не найдено ни одного не посчитанного рейса!"*/, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Refresh_Timer.Enabled = false;
                for (int k = 0; k < list.Count; k++)
                {
                	if (list[k].pathes.Length == 0)
                	{
                		MessageBox.Show(Localization.current_.route_failed/*"Один из рейсов не может быть посчитан!"*/, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                		continue;
                	}
                    this.мир.time = 0.0;
                    ComputeTimeDialog dialog = new ComputeTimeDialog(this.мир, list2[k], list[k], this.игрок);
                    dialog.Text = string.Format("{0} ({1} {2} {3})", dialog.Text, (k + 1), Localization.current_.of, list.Count);
                    if (dialog.ShowDialog(this) == DialogResult.Cancel)
                    {
                        break;
                    }
                    list[k].время_прибытия = this.мир.time;
                }
                this.мир.time = 0.0;
                this.Refresh_Timer.Enabled = true;
                this.Route_Runs_Box_SelectedIndexChanged(null, new EventArgs());
                this.modified = true;
            }
        }

        private void Editor_Form_Closing(object sender, CancelEventArgs e)
        {
            if (!this.modified) return;
            switch (MessageBox.Show(Localization.current_.save_quit/*"Сохранить город перед выходом?"*/, "Transedit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;

                case DialogResult.Yes:
                    this.Save_Item_Click(sender, e);
                    return;
            }
        }
        /// <summary>
        /// Клавиши
        /// </summary>
        private void Editor_Form_KeyDown(object sender, KeyEventArgs e)
        {
        	//HACK: убраны системные звуки при нажатии на клавиши
        	e.SuppressKeyPress = !this.edit_panel.Enabled;
            if ((e.KeyCode == Keys.W) && e.Control)
            {
                MyDirect3D.карта = !MyDirect3D.карта;
                MyDirect3D.масштаб = (MyDirect3D.карта) ? 1.0 : 10.0;
            }
            if ((e.KeyCode == Keys.Add) && e.Control && (MyDirect3D.масштаб < 50))
            {
                MyDirect3D.масштаб = MyDirect3D.масштаб + 1;
            }
            if ((e.KeyCode == Keys.Subtract) && e.Control && (MyDirect3D.масштаб > 1))
            {
                MyDirect3D.масштаб = MyDirect3D.масштаб - 1;
            }
            if (e.KeyCode == Keys.Menu)
            {
                this.alt = true;
            }
            if (e.KeyCode == Keys.ShiftKey)
            {
                this.shift = true;
            }
            if (e.KeyCode == Keys.ControlKey)
            {
                this.ctrl = true;
            }
            if ((this.строящаяся_дорога != null) && (this.строящаяся_дорога.кривая || (this.строительство_дороги == Стадия_стоительства.Нет)))
            {
                if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Z))
                {
                    switch (this.строительство_дороги)
                    {
                        case Стадия_стоительства.Нет:
                        case Стадия_стоительства.Первый_конец:
                            this.строящаяся_дорога.направления[0] -= Math.PI / 36;
                            if (this.строящаяся_дорога.направления[0] <= -Math.PI)//3.1415926535897931)
                            {
                                this.строящаяся_дорога.направления[0] += Math.PI * 2;//6.2831853071795862;
                            }
                            break;

                        case Стадия_стоительства.Второй_конец:
                            this.строящаяся_дорога.направления[1] -= Math.PI / 36;
                            if (this.строящаяся_дорога.направления[1] <= -Math.PI)//3.1415926535897931)
                            {
                                this.строящаяся_дорога.направления[1] += Math.PI * 2;//6.2831853071795862;
                            }
                            break;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.A))
                {
                    switch (this.строительство_дороги)
                    {
                        case Стадия_стоительства.Нет:
                        case Стадия_стоительства.Первый_конец:
                            this.строящаяся_дорога.направления[0] += Math.PI / 36;
                            if (this.строящаяся_дорога.направления[0] > Math.PI)//3.1415926535897931)
                            {
                                this.строящаяся_дорога.направления[0] -= Math.PI * 2;//6.2831853071795862;
                            }
                            break;

                        case Стадия_стоительства.Второй_конец:
                            this.строящаяся_дорога.направления[1] += Math.PI / 36;
                            if (this.строящаяся_дорога.направления[1] > Math.PI)//3.1415926535897931)
                            {
                                this.строящаяся_дорога.направления[1] -= Math.PI * 2;//6.2831853071795862;
                            }
                            break;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
            }
            if (this.строящаяся_дорога != null)
            {
                if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.X))
                {
                    if ((this.строительство_дороги == Стадия_стоительства.Первый_конец) || (this.строительство_дороги == Стадия_стоительства.Нет))
                    {
                        this.строящаяся_дорога.ширина[0] -= 0.5;
                        if (this.строящаяся_дорога.ширина[0] < 0.5)
                        {
                            this.строящаяся_дорога.ширина[0] = 0.5;
                        }
                    }
                    if ((this.строительство_дороги == Стадия_стоительства.Второй_конец) || (this.строительство_дороги == Стадия_стоительства.Нет))
                    {
                        this.строящаяся_дорога.ширина[1] -= 0.5;
                        if (this.строящаяся_дорога.ширина[1] < 0.5)
                        {
                            this.строящаяся_дорога.ширина[1] = 0.5;
                        }
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.S))
                {
                    if ((this.строительство_дороги == Стадия_стоительства.Первый_конец) || (this.строительство_дороги == Стадия_стоительства.Нет))
                    {
                        this.строящаяся_дорога.ширина[0] += 0.5;
                    }
                    if ((this.строительство_дороги == Стадия_стоительства.Второй_конец) || (this.строительство_дороги == Стадия_стоительства.Нет))
                    {
                        this.строящаяся_дорога.ширина[1] += 0.5;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Next) || (e.KeyCode == Keys.C))
                {
                    if ((this.строительство_дороги == Стадия_стоительства.Первый_конец) || (this.строительство_дороги == Стадия_стоительства.Нет))
                    {
                        this.строящаяся_дорога.высота[0] -= 0.5;
                        if (this.строящаяся_дорога.высота[0] < 0.0)
                        {
                            this.строящаяся_дорога.высота[0] = 0.0;
                        }
                    }
                    if ((this.строительство_дороги == Стадия_стоительства.Второй_конец) || (this.строительство_дороги == Стадия_стоительства.Нет))
                    {
                        this.строящаяся_дорога.высота[1] -= 0.5;
                        if (this.строящаяся_дорога.высота[1] < 0.0)
                        {
                            this.строящаяся_дорога.высота[1] = 0.0;
                        }
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Prior) || (e.KeyCode == Keys.D))
                {
                    if ((this.строительство_дороги == Стадия_стоительства.Первый_конец) || (this.строительство_дороги == Стадия_стоительства.Нет))
                    {
                        this.строящаяся_дорога.высота[0] += 0.5;
                    }
                    if ((this.строительство_дороги == Стадия_стоительства.Второй_конец) || (this.строительство_дороги == Стадия_стоительства.Нет))
                    {
                        this.строящаяся_дорога.высота[1] += 0.5;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Escape) && (this.строительство_дороги != Стадия_стоительства.Нет))
                {
                	this.строительство_дороги = Стадия_стоительства.Нет;
                }
            }
            if (this.строящиеся_провода != null)
            {
                if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Z))
                {
                    switch (this.строящиеся_провода.стадия)
                    {
                        case Стадия_стоительства.Нет:
                            this.строящиеся_провода.направление -= Math.PI / 36;//0.087266462599716474 - pi / 36
                            this.строящиеся_провода.направления[0] -= Math.PI / 36;
                            this.строящиеся_провода.направления[1] -= Math.PI / 36;
                            break;

                        case Стадия_стоительства.Второй_конец:
                            this.строящиеся_провода.направления[1] -= Math.PI / 72;//0.043633231299858237
                            break;

                        case Стадия_стоительства.Первый_конец:
                            this.строящиеся_провода.направления[0] -= Math.PI / 72;
                            break;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.A))
                {
                    switch (this.строящиеся_провода.стадия)
                    {
                        case Стадия_стоительства.Нет:
                            this.строящиеся_провода.направление += Math.PI / 36;
                            this.строящиеся_провода.направления[0] += Math.PI / 36;
                            this.строящиеся_провода.направления[1] += Math.PI / 36;
                            break;

                        case Стадия_стоительства.Второй_конец:
                            this.строящиеся_провода.направления[1] += Math.PI / 72;
                            break;

                        case Стадия_стоительства.Первый_конец:
                            this.строящиеся_провода.направления[0] += Math.PI / 72;//pi / 72
                            break;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Next) || (e.KeyCode == Keys.C))
                {
                    if ((this.строящиеся_провода.стадия == Стадия_стоительства.Первый_конец) || (this.строящиеся_провода.стадия == Стадия_стоительства.Нет))
                    {
                        this.строящиеся_провода.высота[0] -= 0.5;
                        if (this.строящиеся_провода.высота[0] < 0.0)
                        {
                            this.строящиеся_провода.высота[0] = 0.0;
                        }
                    }
                    if ((this.строящиеся_провода.стадия == Стадия_стоительства.Второй_конец) || (this.строящиеся_провода.стадия == Стадия_стоительства.Нет))
                    {
                        this.строящиеся_провода.высота[1] -= 0.5;
                        if (this.строящиеся_провода.высота[1] < 0.0)
                        {
                            this.строящиеся_провода.высота[1] = 0.0;
                        }
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Prior) || (e.KeyCode == Keys.D))
                {
                    if ((this.строящиеся_провода.стадия == Стадия_стоительства.Первый_конец) || (this.строящиеся_провода.стадия == Стадия_стоительства.Нет))
                    {
                        this.строящиеся_провода.высота[0] += 0.5;
                    }
                    if ((this.строящиеся_провода.стадия == Стадия_стоительства.Второй_конец) || (this.строящиеся_провода.стадия == Стадия_стоительства.Нет))
                    {
                        this.строящиеся_провода.высота[1] += 0.5;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Escape) && (this.строящиеся_провода.стадия != Стадия_стоительства.Нет))
                {
                	this.строящиеся_провода.стадия = Стадия_стоительства.Нет;
                }
            }
            if ((this.строящаяся_остановка != null) && (e.KeyCode == Keys.Escape))
            {
                this.строящаяся_остановка = null;
                this.EnableControls(true);
                return;
            }
            if (this.строящийся_сигнал_сигнальной_системы != null)
            {
            	if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.X))
                {
            		this.строящийся_сигнал_сигнальной_системы.положение.отклонение = Math.Round(this.строящийся_сигнал_сигнальной_системы.положение.отклонение + 0.2, 1);
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.S))
                {
                    this.строящийся_сигнал_сигнальной_системы.положение.отклонение = Math.Round(this.строящийся_сигнал_сигнальной_системы.положение.отклонение - 0.2, 1);
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.D))
                {
            		this.строящийся_сигнал_сигнальной_системы.положение.высота += 0.2;
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.C))
                {
                    this.строящийся_сигнал_сигнальной_системы.положение.высота = Math.Max(this.строящийся_сигнал_сигнальной_системы.положение.высота - 0.2, 0);
                    this.process_mouse(this.mouse_args, false);
                }
            	if (e.KeyCode == Keys.Escape)
	            {
	                this.строящийся_сигнал_сигнальной_системы = null;
	                this.EnableControls(true);
	                return;
	            }
            }
            if ((this.строящийся_элемент_сигнальной_системы != null) && (e.KeyCode == Keys.Escape))
            {
                this.строящийся_элемент_сигнальной_системы = null;
                this.EnableControls(true);
                return;
            }
            if (this.строящийся_светофор != null)
            {
                if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.X))
                {
                    this.строящийся_светофор.положение.отклонение += 0.2;
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.S))
                {
                    this.строящийся_светофор.положение.отклонение -= 0.2;
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.D))
                {
            		this.строящийся_светофор.положение.высота += 0.2;
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.C))
                {
                    this.строящийся_светофор.положение.высота = Math.Max(this.строящийся_светофор.положение.высота - 0.2, 0);
                    this.process_mouse(this.mouse_args, false);
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.строящийся_светофор = null;
                    this.EnableControls(true);
                    return;
                }
            }
            if ((this.строящийся_светофорный_сигнал != null) && (e.KeyCode == Keys.Escape))
            {
                this.строящийся_светофорный_сигнал = null;
                this.EnableControls(true);
                return;
            }
            if (строящийся_объект != null)
            {
                switch (e.KeyCode)
                {
                    case Keys.Z:
                        строящийся_объект.angle0 -= Math.PI / 36;
                        break;
                    case Keys.A:
                        строящийся_объект.angle0 += Math.PI / 36;
                        break;
                    case Keys.D:
                        строящийся_объект.height0 += 0.5;
                        break;
                    case Keys.C:
                        строящийся_объект.height0 = Math.Max(строящийся_объект.height0 - 0.5, 0.0);
                        break;
                    case Keys.Escape:
                        строящийся_объект = null;
                        EnableControls(true);
                        return;
                }
                if (строящийся_объект.angle0 > Math.PI)
                {
                	строящийся_объект.angle0 -= Math.PI * 2;
                }
                else if (строящийся_объект.angle0 < -Math.PI)
                {
                	строящийся_объект.angle0 += Math.PI * 2;
                }
                UpdateStatusBar();
            }
            if (((e.KeyCode == Keys.Z) && this.Route_Button.Pushed) && ((this.Route_Box.SelectedIndex >= 0) && (this.Route_Runs_Box.SelectedIndex >= 0)))
            {
                Trip рейс = this.выбранный_рейс;
                List<Road> list = new List<Road>(рейс.pathes);
                if (list.Count > 0)
                {
                    while (list[list.Count - 1].следующиеДороги.Length == 1)
                    {
                        list.Add(list[list.Count - 1].следующиеДороги[0]);
                        if (list[list.Count - 1] == рейс.дорога_прибытия)
                        {
                            break;
                        }
                    }
                    рейс.pathes = list.ToArray();
                    this.игрок.cameraPosition.xz_point = рейс.дорога_прибытия.концы[1];
                    this.Route_Runs_ToParkIndex_UpDown.Maximum = this.выбранный_рейс.pathes.Length;
                    this.ОбновитьРаскрашенныеСплайны();
                    this.modified = true;
                }
            }
            if (this.Edit_Button.Pushed)
            {
                if (e.KeyCode == Keys.A)
                {
                    this.Повернуть_всё(Math.PI / 36);
                }
                if (e.KeyCode == Keys.Z)
                {
                    this.Повернуть_всё(-Math.PI / 36);
                }
                if (e.KeyCode == Keys.Q)
                {
                    this.Повернуть_всё(-this.угол_поворота);
                }
            }
        }

        private void Editor_Form_KeyUp(object sender, KeyEventArgs e)
        {
        	e.SuppressKeyPress = false;//а надо ли?
            if (e.KeyCode == Keys.Menu)
            {
                this.alt = false;
            }
            if (e.KeyCode == Keys.ShiftKey)
            {
                this.shift = false;
            }
            if (e.KeyCode == Keys.ControlKey)
            {
                this.ctrl = false;
            }
        }

        private void Editor_Form_Load(object sender, EventArgs e)
        {
            Directory.SetCurrentDirectory(Application.StartupPath + @"\Data");
            if (!MyDirect3D.Initialize(panel, DeviceType.Hardware, CreateFlags.SoftwareVertexProcessing, true))
            {
                MessageBox.Show("Could not initialize Direct3D.", "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                base.Close();
            }
            else
            {
            	MyGUI.splash_title = "Transedit";//"Trancity - editor";
            	MainForm.in_editor = true;
            	Logging.End();
            	Logging.Init();
                MyDirect3D.вид_сверху = true;
                Road.качествоДороги = 5.0;
                this.ApplyLocalization();
                this.RefreshPanelSize(sender, e);
                this.toolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(this.Edit_Button));
                Stop.неЗагружаемКартинки = true;
                /*this.мир = new World();
                this.мир.Create_Meshes();
                this.игрок = new Игрок();
                this.игроки = new Игрок[] { this.игрок };
                this.игра = new Game();
                this.игра.мир = this.мир;
                this.игра.игроки = this.игроки;
                this.UpdatePanels();*/
                this.Reset_World();
                this.без_игроков = new Игрок[0];
                this.refreshing = false;
            }
        }

        private void EnableControls(bool value)
        {
            this.edit_panel.Enabled = value;
            this.toolBar.Enabled = value;
            foreach (MenuItem item in this.Menu.MenuItems)
            {
            	item.Enabled = value;
            }
        }

        private void Exit_Item_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void Find_MinRadius_Item_Click(object sender, EventArgs e)
        {
            double num = 0.0;
            /*Рельс рельс = null;
            foreach (Рельс рельс2 in this.мир.Рельсы)
            {
                if (рельс2.кривая && ((num == 0.0) || (рельс2.АбсолютныйРадиус < num)))
                {
                    рельс = рельс2;
                    num = рельс2.АбсолютныйРадиус;
                }
            }*/
            Road road = null;
            foreach (Road road2 in this.мир.ВсеДороги)
            {
                if (road2.кривая && ((num == 0.0) || (road2.АбсолютныйРадиус < num)))
                {
                    road = road2;
                    num = road2.АбсолютныйРадиус;
                }
            }
            if (road != null)
            {
                this.игрок.cameraPosition.xz_point = road.концы[0];
                MessageBox.Show(this, string.Format(Localization.current_.min_radius/*"Минимальный радиус кривой: {0} м."*/, num.ToString("0.00")), "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show(this, Localization.current_.no_curves/*"Не найдено ни одной кривой!"*/, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Narad_Add_Button_Click(object sender, EventArgs e)
        {
            List<Order> list = new List<Order>(this.выбранный_маршрут.orders);
            string str = (this.выбранный_маршрут.orders.Length + 1).ToString();
            Order item = new Order(new Парк(""), this.выбранный_маршрут, str, "");
            list.Add(item);
            this.выбранный_маршрут.orders = list.ToArray();
            this.Narad_Box.Items.Add(str);
            this.Narad_Box.SelectedIndex = this.Narad_Box.Items.Count - 1;
            this.modified = true;
        }

        private void Narad_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            var flag = (Narad_Box.SelectedIndex >= 0) && (Route_Box.SelectedIndex >= 0);
            Narad_Remove_Button.Enabled = flag;
            Narad_Name_label.Enabled = flag;
            Narad_Name_Box.Enabled = flag;
            Narad_Name_Box.Text = выбранный_наряд.номер;
            Narad_Name_Box.Modified = false;
            Narad_ChangeName_Button.Enabled = false;
            Narad_Park_label.Enabled = flag;
            Narad_Park_Box.Enabled = flag;
            Transport_label.Enabled = flag;
            RollingStockBox.Enabled = flag;
            Narad_Park_Box.SelectedIndex = new List<Парк>(мир.парки).IndexOf(выбранный_наряд.парк);
            Narad_Runs_label.Enabled = flag;
            Narad_Runs_Box.Enabled = flag;
            Narad_Runs_Add_Button.Enabled = flag;
            UpdateNaradControls(flag ? выбранный_наряд : null);
            RollingStockUpdate(выбранный_наряд);
        }

        private void Narad_ChangeName_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Narad_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            this.выбранный_маршрут.orders[selectedIndex].номер = this.Narad_Name_Box.Text;
            this.Narad_Box.Items[selectedIndex] = this.Narad_Name_Box.Text;
            this.Narad_Name_Box.Modified = false;
            this.modified = true;
        }

        private void Narad_Name_Box_ModifiedChanged(object sender, EventArgs e)
        {
            this.Narad_ChangeName_Button.Enabled = this.Narad_Name_Box.Modified;
        }

        private void Narad_Park_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((this.Narad_Park_Box.SelectedIndex >= 0) && (this.Narad_Park_Box.SelectedIndex < this.мир.парки.Length))
            {
                this.выбранный_наряд.парк = this.мир.парки[this.Narad_Park_Box.SelectedIndex];
            }
        }

        private void Narad_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Narad_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            List<Order> list = new List<Order>(this.выбранный_маршрут.orders);
            list.RemoveAt(selectedIndex);
            this.выбранный_маршрут.orders = list.ToArray();
            this.Narad_Box.Items.RemoveAt(selectedIndex);
            this.Narad_Box_SelectedIndexChanged(null, new EventArgs());
            this.modified = true;
        }

        private void Narad_Runs_Add_Button_Click(object sender, EventArgs e)
        {
            List<Trip> list = new List<Trip>(this.выбранный_наряд.рейсы);
            Trip item = new Trip();
            list.Add(item);
            this.выбранный_наряд.рейсы = list.ToArray();
            int length = this.выбранный_наряд.рейсы.Length;
            this.Narad_Runs_Box.Items.Add("Рейс " + length.ToString());
            this.Narad_Runs_Box.SelectedIndex = this.Narad_Runs_Box.Items.Count - 1;
            this.modified = true;
        }

        private void Narad_Runs_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = ((this.Narad_Runs_Box.SelectedIndex >= 0) && (this.Narad_Box.SelectedIndex >= 0)) && (this.Route_Box.SelectedIndex >= 0);
            int selectedIndex = this.Narad_Runs_Box.SelectedIndex;
            this.Narad_Runs_Remove_Button.Enabled = flag;
            this.Narad_Runs_Run_label.Enabled = flag;
            this.Narad_Runs_Run_Box.Enabled = flag;
            this.Narad_Runs_Run_Box.Items.Clear();
            this.Narad_Runs_Run_Box.SelectedIndexChanged -= new EventHandler(this.Narad_Runs_Run_Box_SelectedIndexChanged);
            int num2 = (flag && (this.выбранный_маршрут.AllTrips.Count > 0)) ? 0 : -1;
            if (flag)
            {
                for (int i = 0; i < this.выбранный_маршрут.trips.Count; i++)
                {
                    int num5 = i + 1;
                    this.Narad_Runs_Run_Box.Items.Add("Рейс " + num5.ToString());
                    if ((selectedIndex >= 0) && (this.выбранный_маршрут.trips[i].pathes == this.выбранный_наряд.рейсы[selectedIndex].pathes))
                    {
                        num2 = i;
                    }
                }
                for (int j = 0; j < this.выбранный_маршрут.parkTrips.Count; j++)
                {
                    int num6 = j + 1;
                    this.Narad_Runs_Run_Box.Items.Add("Парковый рейс " + num6.ToString());
                    if ((selectedIndex >= 0) && (this.выбранный_маршрут.parkTrips[j].pathes == this.выбранный_наряд.рейсы[selectedIndex].pathes))
                    {
                        num2 = j + this.выбранный_маршрут.trips.Count;
                    }
                }
            }
            this.Narad_Runs_Run_Box.SelectedIndex = num2;
            this.Narad_Runs_Run_Box.SelectedIndexChanged += new EventHandler(this.Narad_Runs_Run_Box_SelectedIndexChanged);
            this.Narad_Runs_Time1_label.Enabled = flag;
            this.Narad_Runs_Time1_Box.Enabled = flag;
            if (flag)
            {
                this.Narad_Runs_Time1_Box.Time_Seconds = (int)this.выбранный_наряд.рейсы[selectedIndex].время_отправления;
                this.Narad_Runs_Time1_Box_TimeChanged(sender, e);
            }
            else
            {
                this.Narad_Runs_Time1_Box.Time_Seconds = 0;
                this.Narad_Runs_Time2_Box.Time_Seconds = 0;
            }
            this.Narad_Runs_Time2_label.Enabled = flag;
            this.Narad_Runs_Run_Box_SelectedIndexChanged(sender, e);
        }

        private void Narad_Runs_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Narad_Runs_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            List<Trip> list = new List<Trip>(this.выбранный_наряд.рейсы);
            list.RemoveAt(selectedIndex);
            this.выбранный_наряд.рейсы = list.ToArray();
            this.Narad_Runs_Box.Items.RemoveAt(selectedIndex);
            this.Narad_Runs_Box_SelectedIndexChanged(null, new EventArgs());
            this.modified = true;
        }

        private void Narad_Runs_Run_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Narad_Runs_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            this.выбранный_наряд.рейсы[selectedIndex].pathes = this.выбранный_маршрут.AllTrips[this.Narad_Runs_Run_Box.SelectedIndex].pathes;
            this.Narad_Runs_Time1_Box_TimeChanged(sender, e);
        }

        private void Narad_Runs_Time1_Box_TimeChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Narad_Runs_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            this.выбранный_наряд.рейсы[selectedIndex].время_отправления = this.Narad_Runs_Time1_Box.Time_Seconds;
            if (this.выбранный_наряд.рейсы[selectedIndex].время_отправления < 10800.0)
            {
                Trip рейс1 = this.выбранный_наряд.рейсы[selectedIndex];
                рейс1.время_отправления += 86400.0;
            }
            double num2 = 0.0;
            foreach (Trip рейс in this.выбранный_маршрут.AllTrips)
            {
                if (рейс.pathes == this.выбранный_наряд.рейсы[selectedIndex].pathes)
                {
                    num2 = рейс.время_прибытия;
                    break;
                }
            }
            this.выбранный_наряд.рейсы[selectedIndex].время_прибытия = this.выбранный_наряд.рейсы[selectedIndex].время_отправления + num2;
            this.Narad_Runs_Time2_Box.Time_Seconds = (int)this.выбранный_наряд.рейсы[selectedIndex].время_прибытия;
        }

        private void New_Item_Click(object sender, EventArgs e)
        {
            if (this.Set_New_File(this.saveFileDialog))
            {
                /*this.мир = new World();
                this.мир.Create_Meshes();
                this.игрок = new Игрок();
                this.игроки = new Игрок[] { this.игрок };
                this.игра = new Game();
                this.игра.мир = this.мир;
                this.игра.игроки = this.игроки;
                this.UpdatePanels();
                this.угол_поворота = 0.0;*/
                this.Reset_World();
            }
        }

        private void Open_Item_Click(object sender, EventArgs e)
        {
            if (this.Set_New_File(this.openFileDialog))
            {
                this.мир = new World();
                this.игрок = new Игрок();
                this.игроки = new Игрок[] { this.игрок };
                this.игра = new Game();
                this.игра.мир = this.мир;
                this.игра.игроки = this.игроки;
                MyGUI.Splash();
                MyGUI.load_status = 0;
                MyGUI.status_string = Localization.current_.load_city;//"Загрузка города...";
                MyGUI.Splash();
                this.мир.ЗагрузитьГород(this.filename);
                this.мир.Create_Meshes();
                this.UpdatePanels();
                this.угол_поворота = 0.0;
            }
        }
        
        private void Reset_World()
        {
        	this.мир = new World();
            this.мир.Create_Meshes();
            this.игрок = new Игрок();
            this.игроки = new Игрок[] { this.игрок };
            this.игра = new Game();
            this.игра.мир = this.мир;
            this.игра.игроки = this.игроки;
            this.угол_поворота = 0.0;
            this.UpdatePanels();
        }

        private void panel_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouse_args = e;
            if (e.Button == MouseButtons.Right)
            {
                this.dragging = true;
                this.drag_point = new Point(e.X, e.Y);
            }
            this.process_mouse(e, true);
        }

        private void panel_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor_x_Status.Text = "";
            this.Cursor_y_Status.Text = "";
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            this.mouse_args = e;
            if (this.dragging)
            {
                DoublePoint point = new DoublePoint((double)(-e.X + this.drag_point.X), (double)(e.Y - this.drag_point.Y));
                point = (DoublePoint)(point / MyDirect3D.масштаб);
                this.игрок.cameraPosition.xz_point += point;
                this.drag_point = new Point(e.X, e.Y);
            }
            this.process_mouse(e, false);
            this.Cursor_x_Status.Text = "x: " + ((this.cursor_pos.x / 1000.0)).ToString("0.000") + " км";
            this.Cursor_y_Status.Text = "y: " + ((this.cursor_pos.y / 1000.0)).ToString("0.000") + " км";
        }

        private void panel_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouse_args = e;
            if (e.Button == MouseButtons.Right)
            {
                this.dragging = false;
            }
        }

        private void Park_Add_Button_Click(object sender, EventArgs e)
        {
            List<Парк> list = new List<Парк>(this.мир.парки);
            list.Add(new Парк("Парк"));
            this.мир.парки = list.ToArray();
            UpdateParksList();
            this.Park_Box.SelectedIndex = this.Park_Box.Items.Count - 1;
            this.Narad_Box_SelectedIndexChanged(sender, e);
            this.modified = true;
        }

        private void Park_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.Park_Box.SelectedIndex >= 0;
            this.Park_In_Button.Enabled = flag;
            this.Park_Out_Button.Enabled = flag;
            this.Park_Rails_Button.Enabled = flag;
            this.Park_Remove_Button.Enabled = flag;
            this.Park_Name_label.Enabled = flag;
            this.Park_Name_Box.Enabled = flag;
            this.Park_Name_Box.Text = flag ? this.мир.парки[this.Park_Box.SelectedIndex].название : string.Empty;
            this.Park_Name_Box.Modified = false;
            this.Park_ChangeName_Button.Enabled = false;
            this.ОбновитьРаскрашенныеСплайны();
        }

        private void Park_ChangeName_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Park_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            this.мир.парки[selectedIndex].название = this.Park_Name_Box.Text;
            UpdateParksList();
            this.Park_Box.SelectedIndex = selectedIndex;
            this.Park_Name_Box.Modified = false;
            this.Narad_Box_SelectedIndexChanged(sender, e);
            this.modified = true;
        }

        private void Park_Name_Box_ModifiedChanged(object sender, EventArgs e)
        {
            this.Park_ChangeName_Button.Enabled = this.Park_Name_Box.Modified;
        }

        private void Park_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Park_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            List<Парк> list = new List<Парк>(this.мир.парки);
            list.RemoveAt(selectedIndex);
            this.мир.парки = list.ToArray();
			UpdateParksList();
            this.Park_Box.SelectedIndex = selectedIndex - 1;
            this.modified = true;
        }

        private void process_mouse(MouseEventArgs e, bool click)
        {
            DoublePoint point20;
            Положение nearest_post = this.мир.Найти_ближайшее_положение(this.cursor_pos);//this.ближайшее_положение;
            if (this.строящаяся_дорога == null)
            {
                if ((this.Rail_Button.Pushed && click) && ((e.Button == MouseButtons.Left) && (nearest_post.Дорога != null)))
                {
                	if (!this.Spline_Select_mode_Box.Checked)
                	{
                		this.мир.listДороги.Remove(nearest_post.Дорога);
                		UpdateSplinesList();
                		this.modified = true;
                	}
                	else
                	{
                		this.Splines_Instance_Box.SelectedIndex = this.мир.listДороги.IndexOf(nearest_post.Дорога);
                		time_color = 2.0;
	        			this.ОбновитьРаскрашенныеСплайны();
                	}
                }
            }
            else
            {
                this.строящаяся_дорога.кривая = this.Rail_Build_Curve_Button.Pushed;
                DoublePoint point = new DoublePoint(20.0, this.строящаяся_дорога.кривая ? ((double)10) : ((double)0));
                DoublePoint point2 = new DoublePoint(-20.0, this.строящаяся_дорога.кривая ? ((double)10) : ((double)0));
                DoublePoint point3 = (this.shift) ? this.cursor_pos.round_point : this.cursor_pos;
                /*if (this.shift)
                {
                    point3 = point3.round_point;
                }*/
                if ((this.ctrl && this.строящаяся_дорога.кривая) && (this.строительство_дороги != Стадия_стоительства.Нет))
                {
                    this.строящаяся_дорога.ОбновитьСтруктуру();
                    if (this.строительство_дороги == Стадия_стоительства.Второй_конец)
                    {
                    	DoublePoint point4 = this.строящаяся_дорога.концы[0] + new DoublePoint(this.строящаяся_дорога.направления[0] + (MyFeatures.halfPI));//1.5707963267948966);
                        DoublePoint point5 = point4 + ((this.строящаяся_дорога.концы[0] - point4) * new DoublePoint((Math.PI + this.строящаяся_дорога.направления[1]) - this.строящаяся_дорога.направления[0]));
                        DoublePoint point6 = point3 - this.строящаяся_дорога.концы[0];
                        point20 = point5 - this.строящаяся_дорога.концы[0];
                        double angle = point20.угол;
                        point6 /= new DoublePoint(angle);
                        point6.y = 0.0;
                        point6 *= new DoublePoint(angle);
                        point3 = this.строящаяся_дорога.концы[0] + point6;
                    }
                    if (this.строительство_дороги == Стадия_стоительства.Первый_конец)
                    {
                        DoublePoint point7 = this.строящаяся_дорога.концы[1] + new DoublePoint(this.строящаяся_дорога.направления[1] + (MyFeatures.halfPI));//1.5707963267948966);
                        DoublePoint point8 = point7 + ((this.строящаяся_дорога.концы[1] - point7) * new DoublePoint((Math.PI + this.строящаяся_дорога.направления[0]) - this.строящаяся_дорога.направления[1]));
                        DoublePoint point9 = point3 - this.строящаяся_дорога.концы[1];
                        DoublePoint point21 = point8 - this.строящаяся_дорога.концы[1];
                        double num2 = point21.угол;
                        point9 /= new DoublePoint(num2);
                        point9.y = 0.0;
                        point9 *= new DoublePoint(num2);
                        point3 = this.строящаяся_дорога.концы[1] + point9;
                    }
                }
                switch (this.строительство_дороги)
                {
                    case Стадия_стоительства.Нет:
                        this.строящаяся_дорога.концы[0] = point3;
                        this.строящаяся_дорога.концы[1] = point3 + point;
                        break;

                    case Стадия_стоительства.Второй_конец:
                        this.строящаяся_дорога.концы[1] = point3;
                        break;

                    case Стадия_стоительства.Первый_конец:
                        this.строящаяся_дорога.концы[0] = point3;
                        break;
                }
                bool flag = false;
                bool flag2 = false;
                DoublePoint point22 = this.строящаяся_дорога.концы[1] - this.строящаяся_дорога.концы[0];
                if (point22.модуль > 1.0)
                {
                    Road[] дорогаArray = this.мир.Дороги;
                    if (this.строящаяся_дорога is Рельс)
                    {
                        дорогаArray = this.мир.Рельсы;
                    }
                    foreach (Road дорога in дорогаArray)
                    {
                        if ((this.строительство_дороги == Стадия_стоительства.Нет) || (this.строительство_дороги == Стадия_стоительства.Первый_конец))
                        {
                            point20 = this.строящаяся_дорога.концы[0] - дорога.концы[1];
                            if (point20.модуль < 1.0)
                            {
                                flag2 = true;
                                this.строящаяся_дорога.концы[0] = дорога.концы[1];
                                if (this.строящаяся_дорога.кривая || (this.строительство_дороги == Стадия_стоительства.Нет))
                                {
                                    this.строящаяся_дорога.направления[0] = дорога.направления[1] + Math.PI;//3.1415926535897931;
                                    if (!this.строящаяся_дорога.кривая)
                                    {
                                        this.строящаяся_дорога.направления[1] = this.строящаяся_дорога.направления[0] - Math.PI;//3.1415926535897931;
                                    }
                                }
                                this.строящаяся_дорога.ширина[0] = дорога.ширина[1];
                                this.строящаяся_дорога.высота[0] = дорога.высота[1];
                                if (this.строительство_дороги == Стадия_стоительства.Нет)
                                {
                                    this.строящаяся_дорога.ширина[1] = дорога.ширина[1];
                                    this.строящаяся_дорога.высота[1] = дорога.высота[1];
                                    this.строящаяся_дорога.концы[1] = this.строящаяся_дорога.концы[0] + (new DoublePoint(this.строящаяся_дорога.направления[0]) * point);
                                }
                                break;
                            }
                        }
                        if ((this.строительство_дороги == Стадия_стоительства.Нет) || (this.строительство_дороги == Стадия_стоительства.Второй_конец))
                        {
                            point20 = this.строящаяся_дорога.концы[1] - дорога.концы[0];
                            if (point20.модуль < 1.0)
                            {
                                flag = true;
                                this.строящаяся_дорога.концы[1] = дорога.концы[0];
                                if (this.строящаяся_дорога.кривая || (this.строительство_дороги == Стадия_стоительства.Нет))
                                {
                                    this.строящаяся_дорога.направления[1] = дорога.направления[0] + Math.PI;//3.1415926535897931;
                                    if (!this.строящаяся_дорога.кривая)
                                    {
                                        this.строящаяся_дорога.направления[0] = this.строящаяся_дорога.направления[1] - Math.PI;//3.1415926535897931;
                                    }
                                }
                                this.строящаяся_дорога.ширина[1] = дорога.ширина[0];
                                this.строящаяся_дорога.высота[1] = дорога.высота[0];
                                if (this.строительство_дороги == Стадия_стоительства.Нет)
                                {
                                    this.строящаяся_дорога.высота[0] = дорога.высота[0];
                                    this.строящаяся_дорога.ширина[0] = дорога.ширина[0];
                                    this.строящаяся_дорога.концы[0] = this.строящаяся_дорога.концы[1] + (new DoublePoint(дорога.направления[0]) * point2);
                                }
                                break;
                            }
                        }
                    }
                }
                if (!this.строящаяся_дорога.кривая)
                {
                    double num3 = Math.Abs((double)(this.строящаяся_дорога.ширина[0] - this.строящаяся_дорога.ширина[1])) / 2.0;
                    if (this.строительство_дороги == Стадия_стоительства.Нет)
                    {
                        if (!flag2 && !flag)
                        {
                            this.строящаяся_дорога.концы[1] = this.строящаяся_дорога.концы[0] + (new DoublePoint(this.строящаяся_дорога.направления[0]) * point);
                            this.строящаяся_дорога.направления[1] = this.строящаяся_дорога.направления[0] + Math.PI;//3.1415926535897931;
                            if (this.строящаяся_дорога.направления[1] > Math.PI)//3.1415926535897931)
                            {
                                this.строящаяся_дорога.направления[1] -= Math.PI * 2.0;//6.2831853071795862;
                            }
                        }
                    }
                    else if (this.строительство_дороги == Стадия_стоительства.Второй_конец)
                    {
                        DoublePoint point10 = this.строящаяся_дорога.концы[1] - this.строящаяся_дорога.концы[0];
                        point10 /= new DoublePoint(this.строящаяся_дорога.направления[0]);
                        if (this.ctrl)
                        {
                            point10.y = 0.0;
                        }
                        else if (Math.Abs(point10.y) > num3)
                        {
                            point10.y = Math.Sign(point10.y) * num3;
                        }
                        if (point10.x < 1.0)
                        {
                            point10.x = 1.0;
                        }
                        point10 *= new DoublePoint(this.строящаяся_дорога.направления[0]);
                        this.строящаяся_дорога.концы[1] = this.строящаяся_дорога.концы[0] + point10;
                    }
                    else if (this.строительство_дороги == Стадия_стоительства.Первый_конец)
                    {
                        DoublePoint point11 = this.строящаяся_дорога.концы[0] - this.строящаяся_дорога.концы[1];
                        point11 /= new DoublePoint(this.строящаяся_дорога.направления[1]);
                        if (this.ctrl)
                        {
                            point11.y = 0.0;
                        }
                        else if (Math.Abs(point11.y) > num3)
                        {
                            point11.y = Math.Sign(point11.y) * num3;
                        }
                        if (point11.x < 1.0)
                        {
                            point11.x = 1.0;
                        }
                        point11 *= new DoublePoint(this.строящаяся_дорога.направления[1]);
                        this.строящаяся_дорога.концы[0] = this.строящаяся_дорога.концы[1] + point11;
                    }
                }
                this.строящаяся_дорога.ОбновитьСледующиеДороги(this.мир.ВсеДороги);
                if (click && (e.Button == MouseButtons.Left))
                {
                    switch (this.строительство_дороги)
                    {
                        case Стадия_стоительства.Нет:
                            if (!flag2)
                            {
                                if (flag)
                                {
                                    this.строительство_дороги = Стадия_стоительства.Первый_конец;
                                }
                                else
                                {
                                    this.строительство_дороги = Стадия_стоительства.Второй_конец;
                                }
                            }
                            else
                            {
                                this.строительство_дороги = Стадия_стоительства.Второй_конец;
                            }
                            goto Label_0BE2;

                        case Стадия_стоительства.Второй_конец:
                        case Стадия_стоительства.Первый_конец:
                            this.строительство_дороги = Стадия_стоительства.Нет;
                            this.строящаяся_дорога.Color = 0;
                            this.мир.listДороги.Add(this.строящаяся_дорога);

                            if ((this.Rail_Build_попутки_Button.Pushed) && (this.Rail_Build_попутки2_Button.Pushed))
                            {
                                this.мир.listДороги.Add(this.строящаяся_дорога1);
                            }
                            if ((this.Rail_Build_попутки_Button.Pushed) && (this.Rail_Build_попутки3_Button.Pushed))
                            {
                                this.мир.listДороги.Add(this.строящаяся_дорога1);
                                this.мир.listДороги.Add(this.строящаяся_дорога2);
                            }
                            if ((this.Rail_Build_встречки_Button.Pushed) && (this.Rail_Build_встречки1_Button.Pushed))
                            {
                                this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            }
                            if ((this.Rail_Build_встречки_Button.Pushed) && (this.Rail_Build_встречки2_Button.Pushed))
                            {
                                this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                                this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                            }
                            if ((this.Rail_Build_встречки_Button.Pushed) && (this.Rail_Build_встречки3_Button.Pushed))
                            {
                                this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                                this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                                this.мир.listДороги.Add(this.строящаяся_обратная_дорога2);
                            }
                            foreach (Road дорога2 in this.мир.ВсеДороги)
                            {
                                дорога2.ОбновитьСледующиеДороги(this.мир.ВсеДороги);
                            }
                            UpdateSplinesList();
                            this.modified = true;
                            this.строящаяся_дорога = null;
                            this.toolBar_ButtonClick(null, new ToolBarButtonClickEventArgs(null));
                            goto Label_0BE2;
                    }
                }
            }
        Label_0BE2:
            if (this.строящиеся_провода == null)
            {
            	if ((this.Troll_lines_Button.Pushed && click) && (e.Button == MouseButtons.Left))// && (this.ближайшие_провода != null)))
                {
            		if (this.ближайшие_провода != null)
            		{
	                    Контактный_провод[] _проводArray2 = this.ближайшие_провода;
	                    List<Контактный_провод> list = new List<Контактный_провод>(this.мир.контактныеПровода);
	                    foreach (Контактный_провод _провод4 in _проводArray2)
	                    {
	                        list.Remove(_провод4);
	                    }
	                    this.мир.контактныеПровода = list.ToArray();
	                    this.modified = true;
            		}
            		else if (this.ближайший_провод != null)
            		{
            			List<Трамвайный_контактный_провод> list2 = new List<Трамвайный_контактный_провод>(this.мир.контактныеПровода2);
            			list2.Remove(this.ближайший_провод);
            			this.мир.контактныеПровода2 = list2.ToArray();
                    	this.modified = true;
            		}
                }
            }
            else
            {
                DoublePoint point12 = new DoublePoint(20.0, 0.0);
                DoublePoint point13 = new DoublePoint(-20.0, 0.0);
                DoublePoint point14 = (this.shift) ? this.cursor_pos.round_point : this.cursor_pos;
                /*if (this.shift)
                {
                    point14 = point14.round_point;
                }*/
                switch (this.строящиеся_провода.стадия)
                {
                    case Стадия_стоительства.Нет:
                        this.строящиеся_провода.концы[0] = point14;
                        this.строящиеся_провода.концы[1] = point14 + point12;
                        break;

                    case Стадия_стоительства.Второй_конец:
                        this.строящиеся_провода.концы[1] = point14;
                        break;

                    case Стадия_стоительства.Первый_конец:
                        this.строящиеся_провода.концы[0] = point14;
                        break;
                }
                bool flag3 = false;
                bool flag4 = false;
                bool flag5 = (this.строящиеся_провода is Строящиеся_трамвайные_провода);
                point20 = this.строящиеся_провода.концы[1] - this.строящиеся_провода.концы[0];
                if (point20.модуль > 1.0)
                {
                	if (!flag5)
                	{
	                	for (int i = 0; i < (this.мир.контактныеПровода.Length - 1); i += 2)//TODO:
	                    {
	                        DoublePoint[] pointArray;
	                        Контактный_провод _провод = this.мир.контактныеПровода[i];
	                        Контактный_провод _провод2 = this.мир.контактныеПровода[i + 1];
	                        if ((this.строящиеся_провода.стадия == Стадия_стоительства.Нет) || (this.строящиеся_провода.стадия == Стадия_стоительства.Первый_конец))
	                        {
	                            point20 = this.строящиеся_провода.концы[0] - ((DoublePoint)((_провод.конец + _провод2.конец) / 2.0));
	                            if (point20.модуль < 1.0)
	                            {
	                                flag4 = true;
	                                this.строящиеся_провода.концы[0] = (DoublePoint)((_провод.конец + _провод2.конец) / 2.0);
	                                pointArray = new DoublePoint[] { _провод.конец, _провод2.конец };
	                                this.строящиеся_провода.начало = pointArray;
	                                point20 = _провод2.конец - _провод.конец;
	                                double num5 = point20.угол - MyFeatures.halfPI;//1.5707963267948966;
	                                this.строящиеся_провода.направления[0] = num5;
	                                this.строящиеся_провода.высота[0] = _провод.высота[1];
	                                if (this.строящиеся_провода.стадия == Стадия_стоительства.Нет)
	                                {
	                                    this.строящиеся_провода.высота[1] = _провод.высота[1];
	                                    point20 = _провод.конец - _провод2.конец;
	                                    if (Math.Abs((double)(Контактный_провод.расстояние_между_проводами - point20.модуль)) < 0.001)
	                                    {
	                                        this.строящиеся_провода.направление = num5;
	                                    }
	                                    else
	                                    {
	                                        this.строящиеся_провода.направление = (2.0 * num5) - _провод.направление;
	                                    }
	                                    this.строящиеся_провода.направления[1] = this.строящиеся_провода.направление;
	                                    this.строящиеся_провода.концы[1] = this.строящиеся_провода.концы[0] + (new DoublePoint(this.строящиеся_провода.направление) * point12);
	                                }
	                                break;
	                            }
	                        }
	                        if ((this.строящиеся_провода.стадия == Стадия_стоительства.Нет) || (this.строящиеся_провода.стадия == Стадия_стоительства.Второй_конец))
	                        {
	                            point20 = this.строящиеся_провода.концы[1] - ((DoublePoint)((_провод.начало + _провод2.начало) / 2.0));
	                            if (point20.модуль < 1.0)
	                            {
	                                flag3 = true;
	                                this.строящиеся_провода.концы[1] = (DoublePoint)((_провод.начало + _провод2.начало) / 2.0);
	                                pointArray = new DoublePoint[] { _провод.начало, _провод2.начало };
	                                this.строящиеся_провода.конец = pointArray;
	                                point20 = _провод2.начало - _провод.начало;
	                                double num6 = point20.угол - MyFeatures.halfPI;//1.5707963267948966;
	                                this.строящиеся_провода.направления[1] = num6;
	                                this.строящиеся_провода.высота[1] = _провод.высота[0];
	                                if (this.строящиеся_провода.стадия == Стадия_стоительства.Нет)
	                                {
	                                    this.строящиеся_провода.высота[0] = _провод.высота[0];
	                                    point20 = _провод.начало - _провод2.начало;
	                                    if (Math.Abs((double)(Контактный_провод.расстояние_между_проводами - point20.модуль)) < 0.001)
	                                    {
	                                        this.строящиеся_провода.направление = num6;
	                                    }
	                                    else
	                                    {
	                                        this.строящиеся_провода.направление = (2.0 * num6) - _провод.направление;
	                                    }
	                                    this.строящиеся_провода.направления[0] = this.строящиеся_провода.направление;
	                                    this.строящиеся_провода.концы[0] = this.строящиеся_провода.концы[1] + (new DoublePoint(this.строящиеся_провода.направление) * point13);
	                                }
	                                break;
	                            }
	                        }
	                	}
                	}
                	else
                	{
                		for (int i = 0; i < this.мир.контактныеПровода2.Length; i++)
	                    {
	                        Трамвайный_контактный_провод _провод = this.мир.контактныеПровода2[i];
	                        if ((this.строящиеся_провода.стадия == Стадия_стоительства.Нет) || (this.строящиеся_провода.стадия == Стадия_стоительства.Первый_конец))
	                        {
	                            point20 = this.строящиеся_провода.концы[0] - _провод.конец;
	                            if (point20.модуль < 1.0)
	                            {
	                                flag4 = true;
	                                this.строящиеся_провода.концы[0] = _провод.конец;
	                                this.строящиеся_провода.начало = new DoublePoint[1] { _провод.конец };
	                                if (!((Строящиеся_трамвайные_провода)this.строящиеся_провода).flag){
	                                this.строящиеся_провода.направление = _провод.направление;
	                                ((Строящиеся_трамвайные_провода)this.строящиеся_провода).flag = true;}
	                                this.строящиеся_провода.высота[0] = _провод.высота[1];
	                                if (this.строящиеся_провода.стадия == Стадия_стоительства.Нет)
	                                {
	                                    this.строящиеся_провода.высота[1] = _провод.высота[1];
	                                    /*point20 = _провод.конец - _провод2.конец;
	                                    if (Math.Abs((double)(Контактный_провод.расстояние_между_проводами - point20.модуль)) < 0.001)
	                                    {
	                                        this.строящиеся_провода.направление = num5;
	                                    }
	                                    else
	                                    {
	                                        this.строящиеся_провода.направление = (2.0 * num5) - _провод.направление;
	                                    }*/
	                                    this.строящиеся_провода.направления[1] = this.строящиеся_провода.направление;
	                                    this.строящиеся_провода.концы[1] = this.строящиеся_провода.концы[0] + (new DoublePoint(this.строящиеся_провода.направление) * point12);
	                                }
	                                break;
	                            }
	                        }
	                        if ((this.строящиеся_провода.стадия == Стадия_стоительства.Нет) || (this.строящиеся_провода.стадия == Стадия_стоительства.Второй_конец))
	                        {
	                            point20 = this.строящиеся_провода.концы[1] - _провод.начало;
	                            if (point20.модуль < 1.0)
	                            {
	                                flag3 = true;
	                                this.строящиеся_провода.концы[1] = _провод.начало;
	                                this.строящиеся_провода.конец =  new DoublePoint[1] { _провод.начало };
//	                                point20 = _провод2.начало - _провод.начало;
//	                                double num6 = point20.угол - 1.5707963267948966;
//	                                this.строящиеся_провода.направления[1] = num6;
	                                this.строящиеся_провода.высота[1] = _провод.высота[0];
	                                if (this.строящиеся_провода.стадия == Стадия_стоительства.Нет)
	                                {
	                                    this.строящиеся_провода.высота[0] = _провод.высота[0];
	                                    /*point20 = _провод.начало - _провод2.начало;
	                                    if (Math.Abs((double)(Контактный_провод.расстояние_между_проводами - point20.модуль)) < 0.001)
	                                    {
	                                        this.строящиеся_провода.направление = num6;
	                                    }
	                                    else
	                                    {
	                                        this.строящиеся_провода.направление = (2.0 * num6) - _провод.направление;
	                                    }*/
	                                    this.строящиеся_провода.направления[0] = this.строящиеся_провода.направление;
	                                    this.строящиеся_провода.концы[0] = this.строящиеся_провода.концы[1] + (new DoublePoint(this.строящиеся_провода.направление) * point13);
	                                }
	                                break;
	                            }
	                        }
	                    }
                	}
                }
                if (this.строящиеся_провода.стадия == Стадия_стоительства.Нет)
                {
                    if (!flag4 && !flag3)
                    {
                        this.строящиеся_провода.концы[1] = this.строящиеся_провода.концы[0] + (new DoublePoint(this.строящиеся_провода.направление) * point12);
                        this.строящиеся_провода.начало = null;
                        this.строящиеся_провода.конец = null;
                        if (this.строящиеся_провода is Строящиеся_трамвайные_провода) ((Строящиеся_трамвайные_провода)this.строящиеся_провода).flag = false;
                    }
                }
                else if (this.строящиеся_провода.стадия == Стадия_стоительства.Второй_конец)
                {
                    DoublePoint point15 = this.строящиеся_провода.концы[1] - this.строящиеся_провода.концы[0];
                    point15 /= new DoublePoint(this.строящиеся_провода.направление);
                    if (this.ctrl)
                    {
                        point15.y = 0.0;
                    }
                    if (point15.x < 1.0)
                    {
                        point15.x = 1.0;
                    }
                    point15 *= new DoublePoint(this.строящиеся_провода.направление);
                    this.строящиеся_провода.концы[1] = this.строящиеся_провода.концы[0] + point15;
                    if (!flag3)
                    {
                        this.строящиеся_провода.конец = null;
                    }
                }
                else if (this.строящиеся_провода.стадия == Стадия_стоительства.Первый_конец)
                {
                    DoublePoint point16 = this.строящиеся_провода.концы[0] - this.строящиеся_провода.концы[1];
                    point16 /= new DoublePoint(this.строящиеся_провода.направление + Math.PI);
                    if (this.ctrl)
                    {
                        point16.y = 0.0;
                    }
                    if (point16.x < 1.0)
                    {
                        point16.x = 1.0;
                    }
                    point16 *= new DoublePoint(this.строящиеся_провода.направление + Math.PI);
                    this.строящиеся_провода.концы[0] = this.строящиеся_провода.концы[1] + point16;
                    if (!flag4)
                    {
                        this.строящиеся_провода.начало = null;
                    }
                }
                this.строящиеся_провода.Обновить();
                if (click && (e.Button == MouseButtons.Left))
                {
                    switch (this.строящиеся_провода.стадия)
                    {
                        case Стадия_стоительства.Нет:
                            if (!flag4)
                            {
                                if (flag3)
                                {
                                    this.строящиеся_провода.стадия = Стадия_стоительства.Первый_конец;
                                }
                                else
                                {
                                    this.строящиеся_провода.стадия = Стадия_стоительства.Второй_конец;
                                }
                            }
                            else
                            {
                                this.строящиеся_провода.стадия = Стадия_стоительства.Второй_конец;
                            }
                            goto Label_1800;

                        case Стадия_стоительства.Второй_конец:
                        case Стадия_стоительства.Первый_конец:
                            {
                                this.строящиеся_провода.стадия = Стадия_стоительства.Нет;
                                this.строящиеся_провода.провода[0].color = 0;
//                                int num9 = 1;
                                if (!flag5)
                                {
                                	this.строящиеся_провода.провода[1].color = 0;
//                                	num9 = 2;
                                }
//                                this.строящиеся_провода.провода[1].color = 0;
//                                int num9 = (!flag5) ? 2 : 1;//(строящиеся_провода._type == Контактный_провод.Type.Trolleybus) ?  2 : 1;//TODO:
                                /*foreach (Контактный_провод _провод3 in this.мир.контактныеПровода)
                                {
                                    DoublePoint point17 = _провод3.конец - _провод3.начало;
                                    for (int m = 0; m < num9; m++)//TODO: 2
                                    {
                                        if ((_провод3.высота[0] == this.строящиеся_провода.провода[m].высота[0]) && (_провод3.высота[1] == this.строящиеся_провода.провода[m].высота[1]))
                                        {
                                            DoublePoint point18 = this.строящиеся_провода.провода[m].начало - _провод3.начало;
                                            DoublePoint point19 = this.строящиеся_провода.провода[m].конец - _провод3.начало;
                                            point18.угол -= point17.угол;
                                            point19.угол -= point17.угол;
                                            if (Math.Sign(point18.y) != Math.Sign(point19.y))
                                            {
                                                double num8 = point18.x + (((point19.x - point18.x) * (0.0 - point18.y)) / (point19.y - point18.y));
                                                if ((num8 > 0.001) && (num8 < (point17.модуль - 0.001)))
                                                {
                                                    if (_провод3.правый == this.строящиеся_провода.провода[m].правый)
                                                    {
                                                        _провод3.обесточенный = true;
                                                        this.строящиеся_провода.провода[m].обесточенный = true;
                                                    }
                                                    else
                                                    {
                                                        point20 = new DoublePoint(_провод3.направление - this.строящиеся_провода.провода[m].направление);
                                                        if (point20.угол < 0.0)
                                                        {
                                                            _провод3.обесточенный = true;
                                                        }
                                                        else
                                                        {
                                                            this.строящиеся_провода.провода[m].обесточенный = true;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }*/
                                if (flag5)
                                {
                                	this.строящиеся_провода.провода[0].обесточенный = false;
                                	foreach (Контактный_провод _провод3 in this.мир.контактныеПровода)
	                                {
                                		if (this.строящиеся_провода.провода[0].обесточенный) break;
	                                    DoublePoint point17 = _провод3.конец - _провод3.начало;
                                        if ((_провод3.высота[0] == this.строящиеся_провода.провода[0].высота[0]) && (_провод3.высота[1] == this.строящиеся_провода.провода[0].высота[1]))
                                        {
                                            DoublePoint point18 = this.строящиеся_провода.провода[0].начало - _провод3.начало;
                                            DoublePoint point19 = this.строящиеся_провода.провода[0].конец - _провод3.начало;
                                            point18.угол -= point17.угол;
                                            point19.угол -= point17.угол;
                                            if (Math.Sign(point18.y) != Math.Sign(point19.y))
                                            {
                                                double num8 = point18.x + (((point19.x - point18.x) * (0.0 - point18.y)) / (point19.y - point18.y));
                                                if ((num8 > 0.001) && (num8 < (point17.модуль - 0.001)))
                                                {
                                                    point20 = new DoublePoint(_провод3.направление - this.строящиеся_провода.провода[0].направление);
                                                    this.строящиеся_провода.провода[0].обесточенный = (point20.угол != 0.0);
                                                }
                                            }
                                        }
	                                }
                                	Трамвайный_контактный_провод[] _проводArray2 = this.мир.контактныеПровода2;
	                                this.мир.контактныеПровода2 = new Трамвайный_контактный_провод[_проводArray2.Length + 1];
	                                for (int j = 0; j < _проводArray2.Length; j++)
	                                {
	                                    this.мир.контактныеПровода2[j] = _проводArray2[j];
	                                }
	                                this.мир.контактныеПровода2[_проводArray2.Length] = (Трамвайный_контактный_провод)this.строящиеся_провода.провода[0];
                                }
                                else
                                {
                                	foreach (Контактный_провод _провод3 in this.мир.контактныеПровода2)
	                                {
	                                    DoublePoint point17 = _провод3.конец - _провод3.начало;
	                                    for (int k = 0; k < 2; k++)
	                                    {
	                                    	if ((_провод3.высота[0] == this.строящиеся_провода.провода[k].высота[0]) && (_провод3.высота[1] == this.строящиеся_провода.провода[k].высота[1]))
	                                        {
	                                            DoublePoint point18 = this.строящиеся_провода.провода[k].начало - _провод3.начало;
	                                            DoublePoint point19 = this.строящиеся_провода.провода[k].конец - _провод3.начало;
	                                            point18.угол -= point17.угол;
	                                            point19.угол -= point17.угол;
	                                            if (Math.Sign(point18.y) != Math.Sign(point19.y))
	                                            {
	                                                double num8 = point18.x + (((point18.x - point19.x) * point18.y) / (point19.y - point18.y));//(0.0 - point18.y)) / (point19.y - point18.y));
	                                                if ((num8 > 0.001) && (num8 < (point17.модуль - 0.001)))
	                                                {
	                                                    point20 = new DoublePoint(_провод3.направление - this.строящиеся_провода.провода[k].направление);
	                                                    _провод3.обесточенный = (point20.угол != 0.0);
	                                                }
	                                            }
	                                        }
	                                    }
	                                }
                                	foreach (Контактный_провод _провод3 in this.мир.контактныеПровода)
	                                {
	                                    DoublePoint point17 = _провод3.конец - _провод3.начало;
	                                    for (int m = 0; m < 2; m++)//TODO:2
	                                    {
	                                        if ((_провод3.высота[0] == this.строящиеся_провода.провода[m].высота[0]) && (_провод3.высота[1] == this.строящиеся_провода.провода[m].высота[1]))
	                                        {
	                                            DoublePoint point18 = this.строящиеся_провода.провода[m].начало - _провод3.начало;
	                                            DoublePoint point19 = this.строящиеся_провода.провода[m].конец - _провод3.начало;
	                                            point18.угол -= point17.угол;
	                                            point19.угол -= point17.угол;
	                                            if (Math.Sign(point18.y) != Math.Sign(point19.y))
	                                            {
	                                                double num8 = point18.x + (((point18.x - point19.x) * point18.y) / (point19.y - point18.y));//(0.0 - point18.y)) / (point19.y - point18.y));
	                                                if ((num8 > 0.001) && (num8 < (point17.модуль - 0.001)))
	                                                {
	                                                    if (_провод3.правый == this.строящиеся_провода.провода[m].правый)
	                                                    {
	                                                        _провод3.обесточенный = true;
	                                                        this.строящиеся_провода.провода[m].обесточенный = true;
	                                                    }
	                                                    else
	                                                    {
	                                                        point20 = new DoublePoint(_провод3.направление - this.строящиеся_провода.провода[m].направление);
	                                                        if (point20.угол < 0.0)
	                                                        {
	                                                            _провод3.обесточенный = true;
	                                                        }
	                                                        else
	                                                        {
	                                                            this.строящиеся_провода.провода[m].обесточенный = true;
	                                                        }
	                                                    }
	                                                }
	                                            }
	                                        }
	                                    }
	                                }
	                                Контактный_провод[] _проводArray = this.мир.контактныеПровода;
	                                this.мир.контактныеПровода = new Контактный_провод[_проводArray.Length + 2];
	                                for (int j = 0; j < _проводArray.Length; j++)
	                                {
	                                    this.мир.контактныеПровода[j] = _проводArray[j];
	                                }
	                                for (int k = 0; k < 2; k++)
	                                {
	                                    this.мир.контактныеПровода[_проводArray.Length + k] = this.строящиеся_провода.провода[k];
	                                }
                                }
                                this.modified = true;
                                this.строящиеся_провода = null;
                                this.toolBar_ButtonClick(null, new ToolBarButtonClickEventArgs(null));
                                goto Label_1800;
                            }
                    }
                }
            }
        Label_1800:
            UpdateStatusBar();
            if ((this.строящаяся_остановка != null) && (nearest_post.Дорога != null))
            {
                this.строящаяся_остановка.road = nearest_post.Дорога;
                this.строящаяся_остановка.distance = nearest_post.расстояние;
                this.строящаяся_остановка.UpdatePosition(this.мир);
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.строящаяся_остановка = null;
                    this.EnableControls(true);
                }
            }
            if ((this.строящийся_элемент_сигнальной_системы != null) && (nearest_post.Дорога != null))
            {
                this.строящийся_элемент_сигнальной_системы.дорога = nearest_post.Дорога;
                this.строящийся_элемент_сигнальной_системы.расстояние = nearest_post.расстояние;
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.строящийся_элемент_сигнальной_системы = null;
                    this.EnableControls(true);
                }
            }
            if ((this.строящийся_сигнал_сигнальной_системы != null) && (nearest_post.Дорога != null))
            {
                this.строящийся_сигнал_сигнальной_системы.road = nearest_post.Дорога;
                this.строящийся_сигнал_сигнальной_системы.положение.расстояние = Math.Round(nearest_post.расстояние);
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.строящийся_сигнал_сигнальной_системы = null;
                    this.EnableControls(true);
                }
            }
            if ((this.строящийся_светофор != null) && (nearest_post.Дорога != null))
            {
                this.строящийся_светофор.положение.Дорога = nearest_post.Дорога;
                this.строящийся_светофор.положение.расстояние = Math.Round(nearest_post.расстояние);
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.строящийся_светофор = null;
                    this.EnableControls(true);
                }
            }
            if ((this.строящийся_светофорный_сигнал != null) && (nearest_post.Дорога != null))
            {
                this.строящийся_светофорный_сигнал.дорога = nearest_post.Дорога;
                this.строящийся_светофорный_сигнал.расстояние = nearest_post.расстояние;
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.строящийся_светофорный_сигнал = null;
                    this.EnableControls(true);
                }
                this.ОбновитьРаскрашенныеСплайны();
            }
            if ((click && (e.Button == MouseButtons.Left)) && (nearest_post.Дорога != null))
            {
                if (this.Park_Button.Pushed && (this.Park_Box.SelectedIndex >= 0))
                {
                    Парк парк = this.мир.парки[this.Park_Box.SelectedIndex];
                    if (this.Park_In_Button.Pushed)
                    {
                        парк.въезд = nearest_post.Дорога;
                        this.ОбновитьРаскрашенныеСплайны();
                        this.modified = true;
                    }
                    if (this.Park_Out_Button.Pushed)
                    {
                        парк.выезд = nearest_post.Дорога;
                        this.ОбновитьРаскрашенныеСплайны();
                        this.modified = true;
                    }
                    if (this.Park_Rails_Button.Pushed)
                    {
                        int index = -1;
                        for (int n = 0; n < парк.пути_стоянки.Length; n++)
                        {
                            if (парк.пути_стоянки[n] == nearest_post.Дорога)
                            {
                                index = n;
                                break;
                            }
                        }
                        List<Road> list2 = new List<Road>(парк.пути_стоянки);
                        if (index >= 0)
                        {
                            list2.RemoveAt(index);
                        }
                        else
                        {
                            list2.Add(nearest_post.Дорога);
                        }
                        парк.пути_стоянки = list2.ToArray();
                        this.ОбновитьРаскрашенныеСплайны();
                        this.modified = true;
                    }
                }
                if (this.Stops_Button.Pushed && (this.Stops_Box.SelectedIndex >= 0))
                {
                    Stop остановка = this.мир.остановки[this.Stops_Box.SelectedIndex];
                    int num15 = -1;
                    for (int num16 = 0; num16 < остановка.частьПути.Length; num16++)
                    {
                        if (остановка.частьПути[num16] == nearest_post.Дорога)
                        {
                            num15 = num16;
                            break;
                        }
                    }
                    List<Road> list3 = new List<Road>(остановка.частьПути);
                    if (num15 >= 0)
                    {
                        list3.RemoveAt(num15);
                    }
                    else
                    {
                        list3.Add(nearest_post.Дорога);
                    }
                    остановка.частьПути = list3.ToArray();
                    this.ОбновитьРаскрашенныеСплайны();
                    this.modified = true;
                }
                if ((this.Route_Button.Pushed && (this.Route_Box.SelectedIndex >= 0)) && (this.Route_Runs_Box.SelectedIndex >= 0))
                {
                    Trip рейс = this.выбранный_рейс;
                    List<Road> list4 = new List<Road>(рейс.pathes);
                    if (рейс.дорога_прибытия == nearest_post.Дорога)
                    {
                        list4.RemoveAt(рейс.pathes.Length - 1);
                    }
                    else if ((рейс.pathes.Length == 0) || new List<Road>(рейс.дорога_прибытия.следующиеДороги).Contains(nearest_post.Дорога))
                    {
                        list4.Add(nearest_post.Дорога);
                    }
                    рейс.pathes = list4.ToArray();
                    this.Route_Runs_ToParkIndex_UpDown.Maximum = this.выбранный_рейс.pathes.Length;
                    this.ОбновитьРаскрашенныеСплайны();
                    this.modified = true;
                }
            }
            if (строящийся_объект != null)
            {
                строящийся_объект.x0 = cursor_pos.x;
                строящийся_объект.y0 = cursor_pos.y;
                строящийся_объект.bounding_sphere.Update(строящийся_объект.Position3D, строящийся_объект.direction2);
                if (click && e.Button == MouseButtons.Left)
                {
                    if (temp_index >= 0)
                    {
                        мир.объекты[temp_index] = строящийся_объект;
                    }
                    else
                    {
                        мир.объекты.Add(строящийся_объект);
                        UpdateObjectsList();
                        Objects_Instance_Box.SelectedIndex = Objects_Instance_Box.Items.Count - 1;
                    }
                    строящийся_объект = null;
                    temp_index = -1;
                    EnableControls(true);
                    modified = true;
                }
            }
            else if ((this.Object_Button.Pushed && (this.мир.объекты.Count > 0)) && (click && e.Button == MouseButtons.Left))
            {//как бы поиск по клику. потом переделать нормально
            	double x;
            	double dist = 250.0;
            	int arpos = 0;
            	for (int i = 0; i < this.мир.объекты.Count; i++)
            	{
            		x = (this.мир.объекты[i].bounding_sphere.position.xz_point - this.cursor_pos).модуль;
            		if (x < dist)
            		{
            			dist = x;
            			arpos = i;
            		}
            	}
                this.Objects_Instance_Box.SelectedIndex = arpos;
            }
        }
        
        private void UpdateStatusBar()
        {
        	double num18;
        	this.Coord_x1_Status.Text = "";
            this.Coord_y1_Status.Text = "";
            this.Angle1_Status.Text = "";
            this.Coord_x2_Status.Text = "";
            this.Coord_y2_Status.Text = "";
            this.Radius_Status.Text = "";
            this.Angle_Status.Text = "";
            this.Angle2_Status.Text = "";
            this.Length_Status.Text = "";
            this.Wide0_Status.Text = "";
            this.Wide1_Status.Text = "";
            this.Height0_Status.Text = "";
            this.Height1_Status.Text = "";
            this.Maschtab.Text = "масштаб: " + (MyDirect3D.масштаб).ToString("0.0");
            this.Ugol.Text = "Угол: " + (180 * this.угол_поворота / Math.PI).ToString("0") + "\x00b0";
            if (this.строящаяся_дорога != null)
            {
                this.строящаяся_дорога.ОбновитьСтруктуру();
                num18 = this.строящаяся_дорога.концы[0].x / 1000.0;
                this.Coord_x1_Status.Text = "x: " + num18.ToString("0.000") + " км";
                num18 = this.строящаяся_дорога.концы[0].y / 1000.0;
                this.Coord_y1_Status.Text = "y: " + num18.ToString("0.000") + " км";
                num18 = (this.строящаяся_дорога.направления[0] * 180.0) / Math.PI;//3.1415926535897931;
                this.Angle1_Status.Text = num18.ToString("0") + "\x00b0";
                num18 = this.строящаяся_дорога.концы[1].x / 1000.0;
                this.Coord_x2_Status.Text = "x: " + num18.ToString("0.000") + " км";
                num18 = this.строящаяся_дорога.концы[1].y / 1000.0;
                this.Coord_y2_Status.Text = "y: " + num18.ToString("0.000") + " км";
                num18 = (this.строящаяся_дорога.направления[1] * 180.0) / Math.PI;//3.1415926535897931;
                this.Angle2_Status.Text = num18.ToString("0") + "\x00b0";
                this.Length_Status.Text = "l: " + this.строящаяся_дорога.Длина.ToString("0.0") + " м";
                this.Wide0_Status.Text = "w1: " + this.строящаяся_дорога.ширина[0].ToString("0.0") + " м";
                this.Wide1_Status.Text = "w2: " + this.строящаяся_дорога.ширина[1].ToString("0.0") + " м";
                this.Height0_Status.Text = "h1: " + this.строящаяся_дорога.высота[0].ToString("0.0") + " м";
                this.Height1_Status.Text = "h2: " + this.строящаяся_дорога.высота[1].ToString("0.0") + " м";
                if (this.строящаяся_дорога.кривая)
                {
                	this.Radius_Status.Text = "r: " + this.строящаяся_дорога.Радиус.ToString("0.0") + " м";
                	double num12 = this.строящаяся_дорога.структура.угол0 + this.строящаяся_дорога.структура.угол1;
                	num18 = (num12 * 180.0) / Math.PI;//3.1415926535897931;
                	this.Angle_Status.Text = num18.ToString("0") + "\x00b0";
                }
            }
            else if (this.строящиеся_провода != null)
            {
                num18 = this.строящиеся_провода.концы[0].x / 1000.0;
                this.Coord_x1_Status.Text = "x: " + num18.ToString("0.000") + " км";
                num18 = this.строящиеся_провода.концы[0].y / 1000.0;
                this.Coord_y1_Status.Text = "y: " + num18.ToString("0.000") + " км";
                num18 = (this.строящиеся_провода.направление * 180.0) / Math.PI;//3.1415926535897931;
                this.Angle1_Status.Text = num18.ToString("0") + "\x00b0";
                num18 = this.строящиеся_провода.концы[1].x / 1000.0;
                this.Coord_x2_Status.Text = "x: " + num18.ToString("0.000") + " км";
                num18 = this.строящиеся_провода.концы[1].y / 1000.0;
                this.Coord_y2_Status.Text = "y: " + num18.ToString("0.000") + " км";
                num18 = ((this.строящиеся_провода.направление + (2.0 * (this.строящиеся_провода.направления[1] - this.строящиеся_провода.направление))) * 180.0) / Math.PI;//3.1415926535897931;
                this.Angle2_Status.Text = num18.ToString("0") + "\x00b0";
                var point20 = this.строящиеся_провода.концы[1] - this.строящиеся_провода.концы[0];
                this.Length_Status.Text = "l: " + point20.модуль.ToString("0.0") + " м";
                this.Height0_Status.Text = "h1: " + this.строящиеся_провода.высота[0].ToString("0.0") + " м";
                this.Height1_Status.Text = "h2: " + this.строящиеся_провода.высота[1].ToString("0.0") + " м";
                this.Angle_Status.Text = ((((2.0 * (this.строящиеся_провода.направления[1] - this.строящиеся_провода.направление)) * 180.0) / Math.PI)).ToString("0") + "\x00b0";
            }
            else if (this.Stops_Button.Pushed)
            {
            	num18 = 11.0;
            	if (this.строящаяся_остановка != null)
	            {
	            	this.Length_Status.Text = "d: " + this.строящаяся_остановка.distance.ToString("0.0") + " м";
	            	num18 = this.строящаяся_остановка.distance;
	            }
            	else if (Stops_Box.SelectedIndex >= 0)
            	{
            		this.Length_Status.Text = "d: " + this.мир.остановки[Stops_Box.SelectedIndex].distance.ToString("0.0") + " м";
            		num18 = this.мир.остановки[Stops_Box.SelectedIndex].distance;
            	}
            	if (num18 < 10.0) this.Radius_Status.Text = "critical d!";
            }
            else if (this.строящийся_светофор != null)
            {
                this.Length_Status.Text = "d: " + this.строящийся_светофор.положение.расстояние.ToString("0.0") + " м";
                this.Wide0_Status.Text = "offset:";
                this.Wide1_Status.Text = this.строящийся_светофор.положение.отклонение.ToString("0.0") + " м";
                this.Height0_Status.Text = "h: " + this.строящийся_светофор.положение.высота.ToString("0.0") + " м";
            }
            else if (this.строящийся_светофорный_сигнал != null)
            {
            	this.Length_Status.Text = "d: " + this.строящийся_светофорный_сигнал.расстояние.ToString("0.0") + " м";
            }
            else if (this.строящийся_сигнал_сигнальной_системы != null)
            {
            	this.Length_Status.Text = "d: " + this.строящийся_сигнал_сигнальной_системы.положение.расстояние.ToString("0.0") + " м";
            	this.Wide0_Status.Text = "offset:";
                this.Wide1_Status.Text = this.строящийся_сигнал_сигнальной_системы.положение.отклонение.ToString("0.0") + " м";
                this.Height0_Status.Text = "h: " + this.строящийся_сигнал_сигнальной_системы.положение.высота.ToString("0.0") + " м";
            }
            else if (this.строящийся_элемент_сигнальной_системы != null)
            {
            	this.Length_Status.Text = "d: " + this.строящийся_элемент_сигнальной_системы.расстояние.ToString("0.0") + " м";
            }
            else if (this.строящийся_объект != null)
            {
            	this.Radius_Status.Text = "angel:";
            	this.Angle_Status.Text = (((this.строящийся_объект.angle0 * 180.0) / Math.PI)).ToString("0") + "\x00b0";
            	this.Height0_Status.Text = "h: " + this.строящийся_объект.height0.ToString("0.0") + " м";
            }
        }
        
        private void Refresh_All_TripStop_Lists_Item_Click(object sender, EventArgs e)
        {
            foreach (Route маршрут in this.мир.маршруты)
            {
                for (int j = 0; j < маршрут.AllTrips.Count; j++)
                {
                	маршрут.AllTrips[j].UpdateTripStopList(маршрут);
                }
            }
            this.modified = true;
        }

        /*private void Refresh_Timer_Tick2(object sender, EventArgs e)
        {
            if (!this.refreshing)
            {
                this.refreshing = true;
                if (this.Play_Button.Pushed)
                {
                    foreach (Transport транспорт in this.мир.транспорты)
                    {
                        if (транспорт.управление.автоматическое || транспорт.управление.ручное)
                        {
                            транспорт.АвтоматическиУправлять(this.мир);
                        }
                    }
                    this.мир.Обновить(this.без_игроков);
                }
                else
                {
                    this.мир.Обновить_время();
                }
                if (this.строящаяся_дорога != null)
                {
                    if ((this.Rail_Build_попутки_Button.Pushed) && (this.Rail_Build_встречки_Button.Pushed))
                    {
                        if ((this.Rail_Build_попутки1_Button.Pushed) && (this.Rail_Build_встречки1_Button.Pushed))
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }

                        else if ((this.Rail_Build_попутки2_Button.Pushed) && (this.Rail_Build_встречки1_Button.Pushed))
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if ((this.Rail_Build_попутки3_Button.Pushed) && (this.Rail_Build_встречки1_Button.Pushed))
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_дорога2);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if ((this.Rail_Build_попутки1_Button.Pushed) && (this.Rail_Build_встречки2_Button.Pushed))
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }

                        else if ((this.Rail_Build_попутки2_Button.Pushed) && (this.Rail_Build_встречки2_Button.Pushed))
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if ((this.Rail_Build_попутки3_Button.Pushed) && (this.Rail_Build_встречки2_Button.Pushed))
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_дорога2);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if ((this.Rail_Build_попутки1_Button.Pushed) && (this.Rail_Build_встречки3_Button.Pushed))
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога2);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if ((this.Rail_Build_попутки2_Button.Pushed) && (this.Rail_Build_встречки3_Button.Pushed))
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога2);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if ((this.Rail_Build_попутки3_Button.Pushed) && (this.Rail_Build_встречки3_Button.Pushed))
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_дорога2);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога2);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                    }
                    else if (this.Rail_Build_попутки_Button.Pushed)
                    {
                        if (this.Rail_Build_попутки1_Button.Pushed)
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if (this.Rail_Build_попутки2_Button.Pushed)
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_дорога1);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if (this.Rail_Build_попутки3_Button.Pushed)
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_дорога2);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                    }
                    else if (this.Rail_Build_встречки_Button.Pushed)
                    {
                        if (this.Rail_Build_встречки1_Button.Pushed)
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if (this.Rail_Build_встречки2_Button.Pushed)
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                        else if (this.Rail_Build_встречки3_Button.Pushed)
                        {
                            this.мир.listДороги.Add(this.строящаяся_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога1);
                            this.мир.listДороги.Add(this.строящаяся_обратная_дорога2);
                            this.игра.Render();
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                            this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                        }
                    }
                    else
                    {
                        this.мир.listДороги.Add(this.строящаяся_дорога);
                        this.игра.Render();
                        this.мир.listДороги.RemoveAt(this.мир.listДороги.Count - 1);
                    }
                }
                else if (this.строящиеся_провода != null)
                {
                	if (this.строящиеся_провода is Строящиеся_провода2)
                	{
                		int num = (this.строящиеся_провода.стадия == Стадия_стоительства.Нет) ? 1 : 2;
	                    Контактный_провод2[] _проводArray2 = this.мир.контактныеПровода2;
	                    this.мир.контактныеПровода2 = new Контактный_провод2[_проводArray2.Length + num];
	                    for (int i = 0; i < _проводArray2.Length; i++)
	                    {
	                        this.мир.контактныеПровода2[i] = _проводArray2[i];
	                    }
	                    for (int j = 0; j < num; j++)
	                    {
	                    	this.мир.контактныеПровода2[_проводArray2.Length + j] = (Контактный_провод2)this.строящиеся_провода.провода[j];
	                    }
	                    this.игра.Render();
	                    this.мир.контактныеПровода2 = _проводArray2;
                	}
                	else
                	{
	                    int num = (this.строящиеся_провода.стадия == Стадия_стоительства.Нет) ? 2 : 4;
	                    Контактный_провод[] _проводArray = this.мир.контактныеПровода;
	                    this.мир.контактныеПровода = new Контактный_провод[_проводArray.Length + num];
	                    for (int i = 0; i < _проводArray.Length; i++)
	                    {
	                        this.мир.контактныеПровода[i] = _проводArray[i];
	                    }
	                    for (int j = 0; j < num; j++)
	                    {
	                        this.мир.контактныеПровода[_проводArray.Length + j] = this.строящиеся_провода.провода[j];
	                    }
	                    this.игра.Render();
	                    this.мир.контактныеПровода = _проводArray;
                	}
                }
                else if (строящийся_объект != null)
                {
                    List<Объект> объекты = new List<Объект>();
                    foreach (Объект объект in мир.объекты)
                    {
                        объекты.Add(объект);
                    }
                    мир.объекты.Add(строящийся_объект);
                    игра.Render();
                    мир.объекты.Clear();
                    foreach (Объект объект in объекты)
                    {
                        мир.объекты.Add(объект);
                    }
                }
                else
                {
                    this.игра.Render();
                }
                this.refreshing = false;
            }
        }*/

        private void Refresh_Timer_Tick(object sender, EventArgs e)
        {
            if (!this.refreshing)
            {	//HACK: немнго из рендера
                this.refreshing = true;
                this.мир.Обновить_время();
                MyDirect3D.device.BeginScene();
            	MyDirect3D.ResetViewports(игроки.Length);
            	MyDirect3D.SetViewport(-1);
            	MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
                if (this.строящаяся_дорога != null)
                {
                    if (this.Rail_Build_попутки_Button.Pushed)
                    {
                    	var road1 = this.строящаяся_дорога1;
                    	var road2 = this.строящаяся_дорога2;
                        if (this.Rail_Build_попутки2_Button.Pushed)
                        {
                        	if (road1 != null) road1.Render();
                        }
                        else if (this.Rail_Build_попутки3_Button.Pushed)
                        {
                            if (road1 != null) road1.Render();
                            if (road2 != null) road2.Render();
                        }
                    }
                    if (this.Rail_Build_встречки_Button.Pushed)
                    {
                    	var road = this.строящаяся_обратная_дорога;
                    	var road1 = this.строящаяся_обратная_дорога1;
                    	var road2 = this.строящаяся_обратная_дорога2;
                        if (this.Rail_Build_встречки1_Button.Pushed)
                        {
                            if (road != null) road.Render();
                        }
                        else if (this.Rail_Build_встречки2_Button.Pushed)
                        {
                            if (road != null) road.Render();
                            if (road1 != null) road1.Render();
                        }
                        else if (this.Rail_Build_встречки3_Button.Pushed)
                        {
                            if (road != null) road.Render();
                            if (road1 != null) road1.Render();
                            if (road2 != null) road2.Render();
                        }
                    }
                    this.строящаяся_дорога.Render();
                }
                else if (this.строящиеся_провода != null)
                {
                	int num = (this.строящиеся_провода.стадия == Стадия_стоительства.Второй_конец) ? this.строящиеся_провода.провода.Length : this.строящиеся_провода.провода.Length / 2;
                	for (int i = 0; i < num; i++)
                	{
                		this.строящиеся_провода.провода[i].Render();
                	}
                }
                else if (this.строящийся_объект != null)
                {
                    this.строящийся_объект.Render();
                }
                this.игра.мир.RenderMeshesA();
                this.игра.Render();
                this.refreshing = false;
                if (time_color > 0.0) time_color -= Refresh_Timer.Interval / 500.0;
                if ((time_color <= 0.0) && (this.раскрашены_рельсы))
                {
                	time_color = 0.0;
                	this.ОбновитьРаскрашенныеСплайны();
                }
            }
        }
        
        private void RefreshPanelSize(object sender, EventArgs e)
        {
            MyDirect3D.Window_Width = this.Sizable_Panel.ClientSize.Width;
            MyDirect3D.Window_Height = this.Sizable_Panel.ClientSize.Height;
            MyDirect3D.viewport_x = null;
        }

        private void Route_Add_Button_Click(object sender, EventArgs e)
        {
            List<Route> list = new List<Route>(this.мир.маршруты);
            string str = (this.мир.маршруты.Length + 1).ToString();
            Route item = new Route(TypeOfTransport.Tramway, str);
            list.Add(item);
            this.мир.маршруты = list.ToArray();
            this.Route_Box.Items.Add(str);
            this.Route_Box.SelectedIndex = this.Route_Box.Items.Count - 1;
            this.modified = true;
        }

        private void RouteBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            var flag = Route_Box.SelectedIndex >= 0;
            Route_Remove_Button.Enabled = flag;
            Route_Name_label.Enabled = flag;
            Route_Name_Box.Enabled = flag;
            Route_Name_Box.Text = flag ? мир.маршруты[Route_Box.SelectedIndex].number : "";
            Route_Name_Box.Modified = false;
            Route_ChangeName_Button.Enabled = false;
            Route_TransportType_label.Enabled = flag;
            Route_TransportType_Box.Enabled = flag;
            Route_TransportType_Box.SelectedIndex = flag ? ((int)мир.маршруты[Route_Box.SelectedIndex].typeOfTransport) : -1;
            Route_Runs_label.Enabled = flag;
            Route_Runs_Box.Enabled = flag;
            Route_Runs_Add_Button.Enabled = flag;
            StopsButton.Enabled = flag;
            //RollingStockRouteUpdate();
            Narad_label.Enabled = flag;
            Narad_Box.Enabled = flag;
            Narad_Add_Button.Enabled = flag;
            UpdateRouteControls(flag ? мир.маршруты[Route_Box.SelectedIndex] : null);
            ОбновитьРаскрашенныеСплайны();
        }

        private void RouteChangeNameButtonClick(object sender, EventArgs e)
        {
            var selectedIndex = Route_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            мир.маршруты[selectedIndex].number = Route_Name_Box.Text;
            Route_Box.Items[selectedIndex] = Route_Name_Box.Text;
            Route_Name_Box.Modified = false;
            modified = true;
        }

        private void RouteNameBoxModifiedChanged(object sender, EventArgs e)
        {
            Route_ChangeName_Button.Enabled = Route_Name_Box.Modified;
        }

        private void RouteRemoveButtonClick(object sender, EventArgs e)
        {
            var selectedIndex = Route_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            var list = new List<Route>(мир.маршруты);
            list.RemoveAt(selectedIndex);
            мир.маршруты = list.ToArray();
            Route_Box.Items.RemoveAt(selectedIndex);
            RouteBoxSelectedIndexChanged(null, new EventArgs());
            modified = true;
        }

        private void RouteRunsAddButtonClick(object sender, EventArgs e)
        {
            Trip item = new Trip();
            this.выбранный_маршрут.trips.Add(item);
            int index = this.выбранный_маршрут.trips.Count - 1;
            int num2 = index + 1;
            this.Route_Runs_Box.Items.Insert(index, "Рейс " + num2.ToString());
            this.Route_Runs_Box.SelectedIndex = index;
            this.Narad_Runs_Box_SelectedIndexChanged(sender, e);
            this.modified = true;
        }

        private void Route_Runs_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = (this.Route_Runs_Box.SelectedIndex >= 0) && (this.Route_Box.SelectedIndex >= 0);
            this.Route_Runs_Remove_Button.Enabled = flag;
            this.Route_Runs_Park_Box.Enabled = flag;
            this.Route_Runs_Park_Box.Checked = this.Route_Runs_Box.SelectedIndex >= this.выбранный_маршрут.trips.Count;
            this.Route_Runs_ToPark_Box.Enabled = flag && this.Route_Runs_Park_Box.Checked;
            this.Route_Runs_ToPark_Box.Checked = this.выбранный_рейс.inPark;
            this.Route_Runs_ToParkIndex_label.Enabled = flag && this.Route_Runs_ToPark_Box.Checked;
            this.Route_Runs_ToParkIndex_UpDown.Enabled = flag && this.Route_Runs_ToPark_Box.Checked;
            this.Route_Runs_ToParkIndex_UpDown.Maximum = Math.Max(this.Route_Runs_ToParkIndex_UpDown.Value, this.выбранный_рейс.pathes.Length);
            this.Route_Runs_ToParkIndex_UpDown.Value = this.выбранный_рейс.inParkIndex;
            this.Route_Runs_ToParkIndex_UpDown.Maximum = this.выбранный_рейс.pathes.Length;
            this.Route_Runs_Time_label.Enabled = flag;
            this.Route_Runs_Time_Box.Enabled = flag;
            this.Route_Runs_Time_Box.Time_Seconds = (int)this.выбранный_рейс.время_прибытия;
            this.Route_Runs_ComputeTime_Button.Enabled = flag;
            this.ОбновитьРаскрашенныеСплайны();
        }

        private void Route_Runs_ComputeTime_Button_Click(object sender, EventArgs e)
        {
            this.мир.time = 0.0;
            if (выбранный_рейс.tripStopList == null)
            {
                выбранный_рейс.InitTripStopList(выбранный_маршрут);
            }
            ComputeTimeDialog dialog = new ComputeTimeDialog(this.мир, this.выбранный_маршрут.typeOfTransport, this.выбранный_рейс, this.игрок);
            this.Refresh_Timer.Enabled = false;
            if (dialog.ShowDialog(this) != DialogResult.Cancel)
            {
                this.выбранный_рейс.время_прибытия = this.мир.time;
                this.Route_Runs_Box_SelectedIndexChanged(null, new EventArgs());
                this.modified = true;
            }
            this.мир.time = 0.0;
            this.Refresh_Timer.Enabled = true;
        }

        private void Route_Runs_Park_Box_CheckedChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Route_Runs_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                Trip item = this.выбранный_рейс;
                if (this.Route_Runs_Park_Box.Checked)
                {
                    if (selectedIndex < this.выбранный_маршрут.trips.Count)
                    {
                        this.выбранный_маршрут.trips.Remove(item);
                        this.выбранный_маршрут.parkTrips.Add(item);
                        this.Route_Runs_Box.Items.RemoveAt(selectedIndex);
                        this.Route_Runs_Box.Items.Add("Парковый рейс " + this.выбранный_маршрут.parkTrips.Count.ToString());
                        this.Route_Runs_Box.SelectedIndex = this.Route_Runs_Box.Items.Count - 1;
                        this.modified = true;
                    }
                }
                else if (selectedIndex >= this.выбранный_маршрут.trips.Count)
                {
                    this.выбранный_маршрут.parkTrips.Remove(item);
                    this.выбранный_маршрут.trips.Add(item);
                    item.inPark = false;
                    int index = this.выбранный_маршрут.trips.Count - 1;
                    this.Route_Runs_Box.Items.RemoveAt(selectedIndex);
                    int num4 = index + 1;
                    this.Route_Runs_Box.Items.Insert(index, "Рейс " + num4.ToString());
                    this.Route_Runs_Box.SelectedIndex = index;
                    this.modified = true;
                }
                this.Narad_Runs_Box_SelectedIndexChanged(sender, e);
            }
        }

        private void Route_Runs_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Route_Runs_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (selectedIndex < this.выбранный_маршрут.trips.Count)
                {
                    this.выбранный_маршрут.trips.RemoveAt(selectedIndex);
                }
                else
                {
                    this.выбранный_маршрут.parkTrips.RemoveAt(selectedIndex - this.выбранный_маршрут.trips.Count);
                }
                this.Route_Runs_Box.Items.RemoveAt(selectedIndex);
                this.Route_Runs_Box_SelectedIndexChanged(null, new EventArgs());
                this.Narad_Runs_Box_SelectedIndexChanged(sender, e);
                this.modified = true;
            }
        }

        private void RouteRunsTimeBoxTimeChanged(object sender, EventArgs e)
        {
            this.выбранный_рейс.время_прибытия = this.Route_Runs_Time_Box.Time_Seconds;
            this.Narad_Runs_Box_SelectedIndexChanged(sender, e);
            this.modified = true;
        }

        private void RouteRunsToParkBoxCheckedChanged(object sender, EventArgs e)
        {
            this.выбранный_рейс.inPark = this.Route_Runs_ToPark_Box.Checked;
            this.Route_Runs_ToParkIndex_label.Enabled = this.Route_Runs_ToPark_Box.Enabled && this.Route_Runs_ToPark_Box.Checked;
            this.Route_Runs_ToParkIndex_UpDown.Enabled = this.Route_Runs_ToPark_Box.Enabled && this.Route_Runs_ToPark_Box.Checked;
            this.ОбновитьРаскрашенныеСплайны();
            this.modified = true;
        }

        private void RouteRunsToParkIndexUpDownValueChanged(object sender, EventArgs e)
        {
            this.выбранный_рейс.inParkIndex = (int)this.Route_Runs_ToParkIndex_UpDown.Value;
            this.ОбновитьРаскрашенныеСплайны();
            this.modified = true;
        }

        private void RouteShowNaradsBoxCheckedChanged(object sender, EventArgs e)
        {
            narad_panel.Visible = Route_ShowNarads_Box.Checked;
        }

        private void RouteTransportTypeBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Route_TransportType_Box.SelectedIndex < 0) return;
            выбранный_маршрут.typeOfTransport = Route_TransportType_Box.SelectedIndex;
            RollingStockUpdate(выбранный_наряд);
            modified = true;
        }

        private void RunItemClick(object sender, EventArgs e)
        {
            if (this.modified || (this.filename == null))
            {
                MessageBoxButtons yesNoCancel = MessageBoxButtons.YesNoCancel;
                if (this.filename == null)
                {
                    yesNoCancel = MessageBoxButtons.OKCancel;
                }
                DialogResult result = MessageBox.Show(Localization.current_.save_run/*"Сохранить город перед запуском?"*/, "Transedit", yesNoCancel, MessageBoxIcon.Exclamation);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                if (result != DialogResult.No)
                {
                    this.Save_Item_Click(this, new EventArgs());
                }
            }
            base.WindowState = FormWindowState.Minimized;
            Process.Start(Application.ExecutablePath, '"' + this.filename + '"');
        }

        private void Save_Item_Click(object sender, EventArgs e)
        {
            if (this.filename == null)
            {
                if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                this.filename = this.saveFileDialog.FileName;
            }
            MyGUI.load_status = 0;
            MyGUI.status_string = Localization.current_.save_city;//"Сохранене города...";
            MyGUI.Splash();
            double num = this.угол_поворота;
            this.Повернуть_всё(-num);
            try
            {
                this.мир.Сохранить_город(this.filename);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, Localization.current_.save_failed/*"Ошибка во время сохранения"*/ + ":\n" + exception.Message, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            MyGUI.load_status = MyGUI.load_max;
            MyGUI.Splash();
            this.Повернуть_всё(num);
            this.modified = false;
        }

        private void SaveAs_Item_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.filename = this.saveFileDialog.FileName;
                this.Save_Item_Click(sender, e);
            }
        }

        private bool Set_New_File(FileDialog dialog)
        {
            if (this.modified)
            {
                switch (MessageBox.Show(Localization.current_.save_only/*"Сохранить этот город?"*/, "Transedit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                {
                    case DialogResult.Cancel:
                        return false;

                    case DialogResult.Yes:
                        this.Save_Item_Click(this, new EventArgs());
                        break;
                }
            }
            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return false;
            }
            this.filename = dialog.FileName;
            this.modified = false;
            return true;
        }

        private void SetPanelControls(Panel new_panel)
        {
            if (this.old_panel != new_panel)
            {
                if (this.old_panel != null)
                {
                    while (this.edit_panel.Controls.Count > 0)
                    {
                        this.old_panel.Controls.Add(this.edit_panel.Controls[0]);
                    }
                }
                this.edit_panel.Controls.Clear();
                if (new_panel != null)
                {
                    while (new_panel.Controls.Count > 0)
                    {
                        this.edit_panel.Controls.Add(new_panel.Controls[0]);
                    }
                }
                this.old_panel = new_panel;
            }
        }

        private void Signals_Add_Button_Click(object sender, EventArgs e)
        {
            List<Сигнальная_система> list = new List<Сигнальная_система>(this.мир.сигнальныеСистемы);
            Сигнальная_система item = new Сигнальная_система(1, 0);
            item.CreateMesh();
            list.Add(item);
            this.мир.сигнальныеСистемы = list.ToArray();
            this.Signals_Box.Items.Add("Система " + list.Count.ToString());
            this.Signals_Box.SelectedIndex = this.Signals_Box.Items.Count - 1;
            this.modified = true;
        }

        private void Signals_Bound_UpDown_ValueChanged(object sender, EventArgs e)
        {
            this.выбранная_сигнальная_система.граница_переключения = (int)this.Signals_Bound_UpDown.Value;
            this.modified = true;
        }

        private void Signals_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.Signals_Box.SelectedIndex >= 0;
            this.Signals_Remove_Button.Enabled = flag;
            this.Signals_Bound_label.Enabled = flag;
            this.Signals_Bound_UpDown.Enabled = flag;
            this.Signals_Bound_UpDown.Value = flag ? this.выбранная_сигнальная_система.граница_переключения : 0;
            this.Signals_Element_label.Enabled = flag;
            this.Signals_Element_Box.Enabled = flag;
            this.Signals_Element_AddContact_Button.Enabled = flag;
            this.Signals_Element_AddSignal_Button.Enabled = flag && (this.Signals_Model_Box.Items.Count > 0);
            this.UpdateSignalsControls(flag ? this.мир.сигнальныеСистемы[this.Signals_Box.SelectedIndex] : null);
        }

        private void Signals_Element_AddContact_Button_Click(object sender, EventArgs e)
        {
            if (this.мир.ВсеДороги.Length > 0)
            {
                new Сигнальная_система.Контакт(this.выбранная_сигнальная_система, this.мир.ВсеДороги[0], 0.0, false);
//                this.Signals_Element_Box.Items.Add("Контакт " + this.выбранная_сигнальная_система.элементы.Count.ToString());
                this.UpdateSignalsControls(this.выбранная_сигнальная_система);
				this.Signals_Element_Box.SelectedIndex = this.Signals_Element_Box.Items.Count - 1;
                this.Signals_Element_EditLocation_Button_Click(sender, e);
            }
        }

        private void Signals_Element_AddSignal_Button_Click(object sender, EventArgs e)
        {
            if (this.мир.ВсеДороги.Length > 0)
            {
//            	new Сигнальная_система.Сигнал(this.выбранная_сигнальная_система, this.мир.ВсеДороги[0], 0.0, this.Signals_Model_Box.Items[this.Signals_Model_Box.SelectedIndex].ToString());
            	var signal = new Visual_Signal(this.выбранная_сигнальная_система, this.Signals_Model_Box.Items[this.Signals_Model_Box.SelectedIndex].ToString());
            	signal.положение = new Положение();
//                this.Signals_Element_Box.Items.Add("Сигнал " + this.выбранная_сигнальная_система.элементы.Count.ToString());
				this.UpdateSignalsControls(this.выбранная_сигнальная_система);
                this.Signals_Element_Box.SelectedIndex = this.выбранная_сигнальная_система.vsignals.Count - 1;//this.Signals_Element_Box.Items.Count - 1;
                this.выбранная_сигнальная_система.CreateMesh();
                this.Signals_Element_EditLocation_Button_Click(sender, e);
            }
        }

        private void Signals_Element_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = (this.Signals_Element_Box.SelectedIndex >= 0) && (this.Signals_Box.SelectedIndex >= 0);
            this.Signals_Element_Remove_Button.Enabled = flag;
            this.Signals_Element_Minus_Box.Enabled = flag;
//            Сигнальная_система.Элемент элемент = flag ? this.выбранная_сигнальная_система.элементы[this.Signals_Element_Box.SelectedIndex] : null;
            if (flag && (this.Signals_Element_Box.SelectedIndex >= this.выбранная_сигнальная_система.vsignals.Count))//(элемент is Сигнальная_система.Контакт))
            {
                this.Signals_Element_Minus_Box.Visible = true;
                this.Signals_Element_Minus_Box.Checked = /*((Сигнальная_система.Контакт)элемент)*/this.выбранная_сигнальная_система.элементы[this.Signals_Element_Box.SelectedIndex - this.выбранная_сигнальная_система.vsignals.Count].минус;
            }
            else
            {
                this.Signals_Element_Minus_Box.Visible = false;
            }
            this.Signals_Element_Location_label.Enabled = flag;
            this.Signals_Element_ShowLocation_Button.Enabled = flag;
            this.Signals_Element_EditLocation_Button.Enabled = flag;
        }

        private void Signals_Element_EditLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Signals_Element_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            if (selectedIndex < this.выбранная_сигнальная_система.vsignals.Count)
            {
            	this.строящийся_сигнал_сигнальной_системы = this.выбранная_сигнальная_система.vsignals[selectedIndex];
            }
            else
            {
                this.строящийся_элемент_сигнальной_системы = this.выбранная_сигнальная_система.элементы[selectedIndex - this.выбранная_сигнальная_система.vsignals.Count];
            }
            this.EnableControls(false);
            this.modified = true;
        }

        private void Signals_Element_Minus_Box_CheckedChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Signals_Element_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            Сигнальная_система.Контакт элемент = this.выбранная_сигнальная_система.элементы[selectedIndex - this.выбранная_сигнальная_система.vsignals.Count];
            элемент.минус = this.Signals_Element_Minus_Box.Checked;
            this.modified = true;
        }

        private void Signals_Element_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Signals_Element_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            if (selectedIndex < this.выбранная_сигнальная_система.vsignals.Count)
            {
            	this.выбранная_сигнальная_система.Убрать_сигнал(this.выбранная_сигнальная_система.vsignals[selectedIndex]);
            }
            else
            {
            	this.выбранная_сигнальная_система.Убрать_элемент(this.выбранная_сигнальная_система.элементы[selectedIndex - this.выбранная_сигнальная_система.vsignals.Count]);
            }
            UpdateSignalsControls(this.выбранная_сигнальная_система);
            this.Signals_Element_Box.SelectedIndex = selectedIndex - 1;
            this.modified = true;
        }

        private void Signals_Element_ShowLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Signals_Element_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            if (selectedIndex < this.выбранная_сигнальная_система.vsignals.Count)
            {
            	this.игрок.cameraPosition.xz_point = this.выбранная_сигнальная_система.vsignals[selectedIndex].road.НайтиКоординаты(this.выбранная_сигнальная_система.vsignals[selectedIndex].положение.расстояние, 0.0);
            }
            else
            {
                this.игрок.cameraPosition.xz_point = this.выбранная_сигнальная_система.элементы[selectedIndex - this.выбранная_сигнальная_система.vsignals.Count].дорога.НайтиКоординаты(this.выбранная_сигнальная_система.элементы[selectedIndex - this.выбранная_сигнальная_система.vsignals.Count].расстояние, 0.0);
            }
        }

        private void Signals_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Signals_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                List<Сигнальная_система> list = new List<Сигнальная_система>(this.мир.сигнальныеСистемы);
                list.RemoveAt(selectedIndex);
                this.мир.сигнальныеСистемы = list.ToArray();
                this.Signals_Box.Items.RemoveAt(selectedIndex);
                this.Signals_Box_SelectedIndexChanged(null, new EventArgs());
                this.modified = true;
            }
        }

        private void Stops_Add_Button_Click(object sender, EventArgs e)
        {
            if (this.мир.ВсеДороги.Length > 0)
            {
            	Stop item = new Stop(this.Stops_Model_Box.Items[this.Stops_Model_Box.SelectedIndex].ToString(), new TypeOfTransport(TypeOfTransport.Tramway), "Остановка", this.мир.ВсеДороги[0], 0.0);
                item.CreateMesh();
                this.мир.остановки.Add(item);
                this.Stops_Box.Items.Add("Остановка");
                this.Stops_Box.SelectedIndex = this.Stops_Box.Items.Count - 1;
                this.Stops_EditLocation_Button_Click(null, new EventArgs());
            }
        }

        private void Stops_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
        	UpdateStatusBar();
            bool flag = this.Stops_Box.SelectedIndex >= 0;
            this.Stops_Remove_Button.Enabled = flag;
            this.Stops_Name_label.Enabled = flag;
            this.Stops_Name_Box.Enabled = flag;
            this.Stops_Name_Box.Text = flag ? this.мир.остановки[this.Stops_Box.SelectedIndex].название : "";
            this.Stops_Name_Box.Modified = false;
            this.Stops_ChangeName_Button.Enabled = false;
            this.TypeOfTransportBox.Enabled = flag;
            //this.Stops_TransportType_label.Enabled = flag;
            //this.Stops_TransportType_Box.Enabled = flag;
            //this.Stops_TransportType_Box.SelectedIndex = flag ? ((int)this.мир.остановки[this.Stops_Box.SelectedIndex].typeOfTransport) : -1;
            this.Stops_Location_label.Enabled = flag;
            this.Stops_ShowLocation_Button.Enabled = flag;
            this.Stops_EditLocation_Button.Enabled = flag;
            this.ОбновитьРаскрашенныеСплайны();
            var selectedIndex = Stops_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            TramwayBox.Checked = мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Tramway];
            TrolleybusBox.Checked = мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Trolleybus];
            BusBox.Checked = мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Bus];
        }

        private void Stops_ChangeName_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Stops_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                this.мир.остановки[selectedIndex].название = this.Stops_Name_Box.Text;
                this.Stops_Box.Items[selectedIndex] = this.Stops_Name_Box.Text;
                this.Stops_Name_Box.Modified = false;
                this.modified = true;
            }
        }

        private void Stops_EditLocation_Button_Click(object sender, EventArgs e)
        {
            var selectedIndex = Stops_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            строящаяся_остановка = мир.остановки[selectedIndex];
            EnableControls(false);
            modified = true;
        }

        private void Stops_Name_Box_ModifiedChanged(object sender, EventArgs e)
        {
            this.Stops_ChangeName_Button.Enabled = this.Stops_Name_Box.Modified;
        }

        private void Stops_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Stops_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
//                List<Stop> list = new List<Stop>(this.мир.остановки);
                /*list*/this.мир.остановки.RemoveAt(selectedIndex);
//                this.мир.остановки = list;
                this.Stops_Box.Items.RemoveAt(selectedIndex);
                this.Stops_Box_SelectedIndexChanged(null, new EventArgs());
                this.modified = true;
            }
        }

        private void Stops_ShowLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Stops_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                this.игрок.cameraPosition.xz_point = this.мир.остановки[selectedIndex].road.НайтиКоординаты(this.мир.остановки[selectedIndex].distance, 0.0);
            }
        }

        private void Stops_TransportType_Box_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Svetofor_Add_Button_Click(object sender, EventArgs e)
        {
            List<Светофорная_система> list = new List<Светофорная_система>(this.мир.светофорныеСистемы);
            Светофорная_система item = new Светофорная_система();
            list.Add(item);
            this.мир.светофорныеСистемы = list.ToArray();
            this.Svetofor_Box.Items.Add("Система " + list.Count.ToString());
            this.Svetofor_Box.SelectedIndex = this.Svetofor_Box.Items.Count - 1;
            this.modified = true;
        }

        private void Svetofor_Begin_Box_TimeChanged(object sender, EventArgs e)
        {
            this.выбранная_светофорная_система.начало_работы = this.Svetofor_Begin_Box.Time_Seconds;
            this.modified = true;
        }

        private void Svetofor_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.Svetofor_Box.SelectedIndex >= 0;
            this.Svetofor_Remove_Button.Enabled = flag;
            this.Svetofor_Work_label.Enabled = flag;
            this.Svetofor_Begin_Box.Enabled = flag;
            this.Svetofor_Begin_Box.Time_Seconds = (int)this.выбранная_светофорная_система.начало_работы;
            this.Svetofor_End_Box.Enabled = flag;
            this.Svetofor_End_Box.Time_Seconds = (int)this.выбранная_светофорная_система.окончание_работы;
            this.Svetofor_Cycle_label.Enabled = flag;
            this.Svetofor_Cycle_Box.Enabled = flag;
            this.Svetofor_Cycle_Box.Time_Seconds = (int)this.выбранная_светофорная_система.цикл;
            this.Svetofor_Green_label.Enabled = flag;
            this.Svetofor_ToGreen_Box.Enabled = flag;
            this.Svetofor_ToGreen_Box.Time_Seconds = (int)this.выбранная_светофорная_система.время_переключения_на_зелёный;
            this.Svetofor_OfGreen_Box.Enabled = flag;
            this.Svetofor_OfGreen_Box.Time_Seconds = (int)this.выбранная_светофорная_система.время_зелёного;
            this.Svetofor_Element_label.Enabled = flag;
            this.Svetofor_Element_Box.Enabled = flag;
            this.Svetofor_Svetofor_Add_Button.Enabled = flag && (this.Svetofor_Model_Box.Items.Count > 0);
            this.Svetofor_Signal_Add_Button.Enabled = flag;
            this.UpdateSvetoforControls(flag ? this.мир.светофорныеСистемы[this.Svetofor_Box.SelectedIndex] : null);
        }

        private void Svetofor_Cycle_Box_TimeChanged(object sender, EventArgs e)
        {
            this.выбранная_светофорная_система.цикл = this.Svetofor_Cycle_Box.Time_Seconds;
            this.modified = true;
        }

        private void Svetofor_Element_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = (this.Svetofor_Element_Box.SelectedIndex >= 0) && (this.Svetofor_Box.SelectedIndex >= 0);
            bool flag2 = flag && (this.Svetofor_Element_Box.SelectedIndex < this.выбранная_светофорная_система.светофоры.Count);
            this.Svetofor_Element_Remove_Button.Enabled = flag;
//            this.Svetofor_Svetofor_Arrow_Box.Enabled = flag2;
//            this.Svetofor_Svetofor_Arrow_Box.Checked = flag2 && this.выбранная_светофорная_система.светофоры[this.Svetofor_Element_Box.SelectedIndex].стрелка;
			this.Svetofor_Svetofor_ArrowGreen_label.Enabled = flag2;
            this.Svetofor_Svetofor_ArrowGreen_Box.Enabled = flag2;
            this.Svetofor_Svetofor_ArrowYellow_label.Enabled = flag2;
            this.Svetofor_Svetofor_ArrowYellow_Box.Enabled = flag2;
            this.Svetofor_Svetofor_ArrowRed_label.Enabled = flag2;
            this.Svetofor_Svetofor_ArrowRed_Box.Enabled = flag2;
            this.Svetofor_Svetofor_ArrowRed_Box.Items.Clear();
			this.Svetofor_Svetofor_ArrowYellow_Box.Items.Clear();
			this.Svetofor_Svetofor_ArrowGreen_Box.Items.Clear();
			if (flag2)
			{
				this.Svetofor_Svetofor_ArrowRed_Box.Items.Add(Localization.current_.empty);
				this.Svetofor_Svetofor_ArrowYellow_Box.Items.Add(Localization.current_.empty);
				this.Svetofor_Svetofor_ArrowGreen_Box.Items.Add(Localization.current_.empty);
				for (int i = 0; i < this.выбранная_светофорная_система.светофоры[this.Svetofor_Element_Box.SelectedIndex].tex_count; i++)
				{
					this.Svetofor_Svetofor_ArrowRed_Box.Items.Add(this.выбранная_светофорная_система.светофоры[this.Svetofor_Element_Box.SelectedIndex].model.FindStringArg("tex" + i, string.Empty));
					this.Svetofor_Svetofor_ArrowYellow_Box.Items.Add(this.выбранная_светофорная_система.светофоры[this.Svetofor_Element_Box.SelectedIndex].model.FindStringArg("tex" + i, string.Empty));
					this.Svetofor_Svetofor_ArrowGreen_Box.Items.Add(this.выбранная_светофорная_система.светофоры[this.Svetofor_Element_Box.SelectedIndex].model.FindStringArg("tex" + i, string.Empty));
				}
			}
            this.Svetofor_Svetofor_ArrowGreen_Box.SelectedIndex = flag2 ? ((int)this.выбранная_светофорная_система.светофоры[this.Svetofor_Element_Box.SelectedIndex].зелёная_стрелка) : -1;
            this.Svetofor_Svetofor_ArrowYellow_Box.SelectedIndex = flag2 ? ((int)this.выбранная_светофорная_система.светофоры[this.Svetofor_Element_Box.SelectedIndex].жёлтая_стрелка) : -1;
            this.Svetofor_Svetofor_ArrowRed_Box.SelectedIndex = flag2 ? ((int)this.выбранная_светофорная_система.светофоры[this.Svetofor_Element_Box.SelectedIndex].красная_стрелка) : -1;
            this.Svetofor_Element_Location_label.Enabled = flag;
            this.Svetofor_Element_ShowLocation_Button.Enabled = flag;
            this.Svetofor_Element_EditLocation_Button.Enabled = flag;
            this.ОбновитьРаскрашенныеСплайны();
        }

        private void Svetofor_Element_EditLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (selectedIndex < this.выбранная_светофорная_система.светофоры.Count)
                {
                    this.строящийся_светофор = this.выбранная_светофорная_система.светофоры[selectedIndex];
                }
                else
                {
                    selectedIndex -= this.выбранная_светофорная_система.светофоры.Count;
                    this.строящийся_светофорный_сигнал = this.выбранная_светофорная_система.светофорные_сигналы[selectedIndex];
                }
                this.EnableControls(false);
                this.modified = true;
            }
        }

        private void Svetofor_Element_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (selectedIndex < this.выбранная_светофорная_система.светофоры.Count)
                {
                    this.выбранная_светофорная_система.светофоры.RemoveAt(selectedIndex);
                }
                else
                {
                    this.выбранная_светофорная_система.светофорные_сигналы.RemoveAt(selectedIndex - this.выбранная_светофорная_система.светофоры.Count);
                }
                this.UpdateSvetoforControls(this.выбранная_светофорная_система);
                this.Svetofor_Element_Box.SelectedIndex = -1;
                this.modified = true;
            }
        }

        private void Svetofor_Element_ShowLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (selectedIndex < this.выбранная_светофорная_система.светофоры.Count)
                {
                    this.игрок.cameraPosition.xz_point = this.выбранная_светофорная_система.светофоры[selectedIndex].положение.Координаты.xz_point;
                }
                else
                {
                    selectedIndex -= this.выбранная_светофорная_система.светофоры.Count;
                    this.игрок.cameraPosition.xz_point = this.выбранная_светофорная_система.светофорные_сигналы[selectedIndex].дорога.НайтиКоординаты(this.выбранная_светофорная_система.светофорные_сигналы[selectedIndex].расстояние, 0.0);
                }
            }
        }

        private void Svetofor_End_Box_TimeChanged(object sender, EventArgs e)
        {
            this.выбранная_светофорная_система.окончание_работы = this.Svetofor_End_Box.Time_Seconds;
            this.modified = true;
        }

        private void Svetofor_OfGreen_Box_TimeChanged(object sender, EventArgs e)
        {
            this.выбранная_светофорная_система.время_зелёного = this.Svetofor_OfGreen_Box.Time_Seconds;
            this.modified = true;
        }

        private void Svetofor_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            List<Светофорная_система> list = new List<Светофорная_система>(this.мир.светофорныеСистемы);
            list.RemoveAt(selectedIndex);
            this.мир.светофорныеСистемы = list.ToArray();
            this.Svetofor_Box.Items.RemoveAt(selectedIndex);
            this.Svetofor_Box_SelectedIndexChanged(null, new EventArgs());
            this.modified = true;
        }

        private void Svetofor_Signal_Add_Button_Click(object sender, EventArgs e)
        {
            if (this.мир.ВсеДороги.Length > 0)
            {
                this.выбранная_светофорная_система.светофорные_сигналы.Add(new Светофорный_сигнал(this.мир.ВсеДороги[0], 0.0));
                this.UpdateSvetoforControls(this.выбранная_светофорная_система);
                this.Svetofor_Element_Box.SelectedIndex = this.Svetofor_Element_Box.Items.Count - 1;
                this.Svetofor_Element_EditLocation_Button_Click(sender, e);
            }
        }

        private void Svetofor_Svetofor_Add_Button_Click(object sender, EventArgs e)
        {
            if (this.мир.ВсеДороги.Length > 0)
            {
            	Светофор item = new Светофор(this.Svetofor_Model_Box.Items[this.Svetofor_Model_Box.SelectedIndex].ToString());
                item.CreateMesh();
                item.положение = new Положение();
                this.выбранная_светофорная_система.светофоры.Add(item);
                this.UpdateSvetoforControls(this.выбранная_светофорная_система);
                this.Svetofor_Element_Box.SelectedIndex = this.выбранная_светофорная_система.светофоры.Count - 1;
                this.Svetofor_Element_EditLocation_Button_Click(sender, e);
            }
        }

        private void Svetofor_Svetofor_Arrow_Box_CheckedChanged(object sender, EventArgs e)
        {
            /*int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if ((selectedIndex >= 0) && (selectedIndex < this.выбранная_светофорная_система.светофоры.Count))
            {
                if (this.выбранная_светофорная_система.светофоры[selectedIndex].стрелка != this.Svetofor_Svetofor_Arrow_Box.Checked)
                {
                    this.выбранная_светофорная_система.светофоры[selectedIndex].стрелка = this.Svetofor_Svetofor_Arrow_Box.Checked;
                    this.выбранная_светофорная_система.светофоры[selectedIndex].CreateMesh();
                }
                this.modified = true;
            }*/
        }

        private void Svetofor_Svetofor_ArrowGreen_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (((selectedIndex >= 0) && (selectedIndex < this.выбранная_светофорная_система.светофоры.Count)) && (this.Svetofor_Svetofor_ArrowGreen_Box.SelectedIndex >= 0))
            {
                this.выбранная_светофорная_система.светофоры[selectedIndex].зелёная_стрелка = /*(Светофор.Стрелки)*/this.Svetofor_Svetofor_ArrowGreen_Box.SelectedIndex;
                this.modified = true;
            }
        }

        private void Svetofor_Svetofor_ArrowRed_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (((selectedIndex >= 0) && (selectedIndex < this.выбранная_светофорная_система.светофоры.Count)) && (this.Svetofor_Svetofor_ArrowRed_Box.SelectedIndex >= 0))
            {
                this.выбранная_светофорная_система.светофоры[selectedIndex].красная_стрелка = /*(Светофор.Стрелки)*/this.Svetofor_Svetofor_ArrowRed_Box.SelectedIndex;
                this.modified = true;
            }
        }

        private void Svetofor_Svetofor_ArrowYellow_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (((selectedIndex >= 0) && (selectedIndex < this.выбранная_светофорная_система.светофоры.Count)) && (this.Svetofor_Svetofor_ArrowYellow_Box.SelectedIndex >= 0))
            {
                this.выбранная_светофорная_система.светофоры[selectedIndex].жёлтая_стрелка = /*(Светофор.Стрелки)*/this.Svetofor_Svetofor_ArrowYellow_Box.SelectedIndex;
                this.modified = true;
            }
        }

        private void Svetofor_ToGreen_Box_TimeChanged(object sender, EventArgs e)
        {
            this.выбранная_светофорная_система.время_переключения_на_зелёный = this.Svetofor_ToGreen_Box.Time_Seconds;
            this.modified = true;
        }

        private void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            if (e.Button == New_Button)
            {
                New_Item_Click(sender, e);
            }
            if (e.Button == this.Open_Button)
            {
                this.Open_Item_Click(sender, e);
            }
            if (e.Button == this.Save_Button)
            {
                this.Save_Item_Click(sender, e);
            }
            if (e.Button == this.Run_Button)
            {
                this.RunItemClick(sender, e);
            }
            if (e.Button == this.Edit_Button)
            {
                this.edit_panel.Visible = false;
                if (this.Edit_Button.Pushed)
                {
                    this.Rail_Button.Pushed = false;
                    this.Troll_lines_Button.Pushed = false;
                    this.Stops_Button.Pushed = false;
                    this.Park_Button.Pushed = false;
                    this.Route_Button.Pushed = false;
                    this.Signals_Button.Pushed = false;
                    this.Svetofor_Button.Pushed = false;
                    this.Object_Button.Pushed = false;
                }
                else
                {
                    this.Edit_Button.Pushed = true;
                }
            }
            if (e.Button == this.Rail_Button)
            {
//                this.edit_panel.Visible = false;
                if (this.Rail_Button.Pushed)
                {
                	this.SetPanelControls(this.splines_panel);
                    this.edit_panel.Visible = true;
                    this.Edit_Button.Pushed = false;
                    this.Troll_lines_Button.Pushed = false;
                    this.Stops_Button.Pushed = false;
                    this.Park_Button.Pushed = false;
                    this.Route_Button.Pushed = false;
                    this.Signals_Button.Pushed = false;
                    this.Svetofor_Button.Pushed = false;
                    this.Object_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Button.Pushed = true;
                }
            }
            if (e.Button == this.Troll_lines_Button)
            {
                this.edit_panel.Visible = false;
                if (this.Troll_lines_Button.Pushed)
                {
                    this.Edit_Button.Pushed = false;
                    this.Rail_Button.Pushed = false;
                    this.Stops_Button.Pushed = false;
                    this.Park_Button.Pushed = false;
                    this.Route_Button.Pushed = false;
                    this.Signals_Button.Pushed = false;
                    this.Svetofor_Button.Pushed = false;
                    this.Object_Button.Pushed = false;
                }
                else
                {
                    this.Troll_lines_Button.Pushed = true;
                }
            }
            if (e.Button == this.Stops_Button)
            {
                if (this.Stops_Button.Pushed)
                {
                    this.SetPanelControls(this.stops_panel);
                    this.edit_panel.Visible = true;
                    this.Edit_Button.Pushed = false;
                    this.Rail_Button.Pushed = false;
                    this.Troll_lines_Button.Pushed = false;
                    this.Park_Button.Pushed = false;
                    this.Route_Button.Pushed = false;
                    this.Signals_Button.Pushed = false;
                    this.Svetofor_Button.Pushed = false;
                    this.Object_Button.Pushed = false;
                }
                else
                {
                    this.Stops_Button.Pushed = true;
                }
            }
            if (e.Button == this.Park_Button)
            {
                if (this.Park_Button.Pushed)
                {
                    this.SetPanelControls(this.park_panel);
                    this.edit_panel.Visible = true;
                    this.Edit_Button.Pushed = false;
                    this.Rail_Button.Pushed = false;
                    this.Troll_lines_Button.Pushed = false;
                    this.Stops_Button.Pushed = false;
                    this.Route_Button.Pushed = false;
                    this.Signals_Button.Pushed = false;
                    this.Svetofor_Button.Pushed = false;
                    this.Object_Button.Pushed = false;
                }
                else
                {
                    this.Park_Button.Pushed = true;
                }
            }
            if (e.Button == this.Route_Button)
            {
                if (this.Route_Button.Pushed)
                {
                    this.SetPanelControls(this.route_panel);
                    this.edit_panel.Visible = true;
                    this.Edit_Button.Pushed = false;
                    this.Rail_Button.Pushed = false;
                    this.Troll_lines_Button.Pushed = false;
                    this.Stops_Button.Pushed = false;
                    this.Park_Button.Pushed = false;
                    this.Signals_Button.Pushed = false;
                    this.Svetofor_Button.Pushed = false;
                    this.Object_Button.Pushed = false;
                }
                else
                {
                    this.Route_Button.Pushed = true;
                }
            }
            if (e.Button == this.Signals_Button)
            {
                if (this.Signals_Button.Pushed)
                {
                    this.SetPanelControls(this.signals_panel);
                    this.edit_panel.Visible = true;
                    this.Edit_Button.Pushed = false;
                    this.Rail_Button.Pushed = false;
                    this.Troll_lines_Button.Pushed = false;
                    this.Stops_Button.Pushed = false;
                    this.Park_Button.Pushed = false;
                    this.Route_Button.Pushed = false;
                    this.Svetofor_Button.Pushed = false;
                    this.Object_Button.Pushed = false;
                }
                else
                {
                    this.Signals_Button.Pushed = true;
                }
            }
            if (e.Button == this.Svetofor_Button)
            {
                if (this.Svetofor_Button.Pushed)
                {
                    this.SetPanelControls(this.svetofor_panel);
                    this.edit_panel.Visible = true;
                    this.Edit_Button.Pushed = false;
                    this.Rail_Button.Pushed = false;
                    this.Troll_lines_Button.Pushed = false;
                    this.Stops_Button.Pushed = false;
                    this.Park_Button.Pushed = false;
                    this.Route_Button.Pushed = false;
                    this.Signals_Button.Pushed = false;
                    this.Object_Button.Pushed = false;
                }
                else
                {
                    this.Svetofor_Button.Pushed = true;
                }
            }
            if (e.Button == this.Object_Button)
            {
                if (this.Object_Button.Pushed)
                {
                    this.SetPanelControls(this.object_panel);
                    this.edit_panel.Visible = true;
                    this.Edit_Button.Pushed = false;
                    this.Rail_Button.Pushed = false;
                    this.Troll_lines_Button.Pushed = false;
                    this.Stops_Button.Pushed = false;
                    this.Park_Button.Pushed = false;
                    this.Route_Button.Pushed = false;
                    this.Signals_Button.Pushed = false;
                    this.Svetofor_Button.Pushed = false;
                }
                else
                {
                    this.Object_Button.Pushed = true;
                }
            }
            if (this.Rail_Button.Pushed)
            {
                this.Rail_Edit_Button.Visible = true;
                this.Rail_Build_Direct_Button.Visible = splines_aviable;
                this.Rail_Build_Curve_Button.Visible = splines_aviable;
                this.Rail_Build_попутки_Button.Visible = splines_aviable;
                this.Rail_Build_встречки_Button.Visible = splines_aviable;
                //this.Tratuar_Button.Visible = true;
                //this.Tratuar_Button1.Visible = true;
                //this.Rail_Build_double_Button.Visible = false;
                //this.Rail_Build_double1_Button.Visible = false;
                this.Road_Button.Visible = splines_aviable;
//                this.Road_Button.Enabled = false;
            }
            else
            {
                this.Rail_Edit_Button.Visible = false;
                this.Rail_Build_Direct_Button.Visible = false;
                this.Rail_Build_Curve_Button.Visible = false;
                this.Rail_Build_попутки_Button.Visible = false;
                this.Rail_Build_встречки_Button.Visible = false;
                //this.Rail_Build_double_Button.Visible = false;
                //this.Rail_Build_double1_Button.Visible = false;
                //this.Tratuar_Button.Visible = false;
                //this.Tratuar_Button1.Visible = false;
                this.Road_Button.Visible = false;
                this.Rail_Edit_Button.Pushed = true;
                this.Rail_Build_Direct_Button.Pushed = false;
                this.Rail_Build_Curve_Button.Pushed = false;
                this.Rail_Build_встречки_Button.Pushed = false;
                this.Rail_Build_попутки_Button.Pushed = false;
            }
            if (this.Rail_Build_встречки_Button.Pushed)
            {
                this.Rail_Build_встречки1_Button.Visible = true;
                this.Rail_Build_встречки2_Button.Visible = true;
                this.Rail_Build_встречки3_Button.Visible = true;
            }
            else
            {
                this.Rail_Build_встречки1_Button.Visible = false;
                this.Rail_Build_встречки2_Button.Visible = false;
                this.Rail_Build_встречки3_Button.Visible = false;
                this.Rail_Build_встречки1_Button.Pushed = true;
                this.Rail_Build_встречки2_Button.Pushed = false;
                this.Rail_Build_встречки3_Button.Pushed = false;
            }
            if (this.Rail_Build_попутки_Button.Pushed)
            {
                this.Rail_Build_попутки1_Button.Visible = true;
                this.Rail_Build_попутки2_Button.Visible = true;
                this.Rail_Build_попутки3_Button.Visible = true;

            }
            else
            {
                this.Rail_Build_попутки1_Button.Visible = false;
                this.Rail_Build_попутки2_Button.Visible = false;
                this.Rail_Build_попутки3_Button.Visible = false;
                this.Rail_Build_попутки1_Button.Pushed = true;
                this.Rail_Build_попутки2_Button.Pushed = false;
                this.Rail_Build_попутки3_Button.Pushed = false;

            }

            if (e.Button == Rail_Build_попутки1_Button)
            {
                if (this.Rail_Build_попутки1_Button.Pushed)
                {
                    this.Rail_Build_попутки2_Button.Pushed = false;
                    this.Rail_Build_попутки3_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_попутки1_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_попутки2_Button)
            {
                if (this.Rail_Build_попутки2_Button.Pushed)
                {
                    this.Rail_Build_попутки1_Button.Pushed = false;
                    this.Rail_Build_попутки3_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_попутки2_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_попутки3_Button)
            {
                if (this.Rail_Build_попутки3_Button.Pushed)
                {
                    this.Rail_Build_попутки1_Button.Pushed = false;
                    this.Rail_Build_попутки2_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_попутки3_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_встречки1_Button)
            {
                if (this.Rail_Build_встречки1_Button.Pushed)
                {
                    this.Rail_Build_встречки2_Button.Pushed = false;
                    this.Rail_Build_встречки3_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_встречки1_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_встречки2_Button)
            {
                if (this.Rail_Build_встречки2_Button.Pushed)
                {
                    this.Rail_Build_встречки1_Button.Pushed = false;
                    this.Rail_Build_встречки3_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_встречки2_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_встречки3_Button)
            {
                if (this.Rail_Build_встречки3_Button.Pushed)
                {
                    this.Rail_Build_встречки1_Button.Pushed = false;
                    this.Rail_Build_встречки2_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_встречки3_Button.Pushed = true;
                }
            }
            if (e.Button == this.Rail_Edit_Button)
            {
                if (this.Rail_Edit_Button.Pushed)
                {
                    this.Rail_Build_Direct_Button.Pushed = false;
                    this.Rail_Build_Curve_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Edit_Button.Pushed = true;
                }
            }
            else if (e.Button == this.Rail_Build_Direct_Button)
            {
                if (this.Rail_Build_Direct_Button.Pushed)
                {
                    this.Rail_Edit_Button.Pushed = false;
                    this.Rail_Build_Curve_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_Direct_Button.Pushed = true;
                }
            }
            else if (e.Button == this.Rail_Build_Curve_Button)
            {
                if (this.Rail_Build_Curve_Button.Pushed)
                {
                    this.Rail_Edit_Button.Pushed = false;
                    this.Rail_Build_Direct_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_Curve_Button.Pushed = true;
                }
            }
            if (e.Button == this.Road_Button)
            {
            	this.Road_Button.Pushed = !this.Road_Button.Pushed;
            }
            if (this.Rail_Build_Direct_Button.Pushed || this.Rail_Build_Curve_Button.Pushed)
            {
                if (this.строящаяся_дорога == null)//((this.строящаяся_дорога == null) || (e.Button == this.Road_Button))
                {
                    if (this.Road_Button.Pushed)
                    {
                        this.строящаяся_дорога = new Road(0.0, 0.0, 20.0, 0.0, 0.0, true, 5.0, 5.0);
                        this.строящаяся_дорога.ОбновитьСледующиеДороги(this.мир.Дороги);
                    }
                    else
                    {
                        this.строящаяся_дорога = new Рельс(0.0, 0.0, 20.0, 0.0, 0.0, true);
                        this.строящаяся_дорога.ОбновитьСледующиеДороги(this.мир.Рельсы);
                    }
                    this.строящаяся_дорога.name = Splines_Models_Box.Text;
                    this.строящаяся_дорога.CreateMesh();
                    this.строящаяся_дорога.Color = 0xff;
                    if (this.строящаяся_дорога is Рельс)
                    {
                        ((Рельс)this.строящаяся_дорога).добавочные_провода.CreateMesh();
                    }
                    this.edit_panel.Enabled = false;
                }
            }
            else if (this.строящаяся_дорога != null)
            {
                this.строящаяся_дорога = null;
                this.edit_panel.Enabled = true;
            }
            if (this.Troll_lines_Button.Pushed)
            {
                this.Troll_lines_Edit_Button.Visible = true;
                this.Troll_lines_Draw_Button.Visible = true;
                this.Troll_lines_Flag_Button.Visible = true;
            }
            else
            {
                this.Troll_lines_Edit_Button.Visible = false;
                this.Troll_lines_Draw_Button.Visible = false;
                this.Troll_lines_Flag_Button.Visible = false;
                this.Troll_lines_Edit_Button.Pushed = true;
                this.Troll_lines_Draw_Button.Pushed = false;
                this.Troll_lines_Flag_Button.Pushed = false;
            }
            if (e.Button == this.Troll_lines_Edit_Button)
            {
                if (this.Troll_lines_Edit_Button.Pushed)
                {
                    this.Troll_lines_Draw_Button.Pushed = false;
                }
                else
                {
                    this.Troll_lines_Edit_Button.Pushed = true;
                }
            }
            else if (e.Button == this.Troll_lines_Draw_Button)
            {
                if (this.Troll_lines_Draw_Button.Pushed)
                {
                    this.Troll_lines_Edit_Button.Pushed = false;
                }
                else
                {
                    this.Troll_lines_Draw_Button.Pushed = true;
                }
            }
            if (this.Troll_lines_Draw_Button.Pushed)
            {
            	if ((this.строящиеся_провода == null) || (e.Button == this.Troll_lines_Flag_Button))
                {
                	if (!this.Troll_lines_Flag_Button.Pushed)
                	{
                		this.строящиеся_провода = new Строящиеся_провода();
                	}
                	else
                	{
                		this.строящиеся_провода = new Строящиеся_трамвайные_провода();
                	}
                }
            }
            else if (this.строящиеся_провода != null)
            {
                this.строящиеся_провода = null;
            }
            if (this.Park_Button.Pushed)
            {
                this.Park_Edit_Button.Visible = true;
                this.Park_In_Button.Visible = true;
                this.Park_Out_Button.Visible = true;
                this.Park_Rails_Button.Visible = true;
            }
            else
            {
                this.Park_Edit_Button.Visible = false;
                this.Park_In_Button.Visible = false;
                this.Park_Out_Button.Visible = false;
                this.Park_Rails_Button.Visible = false;
                this.Park_Edit_Button.Pushed = true;
                this.Park_In_Button.Pushed = false;
                this.Park_Out_Button.Pushed = false;
                this.Park_Rails_Button.Pushed = false;
            }
            if (e.Button == this.Park_Edit_Button)
            {
                if (this.Park_Edit_Button.Pushed)
                {
                    this.Park_In_Button.Pushed = false;
                    this.Park_Out_Button.Pushed = false;
                    this.Park_Rails_Button.Pushed = false;
                }
                else
                {
                    this.Park_Edit_Button.Pushed = true;
                }
            }
            else if (e.Button == this.Park_In_Button)
            {
                if (this.Park_In_Button.Pushed)
                {
                    this.Park_Edit_Button.Pushed = false;
                    this.Park_Out_Button.Pushed = false;
                    this.Park_Rails_Button.Pushed = false;
                }
                else
                {
                    this.Park_In_Button.Pushed = true;
                }
            }
            else if (e.Button == this.Park_Out_Button)
            {
                if (this.Park_Out_Button.Pushed)
                {
                    this.Park_Edit_Button.Pushed = false;
                    this.Park_In_Button.Pushed = false;
                    this.Park_Rails_Button.Pushed = false;
                }
                else
                {
                    this.Park_Out_Button.Pushed = true;
                }
            }
            else if (e.Button == this.Park_Rails_Button)
            {
                if (this.Park_Rails_Button.Pushed)
                {
                    this.Park_Edit_Button.Pushed = false;
                    this.Park_In_Button.Pushed = false;
                    this.Park_Out_Button.Pushed = false;
                }
                else
                {
                    this.Park_Rails_Button.Pushed = true;
                }
            }
            this.narad_panel.Visible = this.Route_Button.Pushed && this.Route_ShowNarads_Box.Checked;
            this.ОбновитьРаскрашенныеСплайны();
            this.ОбновитьРаскрашенныеПровода();
            this.Check_All_Splines_Boxes();
        }

        private void UpdateNaradControls(Order наряд)
        {
            Narad_Runs_Box.Items.Clear();
            if (наряд != null)
            {
                for (var i = 0; i < наряд.рейсы.Length; i++)
                {
                    var num2 = i + 1;
                    Narad_Runs_Box.Items.Add("Рейс " + num2);
                }
            }
            Narad_Runs_Box_SelectedIndexChanged(null, new EventArgs());
        }

        private void UpdatePanels()
        {
            UpdateParksList();
            Park_Box_SelectedIndexChanged(null, new EventArgs());
            Stops_Box.Items.Clear();
            foreach (var остановка in мир.остановки)
            {
                Stops_Box.Items.Add(остановка.название);
            }
            Stops_Box_SelectedIndexChanged(null, new EventArgs());
            Route_Box.Items.Clear();
            foreach (var маршрут in мир.маршруты)
            {
                Route_Box.Items.Add(маршрут.number);
            }
            RouteBoxSelectedIndexChanged(null, new EventArgs());
            Signals_Box.Items.Clear();
            for (var i = 1; i < мир.сигнальныеСистемы.Length + 1; i++)
            {
                Signals_Box.Items.Add("Система " + i);
            }
            Signals_Box_SelectedIndexChanged(null, new EventArgs());
            Svetofor_Box.Items.Clear();
            for (var j = 1; j < мир.светофорныеСистемы.Length + 1; j++)
            {
                Svetofor_Box.Items.Add("Система " + j);
            }
            Svetofor_Box_SelectedIndexChanged(null, new EventArgs());
            Objects_Box.Items.Clear();
            Stops_Model_Box.Items.Clear();
            Svetofor_Model_Box.Items.Clear();
            Signals_Model_Box.Items.Clear();
            try
            {
            	foreach (var _model in ObjectLoader.objects[0])
                {
                	Objects_Box.Items.Add(_model.name);
                }
                Objects_Box.SelectedIndex = (Objects_Box.Items.Count > 0) ? 0 : -1;
                foreach (var _model in ObjectLoader.objects[1])
                {
                	Stops_Model_Box.Items.Add(_model.name);
                }
                Stops_Model_Box.SelectedIndex = (Stops_Model_Box.Items.Count > 0) ? 0 : -1;
                this.Stops_Add_Button.Enabled = (Stops_Model_Box.Items.Count > 0);
                foreach (var _model in ObjectLoader.objects[2])
                {
                	Svetofor_Model_Box.Items.Add(_model.name);
                }
                Svetofor_Model_Box.SelectedIndex = (Svetofor_Model_Box.Items.Count > 0) ? 0 : -1;
                foreach (var _model in ObjectLoader.objects[3])
                {
                	Signals_Model_Box.Items.Add(_model.name);
                }
                Signals_Model_Box.SelectedIndex = (Signals_Model_Box.Items.Count > 0) ? 0 : -1;
            }
            catch (DirectoryNotFoundException e)
            {
                // если каталог не найден выводим сообщение
                MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Objects_Box.Enabled = false;
                Objects_Instance_Box.Enabled = false;
                Objects_Add_Button.Enabled = false;
                Objects_Remove_Button.Enabled = false;
                Objects_ShowLocation_Button.Enabled = false;
                Objects_EditLocation_Button.Enabled = false;
            }
            UpdateObjectsList();
            Splines_Models_Box.Items.Clear();
        	foreach (var model in SplineLoader.splines)
        	{
        		Splines_Models_Box.Items.Add(model.name);
        	}
        	splines_aviable = (Splines_Models_Box.Items.Count > 0);
        	Splines_Models_Box.Enabled = splines_aviable;
        	Splines_Models_Box.SelectedIndex = (splines_aviable) ? 0 : -1;
        	UpdateSplinesList();
        }
        
        private void UpdateObjectsList()
        {
        	Objects_Instance_Box.Items.Clear();
        	for (var i = 0; i < мир.объекты.Count; i++)
            {
                if (мир.объекты[i] != null)
                {
                	Objects_Instance_Box.Items.Add("Obj " + (i + 1) + ", " + ((мир.объекты[i].model != null) ? мир.объекты[i].model.name : "???"));
                }
            }
            if (Objects_Instance_Box.Items.Count > 0)
            {
                Objects_Instance_Box.SelectedIndex = 0;
            }
            Check_All_Objects_Boxes();
        }
        
        private void UpdateSplinesList()
        {
        	Splines_Instance_Box.Items.Clear();
        	var rails = мир.Рельсы;
        	for (int i = 1; i < rails.Length + 1; i++)
        	{
        		Splines_Instance_Box.Items.Add("Rail " + i + ", " + rails[i-1].name);
        	}
        	var roads = мир.Дороги;
        	for (int i = 1; i < roads.Length + 1; i++)
        	{
        		Splines_Instance_Box.Items.Add("Road " + i + ", " + roads[i-1].name);
        	}
        	if (Splines_Instance_Box.Items.Count > 0)
        	{
        		Splines_Instance_Box.SelectedIndex = 0;
        	}
        	this.Check_All_Splines_Boxes();
        }

        private void UpdateRouteControls(Route маршрут)
        {
            Route_Runs_Box.Items.Clear();
            if (маршрут != null)
            {
                for (var i = 0; i < маршрут.trips.Count; i++)
                {
                    var num4 = i + 1;
                    if (Route_Runs_Box != null) Route_Runs_Box.Items.Add("Рейс " + num4);
                }
                for (var j = 0; j < маршрут.parkTrips.Count; j++)
                {
                    var num5 = j + 1;
                    if (Route_Runs_Box != null) Route_Runs_Box.Items.Add("Парковый рейс " + num5.ToString());
                }
                if (Route_Runs_Box.Items.Count > 0)
                {
                    Route_Runs_Box.SelectedIndex = 0;
                }
            }
            Route_Runs_Box_SelectedIndexChanged(null, new EventArgs());
//            RollingStockRouteBox.Enabled = Route_Box.Items.Count > 0;
            Narad_Box.Items.Clear();
            if (маршрут != null)
            {
                for (var k = 0; k < маршрут.orders.Length; k++)
                {
                    Narad_Box.Items.Add(маршрут.orders[k].номер);
                }
            }
            Narad_Box_SelectedIndexChanged(null, new EventArgs());
//            RollingStockBox.Enabled = Narad_Box.Items.Count > 0;
        }

        private void UpdateSignalsControls(Сигнальная_система система)
        {
            this.Signals_Element_Box.Items.Clear();
            if (система != null)
            {
            	for (int j = 0; j < система.vsignals.Count; j++)
            	{
            		Signals_Element_Box.Items.Add("Сигнал " + (j + 1));
            	}
                for (var i = 0; i < система.элементы.Count; i++)
                {
                	Signals_Element_Box.Items.Add("Контакт " + (i + 1));
                }
                if (система.элементы.Count > 0)
                {
                	Signals_Element_Box.SelectedIndex = система.vsignals.Count;
                }
            }
            Signals_Element_Box_SelectedIndexChanged(null, new EventArgs());
        }

        private void UpdateSvetoforControls(Светофорная_система система)
        {
            Svetofor_Element_Box.Items.Clear();
            if (система != null)
            {
                for (var i = 0; i < система.светофоры.Count; i++)
                {
                    var num3 = i + 1;
                    Svetofor_Element_Box.Items.Add("Светофор " + num3);
                }
                for (var j = 0; j < система.светофорные_сигналы.Count; j++)
                {
                    var num4 = j + 1;
                    Svetofor_Element_Box.Items.Add("Сигнал " + num4);
                }
                if (система.светофорные_сигналы.Count > 0)
                {
                    Svetofor_Element_Box.SelectedIndex = система.светофоры.Count;
                }
            }
            Svetofor_Element_Box_SelectedIndexChanged(null, new EventArgs());
        }
        
        private void UpdateParksList()
        {
        	Park_Box.Items.Clear();
            foreach (var парк in мир.парки)
            {
                Park_Box.Items.Add(парк.название);
            }
        	Narad_Park_Box.Items.Clear();
            foreach (var парк in мир.парки)
            {
                Narad_Park_Box.Items.Add(парк.название);
            }
        }

        private void ОбновитьРаскрашенныеПровода()
        {
            if (this.раскрашены_провода)
            {
                foreach (Контактный_провод _провод in this.мир.контактныеПровода)
                {
                    _провод.color = 0;
                }
                foreach (Трамвайный_контактный_провод _провод2 in this.мир.контактныеПровода2)
                {
                    _провод2.color = 0;
                }
                this.раскрашены_провода = false;
            }
            if (this.Troll_lines_Button.Pushed)
            {
                foreach (Контактный_провод _провод2 in this.мир.контактныеПровода)
                {
                    if (_провод2.обесточенный)
                    {
                        _провод2.color = 0xff0000;
                    }
                }
                foreach (Трамвайный_контактный_провод _провод1 in this.мир.контактныеПровода2)
                {
                    if (_провод1.обесточенный)
                    {
                        _провод1.color = 0xff0000;
                    }
                }
                this.раскрашены_провода = true;
            }
        }

        private void ОбновитьРаскрашенныеСплайны()
        {
            if (this.раскрашены_рельсы)
            {
                foreach (Road дорога in this.мир.ВсеДороги)
                {
                    дорога.Color = 0;
                }
                this.раскрашены_рельсы = false;
            }
            try
            {
            	if ((this.Rail_Button.Pushed) && (Splines_Instance_Box.SelectedIndex >= 0) && (time_color > 0.0))
            	{
            		this.мир.ВсеДороги[Splines_Instance_Box.SelectedIndex].Color = 0xff;
            		this.раскрашены_рельсы = true;
            	}
                if (this.Park_Button.Pushed && (this.Park_Box.SelectedIndex >= 0))
                {
                    Парк парк = this.мир.парки[this.Park_Box.SelectedIndex];
                    if (парк.въезд != null)
                    {
                        парк.въезд.Color = 0xff00;
                    }
                    if (парк.выезд != null)
                    {
                        парк.выезд.Color = 0xff0000;
                    }
                    foreach (Road дорога2 in парк.пути_стоянки)
                    {
                        дорога2.Color = 0xff;
                    }
                    this.раскрашены_рельсы = true;
                }
                if (this.Stops_Button.Pushed && (this.Stops_Box.SelectedIndex >= 0))
                {
                    Stop остановка = this.мир.остановки[this.Stops_Box.SelectedIndex];
                    foreach (Road дорога3 in остановка.частьПути)
                    {
                        дорога3.Color = 0xff;
                    }
                    this.раскрашены_рельсы = true;
                }
                if ((this.Route_Button.Pushed && (this.Route_Box.SelectedIndex >= 0)) && (this.Route_Runs_Box.SelectedIndex >= 0))
                {
                    Trip рейс = this.мир.маршруты[this.Route_Box.SelectedIndex].AllTrips[this.Route_Runs_Box.SelectedIndex];
                    for (int i = 0; i < рейс.pathes.Length; i++)
                    {
                        if (i == 0)
                        {
                            рейс.pathes[i].Color = 0xff0000;
                        }
                        else if (i == (рейс.pathes.Length - 1))
                        {
                            рейс.pathes[i].Color = 0xff00;
                        }
                        else if (рейс.inPark && (i >= рейс.inParkIndex))
                        {
                            рейс.pathes[i].Color = 0xffff00;
                        }
                        else
                        {
                            рейс.pathes[i].Color = 0xff;
                        }
                    }
                    this.раскрашены_рельсы = true;
                }
                if ((this.Svetofor_Button.Pushed && (this.Svetofor_Box.SelectedIndex >= 0)) && ((this.Svetofor_Element_Box.SelectedIndex >= 0) && (this.Svetofor_Element_Box.SelectedIndex >= this.выбранная_светофорная_система.светофоры.Count)))
                {
                    int num2 = this.Svetofor_Element_Box.SelectedIndex - this.выбранная_светофорная_система.светофоры.Count;
                    if (this.выбранная_светофорная_система.светофорные_сигналы[num2].дорога != null)
                    {
                        this.выбранная_светофорная_система.светофорные_сигналы[num2].дорога.Color = 0xff;
                    }
                    this.раскрашены_рельсы = true;
                }
            }
            catch
            {
            }
        }

        private void Повернуть_всё(double угол)
        {
            this.угол_поворота += угол;
            this.игрок.cameraPosition.угол_x += угол;
            this.игрок.cameraRotation.x += угол;
            foreach (Road дорога in this.мир.ВсеДороги)
            {
                дорога.концы[0].угол += угол;
                дорога.концы[1].угол += угол;
                дорога.направления[0] += угол;
                дорога.направления[1] += угол;
                дорога.ОбновитьСтруктуру();
            }
            if (this.строящаяся_дорога != null)
            {
                this.строящаяся_дорога.концы[0].угол += угол;
                this.строящаяся_дорога.концы[1].угол += угол;
                this.строящаяся_дорога.направления[0] += угол;
                this.строящаяся_дорога.направления[1] += угол;
                this.строящаяся_дорога.ОбновитьСтруктуру();
            }
            foreach (Контактный_провод _провод in this.мир.контактныеПровода)
            {
                _провод.начало.угол += угол;
                _провод.конец.угол += угол;
            }
            foreach (Контактный_провод _провод in this.мир.контактныеПровода2)
            {
                _провод.начало.угол += угол;
                _провод.конец.угол += угол;
            }
            if (this.строящиеся_провода != null)
            {
                this.строящиеся_провода.концы[0].угол += угол;
                this.строящиеся_провода.концы[1].угол += угол;
                this.строящиеся_провода.направления[0] += угол;
                this.строящиеся_провода.направления[1] += угол;
                this.строящиеся_провода.направление += угол;
                this.строящиеся_провода.Обновить();
            }
        }

        public DoublePoint cursor_pos
        {
            get
            {
                int num = (base.Size.Width - base.ClientSize.Width) / 2;
                int num2 = (base.Size.Height - base.ClientSize.Height) - num;
                double num3 = ((double)(Control.MousePosition.X - ((((2 * (base.Location.X + num)) + this.Sizable_Panel.Left) + this.Sizable_Panel.Right) / 2))) / MyDirect3D.масштаб;
                double num4 = ((double)(Control.MousePosition.Y - ((((2 * (base.Location.Y + num2)) + this.Sizable_Panel.Top) + this.Sizable_Panel.Bottom) / 2))) / MyDirect3D.масштаб;
                return new DoublePoint(this.игрок.cameraPosition.x + num3, this.игрок.cameraPosition.z - num4);
            }
        }

        /*public Положение ближайшее_положение
        {
            get
            {
                return this.мир.Найти_ближайшее_положение(this.cursor_pos);
            }
        }*/

        public Контактный_провод[] ближайшие_провода
        {
            get
            {
                DoublePoint point = this.cursor_pos;
                List<Контактный_провод> list = new List<Контактный_провод>();
                List<int> list2 = new List<int>();
                for (int i = 0; i < this.мир.контактныеПровода.Length; i++)
                {
                    Контактный_провод item = this.мир.контактныеПровода[i];
                    DoublePoint point2 = (point - item.начало) / (item.конец - item.начало);
                    if ((point2.x >= 0.0) && (point2.x < 1.0))
                    {
//                        DoublePoint point3 = item.конец - item.начало;point3.модуль
                        if (Math.Abs((double)(item.длина * point2.y)) < 0.5)
                        {
                            list.Add(item);
                            list2.Add(i);
                        }
                    }
                }
                if (((list.Count == 2) && ((list2[0] / 2) == (list2[1] / 2))) && (list[0].правый != list[1].правый))
                {
                    return list.ToArray();
                }
                return null;
            }
        }
        
        public Трамвайный_контактный_провод ближайший_провод
        {
            get
            {
                DoublePoint point = this.cursor_pos;
                for (int i = 0; i < this.мир.контактныеПровода2.Length; i++)
                {
                    Трамвайный_контактный_провод item = this.мир.контактныеПровода2[i];
                    DoublePoint point2 = (point - item.начало) / (item.конец - item.начало);
                    if ((point2.x >= 0.0) && (point2.x < 1.0))
                    {
                        if (Math.Abs((double)(item.длина * point2.y)) < 0.5)
                        {
                            return item;
                        }
                    }
                }
                return null;
            }
        }

        private Светофорная_система выбранная_светофорная_система
        {
            get
            {
                if (this.Svetofor_Box.SelectedIndex >= 0)
                {
                    return this.мир.светофорныеСистемы[this.Svetofor_Box.SelectedIndex];
                }
                return new Светофорная_система();
            }
        }

        private Сигнальная_система выбранная_сигнальная_система
        {
            get
            {
                if (this.Signals_Box.SelectedIndex >= 0)
                {
                    return this.мир.сигнальныеСистемы[this.Signals_Box.SelectedIndex];
                }
                return new Сигнальная_система(0, 0);
            }
        }

        public Route выбранный_маршрут
        {
            get
            {
                if (this.Route_Box.SelectedIndex >= 0)
                {
                    return this.мир.маршруты[this.Route_Box.SelectedIndex];
                }
                return new Route(TypeOfTransport.Tramway, "-");
            }
        }

        private Order выбранный_наряд
        {
            get
            {
                if ((this.Narad_Box.SelectedIndex >= 0) && (this.Narad_Box.SelectedIndex < this.выбранный_маршрут.orders.Length))
                {
                    return this.выбранный_маршрут.orders[this.Narad_Box.SelectedIndex];
                }
                return new Order(new Парк(""), new Route(TypeOfTransport.Tramway, ""), "", "");
            }
        }

        private Trip выбранный_рейс
        {
            get
            {
                if (this.Route_Runs_Box.SelectedIndex >= this.выбранный_маршрут.trips.Count)
                {
                    return this.выбранный_маршрут.parkTrips[this.Route_Runs_Box.SelectedIndex - this.выбранный_маршрут.trips.Count];
                }
                if (this.Route_Runs_Box.SelectedIndex >= 0)
                {
                    return this.выбранный_маршрут.trips[this.Route_Runs_Box.SelectedIndex];
                }
                return new Trip();
            }
        }

        public Road строящаяся_дорога1
        {
            get
            {
                Road дорога;
                if (this.строящаяся_дорога == null)
                {
                    return null;
                }
                try { double num5 = this.строящаяся_дорога.ширина[0];
                double num6 = this.строящаяся_дорога.ширина[1];
                DoublePoint point = this.строящаяся_дорога.НайтиКоординаты(0.0, -num5);
                DoublePoint point2 = this.строящаяся_дорога.НайтиКоординаты(this.строящаяся_дорога.Длина, -num6);
                double num7 = this.строящаяся_дорога.направления[0];
                double num8 = this.строящаяся_дорога.направления[1];
                if (this.строящаяся_дорога is Рельс)
                {
                    дорога = new Рельс(point.x, point.y, point2.x, point2.y, num7, num8);
                }
                else
                {
                    дорога = new Road(point.x, point.y, point2.x, point2.y, num7, num8, num5, num6);
                }
                дорога.высота[0] = this.строящаяся_дорога.высота[0];
                дорога.высота[1] = this.строящаяся_дорога.высота[1];
                дорога.кривая = this.строящаяся_дорога.кривая;
                дорога.ОбновитьСледующиеДороги(this.мир.ВсеДороги);
                дорога.name = строящаяся_дорога.name;
                дорога.CreateMesh();
                if (this.строящаяся_дорога.Color == 0xff)
                {
                    дорога.Color = 0xff0000;
                }
                return дорога;}
                catch
                {
                	return null;
                }
            }
        }
        public Road строящаяся_дорога2
        {
            get
            {
                Road дорога;
                if (this.строящаяся_дорога == null)
                {
                    return null;
                }
                try { double num5 = this.строящаяся_дорога.ширина[0];
                double num6 = this.строящаяся_дорога.ширина[1];
                DoublePoint point = this.строящаяся_дорога.НайтиКоординаты(0.0, -2 * num5);
                DoublePoint point2 = this.строящаяся_дорога.НайтиКоординаты(this.строящаяся_дорога.Длина, -2 * num6);
                double num7 = this.строящаяся_дорога.направления[0];
                double num8 = this.строящаяся_дорога.направления[1];
                if (this.строящаяся_дорога is Рельс)
                {
                    дорога = new Рельс(point.x, point.y, point2.x, point2.y, num7, num8);
                }
                else
                {
                    дорога = new Road(point.x, point.y, point2.x, point2.y, num7, num8, num5, num6);
                }
                дорога.высота[0] = this.строящаяся_дорога.высота[0];
                дорога.высота[1] = this.строящаяся_дорога.высота[1];
                дорога.кривая = this.строящаяся_дорога.кривая;
                дорога.ОбновитьСледующиеДороги(this.мир.ВсеДороги);
                дорога.name = строящаяся_дорога.name;
                дорога.CreateMesh();
                if (this.строящаяся_дорога.Color == 0xff)
                {
                    дорога.Color = 0xff0000;
                }
                return дорога;}
                catch
                {
                	return null;
                }
            }
        }
        public Road строящаяся_обратная_дорога
        {
            get
            {
                Road дорога;
                if (this.строящаяся_дорога == null)
                {
                    return null;
                }
                try { double num11 = this.строящаяся_дорога.ширина[0];
                double num12 = this.строящаяся_дорога.ширина[1];
                DoublePoint point11 = this.строящаяся_дорога.НайтиКоординаты(0.0, num11);
                DoublePoint point12 = this.строящаяся_дорога.НайтиКоординаты(this.строящаяся_дорога.Длина, num12);
                double num13 = this.строящаяся_дорога.направления[0];
                double num14 = this.строящаяся_дорога.направления[1];
                if (this.строящаяся_дорога is Рельс)
                {
                    дорога = new Рельс(point12.x, point12.y, point11.x, point11.y, num14, num13);
                }
                else
                {
                    дорога = new Road(point12.x, point12.y, point11.x, point11.y, num14, num13, num12, num11);
                }
                дорога.высота[0] = this.строящаяся_дорога.высота[1];
                дорога.высота[1] = this.строящаяся_дорога.высота[0];
                дорога.кривая = this.строящаяся_дорога.кривая;
                дорога.ОбновитьСледующиеДороги(this.мир.ВсеДороги);
                дорога.name = строящаяся_дорога.name;
                дорога.CreateMesh();
                if (this.строящаяся_дорога.Color == 0xff)
                {
                    дорога.Color = 0xffff00;
                }
                return дорога;}
                catch
                {
                	return null;
                }
            }
        }
        public Road строящаяся_обратная_дорога1
        {
            get
            {
                Road дорога;
                if (this.строящаяся_дорога == null)
                {
                    return null;
                }
                try { double num21 = this.строящаяся_дорога.ширина[0];
                double num22 = this.строящаяся_дорога.ширина[1];
                DoublePoint point21 = this.строящаяся_дорога.НайтиКоординаты(0.0, 2 * num21);
                DoublePoint point22 = this.строящаяся_дорога.НайтиКоординаты(this.строящаяся_дорога.Длина, 2 * num22);
                double num23 = this.строящаяся_дорога.направления[0];
                double num24 = this.строящаяся_дорога.направления[1];
                if (this.строящаяся_дорога is Рельс)
                {
                    дорога = new Рельс(point22.x, point22.y, point21.x, point21.y, num24, num23);
                }
                else
                {
                    дорога = new Road(point22.x, point22.y, point21.x, point21.y, num24, num23, num22, num21);
                }
                дорога.высота[0] = this.строящаяся_дорога.высота[1];
                дорога.высота[1] = this.строящаяся_дорога.высота[0];
                дорога.кривая = this.строящаяся_дорога.кривая;
                дорога.ОбновитьСледующиеДороги(this.мир.ВсеДороги);
                дорога.name = строящаяся_дорога.name;
                дорога.CreateMesh();
                if (this.строящаяся_дорога.Color == 0xff)
                {
                    дорога.Color = 0xffff00;
                }
                return дорога;}
                catch
                {
                	return null;
                }
            }
        }
        public Road строящаяся_обратная_дорога2
        {
            get
            {
                Road дорога;
                if (this.строящаяся_дорога == null)
                {
                    return null;
                }
                try { double num = this.строящаяся_дорога.ширина[0];
                double num2 = this.строящаяся_дорога.ширина[1];
                DoublePoint point = this.строящаяся_дорога.НайтиКоординаты(0.0, 3 * num);
                DoublePoint point2 = this.строящаяся_дорога.НайтиКоординаты(this.строящаяся_дорога.Длина, 3 * num2);
                double num3 = this.строящаяся_дорога.направления[0];
                double num4 = this.строящаяся_дорога.направления[1];
                if (this.строящаяся_дорога is Рельс)
                {
                    дорога = new Рельс(point2.x, point2.y, point.x, point.y, num4, num3);
                }
                else
                {
                    дорога = new Road(point2.x, point2.y, point.x, point.y, num4, num3, num2, num);
                }
                дорога.высота[0] = this.строящаяся_дорога.высота[1];
                дорога.высота[1] = this.строящаяся_дорога.высота[0];
                дорога.кривая = this.строящаяся_дорога.кривая;
                дорога.ОбновитьСледующиеДороги(this.мир.ВсеДороги);
                дорога.name = строящаяся_дорога.name;
                дорога.CreateMesh();
                if (this.строящаяся_дорога.Color == 0xff)
                {
                    дорога.Color = 0xffff00;
                }
                return дорога;}
                catch
                {
                	return null;
                }
            }
        }

        private enum Стадия_стоительства
        {
            Нет,
            Второй_конец,
            Первый_конец
        }
        
        private class Строящиеся_провода
        {
            public double[] высота = new double[2];
            public DoublePoint[] конец;
            public DoublePoint[] концы = new DoublePoint[2];
            public double направление;
            public double[] направления = new double[2];
            public DoublePoint[] начало;
            public Контактный_провод[] провода;// = new Контактный_провод[4];
            public Стадия_стоительства стадия;

            public Строящиеся_провода()
            {
            	this.провода = new Контактный_провод[4];
                this.Create();
            }

            public virtual void Обновить()
            {
                this.направление = (Math.Round((double)((this.направление * 36.0) / Math.PI)) * Math.PI) / 36.0;//3.1415926535897931
                this.направления[0] = (Math.Round((double)((this.направления[0] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.направления[1] = (Math.Round((double)((this.направления[1] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.концы[0].угол -= this.направление;
                this.концы[1].угол -= this.направление;
                this.направления[0] -= this.направление;
                this.направления[1] -= this.направление;
                double num = Контактный_провод.расстояние_между_проводами / 2.0;
                this.провода[0].начало.x = this.концы[0].x + (num * Math.Tan(this.направления[0]));
                this.провода[0].начало.y = this.концы[0].y - num;
                this.провода[1].начало.x = this.концы[0].x - (num * Math.Tan(this.направления[0]));
                this.провода[1].начало.y = this.концы[0].y + num;
                this.провода[0].конец.x = this.концы[1].x + (num * Math.Tan(this.направления[1]));
                this.провода[0].конец.y = this.концы[1].y - num;
                this.провода[1].конец.x = this.концы[1].x - (num * Math.Tan(this.направления[1]));
                this.провода[1].конец.y = this.концы[1].y + num;
                if (this.стадия == Editor.Стадия_стоительства.Первый_конец)
                {
                    this.провода[2].начало = this.провода[0].начало - ((DoublePoint)(new DoublePoint(2.0 * this.направления[0]) * 20.0));
                    this.провода[2].конец = this.провода[0].начало;
                    this.провода[3].начало = this.провода[1].начало - ((DoublePoint)(new DoublePoint(2.0 * this.направления[0]) * 20.0));
                    this.провода[3].конец = this.провода[1].начало;
                }
                else
                {
                    this.провода[2].начало = this.провода[0].конец;
                    this.провода[2].конец = this.провода[0].конец + ((DoublePoint)(new DoublePoint(2.0 * this.направления[1]) * 20.0));
                    this.провода[3].начало = this.провода[1].конец;
                    this.провода[3].конец = this.провода[1].конец + ((DoublePoint)(new DoublePoint(2.0 * this.направления[1]) * 20.0));
                }
                this.провода[0].высота[0] = this.высота[0];
                this.провода[0].высота[1] = this.высота[1];
                this.провода[1].высота[0] = this.высота[0];
                this.провода[1].высота[1] = this.высота[1];
                this.провода[2].высота[0] = this.высота[1];
                this.провода[2].высота[1] = this.высота[1];
                this.провода[3].высота[0] = this.высота[1];
                this.провода[3].высота[1] = this.высота[1];
                this.провода[0].начало.угол += this.направление;
                this.провода[0].конец.угол += this.направление;
                this.провода[1].начало.угол += this.направление;
                this.провода[1].конец.угол += this.направление;
                this.провода[2].начало.угол += this.направление;
                this.провода[2].конец.угол += this.направление;
                this.провода[3].начало.угол += this.направление;
                this.провода[3].конец.угол += this.направление;
                this.концы[0].угол += this.направление;
                this.концы[1].угол += this.направление;
                this.направления[0] += this.направление;
                this.направления[1] += this.направление;
                if (this.начало != null)
                {
                    this.провода[0].начало = this.начало[0];
                    this.провода[1].начало = this.начало[1];
                }
                if (this.конец != null)
                {
                    this.провода[0].конец = this.конец[0];
                    this.провода[1].конец = this.конец[1];
                }
            }
            
            protected virtual void Create()
            {
            	for (int i = 0; i < this.провода.Length; i++)
                {
                	this.провода[i] = new Контактный_провод(0.0, 0.0, 20.0, 0.0, (i % 2) == 0);
                    this.провода[i].CreateMesh();
                    this.провода[i].color = (i < 2) ? 0xff : 0x3f3f3f;
                }
            }
        }
        
        private class Строящиеся_трамвайные_провода : Строящиеся_провода
        {
        	public bool flag = false;
        	
            public Строящиеся_трамвайные_провода()
            {
            	this.провода = new Контактный_провод[2];
                this.Create();
            }

            public override void Обновить()
            {
                this.направление = (Math.Round((double)((this.направление * 36.0) / Math.PI)) * Math.PI) / 36.0;
                this.направления[0] = (Math.Round((double)((this.направления[0] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.направления[1] = (Math.Round((double)((this.направления[1] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.концы[0].угол -= this.направление;
                this.концы[1].угол -= this.направление;
                this.направления[0] -= this.направление;
                this.направления[1] -= this.направление;
                this.провода[0].начало.x = this.концы[0].x;
                this.провода[0].начало.y = this.концы[0].y;
                this.провода[1].начало.x = this.концы[0].x;
                this.провода[1].начало.y = this.концы[0].y;
                this.провода[0].конец.x = this.концы[1].x;
                this.провода[0].конец.y = this.концы[1].y;
                this.провода[1].конец.x = this.концы[1].x;
                this.провода[1].конец.y = this.концы[1].y;
                if (this.стадия == Editor.Стадия_стоительства.Первый_конец)
                {
                    this.провода[1].начало = this.провода[0].начало - ((DoublePoint)(new DoublePoint(2.0 * this.направления[0]) * 20.0));
                    this.провода[1].конец = this.провода[0].начало;
                }
                else
                {
                    this.провода[1].начало = this.провода[0].конец;
                    this.провода[1].конец = this.провода[0].конец + ((DoublePoint)(new DoublePoint(2.0 * this.направления[1]) * 20.0));
                }
                this.провода[0].высота[0] = this.высота[0];
                this.провода[0].высота[1] = this.высота[1];
                this.провода[1].высота[0] = this.высота[1];
                this.провода[1].высота[1] = this.высота[1];
                this.провода[0].начало.угол += this.направление;
                this.провода[0].конец.угол += this.направление;
                this.провода[1].начало.угол += this.направление;
                this.провода[1].конец.угол += this.направление;
                this.концы[0].угол += this.направление;
                this.концы[1].угол += this.направление;
                this.направления[0] += this.направление;
                this.направления[1] += this.направление;
                if (this.начало != null)
                {
                    this.провода[0].начало = this.начало[0];
                }
                if (this.конец != null)
                {
                    this.провода[0].конец = this.конец[0];
                }
            }
            
            protected override void Create()
            {
            	for (int i = 0; i < this.провода.Length; i++)
                {
                	this.провода[i] = new Трамвайный_контактный_провод(0.0, 0.0, 20.0, 0.0);
                    this.провода[i].CreateMesh();
                    this.провода[i].color = (i < 1) ? 0xff : 0x3f3f3f;
                }
            }
        }
        
        private void Objects_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Check_All_Objects_Boxes();
        }

        private void Check_All_Objects_Boxes()
        {
            this.Objects_Instance_Box.Enabled = (Objects_Instance_Box.Items.Count != 0);
            this.Objects_Remove_Button.Enabled = (Objects_Instance_Box.SelectedIndex >= 0);
            this.Objects_Location_label.Enabled = (Objects_Instance_Box.SelectedIndex >= 0);
            this.Objects_ShowLocation_Button.Enabled = (Objects_Instance_Box.SelectedIndex >= 0);
            this.Objects_EditLocation_Button.Enabled = (Objects_Instance_Box.SelectedIndex >= 0);
        }

        private void Objects_Add_Button_Click(object sender, EventArgs e)
        {
            this.строящийся_объект = new Объект(this.Objects_Box.Text, 0.0, 0.0, 0.0, 0.0);
            this.строящийся_объект.CreateMesh();
            this.EnableControls(false);
//            modified = true;
        }

        private void Object_Instance_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Check_All_Objects_Boxes();
        }

        private void ObjectsRemoveButtonClick(object sender, EventArgs e)
        {
            var index = Objects_Instance_Box.SelectedIndex;
            мир.объекты.RemoveAt(index);
            UpdateObjectsList();
            if (index > 0)
            {
                Objects_Instance_Box.SelectedIndex = index - 1;
            }
            modified = true;
        }

        /*
                private void RollingStockRouteUpdate()
                {
                    if (Route_Box.Items.Count <= 0) return;
                    RollingStockRouteBox.Enabled = true;
                    RollingStockRouteBox.Items.Clear();
                    switch (мир.маршруты[Route_Box.SelectedIndex].видТранспорта)
                    {
                        case ВидТранспорта.Трамвай:
                            foreach (var tramway in new[] { "ЛМ-68М", "2 ЛМ-68М", "ЛМ-68М + ЛП-83", "ЛВС-86", "2 ЛВС-86", "ЛВС-89" })
                            {
                                RollingStockRouteBox.Items.Add(tramway);
                            }
                            break;
                        case ВидТранспорта.Троллейбус:
                            foreach (var троллейбус in Модели.Троллейбусы)
                            {
                                RollingStockRouteBox.Items.Add(троллейбус.name);
                            }
                            break;
                        case ВидТранспорта.Автобус:
                            foreach (var автобус in Модели.Автобусы)
                            {
                                RollingStockRouteBox.Items.Add(автобус.name);
                            }
                            break;
                    }
                    RollingStockRouteBox.SelectedIndex=0;
                }
        */

        private void RollingStockUpdate(Order наряд)
        {
//            RollingStockBox.Enabled = наряд.номер.Length > 0;
//            if (!RollingStockBox.Enabled) return;
            RollingStockBox.Items.Clear();
            if ((Narad_Box.Items.Count <= 0) || (Narad_Box.SelectedIndex < 0)) return;
            RollingStockBox.Items.Add("Случайный");
            switch (наряд.маршрут.typeOfTransport)
            {
                case TypeOfTransport.Tramway:
                    foreach (var tramway in Модели.Трамваи)
                    {
                        RollingStockBox.Items.Add(tramway.name);
                    }
                    break;
                case TypeOfTransport.Trolleybus:
                    foreach (var троллейбус in Модели.Троллейбусы)
                    {
                        RollingStockBox.Items.Add(троллейбус.name);
                    }
                    break;
                case TypeOfTransport.Bus:
                    foreach (var автобус in Модели.Автобусы)
                    {
                        RollingStockBox.Items.Add(автобус.name);
                    }
                    break;
            }
            RollingStockBox.Text = (string)((наряд.transport != "") || (наряд.transport != "Случайный") ? наряд.transport : RollingStockBox.Items[0]);
            if (!RollingStockBox.Items.Contains(RollingStockBox.Text))
            {
                RollingStockBox.Text = (string)RollingStockBox.Items[0];
            }
        }

        private void RollingStockBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            выбранный_наряд.transport = RollingStockBox.Text;
        }

        private void Objects_EditLocation_Button_Click(object sender, EventArgs e)
        {
            var selectedIndex = Objects_Instance_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            строящийся_объект = мир.объекты[selectedIndex];
            temp_index = selectedIndex;
            EnableControls(false);
            modified = true;
        }
        
        private void Objects_ShowLocation_Button_Click(object sender, EventArgs e)
	    {
	        int selectedIndex = Objects_Instance_Box.SelectedIndex;
	        if (selectedIndex < 0) return;
	        this.игрок.cameraPosition.xz_point = new DoublePoint(this.мир.объекты[selectedIndex].x0, this.мир.объекты[selectedIndex].y0);
	    }

        private void Route_Runs_Time_Box_Validated(object sender, EventArgs e)
        {
            this.выбранный_рейс.время_прибытия = Route_Runs_Time_Box.Time_Seconds;
        }

        private void TramwayBox_Click(object sender, EventArgs e)
        {
            var selectedIndex = Stops_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Tramway] = TramwayBox.Checked;
            TrolleybusBox.Checked = мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Trolleybus];
            BusBox.Checked = мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Bus];
        }

        private void TrolleybusBox_Click(object sender, EventArgs e)
        {
            var selectedIndex = Stops_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Trolleybus] = TrolleybusBox.Checked;
            TramwayBox.Checked = мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Tramway];
            BusBox.Checked = мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Bus];
        }

        private void BusBox_Click(object sender, EventArgs e)
        {
            var selectedIndex = Stops_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Bus] = BusBox.Checked;
            TramwayBox.Checked = мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Tramway];
            TrolleybusBox.Checked = мир.остановки[selectedIndex].typeOfTransport[TypeOfTransport.Trolleybus];
        }

        private void StopsButton_Click(object sender, EventArgs e)
        {
            StopListForm stop_list_form = new StopListForm(выбранный_маршрут, выбранный_рейс);
            stop_list_form.ShowDialog();
            stop_list_form.Dispose();
        }
		
		private void Splines_Remove_ButtonClick(object sender, EventArgs e)
		{
			if (Splines_Instance_Box.SelectedIndex < 0) return;
			var selectedIndex = Splines_Instance_Box.SelectedIndex;
			this.мир.listДороги.RemoveAt(selectedIndex);
			UpdateSplinesList();
			if (selectedIndex > 0)
			{
				Splines_Instance_Box.SelectedIndex = selectedIndex - 1;
			}
			this.modified = true;
		}
		
		private void Check_All_Splines_Boxes()
        {
			Splines_Models_Box.Enabled = !(this.Rail_Build_Curve_Button.Pushed || this.Rail_Build_Direct_Button.Pushed) && splines_aviable;
            Splines_Instance_Box.Enabled = !(this.Rail_Build_Curve_Button.Pushed || this.Rail_Build_Direct_Button.Pushed) && (Splines_Instance_Box.Items.Count != 0);
            Splines_Remove_Button.Enabled = !(this.Rail_Build_Curve_Button.Pushed || this.Rail_Build_Direct_Button.Pushed) && (Splines_Instance_Box.SelectedIndex >= 0);
            Splines_ChangeModel_Button.Enabled = Splines_Remove_Button.Enabled;
            Splines_ShowLocation_Button.Enabled = Splines_Remove_Button.Enabled;
            Splines_Location_label.Enabled = Splines_Remove_Button.Enabled;
//            Rails_NumericBox.Enabled = Splines_Remove_Button.Enabled;// && (this.мир.ВсеДороги[Splines_Instance_Box.SelectedIndex] is Рельс) && (((Рельс)this.мир.ВсеДороги[Splines_Instance_Box.SelectedIndex]).следующие_рельсы.Length > 1);
            Splines_Instance_BoxSelectedIndexChanged(null, null);
//            if (Rails_NumericBox.Enabled)
//            	Rails_NumericBox.Value = ((Рельс)this.мир.ВсеДороги[Splines_Instance_Box.SelectedIndex]).расстояние_добавочных_проводов;
        }
		
		private void Splines_Models_BoxSelectedIndexChanged(object sender, EventArgs e)
		{
			if (Splines_Models_Box.SelectedIndex < 0) return;
			foreach (var model in SplineLoader.splines)
			{
				if (model.name != Splines_Models_Box.Text) continue;
				this.Road_Button.Pushed = model.type == 1;
				break;
			}
		}
		
		private void Splines_ChangeModel_ButtonClick(object sender, EventArgs e)
		{
			foreach (var model in SplineLoader.splines)
			{
				if (model.name != Splines_Models_Box.Text) continue;
				if (model.type == мир.ВсеДороги[Splines_Instance_Box.SelectedIndex].model.type)
				{
					this.мир.ВсеДороги[Splines_Instance_Box.SelectedIndex].name = model.name;
					this.мир.ВсеДороги[Splines_Instance_Box.SelectedIndex].model = null;
					this.мир.ВсеДороги[Splines_Instance_Box.SelectedIndex].CreateMesh();
					this.modified = true;
				}
				break;
			}
		}
		
		private void Splines_ShowLocation_ButtonClick(object sender, EventArgs e)
		{
			int selectedIndex = Splines_Instance_Box.SelectedIndex;
	        if (selectedIndex < 0) return;
	        var road = this.мир.ВсеДороги[selectedIndex];
	        var point = road.НайтиКоординаты(road.Длина / 2.0, 0.0);
	        this.игрок.cameraPosition.xz_point = new DoublePoint(point.x, point.y);
	        time_color = 2.0;
	        this.ОбновитьРаскрашенныеСплайны();
		}
		
		private void Rails_NumericBoxEnterPressed(object sender, EventArgs e)
		{
			((Рельс)this.мир.ВсеДороги[Splines_Instance_Box.SelectedIndex]).расстояние_добавочных_проводов = this.Rail_Box_NumericBox.Value;
//			MessageBox.Show("успех!");
		}
		
		private void Splines_Instance_BoxSelectedIndexChanged(object sender, EventArgs e)
		{
			Rail_Box_NumericBox.Text = string.Empty;
			int selectedIndex = Splines_Instance_Box.SelectedIndex;
			Rail_Box_NumericBox.Enabled = Splines_Instance_Box.Enabled && (selectedIndex >= 0) && (selectedIndex < this.мир.Рельсы.Length) && (((Рельс)this.мир.ВсеДороги[selectedIndex]).следующие_рельсы.Length > 1);
			if (!Rail_Box_NumericBox.Enabled) return;
			Rail_Box_NumericBox.Value = ((Рельс)this.мир.ВсеДороги[selectedIndex]).расстояние_добавочных_проводов;
		}
    }
}
