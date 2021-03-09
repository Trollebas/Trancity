/*
 * Created by SharpDevelop.
 * User: Andrey
 * Date: 11.08.2009
 * Time: 10:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Common;
using Engine;
using SlimDX.Direct3D9;
using SlimDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;




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
        private MenuItem Exit_Item;
        private MenuItem Find_MinRadius_Item;
        private MenuItem Open_Item;
        private MenuItem New_Item;
        private MenuItem Refresh_All_TripStop_Lists_Item;
        private MenuItem Run_Item;
        private MenuItem Save_Item;
        private MenuItem SaveAs_Item;
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
        private ToolBarButton ButtonUndo;
        private Panel edit_panel;
        private string filename;
        private StatusBarPanel Height0_Status;
        private StatusBarPanel Height1_Status;
        private ImageList imageList;
        private StatusBarPanel Length_Status;
        private MainMenu mainMenu;
        private bool modified;
        public MouseEventArgs mouse_args = new MouseEventArgs(MouseButtons.None, 0, 0, 0, 0);
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
        private Panel old_panel;
        private ToolBarButton New_Button;
        private ToolBarButton Open_Button;
        private OpenFileDialog openFileDialog;
        private Engine.Controls.RenderPanel renderPanel;
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
        private ToolBarButton Rail_Build_Direct_Button;
        private ToolBarButton Rail_Button;
        private ToolBarButton Rail_Edit_Button;
        private Timer Refresh_Timer;
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
        private CheckBox TrolleybusAXBox;
        private Label Route_TransportType_label;
        private ToolBarButton Run_Button;
        private ToolBarButton Save_Button;
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
        //private ToolBarButton Troll_lines_Doblue;
        //private ToolBarButton Troll_lines_Aganist;
        private StatusBarPanel Wide0_Status;
        private StatusBarPanel Wide1_Status;
        private Game ����;
        private ����� �����;
        private �����[] ������;
        private World ���;
        private bool ����������_�������;
        private bool ����������_������;
        private ������_������������ �������������_������;
        private Road ����������_������;
        private Stop ����������_���������;
        private ����������_������� ����������_�������;
        private �������� ����������_��������;
        private �����������_������ ����������_�����������_������;
        private ����������_�������.������� ����������_�������_����������_�������;
        private Visual_Signal ����������_������_����������_�������;
        private double ����_��������;
        private ToolBarButton Object_Button;
        private ������ ����������_������;
        private bool splines_aviable;
        private double time_color = 10.0;
        private bool[] _lastMouseButtons = new bool[5];
        public bool ������� = true;
        private int _���������PosIndex;
        private const int num = 0x400;
        public static bool fmouse = true;
        public MyMenu menu;
        public static int col = 0;
        public static int row = 0;
        public bool ������������������;

        public ���������������� ���������;

        private static void Main(string[] args)
        {
            MyFeatures.CheckFolders(Application.StartupPath);
            var app = new Editor { ��������� = new ����������������() };
            app.������������������();
        }


        /// <summary>
        /// Editor Trancity
        /// </summary>
        public Editor()
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();
            //Logger.Log;
        }

        public void ApplyLocalization()
        {
            Localization.ApplyLocalization(this);
            Localization.ApplyLocalizationToolBar(this.toolBar);
        }

        private void Check_�����_Item_Click(object sender, EventArgs e)
        {
            int num = 0;
            int num2 = 0;
            foreach (Road ����� in this.���.���������)
            {
                �����.�����������������������(this.���.���������);
                if (�����.����������������.Length == 0)
                {
                    num++;
                    this.�����.cameraPosition.XZPoint = �����.�����[0];
                }
                if (�����.���������������.Length == 0)
                {
                    num2++;
                    this.�����.cameraPosition.XZPoint = �����.�����[1];
                }
            }
            if ((num > 0) && (num2 > 0))
            {
                MessageBox.Show(this, string.Format(Localization.current_.joints_begin_end/*"������� {0} ����� ��� ������ � {1} ����� ��� �����������."*/, num.ToString(), num2.ToString()), "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (num > 0)
            {
                MessageBox.Show(this, string.Format(Localization.current_.joints_begin/*"������� {0} ����� ��� ������."*/, num.ToString()), "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (num2 > 0)
            {
                MessageBox.Show(this, string.Format(Localization.current_.joints_end/*"������� {0} ����� ��� �����������."*/, num2.ToString()), "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                MessageBox.Show(this, Localization.current_.joints_checked/*"��� ����� ���������."*/, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void ComputeAllTime_Item_Click(object sender, EventArgs e)
        {
            List<Trip> list = new List<Trip>();
            List<int> list2 = new List<int>();
            foreach (Route ������� in this.���.��������)
            {
                list.AddRange(�������.AllTrips);
                for (int j = 0; j < �������.AllTrips.Count; j++)
                {
                    list2.Add(�������.typeOfTransport);
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].�����_�������� != 0.0)
                {
                    list.RemoveAt(i);
                    list2.RemoveAt(i);
                    i--;
                }
            }
            if (list.Count == 0)
            {
                MessageBox.Show(this, Localization.current_.routes_computed, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Refresh_Timer.Enabled = false;
                for (int k = 0; k < list.Count; k++)
                {
                    if (list[k].pathes.Length == 0)
                    {
                        MessageBox.Show(Localization.current_.route_failed, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        continue;
                    }
                    this.���.time = 0.0;
                    ComputeTimeDialog dialog = new ComputeTimeDialog(this.���, list2[k], list[k], this.�����);
                    dialog.Text = string.Format("{0} ({1} {2} {3})", dialog.Text, (k + 1), Localization.current_.of, list.Count);
                    if (dialog.ShowDialog(this) == DialogResult.Cancel)
                    {
                        break;
                    }
                    list[k].�����_�������� = this.���.time;
                }
                this.���.time = 0.0;
                this.Refresh_Timer.Enabled = true;
                this.Route_Runs_Box_SelectedIndexChanged(null, new EventArgs());
                this.modified = true;
            }
        }

        private void Editor_Form_Readme(object sender, CancelEventArgs e)
        {
            if (!this.modified) return;
            switch (MessageBox.Show("�� ������ ������������ ����������?", "Transedit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
            {
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;

                case DialogResult.Yes:
                    this.Infoo(sender, e);
                    //using (FileStream fstream = File.Open("...Readme_Editor.txt"));
                    return;
            }
        }

        private void Editor_Form_Closing(object sender, CancelEventArgs e)
        {
            if (!this.modified) return;
            switch (MessageBox.Show(Localization.current_.save_quit, "Transedit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
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
        /// �������
        /// </summary>
        public void Editor_Form_KeyDown(object sender, KeyEventArgs e)
        {
            var MouseState = MyDirectInput.Mouse_State;
            var KeyState = MyDirectInput.Key_State;
            var JStatesArray = MyDirectInput.Joystick_States;
            var FJStatesArray = MyDirectInput.Joystick_FilteredStates;
            var joystickDevices = MyDirectInput.JoystickDevices;
            var deviceGuids = MyDirectInput.DeviceGuids;
            // HACK: ������ ��������� ����� ��� ������� �� �������
            e.SuppressKeyPress = !this.edit_panel.Enabled;
            if ((e.KeyCode == Keys.W) && (MyDirect3D.���_������) && e.Control)
            {
                MyDirect3D.���_������ = true;
                MyDirect3D.����� = !MyDirect3D.�����;
                MyDirect3D.������� = (MyDirect3D.�����) ? 1.0 : 10.0;
            }
            if ((e.KeyCode == Keys.Add) && e.Control && (MyDirect3D.������� < 50))
            {
                MyDirect3D.������� = MyDirect3D.������� + 1;
            }
            if ((e.KeyCode == Keys.Subtract) && e.Control && (MyDirect3D.������� > 1))
            {
                MyDirect3D.������� = MyDirect3D.������� - 1;
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
            if ((this.����������_������ != null) && (this.����������_������.������ || (this.�������������_������ == ������_������������.���)))
            {
                if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Z))
                {
                    switch (this.�������������_������)
                    {
                        case ������_������������.���:
                        case ������_������������.������_�����:
                            this.����������_������.�����������[0] -= Math.PI / 36;
                            if (this.����������_������.�����������[0] <= -Math.PI)
                            {
                                this.����������_������.�����������[0] += Math.PI * 2;
                            }
                            break;

                        case ������_������������.������_�����:
                            this.����������_������.�����������[1] -= Math.PI / 36;
                            if (this.����������_������.�����������[1] <= -Math.PI)
                            {
                                this.����������_������.�����������[1] += Math.PI * 2;
                            }
                            break;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.A))
                {
                    switch (this.�������������_������)
                    {
                        case ������_������������.���:
                        case ������_������������.������_�����:
                            this.����������_������.�����������[0] += Math.PI / 36;
                            if (this.����������_������.�����������[0] > Math.PI)
                            {
                                this.����������_������.�����������[0] -= Math.PI * 2;
                            }
                            break;

                        case ������_������������.������_�����:
                            this.����������_������.�����������[1] += Math.PI / 36;
                            if (this.����������_������.�����������[1] > Math.PI)
                            {
                                this.����������_������.�����������[1] -= Math.PI * 2;
                            }
                            break;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
            }
            if (this.����������_������ != null)
            {
                if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.X))
                {
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[0] -= 0.5;
                        if (this.����������_������.������[0] < 0.5)
                        {
                            this.����������_������.������[0] = 0.5;
                        }
                    }
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[1] -= 0.5;
                        if (this.����������_������.������[1] < 0.5)
                        {
                            this.����������_������.������[1] = 0.5;
                        }
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.S))
                {
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[0] += 0.5;
                    }
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[1] += 0.5;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Next) || (e.KeyCode == Keys.C && !e.Control))
                {
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[0] -= 0.5;
                        if (this.����������_������.������[0] < 0.0)
                        {
                            this.����������_������.������[0] = 0.0;
                        }
                    }
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[1] -= 0.5;
                        if (this.����������_������.������[1] < 0.0)
                        {
                            this.����������_������.������[1] = 0.0;
                        }
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Prior) || (e.KeyCode == Keys.D && !e.Control))
                {
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[0] += 0.5;
                    }
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[1] += 0.5;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if (e.KeyCode == Keys.C && e.Control)
                {
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[0] -= 0.1;
                        if (this.����������_������.������[0] < 0.0)
                        {
                            this.����������_������.������[0] = 0.0;
                        }
                    }
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[1] -= 0.1;
                        if (this.����������_������.������[1] < 0.0)
                        {
                            this.����������_������.������[1] = 0.0;
                        }
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if (e.KeyCode == Keys.D && e.Control)
                {
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[0] += 0.1;
                    }
                    if ((this.�������������_������ == ������_������������.������_�����) || (this.�������������_������ == ������_������������.���))
                    {
                        this.����������_������.������[1] += 0.1;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Escape) && (this.�������������_������ != ������_������������.���))
                {
                    this.�������������_������ = ������_������������.���;
                }
            }
            if (this.����������_������� != null)
            {
                if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.Z) || (e.KeyCode == Keys.MButton))
                {
                    switch (this.����������_�������.������)
                    {
                        case ������_������������.���:
                            this.����������_�������.����������� -= Math.PI / 36;
                            this.����������_�������.�����������[0] -= Math.PI / 36;
                            this.����������_�������.�����������[1] -= Math.PI / 36;
                            break;

                        case ������_������������.������_�����:
                            this.����������_�������.�����������[1] -= Math.PI / 72;
                            break;

                        case ������_������������.������_�����:
                            this.����������_�������.�����������[0] -= Math.PI / 72;
                            break;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.A) || (e.KeyCode == Keys.MButton))
                {
                    switch (this.����������_�������.������)
                    {
                        case ������_������������.���:
                            this.����������_�������.����������� += Math.PI / 36;
                            this.����������_�������.�����������[0] += Math.PI / 36;
                            this.����������_�������.�����������[1] += Math.PI / 36;
                            break;

                        case ������_������������.������_�����:
                            this.����������_�������.�����������[1] += Math.PI / 72;
                            break;

                        case ������_������������.������_�����:
                            this.����������_�������.�����������[0] += Math.PI / 72;
                            break;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Next) || (e.KeyCode == Keys.C && !e.Control))
                {
                    if ((this.����������_�������.������ == ������_������������.������_�����) || (this.����������_�������.������ == ������_������������.���))
                    {
                        this.����������_�������.������[0] -= 0.5;
                        if (this.����������_�������.������[0] < 0.0)
                        {
                            this.����������_�������.������[0] = 0.0;
                        }
                    }
                    if ((this.����������_�������.������ == ������_������������.������_�����) || (this.����������_�������.������ == ������_������������.���))
                    {
                        this.����������_�������.������[1] -= 0.5;
                        if (this.����������_�������.������[1] < 0.0)
                        {
                            this.����������_�������.������[1] = 0.0;
                        }
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Prior) || (e.KeyCode == Keys.D && !e.Control))
                {
                    if ((this.����������_�������.������ == ������_������������.������_�����) || (this.����������_�������.������ == ������_������������.���))
                    {
                        this.����������_�������.������[0] += 0.5;
                    }
                    if ((this.����������_�������.������ == ������_������������.������_�����) || (this.����������_�������.������ == ������_������������.���))
                    {
                        this.����������_�������.������[1] += 0.5;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.C) && e.Control)
                {
                    if ((this.����������_�������.������ == ������_������������.������_�����) || (this.����������_�������.������ == ������_������������.���))
                    {
                        this.����������_�������.������[0] -= 0.1;
                        if (this.����������_�������.������[0] < 0.0)
                        {
                            this.����������_�������.������[0] = 0.0;
                        }
                    }
                    if ((this.����������_�������.������ == ������_������������.������_�����) || (this.����������_�������.������ == ������_������������.���))
                    {
                        this.����������_�������.������[1] -= 0.1;
                        if (this.����������_�������.������[1] < 0.0)
                        {
                            this.����������_�������.������[1] = 0.0;
                        }
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.D) && e.Control)
                {
                    if ((this.����������_�������.������ == ������_������������.������_�����) || (this.����������_�������.������ == ������_������������.���))
                    {
                        this.����������_�������.������[0] += 0.1;
                    }
                    if ((this.����������_�������.������ == ������_������������.������_�����) || (this.����������_�������.������ == ������_������������.���))
                    {
                        this.����������_�������.������[1] += 0.1;
                    }
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Escape) && (this.����������_�������.������ != ������_������������.���))
                {
                    this.����������_�������.������ = ������_������������.���;
                }
            }
            if (e.KeyCode == Keys.Tab)
            {
                MyDirect3D.���_������ = !MyDirect3D.���_������;

            }
            if (e.KeyCode == Keys.F10)
            {
                var now = DateTime.Now;
                var path = Application.StartupPath + @"\Screenshots\";
                var screenshot = string.Format(@"{0}\Transedit {1:00}-{2:00}-{3} {4:00}-{5:00}-{6:00}-{7:000}.jpg", path, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond);
                var surface = MyDirect3D.device.GetRenderTarget(0);
                Surface.ToFile(surface, screenshot, ImageFileFormat.Jpg);
                surface.Dispose();
            }
            if (e.KeyCode == Keys.U && e.Control)
            {
                this.UndoAction();
            }
            if (e.KeyCode == Keys.S && e.Control && e.Shift)
            {
                this.SaveAs_Item_Click(sender, e);
            }
            if ((this.����������_��������� != null) && (e.KeyCode == Keys.Escape))
            {
                this.ClearPendingAction();
                this.����������_��������� = null;
                this.EnableControls(true);
                return;
            }
            if (this.����������_������_����������_������� != null)
            {
                if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.X))
                {
                    this.����������_������_����������_�������.���������.���������� = Math.Round(this.����������_������_����������_�������.���������.���������� + 0.2, 1);
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.S))
                {
                    this.����������_������_����������_�������.���������.���������� = Math.Round(this.����������_������_����������_�������.���������.���������� - 0.2, 1);
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.D))
                {
                    this.����������_������_����������_�������.���������.������ += 0.2;
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.C))
                {
                    this.����������_������_����������_�������.���������.������ = Math.Max(this.����������_������_����������_�������.���������.������ - 0.2, 0);
                    this.process_mouse(this.mouse_args, false);
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.����������_������_����������_������� = null;
                    this.EnableControls(true);
                    return;
                }
            }
            if ((this.����������_�������_����������_������� != null) && (e.KeyCode == Keys.Escape))
            {
                this.����������_�������_����������_������� = null;
                this.EnableControls(true);
                return;
            }
            if (this.����������_�������� != null)
            {
                if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.X))
                {
                    this.����������_��������.���������.���������� += 0.2;
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.S))
                {
                    this.����������_��������.���������.���������� -= 0.2;
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Up) || (e.KeyCode == Keys.D))
                {
                    this.����������_��������.���������.������ += 0.2;
                    this.process_mouse(this.mouse_args, false);
                }
                if ((e.KeyCode == Keys.Down) || (e.KeyCode == Keys.C))
                {
                    this.����������_��������.���������.������ = Math.Max(this.����������_��������.���������.������ - 0.2, 0);
                    this.process_mouse(this.mouse_args, false);
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.����������_�������� = null;
                    this.EnableControls(true);
                    return;
                }
            }
            if ((this.����������_�����������_������ != null) && (e.KeyCode == Keys.Escape))
            {
                this.����������_�����������_������ = null;
                this.EnableControls(true);
                return;
            }
            if (this.����������_������ != null)
            {
                if (e.KeyCode == Keys.D && e.Control)
                {
                    this.����������_������.height0 += 0.1;
                }
                if (e.KeyCode == Keys.C && e.Control)
                {
                    this.����������_������.height0 = Math.Max(this.����������_������.height0 - 0.1, 0.0);
                }
                if ((e.KeyCode == Keys.D && !e.Control) || (e.KeyCode == Keys.Scroll))
                {
                    this.����������_������.height0 += 0.5;
                }
                if ((e.KeyCode == Keys.C && !e.Control) || (e.KeyCode == Keys.Scroll))
                {
                    this.����������_������.height0 = Math.Max(this.����������_������.height0 - 0.5, 0.0);
                }

                if ((e.KeyCode == Keys.Z) || (e.KeyCode == Keys.Left && !e.Control))
                {
                    this.����������_������.angle0 -= Math.PI / 36;
                }
                if ((e.KeyCode == Keys.A) || (e.KeyCode == Keys.Right && !e.Control))
                {
                    this.����������_������.angle0 += Math.PI / 36;
                }
                if (e.KeyCode == Keys.Escape)
                {
                    this.����������_������ = null;
                    this.ClearPendingAction();
                    this.EnableControls(true);
                    return;
                }
                if (����������_������.angle0 > Math.PI)
                {
                    ����������_������.angle0 -= Math.PI * 2;
                }
                else if (����������_������.angle0 < -Math.PI)
                {
                    ����������_������.angle0 += Math.PI * 2;
                }
                UpdateStatusBar();
            }
            if (((e.KeyCode == Keys.Z) && this.Route_Button.Pushed) && ((this.Route_Box.SelectedIndex >= 0) && (this.Route_Runs_Box.SelectedIndex >= 0)))
            {
                Trip ���� = this.���������_����;
                List<Road> list = new List<Road>(����.pathes);
                if (list.Count > 0)
                {
                    while (list[list.Count - 1].���������������.Length == 1)
                    {
                        list.Add(list[list.Count - 1].���������������[0]);
                        if (list[list.Count - 1] == ����.������_��������)
                        {
                            break;
                        }
                    }
                    ����.pathes = list.ToArray();
                    this.�����.cameraPosition.XZPoint = ����.������_��������.�����[1];
                    this.Route_Runs_ToParkIndex_UpDown.Maximum = this.���������_����.pathes.Length;
                    this.���������������������������();
                    this.modified = true;
                }
            }
            if (this.Edit_Button.Pushed)
            {
                if (e.KeyCode == Keys.A)
                {
                    this.���������_��(Math.PI / 36);
                }
                if (e.KeyCode == Keys.Z)
                {
                    this.���������_��(-Math.PI / 36);
                }
                if (e.KeyCode == Keys.Q)
                {
                    this.���������_��(-this.����_��������);
                }
            }

        }

        public void Editor_Form_KeyUp(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = false;
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

        public void Editor_Form_Load(object sender, EventArgs e)
        {
            var KeyState = MyDirectInput.Key_State;
            Directory.SetCurrentDirectory(Application.StartupPath + @"\Data");

            //Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"\Data"));
            // ������ ��������� �������� � ����
            // � ���������� ���� ��������� ������������
            var options = new DeviceOptions
            {

                vSync = true,
                windowed = true,
                windowedX = 1600,
                windowedY = 900,
                vertexProcessingMode = 1
            };
            Directory.SetCurrentDirectory(Application.StartupPath + @"\Data");
            // �������������� ��-������
            if (!MyDirect3D.InitializeWOpt(renderPanel, options))
            {
                MessageBox.Show(Localization.current_.initializewdi, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                base.Close();
            }
            else
            {
                MyGUI.splash_title = Localization.current_.namegame;
                MainForm.in_editor = true;
                //���.changed_time = false;
                Road.�������������� = 5.0;
                this.ApplyLocalization();
                this.RefreshPanelSize(sender, e);
                this.toolBar_ButtonClick(this, new ToolBarButtonClickEventArgs(this.Edit_Button));
                Stop.������������������� = false;
                this.Reset_World();
                this.������ = new �����[1];


            }
        }





        public void EnableControls(bool value)
        {
            var KeyState = MyDirectInput.Key_State;
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

        private void Undo_Item_Click(object sender, EventArgs e)
        {
            this.UndoAction();
        }

        private void Infoo(object sender, EventArgs e)
        {
            //using (FileStream fstream = File.Open("...Readme_Editor.txt"));
            Directory.SetCurrentDirectory(Application.StartupPath);
            Process.Start("notepad.exe", "Readme_Editor.txt");
        }

        private void Find_MinRadius_Item_Click(object sender, EventArgs e)
        {
            double num = 0.0;
            Road road = null;
            foreach (Road road2 in this.���.���������)
            {
                if (road2.������ && ((num == 0.0) || (road2.���������������� < num)))
                {
                    road = road2;
                    num = road2.����������������;
                }
            }
            if (road != null)
            {
                this.�����.cameraPosition.XZPoint = road.�����[0];
                MessageBox.Show(this, string.Format(Localization.current_.min_radius, num.ToString("0.00")), "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                MessageBox.Show(this, Localization.current_.no_curves, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void Narad_Add_Button_Click(object sender, EventArgs e)
        {
            List<Order> list = new List<Order>(this.���������_�������.orders);
            string str = (this.���������_�������.orders.Length + 1).ToString();
            Order item = new Order(new ����(""), this.���������_�������, str, "");
            list.Add(item);
            this.���������_�������.orders = list.ToArray();
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
            Narad_Name_Box.Text = ���������_�����.�����;
            Narad_Name_Box.Modified = false;
            Narad_ChangeName_Button.Enabled = false;
            Narad_Park_label.Enabled = flag;
            Narad_Park_Box.Enabled = flag;
            Transport_label.Enabled = flag;
            RollingStockBox.Enabled = flag;
            Narad_Park_Box.SelectedIndex = new List<����>(���.�����).IndexOf(���������_�����.����);
            Narad_Runs_label.Enabled = flag;
            Narad_Runs_Box.Enabled = flag;
            Narad_Runs_Add_Button.Enabled = flag;
            UpdateNaradControls(flag ? ���������_����� : null);
            RollingStockUpdate(���������_�����);
        }

        private void Narad_ChangeName_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Narad_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            this.���������_�������.orders[selectedIndex].����� = this.Narad_Name_Box.Text;
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
            if ((this.Narad_Park_Box.SelectedIndex >= 0) && (this.Narad_Park_Box.SelectedIndex < this.���.�����.Length))
            {
                this.���������_�����.���� = this.���.�����[this.Narad_Park_Box.SelectedIndex];
            }
        }

        private void Narad_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Narad_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            List<Order> list = new List<Order>(this.���������_�������.orders);
            list.RemoveAt(selectedIndex);
            this.���������_�������.orders = list.ToArray();
            this.Narad_Box.Items.RemoveAt(selectedIndex);
            this.Narad_Box_SelectedIndexChanged(null, new EventArgs());
            this.modified = true;
        }

        private void Narad_Runs_Add_Button_Click(object sender, EventArgs e)
        {
            List<Trip> list = new List<Trip>(this.���������_�����.�����);
            Trip item = new Trip();
            list.Add(item);
            this.���������_�����.����� = list.ToArray();
            int length = this.���������_�����.�����.Length;
            this.Narad_Runs_Box.Items.Add("���� " + length.ToString());
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
            int num2 = (flag && (this.���������_�������.AllTrips.Count > 0)) ? 0 : -1;
            if (flag)
            {
                for (int i = 0; i < this.���������_�������.trips.Count; i++)
                {
                    int num5 = i + 1;
                    this.Narad_Runs_Run_Box.Items.Add("���� " + num5.ToString());
                    if ((selectedIndex >= 0) && (this.���������_�������.trips[i].pathes == this.���������_�����.�����[selectedIndex].pathes))
                    {
                        num2 = i;
                    }
                }
                for (int j = 0; j < this.���������_�������.parkTrips.Count; j++)
                {
                    int num6 = j + 1;
                    this.Narad_Runs_Run_Box.Items.Add("�������� ���� " + num6.ToString());
                    if ((selectedIndex >= 0) && (this.���������_�������.parkTrips[j].pathes == this.���������_�����.�����[selectedIndex].pathes))
                    {
                        num2 = j + this.���������_�������.trips.Count;
                    }
                }
            }
            this.Narad_Runs_Run_Box.SelectedIndex = num2;
            this.Narad_Runs_Run_Box.SelectedIndexChanged += new EventHandler(this.Narad_Runs_Run_Box_SelectedIndexChanged);
            this.Narad_Runs_Time1_label.Enabled = flag;
            this.Narad_Runs_Time1_Box.Enabled = flag;
            if (flag)
            {
                this.Narad_Runs_Time1_Box.Time_Seconds = (int)this.���������_�����.�����[selectedIndex].�����_�����������;
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
            if (selectedIndex < 0)
                return;
            List<Trip> list = new List<Trip>(this.���������_�����.�����);
            list.RemoveAt(selectedIndex);
            this.���������_�����.����� = list.ToArray();
            this.Narad_Runs_Box.Items.RemoveAt(selectedIndex);
            this.Narad_Runs_Box_SelectedIndexChanged(null, new EventArgs());
            this.modified = true;
        }

        private void Narad_Runs_Run_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Narad_Runs_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            this.���������_�����.�����[selectedIndex].pathes = this.���������_�������.AllTrips[this.Narad_Runs_Run_Box.SelectedIndex].pathes;
            this.Narad_Runs_Time1_Box_TimeChanged(sender, e);
        }

        private void Narad_Runs_Time1_Box_TimeChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Narad_Runs_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            this.���������_�����.�����[selectedIndex].�����_����������� = this.Narad_Runs_Time1_Box.Time_Seconds;
            if (this.���������_�����.�����[selectedIndex].�����_����������� < 10800.0)
            {
                Trip ����1 = this.���������_�����.�����[selectedIndex];
                ����1.�����_����������� += 86400.0;
            }
            double num2 = 0.0;
            foreach (Trip ���� in this.���������_�������.AllTrips)
            {
                if (����.pathes == this.���������_�����.�����[selectedIndex].pathes)
                {
                    num2 = ����.�����_��������;
                    break;
                }
            }
            this.���������_�����.�����[selectedIndex].�����_�������� = this.���������_�����.�����[selectedIndex].�����_����������� + num2;
            this.Narad_Runs_Time2_Box.Time_Seconds = (int)this.���������_�����.�����[selectedIndex].�����_��������;
        }

        private void New_Item_Click(object sender, EventArgs e)
        {
            if (this.Set_New_File(this.saveFileDialog))
            {
                this.Reset_World();
            }
        }

        private void Open_Item_Click(object sender, EventArgs e)
        {
            if (this.Set_New_File(this.openFileDialog))
            {
                this.��� = new World();
                this.����� = new �����();
                this.������ = new �����[] { this.����� };
                this.���� = new Game();
                this.����.��� = this.���;
                this.����.������ = this.������;
                this.���.��������������(this.filename);
                this.���.Create_Meshes();
                this.UpdatePanels();
                this.����_�������� = 0.0;
            }
        }

        private void Reset_World()
        {
            this.��� = new World();
            this.���.Create_Meshes();
            this.����� = new �����();
            this.������ = new �����[] { this.����� };
            this.���� = new Game();
            this.����.��� = this.���;
            this.����.������ = this.������;
            this.����_�������� = 0.0;
            this.UpdatePanels();
        }



        private void panel_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor_x_Status.Text = "";
            this.Cursor_y_Status.Text = "";
        }

        public void panel_MouseMove(object sender, MouseEventArgs e)
        {
            this.mouse_args = e;
            if (this.dragging && !MyDirect3D.���_������)
            {
                DoublePoint point = new DoublePoint((double)(-e.X + this.drag_point.X), (double)(e.Y - this.drag_point.Y));
                point = (DoublePoint)(point / MyDirect3D.�������);
                this.�����.cameraPosition.XZPoint -= point;
                this.drag_point = new Point(e.X, e.Y);
                ������������������ = true;
                //MyDirect3D.Camera_Position;
                //MyDirect3D.Camera_Rotation;
            }
            if (this.dragging && MyDirect3D.���_������)
            {
                DoublePoint point = new DoublePoint((double)(-e.X + this.drag_point.X), (double)(e.Y - this.drag_point.Y));
                point = (DoublePoint)(point / MyDirect3D.�������);
                this.�����.cameraPosition.XZPoint += point;
                this.drag_point = new Point(e.X, e.Y);
            }
            this.process_mouse(e, false);
            this.Cursor_x_Status.Text = "x: " + ((this.cursor_pos.x)).ToString("0.000") + " �";
            this.Cursor_y_Status.Text = "y: " + ((this.cursor_pos.y)).ToString("0.000") + " �";
            //this.�����.cameraRotation.Add(drag_point);
            //����.������[�����].cameraPosition = new Double3DPoint(0.0, 2.0, 0.0);
            //����.������[].cameraRotation = new DoublePoint(0.0, -0.1);
            MyDirect3D.SetViewport(1);
            MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0xb4ff, 1f, 0);
            //�����.cameraPosition.Add(ref �����.cameraPositionChange);
            //�����.cameraPositionChange.Divide(3.0);
            this.�����.cameraRotation = drag_point;
            �����.cameraRotation.Add(ref �����.cameraRotationChange);
            this.�����.cameraRotation.CopyFromAngle(e.X - this.drag_point.X);
            �����.cameraRotationChange.Divide(1600.0);
            this.�����.cameraRotation.CopyFromAngle(e.Y - this.drag_point.Y);
            this.�����.cameraRotationChange.Divide(900.0);
            if (Math.Abs(�����.cameraRotation.x) > Math.PI)
                �����.cameraRotation.x -= 800.0 * Math.PI * Math.Sign(�����.cameraRotation.x);
            if (Math.Abs(�����.cameraRotation.y) > (Math.PI / 360.0))
                �����.cameraRotation.y = (Math.PI / 360.0) * Math.Sign(�����.cameraRotation.y);
            MyDirect3D.SetCameraPos(�����.cameraPosition, �����.cameraRotation);
            //
            col = (int)Math.Floor(�����.cameraPosition.x / (double)Ground.grid_size);
            row = (int)Math.Floor(�����.cameraPosition.z / (double)Ground.grid_size);
            if (((MyDirect3D.device.Viewport.X + MyDirect3D.device.Viewport.Width) == MyDirect3D.Window_Width) &&
                (MyDirect3D.device.Viewport.Y == 1))// continue;
            {
                Common.MyGUI.default_font.DrawString(null, ConvertTime.TimeFromSeconds(���.time % 86400.0), MyDirect3D.Window_Width - 105/*0x69*/, 15, Color.Black);
                //                    MyGUI.default_font.DrawString(null, "�����������".PadLeft(27), MyDirect3D.Window_Width - 398, 15, Color.Black);
            }
        }

        public void rotate_MouseMove(object sender, MouseEventArgs j)
        {
            this.mouse_args = j;
            if (this.dragging && !MyDirect3D.���_������)
            {
                this.dragging = true;
                DoublePoint point = new DoublePoint((double)(-j.X + this.drag_point.X), (double)(j.Y - this.drag_point.Y));
                point = (DoublePoint)(point / MyDirect3D.�������);
                this.�����.cameraPosition.XYPoint -= point;
                this.�����.cameraRotation.Add(point);
                this.drag_point = new Point(j.X, j.Y);
                if (Math.Abs(������[0].cameraRotation.x) > Math.PI)
                    ������[0].cameraRotation.x -= 2.0 * Math.PI * Math.Sign(������[0].cameraRotation.x);
                if (Math.Abs(������[0].cameraRotation.y) > (Math.PI / 2.0))
                    ������[0].cameraRotation.y = (Math.PI / 2.0) * Math.Sign(������[0].cameraRotation.y);
            }
            this.process_mouse(j, true);
        }

        public void panel_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouse_args = e;
            if (e.Button == MouseButtons.Right)
            {
                this.dragging = false;
            }
        }

        public void panel_OnMouseWheel(object sender, MouseEventArgs e)
        {

            this.mouse_args = e;
            var MouseState = MyDirectInput.Mouse_State;
            var KeyState = MyDirectInput.Key_State;
            var JStatesArray = MyDirectInput.Joystick_States;
            var FJStatesArray = MyDirectInput.Joystick_FilteredStates;
            var joystickDevices = MyDirectInput.JoystickDevices;
            var deviceGuids = MyDirectInput.DeviceGuids;
            //            const int num = 0x400;
            //            byte[] mouseButtons = state.GetMouseButtons();
            bool[] mouseButtons = MouseState.GetButtons();
            int x = MouseState.X;
            int y = MouseState.Y;
            int z = MouseState.Z;
            if ((e.Delta > 0) && !MyDirect3D.���_������)
            {
                DoublePoint point = �����.cameraPosition.YPoint;
                this.�����.cameraPosition.XYPoint = point;
                �����.cameraPositionChange.y += 0.001 * z;
                /*MyDirect3D.������� += 0.001 * z;
                            if (MyDirect3D.������� <= 2.5) MyDirect3D.������� = 2.5;
//                            if (mouseButtons[0] != 0)
                            if (mouseButtons[0])
                            {
                                DoublePoint point = new DoublePoint(�����.cameraPositionChange.y);
                                point.y -= 0.01 * y;
                                �����.cameraPositionChange.x = point.x;
                                �����.cameraPositionChange.z = point.y;
                            }
                            DoublePoint point5 = �����.�����������������.position - �����.cameraPosition.XYPoint;
                            if (�����.����������������� != null)
                    {

                    }
                    int current_joystick = -1;
                    for (int k = 0; k < joystickDevices.Length; k++)
                    {
                        if (�����.inputGuid == deviceGuids[k])
                        {
                            current_joystick = k;
                            break;
                        }
                    }
                    if (current_joystick == -1)
                    {
                        if (point5.Modulus > 200.0)
                        {
                            �����.�����������������.���������� = ����������.��������������;
                            �����.����������������� = null;
                            �����.�������������� = null;
                        }
                    }*/

            }
        }

        public void panel_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouse_args = e;
            if (e.Button == MouseButtons.Right)
            {
                this.dragging = true;
                this.drag_point = new Point(e.X, e.Y);
            }
            this.process_mouse(e, true);
        }

        public void CameraHeight()
        {

        }

        public void Render()
        {
            if (MyDirect3D.device == null) return;
            if (MainForm.in_editor) goto Label_new;
            if (MyDirect3D._newDevice.IsDeviceLost) return;
            MyDirect3D._newDevice.BeginScene();
            //            MyDirect3D.device.BeginScene();
            MyDirect3D.ResetViewports(������.Length);
            MyDirect3D.SetViewport(-1);
            MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
            if (!�������)
            {
                menu.Draw();
                MyDirect3D._newDevice.EndScene();
                //                MyDirect3D.device.EndScene();
                //                MyDirect3D.device.Present();
                return;
            }
        Label_new:
            for (var i = 0; i < ������.Length; i++)
            {
                MyDirect3D.SetViewport(i);
                MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0xb4ff, 1f, 0);
                //
                ������[i].cameraPosition.Add(ref ������[i].cameraPositionChange);
                ������[i].cameraPositionChange.Divide(3.0);
                ������[i].cameraRotation.Add(ref ������[i].cameraRotationChange);
                ������[i].cameraRotationChange.Divide(3.0);
                //� ����� ������ ����������? ����������� �������� ������
                if (Math.Abs(������[i].cameraRotation.x) > Math.PI)
                    ������[i].cameraRotation.x -= 2.0 * Math.PI * Math.Sign(������[i].cameraRotation.x);
                if (Math.Abs(������[i].cameraRotation.y) > (Math.PI / 2.0))
                    ������[i].cameraRotation.y = (Math.PI / 2.0) * Math.Sign(������[i].cameraRotation.y);
                //
                MyDirect3D.SetCameraPos(������[i].cameraPosition, ������[i].cameraRotation);
                //
                col = (int)Math.Floor(������[i].cameraPosition.x / (double)Ground.grid_size);
                row = (int)Math.Floor(������[i].cameraPosition.z / (double)Ground.grid_size);
                //
                MyDirect3D.ComputeFrustum();
                ���.RenderMeshes2();
                ���.RenderMeshes();
                MeshObject.RenderList();
                MyDirect3D.Alpha = true;
                ���.RenderMeshesA();
                MeshObject.RenderListA();
                MyDirect3D.Alpha = false;
                if (������[i].����������������� != null)
                {
                    var _transport = (Transport)������[i].�����������������;
                    var speed_str = (_transport.�������� * 3.6).ToString("###0.00");
                    var control_str = "";
                    if (_transport.����������.��������������)
                    {
                        control_str = _transport.����������.������ ? Localization.current_.ctrl_s : Localization.current_.ctrl_a;
                    }
                    else
                    {
                        control_str = _transport.����������.������ ? Localization.current_.ctrl_m : "-";
                    }
                    if (MainForm.debug)
                    {
                        var str111 = "\nCS: " + ((_transport.currentStop != null) ? _transport.currentStop.�������� : "")
                            + "\nNS: " + ((_transport.nextStop != null) ? _transport.nextStop.�������� : "")
                            + "\nSI: " + _transport.stopIndex
                            + "\n\nX: " + _transport.����������3D.x.ToString("#0.0")
                            + "\nY: " + _transport.����������3D.y.ToString("#0.0")
                            + "\nZ: " + _transport.����������3D.z.ToString("#0.0")
                            + "\nrY: " + (_transport.direction * 180.0 / Math.PI).ToString("#0.0")
                            + "\nrZ: " + (_transport.�����������Y * 180.0 / Math.PI).ToString("#0.0");
                        Common.MyGUI.default_font.DrawString(null, str111, (int)(420 + MyDirect3D.device.Viewport.X), (int)(15 + MyDirect3D.device.Viewport.Y), Color.Black);
                    }
                    if (_transport is �������)//(������[i].����������������� is �������)
                    {
                        var ������� = (�������)_transport;//������[i].�����������������;
                        var str = "-";
                        if (�������.�������_���������� is �������_����������.����_�������)
                        {
                            var �������2 = (�������_����������.����_�������)�������.�������_����������;
                            switch (�������2.�������_�����������)
                            {
                                case -5:
                                    str = "��";
                                    break;

                                case -4:
                                    str = "�4";
                                    break;

                                case -3:
                                    str = "�3";
                                    break;

                                case -2:
                                    str = "�2";
                                    break;

                                case -1:
                                    str = "�1";
                                    break;

                                case 0:
                                    str = "0";
                                    break;

                                case 1:
                                    str = "�";
                                    break;

                                case 2:
                                    str = "�1";
                                    break;

                                case 3:
                                    str = "�2";
                                    break;

                                case 4:
                                    str = "�3";
                                    break;
                            }
                            var str2 = (�������2.�������_��������� == 1) ? Localization.current_.forward : (�������2.�������_��������� == -1) ? Localization.current_.back : "0";
                            str = str + "\n" + Localization.current_.reverse + ": " + str2;
                        }
                        str = str + "\n" + ((�������.�����������.������) ? Localization.current_.tk_on : Localization.current_.tk_off)
                             + "\n" + Localization.current_.parking_brake + " " + (�������.stand_brake ? Localization.current_.enable : Localization.current_.disable);
                        var str5 = �������.�������.number;
                        if (�������.�_����)
                        {
                            str5 = str5 + " (" + Localization.current_.route_in_park + ")";
                        }
                        if (�������.����� != null)
                        {
                            //                            var str15 = str5;
                            str5 = str5 + "\n" + Localization.current_.order + ": " + �������.�����.�������.number + "/" + �������.�����.�����;
                            if (�������.���� != null)
                            {
                                if (���.time < �������.����.�����_�����������)
                                {
                                    str5 = str5 + "\n" + Localization.current_.departure_time + ": " + �������.����.str_�����_�����������;
                                }
                                str5 = str5 + "\n" + Localization.current_.arrival_time + ": " + �������.����.str_�����_��������;
                                if (((�������.����_index < (�������.����.pathes.Length - 1)) && (�������.��������_���.�������_�����.���������_������.Length > 1)) && ((�������.����_index > 0) || (�������.��������_���.�������_����� == �������.����.pathes[0])))
                                {
                                    var ������ = �������.����.pathes[�������.����_index + 1];
                                    var str6 = Localization.current_.nr_pryamo;
                                    if (������.������)
                                    {
                                        if (������.���������������0 > 0.0)
                                        {
                                            str6 = Localization.current_.nr_right;
                                        }
                                        else if (������.���������������0 < 0.0)
                                        {
                                            str6 = Localization.current_.nr_left;
                                        }
                                    }
                                    str5 = str5 + "\n" + Localization.current_.nr + ": " + str6;
                                    str5 = str5 + "\nNS: " + ((_transport.nextStop != null) ? _transport.nextStop.�������� : "");
                                }
                            }
                        }
                        Common.MyGUI.default_font.DrawString(null, Localization.current_.tram_control + ": " + control_str + "\n" + Localization.current_.ctrl_pos + ": " + str + "\n" + Localization.current_.speed + ": " + speed_str + " " + Localization.current_.speed_km + "\n" + Localization.current_.route + ": " + str5, (int)(15 + MyDirect3D.device.Viewport.X), (int)(15 + MyDirect3D.device.Viewport.Y), Color.Black);
                    }
                    if (_transport is ����������)// (������[i].����������������� is ����������)
                    {
                        var ���������� = (����������)_transport;//������[i].�����������������;
                        var str7 = "-";
                        var str8 = "���������� ���";
                        if (����������.�������_���������� is �������_����������.����_����������)
                        {
                            str8 = Localization.current_.trol_control;
                            str7 = "\n" + Localization.current_.ctrl_pos + ": ";
                            var ����������2 = (�������_����������.����_����������)����������.�������_����������;
                            switch (����������2.�������_�����������)
                            {
                                case -2:
                                    str7 = str7 + "�2";
                                    break;

                                case -1:
                                    str7 = str7 + "�1";
                                    break;

                                case 0:
                                    str7 = str7 + "0";
                                    break;

                                case 1:
                                    str7 = str7 + "�";
                                    break;

                                case 2:
                                    str7 = str7 + "�1";
                                    break;

                                case 3:
                                    str7 = str7 + "�2";
                                    break;

                                case 4:
                                    str7 = str7 + "�3";
                                    break;
                            }
                            str7 = str7 + "\n" + Localization.current_.air_brake + ": " + ((����������2.��������������_������ * 100.0)).ToString("0") + "%";
                            var str9 = (����������2.�������_��������� == 1) ? Localization.current_.forward : (����������2.�������_��������� == -1) ? Localization.current_.back : "0";
                            str7 = str7 + "\n" + Localization.current_.reverse + ": " + str9;
                            str7 = str7 + "\n" + ((����������.������_�������) ? Localization.current_.st_on : Localization.current_.st_off);
                            str7 = str7 + "\n" + Localization.current_.trol + " " + ((����������.�������) ? Localization.current_.enable : Localization.current_.disable);
                            if (����������.�� != null)
                            {
                                str7 = str7 + "\n" + Localization.current_.ax + " " + (����������.��.������� ? Localization.current_.enable : Localization.current_.disable) + "\n" + Localization.current_.ax_power + ": " + (����������.��.�������_������� / ����������.��.������_�������).ToString("##0%");
                            }
                        }
                        else if (����������.�������_���������� is �������_����������.��_����)
                        {
                            str8 = Localization.current_.bus_control;
                            var ���� = (�������_����������.��_����)����������.�������_����������;
                            str7 = (("\n" + Localization.current_.gmod + ": " + ����.�������_�����) + "\n" + Localization.current_.cur_pos + ": " + ����.�������_��������) + "\n" + Localization.current_.pedal_pos + ": ";
                            if (����.���������_������� > 0.0)
                            {
                                str7 = str7 + Localization.current_.gas + " ";
                            }
                            if (����.���������_������� < 0.0)
                            {
                                str7 = str7 + Localization.current_.brake + " ";
                            }
                            str7 = str7 + ((Math.Abs(����.���������_�������) * 100.0)).ToString("0") + "%"
                                + "\n" + Localization.current_.engine + " " + (����������.������� ? Localization.current_.enable : Localization.current_.disable);
                        }
                        str7 = str7 + "\n" + Localization.current_.parking_brake + " " + (����������.stand_brake ? Localization.current_.enable : Localization.current_.disable);
                        if (����������.����������� > 0.0)
                        {
                            str7 = str7 + "\n" + Localization.current_.sterling + ": " + (((����������.����������� * 180.0) / 3.1415926535897931)).ToString("0") + "\x00b0 " + Localization.current_.ster_r;
                        }
                        else if (����������.����������� < 0.0)
                        {
                            str7 = str7 + "\n" + Localization.current_.sterling + ": " + (((-����������.����������� * 180.0) / 3.1415926535897931)).ToString("0") + "\x00b0 " + Localization.current_.ster_l;
                        }
                        else
                        {
                            str7 = str7 + "\n" + Localization.current_.sterling + ": " + Localization.current_.nr_pryamo;
                        }
                        var str12 = ����������.�������.number;
                        if (����������.�_����)
                        {
                            str12 = str12 + " (" + Localization.current_.route_in_park + ")";
                        }
                        if (����������.����� != null)
                        {
                            var str16 = str12;
                            str12 = str16 + "\n" + Localization.current_.order + ": " + ����������.�����.�������.number + "/" + ����������.�����.�����;
                            if (����������.���� != null)
                            {
                                if (���.time < ����������.����.�����_�����������)
                                {
                                    str12 = str12 + "\n" + Localization.current_.departure_time + ": " + ����������.����.str_�����_�����������;
                                }
                                str12 = str12 + "\n" + Localization.current_.arrival_time + ": " + ����������.����.str_�����_��������;
                                if ((((����������.����_index < (����������.����.pathes.Length - 1)) && (����������.���������.������ != null)) && (����������.���������.������.���������������.Length > 1)) && ((����������.����_index > 0) || (����������.���������.������ == ����������.����.pathes[0])))
                                {
                                    var ������2 = ����������.����.pathes[����������.����_index + 1];
                                    var str13 = Localization.current_.nr_pryamo;
                                    if (������2.������)
                                    {
                                        if (������2.���������������0 > 0.0)
                                        {
                                            str13 = Localization.current_.nr_right;
                                        }
                                        else if (������2.���������������0 < 0.0)
                                        {
                                            str13 = Localization.current_.nr_left;
                                        }
                                    }
                                    str12 = str12 + "\n" + Localization.current_.nr + ": " + str13;
                                    str12 = str12 + "\nNS: " + ((_transport.nextStop != null) ? _transport.nextStop.�������� : "");
                                }
                            }
                        }
                        Common.MyGUI.default_font.DrawString(null, str8 + ": " + control_str + str7 + "\n" + Localization.current_.speed + ": " + speed_str + " " + Localization.current_.speed_km + "\n" + Localization.current_.route + ": " + str12, 15 + MyDirect3D.device.Viewport.X, 15 + MyDirect3D.device.Viewport.Y, Color.Black);
                    }
                }
                if (((MyDirect3D.device.Viewport.X + MyDirect3D.device.Viewport.Width) == MyDirect3D.Window_Width) &&
                    (MyDirect3D.device.Viewport.Y == 0))// continue;
                {
                    Common.MyGUI.default_font.DrawString(null, ConvertTime.TimeFromSeconds(���.time % 86400.0), MyDirect3D.Window_Width - 105/*0x69*/, 15, Color.Black);
                    //                    MyGUI.default_font.DrawString(null, "�����������".PadLeft(27), MyDirect3D.Window_Width - 398, 15, Color.Black);
                }
                if (!MainForm.debug) continue;
                var _str = "\ndTmax: " + World.dtmax.ToString("#0.000") + "\nFPS: " + MyDirect3D._newDevice.FPS.ToString("  00")
                    + "\nX: " + MyDirect3D.Camera_Position.x.ToString("#0.0")
                    + "\nY: " + MyDirect3D.Camera_Position.y.ToString("#0.0")
                    + "\nZ: " + MyDirect3D.Camera_Position.z.ToString("#0.0")
                    + "\nrY: " + MyDirect3D.Camera_Rotation.x.ToString("#0.000")
                    + "\nrZ: " + MyDirect3D.Camera_Rotation.y.ToString("#0.000");
                Common.MyGUI.default_font.DrawString(null, _str, new Rectangle(MyDirect3D.Window_Width - 160, 15, 160, 500), DrawTextFormat.Right, Color.Black);
                //                MyGUI.default_font.DrawString(null, _str, MyDirect3D.Window_Width - 160, 15, Color.Black);
            }
            MyDirect3D._newDevice.EndScene();
            //            MyDirect3D.device.EndScene();
            //            MyDirect3D.device.Present();
        }


        public void InGame()
        {
            this._���������PosIndex++;
            if (this._���������PosIndex >= ���.����������.Count)//.Length)
            {
                this._���������PosIndex = 0;
            }
            foreach (var ��������� in ((Transport)���.����������[this._���������PosIndex]).���������_���������)
            {
                if (���������.������ != null)
                {
                    ���������.������.����������������.Remove(���������);
                }
            }
                ((Transport)���.����������[_���������PosIndex]).�����������������(���);
            foreach (var ���������2 in ((Transport)���.����������[_���������PosIndex]).���������_���������)
            {
                if (���������2.������ != null)
                {
                    ���������2.������.����������������.Add(���������2);
                }
            }
            foreach (Transport ��������� in ���.����������)
            {
                if (���������.����������.��������������)
                {
                    ���������.����������������������(���);
                }
            }
            var MouseState = MyDirectInput.Mouse_State;
            var KeyState = MyDirectInput.Key_State;
            var JStatesArray = MyDirectInput.Joystick_States;
            var FJStatesArray = MyDirectInput.Joystick_FilteredStates;
            var joystickDevices = MyDirectInput.JoystickDevices;
            var deviceGuids = MyDirectInput.DeviceGuids;
            //            const int num = 0x400;
            //            byte[] mouseButtons = state.GetMouseButtons();
            bool[] mouseButtons = MouseState.GetButtons();
            int x = MouseState.X;
            int y = MouseState.Y;
            int z = MouseState.Z;
            if (MyDirectInput.alt_f4) return;

            //var changed = true;

            if (KeyState[Key.Escape])
            {
                ������� = !�������;
            }
            for (var i = 0; i < joystickDevices.Length; i++)
            {
                if (FJStatesArray[i][9])
                {
                    ������� = !�������;
                }
            }
            if (�������)
            {
                foreach (var ����� in ������)
                {

                    int current_joystick = -1;
                    for (int k = 0; k < joystickDevices.Length; k++)
                    {

                    }
                    if (current_joystick == -1)//(�����.inputGuid == MyDirectInput.Keyboard_Device.Information.InstanceGuid)//SystemGuid.Keyboard)
                    { }
                    if (!MyDirect3D.���_������)
                    {
                        //                            if (mouseButtons[0] == 0)
                        if (!mouseButtons[0])
                        {
                            �����.cameraRotationChange.x -= 0.001 * x;
                            �����.cameraRotationChange.y -= 0.001 * y;
                        }
                        else
                        {
                            DoublePoint point = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z) / new DoublePoint(�����.cameraRotation.x);
                            point.x -= 0.01 * y;
                            point.y -= 0.01 * x;
                            �����.cameraPositionChange.x = (point * new DoublePoint(�����.cameraRotation.x)).x;
                            �����.cameraPositionChange.z = (point * new DoublePoint(�����.cameraRotation.x)).y;
                        }
                        �����.cameraPositionChange.y += 0.001 * z;
                    }
                    else
                    {
                        MyDirect3D.������� += 0.001 * z;
                        if (MyDirect3D.������� <= 2.5) MyDirect3D.������� = 2.5;
                        //                            if (mouseButtons[0] != 0)
                        if (mouseButtons[0])
                        {
                            DoublePoint point = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z);
                            point.x += 0.01 * x;
                            point.y -= 0.01 * y;
                            �����.cameraPositionChange.x = point.x;
                            �����.cameraPositionChange.z = point.y;
                        }
                    }
                    this._lastMouseButtons = mouseButtons;


                    FilteredJoystickState Current_FJState = FJStatesArray[current_joystick];
                    JoystickState Current_JState = JStatesArray[current_joystick];

                    double num8 = (0.05 * Current_JState.X) / ((double)num);
                    double num9 = (0.02 * Current_JState.Y) / ((double)num);
                    double num10 = (0.05 * Current_JState.Z) / ((double)num);
                    switch (Current_JState.GetPointOfViewControllers()[0])//.GetPointOfView()[0])
                    {
                        case 0:
                            num10 = 0.04;
                            break;

                        case 0x4650:
                            num10 = -0.04;
                            break;

                        default:
                            num10 = 0.0;
                            break;
                    }
                    if (((�����.����������������� != null) && (�����.����������������� is ������������_���������)) && �����.�����������������.����������.������)
                    {
                        if (!Current_FJState[4, false])
                        {
                            int num12 = 6;
                            if (((Transport)�����.�����������������).�������_���������� is �������_����������.����������)
                            {
                                num12 = 10;
                            }
                            if (Current_FJState[num12, false])
                            {
                                �����.cameraRotationChange.x -= num8;
                                �����.cameraRotationChange.y -= num9;
                            }
                        }
                        else
                        {
                            DoublePoint point2 = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z) / new DoublePoint(�����.cameraRotation.x);
                            point2.x -= 10.0 * num9;
                            point2.y -= 10.0 * num8;
                            �����.cameraPositionChange.x = (point2 * new DoublePoint(�����.cameraRotation.x)).x;
                            �����.cameraPositionChange.z = (point2 * new DoublePoint(�����.cameraRotation.x)).y;
                            �����.cameraPositionChange.y += num10;
                        }
                    }
                    else if (!Current_FJState[4, false])
                    {
                        �����.cameraRotationChange.x -= num8;
                        �����.cameraRotationChange.y -= num9;
                    }
                    else
                    {
                        DoublePoint point3 = new DoublePoint(�����.cameraPositionChange.x, �����.cameraPositionChange.z) / new DoublePoint(�����.cameraRotation.x);
                        point3.x -= 10.0 * num9;
                        point3.y -= 10.0 * num8;
                        �����.cameraPositionChange.x = (point3 * new DoublePoint(�����.cameraRotation.x)).x;
                        �����.cameraPositionChange.z = (point3 * new DoublePoint(�����.cameraRotation.x)).y;
                        �����.cameraPositionChange.y += num10;
                    }

                }
            }
            if (KeyState[Key.F1])
            {
                MainForm.debug = !MainForm.debug;
            }
            if (KeyState[Key.F5])
            {
                //MainForm.IsKeyLocked = MainForm.IsKeyLocked;
                //MainForm.IsMnemonic = MainForm.IsMnemonic;
            }
            if (KeyState[Key.F10])
            {
                var now = DateTime.Now;
                var path = Application.StartupPath + @"\Screenshots\";
                var screenshot = string.Format(@"{0}\Transedit {1:00}-{2:00}-{3} {4:00}-{5:00}-{6:00}-{7:000}.jpg", path, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond);
                var surface = MyDirect3D.device.GetRenderTarget(0);
                Surface.ToFile(surface, screenshot, ImageFileFormat.Jpg);
                surface.Dispose();
            }

            /*if (KeyState[Key.F9])
               {
               var now = DateTime.Now;
                var path = Application.StartupPath + @"\Screenshots\";
                var screenshot = string.Format(@"{0}\Transedit {1:00}-{2:00}-{3} {4:00}-{5:00}-{6:00}-{7:000}.jpg", now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond);
                var surface = MyDirect3D.device.GetRenderTarget(0);
                Surface.ToFile(surface, screenshot, ImageFileFormat.Jpg);
                Clipboard.SetImage(Image.FromFile(path));
                // ������ ����������� �� ������� ���������� �������
            Bitmap bmpCopy = new Bitmap(screenshot);
            // �������� ������ Graphics
            using (Graphics g = Graphics.FromImage(bmpCopy))
                // �������������� ������� � �������������� ��������
                // bmp - ������ ��������
                // 0, 0 - ���������� ������ �������� ���� �� ����� ��������
                // copyRect - ������� �� ������� �����������
                // ������� ��������� ��� �����������
                
            // ����� � ����� ������ "����������" �����
            Clipboard.SetImage(bmpCopy);
            }*/


        }


        private void Park_Add_Button_Click(object sender, EventArgs e)
        {
            DoRegisterAction(new AddDepotAction(new ����("����")));
            this.modified = true;
        }

        private void Park_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            Check_All_Park_Boxes();
        }

        private void Check_All_Park_Boxes()
        {
            bool flag = this.Park_Box.SelectedIndex >= 0;
            this.Park_In_Button.Enabled = flag;
            this.Park_Out_Button.Enabled = flag;
            this.Park_Rails_Button.Enabled = flag;
            this.Park_Remove_Button.Enabled = flag;
            this.Park_Name_label.Enabled = flag;
            this.Park_Name_Box.Enabled = flag;
            this.Park_Name_Box.Text = flag ? this.���.�����[this.Park_Box.SelectedIndex].�������� : string.Empty;
            this.Park_Name_Box.Modified = false;
            this.Park_ChangeName_Button.Enabled = false;
            this.���������������������������();
        }

        private void Park_ChangeName_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Park_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            this.���.�����[selectedIndex].�������� = this.Park_Name_Box.Text;
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
            if (selectedIndex < 0)
                return;
            DoRegisterAction(new RemoveDepotAction(selectedIndex));
            this.modified = true;
        }

        private void process_mouse(MouseEventArgs e, bool click)
        {
            DoublePoint point20;
            ��������� nearest_post = this.���.�����_���������_���������(this.cursor_pos);
            if (this.����������_������ == null)
            {
                if ((this.Rail_Button.Pushed && click) && ((e.Button == MouseButtons.Left) && (nearest_post.������ != null)))
                {
                    if (!this.Spline_Select_mode_Box.Checked)
                    {
                        this.DoRegisterAction(new Editor.RemoveRoadAction(nearest_post.������));
                        this.modified = true;
                    }
                    else
                    {
                        this.Splines_Instance_Box.SelectedIndex = this.���.list������.IndexOf(nearest_post.������);
                        time_color = 2.0;
                        this.���������������������������();
                    }
                }
            }
            else
            {
                this.����������_������.������ = this.Rail_Build_Curve_Button.Pushed;
                DoublePoint point = new DoublePoint(20.0, this.����������_������.������ ? 10.0 : 0.0);
                DoublePoint point2 = new DoublePoint(-20.0, this.����������_������.������ ? 10.0 : 0.0);
                DoublePoint point3 = (this.shift) ? this.cursor_pos.RoundPoint : this.cursor_pos;

                if ((this.ctrl && this.����������_������.������) && (this.�������������_������ != ������_������������.���))
                {
                    this.����������_������.�����������������();
                    DoublePoint point41;
                    DoublePoint point40;
                    if (this.�������������_������ == ������_������������.������_�����)
                    {
                        DoublePoint point4 = this.����������_������.�����[0] + new DoublePoint(this.����������_������.�����������[0] + (Math.PI / 2.0));
                        point40 = (this.����������_������.�����[0] - point4);
                        point41 = new DoublePoint((Math.PI + this.����������_������.�����������[1]) - this.����������_������.�����������[0]);
                        point40.Multyply(ref point41);
                        DoublePoint point5 = point4.Add(ref point40);
                        DoublePoint point6 = point3 - this.����������_������.�����[0];
                        point20 = point5.Subtract(ref this.����������_������.�����[0]);

                        DoublePoint ang0 = new DoublePoint(point20.Angle);
                        point6.Divide(ref ang0);
                        point6.y = 0.0;
                        point6.Multyply(ref ang0);
                        point3 = point6.Add(ref this.����������_������.�����[0]);
                    }
                    if (this.�������������_������ == ������_������������.������_�����)
                    {
                        DoublePoint point7 = this.����������_������.�����[1] + new DoublePoint(this.����������_������.�����������[1] + (Math.PI / 2.0));
                        point40 = (this.����������_������.�����[1] - point7);
                        point41 = new DoublePoint((Math.PI + this.����������_������.�����������[0]) - this.����������_������.�����������[1]);
                        point40.Multyply(ref point41);
                        DoublePoint point8 = point7.Add(ref point40);
                        DoublePoint point9 = point3 - this.����������_������.�����[1];
                        DoublePoint point21 = point8.Subtract(ref this.����������_������.�����[1]);

                        DoublePoint ang1 = new DoublePoint(point21.Angle);
                        point9.Divide(ref ang1);
                        point9.y = 0.0;
                        point9.Multyply(ref ang1);
                        point3 = point9.Add(ref this.����������_������.�����[1]);
                    }
                }
                switch (this.�������������_������)
                {
                    case ������_������������.���:
                        this.����������_������.�����[0] = point3;
                        this.����������_������.�����[1] = point3 + point;
                        break;

                    case ������_������������.������_�����:
                        this.����������_������.�����[1] = point3;
                        break;

                    case ������_������������.������_�����:
                        this.����������_������.�����[0] = point3;
                        break;
                }
                bool flag = false;
                bool flag2 = false;
                DoublePoint point22 = this.����������_������.�����[1] - this.����������_������.�����[0];
                if (point22.Modulus > 1.0)
                {
                    Road[] ������Array = this.���.������;
                    if (this.����������_������ is �����)
                    {
                        ������Array = this.���.������;
                    }
                    foreach (Road ������ in ������Array)
                    {
                        if ((this.�������������_������ == ������_������������.���) || (this.�������������_������ == ������_������������.������_�����))
                        {
                            point20 = this.����������_������.�����[0] - ������.�����[1];
                            if (point20.Modulus < 1.0)
                            {
                                flag2 = true;
                                this.����������_������.�����[0] = ������.�����[1];
                                if (this.����������_������.������ || (this.�������������_������ == ������_������������.���))
                                {
                                    this.����������_������.�����������[0] = ������.�����������[1] + Math.PI;
                                    if (!this.����������_������.������)
                                    {
                                        this.����������_������.�����������[1] = this.����������_������.�����������[0] - Math.PI;
                                    }
                                }
                                this.����������_������.������[0] = ������.������[1];
                                this.����������_������.������[0] = ������.������[1];
                                if (this.�������������_������ == ������_������������.���)
                                {
                                    this.����������_������.������[1] = ������.������[1];
                                    this.����������_������.������[1] = ������.������[1];
                                    this.����������_������.�����[1] = this.����������_������.�����[0] + new DoublePoint(this.����������_������.�����������[0]).Multyply(ref point);
                                }
                                break;
                            }
                        }
                        if ((this.�������������_������ == ������_������������.���) || (this.�������������_������ == ������_������������.������_�����))
                        {
                            point20 = this.����������_������.�����[1] - ������.�����[0];
                            if (point20.Modulus < 1.0)
                            {
                                flag = true;
                                this.����������_������.�����[1] = ������.�����[0];
                                if (this.����������_������.������ || (this.�������������_������ == ������_������������.���))
                                {
                                    this.����������_������.�����������[1] = ������.�����������[0] + Math.PI;
                                    if (!this.����������_������.������)
                                    {
                                        this.����������_������.�����������[0] = this.����������_������.�����������[1] - Math.PI;
                                    }
                                }
                                this.����������_������.������[1] = ������.������[0];
                                this.����������_������.������[1] = ������.������[0];
                                if (this.�������������_������ == ������_������������.���)
                                {
                                    this.����������_������.������[0] = ������.������[0];
                                    this.����������_������.������[0] = ������.������[0];
                                    this.����������_������.�����[0] = this.����������_������.�����[1] + new DoublePoint(������.�����������[0]).Multyply(ref point2);
                                }
                                break;
                            }
                        }
                    }
                }
                if (!this.����������_������.������)
                {
                    double num3 = Math.Abs((double)(this.����������_������.������[0] - this.����������_������.������[1])) / 2.0;
                    DoublePoint point001 = new DoublePoint();
                    if (this.�������������_������ == ������_������������.���)
                    {
                        if (!flag2 && !flag)
                        {
                            this.����������_������.�����[1] = this.����������_������.�����[0] + (new DoublePoint(this.����������_������.�����������[0]).Multyply(ref point));
                            this.����������_������.�����������[1] = this.����������_������.�����������[0] + Math.PI;
                            if (this.����������_������.�����������[1] > Math.PI)
                            {
                                this.����������_������.�����������[1] -= Math.PI * 2.0;
                            }
                        }
                    }
                    else if (this.�������������_������ == ������_������������.������_�����)
                    {
                        DoublePoint point10 = this.����������_������.�����[1] - this.����������_������.�����[0];
                        point001.CopyFromAngle(this.����������_������.�����������[0]);
                        point10.Divide(ref point001);
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
                        point10.Multyply(ref point001);
                        this.����������_������.�����[1] = point10.Add(ref this.����������_������.�����[0]);
                    }
                    else if (this.�������������_������ == ������_������������.������_�����)
                    {
                        DoublePoint point11 = this.����������_������.�����[0] - this.����������_������.�����[1];
                        point001.CopyFromAngle(this.����������_������.�����������[1]);
                        point11.Divide(ref point001);
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
                        point11.Multyply(ref point001);
                        this.����������_������.�����[0] = point11.Add(ref this.����������_������.�����[1]);
                    }
                }
                this.����������_������.�����������������������(this.���.���������);
                if (click && (e.Button == MouseButtons.Left))
                {
                    switch (this.�������������_������)
                    {
                        case ������_������������.���:
                            if (!flag2)
                            {
                                if (flag)
                                {
                                    this.�������������_������ = ������_������������.������_�����;
                                }
                                else
                                {
                                    this.�������������_������ = ������_������������.������_�����;
                                }
                            }
                            else
                            {
                                this.�������������_������ = ������_������������.������_�����;
                            }
                            goto Label_0BE2;

                        case ������_������������.������_�����:
                        case ������_������������.������_�����:
                            this.�������������_������ = ������_������������.���;
                            this.����������_������.Color = 0;
                            AddRoadsAction action = null;

                            if ((this.Rail_Build_�������_Button.Pushed) && (this.Rail_Build_�������2_Button.Pushed))
                            {
                                action = new AddRoadsAction(this.����������_������, this.����������_������1);
                            }
                            if ((this.Rail_Build_�������_Button.Pushed) && (this.Rail_Build_�������3_Button.Pushed))
                            {
                                action = new AddRoadsAction(this.����������_������, this.����������_������1, this.����������_������2);
                                this.���.list������.Add(this.����������_������1);
                                this.���.list������.Add(this.����������_������2);
                            }
                            if ((this.Rail_Build_��������_Button.Pushed) && (this.Rail_Build_��������1_Button.Pushed))
                            {
                                action = new AddRoadsAction(this.����������_������, this.����������_��������_������);
                            }
                            if ((this.Rail_Build_��������_Button.Pushed) && (this.Rail_Build_��������2_Button.Pushed))
                            {
                                action = new AddRoadsAction(this.����������_������, this.����������_��������_������, this.����������_��������_������1);
                            }
                            if ((this.Rail_Build_��������_Button.Pushed) && (this.Rail_Build_��������3_Button.Pushed))
                            {
                                action = new AddRoadsAction(this.����������_������, this.����������_��������_������, this.����������_��������_������1, this.����������_��������_������2);
                            }
                            if (action == null)
                                action = new AddRoadsAction(this.����������_������);

                            DoRegisterAction(action);
                            this.modified = true;
                            this.����������_������ = null;
                            this.toolBar_ButtonClick(null, new ToolBarButtonClickEventArgs(null));
                            goto Label_0BE2;
                    }
                }
            }
        Label_0BE2:
            if (this.����������_������� == null)
            {
                if ((this.Troll_lines_Button.Pushed && click) && (e.Button == MouseButtons.Left))
                {
                    if (this.���������_������� != null)
                    {
                        DoRegisterAction(new RemoveWiresAction(this.���������_�������));
                        this.modified = true;
                    }
                    else if (this.���������_������ != null)
                    {
                        DoRegisterAction(new RemoveTramWireAction(this.���������_������));
                        this.modified = true;
                    }

                }
            }
            if (this.����������_������� == null)
            {
                if ((this.Troll_lines_Button.Pushed && click) && (e.Button == MouseButtons.Middle))
                {
                    if (this.��������_������� != null)
                    {
                        DoRegisterAction(new RemoveWiresAction(this.��������_�������));
                        this.modified = true;
                    }
                }
            }
            else
            {
                DoublePoint point12 = new DoublePoint(7.5, 0.0);
                DoublePoint point13 = new DoublePoint(-7.5, 0.0);
                DoublePoint point14 = (this.shift) ? this.cursor_pos.RoundPoint : this.cursor_pos;

                switch (this.����������_�������.������)
                {
                    case ������_������������.���:
                        this.����������_�������.�����[0] = point14;
                        this.����������_�������.�����[1] = point14 + point12;
                        break;

                    case ������_������������.������_�����:
                        this.����������_�������.�����[1] = point14;
                        break;

                    case ������_������������.������_�����:
                        this.����������_�������.�����[0] = point14;
                        break;
                }
                bool flag3 = false;
                bool flag4 = false;
                bool isTramWire = (this.����������_������� is ����������_����������_�������);
                point20 = this.����������_�������.�����[1] - this.����������_�������.�����[0];
                if (point20.Modulus > 1.0)
                {
                    if (!isTramWire)
                    {
                        for (int i = 0; i < (this.���.�����������������.Length - 1); i += 2)
                        {
                            DoublePoint[] pointArray;
                            ����������_������ _������ = this.���.�����������������[i];
                            ����������_������ _������2 = this.���.�����������������[i + 1];
                            if ((this.����������_�������.������ == ������_������������.���) || (this.����������_�������.������ == ������_������������.������_�����))
                            {
                                point20 = this.����������_�������.�����[0] - ((DoublePoint)((_������.����� + _������2.�����) / 2.0));
                                if (point20.Modulus < 1.0)
                                {
                                    flag4 = true;
                                    this.����������_�������.�����[0] = (DoublePoint)((_������.����� + _������2.�����) / 2.0);
                                    pointArray = new DoublePoint[] { _������.�����, _������2.����� };
                                    this.����������_�������.������ = pointArray;
                                    point20 = _������2.����� - _������.�����;
                                    double num5 = point20.Angle - (Math.PI / 2.0);
                                    this.����������_�������.�����������[0] = num5;
                                    this.����������_�������.������[0] = _������.������[1];
                                    if (this.����������_�������.������ == ������_������������.���)
                                    {
                                        this.����������_�������.������[1] = _������.������[1];
                                        point20 = _������.����� - _������2.�����;
                                        if (Math.Abs(����������_������.����������_�����_��������� - point20.Modulus) < 0.001)
                                        {
                                            this.����������_�������.����������� = num5;
                                        }
                                        else
                                        {
                                            this.����������_�������.����������� = (2.0 * num5) - _������.�����������;
                                        }
                                        this.����������_�������.�����������[1] = this.����������_�������.�����������;
                                        this.����������_�������.�����[1] = this.����������_�������.�����[0] + new DoublePoint(this.����������_�������.�����������).Multyply(ref point12);
                                    }
                                    break;
                                }
                            }
                            if ((this.����������_�������.������ == ������_������������.���) || (this.����������_�������.������ == ������_������������.������_�����))
                            {
                                point20 = this.����������_�������.�����[1] - ((DoublePoint)((_������.������ + _������2.������) / 2.0));
                                if (point20.Modulus < 1.0)
                                {
                                    flag3 = true;
                                    this.����������_�������.�����[1] = (DoublePoint)((_������.������ + _������2.������) / 2.0);
                                    pointArray = new DoublePoint[] { _������.������, _������2.������ };
                                    this.����������_�������.����� = pointArray;
                                    point20 = _������2.������ - _������.������;
                                    double num6 = point20.Angle - (Math.PI / 2.0);
                                    this.����������_�������.�����������[1] = num6;
                                    this.����������_�������.������[1] = _������.������[0];
                                    if (this.����������_�������.������ == ������_������������.���)
                                    {
                                        this.����������_�������.������[0] = _������.������[0];
                                        point20 = _������.������ - _������2.������;
                                        if (Math.Abs((double)(����������_������.����������_�����_��������� - point20.Modulus)) < 0.001)
                                        {
                                            this.����������_�������.����������� = num6;
                                        }
                                        else
                                        {
                                            this.����������_�������.����������� = (2.0 * num6) - _������.�����������;
                                        }
                                        this.����������_�������.�����������[0] = this.����������_�������.�����������;
                                        this.����������_�������.�����[0] = this.����������_�������.�����[1] + new DoublePoint(this.����������_�������.�����������).Multyply(ref point13);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < this.���.�����������������2.Length; i++)
                        {
                            ����������_����������_������ _������ = this.���.�����������������2[i];
                            if ((this.����������_�������.������ == ������_������������.���) || (this.����������_�������.������ == ������_������������.������_�����))
                            {
                                point20 = this.����������_�������.�����[0] - _������.�����;
                                if (point20.Modulus < 1.0)
                                {
                                    flag4 = true;
                                    this.����������_�������.�����[0] = _������.�����;
                                    this.����������_�������.������ = new DoublePoint[1] { _������.����� };
                                    if (!((����������_����������_�������)this.����������_�������).flag)
                                    {
                                        this.����������_�������.����������� = _������.�����������;
                                        ((����������_����������_�������)this.����������_�������).flag = true;
                                    }
                                    this.����������_�������.������[0] = _������.������[1];
                                    if (this.����������_�������.������ == ������_������������.���)
                                    {
                                        this.����������_�������.������[1] = _������.������[1];
                                        this.����������_�������.�����������[1] = this.����������_�������.�����������;
                                        this.����������_�������.�����[1] = this.����������_�������.�����[0] + new DoublePoint(this.����������_�������.�����������).Multyply(ref point12);
                                    }
                                    break;
                                }
                            }
                            if ((this.����������_�������.������ == ������_������������.���) || (this.����������_�������.������ == ������_������������.������_�����))
                            {
                                point20 = this.����������_�������.�����[1] - _������.������;
                                if (point20.Modulus < 1.0)
                                {
                                    flag3 = true;
                                    this.����������_�������.�����[1] = _������.������;
                                    this.����������_�������.����� = new DoublePoint[1] { _������.������ };
                                    this.����������_�������.������[1] = _������.������[0];
                                    if (this.����������_�������.������ == ������_������������.���)
                                    {
                                        this.����������_�������.������[0] = _������.������[0];
                                        this.����������_�������.�����������[0] = this.����������_�������.�����������;
                                        this.����������_�������.�����[0] = this.����������_�������.�����[1] + new DoublePoint(this.����������_�������.�����������).Multyply(ref point13);
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
                if (this.����������_�������.������ == ������_������������.���)
                {
                    if (!flag4 && !flag3)
                    {
                        this.����������_�������.�����[1] = this.����������_�������.�����[0] + new DoublePoint(this.����������_�������.�����������).Multyply(ref point12);
                        this.����������_�������.������ = null;
                        this.����������_�������.����� = null;
                        if (isTramWire)
                            ((����������_����������_�������)this.����������_�������).flag = false;
                    }
                }
                else if (this.����������_�������.������ == ������_������������.������_�����)
                {
                    DoublePoint point15 = this.����������_�������.�����[1] - this.����������_�������.�����[0];
                    var point0 = new DoublePoint(this.����������_�������.�����������);
                    point15.Divide(ref point0);
                    if (this.ctrl)
                    {
                        point15.y = 0.0;
                    }
                    if (point15.x < 1.0)
                    {
                        point15.x = 1.0;
                    }
                    point15.Multyply(ref point0);
                    this.����������_�������.�����[1] = point15.Add(ref this.����������_�������.�����[0]);
                    if (!flag3)
                    {
                        this.����������_�������.����� = null;
                    }
                }
                else if (this.����������_�������.������ == ������_������������.������_�����)
                {
                    DoublePoint point16 = this.����������_�������.�����[0] - this.����������_�������.�����[1];
                    var point01 = new DoublePoint(this.����������_�������.����������� + Math.PI);
                    point16.Divide(ref point01);
                    if (this.ctrl)
                    {
                        point16.y = 0.0;
                    }
                    if (point16.x < 1.0)
                    {
                        point16.x = 1.0;
                    }
                    point16.Multyply(ref point01);
                    this.����������_�������.�����[0] = point16.Add(ref this.����������_�������.�����[1]);
                    if (!flag4)
                    {
                        this.����������_�������.������ = null;
                    }
                }
                this.����������_�������.��������();
                if (click && (e.Button == MouseButtons.Left))
                {
                    switch (this.����������_�������.������)
                    {
                        case ������_������������.���:
                            if (!flag4)
                            {
                                if (flag3)
                                {
                                    this.����������_�������.������ = ������_������������.������_�����;
                                }
                                else
                                {
                                    this.����������_�������.������ = ������_������������.������_�����;
                                }
                            }
                            else
                            {
                                this.����������_�������.������ = ������_������������.������_�����;
                            }
                            goto Label_1800;

                        case ������_������������.������_�����:
                        case ������_������������.������_�����:
                            {
                                this.����������_�������.������ = ������_������������.���;
                                this.����������_�������.�������[0].color = 0;
                                if (!isTramWire)
                                {
                                    this.����������_�������.�������[1].color = 0;
                                }
                                if (isTramWire)
                                {
                                    this.����������_�������.�������[0].������������ = false;
                                    foreach (����������_������ _������3 in this.���.�����������������)
                                    {
                                        if (this.����������_�������.�������[0].������������) break;
                                        DoublePoint point17 = _������3.����� - _������3.������;
                                        if ((_������3.������[0] == this.����������_�������.�������[0].������[0]) && (_������3.������[1] == this.����������_�������.�������[0].������[1]))
                                        {
                                            DoublePoint point18 = this.����������_�������.�������[0].������ - _������3.������;
                                            DoublePoint point19 = this.����������_�������.�������[0].����� - _������3.������;
                                            point18.Angle -= point17.Angle;
                                            point19.Angle -= point17.Angle;
                                            if (Math.Sign(point18.y) != Math.Sign(point19.y))
                                            {
                                                double num8 = point18.x + (((point19.x - point18.x) * (0.0 - point18.y)) / (point19.y - point18.y));
                                                if ((num8 > 0.001) && (num8 < (point17.Modulus - 0.001)))
                                                {
                                                    point20 = new DoublePoint(_������3.����������� - this.����������_�������.�������[0].�����������);
                                                    this.����������_�������.�������[0].������������ = (point20.Angle != 0.0);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (����������_������ _������3 in this.���.�����������������2)
                                    {
                                        DoublePoint point17 = _������3.����� - _������3.������;
                                        for (int k = 0; k < 2; k++)
                                        {
                                            if ((_������3.������[0] == this.����������_�������.�������[k].������[0]) && (_������3.������[1] == this.����������_�������.�������[k].������[1]))
                                            {
                                                DoublePoint point18 = this.����������_�������.�������[k].������ - _������3.������;
                                                DoublePoint point19 = this.����������_�������.�������[k].����� - _������3.������;
                                                point18.Angle -= point17.Angle;
                                                point19.Angle -= point17.Angle;
                                                if (Math.Sign(point18.y) != Math.Sign(point19.y))
                                                {
                                                    double num8 = point18.x + (((point18.x - point19.x) * point18.y) / (point19.y - point18.y));//(0.0 - point18.y)) / (point19.y - point18.y));
                                                    if ((num8 > 0.001) && (num8 < (point17.Modulus - 0.001)))
                                                    {
                                                        point20 = new DoublePoint(_������3.����������� - this.����������_�������.�������[k].�����������);
                                                        _������3.������������ = (point20.Angle != 0.0);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    foreach (����������_������ _������3 in this.���.�����������������)
                                    {
                                        DoublePoint point17 = _������3.����� - _������3.������;
                                        for (int m = 0; m < 2; m++)
                                        {
                                            if ((_������3.������[0] == this.����������_�������.�������[m].������[0]) && (_������3.������[1] == this.����������_�������.�������[m].������[1]))
                                            {
                                                DoublePoint point18 = this.����������_�������.�������[m].������ - _������3.������;
                                                DoublePoint point19 = this.����������_�������.�������[m].����� - _������3.������;
                                                point18.Angle -= point17.Angle;
                                                point19.Angle -= point17.Angle;
                                                if (Math.Sign(point18.y) != Math.Sign(point19.y))
                                                {
                                                    double num8 = point18.x + (((point18.x - point19.x) * point18.y) / (point19.y - point18.y));
                                                    if ((num8 > 0.001) && (num8 < (point17.Modulus - 0.001)))
                                                    {
                                                        if (_������3.������ == this.����������_�������.�������[m].������)
                                                        {
                                                            _������3.������������ = true;
                                                            this.����������_�������.�������[m].������������ = true;
                                                        }
                                                        else
                                                        {
                                                            point20 = new DoublePoint(_������3.����������� - this.����������_�������.�������[m].�����������);
                                                            if (point20.Angle < 0.0)
                                                            {
                                                                _������3.������������ = true;
                                                            }
                                                            else
                                                            {
                                                                this.����������_�������.�������[m].������������ = true;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                this.DoPendingAction();
                                this.modified = true;
                                this.����������_������� = null;
                                this.toolBar_ButtonClick(null, new ToolBarButtonClickEventArgs(null));
                                goto Label_1800;
                            }
                    }
                }
            }
        Label_1800:
            UpdateStatusBar();
            if ((this.����������_��������� != null) && (nearest_post.������ != null))
            {
                this.����������_���������.road = nearest_post.������;
                this.����������_���������.distance = nearest_post.����������;
                this.����������_���������.UpdatePosition(this.���);
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.DoPendingAction();
                    this.����������_��������� = null;
                    this.EnableControls(true);
                }
            }
            if ((this.����������_�������_����������_������� != null) && (nearest_post.������ != null))
            {
                this.����������_�������_����������_�������.������ = nearest_post.������;
                this.����������_�������_����������_�������.���������� = nearest_post.����������;
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.����������_�������_����������_������� = null;
                    this.EnableControls(true);
                }
            }
            if ((this.����������_������_����������_������� != null) && (nearest_post.������ != null))
            {
                this.����������_������_����������_�������.road = nearest_post.������;
                this.����������_������_����������_�������.���������.���������� = Math.Round(nearest_post.����������);
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.����������_������_����������_������� = null;
                    this.EnableControls(true);
                }
            }
            if ((this.����������_�������� != null) && (nearest_post.������ != null))
            {
                this.����������_��������.���������.������ = nearest_post.������;
                this.����������_��������.���������.���������� = Math.Round(nearest_post.����������);
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.����������_�������� = null;
                    this.EnableControls(true);
                }
            }
            if ((this.����������_�����������_������ != null) && (nearest_post.������ != null))
            {
                this.����������_�����������_������.������ = nearest_post.������;
                this.����������_�����������_������.���������� = nearest_post.����������;
                if (click && (e.Button == MouseButtons.Left))
                {
                    this.����������_�����������_������ = null;
                    this.EnableControls(true);
                }
                this.���������������������������();
            }
            if ((click && (e.Button == MouseButtons.Left)) && (nearest_post.������ != null))
            {
                if (this.Park_Button.Pushed && (this.Park_Box.SelectedIndex >= 0))
                {
                    ���� ���� = this.���.�����[this.Park_Box.SelectedIndex];
                    if (this.Park_In_Button.Pushed)
                    {
                        ����.����� = nearest_post.������;
                        this.���������������������������();
                        this.modified = true;
                    }
                    if (this.Park_Out_Button.Pushed)
                    {
                        ����.����� = nearest_post.������;
                        this.���������������������������();
                        this.modified = true;
                    }
                    if (this.Park_Rails_Button.Pushed)
                    {
                        int index = -1;
                        for (int n = 0; n < ����.����_�������.Length; n++)
                        {
                            if (����.����_�������[n] == nearest_post.������)
                            {
                                index = n;
                                break;
                            }
                        }
                        List<Road> list2 = new List<Road>(����.����_�������);
                        if (index >= 0)
                        {
                            list2.RemoveAt(index);
                        }
                        else
                        {
                            list2.Add(nearest_post.������);
                        }
                        ����.����_������� = list2.ToArray();
                        this.���������������������������();
                        this.modified = true;
                    }
                }
                if (this.Stops_Button.Pushed && (this.Stops_Box.SelectedIndex >= 0))
                {
                    Stop ��������� = this.���.���������[this.Stops_Box.SelectedIndex];
                    int num15 = -1;
                    for (int num16 = 0; num16 < ���������.���������.Length; num16++)
                    {
                        if (���������.���������[num16] == nearest_post.������)
                        {
                            num15 = num16;
                            break;
                        }
                    }
                    List<Road> list3 = new List<Road>(���������.���������);
                    if (num15 >= 0)
                    {
                        list3.RemoveAt(num15);
                    }
                    else
                    {
                        list3.Add(nearest_post.������);
                    }
                    ���������.��������� = list3.ToArray();
                    this.���������������������������();
                    this.modified = true;
                }
                if ((this.Route_Button.Pushed && (this.Route_Box.SelectedIndex >= 0)) && (this.Route_Runs_Box.SelectedIndex >= 0))
                {
                    Trip ���� = this.���������_����;
                    List<Road> list4 = new List<Road>(����.pathes);
                    if (����.������_�������� == nearest_post.������)
                    {
                        list4.RemoveAt(����.pathes.Length - 1);
                    }
                    else if ((����.pathes.Length == 0) || new List<Road>(����.������_��������.���������������).Contains(nearest_post.������))
                    {
                        list4.Add(nearest_post.������);
                    }
                    ����.pathes = list4.ToArray();
                    this.Route_Runs_ToParkIndex_UpDown.Maximum = this.���������_����.pathes.Length;
                    this.���������������������������();
                    this.modified = true;
                }
            }
            if (����������_������ != null)
            {
                ����������_������.position = cursor_pos;
                if (click && e.Button == MouseButtons.Left)
                {
                    DoPendingAction();
                    ����������_������ = null;
                    EnableControls(true);
                    modified = true;
                }
            }
            else if ((this.Object_Button.Pushed && (this.���.�������.Count > 0)) && (click && e.Button == MouseButtons.Left))
            {
                // ��� �� ����� �� �����. ����� ���������� ���������
                double x;
                double dist = 250.0;
                int arpos = -1;
                for (int i = 0; i < this.���.�������.Count; i++)
                {
                    x = (this.���.�������[i].bounding_sphere.position.XZPoint - this.cursor_pos).Modulus;
                    if (x < dist)
                    {
                        dist = x;
                        arpos = i;
                    }
                }
                if (arpos >= 0)
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
            this.Maschtab.Text = Localization.current_.scale + (MyDirect3D.�������).ToString("0.0");
            this.Ugol.Text = Localization.current_.angle + (180 * this.����_�������� / Math.PI).ToString("0") + "\x00b0";
            if (this.����������_������ != null)
            {
                this.����������_������.�����������������();
                num18 = this.����������_������.�����[0].x / 1000.0;
                this.Coord_x1_Status.Text = "x: " + num18.ToString("0.000") + " ��";
                num18 = this.����������_������.�����[0].y / 1000.0;
                this.Coord_y1_Status.Text = "y: " + num18.ToString("0.000") + " ��";
                num18 = (this.����������_������.�����������[0] * 180.0) / Math.PI;
                this.Angle1_Status.Text = num18.ToString("0") + "\x00b0";
                num18 = this.����������_������.�����[1].x / 1000.0;
                this.Coord_x2_Status.Text = "x: " + num18.ToString("0.000") + " ��";
                num18 = this.����������_������.�����[1].y / 1000.0;
                this.Coord_y2_Status.Text = "y: " + num18.ToString("0.000") + " ��";
                num18 = (this.����������_������.�����������[1] * 180.0) / Math.PI;
                this.Angle2_Status.Text = num18.ToString("0") + "\x00b0";
                this.Length_Status.Text = "l: " + this.����������_������.�����.ToString("0.0") + " �";
                this.Wide0_Status.Text = "w1: " + this.����������_������.������[0].ToString("0.0") + " �";
                this.Wide1_Status.Text = "w2: " + this.����������_������.������[1].ToString("0.0") + " �";
                this.Height0_Status.Text = "h1: " + this.����������_������.������[0].ToString("0.0") + " �";
                this.Height1_Status.Text = "h2: " + this.����������_������.������[1].ToString("0.0") + " �";
                if (this.����������_������.������)
                {
                    this.Radius_Status.Text = "r: " + this.����������_������.������.ToString("0.0") + " �";
                    double num12 = this.����������_������.���������.����0 + this.����������_������.���������.����1;
                    num18 = (num12 * 180.0) / Math.PI;
                    this.Angle_Status.Text = num18.ToString("0") + "\x00b0";
                }
            }
            else if (this.����������_������� != null)
            {
                num18 = this.����������_�������.�����[0].x / 1000.0;
                this.Coord_x1_Status.Text = "x: " + num18.ToString("0.000") + " ��";
                num18 = this.����������_�������.�����[0].y / 1000.0;
                this.Coord_y1_Status.Text = "y: " + num18.ToString("0.000") + " ��";
                num18 = (this.����������_�������.����������� * 180.0) / Math.PI;
                this.Angle1_Status.Text = num18.ToString("0") + "\x00b0";
                num18 = this.����������_�������.�����[1].x / 1000.0;
                this.Coord_x2_Status.Text = "x: " + num18.ToString("0.000") + " ��";
                num18 = this.����������_�������.�����[1].y / 1000.0;
                this.Coord_y2_Status.Text = "y: " + num18.ToString("0.000") + " ��";
                num18 = ((this.����������_�������.����������� + (2.0 * (this.����������_�������.�����������[1] - this.����������_�������.�����������))) * 180.0) / Math.PI;
                this.Angle2_Status.Text = num18.ToString("0") + "\x00b0";
                var point20 = this.����������_�������.�����[1] - this.����������_�������.�����[0];
                this.Length_Status.Text = "l: " + point20.Modulus.ToString("0.0") + " �";
                this.Height0_Status.Text = "h1: " + this.����������_�������.������[0].ToString("0.0") + " �";
                this.Height1_Status.Text = "h2: " + this.����������_�������.������[1].ToString("0.0") + " �";
                this.Angle_Status.Text = ((((2.0 * (this.����������_�������.�����������[1] - this.����������_�������.�����������)) * 180.0) / Math.PI)).ToString("0") + "\x00b0";
            }
            else if (this.Stops_Button.Pushed)
            {
                num18 = 11.0;
                if (this.����������_��������� != null)
                {
                    this.Length_Status.Text = "d: " + this.����������_���������.distance.ToString("0.0") + " �";
                    num18 = this.����������_���������.distance;
                }
                else if (Stops_Box.SelectedIndex >= 0)
                {
                    this.Length_Status.Text = "d: " + this.���.���������[Stops_Box.SelectedIndex].distance.ToString("0.0") + " �";
                    num18 = this.���.���������[Stops_Box.SelectedIndex].distance;
                }
                if (num18 < 10.0) this.Radius_Status.Text = "critical d!";
            }
            else if (this.����������_�������� != null)
            {
                this.Length_Status.Text = "d: " + this.����������_��������.���������.����������.ToString("0.0") + " �";
                this.Wide0_Status.Text = "offset:";
                this.Wide1_Status.Text = this.����������_��������.���������.����������.ToString("0.0") + " �";
                this.Height0_Status.Text = "h: " + this.����������_��������.���������.������.ToString("0.0") + " �";
            }
            else if (this.����������_�����������_������ != null)
            {
                this.Length_Status.Text = "d: " + this.����������_�����������_������.����������.ToString("0.0") + " �";
            }
            else if (this.����������_������_����������_������� != null)
            {
                this.Length_Status.Text = "d: " + this.����������_������_����������_�������.���������.����������.ToString("0.0") + " �";
                this.Wide0_Status.Text = "offset:";
                this.Wide1_Status.Text = this.����������_������_����������_�������.���������.����������.ToString("0.0") + " �";
                this.Height0_Status.Text = "h: " + this.����������_������_����������_�������.���������.������.ToString("0.0") + " �";
            }
            else if (this.����������_�������_����������_������� != null)
            {
                this.Length_Status.Text = "d: " + this.����������_�������_����������_�������.����������.ToString("0.0") + " �";
            }
            else if (this.����������_������ != null)
            {
                this.Radius_Status.Text = "angle:";
                this.Angle_Status.Text = (((this.����������_������.angle0 * 180.0) / Math.PI)).ToString("0") + "\x00b0";
                this.Height0_Status.Text = "h: " + this.����������_������.height0.ToString("0.0") + " �";
            }
        }

        private void Refresh_All_TripStop_Lists_Item_Click(object sender, EventArgs e)
        {
            this.���.���������.Sort((IComparer<Stop>)null);
            foreach (Route ������� in this.���.��������)
            {
                for (int j = 0; j < �������.AllTrips.Count; j++)
                {
                    �������.AllTrips[j].UpdateTripStopList(�������);
                }
            }
            this.modified = true;
        }

        private void Refresh_Timer_Tick(object sender, EventArgs e)
        {
            if (!Refresh_Timer.Enabled)
                return;
            Refresh_Timer.Enabled = false;

            // HACK: ������ �� �������
            this.���.��������_�����();
            MyDirect3D.device.BeginScene();
            MyDirect3D.ResetViewports(������.Length);
            MyDirect3D.SetViewport(-1);
            MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
            if (this.����������_������ != null)
            {
                if (this.Rail_Build_�������_Button.Pushed)
                {
                    var road1 = this.����������_������1;
                    var road2 = this.����������_������2;
                    if (this.Rail_Build_�������2_Button.Pushed)
                    {
                        if (road1 != null) road1.Render();
                    }
                    else if (this.Rail_Build_�������3_Button.Pushed)
                    {
                        if (road1 != null) road1.Render();
                        if (road2 != null) road2.Render();
                    }
                }
                if (this.Rail_Build_��������_Button.Pushed)
                {
                    var road = this.����������_��������_������;
                    var road1 = this.����������_��������_������1;
                    var road2 = this.����������_��������_������2;
                    if (this.Rail_Build_��������1_Button.Pushed)
                    {
                        if (road != null) road.Render();
                    }
                    else if (this.Rail_Build_��������2_Button.Pushed)
                    {
                        if (road != null) road.Render();
                        if (road1 != null) road1.Render();
                    }
                    else if (this.Rail_Build_��������3_Button.Pushed)
                    {
                        if (road != null) road.Render();
                        if (road1 != null) road1.Render();
                        if (road2 != null) road2.Render();
                    }
                }
                this.����������_������.Render();
            }
            else if (this.����������_������� != null)
            {
                int num = (this.����������_�������.������ == ������_������������.������_�����) ? this.����������_�������.�������.Length : this.����������_�������.�������.Length / 2;
                for (int i = 0; i < num; i++)
                {
                    this.����������_�������.�������[i].Render();
                }
                /*
                if (this.Troll_lines_Doblue.Pushed)
                {
                    var troll1 = this.����������_�������1;
                       if (troll1 != null) troll1.Render();
                }
                if (this.Troll_lines_Against.Pushed)
                {
                    var troll2 = this.����������_��������_�������;
                       if (troll2 != null) troll2.Render();
                }
                */
            }
            else if (this.����������_��������� != null)
            {
                this.����������_���������.Render();
            }
            else if (this.����������_������ != null)
            {
                this.����������_������.Render();
            }
            this.����.���.RenderMeshesA();
            this.����.Render();
            Refresh_Timer.Enabled = true;
            if (time_color > 0.0)
                time_color -= Refresh_Timer.Interval / 500.0;
            if ((time_color <= 0.0) && (this.����������_������))
            {
                time_color = 0.0;
                this.���������������������������();
            }
        }

        private void RefreshPanelSize(object sender, EventArgs e)
        {
            MyDirect3D.Window_Width = this.Sizable_Panel.ClientSize.Width;
            MyDirect3D.Window_Height = this.Sizable_Panel.ClientSize.Height;
            //MyDirect3D.viewport_x;
            //MyDirect3D.viewport_y;
        }

        private void Route_Add_Button_Click(object sender, EventArgs e)
        {
            List<Route> list = new List<Route>(this.���.��������);
            string str = (this.���.��������.Length + 1).ToString();
            Route item = new Route(TypeOfTransport.Tramway, str);
            list.Add(item);
            this.���.�������� = list.ToArray();
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
            Route_Name_Box.Text = flag ? ���.��������[Route_Box.SelectedIndex].number : "";
            Route_Name_Box.Modified = false;
            Route_ChangeName_Button.Enabled = false;
            Route_TransportType_label.Enabled = flag;
            Route_TransportType_Box.Enabled = flag;
            Route_TransportType_Box.SelectedIndex = flag ? ((int)���.��������[Route_Box.SelectedIndex].typeOfTransport) : -1;
            Route_Runs_label.Enabled = flag;
            TrolleybusAXBox.Enabled = flag;
            Route_Runs_Box.Enabled = flag;
            Route_Runs_Add_Button.Enabled = flag;
            StopsButton.Enabled = flag;
            Narad_label.Enabled = flag;
            Narad_Box.Enabled = flag;
            Narad_Add_Button.Enabled = flag;
            UpdateRouteControls(flag ? ���.��������[Route_Box.SelectedIndex] : null);
            ���������������������������();
        }

        private void RouteChangeNameButtonClick(object sender, EventArgs e)
        {
            var selectedIndex = Route_Box.SelectedIndex;
            if (selectedIndex < 0) return;
            ���.��������[selectedIndex].number = Route_Name_Box.Text;
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
            var list = new List<Route>(���.��������);
            list.RemoveAt(selectedIndex);
            ���.�������� = list.ToArray();
            Route_Box.Items.RemoveAt(selectedIndex);
            RouteBoxSelectedIndexChanged(null, new EventArgs());
            modified = true;
        }

        private void RouteRunsAddButtonClick(object sender, EventArgs e)
        {
            Trip item = new Trip();
            this.���������_�������.trips.Add(item);
            int index = this.���������_�������.trips.Count - 1;
            int num2 = index + 1;
            this.Route_Runs_Box.Items.Insert(index, "���� " + num2.ToString());
            this.Route_Runs_Box.SelectedIndex = index;
            this.Narad_Runs_Box_SelectedIndexChanged(sender, e);
            this.modified = true;
        }

        private void Route_Runs_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = (this.Route_Runs_Box.SelectedIndex >= 0) && (this.Route_Box.SelectedIndex >= 0);
            this.Route_Runs_Remove_Button.Enabled = flag;
            this.Route_Runs_Park_Box.Enabled = flag;
            this.Route_Runs_Park_Box.Checked = this.Route_Runs_Box.SelectedIndex >= this.���������_�������.trips.Count;
            this.Route_Runs_ToPark_Box.Enabled = flag && this.Route_Runs_Park_Box.Checked;
            this.Route_Runs_ToPark_Box.Checked = this.���������_����.inPark;
            this.Route_Runs_ToParkIndex_label.Enabled = flag && this.Route_Runs_ToPark_Box.Checked;
            this.Route_Runs_ToParkIndex_UpDown.Enabled = flag && this.Route_Runs_ToPark_Box.Checked;
            this.Route_Runs_ToParkIndex_UpDown.Maximum = Math.Max(this.Route_Runs_ToParkIndex_UpDown.Value, this.���������_����.pathes.Length);
            this.Route_Runs_ToParkIndex_UpDown.Value = this.���������_����.inParkIndex;
            this.Route_Runs_ToParkIndex_UpDown.Maximum = this.���������_����.pathes.Length;
            this.Route_Runs_Time_label.Enabled = flag;
            this.Route_Runs_Time_Box.Enabled = flag;
            this.Route_Runs_Time_Box.Time_Seconds = (int)this.���������_����.�����_��������;
            this.Route_Runs_ComputeTime_Button.Enabled = flag;
            this.���������������������������();
        }

        private void Route_Runs_ComputeTime_Button_Click(object sender, EventArgs e)
        {
            this.���.time = 0.0;
            if (���������_����.tripStopList == null)
            {
                ���������_����.InitTripStopList(���������_�������);
            }
            ComputeTimeDialog dialog = new ComputeTimeDialog(this.���, this.���������_�������.typeOfTransport, this.���������_����, this.�����);
            this.Refresh_Timer.Enabled = false;
            if (dialog.ShowDialog(this) != DialogResult.Cancel)
            {
                this.���������_����.�����_�������� = this.���.time;
                this.Route_Runs_Box_SelectedIndexChanged(null, new EventArgs());
                this.modified = true;
            }
            this.���.time = 0.0;
            this.Refresh_Timer.Enabled = true;
        }

        private void Route_Runs_Park_Box_CheckedChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Route_Runs_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                Trip item = this.���������_����;
                if (this.Route_Runs_Park_Box.Checked)
                {
                    if (selectedIndex < this.���������_�������.trips.Count)
                    {
                        this.���������_�������.trips.Remove(item);
                        this.���������_�������.parkTrips.Add(item);
                        this.Route_Runs_Box.Items.RemoveAt(selectedIndex);
                        this.Route_Runs_Box.Items.Add("�������� ���� " + this.���������_�������.parkTrips.Count.ToString());
                        this.Route_Runs_Box.SelectedIndex = this.Route_Runs_Box.Items.Count - 1;
                        this.modified = true;
                    }
                }
                else if (selectedIndex >= this.���������_�������.trips.Count)
                {
                    this.���������_�������.parkTrips.Remove(item);
                    this.���������_�������.trips.Add(item);
                    item.inPark = false;
                    int index = this.���������_�������.trips.Count - 1;
                    this.Route_Runs_Box.Items.RemoveAt(selectedIndex);
                    int num4 = index + 1;
                    this.Route_Runs_Box.Items.Insert(index, "���� " + num4.ToString());
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
                if (selectedIndex < this.���������_�������.trips.Count)
                {
                    this.���������_�������.trips.RemoveAt(selectedIndex);
                }
                else
                {
                    this.���������_�������.parkTrips.RemoveAt(selectedIndex - this.���������_�������.trips.Count);
                }
                this.Route_Runs_Box.Items.RemoveAt(selectedIndex);
                this.Route_Runs_Box_SelectedIndexChanged(null, new EventArgs());
                this.Narad_Runs_Box_SelectedIndexChanged(sender, e);
                this.modified = true;
            }
        }

        private void RouteRunsTimeBoxTimeChanged(object sender, EventArgs e)
        {
            this.���������_����.�����_�������� = this.Route_Runs_Time_Box.Time_Seconds;
            this.Narad_Runs_Box_SelectedIndexChanged(sender, e);
            this.modified = true;
        }

        private void RouteRunsToParkBoxCheckedChanged(object sender, EventArgs e)
        {
            this.���������_����.inPark = this.Route_Runs_ToPark_Box.Checked;
            this.Route_Runs_ToParkIndex_label.Enabled = this.Route_Runs_ToPark_Box.Enabled && this.Route_Runs_ToPark_Box.Checked;
            this.Route_Runs_ToParkIndex_UpDown.Enabled = this.Route_Runs_ToPark_Box.Enabled && this.Route_Runs_ToPark_Box.Checked;
            this.���������������������������();
            this.modified = true;
        }

        private void RouteRunsToParkIndexUpDownValueChanged(object sender, EventArgs e)
        {
            this.���������_����.inParkIndex = (int)this.Route_Runs_ToParkIndex_UpDown.Value;
            this.���������������������������();
            this.modified = true;
        }

        private void RouteShowNaradsBoxCheckedChanged(object sender, EventArgs e)
        {
            narad_panel.Visible = Route_ShowNarads_Box.Checked;
        }

        /*private void TrolleybusAXBox(object sender, EventArgs e)
        {
            
        }*/

        private void RouteTransportTypeBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Route_TransportType_Box.SelectedIndex < 0) return;
            ���������_�������.typeOfTransport = Route_TransportType_Box.SelectedIndex;
            RollingStockUpdate(���������_�����);
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
                DialogResult result = MessageBox.Show(Localization.current_.save_run, "Transedit", yesNoCancel, MessageBoxIcon.Exclamation);
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
            Process.Start(Application.ExecutablePath, string.Format("\"{0}\" -nolog", this.filename));
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
            double num = this.����_��������;
            this.���������_��(-num);
            try
            {
                this.���.���������_�����(this.filename);
            }
            catch (Exception exception)
            {
                MessageBox.Show(this, Localization.current_.save_failed + ":\n" + exception.Message, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            this.���������_��(num);
            this.modified = false;
        }

        public void NoReleasedFunction(object sender, EventArgs e)
        {
            MessageBox.Show(this, Localization.current_.functionnowork, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void SaveAs_Item_Click(object sender, EventArgs e)
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
                switch (MessageBox.Show(Localization.current_.save_only, "Transedit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
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
            List<����������_�������> list = new List<����������_�������>(this.���.�����������������);
            ����������_������� item = new ����������_�������(1, 0);
            item.CreateMesh();
            list.Add(item);
            this.���.����������������� = list.ToArray();
            this.Signals_Box.Items.Add("������� " + list.Count.ToString());
            this.Signals_Box.SelectedIndex = this.Signals_Box.Items.Count - 1;
            this.modified = true;
        }

        private void Signals_Bound_UpDown_ValueChanged(object sender, EventArgs e)
        {
            this.���������_����������_�������.�������_������������ = (int)this.Signals_Bound_UpDown.Value;
            this.modified = true;
        }

        private void Signals_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.Signals_Box.SelectedIndex >= 0;
            this.Signals_Remove_Button.Enabled = flag;
            this.Signals_Bound_label.Enabled = flag;
            this.Signals_Bound_UpDown.Enabled = flag;
            this.Signals_Bound_UpDown.Value = flag ? this.���������_����������_�������.�������_������������ : 0;
            this.Signals_Element_label.Enabled = flag;
            this.Signals_Element_Box.Enabled = flag;
            this.Signals_Element_AddContact_Button.Enabled = flag;
            this.Signals_Element_AddSignal_Button.Enabled = flag && (this.Signals_Model_Box.Items.Count > 0);
            this.UpdateSignalsControls(flag ? this.���.�����������������[this.Signals_Box.SelectedIndex] : null);
        }

        private void Signals_Element_AddContact_Button_Click(object sender, EventArgs e)
        {
            if (this.���.���������.Length > 0)
            {
                new ����������_�������.�������(this.���������_����������_�������, this.���.���������[0], 0.0, false);
                this.UpdateSignalsControls(this.���������_����������_�������);
                this.Signals_Element_Box.SelectedIndex = this.Signals_Element_Box.Items.Count - 1;
                this.Signals_Element_EditLocation_Button_Click(sender, e);
            }
        }

        private void Signals_Element_AddSignal_Button_Click(object sender, EventArgs e)
        {
            if (this.���.���������.Length > 0)
            {
                var signal = new Visual_Signal(this.���������_����������_�������, this.Signals_Model_Box.Items[this.Signals_Model_Box.SelectedIndex].ToString());
                signal.��������� = new ���������();
                this.UpdateSignalsControls(this.���������_����������_�������);
                this.Signals_Element_Box.SelectedIndex = this.���������_����������_�������.vsignals.Count - 1;
                this.���������_����������_�������.CreateMesh();
                this.Signals_Element_EditLocation_Button_Click(sender, e);
            }
        }

        private void Signals_Element_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = (this.Signals_Element_Box.SelectedIndex >= 0) && (this.Signals_Box.SelectedIndex >= 0);
            this.Signals_Element_Remove_Button.Enabled = flag;
            this.Signals_Element_Minus_Box.Enabled = flag;
            if (flag && (this.Signals_Element_Box.SelectedIndex >= this.���������_����������_�������.vsignals.Count))
            {
                this.Signals_Element_Minus_Box.Visible = true;
                this.Signals_Element_Minus_Box.Checked = this.���������_����������_�������.��������[this.Signals_Element_Box.SelectedIndex - this.���������_����������_�������.vsignals.Count].�����;
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
            if (selectedIndex < 0)
                return;
            if (selectedIndex < this.���������_����������_�������.vsignals.Count)
            {
                this.����������_������_����������_������� = this.���������_����������_�������.vsignals[selectedIndex];
            }
            else
            {
                this.����������_�������_����������_������� = this.���������_����������_�������.��������[selectedIndex - this.���������_����������_�������.vsignals.Count];
            }
            this.EnableControls(false);
            this.modified = true;
        }

        private void Signals_Element_Minus_Box_CheckedChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Signals_Element_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            ����������_�������.������� ������� = this.���������_����������_�������.��������[selectedIndex - this.���������_����������_�������.vsignals.Count];
            �������.����� = this.Signals_Element_Minus_Box.Checked;
            this.modified = true;
        }

        private void Signals_Element_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Signals_Element_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            if (selectedIndex < this.���������_����������_�������.vsignals.Count)
            {
                this.���������_����������_�������.������_������(this.���������_����������_�������.vsignals[selectedIndex]);
            }
            else
            {
                this.���������_����������_�������.������_�������(this.���������_����������_�������.��������[selectedIndex - this.���������_����������_�������.vsignals.Count]);
            }
            UpdateSignalsControls(this.���������_����������_�������);
            this.Signals_Element_Box.SelectedIndex = selectedIndex - 1;
            this.modified = true;
        }

        private void Signals_Element_ShowLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Signals_Element_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            if (selectedIndex < this.���������_����������_�������.vsignals.Count)
            {
                this.�����.cameraPosition.XZPoint = this.���������_����������_�������.vsignals[selectedIndex].road.���������������(this.���������_����������_�������.vsignals[selectedIndex].���������.����������, 0.0);
            }
            else
            {
                this.�����.cameraPosition.XZPoint = this.���������_����������_�������.��������[selectedIndex - this.���������_����������_�������.vsignals.Count].������.���������������(this.���������_����������_�������.��������[selectedIndex - this.���������_����������_�������.vsignals.Count].����������, 0.0);
            }
        }

        private void Signals_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Signals_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            List<����������_�������> list = new List<����������_�������>(this.���.�����������������);
            list.RemoveAt(selectedIndex);
            this.���.����������������� = list.ToArray();
            this.Signals_Box.Items.RemoveAt(selectedIndex);
            this.Signals_Box_SelectedIndexChanged(null, new EventArgs());
            this.modified = true;
        }

        private void Stops_Add_Button_Click(object sender, EventArgs e)
        {
            if (this.���.���������.Length <= 0)
                return;
            this.����������_��������� = new Stop(this.Stops_Model_Box.Items[this.Stops_Model_Box.SelectedIndex].ToString(), new TypeOfTransport(TypeOfTransport.Tramway), "���������", this.���.���������[0], 0.0);
            this.����������_���������.CreateMesh();
            this.RegisterPendingAction(new AddStopAction(this.����������_���������));
            this.EnableControls(false);
        }

        private void Stops_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            Check_All_Stops_Boxes();
        }

        private void Check_All_Stops_Boxes()
        {
            var stopSelected = this.Stops_Box.SelectedIndex >= 0;
            this.Stops_Remove_Button.Enabled = stopSelected;
            this.Stops_Name_label.Enabled = stopSelected;
            this.Stops_Name_Box.Enabled = stopSelected;
            this.Stops_Name_Box.Text = string.Empty;
            this.Stops_Name_Box.Modified = false;
            this.Stops_ChangeName_Button.Enabled = false;
            this.TypeOfTransportBox.Enabled = stopSelected;
            this.Stops_Location_label.Enabled = stopSelected;
            this.Stops_ShowLocation_Button.Enabled = stopSelected;
            this.Stops_EditLocation_Button.Enabled = stopSelected;
            this.���������������������������();
            this.UpdateStatusBar();

            if (!stopSelected)
                return;
            var selectedStop = ���.���������[Stops_Box.SelectedIndex];
            Stops_Name_Box.Text = selectedStop.��������;
            TramwayBox.Checked = selectedStop.typeOfTransport[TypeOfTransport.Tramway];
            TrolleybusBox.Checked = selectedStop.typeOfTransport[TypeOfTransport.Trolleybus];
            BusBox.Checked = selectedStop.typeOfTransport[TypeOfTransport.Bus];
        }

        private void Stops_ChangeName_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Stops_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            this.���.���������[selectedIndex].�������� = this.Stops_Name_Box.Text;
            this.Stops_Box.Items[selectedIndex] = this.Stops_Name_Box.Text;
            this.Stops_Name_Box.Modified = false;
            this.modified = true;
        }

        private void Stops_EditLocation_Button_Click(object sender, EventArgs e)
        {
            var selectedIndex = Stops_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            ����������_��������� = ���.���������[selectedIndex];
            RegisterPendingAction(new MoveStopAction(����������_���������), true);
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
            if (selectedIndex < 0)
                return;
            this.DoRegisterAction(new RemoveStopAction(selectedIndex));
            this.modified = true;
        }

        private void Stops_ShowLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Stops_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;

            this.�����.cameraPosition.XZPoint = this.���.���������[selectedIndex].road.���������������(this.���.���������[selectedIndex].distance, 0.0);
        }

        private void Svetofor_Add_Button_Click(object sender, EventArgs e)
        {
            List<�����������_�������> list = new List<�����������_�������>(this.���.������������������);
            �����������_������� item = new �����������_�������();
            list.Add(item);
            this.���.������������������ = list.ToArray();
            this.Svetofor_Box.Items.Add("������� " + list.Count.ToString());
            this.Svetofor_Box.SelectedIndex = this.Svetofor_Box.Items.Count - 1;
            this.modified = true;
        }

        private void Svetofor_Begin_Box_TimeChanged(object sender, EventArgs e)
        {
            this.���������_�����������_�������.������_������ = this.Svetofor_Begin_Box.Time_Seconds;
            this.modified = true;
        }

        private void Svetofor_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = this.Svetofor_Box.SelectedIndex >= 0;
            this.Svetofor_Remove_Button.Enabled = flag;
            this.Svetofor_Work_label.Enabled = flag;
            this.Svetofor_Begin_Box.Enabled = flag;
            this.Svetofor_Begin_Box.Time_Seconds = (int)this.���������_�����������_�������.������_������;
            this.Svetofor_End_Box.Enabled = flag;
            this.Svetofor_End_Box.Time_Seconds = (int)this.���������_�����������_�������.���������_������;
            this.Svetofor_Cycle_label.Enabled = flag;
            this.Svetofor_Cycle_Box.Enabled = flag;
            this.Svetofor_Cycle_Box.Time_Seconds = (int)this.���������_�����������_�������.����;
            this.Svetofor_Green_label.Enabled = flag;
            this.Svetofor_ToGreen_Box.Enabled = flag;
            this.Svetofor_ToGreen_Box.Time_Seconds = (int)this.���������_�����������_�������.�����_������������_��_������;
            this.Svetofor_OfGreen_Box.Enabled = flag;
            this.Svetofor_OfGreen_Box.Time_Seconds = (int)this.���������_�����������_�������.�����_�������;
            this.Svetofor_Element_label.Enabled = flag;
            this.Svetofor_Element_Box.Enabled = flag;
            this.Svetofor_Svetofor_Add_Button.Enabled = flag && (this.Svetofor_Model_Box.Items.Count > 0);
            this.Svetofor_Signal_Add_Button.Enabled = flag;
            this.UpdateSvetoforControls(flag ? this.���.������������������[this.Svetofor_Box.SelectedIndex] : null);
        }

        private void Svetofor_Cycle_Box_TimeChanged(object sender, EventArgs e)
        {
            this.���������_�����������_�������.���� = this.Svetofor_Cycle_Box.Time_Seconds;
            this.modified = true;
        }

        private void Svetofor_Element_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = (this.Svetofor_Element_Box.SelectedIndex >= 0) && (this.Svetofor_Box.SelectedIndex >= 0);
            bool flag2 = flag && (this.Svetofor_Element_Box.SelectedIndex < this.���������_�����������_�������.���������.Count);
            this.Svetofor_Element_Remove_Button.Enabled = flag;
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
                for (int i = 0; i < this.���������_�����������_�������.���������[this.Svetofor_Element_Box.SelectedIndex].tex_count; i++)
                {
                    this.Svetofor_Svetofor_ArrowRed_Box.Items.Add(this.���������_�����������_�������.���������[this.Svetofor_Element_Box.SelectedIndex].model.FindStringArg("tex" + i, string.Empty));
                    this.Svetofor_Svetofor_ArrowYellow_Box.Items.Add(this.���������_�����������_�������.���������[this.Svetofor_Element_Box.SelectedIndex].model.FindStringArg("tex" + i, string.Empty));
                    this.Svetofor_Svetofor_ArrowGreen_Box.Items.Add(this.���������_�����������_�������.���������[this.Svetofor_Element_Box.SelectedIndex].model.FindStringArg("tex" + i, string.Empty));
                }
            }
            this.Svetofor_Svetofor_ArrowGreen_Box.SelectedIndex = flag2 ? ((int)this.���������_�����������_�������.���������[this.Svetofor_Element_Box.SelectedIndex].������_�������) : -1;
            this.Svetofor_Svetofor_ArrowYellow_Box.SelectedIndex = flag2 ? ((int)this.���������_�����������_�������.���������[this.Svetofor_Element_Box.SelectedIndex].�����_�������) : -1;
            this.Svetofor_Svetofor_ArrowRed_Box.SelectedIndex = flag2 ? ((int)this.���������_�����������_�������.���������[this.Svetofor_Element_Box.SelectedIndex].�������_�������) : -1;
            this.Svetofor_Element_Location_label.Enabled = flag;
            this.Svetofor_Element_ShowLocation_Button.Enabled = flag;
            this.Svetofor_Element_EditLocation_Button.Enabled = flag;
            this.���������������������������();
        }

        private void Svetofor_Element_EditLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (selectedIndex >= 0)
            {
                if (selectedIndex < this.���������_�����������_�������.���������.Count)
                {
                    this.����������_�������� = this.���������_�����������_�������.���������[selectedIndex];
                }
                else
                {
                    selectedIndex -= this.���������_�����������_�������.���������.Count;
                    this.����������_�����������_������ = this.���������_�����������_�������.�����������_�������[selectedIndex];
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
                if (selectedIndex < this.���������_�����������_�������.���������.Count)
                {
                    this.���������_�����������_�������.���������.RemoveAt(selectedIndex);
                }
                else
                {
                    this.���������_�����������_�������.�����������_�������.RemoveAt(selectedIndex - this.���������_�����������_�������.���������.Count);
                }
                this.UpdateSvetoforControls(this.���������_�����������_�������);
                this.Svetofor_Element_Box.SelectedIndex = -1;
                this.modified = true;
            }
        }

        private void Svetofor_Element_ShowLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;

            if (selectedIndex < this.���������_�����������_�������.���������.Count)
            {
                this.�����.cameraPosition.XZPoint = this.���������_�����������_�������.���������[selectedIndex].���������.����������.XZPoint;
            }
            else
            {
                selectedIndex -= this.���������_�����������_�������.���������.Count;
                this.�����.cameraPosition.XZPoint = this.���������_�����������_�������.�����������_�������[selectedIndex].������.���������������(this.���������_�����������_�������.�����������_�������[selectedIndex].����������, 0.0);
            }
        }

        private void Svetofor_End_Box_TimeChanged(object sender, EventArgs e)
        {
            this.���������_�����������_�������.���������_������ = this.Svetofor_End_Box.Time_Seconds;
            this.modified = true;
        }

        private void Svetofor_OfGreen_Box_TimeChanged(object sender, EventArgs e)
        {
            this.���������_�����������_�������.�����_������� = this.Svetofor_OfGreen_Box.Time_Seconds;
            this.modified = true;
        }

        private void Svetofor_Remove_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            List<�����������_�������> list = new List<�����������_�������>(this.���.������������������);
            list.RemoveAt(selectedIndex);
            this.���.������������������ = list.ToArray();
            this.Svetofor_Box.Items.RemoveAt(selectedIndex);
            this.Svetofor_Box_SelectedIndexChanged(null, new EventArgs());
            this.modified = true;
        }

        private void Svetofor_Signal_Add_Button_Click(object sender, EventArgs e)
        {
            if (this.���.���������.Length <= 0)
                return;

            this.���������_�����������_�������.�����������_�������.Add(new �����������_������(this.���.���������[0], 0.0));
            this.UpdateSvetoforControls(this.���������_�����������_�������);
            this.Svetofor_Element_Box.SelectedIndex = this.Svetofor_Element_Box.Items.Count - 1;
            this.Svetofor_Element_EditLocation_Button_Click(sender, e);
        }

        private void Svetofor_Svetofor_Add_Button_Click(object sender, EventArgs e)
        {
            if (this.���.���������.Length <= 0)
                return;

            �������� item = new ��������(this.Svetofor_Model_Box.Items[this.Svetofor_Model_Box.SelectedIndex].ToString());
            item.CreateMesh();
            item.��������� = new ���������();
            this.���������_�����������_�������.���������.Add(item);
            this.UpdateSvetoforControls(this.���������_�����������_�������);
            this.Svetofor_Element_Box.SelectedIndex = this.���������_�����������_�������.���������.Count - 1;
            this.Svetofor_Element_EditLocation_Button_Click(sender, e);
        }

        private void Svetofor_Svetofor_ArrowGreen_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (((selectedIndex >= 0) && (selectedIndex < this.���������_�����������_�������.���������.Count)) && (this.Svetofor_Svetofor_ArrowGreen_Box.SelectedIndex >= 0))
            {
                this.���������_�����������_�������.���������[selectedIndex].������_������� = this.Svetofor_Svetofor_ArrowGreen_Box.SelectedIndex;
                this.modified = true;
            }
        }

        private void Svetofor_Svetofor_ArrowRed_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (((selectedIndex >= 0) && (selectedIndex < this.���������_�����������_�������.���������.Count)) && (this.Svetofor_Svetofor_ArrowRed_Box.SelectedIndex >= 0))
            {
                this.���������_�����������_�������.���������[selectedIndex].�������_������� = this.Svetofor_Svetofor_ArrowRed_Box.SelectedIndex;
                this.modified = true;
            }
        }

        private void Svetofor_Svetofor_ArrowYellow_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = this.Svetofor_Element_Box.SelectedIndex;
            if (((selectedIndex >= 0) && (selectedIndex < this.���������_�����������_�������.���������.Count)) && (this.Svetofor_Svetofor_ArrowYellow_Box.SelectedIndex >= 0))
            {
                this.���������_�����������_�������.���������[selectedIndex].�����_������� = this.Svetofor_Svetofor_ArrowYellow_Box.SelectedIndex;
                this.modified = true;
            }
        }

        private void Svetofor_ToGreen_Box_TimeChanged(object sender, EventArgs e)
        {
            this.���������_�����������_�������.�����_������������_��_������ = this.Svetofor_ToGreen_Box.Time_Seconds;
            this.modified = true;
        }

        public void toolBar_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
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
            if (e.Button == this.ButtonUndo)
            {
                this.UndoAction();
            }
            if (e.Button == this.Info)
            {
                //using (FileStream fstream = File.OpenRead("Readme_Editor.txt"));
                //ShellExecute(0, 'open', 'Readme_Editor.txt', nil, nil, SW_SHOW);
                //OpenDocument('Readme_Editor.txt');
                //StreamReader sr = new StreamReader("Readme_Editor.txt");
                //FileStream file1 = new FileStream("Readme_Editor.txt", FileMode.Open);
                //Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @".."));
                Directory.SetCurrentDirectory(Application.StartupPath);
                Process.Start("notepad.exe", "Readme_Editor.txt");

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
                this.Rail_Build_�������_Button.Visible = splines_aviable;
                this.Rail_Build_��������_Button.Visible = splines_aviable;
                this.TrollWireOverRoad.Visible = splines_aviable;
                this.TramWireOverRail.Visible = splines_aviable;
                this.Road_Button.Visible = splines_aviable;
                //                this.Road_Button.Enabled = false;
            }
            else
            {
                this.Rail_Edit_Button.Visible = false;
                this.Rail_Build_Direct_Button.Visible = false;
                this.Rail_Build_Curve_Button.Visible = false;
                this.Rail_Build_�������_Button.Visible = false;
                this.Rail_Build_��������_Button.Visible = false;
                this.Road_Button.Visible = false;
                this.Rail_Edit_Button.Pushed = true;
                this.Rail_Build_Direct_Button.Pushed = false;
                this.Rail_Build_Curve_Button.Pushed = false;
                this.Rail_Build_��������_Button.Pushed = false;
                this.Rail_Build_�������_Button.Pushed = false;
                this.TrollWireOverRoad.Visible = false;
                this.TramWireOverRail.Visible = false;
            }
            if (this.Rail_Build_��������_Button.Pushed)
            {
                this.Rail_Build_��������1_Button.Visible = true;
                this.Rail_Build_��������2_Button.Visible = true;
                this.Rail_Build_��������3_Button.Visible = true;
            }
            else
            {
                this.Rail_Build_��������1_Button.Visible = false;
                this.Rail_Build_��������2_Button.Visible = false;
                this.Rail_Build_��������3_Button.Visible = false;
                this.Rail_Build_��������1_Button.Pushed = true;
                this.Rail_Build_��������2_Button.Pushed = false;
                this.Rail_Build_��������3_Button.Pushed = false;
            }
            if (this.Rail_Build_�������_Button.Pushed)
            {
                this.Rail_Build_�������1_Button.Visible = true;
                this.Rail_Build_�������2_Button.Visible = true;
                this.Rail_Build_�������3_Button.Visible = true;

            }
            else
            {
                this.Rail_Build_�������1_Button.Visible = false;
                this.Rail_Build_�������2_Button.Visible = false;
                this.Rail_Build_�������3_Button.Visible = false;
                this.Rail_Build_�������1_Button.Pushed = true;
                this.Rail_Build_�������2_Button.Pushed = false;
                this.Rail_Build_�������3_Button.Pushed = false;

            }

            if (e.Button == Rail_Build_�������1_Button)
            {
                if (this.Rail_Build_�������1_Button.Pushed)
                {
                    this.Rail_Build_�������2_Button.Pushed = false;
                    this.Rail_Build_�������3_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_�������1_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_�������2_Button)
            {
                if (this.Rail_Build_�������2_Button.Pushed)
                {
                    this.Rail_Build_�������1_Button.Pushed = false;
                    this.Rail_Build_�������3_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_�������2_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_�������3_Button)
            {
                if (this.Rail_Build_�������3_Button.Pushed)
                {
                    this.Rail_Build_�������1_Button.Pushed = false;
                    this.Rail_Build_�������2_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_�������3_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_��������1_Button)
            {
                if (this.Rail_Build_��������1_Button.Pushed)
                {
                    this.Rail_Build_��������2_Button.Pushed = false;
                    this.Rail_Build_��������3_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_��������1_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_��������2_Button)
            {
                if (this.Rail_Build_��������2_Button.Pushed)
                {
                    this.Rail_Build_��������1_Button.Pushed = false;
                    this.Rail_Build_��������3_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_��������2_Button.Pushed = true;
                }
            }
            if (e.Button == Rail_Build_��������3_Button)
            {
                if (this.Rail_Build_��������3_Button.Pushed)
                {
                    this.Rail_Build_��������1_Button.Pushed = false;
                    this.Rail_Build_��������2_Button.Pushed = false;
                }
                else
                {
                    this.Rail_Build_��������3_Button.Pushed = true;
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
            if (this.TrollWireOverRoad.Pushed)
            {
                this.Rail_Edit_Button.Pushed = false;
                this.TramWireOverRail.Pushed = false;
                MessageBox.Show(this, Localization.current_.functionnowork, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (this.TramWireOverRail.Pushed)
            {
                this.Rail_Edit_Button.Pushed = false;
                this.Road_Button.Pushed = false;
                this.TrollWireOverRoad.Pushed = false;
                MessageBox.Show(this, Localization.current_.functionnowork, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (this.Rail_Build_Direct_Button.Pushed || this.Rail_Build_Curve_Button.Pushed)
            {
                if (this.����������_������ == null)
                {
                    if (this.Road_Button.Pushed)
                    {
                        this.����������_������ = new Road(0.0, 0.0, 20.0, 0.0, 0.0, true, 5.0, 5.0);
                        this.����������_������.�����������������������(this.���.������);
                    }
                    else
                    {
                        this.����������_������ = new �����(0.0, 0.0, 20.0, 0.0, 0.0, true);
                        this.����������_������.�����������������������(this.���.������);
                    }
                    this.����������_������.name = Splines_Models_Box.Text;
                    this.����������_������.CreateMesh();
                    this.����������_������.Color = 0xff;
                    if (this.����������_������ is �����)
                    {
                        ((�����)this.����������_������).����������_�������.CreateMesh();
                    }
                    this.edit_panel.Enabled = false;
                }
            }
            else if (this.����������_������ != null)
            {
                this.����������_������ = null;
                this.edit_panel.Enabled = true;
            }
            if (this.Troll_lines_Button.Pushed)
            {
                this.Troll_lines_Edit_Button.Visible = true;
                this.Troll_lines_Draw_Button.Visible = true;
                this.Troll_lines_Flag_Button.Visible = true;
                //this.Troll_lines_Doblue.Visible = true;
                //this.Troll_lines_Against.Visible = true;
            }
            else
            {
                this.Troll_lines_Edit_Button.Visible = false;
                this.Troll_lines_Draw_Button.Visible = false;
                this.Troll_lines_Flag_Button.Visible = false;
                this.Troll_lines_Doblue.Visible = false;
                this.Troll_lines_Against.Visible = false;
                this.Troll_lines_Edit_Button.Pushed = true;
                this.Troll_lines_Draw_Button.Pushed = false;
                this.Troll_lines_Flag_Button.Pushed = false;
                //this.Troll_lines_Doblue.Pushed = false;
                //this.Troll_lines_Against.Pushed = false;
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
                if ((this.����������_������� == null) || (e.Button == this.Troll_lines_Flag_Button))
                {
                    if (this.����������_������� != null)
                        this.ClearPendingAction();
                    if (!this.Troll_lines_Flag_Button.Pushed)
                    {
                        this.����������_������� = new ����������_�������();
                        RegisterPendingAction(new AddWiresAction(this.����������_�������.�������[0], this.����������_�������.�������[1]));
                    }
                    else
                    {
                        this.����������_������� = new ����������_����������_�������();
                        RegisterPendingAction(new AddTramWireAction(this.����������_�������.�������[0] as ����������_����������_������));
                    }
                    if (this.Troll_lines_Doblue.Pushed)
                    {
                        //this.Troll_lines_Against.Pushed = false;
                        //if (!this.Troll_lines_Flag_Button.Pushed)
                        //{
                        //this.����������_������� = new ����������_�������();
                        /*RegisterPendingAction(new AddWiresAction(this.����������_�������.�������[0], this.����������_�������.�������[1]));
                        this.����������_������� = new ����������_������������_�������������_�������();*/
                        //}
                        //if (this.Troll_lines_Flag_Button.Pushed)
                        //{
                        //this.����������_������� = new ����������_����������_�������();
                        /*RegisterPendingAction(new AddTramWireAction(this.����������_�������.�������[0] as ����������_����������_������));
                        this.����������_������� = new ����������_������������_�������();*/
                        //}
                        MessageBox.Show(this, Localization.current_.functionnowork, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    if (this.Troll_lines_Against.Pushed)
                    {
                        //this.Troll_lines_Doblue.Pushed = false;
                        // if (!this.Troll_lines_Flag_Button.Pushed)
                        // {
                        //this.����������_������� = new ����������_�������();
                        /*RegisterPendingAction(new AddWiresAction(this.����������_�������.�������[0], this.����������_�������.�������[1]));
                        this.����������_������� = new ����������_�������_�������������_�������();*/
                        //}
                        //if (this.Troll_lines_Flag_Button.Pushed)
                        //{
                        //this.����������_������� = new ����������_����������_�������();
                        /*RegisterPendingAction(new AddTramWireAction(this.����������_�������.�������[1] as ����������_����������_������));
                        this.����������_������� = new ����������_�������_�������();*/
                        //}
                        MessageBox.Show(this, Localization.current_.functionnowork, "Transedit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else if (this.����������_������� != null)
            {
                this.ClearPendingAction();
                this.����������_������� = null;
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
            this.���������������������������();
            this.���������������������������();
            this.Check_All_Splines_Boxes();
        }

        private void UpdateNaradControls(Order �����)
        {
            Narad_Runs_Box.Items.Clear();
            if (����� != null)
            {
                for (var i = 0; i < �����.�����.Length; i++)
                {
                    var num2 = i + 1;
                    Narad_Runs_Box.Items.Add("���� " + num2);
                }
            }
            Narad_Runs_Box_SelectedIndexChanged(null, new EventArgs());
        }

        private void UpdatePanels()
        {
            UpdateParksList();
            UpdateStopsList();
            Route_Box.Items.Clear();
            foreach (var ������� in ���.��������)
            {
                Route_Box.Items.Add(�������.number);
            }
            RouteBoxSelectedIndexChanged(null, new EventArgs());
            Signals_Box.Items.Clear();
            for (var i = 1; i < ���.�����������������.Length + 1; i++)
            {
                Signals_Box.Items.Add("������� " + i);
            }
            Signals_Box_SelectedIndexChanged(null, new EventArgs());
            Svetofor_Box.Items.Clear();
            for (var j = 1; j < ���.������������������.Length + 1; j++)
            {
                Svetofor_Box.Items.Add("������� " + j);
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
                // ���� ������� �� ������ ������� ���������
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
            for (var i = 0; i < ���.�������.Count; i++)
            {
                if (���.�������[i] != null)
                {
                    Objects_Instance_Box.Items.Add("Obj " + (i + 1) + ", " + ((���.�������[i].model != null) ? ���.�������[i].model.name : "???"));
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
            var rails = ���.������;
            for (int i = 1; i < rails.Length + 1; i++)
            {
                Splines_Instance_Box.Items.Add("Rail " + i + ", " + rails[i - 1].name);
            }
            var roads = ���.������;
            for (int i = 1; i < roads.Length + 1; i++)
            {
                Splines_Instance_Box.Items.Add("Road " + i + ", " + roads[i - 1].name);
            }
            if (Splines_Instance_Box.Items.Count > 0)
            {
                Splines_Instance_Box.SelectedIndex = 0;
            }
            this.Check_All_Splines_Boxes();
        }

        private void UpdateStopsList()
        {
            Stops_Box.Items.Clear();
            foreach (var ��������� in ���.���������)
            {
                Stops_Box.Items.Add(���������.��������);
            }
            if (Stops_Box.Items.Count > 0)
                Stops_Box.SelectedIndex = 0;
            else
                Check_All_Stops_Boxes();
        }

        private void UpdateRouteControls(Route �������)
        {
            Route_Runs_Box.Items.Clear();
            if (������� != null)
            {
                for (var i = 0; i < �������.trips.Count; i++)
                {
                    var num4 = i + 1;
                    if (Route_Runs_Box != null) Route_Runs_Box.Items.Add("���� " + num4);
                }
                for (var j = 0; j < �������.parkTrips.Count; j++)
                {
                    var num5 = j + 1;
                    if (Route_Runs_Box != null) Route_Runs_Box.Items.Add("�������� ���� " + num5.ToString());
                }
                if (Route_Runs_Box.Items.Count > 0)
                {
                    Route_Runs_Box.SelectedIndex = 0;
                }
            }
            Route_Runs_Box_SelectedIndexChanged(null, new EventArgs());
            Narad_Box.Items.Clear();
            if (������� != null)
            {
                for (var k = 0; k < �������.orders.Length; k++)
                {
                    Narad_Box.Items.Add(�������.orders[k].�����);
                }
            }
            Narad_Box_SelectedIndexChanged(null, new EventArgs());
        }

        private void UpdateSignalsControls(����������_������� �������)
        {
            this.Signals_Element_Box.Items.Clear();
            if (������� != null)
            {
                for (int j = 0; j < �������.vsignals.Count; j++)
                {
                    Signals_Element_Box.Items.Add("������ " + (j + 1));
                }
                for (var i = 0; i < �������.��������.Count; i++)
                {
                    Signals_Element_Box.Items.Add("������� " + (i + 1));
                }
                if (�������.��������.Count > 0)
                {
                    Signals_Element_Box.SelectedIndex = �������.vsignals.Count;
                }
            }
            Signals_Element_Box_SelectedIndexChanged(null, new EventArgs());
        }

        private void UpdateSvetoforControls(�����������_������� �������)
        {
            Svetofor_Element_Box.Items.Clear();
            if (������� != null)
            {
                for (var i = 0; i < �������.���������.Count; i++)
                {
                    var num3 = i + 1;
                    Svetofor_Element_Box.Items.Add("�������� " + num3);
                }
                for (var j = 0; j < �������.�����������_�������.Count; j++)
                {
                    var num4 = j + 1;
                    Svetofor_Element_Box.Items.Add("������ " + num4);
                }
                if (�������.�����������_�������.Count > 0)
                {
                    Svetofor_Element_Box.SelectedIndex = �������.���������.Count;
                }
            }
            Svetofor_Element_Box_SelectedIndexChanged(null, new EventArgs());
        }

        private void UpdateParksList()
        {
            Park_Box.Items.Clear();
            foreach (var ���� in ���.�����)
            {
                Park_Box.Items.Add(����.��������);
            }
            if (���.�����.Length > 0)
                Park_Box.SelectedIndex = 0;
            else
                Check_All_Park_Boxes();
            Narad_Park_Box.Items.Clear();
            foreach (var ���� in ���.�����)
            {
                Narad_Park_Box.Items.Add(����.��������);
            }
        }

        private void ���������������������������()
        {
            if (this.����������_�������)
            {
                foreach (����������_������ _������ in this.���.�����������������)
                {
                    _������.color = 0x000000;
                }
                foreach (����������_����������_������ _������2 in this.���.�����������������2)
                {
                    _������2.color = 0x000000;
                }
                this.����������_������� = false;
            }
            if (this.Troll_lines_Button.Pushed)
            {
                foreach (����������_������ _������2 in this.���.�����������������)
                {
                    _������2.color = 0xF2B65C;
                    if (_������2.������������)
                    {
                        _������2.color = 0xff0000;
                    }
                }
                foreach (����������_����������_������ _������1 in this.���.�����������������2)
                {
                    _������1.color = 0xF2B65C;
                    if (_������1.������������)
                    {
                        _������1.color = 0xff0000;
                    }
                }
                this.����������_������� = true;
            }
        }

        private void ���������������������������()
        {
            if (this.����������_������)
            {
                foreach (Road ������ in this.���.���������)
                {
                    ������.Color = 0;
                }
                this.����������_������ = false;
            }
            try
            {
                if ((this.Rail_Button.Pushed) && (Splines_Instance_Box.SelectedIndex >= 0) && (time_color > 0.0))
                {
                    if (Splines_Instance_Box.SelectedIndex < this.���.������.Length)
                    {
                        this.���.������[Splines_Instance_Box.SelectedIndex].Color = 0xff;
                    }
                    else
                    {
                        this.���.������[Splines_Instance_Box.SelectedIndex - this.���.������.Length].Color = 0xff;
                    }
                    this.����������_������ = true;
                }
                if (this.Park_Button.Pushed && (this.Park_Box.SelectedIndex >= 0))
                {
                    ���� ���� = this.���.�����[this.Park_Box.SelectedIndex];
                    if (����.����� != null)
                    {
                        ����.�����.Color = 0xff00;
                    }
                    if (����.����� != null)
                    {
                        ����.�����.Color = 0xff0000;
                    }
                    foreach (Road ������2 in ����.����_�������)
                    {
                        ������2.Color = 0xff;
                    }
                    this.����������_������ = true;
                }
                if (this.Stops_Button.Pushed && (this.Stops_Box.SelectedIndex >= 0))
                {
                    Stop ��������� = this.���.���������[this.Stops_Box.SelectedIndex];
                    foreach (Road ������3 in ���������.���������)
                    {
                        ������3.Color = 0xff;
                    }
                    this.����������_������ = true;
                }
                if ((this.Route_Button.Pushed && (this.Route_Box.SelectedIndex >= 0)) && (this.Route_Runs_Box.SelectedIndex >= 0))
                {
                    Trip ���� = this.���.��������[this.Route_Box.SelectedIndex].AllTrips[this.Route_Runs_Box.SelectedIndex];
                    for (int i = 0; i < ����.pathes.Length; i++)
                    {
                        if (i == 0)
                        {
                            ����.pathes[i].Color = 0xff0000;
                        }
                        else if (i == (����.pathes.Length - 1))
                        {
                            ����.pathes[i].Color = 0xff00;
                        }
                        else if (����.inPark && (i >= ����.inParkIndex))
                        {
                            ����.pathes[i].Color = 0xffff00;
                        }
                        else
                        {
                            ����.pathes[i].Color = 0xff;
                        }
                    }
                    this.����������_������ = true;
                }
                if ((this.Svetofor_Button.Pushed && (this.Svetofor_Box.SelectedIndex >= 0)) && ((this.Svetofor_Element_Box.SelectedIndex >= 0) && (this.Svetofor_Element_Box.SelectedIndex >= this.���������_�����������_�������.���������.Count)))
                {
                    int num2 = this.Svetofor_Element_Box.SelectedIndex - this.���������_�����������_�������.���������.Count;
                    if (this.���������_�����������_�������.�����������_�������[num2].������ != null)
                    {
                        this.���������_�����������_�������.�����������_�������[num2].������.Color = 0xff;
                    }
                    this.����������_������ = true;
                }
            }
            catch
            {
            }
        }

        private void ���������_��(double ����)
        {
            this.����_�������� += ����;
            this.�����.cameraPosition.AngleX += ����;
            this.�����.cameraRotation.x += ����;
            foreach (Road ������ in this.���.���������)
            {
                ������.�����[0].Angle += ����;
                ������.�����[1].Angle += ����;
                ������.�����������[0] += ����;
                ������.�����������[1] += ����;
                ������.�����������������();
            }
            if (this.����������_������ != null)
            {
                this.����������_������.�����[0].Angle += ����;
                this.����������_������.�����[1].Angle += ����;
                this.����������_������.�����������[0] += ����;
                this.����������_������.�����������[1] += ����;
                this.����������_������.�����������������();
            }
            foreach (����������_������ _������ in this.���.�����������������)
            {
                _������.������.Angle += ����;
                _������.�����.Angle += ����;
            }
            foreach (����������_������ _������ in this.���.�����������������2)
            {
                _������.������.Angle += ����;
                _������.�����.Angle += ����;
            }
            if (this.����������_������� != null)
            {
                this.����������_�������.�����[0].Angle += ����;
                this.����������_�������.�����[1].Angle += ����;
                this.����������_�������.�����������[0] += ����;
                this.����������_�������.�����������[1] += ����;
                this.����������_�������.����������� += ����;
                this.����������_�������.��������();
            }
            foreach (������ _������ in this.���.�������)
            {
                //_������.bounding_box += ����;
            }
            if (this.����������_������ != null)
            {
                //this.����������_������.position.Angle += ����;
            }
        }

        public DoublePoint cursor_pos
        {
            get
            {
                int num = (base.Size.Width - base.ClientSize.Width) / 2;
                int num2 = (base.Size.Height - base.ClientSize.Height) - num;
                double num3 = ((double)(Control.MousePosition.X - ((((2 * (base.Location.X + num)) + this.Sizable_Panel.Left) + this.Sizable_Panel.Right) / 2))) / MyDirect3D.�������;
                double num4 = ((double)(Control.MousePosition.Y - ((((2 * (base.Location.Y + num2)) + this.Sizable_Panel.Top) + this.Sizable_Panel.Bottom) / 2))) / MyDirect3D.�������;
                return new DoublePoint(this.�����.cameraPosition.x + num3, this.�����.cameraPosition.z - num4);
            }
        }

        public ����������_������[] ���������_�������
        {
            get
            {
                DoublePoint point = this.cursor_pos;
                List<����������_������> list = new List<����������_������>();
                List<int> list2 = new List<int>();
                for (int i = 0; i < this.���.�����������������.Length; i++)
                {
                    ����������_������ item = this.���.�����������������[i];
                    DoublePoint point2 = (point - item.������) / (item.����� - item.������);
                    if ((point2.x >= 0.0) && (point2.x < 1.0))
                    {
                        if (Math.Abs((double)(item.����� * point2.y)) < 0.5)
                        {
                            list.Add(item);
                            list2.Add(i);
                        }
                    }
                }
                if (((list.Count == 2) && ((list2[0] / 2) == (list2[1] / 2))) && (list[0].������ != list[1].������))
                {
                    return list.ToArray();
                }
                return null;
            }
        }

        public ����������_������[] ��������_�������
        {
            get
            {
                DoublePoint point1 = this.cursor_pos;
                List<����������_������> list = new List<����������_������>();
                List<int> list2 = new List<int>();
                for (int i = 0; i < this.���.�����������������.Length; i++)
                {
                    ����������_������ item = this.���.�����������������[i];
                    DoublePoint point3 = (point1 - item.�����) / (item.����� - item.������);
                    if ((point3.x >= 0.0) && (point3.x < 1.0))
                    {
                        if (Math.Abs((double)(item.����� * point3.y)) < 0.5)
                        {
                            list.Add(item);
                            list2.Add(i);
                        }
                    }
                }
                if (((list.Count == 2) && ((list2[0] / 2) == (list2[1] / 2))) && (list[0].������ != list[1].������))
                {
                    return list.ToArray();
                }
                return null;

            }
        }

        public ����������_����������_������ ���������_������
        {
            get
            {
                DoublePoint point = this.cursor_pos;
                for (int i = 0; i < this.���.�����������������2.Length; i++)
                {
                    ����������_����������_������ item = this.���.�����������������2[i];
                    DoublePoint point2 = (point - item.������) / (item.����� - item.������);
                    if ((point2.x >= 0.0) && (point2.x < 1.0))
                    {
                        if (Math.Abs((double)(item.����� * point2.y)) < 0.5)
                        {
                            return item;
                        }
                    }

                }
                return null;
            }
        }

        private �����������_������� ���������_�����������_�������
        {
            get
            {
                if (this.Svetofor_Box.SelectedIndex >= 0)
                {
                    return this.���.������������������[this.Svetofor_Box.SelectedIndex];
                }
                return new �����������_�������();
            }
        }

        private ����������_������� ���������_����������_�������
        {
            get
            {
                if (this.Signals_Box.SelectedIndex >= 0)
                {
                    return this.���.�����������������[this.Signals_Box.SelectedIndex];
                }
                return new ����������_�������(0, 0);
            }
        }

        public Route ���������_�������
        {
            get
            {
                if (this.Route_Box.SelectedIndex >= 0)
                {
                    return this.���.��������[this.Route_Box.SelectedIndex];
                }
                return new Route(TypeOfTransport.Tramway, "-");
            }
        }

        private Order ���������_�����
        {
            get
            {
                if ((this.Narad_Box.SelectedIndex >= 0) && (this.Narad_Box.SelectedIndex < this.���������_�������.orders.Length))
                {
                    return this.���������_�������.orders[this.Narad_Box.SelectedIndex];
                }
                return new Order(new ����(""), new Route(TypeOfTransport.Tramway, ""), "", "");
            }
        }

        private Trip ���������_����
        {
            get
            {
                if (this.Route_Runs_Box.SelectedIndex >= this.���������_�������.trips.Count)
                {
                    return this.���������_�������.parkTrips[this.Route_Runs_Box.SelectedIndex - this.���������_�������.trips.Count];
                }
                if (this.Route_Runs_Box.SelectedIndex >= 0)
                {
                    return this.���������_�������.trips[this.Route_Runs_Box.SelectedIndex];
                }
                return new Trip();
            }
        }

        public Road ����������_������1
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num5 = this.����������_������.������[0];
                    double num6 = this.����������_������.������[1];
                    DoublePoint point = this.����������_������.���������������(0.0, -num5);
                    DoublePoint point2 = this.����������_������.���������������(this.����������_������.�����, -num6);
                    double num7 = this.����������_������.�����������[0];
                    double num8 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point.x, point.y, point2.x, point2.y, num7, num8);
                    }
                    else
                    {
                        ������ = new Road(point.x, point.y, point2.x, point2.y, num7, num8, num5, num6);
                    }
                    ������.������[0] = this.����������_������.������[0];
                    ������.������[1] = this.����������_������.������[1];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xff0000;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }
        public Road ����������_������2
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num5 = this.����������_������.������[0];
                    double num6 = this.����������_������.������[1];
                    DoublePoint point = this.����������_������.���������������(0.0, -2 * num5);
                    DoublePoint point2 = this.����������_������.���������������(this.����������_������.�����, -2 * num6);
                    double num7 = this.����������_������.�����������[0];
                    double num8 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point.x, point.y, point2.x, point2.y, num7, num8);
                    }
                    else
                    {
                        ������ = new Road(point.x, point.y, point2.x, point2.y, num7, num8, num5, num6);
                    }
                    ������.������[0] = this.����������_������.������[0];
                    ������.������[1] = this.����������_������.������[1];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xff0000;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }
        public Road ����������_��������_������
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num11 = this.����������_������.������[0];
                    double num12 = this.����������_������.������[1];
                    DoublePoint point11 = this.����������_������.���������������(0.0, num11);
                    DoublePoint point12 = this.����������_������.���������������(this.����������_������.�����, num12);
                    double num13 = this.����������_������.�����������[0];
                    double num14 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point12.x, point12.y, point11.x, point11.y, num14, num13);
                    }
                    else
                    {
                        ������ = new Road(point12.x, point12.y, point11.x, point11.y, num14, num13, num12, num11);
                    }
                    ������.������[0] = this.����������_������.������[1];
                    ������.������[1] = this.����������_������.������[0];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xffff00;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }
        public Road ����������_��������_������1
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num21 = this.����������_������.������[0];
                    double num22 = this.����������_������.������[1];
                    DoublePoint point21 = this.����������_������.���������������(0.0, 2 * num21);
                    DoublePoint point22 = this.����������_������.���������������(this.����������_������.�����, 2 * num22);
                    double num23 = this.����������_������.�����������[0];
                    double num24 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point22.x, point22.y, point21.x, point21.y, num24, num23);
                    }
                    else
                    {
                        ������ = new Road(point22.x, point22.y, point21.x, point21.y, num24, num23, num22, num21);
                    }
                    ������.������[0] = this.����������_������.������[1];
                    ������.������[1] = this.����������_������.������[0];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xffff00;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }
        public Road ����������_��������_������2
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num = this.����������_������.������[0];
                    double num2 = this.����������_������.������[1];
                    DoublePoint point = this.����������_������.���������������(0.0, 3 * num);
                    DoublePoint point2 = this.����������_������.���������������(this.����������_������.�����, 3 * num2);
                    double num3 = this.����������_������.�����������[0];
                    double num4 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point2.x, point2.y, point.x, point.y, num4, num3);
                    }
                    else
                    {
                        ������ = new Road(point2.x, point2.y, point.x, point.y, num4, num3, num2, num);
                    }
                    ������.������[0] = this.����������_������.������[1];
                    ������.������[1] = this.����������_������.������[0];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xffff00;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }

        private enum ������_������������
        {
            ���,
            ������_�����,
            ������_�����
        }

        private class ����������_�������
        {
            public double[] ������ = new double[2];
            public DoublePoint[] �����;
            public DoublePoint[] ����� = new DoublePoint[2];
            public double �����������;
            public double[] ����������� = new double[2];
            public DoublePoint[] ������;
            public ����������_������[] �������;
            public ������_������������ ������;

            public ����������_�������()
            {
                this.������� = new ����������_������[4];
                this.Create();
            }

            public virtual void ��������()
            {
                this.����������� = (Math.Round((double)((this.����������� * 36.0) / Math.PI)) * Math.PI) / 36.0;
                this.�����������[0] = (Math.Round((double)((this.�����������[0] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����������[1] = (Math.Round((double)((this.�����������[1] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����[0].Angle -= this.�����������;
                this.�����[1].Angle -= this.�����������;
                this.�����������[0] -= this.�����������;
                this.�����������[1] -= this.�����������;
                double num = ����������_������.����������_�����_��������� / 2.0;
                this.�������[0].������.x = this.�����[0].x + (num * Math.Tan(this.�����������[0]));
                this.�������[0].������.y = this.�����[0].y - num;
                this.�������[1].������.x = this.�����[0].x - (num * Math.Tan(this.�����������[0]));
                this.�������[1].������.y = this.�����[0].y + num;
                this.�������[0].�����.x = this.�����[1].x + (num * Math.Tan(this.�����������[1]));
                this.�������[0].�����.y = this.�����[1].y - num;
                this.�������[1].�����.x = this.�����[1].x - (num * Math.Tan(this.�����������[1]));
                this.�������[1].�����.y = this.�����[1].y + num;
                if (this.������ == Editor.������_������������.������_�����)
                {
                    this.�������[2].������ = this.�������[0].������ - ((DoublePoint)(new DoublePoint(2.0 * this.�����������[0]) * 10.0));
                    this.�������[2].����� = this.�������[0].������;
                    this.�������[3].������ = this.�������[1].������ - ((DoublePoint)(new DoublePoint(2.0 * this.�����������[0]) * 10.0));
                    this.�������[3].����� = this.�������[1].������;
                }
                else
                {
                    this.�������[2].������ = this.�������[0].�����;
                    this.�������[2].����� = this.�������[0].����� + ((DoublePoint)(new DoublePoint(2.0 * this.�����������[1]) * 10.0));
                    this.�������[3].������ = this.�������[1].�����;
                    this.�������[3].����� = this.�������[1].����� + ((DoublePoint)(new DoublePoint(2.0 * this.�����������[1]) * 10.0));
                }
                this.�������[0].������[0] = this.������[0];
                this.�������[0].������[1] = this.������[1];
                this.�������[1].������[0] = this.������[0];
                this.�������[1].������[1] = this.������[1];
                this.�������[2].������[0] = this.������[1];
                this.�������[2].������[1] = this.������[1];
                this.�������[3].������[0] = this.������[1];
                this.�������[3].������[1] = this.������[1];
                this.�������[0].������.Angle += this.�����������;
                this.�������[0].�����.Angle += this.�����������;
                this.�������[1].������.Angle += this.�����������;
                this.�������[1].�����.Angle += this.�����������;
                this.�������[2].������.Angle += this.�����������;
                this.�������[2].�����.Angle += this.�����������;
                this.�������[3].������.Angle += this.�����������;
                this.�������[3].�����.Angle += this.�����������;
                this.�����[0].Angle += this.�����������;
                this.�����[1].Angle += this.�����������;
                this.�����������[0] += this.�����������;
                this.�����������[1] += this.�����������;
                if (this.������ != null)
                {
                    this.�������[0].������ = this.������[0];
                    this.�������[1].������ = this.������[1];
                }
                if (this.����� != null)
                {
                    this.�������[0].����� = this.�����[0];
                    this.�������[1].����� = this.�����[1];
                }
            }

            protected virtual void Create()
            {
                for (int i = 0; i < this.�������.Length; i++)
                {
                    this.�������[i] = new ����������_������(0.0, 0.0, 20.0, 0.0, (i % 2) == 0);
                    this.�������[i].CreateMesh();
                    this.�������[i].color = (i < 2) ? 0xff : 0x3f3f3f;
                    //0xff
                }
            }
        }

        private class ����������_����������_������� : ����������_�������
        {
            public bool flag = false;

            public ����������_����������_�������()
            {
                this.������� = new ����������_������[2];
                this.Create();
            }

            public override void ��������()
            {
                this.����������� = (Math.Round((double)((this.����������� * 36.0) / Math.PI)) * Math.PI) / 36.0;
                this.�����������[0] = (Math.Round((double)((this.�����������[0] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����������[1] = (Math.Round((double)((this.�����������[1] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����[0].Angle -= this.�����������;
                this.�����[1].Angle -= this.�����������;
                this.�����������[0] -= this.�����������;
                this.�����������[1] -= this.�����������;
                this.�������[0].������.x = this.�����[0].x;
                this.�������[0].������.y = this.�����[0].y;
                this.�������[1].������.x = this.�����[0].x;
                this.�������[1].������.y = this.�����[0].y;
                this.�������[0].�����.x = this.�����[1].x;
                this.�������[0].�����.y = this.�����[1].y;
                this.�������[1].�����.x = this.�����[1].x;
                this.�������[1].�����.y = this.�����[1].y;
                if (this.������ == Editor.������_������������.������_�����)
                {
                    this.�������[1].������ = this.�������[0].������ - ((DoublePoint)(new DoublePoint(2.0 * this.�����������[0]) * 10.0));
                    this.�������[1].����� = this.�������[0].������;
                }
                else
                {
                    this.�������[1].������ = this.�������[0].�����;
                    this.�������[1].����� = this.�������[0].����� + ((DoublePoint)(new DoublePoint(2.0 * this.�����������[1]) * 10.0));
                }
                this.�������[0].������[0] = this.������[0];
                this.�������[0].������[1] = this.������[1];
                this.�������[1].������[0] = this.������[1];
                this.�������[1].������[1] = this.������[1];
                this.�������[0].������.Angle += this.�����������;
                this.�������[0].�����.Angle += this.�����������;
                this.�������[1].������.Angle += this.�����������;
                this.�������[1].�����.Angle += this.�����������;
                this.�����[0].Angle += this.�����������;
                this.�����[1].Angle += this.�����������;
                this.�����������[0] += this.�����������;
                this.�����������[1] += this.�����������;
                if (this.������ != null)
                {
                    this.�������[0].������ = this.������[0];
                }
                if (this.����� != null)
                {
                    this.�������[0].����� = this.�����[0];
                }
            }

            protected override void Create()
            {
                for (int i = 0; i < this.�������.Length; i++)
                {
                    this.�������[i] = new ����������_����������_������(0.0, 0.0, 20.0, 0.0);
                    this.�������[i].CreateMesh();
                    this.�������[i].color = (i < 1) ? 0xff : 0x3f3f3f;
                }
            }
        }

        private class ����������_������������_������� : ����������_�������
        {
            public bool flag = false;

            public ����������_������������_�������()
            {
                this.������� = new ����������_������[6];
                this.Create();
            }

            public override void ��������()
            {
                this.����������� = (Math.Round((double)((this.����������� * 36.0) / Math.PI)) * Math.PI) / 36.0;
                this.�����������[0] = (Math.Round((double)((this.�����������[0] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����������[1] = (Math.Round((double)((this.�����������[1] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����[0].Angle -= this.�����������;
                this.�����[1].Angle -= this.�����������;
                this.�����������[0] -= this.�����������;
                this.�����������[1] -= this.�����������;
                this.�������[0].������.x = this.�����[0].x + 2;
                this.�������[0].������.y = this.�����[0].y;
                this.�������[1].������.x = this.�����[0].x + 2;
                this.�������[1].������.y = this.�����[0].y;
                this.�������[0].�����.x = this.�����[1].x + 2;
                this.�������[0].�����.y = this.�����[1].y;
                this.�������[1].�����.x = this.�����[1].x + 2;
                this.�������[1].�����.y = this.�����[1].y;
                if (this.������ == Editor.������_������������.������_�����)
                {
                    this.�������[1].������ = this.�������[0].������ - ((DoublePoint)(new DoublePoint(4.0 * this.�����������[0]) * 10.0));
                    this.�������[1].����� = this.�������[0].������;

                }
                else
                {
                    this.�������[1].������ = this.�������[0].�����;
                    this.�������[1].����� = this.�������[0].����� + ((DoublePoint)(new DoublePoint(4.0 * this.�����������[1]) * 10.0));

                }
                this.�������[0].������[0] = this.������[0];
                this.�������[0].������[1] = this.������[1];
                this.�������[1].������[0] = this.������[1];
                this.�������[1].������[1] = this.������[1];
                this.�������[0].������.Angle += this.�����������;
                this.�������[0].�����.Angle += this.�����������;
                this.�������[1].������.Angle += this.�����������;
                this.�������[1].�����.Angle += this.�����������;
                this.�����[0].Angle += this.�����������;
                this.�����[1].Angle += this.�����������;
                this.�����������[0] += this.�����������;
                this.�����������[1] += this.�����������;
                if (this.������ != null)
                {
                    this.�������[0].������ = this.������[0];

                }
                if (this.����� != null)
                {
                    this.�������[0].����� = this.�����[0];

                }
            }

            protected override void Create()
            {
                for (int i = 1; i < this.�������.Length; i++)
                {
                    this.�������[i] = new ����������_����������_������(2.0, 0.0, 20.0, 0.0);
                    this.�������[i].CreateMesh();
                    this.�������[i].color = (i < 3) ? 0x00FF00 : 0x7CC37C;
                }
            }
        }

        private class ����������_������������_�������������_������� : ����������_�������
        {
            public bool flag = false;

            public ����������_������������_�������������_�������()
            {
                this.������� = new ����������_������[8];
                this.Create();
            }

            public virtual void ��������()
            {
                this.����������� = (Math.Round((double)((this.����������� * 36.0) / Math.PI)) * Math.PI) / 36.0;
                this.�����������[0] = (Math.Round((double)((this.�����������[0] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����������[1] = (Math.Round((double)((this.�����������[1] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����[0].Angle -= this.�����������;
                this.�����[1].Angle -= this.�����������;
                this.�����������[0] -= this.�����������;
                this.�����������[1] -= this.�����������;
                double num = ����������_������.����������_�����_��������� / 2.0;
                this.�������[0].������.x = this.�����[0].x + (num * Math.Tan(this.�����������[0]));
                this.�������[0].������.y = this.�����[0].y - num;
                this.�������[1].������.x = this.�����[0].x - (num * Math.Tan(this.�����������[0]));
                this.�������[1].������.y = this.�����[0].y + num;
                this.�������[0].�����.x = this.�����[1].x + (num * Math.Tan(this.�����������[1]));
                this.�������[0].�����.y = this.�����[1].y - num;
                this.�������[1].�����.x = this.�����[1].x - (num * Math.Tan(this.�����������[1]));
                this.�������[1].�����.y = this.�����[1].y + num;
                if (this.������ == Editor.������_������������.������_�����)
                {
                    this.�������[2].������ = this.�������[0].������ - ((DoublePoint)(new DoublePoint(4.0 * this.�����������[0]) * 10.0));
                    this.�������[2].����� = this.�������[0].������;
                    this.�������[3].������ = this.�������[1].������ - ((DoublePoint)(new DoublePoint(4.0 * this.�����������[0]) * 10.0));
                    this.�������[3].����� = this.�������[1].������;
                }
                else
                {
                    this.�������[2].������ = this.�������[0].�����;
                    this.�������[2].����� = this.�������[0].����� + ((DoublePoint)(new DoublePoint(4.0 * this.�����������[1]) * 10.0));
                    this.�������[3].������ = this.�������[1].�����;
                    this.�������[3].����� = this.�������[1].����� + ((DoublePoint)(new DoublePoint(4.0 * this.�����������[1]) * 10.0));
                }
                this.�������[0].������[0] = this.������[0];
                this.�������[0].������[1] = this.������[1];
                this.�������[1].������[0] = this.������[0];
                this.�������[1].������[1] = this.������[1];
                this.�������[2].������[0] = this.������[1];
                this.�������[2].������[1] = this.������[1];
                this.�������[3].������[0] = this.������[1];
                this.�������[3].������[1] = this.������[1];
                this.�������[0].������.Angle += this.�����������;
                this.�������[0].�����.Angle += this.�����������;
                this.�������[1].������.Angle += this.�����������;
                this.�������[1].�����.Angle += this.�����������;
                this.�������[2].������.Angle += this.�����������;
                this.�������[2].�����.Angle += this.�����������;
                this.�������[3].������.Angle += this.�����������;
                this.�������[3].�����.Angle += this.�����������;
                this.�����[0].Angle += this.�����������;
                this.�����[1].Angle += this.�����������;
                this.�����������[0] += this.�����������;
                this.�����������[1] += this.�����������;
                if (this.������ != null)
                {
                    this.�������[0].������ = this.������[0];
                    this.�������[1].������ = this.������[1];
                }
                if (this.����� != null)
                {
                    this.�������[0].����� = this.�����[0];
                    this.�������[1].����� = this.�����[1];
                }
            }

            protected override void Create()
            {
                for (int i = 1; i < this.�������.Length; i++)
                {
                    this.�������[i] = new ����������_������(2.0, 0.0, 20.0, 0.0, (i % 2) == 0);
                    this.�������[i].CreateMesh();
                    this.�������[i].color = (i < 4) ? 0x00FF00 : 0x7CC37C;
                }
            }
        }

        private class ����������_�������_������� : ����������_�������
        {
            public bool flag = false;

            public ����������_�������_�������()
            {
                this.������� = new ����������_������[6];
                this.Create();
            }

            public override void ��������()
            {
                this.����������� = (Math.Round((double)((this.����������� * 36.0) / Math.PI)) * Math.PI) / 36.0;
                this.�����������[0] = (Math.Round((double)((this.�����������[0] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����������[1] = (Math.Round((double)((this.�����������[1] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����[0].Angle -= this.�����������;
                this.�����[1].Angle -= this.�����������;
                this.�����������[0] -= this.�����������;
                this.�����������[1] -= this.�����������;
                this.�������[0].������.x = this.�����[0].x;
                this.�������[0].������.y = this.�����[0].y;
                this.�������[1].������.x = this.�����[0].x;
                this.�������[1].������.y = this.�����[0].y;
                this.�������[0].�����.x = this.�����[1].x;
                this.�������[0].�����.y = this.�����[1].y;
                this.�������[1].�����.x = this.�����[1].x;
                this.�������[1].�����.y = this.�����[1].y;
                if (this.������ == Editor.������_������������.������_�����)
                {
                    this.�������[1].������ = this.�������[0].������ - ((DoublePoint)(new DoublePoint(4.0 * this.�����������[0]) * 10.0));
                    this.�������[1].����� = this.�������[0].������;
                }
                else
                {
                    this.�������[1].������ = this.�������[0].�����;
                    this.�������[1].����� = this.�������[0].����� + ((DoublePoint)(new DoublePoint(4.0 * this.�����������[1]) * 10.0));
                }
                this.�������[0].������[0] = this.������[0];
                this.�������[0].������[1] = this.������[1];
                this.�������[1].������[0] = this.������[1];
                this.�������[1].������[1] = this.������[1];
                this.�������[0].������.Angle += this.�����������;
                this.�������[0].�����.Angle += this.�����������;
                this.�������[1].������.Angle += this.�����������;
                this.�������[1].�����.Angle += this.�����������;
                this.�����[0].Angle += this.�����������;
                this.�����[1].Angle += this.�����������;
                this.�����������[0] += this.�����������;
                this.�����������[1] += this.�����������;
                if (this.������ != null)
                {
                    this.�������[0].������ = this.������[0];
                }
                if (this.����� != null)
                {
                    this.�������[0].����� = this.�����[0];
                }
            }

            protected override void Create()
            {
                for (int i = 1; i < this.�������.Length; i++)
                {
                    this.�������[i] = new ����������_����������_������(5.0, 0.0, 20.0, 0.0);
                    this.�������[i].CreateMesh();
                    this.�������[i].color = (i < 3) ? 0x76A5AF : 0xF9CB9C;
                }
            }
        }

        private class ����������_�������_�������������_������� : ����������_�������
        {
            public bool flag = false;

            public ����������_�������_�������������_�������()
            {
                this.������� = new ����������_������[8];
                this.Create();
            }

            public virtual void ��������()
            {
                this.����������� = (Math.Round((double)((this.����������� * 36.0) / Math.PI)) * Math.PI) / 36.0;
                this.�����������[0] = (Math.Round((double)((this.�����������[0] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����������[1] = (Math.Round((double)((this.�����������[1] * 72.0) / Math.PI)) * Math.PI) / 72.0;
                this.�����[0].Angle -= this.�����������;
                this.�����[1].Angle -= this.�����������;
                this.�����������[0] -= this.�����������;
                this.�����������[1] -= this.�����������;
                double num = ����������_������.����������_�����_��������� / 2.0;
                this.�������[0].������.x = this.�����[0].x + (num * Math.Tan(this.�����������[0]));
                this.�������[0].������.y = this.�����[0].y - num;
                this.�������[1].������.x = this.�����[0].x - (num * Math.Tan(this.�����������[0]));
                this.�������[1].������.y = this.�����[0].y + num;
                this.�������[0].�����.x = this.�����[1].x + (num * Math.Tan(this.�����������[1]));
                this.�������[0].�����.y = this.�����[1].y - num;
                this.�������[1].�����.x = this.�����[1].x - (num * Math.Tan(this.�����������[1]));
                this.�������[1].�����.y = this.�����[1].y + num;
                if (this.������ == Editor.������_������������.������_�����)
                {
                    this.�������[2].������ = this.�������[0].������ - ((DoublePoint)(new DoublePoint(4.0 * this.�����������[0]) * 10.0));
                    this.�������[2].����� = this.�������[0].������;
                    this.�������[3].������ = this.�������[1].������ - ((DoublePoint)(new DoublePoint(4.0 * this.�����������[0]) * 10.0));
                    this.�������[3].����� = this.�������[1].������;
                }
                else
                {
                    this.�������[2].������ = this.�������[0].�����;
                    this.�������[2].����� = this.�������[0].����� + ((DoublePoint)(new DoublePoint(4.0 * this.�����������[1]) * 10.0));
                    this.�������[3].������ = this.�������[1].�����;
                    this.�������[3].����� = this.�������[1].����� + ((DoublePoint)(new DoublePoint(4.0 * this.�����������[1]) * 10.0));
                }
                this.�������[0].������[0] = this.������[0];
                this.�������[0].������[1] = this.������[1];
                this.�������[1].������[0] = this.������[0];
                this.�������[1].������[1] = this.������[1];
                this.�������[2].������[0] = this.������[1];
                this.�������[2].������[1] = this.������[1];
                this.�������[3].������[0] = this.������[1];
                this.�������[3].������[1] = this.������[1];
                this.�������[0].������.Angle += this.�����������;
                this.�������[0].�����.Angle += this.�����������;
                this.�������[1].������.Angle += this.�����������;
                this.�������[1].�����.Angle += this.�����������;
                this.�������[2].������.Angle += this.�����������;
                this.�������[2].�����.Angle += this.�����������;
                this.�������[3].������.Angle += this.�����������;
                this.�������[3].�����.Angle += this.�����������;
                this.�����[0].Angle += this.�����������;
                this.�����[1].Angle += this.�����������;
                this.�����������[0] += this.�����������;
                this.�����������[1] += this.�����������;
                if (this.������ != null)
                {
                    this.�������[0].������ = this.������[0];
                    this.�������[1].������ = this.������[1];
                }
                if (this.����� != null)
                {
                    this.�������[0].����� = this.�����[0];
                    this.�������[1].����� = this.�����[1];
                }
            }

            protected override void Create()
            {
                for (int i = 1; i < this.�������.Length; i++)
                {
                    this.�������[i] = new ����������_������(5.0, 0.0, 20.0, 0.0, (i % 2) == 0);
                    this.�������[i].CreateMesh();
                    this.�������[i].color = (i < 4) ? 0x76A5AF : 0xF9CB9C;
                }
            }
        }
        /*
        public class ����������_��������_������� : ����������_�������
        {}
        public class ����������_��������_�������1 : ����������_����������_�������
        {}
        public class ����������_�������1 : ����������_�������
        {}
        public class ����������_�������11 : ����������_����������_�������
        {}
        */
        /*
       public Road ����������_������1
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num5 = this.����������_������.������[0];
                    double num6 = this.����������_������.������[1];
                    DoublePoint point = this.����������_������.���������������(0.0, -num5);
                    DoublePoint point2 = this.����������_������.���������������(this.����������_������.�����, -num6);
                    double num7 = this.����������_������.�����������[0];
                    double num8 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point.x, point.y, point2.x, point2.y, num7, num8);
                    }
                    else
                    {
                        ������ = new Road(point.x, point.y, point2.x, point2.y, num7, num8, num5, num6);
                    }
                    ������.������[0] = this.����������_������.������[0];
                    ������.������[1] = this.����������_������.������[1];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xff0000;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }
        public Road ����������_������2
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num5 = this.����������_������.������[0];
                    double num6 = this.����������_������.������[1];
                    DoublePoint point = this.����������_������.���������������(0.0, -2 * num5);
                    DoublePoint point2 = this.����������_������.���������������(this.����������_������.�����, -2 * num6);
                    double num7 = this.����������_������.�����������[0];
                    double num8 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point.x, point.y, point2.x, point2.y, num7, num8);
                    }
                    else
                    {
                        ������ = new Road(point.x, point.y, point2.x, point2.y, num7, num8, num5, num6);
                    }
                    ������.������[0] = this.����������_������.������[0];
                    ������.������[1] = this.����������_������.������[1];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xff0000;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }
        public Road ����������_��������_������
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num11 = this.����������_������.������[0];
                    double num12 = this.����������_������.������[1];
                    DoublePoint point11 = this.����������_������.���������������(0.0, num11);
                    DoublePoint point12 = this.����������_������.���������������(this.����������_������.�����, num12);
                    double num13 = this.����������_������.�����������[0];
                    double num14 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point12.x, point12.y, point11.x, point11.y, num14, num13);
                    }
                    else
                    {
                        ������ = new Road(point12.x, point12.y, point11.x, point11.y, num14, num13, num12, num11);
                    }
                    ������.������[0] = this.����������_������.������[1];
                    ������.������[1] = this.����������_������.������[0];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xffff00;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }
        public Road ����������_��������_������1
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num21 = this.����������_������.������[0];
                    double num22 = this.����������_������.������[1];
                    DoublePoint point21 = this.����������_������.���������������(0.0, 2 * num21);
                    DoublePoint point22 = this.����������_������.���������������(this.����������_������.�����, 2 * num22);
                    double num23 = this.����������_������.�����������[0];
                    double num24 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point22.x, point22.y, point21.x, point21.y, num24, num23);
                    }
                    else
                    {
                        ������ = new Road(point22.x, point22.y, point21.x, point21.y, num24, num23, num22, num21);
                    }
                    ������.������[0] = this.����������_������.������[1];
                    ������.������[1] = this.����������_������.������[0];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xffff00;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }
        public Road ����������_��������_������2
        {
            get
            {
                Road ������;
                if (this.����������_������ == null)
                {
                    return null;
                }
                try
                {
                    double num = this.����������_������.������[0];
                    double num2 = this.����������_������.������[1];
                    DoublePoint point = this.����������_������.���������������(0.0, 3 * num);
                    DoublePoint point2 = this.����������_������.���������������(this.����������_������.�����, 3 * num2);
                    double num3 = this.����������_������.�����������[0];
                    double num4 = this.����������_������.�����������[1];
                    if (this.����������_������ is �����)
                    {
                        ������ = new �����(point2.x, point2.y, point.x, point.y, num4, num3);
                    }
                    else
                    {
                        ������ = new Road(point2.x, point2.y, point.x, point.y, num4, num3, num2, num);
                    }
                    ������.������[0] = this.����������_������.������[1];
                    ������.������[1] = this.����������_������.������[0];
                    ������.������ = this.����������_������.������;
                    ������.�����������������������(this.���.���������);
                    ������.name = ����������_������.name;
                    ������.CreateMesh();
                    if (this.����������_������.Color == 0xff)
                    {
                        ������.Color = 0xffff00;
                    }
                    return ������;
                }
                catch
                {
                    return null;
                }
            }
        }*/

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
            this.����������_������ = new ������(this.Objects_Box.Text, cursor_pos.x, cursor_pos.y, 0.0, 0.0);
            this.����������_������.CreateMesh();
            RegisterPendingAction(new AddObjectAction(this.����������_������));
            this.EnableControls(false);
        }

        private void Object_Instance_Box_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Check_All_Objects_Boxes();
        }

        private void ObjectsRemoveButtonClick(object sender, EventArgs e)
        {
            var index = Objects_Instance_Box.SelectedIndex;
            this.DoRegisterAction(new RemoveObjectAction(index));
            if (index > 0)
            {
                Objects_Instance_Box.SelectedIndex = index - 1;
            }
            modified = true;
        }

        private void RollingStockUpdate(Order �����)
        {
            RollingStockBox.Items.Clear();
            if ((Narad_Box.Items.Count <= 0) || (Narad_Box.SelectedIndex < 0))
                return;
            RollingStockBox.Items.Add("���������");
            switch (�����.�������.typeOfTransport)
            {
                case TypeOfTransport.Tramway:
                    foreach (var tramway in ������.�������)
                    {
                        RollingStockBox.Items.Add(tramway.name);
                    }
                    break;
                case TypeOfTransport.Trolleybus:
                    foreach (var ���������� in ������.�����������)
                    {
                        RollingStockBox.Items.Add(����������.name);
                    }
                    break;
                case TypeOfTransport.Bus:
                    foreach (var ������� in ������.��������)
                    {
                        RollingStockBox.Items.Add(�������.name);
                    }
                    break;
            }
            RollingStockBox.Text = (string)((�����.transport != "") || (�����.transport != "���������") ? �����.transport : RollingStockBox.Items[0]);
            if (!RollingStockBox.Items.Contains(RollingStockBox.Text))
            {
                RollingStockBox.Text = (string)RollingStockBox.Items[0];
            }
        }

        private void RollingStockBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ���������_�����.transport = RollingStockBox.Text;
        }

        private void Objects_EditLocation_Button_Click(object sender, EventArgs e)
        {
            var selectedIndex = Objects_Instance_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            ����������_������ = ���.�������[selectedIndex];
            RegisterPendingAction(new MoveObjectAction(����������_������), true);
            EnableControls(false);
        }

        private void Objects_ShowLocation_Button_Click(object sender, EventArgs e)
        {
            int selectedIndex = Objects_Instance_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            this.�����.cameraPosition.XZPoint = this.���.�������[selectedIndex].position;
        }

        private void Route_Runs_Time_Box_Validated(object sender, EventArgs e)
        {
            this.���������_����.�����_�������� = Route_Runs_Time_Box.Time_Seconds;
        }

        private void TramwayBox_Click(object sender, EventArgs e)
        {
            var selectedIndex = Stops_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            ���.���������[selectedIndex].typeOfTransport[TypeOfTransport.Tramway] = TramwayBox.Checked;
            TrolleybusBox.Checked = ���.���������[selectedIndex].typeOfTransport[TypeOfTransport.Trolleybus];
            BusBox.Checked = ���.���������[selectedIndex].typeOfTransport[TypeOfTransport.Bus];
        }

        private void TrolleybusBox_Click(object sender, EventArgs e)
        {
            var selectedIndex = Stops_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            ���.���������[selectedIndex].typeOfTransport[TypeOfTransport.Trolleybus] = TrolleybusBox.Checked;
            TramwayBox.Checked = ���.���������[selectedIndex].typeOfTransport[TypeOfTransport.Tramway];
            BusBox.Checked = ���.���������[selectedIndex].typeOfTransport[TypeOfTransport.Bus];
        }

        private void TrolleybusAXBox_Click(object sender, EventArgs e)
        {




            this.���������������������������();
        }

        private void BusBox_Click(object sender, EventArgs e)
        {
            var selectedIndex = Stops_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            ���.���������[selectedIndex].typeOfTransport[TypeOfTransport.Bus] = BusBox.Checked;
            TramwayBox.Checked = ���.���������[selectedIndex].typeOfTransport[TypeOfTransport.Tramway];
            TrolleybusBox.Checked = ���.���������[selectedIndex].typeOfTransport[TypeOfTransport.Trolleybus];
        }

        private void StopsButton_Click(object sender, EventArgs e)
        {
            StopListForm stop_list_form = new StopListForm(���������_�������, ���������_����);
            stop_list_form.ShowDialog();
            stop_list_form.Dispose();
        }

        private void Splines_Remove_ButtonClick(object sender, EventArgs e)
        {
            if (Splines_Instance_Box.SelectedIndex < 0)
                return;
            var selectedIndex = Splines_Instance_Box.SelectedIndex;
            this.DoRegisterAction(new RemoveRoadAction(selectedIndex));
            if (selectedIndex >= 0)
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
            Splines_Instance_BoxSelectedIndexChanged(null, null);
        }

        private void Splines_Models_BoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Splines_Models_Box.SelectedIndex < 0) return;
            foreach (var model in SplineLoader.splines)
            {
                if (model.name != Splines_Models_Box.Text)
                    continue;
                this.Road_Button.Pushed = model.type == 1;
                break;
            }
        }

        private void Splines_ChangeModel_ButtonClick(object sender, EventArgs e)
        {
            foreach (var model in SplineLoader.splines)
            {
                if (model.name != Splines_Models_Box.Text)
                    continue;
                if (model.type == ���.���������[Splines_Instance_Box.SelectedIndex].model.type)
                {
                    var road = (Splines_Instance_Box.SelectedIndex < this.���.������.Length) ? this.���.������[Splines_Instance_Box.SelectedIndex] : this.���.������[Splines_Instance_Box.SelectedIndex - this.���.������.Length];
                    road.name = model.name;
                    road.model = null;
                    road.CreateMesh();
                    this.modified = true;
                }
                break;
            }
        }

        private void Splines_ShowLocation_ButtonClick(object sender, EventArgs e)
        {
            int selectedIndex = Splines_Instance_Box.SelectedIndex;
            if (selectedIndex < 0)
                return;
            var road = (selectedIndex < this.���.������.Length) ? this.���.������[selectedIndex] : this.���.������[selectedIndex - this.���.������.Length];
            var point = road.���������������(road.����� / 2.0, 0.0);
            this.�����.cameraPosition.XZPoint = new DoublePoint(point.x, point.y);
            time_color = 2.0;
            this.���������������������������();
        }

        private void Rails_NumericBoxEnterPressed(object sender, EventArgs e)
        {
            ((�����)this.���.���������[Splines_Instance_Box.SelectedIndex]).����������_����������_�������� = this.Rail_Box_NumericBox.Value;
        }

        private void Splines_Instance_BoxSelectedIndexChanged(object sender, EventArgs e)
        {
            Rail_Box_NumericBox.Text = string.Empty;
            int selectedIndex = Splines_Instance_Box.SelectedIndex;
            Rail_Box_NumericBox.Enabled = Splines_Instance_Box.Enabled && (selectedIndex >= 0) && (selectedIndex < this.���.������.Length) && (((�����)this.���.������[selectedIndex]).���������_������.Length > 1);
            if (!Rail_Box_NumericBox.Enabled)
                return;
            Rail_Box_NumericBox.Value = ((�����)this.���.���������[selectedIndex]).����������_����������_��������;
        }

        private void EditorDeactivate(object sender, EventArgs e)
        {
            this.Refresh_Timer.Enabled = false;
        }

        private void EditorActivated(object sender, EventArgs e)
        {
            if (!this.Refresh_Timer.Enabled)
                this.Refresh_Timer.Enabled = true;
        }

        /* public void ReadmeEditor()
        {
            //ReadmeEditor = new ReadmeEditor();
            if ()
            {
                Directory.SetCurrentDirectory(Application.StartupPath + @"..");
                Process.Start("notepad.exe", "Readme_Editor.txt");
            }
            if
            {
                MessageBox.Show("�������� ����� ��� � �� ��� �������!\n������� ������ �� � �������� ������ �����.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                
            }
            
        }*/

        public void ������������������()
        {
            Directory.SetCurrentDirectory(Application.StartupPath);
            using (var ini = new Ini(@".\options.ini", StreamWorkMode.Read))
            {
                ���������.readme_editor = ini.ReadBool("Common", "readme_editor", false);
                ���������.������������ = new Size(ini.ReadInt("Common", "displayWidth", 0x500), ini.ReadInt("Common", "displayHeight", 960));
            }
        }


        public void ������������������()
        {
            Directory.SetCurrentDirectory(Application.StartupPath);
            using (var ini = new Ini(@".\options.ini", StreamWorkMode.Write))
            {
                ini.Write("Common", "readme_editor", ToString());
            }
        }
        void RenderPanelLoad(object sender, EventArgs e)
        {

        }

        public struct ����������������
        {
            public bool readme_editor;
            public Size ������������;
        }
    }
}
