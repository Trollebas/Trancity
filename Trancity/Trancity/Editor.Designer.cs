/*
 * Created by SharpDevelop.
 * User: Andrey
 * Date: 11.08.2009
 * Time: 10:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.ComponentModel;
using System.Windows.Forms;
using System;
using System.Drawing;
using  Trancity;

namespace Trancity
{
	partial class Editor
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private IContainer components = null;

		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		public void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Editor));
			this.mainMenu = new System.Windows.Forms.MainMenu(this.components);
			this.City_Item = new System.Windows.Forms.MenuItem();
			this.New_Item = new System.Windows.Forms.MenuItem();
			this.Open_Item = new System.Windows.Forms.MenuItem();
			this.Save_Item = new System.Windows.Forms.MenuItem();
			this.SaveAs_Item = new System.Windows.Forms.MenuItem();
			this.SeparatorItem1 = new System.Windows.Forms.MenuItem();
			this.Refresh_All_TripStop_Lists_Item = new System.Windows.Forms.MenuItem();
			this.Check_Joints_Item = new System.Windows.Forms.MenuItem();
			this.Find_MinRadius_Item = new System.Windows.Forms.MenuItem();
			this.ComputeAllTime_Item = new System.Windows.Forms.MenuItem();
			this.Run_Item = new System.Windows.Forms.MenuItem();
			this.SeparatorItem2 = new System.Windows.Forms.MenuItem();
			this.Exit_Item = new System.Windows.Forms.MenuItem();
			this.statusBar = new System.Windows.Forms.StatusBar();
			this.Cursor_x_Status = new System.Windows.Forms.StatusBarPanel();
			this.Cursor_y_Status = new System.Windows.Forms.StatusBarPanel();
			this.SeparatorPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.Coord_x1_Status = new System.Windows.Forms.StatusBarPanel();
			this.Coord_y1_Status = new System.Windows.Forms.StatusBarPanel();
			this.Angle1_Status = new System.Windows.Forms.StatusBarPanel();
			this.SeparatorPanel2 = new System.Windows.Forms.StatusBarPanel();
			this.Coord_x2_Status = new System.Windows.Forms.StatusBarPanel();
			this.Coord_y2_Status = new System.Windows.Forms.StatusBarPanel();
			this.Angle2_Status = new System.Windows.Forms.StatusBarPanel();
			this.SeparatorPanel3 = new System.Windows.Forms.StatusBarPanel();
			this.Length_Status = new System.Windows.Forms.StatusBarPanel();
			this.Radius_Status = new System.Windows.Forms.StatusBarPanel();
			this.Angle_Status = new System.Windows.Forms.StatusBarPanel();
			this.Wide0_Status = new System.Windows.Forms.StatusBarPanel();
			this.Wide1_Status = new System.Windows.Forms.StatusBarPanel();
			this.Height0_Status = new System.Windows.Forms.StatusBarPanel();
			this.Height1_Status = new System.Windows.Forms.StatusBarPanel();
			this.SeparatorPanel4 = new System.Windows.Forms.StatusBarPanel();
			this.Maschtab = new System.Windows.Forms.StatusBarPanel();
			this.SeparatorPanel5 = new System.Windows.Forms.StatusBarPanel();
			this.Ugol = new System.Windows.Forms.StatusBarPanel();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.New_Button = new System.Windows.Forms.ToolBarButton();
			this.Open_Button = new System.Windows.Forms.ToolBarButton();
			this.Save_Button = new System.Windows.Forms.ToolBarButton();
			this.SeparatorButton1 = new System.Windows.Forms.ToolBarButton();
			this.SeparatorButton2 = new System.Windows.Forms.ToolBarButton();
			this.Run_Button = new System.Windows.Forms.ToolBarButton();
			this.Play_Button = new System.Windows.Forms.ToolBarButton();
			this.SeparatorButton3 = new System.Windows.Forms.ToolBarButton();
			this.SeparatorButton4 = new System.Windows.Forms.ToolBarButton();
			this.ButtonUndo = new System.Windows.Forms.ToolBarButton();
			this.Edit_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Button = new System.Windows.Forms.ToolBarButton();
			this.Troll_lines_Button = new System.Windows.Forms.ToolBarButton();
			this.SeparatorButton5 = new System.Windows.Forms.ToolBarButton();
			this.SeparatorButton6 = new System.Windows.Forms.ToolBarButton();
			this.Stops_Button = new System.Windows.Forms.ToolBarButton();
			this.Park_Button = new System.Windows.Forms.ToolBarButton();
			this.Route_Button = new System.Windows.Forms.ToolBarButton();
			this.Signals_Button = new System.Windows.Forms.ToolBarButton();
			this.Svetofor_Button = new System.Windows.Forms.ToolBarButton();
			this.Object_Button = new System.Windows.Forms.ToolBarButton();
			this.Info = new System.Windows.Forms.ToolBarButton();
			this.toolBarButton3 = new System.Windows.Forms.ToolBarButton();
			this.SeparatorButton8 = new System.Windows.Forms.ToolBarButton();
			this.Rail_Edit_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_Direct_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_Curve_Button = new System.Windows.Forms.ToolBarButton();
			this.Road_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_попутки_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_попутки1_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_попутки2_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_попутки3_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_встречки_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_встречки1_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_встречки2_Button = new System.Windows.Forms.ToolBarButton();
			this.Rail_Build_встречки3_Button = new System.Windows.Forms.ToolBarButton();
			this.TramWireOverRail = new System.Windows.Forms.ToolBarButton();
			this.TrollWireOverRoad = new System.Windows.Forms.ToolBarButton();
			this.Park_Edit_Button = new System.Windows.Forms.ToolBarButton();
			this.Park_In_Button = new System.Windows.Forms.ToolBarButton();
			this.Park_Out_Button = new System.Windows.Forms.ToolBarButton();
			this.Park_Rails_Button = new System.Windows.Forms.ToolBarButton();
			this.Troll_lines_Edit_Button = new System.Windows.Forms.ToolBarButton();
			this.Troll_lines_Draw_Button = new System.Windows.Forms.ToolBarButton();
			this.Troll_lines_Flag_Button = new System.Windows.Forms.ToolBarButton();
			this.Troll_lines_Doblue = new System.Windows.Forms.ToolBarButton();
			this.Troll_lines_Against = new System.Windows.Forms.ToolBarButton();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.renderPanel = new Engine.Controls.RenderPanel();
			this.object_panel = new System.Windows.Forms.Panel();
			this.Objects_Instance_Box = new System.Windows.Forms.ComboBox();
			this.Objects_Instance_label = new System.Windows.Forms.Label();
			this.Objects_EditLocation_Button = new System.Windows.Forms.Button();
			this.Objects_ShowLocation_Button = new System.Windows.Forms.Button();
			this.Objects_Location_label = new System.Windows.Forms.Label();
			this.Objects_Remove_Button = new System.Windows.Forms.Button();
			this.Objects_Add_Button = new System.Windows.Forms.Button();
			this.Objects_Box = new System.Windows.Forms.ComboBox();
			this.Objects_label = new System.Windows.Forms.Label();
			this.route_panel = new System.Windows.Forms.Panel();
			this.StopsButton = new System.Windows.Forms.Button();
			this.Route_TransportType_Box = new System.Windows.Forms.ComboBox();
			this.Route_TransportType_label = new System.Windows.Forms.Label();
			this.TrolleybusAXBox = new System.Windows.Forms.CheckBox();
			this.Route_ShowNarads_Box = new System.Windows.Forms.CheckBox();
			this.Route_Runs_ComputeTime_Button = new System.Windows.Forms.Button();
			this.Route_Runs_Time_Box = new TimeBox();
			this.Route_Runs_ToParkIndex_UpDown = new System.Windows.Forms.NumericUpDown();
			this.Route_Runs_ToPark_Box = new System.Windows.Forms.CheckBox();
			this.Route_Runs_Park_Box = new System.Windows.Forms.CheckBox();
			this.Route_Runs_Time_label = new System.Windows.Forms.Label();
			this.Route_Runs_ToParkIndex_label = new System.Windows.Forms.Label();
			this.Route_Runs_label = new System.Windows.Forms.Label();
			this.Route_Name_label = new System.Windows.Forms.Label();
			this.Route_Name_Box = new System.Windows.Forms.TextBox();
			this.Route_Runs_Remove_Button = new System.Windows.Forms.Button();
			this.Route_Remove_Button = new System.Windows.Forms.Button();
			this.Route_Runs_Add_Button = new System.Windows.Forms.Button();
			this.Route_Add_Button = new System.Windows.Forms.Button();
			this.Route_ChangeName_Button = new System.Windows.Forms.Button();
			this.Route_label = new System.Windows.Forms.Label();
			this.Route_Runs_Box = new System.Windows.Forms.ComboBox();
			this.Route_Box = new System.Windows.Forms.ComboBox();
			this.svetofor_panel = new System.Windows.Forms.Panel();
			this.Svetofor_Model_Box = new System.Windows.Forms.ComboBox();
			this.Svetofor_Model_label = new System.Windows.Forms.Label();
			this.Svetofor_Svetofor_ArrowRed_Box = new System.Windows.Forms.ComboBox();
			this.Svetofor_Svetofor_ArrowYellow_Box = new System.Windows.Forms.ComboBox();
			this.Svetofor_Svetofor_ArrowGreen_Box = new System.Windows.Forms.ComboBox();
			this.Svetofor_Cycle_Box = new TimeBox();
			this.Svetofor_OfGreen_Box = new TimeBox();
			this.Svetofor_End_Box = new TimeBox();
			this.Svetofor_ToGreen_Box = new TimeBox();
			this.Svetofor_Begin_Box = new TimeBox();
			this.Svetofor_Element_Location_label = new System.Windows.Forms.Label();
			this.Svetofor_Cycle_label = new System.Windows.Forms.Label();
			this.Svetofor_Green_label = new System.Windows.Forms.Label();
			this.Svetofor_Work_label = new System.Windows.Forms.Label();
			this.Svetofor_Remove_Button = new System.Windows.Forms.Button();
			this.Svetofor_Svetofor_ArrowRed_label = new System.Windows.Forms.Label();
			this.Svetofor_Svetofor_ArrowYellow_label = new System.Windows.Forms.Label();
			this.Svetofor_Svetofor_ArrowGreen_label = new System.Windows.Forms.Label();
			this.Svetofor_Element_label = new System.Windows.Forms.Label();
			this.Svetofor_Add_Button = new System.Windows.Forms.Button();
			this.Svetofor_Element_EditLocation_Button = new System.Windows.Forms.Button();
			this.Svetofor_Element_ShowLocation_Button = new System.Windows.Forms.Button();
			this.Svetofor_label = new System.Windows.Forms.Label();
			this.Svetofor_Element_Remove_Button = new System.Windows.Forms.Button();
			this.Svetofor_Box = new System.Windows.Forms.ComboBox();
			this.Svetofor_Element_Box = new System.Windows.Forms.ComboBox();
			this.Svetofor_Signal_Add_Button = new System.Windows.Forms.Button();
			this.Svetofor_Svetofor_Add_Button = new System.Windows.Forms.Button();
			this.splines_panel = new System.Windows.Forms.Panel();
			this.Rail_Box_NumericBox = new Common.NumericBox();
			this.Spline_Select_mode_Box = new System.Windows.Forms.CheckBox();
			this.Splines_Instance_Box = new System.Windows.Forms.ComboBox();
			this.Splines_Instance_label = new System.Windows.Forms.Label();
			this.Splines_ShowLocation_Button = new System.Windows.Forms.Button();
			this.Rail_Box_dist_Label = new System.Windows.Forms.Label();
			this.Splines_Location_label = new System.Windows.Forms.Label();
			this.Splines_ChangeModel_Button = new System.Windows.Forms.Button();
			this.Splines_Remove_Button = new System.Windows.Forms.Button();
			this.Splines_Models_Box = new System.Windows.Forms.ComboBox();
			this.Splines_label = new System.Windows.Forms.Label();
			this.signals_panel = new System.Windows.Forms.Panel();
			this.Signals_Model_Box = new System.Windows.Forms.ComboBox();
			this.Signals_Model_label = new System.Windows.Forms.Label();
			this.Signals_Element_Minus_Box = new System.Windows.Forms.CheckBox();
			this.Signals_Bound_UpDown = new System.Windows.Forms.NumericUpDown();
			this.Signals_Element_Location_label = new System.Windows.Forms.Label();
			this.Signals_Bound_label = new System.Windows.Forms.Label();
			this.Signals_Remove_Button = new System.Windows.Forms.Button();
			this.Signals_Element_label = new System.Windows.Forms.Label();
			this.Signals_Add_Button = new System.Windows.Forms.Button();
			this.Signals_Element_EditLocation_Button = new System.Windows.Forms.Button();
			this.Signals_Element_ShowLocation_Button = new System.Windows.Forms.Button();
			this.Signals_label = new System.Windows.Forms.Label();
			this.Signals_Element_Remove_Button = new System.Windows.Forms.Button();
			this.Signals_Box = new System.Windows.Forms.ComboBox();
			this.Signals_Element_Box = new System.Windows.Forms.ComboBox();
			this.Signals_Element_AddSignal_Button = new System.Windows.Forms.Button();
			this.Signals_Element_AddContact_Button = new System.Windows.Forms.Button();
			this.stops_panel = new System.Windows.Forms.Panel();
			this.Stops_Model_Box = new System.Windows.Forms.ComboBox();
			this.Stops_Model_label = new System.Windows.Forms.Label();
			this.TypeOfTransportBox = new System.Windows.Forms.GroupBox();
			this.BusBox = new System.Windows.Forms.CheckBox();
			this.TrolleybusBox = new System.Windows.Forms.CheckBox();
			this.TramwayBox = new System.Windows.Forms.CheckBox();
			this.Stops_Location_label = new System.Windows.Forms.Label();
			this.Stops_Name_label = new System.Windows.Forms.Label();
			this.Stops_Name_Box = new System.Windows.Forms.TextBox();
			this.Stops_Remove_Button = new System.Windows.Forms.Button();
			this.Stops_Add_Button = new System.Windows.Forms.Button();
			this.Stops_EditLocation_Button = new System.Windows.Forms.Button();
			this.Stops_ShowLocation_Button = new System.Windows.Forms.Button();
			this.Stops_ChangeName_Button = new System.Windows.Forms.Button();
			this.Stops_label = new System.Windows.Forms.Label();
			this.Stops_Box = new System.Windows.Forms.ComboBox();
			this.park_panel = new System.Windows.Forms.Panel();
			this.Park_Name_label = new System.Windows.Forms.Label();
			this.Park_Name_Box = new System.Windows.Forms.TextBox();
			this.Park_Remove_Button = new System.Windows.Forms.Button();
			this.Park_ChangeName_Button = new System.Windows.Forms.Button();
			this.Park_Add_Button = new System.Windows.Forms.Button();
			this.Park_label = new System.Windows.Forms.Label();
			this.Park_Box = new System.Windows.Forms.ComboBox();
			this.Refresh_Timer = new System.Windows.Forms.Timer(this.components);
			this.Sizable_Panel = new System.Windows.Forms.Panel();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.edit_panel = new System.Windows.Forms.Panel();
			this.narad_panel = new System.Windows.Forms.Panel();
			this.RollingStockBox = new System.Windows.Forms.ComboBox();
			this.Transport_label = new System.Windows.Forms.Label();
			this.Narad_Runs_Time2_Box = new TimeBox();
			this.Narad_Runs_Time1_Box = new TimeBox();
			this.Narad_Runs_Time2_label = new System.Windows.Forms.Label();
			this.Narad_Runs_Time1_label = new System.Windows.Forms.Label();
			this.Narad_Runs_label = new System.Windows.Forms.Label();
			this.Narad_Park_label = new System.Windows.Forms.Label();
			this.Narad_Runs_Run_label = new System.Windows.Forms.Label();
			this.Narad_Name_label = new System.Windows.Forms.Label();
			this.Narad_Name_Box = new System.Windows.Forms.TextBox();
			this.Narad_Runs_Remove_Button = new System.Windows.Forms.Button();
			this.Narad_Remove_Button = new System.Windows.Forms.Button();
			this.Narad_Runs_Add_Button = new System.Windows.Forms.Button();
			this.Narad_Add_Button = new System.Windows.Forms.Button();
			this.Narad_ChangeName_Button = new System.Windows.Forms.Button();
			this.Narad_label = new System.Windows.Forms.Label();
			this.Narad_Park_Box = new System.Windows.Forms.ComboBox();
			this.Narad_Runs_Run_Box = new System.Windows.Forms.ComboBox();
			this.Narad_Runs_Box = new System.Windows.Forms.ComboBox();
			this.Narad_Box = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.Cursor_x_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Cursor_y_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SeparatorPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Coord_x1_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Coord_y1_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Angle1_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SeparatorPanel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Coord_x2_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Coord_y2_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Angle2_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SeparatorPanel3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Length_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Radius_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Angle_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Wide0_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Wide1_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Height0_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Height1_Status)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SeparatorPanel4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Maschtab)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SeparatorPanel5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.Ugol)).BeginInit();
			this.object_panel.SuspendLayout();
			this.route_panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Route_Runs_ToParkIndex_UpDown)).BeginInit();
			this.svetofor_panel.SuspendLayout();
			this.splines_panel.SuspendLayout();
			this.signals_panel.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.Signals_Bound_UpDown)).BeginInit();
			this.stops_panel.SuspendLayout();
			this.TypeOfTransportBox.SuspendLayout();
			this.park_panel.SuspendLayout();
			this.Sizable_Panel.SuspendLayout();
			this.narad_panel.SuspendLayout();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.City_Item});
			// 
			// City_Item
			// 
			this.City_Item.Index = 0;
			this.City_Item.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
			this.New_Item,
			this.Open_Item,
			this.Save_Item,
			this.SaveAs_Item,
			this.SeparatorItem1,
			this.Refresh_All_TripStop_Lists_Item,
			this.Check_Joints_Item,
			this.Find_MinRadius_Item,
			this.ComputeAllTime_Item,
			this.Run_Item,
			this.SeparatorItem2,
			this.Exit_Item});
			this.City_Item.Text = "Карта";
			// 
			// New_Item
			// 
			this.New_Item.Index = 0;
			this.New_Item.Shortcut = System.Windows.Forms.Shortcut.CtrlN;
			this.New_Item.Text = "Новый";
			this.New_Item.Click += new System.EventHandler(this.New_Item_Click);
			// 
			// Open_Item
			// 
			this.Open_Item.Index = 1;
			this.Open_Item.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.Open_Item.Text = "Открыть...";
			this.Open_Item.Click += new System.EventHandler(this.Open_Item_Click);
			// 
			// Save_Item
			// 
			this.Save_Item.Index = 2;
			this.Save_Item.Shortcut = System.Windows.Forms.Shortcut.CtrlS;
			this.Save_Item.Text = "Сохранить";
			this.Save_Item.Click += new System.EventHandler(this.Save_Item_Click);
			// 
			// SaveAs_Item
			// 
			this.SaveAs_Item.Index = 3;
			this.SaveAs_Item.Text = "Сохранить как...";
			this.SaveAs_Item.Click += new System.EventHandler(this.SaveAs_Item_Click);
			// 
			// SeparatorItem1
			// 
			this.SeparatorItem1.Index = 4;
			this.SeparatorItem1.Text = "-";
			// 
			// Refresh_All_TripStop_Lists_Item
			// 
			this.Refresh_All_TripStop_Lists_Item.Index = 5;
			this.Refresh_All_TripStop_Lists_Item.Shortcut = System.Windows.Forms.Shortcut.F3;
			this.Refresh_All_TripStop_Lists_Item.Text = "Обновить все остановки рейсов...";
			this.Refresh_All_TripStop_Lists_Item.Click += new System.EventHandler(this.Refresh_All_TripStop_Lists_Item_Click);
			// 
			// Check_Joints_Item
			// 
			this.Check_Joints_Item.Index = 6;
			this.Check_Joints_Item.Shortcut = System.Windows.Forms.Shortcut.F4;
			this.Check_Joints_Item.Text = "Проверить стыки...";
			this.Check_Joints_Item.Click += new System.EventHandler(this.Check_стыки_Item_Click);
			// 
			// Find_MinRadius_Item
			// 
			this.Find_MinRadius_Item.Index = 7;
			this.Find_MinRadius_Item.Text = "Найти минимальный радиус кривой...";
			this.Find_MinRadius_Item.Click += new System.EventHandler(this.Find_MinRadius_Item_Click);
			// 
			// ComputeAllTime_Item
			// 
			this.ComputeAllTime_Item.Index = 8;
			this.ComputeAllTime_Item.Text = "Посчитать время всех рейсов...";
			this.ComputeAllTime_Item.Click += new System.EventHandler(this.ComputeAllTime_Item_Click);
			// 
			// Run_Item
			// 
			this.Run_Item.Index = 9;
			this.Run_Item.Shortcut = System.Windows.Forms.Shortcut.F5;
			this.Run_Item.Text = "Запустить";
			this.Run_Item.Click += new System.EventHandler(this.RunItemClick);
			// 
			// SeparatorItem2
			// 
			this.SeparatorItem2.Index = 10;
			this.SeparatorItem2.Text = "-";
			// 
			// Exit_Item
			// 
			this.Exit_Item.Index = 11;
			this.Exit_Item.Shortcut = System.Windows.Forms.Shortcut.AltF4;
			this.Exit_Item.Text = "Выход";
			this.Exit_Item.Click += new System.EventHandler(this.Exit_Item_Click);
			// 
			// statusBar
			// 
			this.statusBar.Location = new System.Drawing.Point(0, 851);
			this.statusBar.Name = "statusBar";
			this.statusBar.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
			this.Cursor_x_Status,
			this.Cursor_y_Status,
			this.SeparatorPanel1,
			this.Coord_x1_Status,
			this.Coord_y1_Status,
			this.Angle1_Status,
			this.SeparatorPanel2,
			this.Coord_x2_Status,
			this.Coord_y2_Status,
			this.Angle2_Status,
			this.SeparatorPanel3,
			this.Length_Status,
			this.Radius_Status,
			this.Angle_Status,
			this.Wide0_Status,
			this.Wide1_Status,
			this.Height0_Status,
			this.Height1_Status,
			this.SeparatorPanel4,
			this.Maschtab,
			this.SeparatorPanel5,
			this.Ugol});
			this.statusBar.ShowPanels = true;
			this.statusBar.Size = new System.Drawing.Size(850, 22);
			this.statusBar.TabIndex = 0;
			// 
			// Cursor_x_Status
			// 
			this.Cursor_x_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Cursor_x_Status.Name = "Cursor_x_Status";
			this.Cursor_x_Status.Width = 92;
			// 
			// Cursor_y_Status
			// 
			this.Cursor_y_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Cursor_y_Status.Name = "Cursor_y_Status";
			this.Cursor_y_Status.Width = 92;
			// 
			// SeparatorPanel1
			// 
			this.SeparatorPanel1.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.SeparatorPanel1.Name = "SeparatorPanel1";
			this.SeparatorPanel1.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			this.SeparatorPanel1.Width = 24;
			// 
			// Coord_x1_Status
			// 
			this.Coord_x1_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Coord_x1_Status.Name = "Coord_x1_Status";
			this.Coord_x1_Status.Width = 92;
			// 
			// Coord_y1_Status
			// 
			this.Coord_y1_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Coord_y1_Status.Name = "Coord_y1_Status";
			this.Coord_y1_Status.Width = 92;
			// 
			// Angle1_Status
			// 
			this.Angle1_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Angle1_Status.Name = "Angle1_Status";
			this.Angle1_Status.Width = 42;
			// 
			// SeparatorPanel2
			// 
			this.SeparatorPanel2.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.SeparatorPanel2.Name = "SeparatorPanel2";
			this.SeparatorPanel2.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			this.SeparatorPanel2.Width = 24;
			// 
			// Coord_x2_Status
			// 
			this.Coord_x2_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Coord_x2_Status.Name = "Coord_x2_Status";
			this.Coord_x2_Status.Width = 92;
			// 
			// Coord_y2_Status
			// 
			this.Coord_y2_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Coord_y2_Status.Name = "Coord_y2_Status";
			this.Coord_y2_Status.Width = 92;
			// 
			// Angle2_Status
			// 
			this.Angle2_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Angle2_Status.Name = "Angle2_Status";
			this.Angle2_Status.Width = 42;
			// 
			// SeparatorPanel3
			// 
			this.SeparatorPanel3.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.SeparatorPanel3.Name = "SeparatorPanel3";
			this.SeparatorPanel3.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			this.SeparatorPanel3.Width = 24;
			// 
			// Length_Status
			// 
			this.Length_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Length_Status.Name = "Length_Status";
			this.Length_Status.Width = 80;
			// 
			// Radius_Status
			// 
			this.Radius_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Radius_Status.Name = "Radius_Status";
			this.Radius_Status.Width = 80;
			// 
			// Angle_Status
			// 
			this.Angle_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Angle_Status.Name = "Angle_Status";
			this.Angle_Status.Width = 42;
			// 
			// Wide0_Status
			// 
			this.Wide0_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Wide0_Status.Name = "Wide0_Status";
			this.Wide0_Status.Width = 80;
			// 
			// Wide1_Status
			// 
			this.Wide1_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Wide1_Status.Name = "Wide1_Status";
			this.Wide1_Status.Width = 80;
			// 
			// Height0_Status
			// 
			this.Height0_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Height0_Status.Name = "Height0_Status";
			this.Height0_Status.Width = 80;
			// 
			// Height1_Status
			// 
			this.Height1_Status.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Height1_Status.Name = "Height1_Status";
			this.Height1_Status.Width = 80;
			// 
			// SeparatorPanel4
			// 
			this.SeparatorPanel4.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.SeparatorPanel4.Name = "SeparatorPanel4";
			this.SeparatorPanel4.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			this.SeparatorPanel4.Width = 25;
			// 
			// Maschtab
			// 
			this.Maschtab.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Maschtab.Name = "Maschtab";
			this.Maschtab.Text = "Выбранный масштаб: 10.0";
			this.Maschtab.Width = 170;
			// 
			// SeparatorPanel5
			// 
			this.SeparatorPanel5.BorderStyle = System.Windows.Forms.StatusBarPanelBorderStyle.None;
			this.SeparatorPanel5.Name = "SeparatorPanel5";
			this.SeparatorPanel5.Style = System.Windows.Forms.StatusBarPanelStyle.OwnerDraw;
			this.SeparatorPanel5.Width = 25;
			// 
			// Ugol
			// 
			this.Ugol.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.Ugol.Name = "Ugol";
			this.Ugol.Text = "Угол: 0°";
			this.Ugol.Width = 120;
			// 
			// toolBar
			// 
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
			this.New_Button,
			this.Open_Button,
			this.Save_Button,
			this.SeparatorButton1,
			this.SeparatorButton2,
			this.Run_Button,
			this.Play_Button,
			this.SeparatorButton3,
			this.SeparatorButton4,
			this.ButtonUndo,
			this.Edit_Button,
			this.Rail_Button,
			this.Troll_lines_Button,
			this.SeparatorButton5,
			this.SeparatorButton6,
			this.Stops_Button,
			this.Park_Button,
			this.Route_Button,
			this.Signals_Button,
			this.Svetofor_Button,
			this.Object_Button,
			this.Info,
			this.toolBarButton3,
			this.SeparatorButton8,
			this.Rail_Edit_Button,
			this.Rail_Build_Direct_Button,
			this.Rail_Build_Curve_Button,
			this.Road_Button,
			this.Rail_Build_попутки_Button,
			this.Rail_Build_попутки1_Button,
			this.Rail_Build_попутки2_Button,
			this.Rail_Build_попутки3_Button,
			this.Rail_Build_встречки_Button,
			this.Rail_Build_встречки1_Button,
			this.Rail_Build_встречки2_Button,
			this.Rail_Build_встречки3_Button,
			this.TramWireOverRail,
			this.TrollWireOverRoad,
			this.Park_Edit_Button,
			this.Park_In_Button,
			this.Park_Out_Button,
			this.Park_Rails_Button,
			this.Troll_lines_Edit_Button,
			this.Troll_lines_Draw_Button,
			this.Troll_lines_Flag_Button,
			this.Troll_lines_Doblue,
			this.Troll_lines_Against});
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imageList;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(1008, 28);
			this.toolBar.TabIndex = 1;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// New_Button
			// 
			this.New_Button.ImageIndex = 2;
			this.New_Button.Name = "New_Button";
			this.New_Button.ToolTipText = "Новая карта";
			// 
			// Open_Button
			// 
			this.Open_Button.ImageIndex = 0;
			this.Open_Button.Name = "Open_Button";
			this.Open_Button.ToolTipText = "Открыть карту";
			// 
			// Save_Button
			// 
			this.Save_Button.ImageIndex = 1;
			this.Save_Button.Name = "Save_Button";
			this.Save_Button.ToolTipText = "Сохранить карту";
			// 
			// SeparatorButton1
			// 
			this.SeparatorButton1.Name = "SeparatorButton1";
			this.SeparatorButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// SeparatorButton2
			// 
			this.SeparatorButton2.Name = "SeparatorButton2";
			this.SeparatorButton2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// Run_Button
			// 
			this.Run_Button.ImageIndex = 3;
			this.Run_Button.Name = "Run_Button";
			this.Run_Button.ToolTipText = "Запустить игру на карте";
			// 
			// Play_Button
			// 
			this.Play_Button.Enabled = false;
			this.Play_Button.ImageIndex = 3;
			this.Play_Button.Name = "Play_Button";
			this.Play_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Play_Button.ToolTipText = "Play";
			this.Play_Button.Visible = false;
			// 
			// SeparatorButton3
			// 
			this.SeparatorButton3.Name = "SeparatorButton3";
			this.SeparatorButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// SeparatorButton4
			// 
			this.SeparatorButton4.Name = "SeparatorButton4";
			this.SeparatorButton4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// ButtonUndo
			// 
			this.ButtonUndo.ImageIndex = 26;
			this.ButtonUndo.Name = "ButtonUndo";
			this.ButtonUndo.ToolTipText = "Отменить";
			// 
			// Edit_Button
			// 
			this.Edit_Button.ImageIndex = 4;
			this.Edit_Button.Name = "Edit_Button";
			this.Edit_Button.Pushed = true;
			// 
			// Rail_Button
			// 
			this.Rail_Button.ImageIndex = 5;
			this.Rail_Button.Name = "Rail_Button";
			this.Rail_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Rail_Button.ToolTipText = "Рельсы и дороги";
			// 
			// Troll_lines_Button
			// 
			this.Troll_lines_Button.ImageIndex = 28;
			this.Troll_lines_Button.Name = "Troll_lines_Button";
			this.Troll_lines_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Troll_lines_Button.ToolTipText = "Контактные провода троллейбуса";
			// 
			// SeparatorButton5
			// 
			this.SeparatorButton5.Name = "SeparatorButton5";
			this.SeparatorButton5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// SeparatorButton6
			// 
			this.SeparatorButton6.Name = "SeparatorButton6";
			this.SeparatorButton6.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// Stops_Button
			// 
			this.Stops_Button.ImageIndex = 7;
			this.Stops_Button.Name = "Stops_Button";
			this.Stops_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Stops_Button.ToolTipText = "Остановки";
			// 
			// Park_Button
			// 
			this.Park_Button.ImageIndex = 8;
			this.Park_Button.Name = "Park_Button";
			this.Park_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Park_Button.ToolTipText = "Парки";
			// 
			// Route_Button
			// 
			this.Route_Button.ImageIndex = 9;
			this.Route_Button.Name = "Route_Button";
			this.Route_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Route_Button.ToolTipText = "Маршруты";
			// 
			// Signals_Button
			// 
			this.Signals_Button.ImageIndex = 10;
			this.Signals_Button.Name = "Signals_Button";
			this.Signals_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Signals_Button.ToolTipText = "Сигнальные системы";
			// 
			// Svetofor_Button
			// 
			this.Svetofor_Button.ImageIndex = 11;
			this.Svetofor_Button.Name = "Svetofor_Button";
			this.Svetofor_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Svetofor_Button.ToolTipText = "Светофоры";
			// 
			// Object_Button
			// 
			this.Object_Button.ImageIndex = 12;
			this.Object_Button.Name = "Object_Button";
			this.Object_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Object_Button.ToolTipText = "Объекты";
			// 
			// Info
			// 
			this.Info.ImageIndex = 27;
			this.Info.Name = "Info";
			this.Info.ToolTipText = "Помощь по редактору";
			// 
			// toolBarButton3
			// 
			this.toolBarButton3.Name = "toolBarButton3";
			this.toolBarButton3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// SeparatorButton8
			// 
			this.SeparatorButton8.Name = "SeparatorButton8";
			this.SeparatorButton8.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// Rail_Edit_Button
			// 
			this.Rail_Edit_Button.ImageIndex = 4;
			this.Rail_Edit_Button.Name = "Rail_Edit_Button";
			this.Rail_Edit_Button.Pushed = true;
			this.Rail_Edit_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Rail_Edit_Button.ToolTipText = "Редактировать";
			// 
			// Rail_Build_Direct_Button
			// 
			this.Rail_Build_Direct_Button.ImageIndex = 13;
			this.Rail_Build_Direct_Button.Name = "Rail_Build_Direct_Button";
			this.Rail_Build_Direct_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Rail_Build_Direct_Button.ToolTipText = "Строить прямые участки";
			// 
			// Rail_Build_Curve_Button
			// 
			this.Rail_Build_Curve_Button.ImageIndex = 14;
			this.Rail_Build_Curve_Button.Name = "Rail_Build_Curve_Button";
			this.Rail_Build_Curve_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Rail_Build_Curve_Button.ToolTipText = "Строить поворотные участки";
			// 
			// Road_Button
			// 
			this.Road_Button.ImageIndex = 16;
			this.Road_Button.Name = "Road_Button";
			this.Road_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Road_Button.ToolTipText = "Дороги";
			// 
			// Rail_Build_попутки_Button
			// 
			this.Rail_Build_попутки_Button.ImageIndex = 20;
			this.Rail_Build_попутки_Button.Name = "Rail_Build_попутки_Button";
			this.Rail_Build_попутки_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// Rail_Build_попутки1_Button
			// 
			this.Rail_Build_попутки1_Button.ImageIndex = 22;
			this.Rail_Build_попутки1_Button.Name = "Rail_Build_попутки1_Button";
			this.Rail_Build_попутки1_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// Rail_Build_попутки2_Button
			// 
			this.Rail_Build_попутки2_Button.ImageIndex = 23;
			this.Rail_Build_попутки2_Button.Name = "Rail_Build_попутки2_Button";
			this.Rail_Build_попутки2_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// Rail_Build_попутки3_Button
			// 
			this.Rail_Build_попутки3_Button.ImageIndex = 24;
			this.Rail_Build_попутки3_Button.Name = "Rail_Build_попутки3_Button";
			this.Rail_Build_попутки3_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// Rail_Build_встречки_Button
			// 
			this.Rail_Build_встречки_Button.ImageIndex = 21;
			this.Rail_Build_встречки_Button.Name = "Rail_Build_встречки_Button";
			this.Rail_Build_встречки_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// Rail_Build_встречки1_Button
			// 
			this.Rail_Build_встречки1_Button.ImageIndex = 22;
			this.Rail_Build_встречки1_Button.Name = "Rail_Build_встречки1_Button";
			this.Rail_Build_встречки1_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// Rail_Build_встречки2_Button
			// 
			this.Rail_Build_встречки2_Button.ImageIndex = 23;
			this.Rail_Build_встречки2_Button.Name = "Rail_Build_встречки2_Button";
			this.Rail_Build_встречки2_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// Rail_Build_встречки3_Button
			// 
			this.Rail_Build_встречки3_Button.ImageIndex = 24;
			this.Rail_Build_встречки3_Button.Name = "Rail_Build_встречки3_Button";
			this.Rail_Build_встречки3_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// TramWireOverRail
			// 
			this.TramWireOverRail.ImageIndex = 29;
			this.TramWireOverRail.Name = "TramWireOverRail";
			this.TramWireOverRail.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.TramWireOverRail.ToolTipText = "Трамвайная КС";
			// 
			// TrollWireOverRoad
			// 
			this.TrollWireOverRoad.ImageIndex = 6;
			this.TrollWireOverRoad.Name = "TrollWireOverRoad";
			this.TrollWireOverRoad.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.TrollWireOverRoad.ToolTipText = "Троллейбусная КС";
			// 
			// Park_Edit_Button
			// 
			this.Park_Edit_Button.ImageIndex = 4;
			this.Park_Edit_Button.Name = "Park_Edit_Button";
			this.Park_Edit_Button.Pushed = true;
			this.Park_Edit_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			// 
			// Park_In_Button
			// 
			this.Park_In_Button.ImageIndex = 17;
			this.Park_In_Button.Name = "Park_In_Button";
			this.Park_In_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Park_In_Button.ToolTipText = "Въезд";
			// 
			// Park_Out_Button
			// 
			this.Park_Out_Button.ImageIndex = 18;
			this.Park_Out_Button.Name = "Park_Out_Button";
			this.Park_Out_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Park_Out_Button.ToolTipText = "Выезд";
			// 
			// Park_Rails_Button
			// 
			this.Park_Rails_Button.ImageIndex = 19;
			this.Park_Rails_Button.Name = "Park_Rails_Button";
			this.Park_Rails_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Park_Rails_Button.ToolTipText = "Пути стоянки";
			// 
			// Troll_lines_Edit_Button
			// 
			this.Troll_lines_Edit_Button.ImageIndex = 4;
			this.Troll_lines_Edit_Button.Name = "Troll_lines_Edit_Button";
			this.Troll_lines_Edit_Button.Pushed = true;
			this.Troll_lines_Edit_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Troll_lines_Edit_Button.ToolTipText = "Редактировать КС";
			// 
			// Troll_lines_Draw_Button
			// 
			this.Troll_lines_Draw_Button.ImageIndex = 6;
			this.Troll_lines_Draw_Button.Name = "Troll_lines_Draw_Button";
			this.Troll_lines_Draw_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Troll_lines_Draw_Button.ToolTipText = "Троллейбусная КС";
			// 
			// Troll_lines_Flag_Button
			// 
			this.Troll_lines_Flag_Button.ImageIndex = 22;
			this.Troll_lines_Flag_Button.Name = "Troll_lines_Flag_Button";
			this.Troll_lines_Flag_Button.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Troll_lines_Flag_Button.ToolTipText = "Трамвайная КС";
			// 
			// Troll_lines_Doblue
			// 
			this.Troll_lines_Doblue.ImageIndex = 20;
			this.Troll_lines_Doblue.Name = "Troll_lines_Doblue";
			this.Troll_lines_Doblue.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Troll_lines_Doblue.ToolTipText = "Две линии КС";
			// 
			// Troll_lines_Against
			// 
			this.Troll_lines_Against.ImageIndex = 21;
			this.Troll_lines_Against.Name = "Troll_lines_Against";
			this.Troll_lines_Against.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
			this.Troll_lines_Against.ToolTipText = "КС паралельно";
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList.Images.SetKeyName(0, "Open_Button.PNG");
			this.imageList.Images.SetKeyName(1, "Save_Button.PNG");
			this.imageList.Images.SetKeyName(2, "New_Button.PNG");
			this.imageList.Images.SetKeyName(3, "Run_Button.PNG");
			this.imageList.Images.SetKeyName(4, "Arrow_Button.PNG");
			this.imageList.Images.SetKeyName(5, "Rail_Button.PNG");
			this.imageList.Images.SetKeyName(6, "Contact_wire_Button.PNG");
			this.imageList.Images.SetKeyName(7, "Stop_Button.PNG");
			this.imageList.Images.SetKeyName(8, "Park_Button.PNG");
			this.imageList.Images.SetKeyName(9, "Route_Button.PNG");
			this.imageList.Images.SetKeyName(10, "Signal_Button.PNG");
			this.imageList.Images.SetKeyName(11, "Svetofor_Button.PNG");
			this.imageList.Images.SetKeyName(12, "Object_Button.PNG");
			this.imageList.Images.SetKeyName(13, "Direct_Button.PNG");
			this.imageList.Images.SetKeyName(14, "Curve_Button.PNG");
			this.imageList.Images.SetKeyName(15, "Both_Button.PNG");
			this.imageList.Images.SetKeyName(16, "Road_Button.PNG");
			this.imageList.Images.SetKeyName(17, "In_Button.PNG");
			this.imageList.Images.SetKeyName(18, "Out_Button.PNG");
			this.imageList.Images.SetKeyName(19, "Path_Button.PNG");
			this.imageList.Images.SetKeyName(20, "Duoble1.PNG");
			this.imageList.Images.SetKeyName(21, "Duoble0.PNG");
			this.imageList.Images.SetKeyName(22, "1.PNG");
			this.imageList.Images.SetKeyName(23, "2.PNG");
			this.imageList.Images.SetKeyName(24, "3.PNG");
			this.imageList.Images.SetKeyName(25, "кс.png");
			this.imageList.Images.SetKeyName(26, "Undo.ico");
			this.imageList.Images.SetKeyName(27, "info.ico");
			this.imageList.Images.SetKeyName(28, "Contact_Wire");
			this.imageList.Images.SetKeyName(29, "Catenary_Tram");
			// 
			// renderPanel
			// 
			this.renderPanel.Location = new System.Drawing.Point(0, 0);
			this.renderPanel.Name = "renderPanel";
			this.renderPanel.Size = new System.Drawing.Size(1600, 1200);
			this.renderPanel.TabIndex = 2;
			this.renderPanel.Load += new System.EventHandler(this.RenderPanelLoad);
			this.renderPanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
			this.renderPanel.MouseLeave += new System.EventHandler(this.panel_MouseLeave);
			this.renderPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
			this.renderPanel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
			// 
			// object_panel
			// 
			this.object_panel.Controls.Add(this.Objects_Instance_Box);
			this.object_panel.Controls.Add(this.Objects_Instance_label);
			this.object_panel.Controls.Add(this.Objects_EditLocation_Button);
			this.object_panel.Controls.Add(this.Objects_ShowLocation_Button);
			this.object_panel.Controls.Add(this.Objects_Location_label);
			this.object_panel.Controls.Add(this.Objects_Remove_Button);
			this.object_panel.Controls.Add(this.Objects_Add_Button);
			this.object_panel.Controls.Add(this.Objects_Box);
			this.object_panel.Controls.Add(this.Objects_label);
			this.object_panel.Location = new System.Drawing.Point(3, 637);
			this.object_panel.Name = "object_panel";
			this.object_panel.Size = new System.Drawing.Size(158, 291);
			this.object_panel.TabIndex = 6;
			this.object_panel.Visible = false;
			// 
			// Objects_Instance_Box
			// 
			this.Objects_Instance_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Objects_Instance_Box.FormattingEnabled = true;
			this.Objects_Instance_Box.Location = new System.Drawing.Point(11, 79);
			this.Objects_Instance_Box.Name = "Objects_Instance_Box";
			this.Objects_Instance_Box.Size = new System.Drawing.Size(136, 21);
			this.Objects_Instance_Box.TabIndex = 8;
			this.Objects_Instance_Box.SelectedIndexChanged += new System.EventHandler(this.Object_Instance_Box_SelectedIndexChanged);
			// 
			// Objects_Instance_label
			// 
			this.Objects_Instance_label.AutoSize = true;
			this.Objects_Instance_label.Location = new System.Drawing.Point(11, 63);
			this.Objects_Instance_label.Name = "Objects_Instance_label";
			this.Objects_Instance_label.Size = new System.Drawing.Size(75, 13);
			this.Objects_Instance_label.TabIndex = 7;
			this.Objects_Instance_label.Text = "Экземпляры:";
			// 
			// Objects_EditLocation_Button
			// 
			this.Objects_EditLocation_Button.Location = new System.Drawing.Point(11, 238);
			this.Objects_EditLocation_Button.Name = "Objects_EditLocation_Button";
			this.Objects_EditLocation_Button.Size = new System.Drawing.Size(136, 23);
			this.Objects_EditLocation_Button.TabIndex = 6;
			this.Objects_EditLocation_Button.Text = "Изменить";
			this.Objects_EditLocation_Button.UseVisualStyleBackColor = true;
			this.Objects_EditLocation_Button.Click += new System.EventHandler(this.Objects_EditLocation_Button_Click);
			// 
			// Objects_ShowLocation_Button
			// 
			this.Objects_ShowLocation_Button.Location = new System.Drawing.Point(11, 209);
			this.Objects_ShowLocation_Button.Name = "Objects_ShowLocation_Button";
			this.Objects_ShowLocation_Button.Size = new System.Drawing.Size(136, 23);
			this.Objects_ShowLocation_Button.TabIndex = 5;
			this.Objects_ShowLocation_Button.Text = "Показать";
			this.Objects_ShowLocation_Button.UseVisualStyleBackColor = true;
			this.Objects_ShowLocation_Button.Click += new System.EventHandler(this.Objects_ShowLocation_Button_Click);
			// 
			// Objects_Location_label
			// 
			this.Objects_Location_label.AutoSize = true;
			this.Objects_Location_label.Location = new System.Drawing.Point(11, 193);
			this.Objects_Location_label.Name = "Objects_Location_label";
			this.Objects_Location_label.Size = new System.Drawing.Size(98, 13);
			this.Objects_Location_label.TabIndex = 4;
			this.Objects_Location_label.Text = "Местоположение:";
			// 
			// Objects_Remove_Button
			// 
			this.Objects_Remove_Button.Location = new System.Drawing.Point(11, 135);
			this.Objects_Remove_Button.Name = "Objects_Remove_Button";
			this.Objects_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Objects_Remove_Button.TabIndex = 3;
			this.Objects_Remove_Button.Text = "Удалить объект";
			this.Objects_Remove_Button.UseVisualStyleBackColor = true;
			this.Objects_Remove_Button.Click += new System.EventHandler(this.ObjectsRemoveButtonClick);
			// 
			// Objects_Add_Button
			// 
			this.Objects_Add_Button.Location = new System.Drawing.Point(11, 106);
			this.Objects_Add_Button.Name = "Objects_Add_Button";
			this.Objects_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Objects_Add_Button.TabIndex = 2;
			this.Objects_Add_Button.Text = "Добавить объект";
			this.Objects_Add_Button.UseVisualStyleBackColor = true;
			this.Objects_Add_Button.Click += new System.EventHandler(this.Objects_Add_Button_Click);
			// 
			// Objects_Box
			// 
			this.Objects_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Objects_Box.FormattingEnabled = true;
			this.Objects_Box.Location = new System.Drawing.Point(11, 36);
			this.Objects_Box.Name = "Objects_Box";
			this.Objects_Box.Size = new System.Drawing.Size(136, 21);
			this.Objects_Box.TabIndex = 1;
			this.Objects_Box.SelectedIndexChanged += new System.EventHandler(this.Objects_Box_SelectedIndexChanged);
			// 
			// Objects_label
			// 
			this.Objects_label.AutoSize = true;
			this.Objects_label.Location = new System.Drawing.Point(11, 20);
			this.Objects_label.Name = "Objects_label";
			this.Objects_label.Size = new System.Drawing.Size(56, 13);
			this.Objects_label.TabIndex = 0;
			this.Objects_label.Text = "Объекты:";
			// 
			// route_panel
			// 
			this.route_panel.Controls.Add(this.StopsButton);
			this.route_panel.Controls.Add(this.Route_TransportType_Box);
			this.route_panel.Controls.Add(this.Route_TransportType_label);
			this.route_panel.Controls.Add(this.TrolleybusAXBox);
			this.route_panel.Controls.Add(this.Route_ShowNarads_Box);
			this.route_panel.Controls.Add(this.Route_Runs_ComputeTime_Button);
			this.route_panel.Controls.Add(this.Route_Runs_Time_Box);
			this.route_panel.Controls.Add(this.Route_Runs_ToParkIndex_UpDown);
			this.route_panel.Controls.Add(this.Route_Runs_ToPark_Box);
			this.route_panel.Controls.Add(this.Route_Runs_Park_Box);
			this.route_panel.Controls.Add(this.Route_Runs_Time_label);
			this.route_panel.Controls.Add(this.Route_Runs_ToParkIndex_label);
			this.route_panel.Controls.Add(this.Route_Runs_label);
			this.route_panel.Controls.Add(this.Route_Name_label);
			this.route_panel.Controls.Add(this.Route_Name_Box);
			this.route_panel.Controls.Add(this.Route_Runs_Remove_Button);
			this.route_panel.Controls.Add(this.Route_Remove_Button);
			this.route_panel.Controls.Add(this.Route_Runs_Add_Button);
			this.route_panel.Controls.Add(this.Route_Add_Button);
			this.route_panel.Controls.Add(this.Route_ChangeName_Button);
			this.route_panel.Controls.Add(this.Route_label);
			this.route_panel.Controls.Add(this.Route_Runs_Box);
			this.route_panel.Controls.Add(this.Route_Box);
			this.route_panel.Location = new System.Drawing.Point(528, 11);
			this.route_panel.Name = "route_panel";
			this.route_panel.Padding = new System.Windows.Forms.Padding(8, 20, 8, 0);
			this.route_panel.Size = new System.Drawing.Size(158, 641);
			this.route_panel.TabIndex = 5;
			this.route_panel.Visible = false;
			// 
			// StopsButton
			// 
			this.StopsButton.Location = new System.Drawing.Point(11, 549);
			this.StopsButton.Name = "StopsButton";
			this.StopsButton.Size = new System.Drawing.Size(136, 23);
			this.StopsButton.TabIndex = 14;
			this.StopsButton.Text = "Остановки...";
			this.StopsButton.UseVisualStyleBackColor = true;
			this.StopsButton.Click += new System.EventHandler(this.StopsButton_Click);
			// 
			// Route_TransportType_Box
			// 
			this.Route_TransportType_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Route_TransportType_Box.FormattingEnabled = true;
			this.Route_TransportType_Box.Items.AddRange(new object[] {
			"Трамвай",
			"Троллейбус",
			"Автобус"});
			this.Route_TransportType_Box.Location = new System.Drawing.Point(11, 233);
			this.Route_TransportType_Box.Name = "Route_TransportType_Box";
			this.Route_TransportType_Box.Size = new System.Drawing.Size(136, 21);
			this.Route_TransportType_Box.TabIndex = 11;
			this.Route_TransportType_Box.SelectedIndexChanged += new System.EventHandler(this.RouteTransportTypeBoxSelectedIndexChanged);
			// 
			// Route_TransportType_label
			// 
			this.Route_TransportType_label.AutoSize = true;
			this.Route_TransportType_label.Location = new System.Drawing.Point(11, 217);
			this.Route_TransportType_label.Name = "Route_TransportType_label";
			this.Route_TransportType_label.Size = new System.Drawing.Size(90, 13);
			this.Route_TransportType_label.TabIndex = 10;
			this.Route_TransportType_label.Text = "Вид транспорта:";
			// 
			// TrolleybusAXBox
			// 
			this.TrolleybusAXBox.AutoSize = true;
			this.TrolleybusAXBox.Location = new System.Drawing.Point(11, 260);
			this.TrolleybusAXBox.Name = "TrolleybusAXBox";
			this.TrolleybusAXBox.Size = new System.Drawing.Size(182, 27);
			this.TrolleybusAXBox.TabIndex = 10;
			this.TrolleybusAXBox.Text = "Маршрут с АХ";
			this.TrolleybusAXBox.UseVisualStyleBackColor = true;
			this.TrolleybusAXBox.Click += new System.EventHandler(this.TrolleybusAXBox_Click);
			// 
			// Route_ShowNarads_Box
			// 
			this.Route_ShowNarads_Box.AutoSize = true;
			this.Route_ShowNarads_Box.Location = new System.Drawing.Point(11, 578);
			this.Route_ShowNarads_Box.Name = "Route_ShowNarads_Box";
			this.Route_ShowNarads_Box.Size = new System.Drawing.Size(116, 17);
			this.Route_ShowNarads_Box.TabIndex = 9;
			this.Route_ShowNarads_Box.Text = "Показать наряды";
			this.Route_ShowNarads_Box.UseVisualStyleBackColor = true;
			this.Route_ShowNarads_Box.CheckedChanged += new System.EventHandler(this.RouteShowNaradsBoxCheckedChanged);
			// 
			// Route_Runs_ComputeTime_Button
			// 
			this.Route_Runs_ComputeTime_Button.Location = new System.Drawing.Point(11, 520);
			this.Route_Runs_ComputeTime_Button.Name = "Route_Runs_ComputeTime_Button";
			this.Route_Runs_ComputeTime_Button.Size = new System.Drawing.Size(136, 23);
			this.Route_Runs_ComputeTime_Button.TabIndex = 8;
			this.Route_Runs_ComputeTime_Button.Text = "Посчитать время...";
			this.Route_Runs_ComputeTime_Button.UseVisualStyleBackColor = true;
			this.Route_Runs_ComputeTime_Button.Click += new System.EventHandler(this.Route_Runs_ComputeTime_Button_Click);
			// 
			// Route_Runs_Time_Box
			// 
			this.Route_Runs_Time_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Route_Runs_Time_Box.Hours = 0;
			this.Route_Runs_Time_Box.Location = new System.Drawing.Point(11, 493);
			this.Route_Runs_Time_Box.MinimumSize = new System.Drawing.Size(40, 21);
			this.Route_Runs_Time_Box.Minutes = 0;
			this.Route_Runs_Time_Box.Name = "Route_Runs_Time_Box";
			this.Route_Runs_Time_Box.Seconds = 0;
			this.Route_Runs_Time_Box.Size = new System.Drawing.Size(136, 21);
			this.Route_Runs_Time_Box.TabIndex = 7;
			this.Route_Runs_Time_Box.Time = new System.DateTime(((long)(0)));
			this.Route_Runs_Time_Box.Time_Minutes = 0;
			this.Route_Runs_Time_Box.Time_Seconds = 0;
			this.Route_Runs_Time_Box.ViewSeconds = true;
			this.Route_Runs_Time_Box.Validated += new System.EventHandler(this.Route_Runs_Time_Box_Validated);
			// 
			// Route_Runs_ToParkIndex_UpDown
			// 
			this.Route_Runs_ToParkIndex_UpDown.Location = new System.Drawing.Point(11, 453);
			this.Route_Runs_ToParkIndex_UpDown.Name = "Route_Runs_ToParkIndex_UpDown";
			this.Route_Runs_ToParkIndex_UpDown.Size = new System.Drawing.Size(136, 20);
			this.Route_Runs_ToParkIndex_UpDown.TabIndex = 6;
			this.Route_Runs_ToParkIndex_UpDown.ValueChanged += new System.EventHandler(this.RouteRunsToParkIndexUpDownValueChanged);
			// 
			// Route_Runs_ToPark_Box
			// 
			this.Route_Runs_ToPark_Box.AutoSize = true;
			this.Route_Runs_ToPark_Box.Location = new System.Drawing.Point(11, 404);
			this.Route_Runs_ToPark_Box.Name = "Route_Runs_ToPark_Box";
			this.Route_Runs_ToPark_Box.Size = new System.Drawing.Size(60, 17);
			this.Route_Runs_ToPark_Box.TabIndex = 5;
			this.Route_Runs_ToPark_Box.Text = "В парк";
			this.Route_Runs_ToPark_Box.UseVisualStyleBackColor = true;
			this.Route_Runs_ToPark_Box.CheckedChanged += new System.EventHandler(this.RouteRunsToParkBoxCheckedChanged);
			// 
			// Route_Runs_Park_Box
			// 
			this.Route_Runs_Park_Box.AutoSize = true;
			this.Route_Runs_Park_Box.Location = new System.Drawing.Point(11, 381);
			this.Route_Runs_Park_Box.Name = "Route_Runs_Park_Box";
			this.Route_Runs_Park_Box.Size = new System.Drawing.Size(105, 17);
			this.Route_Runs_Park_Box.TabIndex = 5;
			this.Route_Runs_Park_Box.Text = "Парковый рейс";
			this.Route_Runs_Park_Box.UseVisualStyleBackColor = true;
			this.Route_Runs_Park_Box.CheckedChanged += new System.EventHandler(this.Route_Runs_Park_Box_CheckedChanged);
			// 
			// Route_Runs_Time_label
			// 
			this.Route_Runs_Time_label.AutoSize = true;
			this.Route_Runs_Time_label.Location = new System.Drawing.Point(11, 477);
			this.Route_Runs_Time_label.Name = "Route_Runs_Time_label";
			this.Route_Runs_Time_label.Size = new System.Drawing.Size(77, 13);
			this.Route_Runs_Time_label.TabIndex = 4;
			this.Route_Runs_Time_label.Text = "Время в пути:";
			// 
			// Route_Runs_ToParkIndex_label
			// 
			this.Route_Runs_ToParkIndex_label.AutoSize = true;
			this.Route_Runs_ToParkIndex_label.Location = new System.Drawing.Point(11, 424);
			this.Route_Runs_ToParkIndex_label.Name = "Route_Runs_ToParkIndex_label";
			this.Route_Runs_ToParkIndex_label.Size = new System.Drawing.Size(113, 26);
			this.Route_Runs_ToParkIndex_label.TabIndex = 4;
			this.Route_Runs_ToParkIndex_label.Text = "Номер пути, начиная\r\nс которого в парк:";
			// 
			// Route_Runs_label
			// 
			this.Route_Runs_label.AutoSize = true;
			this.Route_Runs_label.Location = new System.Drawing.Point(11, 280);
			this.Route_Runs_label.Name = "Route_Runs_label";
			this.Route_Runs_label.Size = new System.Drawing.Size(102, 13);
			this.Route_Runs_label.TabIndex = 4;
			this.Route_Runs_label.Text = "Трассы маршрута:";
			// 
			// Route_Name_label
			// 
			this.Route_Name_label.AutoSize = true;
			this.Route_Name_label.Location = new System.Drawing.Point(11, 131);
			this.Route_Name_label.Name = "Route_Name_label";
			this.Route_Name_label.Size = new System.Drawing.Size(113, 13);
			this.Route_Name_label.TabIndex = 4;
			this.Route_Name_label.Text = "Название маршрута:";
			// 
			// Route_Name_Box
			// 
			this.Route_Name_Box.Location = new System.Drawing.Point(11, 147);
			this.Route_Name_Box.Name = "Route_Name_Box";
			this.Route_Name_Box.Size = new System.Drawing.Size(136, 20);
			this.Route_Name_Box.TabIndex = 3;
			this.Route_Name_Box.ModifiedChanged += new System.EventHandler(this.RouteNameBoxModifiedChanged);
			// 
			// Route_Runs_Remove_Button
			// 
			this.Route_Runs_Remove_Button.Location = new System.Drawing.Point(11, 352);
			this.Route_Runs_Remove_Button.Name = "Route_Runs_Remove_Button";
			this.Route_Runs_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Route_Runs_Remove_Button.TabIndex = 2;
			this.Route_Runs_Remove_Button.Text = "Удалить трассу";
			this.Route_Runs_Remove_Button.UseVisualStyleBackColor = true;
			this.Route_Runs_Remove_Button.Click += new System.EventHandler(this.Route_Runs_Remove_Button_Click);
			// 
			// Route_Remove_Button
			// 
			this.Route_Remove_Button.Location = new System.Drawing.Point(11, 92);
			this.Route_Remove_Button.Name = "Route_Remove_Button";
			this.Route_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Route_Remove_Button.TabIndex = 2;
			this.Route_Remove_Button.Text = "Удалить маршрут";
			this.Route_Remove_Button.UseVisualStyleBackColor = true;
			this.Route_Remove_Button.Click += new System.EventHandler(this.RouteRemoveButtonClick);
			// 
			// Route_Runs_Add_Button
			// 
			this.Route_Runs_Add_Button.Location = new System.Drawing.Point(11, 323);
			this.Route_Runs_Add_Button.Name = "Route_Runs_Add_Button";
			this.Route_Runs_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Route_Runs_Add_Button.TabIndex = 2;
			this.Route_Runs_Add_Button.Text = "Добавить трассу";
			this.Route_Runs_Add_Button.UseVisualStyleBackColor = true;
			this.Route_Runs_Add_Button.Click += new System.EventHandler(this.RouteRunsAddButtonClick);
			// 
			// Route_Add_Button
			// 
			this.Route_Add_Button.Location = new System.Drawing.Point(11, 63);
			this.Route_Add_Button.Name = "Route_Add_Button";
			this.Route_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Route_Add_Button.TabIndex = 2;
			this.Route_Add_Button.Text = "Добавить маршрут";
			this.Route_Add_Button.UseVisualStyleBackColor = true;
			this.Route_Add_Button.Click += new System.EventHandler(this.Route_Add_Button_Click);
			// 
			// Route_ChangeName_Button
			// 
			this.Route_ChangeName_Button.Location = new System.Drawing.Point(11, 174);
			this.Route_ChangeName_Button.Name = "Route_ChangeName_Button";
			this.Route_ChangeName_Button.Size = new System.Drawing.Size(136, 23);
			this.Route_ChangeName_Button.TabIndex = 2;
			this.Route_ChangeName_Button.Text = "Изменить название";
			this.Route_ChangeName_Button.UseVisualStyleBackColor = true;
			this.Route_ChangeName_Button.Click += new System.EventHandler(this.RouteChangeNameButtonClick);
			// 
			// Route_label
			// 
			this.Route_label.AutoSize = true;
			this.Route_label.Location = new System.Drawing.Point(11, 20);
			this.Route_label.Name = "Route_label";
			this.Route_label.Size = new System.Drawing.Size(63, 13);
			this.Route_label.TabIndex = 1;
			this.Route_label.Text = "Маршруты:";
			// 
			// Route_Runs_Box
			// 
			this.Route_Runs_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Route_Runs_Box.FormattingEnabled = true;
			this.Route_Runs_Box.Location = new System.Drawing.Point(11, 296);
			this.Route_Runs_Box.Name = "Route_Runs_Box";
			this.Route_Runs_Box.Size = new System.Drawing.Size(136, 21);
			this.Route_Runs_Box.TabIndex = 0;
			this.Route_Runs_Box.SelectedIndexChanged += new System.EventHandler(this.Route_Runs_Box_SelectedIndexChanged);
			// 
			// Route_Box
			// 
			this.Route_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Route_Box.FormattingEnabled = true;
			this.Route_Box.Location = new System.Drawing.Point(11, 36);
			this.Route_Box.Name = "Route_Box";
			this.Route_Box.Size = new System.Drawing.Size(136, 21);
			this.Route_Box.TabIndex = 0;
			this.Route_Box.SelectedIndexChanged += new System.EventHandler(this.RouteBoxSelectedIndexChanged);
			// 
			// svetofor_panel
			// 
			this.svetofor_panel.Controls.Add(this.Svetofor_Model_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_Model_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Svetofor_ArrowRed_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_Svetofor_ArrowYellow_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_Svetofor_ArrowGreen_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_Cycle_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_OfGreen_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_End_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_ToGreen_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_Begin_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_Element_Location_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Cycle_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Green_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Work_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Remove_Button);
			this.svetofor_panel.Controls.Add(this.Svetofor_Svetofor_ArrowRed_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Svetofor_ArrowYellow_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Svetofor_ArrowGreen_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Element_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Add_Button);
			this.svetofor_panel.Controls.Add(this.Svetofor_Element_EditLocation_Button);
			this.svetofor_panel.Controls.Add(this.Svetofor_Element_ShowLocation_Button);
			this.svetofor_panel.Controls.Add(this.Svetofor_label);
			this.svetofor_panel.Controls.Add(this.Svetofor_Element_Remove_Button);
			this.svetofor_panel.Controls.Add(this.Svetofor_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_Element_Box);
			this.svetofor_panel.Controls.Add(this.Svetofor_Signal_Add_Button);
			this.svetofor_panel.Controls.Add(this.Svetofor_Svetofor_Add_Button);
			this.svetofor_panel.Location = new System.Drawing.Point(183, 11);
			this.svetofor_panel.Name = "svetofor_panel";
			this.svetofor_panel.Padding = new System.Windows.Forms.Padding(8, 20, 8, 0);
			this.svetofor_panel.Size = new System.Drawing.Size(158, 796);
			this.svetofor_panel.TabIndex = 5;
			this.svetofor_panel.Visible = false;
			// 
			// Svetofor_Model_Box
			// 
			this.Svetofor_Model_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Svetofor_Model_Box.FormattingEnabled = true;
			this.Svetofor_Model_Box.Location = new System.Drawing.Point(11, 653);
			this.Svetofor_Model_Box.Name = "Svetofor_Model_Box";
			this.Svetofor_Model_Box.Size = new System.Drawing.Size(136, 21);
			this.Svetofor_Model_Box.TabIndex = 11;
			// 
			// Svetofor_Model_label
			// 
			this.Svetofor_Model_label.AutoSize = true;
			this.Svetofor_Model_label.Location = new System.Drawing.Point(11, 637);
			this.Svetofor_Model_label.Name = "Svetofor_Model_label";
			this.Svetofor_Model_label.Size = new System.Drawing.Size(49, 13);
			this.Svetofor_Model_label.TabIndex = 10;
			this.Svetofor_Model_label.Text = "Модель:";
			// 
			// Svetofor_Svetofor_ArrowRed_Box
			// 
			this.Svetofor_Svetofor_ArrowRed_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Svetofor_Svetofor_ArrowRed_Box.FormattingEnabled = true;
			this.Svetofor_Svetofor_ArrowRed_Box.Location = new System.Drawing.Point(11, 511);
			this.Svetofor_Svetofor_ArrowRed_Box.Name = "Svetofor_Svetofor_ArrowRed_Box";
			this.Svetofor_Svetofor_ArrowRed_Box.Size = new System.Drawing.Size(136, 21);
			this.Svetofor_Svetofor_ArrowRed_Box.TabIndex = 8;
			this.Svetofor_Svetofor_ArrowRed_Box.SelectedIndexChanged += new System.EventHandler(this.Svetofor_Svetofor_ArrowRed_Box_SelectedIndexChanged);
			// 
			// Svetofor_Svetofor_ArrowYellow_Box
			// 
			this.Svetofor_Svetofor_ArrowYellow_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Svetofor_Svetofor_ArrowYellow_Box.FormattingEnabled = true;
			this.Svetofor_Svetofor_ArrowYellow_Box.Location = new System.Drawing.Point(11, 469);
			this.Svetofor_Svetofor_ArrowYellow_Box.Name = "Svetofor_Svetofor_ArrowYellow_Box";
			this.Svetofor_Svetofor_ArrowYellow_Box.Size = new System.Drawing.Size(136, 21);
			this.Svetofor_Svetofor_ArrowYellow_Box.TabIndex = 8;
			this.Svetofor_Svetofor_ArrowYellow_Box.SelectedIndexChanged += new System.EventHandler(this.Svetofor_Svetofor_ArrowYellow_Box_SelectedIndexChanged);
			// 
			// Svetofor_Svetofor_ArrowGreen_Box
			// 
			this.Svetofor_Svetofor_ArrowGreen_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Svetofor_Svetofor_ArrowGreen_Box.FormattingEnabled = true;
			this.Svetofor_Svetofor_ArrowGreen_Box.Location = new System.Drawing.Point(11, 429);
			this.Svetofor_Svetofor_ArrowGreen_Box.Name = "Svetofor_Svetofor_ArrowGreen_Box";
			this.Svetofor_Svetofor_ArrowGreen_Box.Size = new System.Drawing.Size(136, 21);
			this.Svetofor_Svetofor_ArrowGreen_Box.TabIndex = 8;
			this.Svetofor_Svetofor_ArrowGreen_Box.SelectedIndexChanged += new System.EventHandler(this.Svetofor_Svetofor_ArrowGreen_Box_SelectedIndexChanged);
			// 
			// Svetofor_Cycle_Box
			// 
			this.Svetofor_Cycle_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Svetofor_Cycle_Box.Hours = 0;
			this.Svetofor_Cycle_Box.Location = new System.Drawing.Point(11, 187);
			this.Svetofor_Cycle_Box.MinimumSize = new System.Drawing.Size(40, 21);
			this.Svetofor_Cycle_Box.Minutes = 0;
			this.Svetofor_Cycle_Box.Name = "Svetofor_Cycle_Box";
			this.Svetofor_Cycle_Box.Seconds = 0;
			this.Svetofor_Cycle_Box.Size = new System.Drawing.Size(136, 21);
			this.Svetofor_Cycle_Box.TabIndex = 7;
			this.Svetofor_Cycle_Box.Time = new System.DateTime(((long)(0)));
			this.Svetofor_Cycle_Box.Time_Minutes = 0;
			this.Svetofor_Cycle_Box.Time_Seconds = 0;
			this.Svetofor_Cycle_Box.ViewSeconds = true;
			this.Svetofor_Cycle_Box.TimeChanged += new System.EventHandler(this.Svetofor_Cycle_Box_TimeChanged);
			// 
			// Svetofor_OfGreen_Box
			// 
			this.Svetofor_OfGreen_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Svetofor_OfGreen_Box.Hours = 0;
			this.Svetofor_OfGreen_Box.Location = new System.Drawing.Point(82, 240);
			this.Svetofor_OfGreen_Box.MinimumSize = new System.Drawing.Size(40, 21);
			this.Svetofor_OfGreen_Box.Minutes = 0;
			this.Svetofor_OfGreen_Box.Name = "Svetofor_OfGreen_Box";
			this.Svetofor_OfGreen_Box.Seconds = 0;
			this.Svetofor_OfGreen_Box.Size = new System.Drawing.Size(65, 21);
			this.Svetofor_OfGreen_Box.TabIndex = 7;
			this.Svetofor_OfGreen_Box.Time = new System.DateTime(((long)(0)));
			this.Svetofor_OfGreen_Box.Time_Minutes = 0;
			this.Svetofor_OfGreen_Box.Time_Seconds = 0;
			this.Svetofor_OfGreen_Box.ViewSeconds = true;
			this.Svetofor_OfGreen_Box.TimeChanged += new System.EventHandler(this.Svetofor_OfGreen_Box_TimeChanged);
			// 
			// Svetofor_End_Box
			// 
			this.Svetofor_End_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Svetofor_End_Box.Hours = 0;
			this.Svetofor_End_Box.Location = new System.Drawing.Point(82, 147);
			this.Svetofor_End_Box.MinimumSize = new System.Drawing.Size(40, 21);
			this.Svetofor_End_Box.Minutes = 0;
			this.Svetofor_End_Box.Name = "Svetofor_End_Box";
			this.Svetofor_End_Box.Seconds = 0;
			this.Svetofor_End_Box.Size = new System.Drawing.Size(65, 21);
			this.Svetofor_End_Box.TabIndex = 7;
			this.Svetofor_End_Box.Time = new System.DateTime(((long)(0)));
			this.Svetofor_End_Box.Time_Minutes = 0;
			this.Svetofor_End_Box.Time_Seconds = 0;
			this.Svetofor_End_Box.ViewSeconds = true;
			this.Svetofor_End_Box.TimeChanged += new System.EventHandler(this.Svetofor_End_Box_TimeChanged);
			// 
			// Svetofor_ToGreen_Box
			// 
			this.Svetofor_ToGreen_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Svetofor_ToGreen_Box.Hours = 0;
			this.Svetofor_ToGreen_Box.Location = new System.Drawing.Point(11, 240);
			this.Svetofor_ToGreen_Box.MinimumSize = new System.Drawing.Size(40, 21);
			this.Svetofor_ToGreen_Box.Minutes = 0;
			this.Svetofor_ToGreen_Box.Name = "Svetofor_ToGreen_Box";
			this.Svetofor_ToGreen_Box.Seconds = 0;
			this.Svetofor_ToGreen_Box.Size = new System.Drawing.Size(65, 21);
			this.Svetofor_ToGreen_Box.TabIndex = 7;
			this.Svetofor_ToGreen_Box.Time = new System.DateTime(((long)(0)));
			this.Svetofor_ToGreen_Box.Time_Minutes = 0;
			this.Svetofor_ToGreen_Box.Time_Seconds = 0;
			this.Svetofor_ToGreen_Box.ViewSeconds = true;
			this.Svetofor_ToGreen_Box.TimeChanged += new System.EventHandler(this.Svetofor_ToGreen_Box_TimeChanged);
			// 
			// Svetofor_Begin_Box
			// 
			this.Svetofor_Begin_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Svetofor_Begin_Box.Hours = 0;
			this.Svetofor_Begin_Box.Location = new System.Drawing.Point(11, 147);
			this.Svetofor_Begin_Box.MinimumSize = new System.Drawing.Size(40, 21);
			this.Svetofor_Begin_Box.Minutes = 0;
			this.Svetofor_Begin_Box.Name = "Svetofor_Begin_Box";
			this.Svetofor_Begin_Box.Seconds = 0;
			this.Svetofor_Begin_Box.Size = new System.Drawing.Size(65, 21);
			this.Svetofor_Begin_Box.TabIndex = 7;
			this.Svetofor_Begin_Box.Time = new System.DateTime(((long)(0)));
			this.Svetofor_Begin_Box.Time_Minutes = 0;
			this.Svetofor_Begin_Box.Time_Seconds = 0;
			this.Svetofor_Begin_Box.ViewSeconds = true;
			this.Svetofor_Begin_Box.TimeChanged += new System.EventHandler(this.Svetofor_Begin_Box_TimeChanged);
			// 
			// Svetofor_Element_Location_label
			// 
			this.Svetofor_Element_Location_label.AutoSize = true;
			this.Svetofor_Element_Location_label.Location = new System.Drawing.Point(11, 548);
			this.Svetofor_Element_Location_label.Name = "Svetofor_Element_Location_label";
			this.Svetofor_Element_Location_label.Size = new System.Drawing.Size(98, 13);
			this.Svetofor_Element_Location_label.TabIndex = 4;
			this.Svetofor_Element_Location_label.Text = "Местоположение:";
			// 
			// Svetofor_Cycle_label
			// 
			this.Svetofor_Cycle_label.AutoSize = true;
			this.Svetofor_Cycle_label.Location = new System.Drawing.Point(11, 171);
			this.Svetofor_Cycle_label.Name = "Svetofor_Cycle_label";
			this.Svetofor_Cycle_label.Size = new System.Drawing.Size(107, 13);
			this.Svetofor_Cycle_label.TabIndex = 4;
			this.Svetofor_Cycle_label.Text = "Светофорный цикл:";
			// 
			// Svetofor_Green_label
			// 
			this.Svetofor_Green_label.AutoSize = true;
			this.Svetofor_Green_label.Location = new System.Drawing.Point(11, 211);
			this.Svetofor_Green_label.Name = "Svetofor_Green_label";
			this.Svetofor_Green_label.Size = new System.Drawing.Size(122, 26);
			this.Svetofor_Green_label.TabIndex = 4;
			this.Svetofor_Green_label.Text = "Время и длительность\r\nзелёного сигнала:";
			// 
			// Svetofor_Work_label
			// 
			this.Svetofor_Work_label.AutoSize = true;
			this.Svetofor_Work_label.Location = new System.Drawing.Point(11, 131);
			this.Svetofor_Work_label.Name = "Svetofor_Work_label";
			this.Svetofor_Work_label.Size = new System.Drawing.Size(83, 13);
			this.Svetofor_Work_label.TabIndex = 4;
			this.Svetofor_Work_label.Text = "Время работы:";
			// 
			// Svetofor_Remove_Button
			// 
			this.Svetofor_Remove_Button.Location = new System.Drawing.Point(11, 92);
			this.Svetofor_Remove_Button.Name = "Svetofor_Remove_Button";
			this.Svetofor_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Svetofor_Remove_Button.TabIndex = 2;
			this.Svetofor_Remove_Button.Text = "Удалить систему";
			this.Svetofor_Remove_Button.UseVisualStyleBackColor = true;
			this.Svetofor_Remove_Button.Click += new System.EventHandler(this.Svetofor_Remove_Button_Click);
			// 
			// Svetofor_Svetofor_ArrowRed_label
			// 
			this.Svetofor_Svetofor_ArrowRed_label.AutoSize = true;
			this.Svetofor_Svetofor_ArrowRed_label.Location = new System.Drawing.Point(11, 495);
			this.Svetofor_Svetofor_ArrowRed_label.Name = "Svetofor_Svetofor_ArrowRed_label";
			this.Svetofor_Svetofor_ArrowRed_label.Size = new System.Drawing.Size(97, 13);
			this.Svetofor_Svetofor_ArrowRed_label.TabIndex = 4;
			this.Svetofor_Svetofor_ArrowRed_label.Text = "Красная стрелка:";
			// 
			// Svetofor_Svetofor_ArrowYellow_label
			// 
			this.Svetofor_Svetofor_ArrowYellow_label.AutoSize = true;
			this.Svetofor_Svetofor_ArrowYellow_label.Location = new System.Drawing.Point(11, 453);
			this.Svetofor_Svetofor_ArrowYellow_label.Name = "Svetofor_Svetofor_ArrowYellow_label";
			this.Svetofor_Svetofor_ArrowYellow_label.Size = new System.Drawing.Size(94, 13);
			this.Svetofor_Svetofor_ArrowYellow_label.TabIndex = 4;
			this.Svetofor_Svetofor_ArrowYellow_label.Text = "Жёлтая стрелка:";
			// 
			// Svetofor_Svetofor_ArrowGreen_label
			// 
			this.Svetofor_Svetofor_ArrowGreen_label.AutoSize = true;
			this.Svetofor_Svetofor_ArrowGreen_label.Location = new System.Drawing.Point(11, 413);
			this.Svetofor_Svetofor_ArrowGreen_label.Name = "Svetofor_Svetofor_ArrowGreen_label";
			this.Svetofor_Svetofor_ArrowGreen_label.Size = new System.Drawing.Size(97, 13);
			this.Svetofor_Svetofor_ArrowGreen_label.TabIndex = 4;
			this.Svetofor_Svetofor_ArrowGreen_label.Text = "Зелёная стрелка:";
			// 
			// Svetofor_Element_label
			// 
			this.Svetofor_Element_label.AutoSize = true;
			this.Svetofor_Element_label.Location = new System.Drawing.Point(11, 276);
			this.Svetofor_Element_label.Name = "Svetofor_Element_label";
			this.Svetofor_Element_label.Size = new System.Drawing.Size(62, 13);
			this.Svetofor_Element_label.TabIndex = 4;
			this.Svetofor_Element_label.Text = "Элементы:";
			// 
			// Svetofor_Add_Button
			// 
			this.Svetofor_Add_Button.Location = new System.Drawing.Point(11, 63);
			this.Svetofor_Add_Button.Name = "Svetofor_Add_Button";
			this.Svetofor_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Svetofor_Add_Button.TabIndex = 2;
			this.Svetofor_Add_Button.Text = "Добавить систему";
			this.Svetofor_Add_Button.UseVisualStyleBackColor = true;
			this.Svetofor_Add_Button.Click += new System.EventHandler(this.Svetofor_Add_Button_Click);
			// 
			// Svetofor_Element_EditLocation_Button
			// 
			this.Svetofor_Element_EditLocation_Button.Location = new System.Drawing.Point(11, 593);
			this.Svetofor_Element_EditLocation_Button.Name = "Svetofor_Element_EditLocation_Button";
			this.Svetofor_Element_EditLocation_Button.Size = new System.Drawing.Size(136, 23);
			this.Svetofor_Element_EditLocation_Button.TabIndex = 2;
			this.Svetofor_Element_EditLocation_Button.Text = "Изменить";
			this.Svetofor_Element_EditLocation_Button.UseVisualStyleBackColor = true;
			this.Svetofor_Element_EditLocation_Button.Click += new System.EventHandler(this.Svetofor_Element_EditLocation_Button_Click);
			// 
			// Svetofor_Element_ShowLocation_Button
			// 
			this.Svetofor_Element_ShowLocation_Button.Location = new System.Drawing.Point(11, 564);
			this.Svetofor_Element_ShowLocation_Button.Name = "Svetofor_Element_ShowLocation_Button";
			this.Svetofor_Element_ShowLocation_Button.Size = new System.Drawing.Size(136, 23);
			this.Svetofor_Element_ShowLocation_Button.TabIndex = 2;
			this.Svetofor_Element_ShowLocation_Button.Text = "Показать";
			this.Svetofor_Element_ShowLocation_Button.UseVisualStyleBackColor = true;
			this.Svetofor_Element_ShowLocation_Button.Click += new System.EventHandler(this.Svetofor_Element_ShowLocation_Button_Click);
			// 
			// Svetofor_label
			// 
			this.Svetofor_label.AutoSize = true;
			this.Svetofor_label.Location = new System.Drawing.Point(11, 20);
			this.Svetofor_label.Name = "Svetofor_label";
			this.Svetofor_label.Size = new System.Drawing.Size(128, 13);
			this.Svetofor_label.TabIndex = 1;
			this.Svetofor_label.Text = "Светофорные системы:";
			// 
			// Svetofor_Element_Remove_Button
			// 
			this.Svetofor_Element_Remove_Button.Location = new System.Drawing.Point(11, 377);
			this.Svetofor_Element_Remove_Button.Name = "Svetofor_Element_Remove_Button";
			this.Svetofor_Element_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Svetofor_Element_Remove_Button.TabIndex = 2;
			this.Svetofor_Element_Remove_Button.Text = "Удалить элемент";
			this.Svetofor_Element_Remove_Button.UseVisualStyleBackColor = true;
			this.Svetofor_Element_Remove_Button.Click += new System.EventHandler(this.Svetofor_Element_Remove_Button_Click);
			// 
			// Svetofor_Box
			// 
			this.Svetofor_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Svetofor_Box.FormattingEnabled = true;
			this.Svetofor_Box.Location = new System.Drawing.Point(11, 36);
			this.Svetofor_Box.Name = "Svetofor_Box";
			this.Svetofor_Box.Size = new System.Drawing.Size(136, 21);
			this.Svetofor_Box.TabIndex = 0;
			this.Svetofor_Box.SelectedIndexChanged += new System.EventHandler(this.Svetofor_Box_SelectedIndexChanged);
			// 
			// Svetofor_Element_Box
			// 
			this.Svetofor_Element_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Svetofor_Element_Box.FormattingEnabled = true;
			this.Svetofor_Element_Box.Location = new System.Drawing.Point(11, 292);
			this.Svetofor_Element_Box.Name = "Svetofor_Element_Box";
			this.Svetofor_Element_Box.Size = new System.Drawing.Size(136, 21);
			this.Svetofor_Element_Box.TabIndex = 0;
			this.Svetofor_Element_Box.SelectedIndexChanged += new System.EventHandler(this.Svetofor_Element_Box_SelectedIndexChanged);
			// 
			// Svetofor_Signal_Add_Button
			// 
			this.Svetofor_Signal_Add_Button.Location = new System.Drawing.Point(11, 348);
			this.Svetofor_Signal_Add_Button.Name = "Svetofor_Signal_Add_Button";
			this.Svetofor_Signal_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Svetofor_Signal_Add_Button.TabIndex = 2;
			this.Svetofor_Signal_Add_Button.Text = "Добавить сигнал";
			this.Svetofor_Signal_Add_Button.UseVisualStyleBackColor = true;
			this.Svetofor_Signal_Add_Button.Click += new System.EventHandler(this.Svetofor_Signal_Add_Button_Click);
			// 
			// Svetofor_Svetofor_Add_Button
			// 
			this.Svetofor_Svetofor_Add_Button.Location = new System.Drawing.Point(11, 319);
			this.Svetofor_Svetofor_Add_Button.Name = "Svetofor_Svetofor_Add_Button";
			this.Svetofor_Svetofor_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Svetofor_Svetofor_Add_Button.TabIndex = 2;
			this.Svetofor_Svetofor_Add_Button.Text = "Добавить светофор";
			this.Svetofor_Svetofor_Add_Button.UseVisualStyleBackColor = true;
			this.Svetofor_Svetofor_Add_Button.Click += new System.EventHandler(this.Svetofor_Svetofor_Add_Button_Click);
			// 
			// splines_panel
			// 
			this.splines_panel.Controls.Add(this.Rail_Box_NumericBox);
			this.splines_panel.Controls.Add(this.Spline_Select_mode_Box);
			this.splines_panel.Controls.Add(this.Splines_Instance_Box);
			this.splines_panel.Controls.Add(this.Splines_Instance_label);
			this.splines_panel.Controls.Add(this.Splines_ShowLocation_Button);
			this.splines_panel.Controls.Add(this.Rail_Box_dist_Label);
			this.splines_panel.Controls.Add(this.Splines_Location_label);
			this.splines_panel.Controls.Add(this.Splines_ChangeModel_Button);
			this.splines_panel.Controls.Add(this.Splines_Remove_Button);
			this.splines_panel.Controls.Add(this.Splines_Models_Box);
			this.splines_panel.Controls.Add(this.Splines_label);
			this.splines_panel.Location = new System.Drawing.Point(0, 98);
			this.splines_panel.Name = "splines_panel";
			this.splines_panel.Size = new System.Drawing.Size(158, 309);
			this.splines_panel.TabIndex = 9;
			this.splines_panel.Visible = false;
			// 
			// Rail_Box_NumericBox
			// 
			this.Rail_Box_NumericBox.Location = new System.Drawing.Point(11, 252);
			this.Rail_Box_NumericBox.Name = "Rail_Box_NumericBox";
			this.Rail_Box_NumericBox.Size = new System.Drawing.Size(136, 20);
			this.Rail_Box_NumericBox.TabIndex = 10;
			this.Rail_Box_NumericBox.Text = "0";
			this.Rail_Box_NumericBox.Value = 0D;
			this.Rail_Box_NumericBox.EnterPressed += new System.EventHandler(this.Rails_NumericBoxEnterPressed);
			// 
			// Spline_Select_mode_Box
			// 
			this.Spline_Select_mode_Box.Location = new System.Drawing.Point(12, 282);
			this.Spline_Select_mode_Box.Name = "Spline_Select_mode_Box";
			this.Spline_Select_mode_Box.Size = new System.Drawing.Size(135, 24);
			this.Spline_Select_mode_Box.TabIndex = 9;
			this.Spline_Select_mode_Box.Text = "Режим выбора";
			this.Spline_Select_mode_Box.UseVisualStyleBackColor = true;
			// 
			// Splines_Instance_Box
			// 
			this.Splines_Instance_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Splines_Instance_Box.FormattingEnabled = true;
			this.Splines_Instance_Box.Location = new System.Drawing.Point(11, 79);
			this.Splines_Instance_Box.Name = "Splines_Instance_Box";
			this.Splines_Instance_Box.Size = new System.Drawing.Size(136, 21);
			this.Splines_Instance_Box.TabIndex = 8;
			this.Splines_Instance_Box.SelectedIndexChanged += new System.EventHandler(this.Splines_Instance_BoxSelectedIndexChanged);
			// 
			// Splines_Instance_label
			// 
			this.Splines_Instance_label.AutoSize = true;
			this.Splines_Instance_label.Location = new System.Drawing.Point(11, 63);
			this.Splines_Instance_label.Name = "Splines_Instance_label";
			this.Splines_Instance_label.Size = new System.Drawing.Size(75, 13);
			this.Splines_Instance_label.TabIndex = 7;
			this.Splines_Instance_label.Text = "Экземпляры:";
			// 
			// Splines_ShowLocation_Button
			// 
			this.Splines_ShowLocation_Button.Location = new System.Drawing.Point(11, 209);
			this.Splines_ShowLocation_Button.Name = "Splines_ShowLocation_Button";
			this.Splines_ShowLocation_Button.Size = new System.Drawing.Size(136, 23);
			this.Splines_ShowLocation_Button.TabIndex = 5;
			this.Splines_ShowLocation_Button.Text = "Показать";
			this.Splines_ShowLocation_Button.UseVisualStyleBackColor = true;
			this.Splines_ShowLocation_Button.Click += new System.EventHandler(this.Splines_ShowLocation_ButtonClick);
			// 
			// Rail_Box_dist_Label
			// 
			this.Rail_Box_dist_Label.AutoSize = true;
			this.Rail_Box_dist_Label.Location = new System.Drawing.Point(11, 237);
			this.Rail_Box_dist_Label.Name = "Rail_Box_dist_Label";
			this.Rail_Box_dist_Label.Size = new System.Drawing.Size(115, 13);
			this.Rail_Box_dist_Label.TabIndex = 4;
			this.Rail_Box_dist_Label.Text = "Расстояние коробки:";
			// 
			// Splines_Location_label
			// 
			this.Splines_Location_label.AutoSize = true;
			this.Splines_Location_label.Location = new System.Drawing.Point(11, 193);
			this.Splines_Location_label.Name = "Splines_Location_label";
			this.Splines_Location_label.Size = new System.Drawing.Size(98, 13);
			this.Splines_Location_label.TabIndex = 4;
			this.Splines_Location_label.Text = "Местоположение:";
			// 
			// Splines_ChangeModel_Button
			// 
			this.Splines_ChangeModel_Button.Location = new System.Drawing.Point(11, 135);
			this.Splines_ChangeModel_Button.Name = "Splines_ChangeModel_Button";
			this.Splines_ChangeModel_Button.Size = new System.Drawing.Size(136, 23);
			this.Splines_ChangeModel_Button.TabIndex = 3;
			this.Splines_ChangeModel_Button.Text = "Сменить модель";
			this.Splines_ChangeModel_Button.UseVisualStyleBackColor = true;
			this.Splines_ChangeModel_Button.Click += new System.EventHandler(this.Splines_ChangeModel_ButtonClick);
			// 
			// Splines_Remove_Button
			// 
			this.Splines_Remove_Button.Location = new System.Drawing.Point(11, 106);
			this.Splines_Remove_Button.Name = "Splines_Remove_Button";
			this.Splines_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Splines_Remove_Button.TabIndex = 2;
			this.Splines_Remove_Button.Text = "Удалить сплайн";
			this.Splines_Remove_Button.UseVisualStyleBackColor = true;
			this.Splines_Remove_Button.Click += new System.EventHandler(this.Splines_Remove_ButtonClick);
			// 
			// Splines_Models_Box
			// 
			this.Splines_Models_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Splines_Models_Box.FormattingEnabled = true;
			this.Splines_Models_Box.Location = new System.Drawing.Point(11, 36);
			this.Splines_Models_Box.Name = "Splines_Models_Box";
			this.Splines_Models_Box.Size = new System.Drawing.Size(136, 21);
			this.Splines_Models_Box.TabIndex = 1;
			this.Splines_Models_Box.SelectedIndexChanged += new System.EventHandler(this.Splines_Models_BoxSelectedIndexChanged);
			// 
			// Splines_label
			// 
			this.Splines_label.AutoSize = true;
			this.Splines_label.Location = new System.Drawing.Point(11, 20);
			this.Splines_label.Name = "Splines_label";
			this.Splines_label.Size = new System.Drawing.Size(55, 13);
			this.Splines_label.TabIndex = 0;
			this.Splines_label.Text = "Сплайны:";
			// 
			// signals_panel
			// 
			this.signals_panel.Controls.Add(this.Signals_Model_Box);
			this.signals_panel.Controls.Add(this.Signals_Model_label);
			this.signals_panel.Controls.Add(this.Signals_Element_Minus_Box);
			this.signals_panel.Controls.Add(this.Signals_Bound_UpDown);
			this.signals_panel.Controls.Add(this.Signals_Element_Location_label);
			this.signals_panel.Controls.Add(this.Signals_Bound_label);
			this.signals_panel.Controls.Add(this.Signals_Remove_Button);
			this.signals_panel.Controls.Add(this.Signals_Element_label);
			this.signals_panel.Controls.Add(this.Signals_Add_Button);
			this.signals_panel.Controls.Add(this.Signals_Element_EditLocation_Button);
			this.signals_panel.Controls.Add(this.Signals_Element_ShowLocation_Button);
			this.signals_panel.Controls.Add(this.Signals_label);
			this.signals_panel.Controls.Add(this.Signals_Element_Remove_Button);
			this.signals_panel.Controls.Add(this.Signals_Box);
			this.signals_panel.Controls.Add(this.Signals_Element_Box);
			this.signals_panel.Controls.Add(this.Signals_Element_AddSignal_Button);
			this.signals_panel.Controls.Add(this.Signals_Element_AddContact_Button);
			this.signals_panel.Location = new System.Drawing.Point(3, 3);
			this.signals_panel.Name = "signals_panel";
			this.signals_panel.Padding = new System.Windows.Forms.Padding(8, 20, 8, 0);
			this.signals_panel.Size = new System.Drawing.Size(158, 614);
			this.signals_panel.TabIndex = 5;
			this.signals_panel.Visible = false;
			// 
			// Signals_Model_Box
			// 
			this.Signals_Model_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Signals_Model_Box.FormattingEnabled = true;
			this.Signals_Model_Box.Location = new System.Drawing.Point(11, 501);
			this.Signals_Model_Box.Name = "Signals_Model_Box";
			this.Signals_Model_Box.Size = new System.Drawing.Size(136, 21);
			this.Signals_Model_Box.TabIndex = 8;
			// 
			// Signals_Model_label
			// 
			this.Signals_Model_label.AutoSize = true;
			this.Signals_Model_label.Location = new System.Drawing.Point(11, 485);
			this.Signals_Model_label.Name = "Signals_Model_label";
			this.Signals_Model_label.Size = new System.Drawing.Size(49, 13);
			this.Signals_Model_label.TabIndex = 7;
			this.Signals_Model_label.Text = "Модель:";
			// 
			// Signals_Element_Minus_Box
			// 
			this.Signals_Element_Minus_Box.AutoSize = true;
			this.Signals_Element_Minus_Box.Location = new System.Drawing.Point(11, 357);
			this.Signals_Element_Minus_Box.Name = "Signals_Element_Minus_Box";
			this.Signals_Element_Minus_Box.Size = new System.Drawing.Size(125, 17);
			this.Signals_Element_Minus_Box.TabIndex = 6;
			this.Signals_Element_Minus_Box.Text = "Минусовой контакт";
			this.Signals_Element_Minus_Box.UseVisualStyleBackColor = true;
			this.Signals_Element_Minus_Box.Visible = false;
			this.Signals_Element_Minus_Box.CheckedChanged += new System.EventHandler(this.Signals_Element_Minus_Box_CheckedChanged);
			// 
			// Signals_Bound_UpDown
			// 
			this.Signals_Bound_UpDown.Location = new System.Drawing.Point(11, 147);
			this.Signals_Bound_UpDown.Name = "Signals_Bound_UpDown";
			this.Signals_Bound_UpDown.Size = new System.Drawing.Size(136, 20);
			this.Signals_Bound_UpDown.TabIndex = 5;
			this.Signals_Bound_UpDown.ValueChanged += new System.EventHandler(this.Signals_Bound_UpDown_ValueChanged);
			// 
			// Signals_Element_Location_label
			// 
			this.Signals_Element_Location_label.AutoSize = true;
			this.Signals_Element_Location_label.Location = new System.Drawing.Point(11, 397);
			this.Signals_Element_Location_label.Name = "Signals_Element_Location_label";
			this.Signals_Element_Location_label.Size = new System.Drawing.Size(98, 13);
			this.Signals_Element_Location_label.TabIndex = 4;
			this.Signals_Element_Location_label.Text = "Местоположение:";
			// 
			// Signals_Bound_label
			// 
			this.Signals_Bound_label.AutoSize = true;
			this.Signals_Bound_label.Location = new System.Drawing.Point(11, 131);
			this.Signals_Bound_label.Name = "Signals_Bound_label";
			this.Signals_Bound_label.Size = new System.Drawing.Size(126, 13);
			this.Signals_Bound_label.TabIndex = 4;
			this.Signals_Bound_label.Text = "Красный при значении:";
			// 
			// Signals_Remove_Button
			// 
			this.Signals_Remove_Button.Location = new System.Drawing.Point(11, 92);
			this.Signals_Remove_Button.Name = "Signals_Remove_Button";
			this.Signals_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Signals_Remove_Button.TabIndex = 2;
			this.Signals_Remove_Button.Text = "Удалить систему";
			this.Signals_Remove_Button.UseVisualStyleBackColor = true;
			this.Signals_Remove_Button.Click += new System.EventHandler(this.Signals_Remove_Button_Click);
			// 
			// Signals_Element_label
			// 
			this.Signals_Element_label.AutoSize = true;
			this.Signals_Element_label.Location = new System.Drawing.Point(11, 204);
			this.Signals_Element_label.Name = "Signals_Element_label";
			this.Signals_Element_label.Size = new System.Drawing.Size(62, 13);
			this.Signals_Element_label.TabIndex = 4;
			this.Signals_Element_label.Text = "Элементы:";
			// 
			// Signals_Add_Button
			// 
			this.Signals_Add_Button.Location = new System.Drawing.Point(11, 63);
			this.Signals_Add_Button.Name = "Signals_Add_Button";
			this.Signals_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Signals_Add_Button.TabIndex = 2;
			this.Signals_Add_Button.Text = "Добавить систему";
			this.Signals_Add_Button.UseVisualStyleBackColor = true;
			this.Signals_Add_Button.Click += new System.EventHandler(this.Signals_Add_Button_Click);
			// 
			// Signals_Element_EditLocation_Button
			// 
			this.Signals_Element_EditLocation_Button.Location = new System.Drawing.Point(11, 442);
			this.Signals_Element_EditLocation_Button.Name = "Signals_Element_EditLocation_Button";
			this.Signals_Element_EditLocation_Button.Size = new System.Drawing.Size(136, 23);
			this.Signals_Element_EditLocation_Button.TabIndex = 2;
			this.Signals_Element_EditLocation_Button.Text = "Изменить";
			this.Signals_Element_EditLocation_Button.UseVisualStyleBackColor = true;
			this.Signals_Element_EditLocation_Button.Click += new System.EventHandler(this.Signals_Element_EditLocation_Button_Click);
			// 
			// Signals_Element_ShowLocation_Button
			// 
			this.Signals_Element_ShowLocation_Button.Location = new System.Drawing.Point(11, 413);
			this.Signals_Element_ShowLocation_Button.Name = "Signals_Element_ShowLocation_Button";
			this.Signals_Element_ShowLocation_Button.Size = new System.Drawing.Size(136, 23);
			this.Signals_Element_ShowLocation_Button.TabIndex = 2;
			this.Signals_Element_ShowLocation_Button.Text = "Показать";
			this.Signals_Element_ShowLocation_Button.UseVisualStyleBackColor = true;
			this.Signals_Element_ShowLocation_Button.Click += new System.EventHandler(this.Signals_Element_ShowLocation_Button_Click);
			// 
			// Signals_label
			// 
			this.Signals_label.AutoSize = true;
			this.Signals_label.Location = new System.Drawing.Point(11, 20);
			this.Signals_label.Name = "Signals_label";
			this.Signals_label.Size = new System.Drawing.Size(120, 13);
			this.Signals_label.TabIndex = 1;
			this.Signals_label.Text = "Сигнальные системы:";
			// 
			// Signals_Element_Remove_Button
			// 
			this.Signals_Element_Remove_Button.Location = new System.Drawing.Point(11, 305);
			this.Signals_Element_Remove_Button.Name = "Signals_Element_Remove_Button";
			this.Signals_Element_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Signals_Element_Remove_Button.TabIndex = 2;
			this.Signals_Element_Remove_Button.Text = "Удалить элемент";
			this.Signals_Element_Remove_Button.UseVisualStyleBackColor = true;
			this.Signals_Element_Remove_Button.Click += new System.EventHandler(this.Signals_Element_Remove_Button_Click);
			// 
			// Signals_Box
			// 
			this.Signals_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Signals_Box.FormattingEnabled = true;
			this.Signals_Box.Location = new System.Drawing.Point(11, 36);
			this.Signals_Box.Name = "Signals_Box";
			this.Signals_Box.Size = new System.Drawing.Size(136, 21);
			this.Signals_Box.TabIndex = 0;
			this.Signals_Box.SelectedIndexChanged += new System.EventHandler(this.Signals_Box_SelectedIndexChanged);
			// 
			// Signals_Element_Box
			// 
			this.Signals_Element_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Signals_Element_Box.FormattingEnabled = true;
			this.Signals_Element_Box.Location = new System.Drawing.Point(11, 220);
			this.Signals_Element_Box.Name = "Signals_Element_Box";
			this.Signals_Element_Box.Size = new System.Drawing.Size(136, 21);
			this.Signals_Element_Box.TabIndex = 0;
			this.Signals_Element_Box.SelectedIndexChanged += new System.EventHandler(this.Signals_Element_Box_SelectedIndexChanged);
			// 
			// Signals_Element_AddSignal_Button
			// 
			this.Signals_Element_AddSignal_Button.Location = new System.Drawing.Point(11, 276);
			this.Signals_Element_AddSignal_Button.Name = "Signals_Element_AddSignal_Button";
			this.Signals_Element_AddSignal_Button.Size = new System.Drawing.Size(136, 23);
			this.Signals_Element_AddSignal_Button.TabIndex = 2;
			this.Signals_Element_AddSignal_Button.Text = "Добавить сигнал";
			this.Signals_Element_AddSignal_Button.UseVisualStyleBackColor = true;
			this.Signals_Element_AddSignal_Button.Click += new System.EventHandler(this.Signals_Element_AddSignal_Button_Click);
			// 
			// Signals_Element_AddContact_Button
			// 
			this.Signals_Element_AddContact_Button.Location = new System.Drawing.Point(11, 247);
			this.Signals_Element_AddContact_Button.Name = "Signals_Element_AddContact_Button";
			this.Signals_Element_AddContact_Button.Size = new System.Drawing.Size(136, 23);
			this.Signals_Element_AddContact_Button.TabIndex = 2;
			this.Signals_Element_AddContact_Button.Text = "Добавить контакт";
			this.Signals_Element_AddContact_Button.UseVisualStyleBackColor = true;
			this.Signals_Element_AddContact_Button.Click += new System.EventHandler(this.Signals_Element_AddContact_Button_Click);
			// 
			// stops_panel
			// 
			this.stops_panel.Controls.Add(this.Stops_Model_Box);
			this.stops_panel.Controls.Add(this.Stops_Model_label);
			this.stops_panel.Controls.Add(this.TypeOfTransportBox);
			this.stops_panel.Controls.Add(this.Stops_Location_label);
			this.stops_panel.Controls.Add(this.Stops_Name_label);
			this.stops_panel.Controls.Add(this.Stops_Name_Box);
			this.stops_panel.Controls.Add(this.Stops_Remove_Button);
			this.stops_panel.Controls.Add(this.Stops_Add_Button);
			this.stops_panel.Controls.Add(this.Stops_EditLocation_Button);
			this.stops_panel.Controls.Add(this.Stops_ShowLocation_Button);
			this.stops_panel.Controls.Add(this.Stops_ChangeName_Button);
			this.stops_panel.Controls.Add(this.Stops_label);
			this.stops_panel.Controls.Add(this.Stops_Box);
			this.stops_panel.Location = new System.Drawing.Point(364, 233);
			this.stops_panel.Name = "stops_panel";
			this.stops_panel.Padding = new System.Windows.Forms.Padding(8, 20, 8, 0);
			this.stops_panel.Size = new System.Drawing.Size(158, 562);
			this.stops_panel.TabIndex = 5;
			this.stops_panel.Visible = false;
			// 
			// Stops_Model_Box
			// 
			this.Stops_Model_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Stops_Model_Box.FormattingEnabled = true;
			this.Stops_Model_Box.Location = new System.Drawing.Point(11, 431);
			this.Stops_Model_Box.Name = "Stops_Model_Box";
			this.Stops_Model_Box.Size = new System.Drawing.Size(136, 21);
			this.Stops_Model_Box.TabIndex = 13;
			// 
			// Stops_Model_label
			// 
			this.Stops_Model_label.AutoSize = true;
			this.Stops_Model_label.Location = new System.Drawing.Point(11, 415);
			this.Stops_Model_label.Name = "Stops_Model_label";
			this.Stops_Model_label.Size = new System.Drawing.Size(49, 13);
			this.Stops_Model_label.TabIndex = 12;
			this.Stops_Model_label.Text = "Модель:";
			// 
			// TypeOfTransportBox
			// 
			this.TypeOfTransportBox.Controls.Add(this.BusBox);
			this.TypeOfTransportBox.Controls.Add(this.TrolleybusBox);
			this.TypeOfTransportBox.Controls.Add(this.TramwayBox);
			this.TypeOfTransportBox.Location = new System.Drawing.Point(11, 213);
			this.TypeOfTransportBox.Name = "TypeOfTransportBox";
			this.TypeOfTransportBox.Size = new System.Drawing.Size(136, 98);
			this.TypeOfTransportBox.TabIndex = 6;
			this.TypeOfTransportBox.TabStop = false;
			this.TypeOfTransportBox.Text = "Вид транспорта";
			// 
			// BusBox
			// 
			this.BusBox.AutoSize = true;
			this.BusBox.Location = new System.Drawing.Point(15, 67);
			this.BusBox.Name = "BusBox";
			this.BusBox.Size = new System.Drawing.Size(67, 17);
			this.BusBox.TabIndex = 2;
			this.BusBox.Text = "Автобус";
			this.BusBox.UseVisualStyleBackColor = true;
			this.BusBox.Click += new System.EventHandler(this.BusBox_Click);
			// 
			// TrolleybusBox
			// 
			this.TrolleybusBox.AutoSize = true;
			this.TrolleybusBox.Location = new System.Drawing.Point(15, 44);
			this.TrolleybusBox.Name = "TrolleybusBox";
			this.TrolleybusBox.Size = new System.Drawing.Size(86, 17);
			this.TrolleybusBox.TabIndex = 1;
			this.TrolleybusBox.Text = "Троллейбус";
			this.TrolleybusBox.UseVisualStyleBackColor = true;
			this.TrolleybusBox.Click += new System.EventHandler(this.TrolleybusBox_Click);
			// 
			// TramwayBox
			// 
			this.TramwayBox.AutoSize = true;
			this.TramwayBox.Location = new System.Drawing.Point(15, 21);
			this.TramwayBox.Name = "TramwayBox";
			this.TramwayBox.Size = new System.Drawing.Size(71, 17);
			this.TramwayBox.TabIndex = 0;
			this.TramwayBox.Text = "Трамвай";
			this.TramwayBox.UseVisualStyleBackColor = true;
			this.TramwayBox.Click += new System.EventHandler(this.TramwayBox_Click);
			// 
			// Stops_Location_label
			// 
			this.Stops_Location_label.AutoSize = true;
			this.Stops_Location_label.Location = new System.Drawing.Point(11, 327);
			this.Stops_Location_label.Name = "Stops_Location_label";
			this.Stops_Location_label.Size = new System.Drawing.Size(98, 13);
			this.Stops_Location_label.TabIndex = 4;
			this.Stops_Location_label.Text = "Местоположение:";
			// 
			// Stops_Name_label
			// 
			this.Stops_Name_label.AutoSize = true;
			this.Stops_Name_label.Location = new System.Drawing.Point(11, 131);
			this.Stops_Name_label.Name = "Stops_Name_label";
			this.Stops_Name_label.Size = new System.Drawing.Size(116, 13);
			this.Stops_Name_label.TabIndex = 4;
			this.Stops_Name_label.Text = "Название остановки:";
			// 
			// Stops_Name_Box
			// 
			this.Stops_Name_Box.Location = new System.Drawing.Point(11, 147);
			this.Stops_Name_Box.Name = "Stops_Name_Box";
			this.Stops_Name_Box.Size = new System.Drawing.Size(136, 20);
			this.Stops_Name_Box.TabIndex = 3;
			this.Stops_Name_Box.ModifiedChanged += new System.EventHandler(this.Stops_Name_Box_ModifiedChanged);
			// 
			// Stops_Remove_Button
			// 
			this.Stops_Remove_Button.Location = new System.Drawing.Point(11, 92);
			this.Stops_Remove_Button.Name = "Stops_Remove_Button";
			this.Stops_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Stops_Remove_Button.TabIndex = 2;
			this.Stops_Remove_Button.Text = "Удалить остановку";
			this.Stops_Remove_Button.UseVisualStyleBackColor = true;
			this.Stops_Remove_Button.Click += new System.EventHandler(this.Stops_Remove_Button_Click);
			// 
			// Stops_Add_Button
			// 
			this.Stops_Add_Button.Location = new System.Drawing.Point(11, 63);
			this.Stops_Add_Button.Name = "Stops_Add_Button";
			this.Stops_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Stops_Add_Button.TabIndex = 2;
			this.Stops_Add_Button.Text = "Добавить остановку";
			this.Stops_Add_Button.UseVisualStyleBackColor = true;
			this.Stops_Add_Button.Click += new System.EventHandler(this.Stops_Add_Button_Click);
			// 
			// Stops_EditLocation_Button
			// 
			this.Stops_EditLocation_Button.Location = new System.Drawing.Point(11, 372);
			this.Stops_EditLocation_Button.Name = "Stops_EditLocation_Button";
			this.Stops_EditLocation_Button.Size = new System.Drawing.Size(136, 23);
			this.Stops_EditLocation_Button.TabIndex = 2;
			this.Stops_EditLocation_Button.Text = "Изменить";
			this.Stops_EditLocation_Button.UseVisualStyleBackColor = true;
			this.Stops_EditLocation_Button.Click += new System.EventHandler(this.Stops_EditLocation_Button_Click);
			// 
			// Stops_ShowLocation_Button
			// 
			this.Stops_ShowLocation_Button.Location = new System.Drawing.Point(11, 343);
			this.Stops_ShowLocation_Button.Name = "Stops_ShowLocation_Button";
			this.Stops_ShowLocation_Button.Size = new System.Drawing.Size(136, 23);
			this.Stops_ShowLocation_Button.TabIndex = 2;
			this.Stops_ShowLocation_Button.Text = "Показать";
			this.Stops_ShowLocation_Button.UseVisualStyleBackColor = true;
			this.Stops_ShowLocation_Button.Click += new System.EventHandler(this.Stops_ShowLocation_Button_Click);
			// 
			// Stops_ChangeName_Button
			// 
			this.Stops_ChangeName_Button.Location = new System.Drawing.Point(11, 174);
			this.Stops_ChangeName_Button.Name = "Stops_ChangeName_Button";
			this.Stops_ChangeName_Button.Size = new System.Drawing.Size(136, 23);
			this.Stops_ChangeName_Button.TabIndex = 2;
			this.Stops_ChangeName_Button.Text = "Изменить название";
			this.Stops_ChangeName_Button.UseVisualStyleBackColor = true;
			this.Stops_ChangeName_Button.Click += new System.EventHandler(this.Stops_ChangeName_Button_Click);
			// 
			// Stops_label
			// 
			this.Stops_label.AutoSize = true;
			this.Stops_label.Location = new System.Drawing.Point(11, 20);
			this.Stops_label.Name = "Stops_label";
			this.Stops_label.Size = new System.Drawing.Size(65, 13);
			this.Stops_label.TabIndex = 1;
			this.Stops_label.Text = "Остановки:";
			// 
			// Stops_Box
			// 
			this.Stops_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Stops_Box.FormattingEnabled = true;
			this.Stops_Box.Location = new System.Drawing.Point(11, 36);
			this.Stops_Box.Name = "Stops_Box";
			this.Stops_Box.Size = new System.Drawing.Size(136, 21);
			this.Stops_Box.TabIndex = 0;
			this.Stops_Box.SelectedIndexChanged += new System.EventHandler(this.Stops_Box_SelectedIndexChanged);
			// 
			// park_panel
			// 
			this.park_panel.Controls.Add(this.Park_Name_label);
			this.park_panel.Controls.Add(this.Park_Name_Box);
			this.park_panel.Controls.Add(this.Park_Remove_Button);
			this.park_panel.Controls.Add(this.Park_ChangeName_Button);
			this.park_panel.Controls.Add(this.Park_Add_Button);
			this.park_panel.Controls.Add(this.Park_label);
			this.park_panel.Controls.Add(this.Park_Box);
			this.park_panel.Location = new System.Drawing.Point(364, 11);
			this.park_panel.Name = "park_panel";
			this.park_panel.Padding = new System.Windows.Forms.Padding(8, 20, 8, 0);
			this.park_panel.Size = new System.Drawing.Size(158, 217);
			this.park_panel.TabIndex = 5;
			this.park_panel.Visible = false;
			// 
			// Park_Name_label
			// 
			this.Park_Name_label.AutoSize = true;
			this.Park_Name_label.Location = new System.Drawing.Point(11, 131);
			this.Park_Name_label.Name = "Park_Name_label";
			this.Park_Name_label.Size = new System.Drawing.Size(93, 13);
			this.Park_Name_label.TabIndex = 4;
			this.Park_Name_label.Text = "Название парка:";
			// 
			// Park_Name_Box
			// 
			this.Park_Name_Box.Location = new System.Drawing.Point(11, 147);
			this.Park_Name_Box.Name = "Park_Name_Box";
			this.Park_Name_Box.Size = new System.Drawing.Size(136, 20);
			this.Park_Name_Box.TabIndex = 3;
			this.Park_Name_Box.ModifiedChanged += new System.EventHandler(this.Park_Name_Box_ModifiedChanged);
			// 
			// Park_Remove_Button
			// 
			this.Park_Remove_Button.Location = new System.Drawing.Point(11, 92);
			this.Park_Remove_Button.Name = "Park_Remove_Button";
			this.Park_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Park_Remove_Button.TabIndex = 2;
			this.Park_Remove_Button.Text = "Удалить парк";
			this.Park_Remove_Button.UseVisualStyleBackColor = true;
			this.Park_Remove_Button.Click += new System.EventHandler(this.Park_Remove_Button_Click);
			// 
			// Park_ChangeName_Button
			// 
			this.Park_ChangeName_Button.Location = new System.Drawing.Point(11, 174);
			this.Park_ChangeName_Button.Name = "Park_ChangeName_Button";
			this.Park_ChangeName_Button.Size = new System.Drawing.Size(136, 23);
			this.Park_ChangeName_Button.TabIndex = 2;
			this.Park_ChangeName_Button.Text = "Изменить название";
			this.Park_ChangeName_Button.UseVisualStyleBackColor = true;
			this.Park_ChangeName_Button.Click += new System.EventHandler(this.Park_ChangeName_Button_Click);
			// 
			// Park_Add_Button
			// 
			this.Park_Add_Button.Location = new System.Drawing.Point(11, 63);
			this.Park_Add_Button.Name = "Park_Add_Button";
			this.Park_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Park_Add_Button.TabIndex = 2;
			this.Park_Add_Button.Text = "Добавить парк";
			this.Park_Add_Button.UseVisualStyleBackColor = true;
			this.Park_Add_Button.Click += new System.EventHandler(this.Park_Add_Button_Click);
			// 
			// Park_label
			// 
			this.Park_label.AutoSize = true;
			this.Park_label.Location = new System.Drawing.Point(11, 20);
			this.Park_label.Name = "Park_label";
			this.Park_label.Size = new System.Drawing.Size(96, 13);
			this.Park_label.TabIndex = 1;
			this.Park_label.Text = "Выбранный парк:";
			// 
			// Park_Box
			// 
			this.Park_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Park_Box.FormattingEnabled = true;
			this.Park_Box.Location = new System.Drawing.Point(11, 36);
			this.Park_Box.Name = "Park_Box";
			this.Park_Box.Size = new System.Drawing.Size(136, 21);
			this.Park_Box.TabIndex = 0;
			this.Park_Box.SelectedIndexChanged += new System.EventHandler(this.Park_Box_SelectedIndexChanged);
			// 
			// Refresh_Timer
			// 
			this.Refresh_Timer.Enabled = true;
			this.Refresh_Timer.Interval = 30;
			this.Refresh_Timer.Tick += new System.EventHandler(this.Refresh_Timer_Tick);
			// 
			// Sizable_Panel
			// 
			this.Sizable_Panel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.Sizable_Panel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Sizable_Panel.Controls.Add(this.renderPanel);
			this.Sizable_Panel.Location = new System.Drawing.Point(0, 28);
			this.Sizable_Panel.Name = "Sizable_Panel";
			this.Sizable_Panel.Size = new System.Drawing.Size(1012, 1009);
			this.Sizable_Panel.TabIndex = 3;
			this.Sizable_Panel.SizeChanged += new System.EventHandler(this.RefreshPanelSize);
			// 
			// openFileDialog
			// 
			this.openFileDialog.DefaultExt = "city";
			this.openFileDialog.Filter = "Trancity Maps (*.city)|*.city|All files (*.*)|*.*";
			this.openFileDialog.InitialDirectory = "..\\Cities";
			this.openFileDialog.Title = "Открыть карту";
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.DefaultExt = "city";
			this.saveFileDialog.Filter = "Trancity Maps (*.city)|*.city";
			this.saveFileDialog.InitialDirectory = "..\\Cities";
			this.saveFileDialog.Title = "Сохранить карту";
			// 
			// edit_panel
			// 
			this.edit_panel.Dock = System.Windows.Forms.DockStyle.Right;
			this.edit_panel.Location = new System.Drawing.Point(850, 28);
			this.edit_panel.Name = "edit_panel";
			this.edit_panel.Padding = new System.Windows.Forms.Padding(8, 20, 8, 0);
			this.edit_panel.Size = new System.Drawing.Size(158, 845);
			this.edit_panel.TabIndex = 4;
			// 
			// narad_panel
			// 
			this.narad_panel.Controls.Add(this.RollingStockBox);
			this.narad_panel.Controls.Add(this.Transport_label);
			this.narad_panel.Controls.Add(this.Narad_Runs_Time2_Box);
			this.narad_panel.Controls.Add(this.Narad_Runs_Time1_Box);
			this.narad_panel.Controls.Add(this.Narad_Runs_Time2_label);
			this.narad_panel.Controls.Add(this.Narad_Runs_Time1_label);
			this.narad_panel.Controls.Add(this.Narad_Runs_label);
			this.narad_panel.Controls.Add(this.Narad_Park_label);
			this.narad_panel.Controls.Add(this.Narad_Runs_Run_label);
			this.narad_panel.Controls.Add(this.Narad_Name_label);
			this.narad_panel.Controls.Add(this.Narad_Name_Box);
			this.narad_panel.Controls.Add(this.Narad_Runs_Remove_Button);
			this.narad_panel.Controls.Add(this.Narad_Remove_Button);
			this.narad_panel.Controls.Add(this.Narad_Runs_Add_Button);
			this.narad_panel.Controls.Add(this.Narad_Add_Button);
			this.narad_panel.Controls.Add(this.Narad_ChangeName_Button);
			this.narad_panel.Controls.Add(this.Narad_label);
			this.narad_panel.Controls.Add(this.Narad_Park_Box);
			this.narad_panel.Controls.Add(this.Narad_Runs_Run_Box);
			this.narad_panel.Controls.Add(this.Narad_Runs_Box);
			this.narad_panel.Controls.Add(this.Narad_Box);
			this.narad_panel.Dock = System.Windows.Forms.DockStyle.Right;
			this.narad_panel.Location = new System.Drawing.Point(692, 28);
			this.narad_panel.Name = "narad_panel";
			this.narad_panel.Padding = new System.Windows.Forms.Padding(8, 20, 8, 0);
			this.narad_panel.Size = new System.Drawing.Size(158, 823);
			this.narad_panel.TabIndex = 1;
			this.narad_panel.Visible = false;
			// 
			// RollingStockBox
			// 
			this.RollingStockBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.RollingStockBox.FormattingEnabled = true;
			this.RollingStockBox.Location = new System.Drawing.Point(11, 550);
			this.RollingStockBox.Name = "RollingStockBox";
			this.RollingStockBox.Size = new System.Drawing.Size(136, 21);
			this.RollingStockBox.TabIndex = 9;
			this.RollingStockBox.SelectedIndexChanged += new System.EventHandler(this.RollingStockBox_SelectedIndexChanged);
			// 
			// Transport_label
			// 
			this.Transport_label.AutoSize = true;
			this.Transport_label.Location = new System.Drawing.Point(11, 534);
			this.Transport_label.Name = "Transport_label";
			this.Transport_label.Size = new System.Drawing.Size(121, 13);
			this.Transport_label.TabIndex = 8;
			this.Transport_label.Text = "Подвижной состав:";
			// 
			// Narad_Runs_Time2_Box
			// 
			this.Narad_Runs_Time2_Box.Enabled = false;
			this.Narad_Runs_Time2_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Narad_Runs_Time2_Box.Hours = 0;
			this.Narad_Runs_Time2_Box.Location = new System.Drawing.Point(11, 495);
			this.Narad_Runs_Time2_Box.MinimumSize = new System.Drawing.Size(40, 21);
			this.Narad_Runs_Time2_Box.Minutes = 0;
			this.Narad_Runs_Time2_Box.Name = "Narad_Runs_Time2_Box";
			this.Narad_Runs_Time2_Box.Seconds = 0;
			this.Narad_Runs_Time2_Box.Size = new System.Drawing.Size(136, 21);
			this.Narad_Runs_Time2_Box.TabIndex = 7;
			this.Narad_Runs_Time2_Box.Time = new System.DateTime(((long)(0)));
			this.Narad_Runs_Time2_Box.Time_Minutes = 0;
			this.Narad_Runs_Time2_Box.Time_Seconds = 0;
			this.Narad_Runs_Time2_Box.ViewSeconds = false;
			// 
			// Narad_Runs_Time1_Box
			// 
			this.Narad_Runs_Time1_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Narad_Runs_Time1_Box.Hours = 0;
			this.Narad_Runs_Time1_Box.Location = new System.Drawing.Point(11, 455);
			this.Narad_Runs_Time1_Box.MinimumSize = new System.Drawing.Size(40, 21);
			this.Narad_Runs_Time1_Box.Minutes = 0;
			this.Narad_Runs_Time1_Box.Name = "Narad_Runs_Time1_Box";
			this.Narad_Runs_Time1_Box.Seconds = 0;
			this.Narad_Runs_Time1_Box.Size = new System.Drawing.Size(136, 21);
			this.Narad_Runs_Time1_Box.TabIndex = 7;
			this.Narad_Runs_Time1_Box.Time = new System.DateTime(((long)(0)));
			this.Narad_Runs_Time1_Box.Time_Minutes = 0;
			this.Narad_Runs_Time1_Box.Time_Seconds = 0;
			this.Narad_Runs_Time1_Box.ViewSeconds = false;
			this.Narad_Runs_Time1_Box.TimeChanged += new System.EventHandler(this.Narad_Runs_Time1_Box_TimeChanged);
			// 
			// Narad_Runs_Time2_label
			// 
			this.Narad_Runs_Time2_label.AutoSize = true;
			this.Narad_Runs_Time2_label.Location = new System.Drawing.Point(11, 479);
			this.Narad_Runs_Time2_label.Name = "Narad_Runs_Time2_label";
			this.Narad_Runs_Time2_label.Size = new System.Drawing.Size(108, 13);
			this.Narad_Runs_Time2_label.TabIndex = 4;
			this.Narad_Runs_Time2_label.Text = "Время прибытия:";
			// 
			// Narad_Runs_Time1_label
			// 
			this.Narad_Runs_Time1_label.AutoSize = true;
			this.Narad_Runs_Time1_label.Location = new System.Drawing.Point(11, 439);
			this.Narad_Runs_Time1_label.Name = "Narad_Runs_Time1_label";
			this.Narad_Runs_Time1_label.Size = new System.Drawing.Size(127, 13);
			this.Narad_Runs_Time1_label.TabIndex = 4;
			this.Narad_Runs_Time1_label.Text = "Время отправления:";
			// 
			// Narad_Runs_label
			// 
			this.Narad_Runs_label.AutoSize = true;
			this.Narad_Runs_label.Location = new System.Drawing.Point(11, 280);
			this.Narad_Runs_label.Name = "Narad_Runs_label";
			this.Narad_Runs_label.Size = new System.Drawing.Size(48, 13);
			this.Narad_Runs_label.TabIndex = 4;
			this.Narad_Runs_label.Text = "Рейсы:";
			// 
			// Narad_Park_label
			// 
			this.Narad_Park_label.AutoSize = true;
			this.Narad_Park_label.Location = new System.Drawing.Point(11, 217);
			this.Narad_Park_label.Name = "Narad_Park_label";
			this.Narad_Park_label.Size = new System.Drawing.Size(41, 13);
			this.Narad_Park_label.TabIndex = 4;
			this.Narad_Park_label.Text = "Парк:";
			// 
			// Narad_Runs_Run_label
			// 
			this.Narad_Runs_Run_label.AutoSize = true;
			this.Narad_Runs_Run_label.Location = new System.Drawing.Point(11, 391);
			this.Narad_Runs_Run_label.Name = "Narad_Runs_Run_label";
			this.Narad_Runs_Run_label.Size = new System.Drawing.Size(90, 13);
			this.Narad_Runs_Run_label.TabIndex = 4;
			this.Narad_Runs_Run_label.Text = "Трасса рейса:";
			// 
			// Narad_Name_label
			// 
			this.Narad_Name_label.AutoSize = true;
			this.Narad_Name_label.Location = new System.Drawing.Point(11, 131);
			this.Narad_Name_label.Name = "Narad_Name_label";
			this.Narad_Name_label.Size = new System.Drawing.Size(115, 13);
			this.Narad_Name_label.TabIndex = 4;
			this.Narad_Name_label.Text = "Название наряда:";
			// 
			// Narad_Name_Box
			// 
			this.Narad_Name_Box.Location = new System.Drawing.Point(11, 147);
			this.Narad_Name_Box.Name = "Narad_Name_Box";
			this.Narad_Name_Box.Size = new System.Drawing.Size(136, 21);
			this.Narad_Name_Box.TabIndex = 3;
			this.Narad_Name_Box.ModifiedChanged += new System.EventHandler(this.Narad_Name_Box_ModifiedChanged);
			// 
			// Narad_Runs_Remove_Button
			// 
			this.Narad_Runs_Remove_Button.Location = new System.Drawing.Point(11, 352);
			this.Narad_Runs_Remove_Button.Name = "Narad_Runs_Remove_Button";
			this.Narad_Runs_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Narad_Runs_Remove_Button.TabIndex = 2;
			this.Narad_Runs_Remove_Button.Text = "Удалить рейс";
			this.Narad_Runs_Remove_Button.UseVisualStyleBackColor = true;
			this.Narad_Runs_Remove_Button.Click += new System.EventHandler(this.Narad_Runs_Remove_Button_Click);
			// 
			// Narad_Remove_Button
			// 
			this.Narad_Remove_Button.Location = new System.Drawing.Point(11, 92);
			this.Narad_Remove_Button.Name = "Narad_Remove_Button";
			this.Narad_Remove_Button.Size = new System.Drawing.Size(136, 23);
			this.Narad_Remove_Button.TabIndex = 2;
			this.Narad_Remove_Button.Text = "Удалить наряд";
			this.Narad_Remove_Button.UseVisualStyleBackColor = true;
			this.Narad_Remove_Button.Click += new System.EventHandler(this.Narad_Remove_Button_Click);
			// 
			// Narad_Runs_Add_Button
			// 
			this.Narad_Runs_Add_Button.Location = new System.Drawing.Point(11, 323);
			this.Narad_Runs_Add_Button.Name = "Narad_Runs_Add_Button";
			this.Narad_Runs_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Narad_Runs_Add_Button.TabIndex = 2;
			this.Narad_Runs_Add_Button.Text = "Добавить рейс";
			this.Narad_Runs_Add_Button.UseVisualStyleBackColor = true;
			this.Narad_Runs_Add_Button.Click += new System.EventHandler(this.Narad_Runs_Add_Button_Click);
			// 
			// Narad_Add_Button
			// 
			this.Narad_Add_Button.Location = new System.Drawing.Point(11, 63);
			this.Narad_Add_Button.Name = "Narad_Add_Button";
			this.Narad_Add_Button.Size = new System.Drawing.Size(136, 23);
			this.Narad_Add_Button.TabIndex = 2;
			this.Narad_Add_Button.Text = "Добавить наряд";
			this.Narad_Add_Button.UseVisualStyleBackColor = true;
			this.Narad_Add_Button.Click += new System.EventHandler(this.Narad_Add_Button_Click);
			// 
			// Narad_ChangeName_Button
			// 
			this.Narad_ChangeName_Button.Location = new System.Drawing.Point(11, 174);
			this.Narad_ChangeName_Button.Name = "Narad_ChangeName_Button";
			this.Narad_ChangeName_Button.Size = new System.Drawing.Size(136, 23);
			this.Narad_ChangeName_Button.TabIndex = 2;
			this.Narad_ChangeName_Button.Text = "Изменить название";
			this.Narad_ChangeName_Button.UseVisualStyleBackColor = true;
			this.Narad_ChangeName_Button.Click += new System.EventHandler(this.Narad_ChangeName_Button_Click);
			// 
			// Narad_label
			// 
			this.Narad_label.AutoSize = true;
			this.Narad_label.Location = new System.Drawing.Point(11, 20);
			this.Narad_label.Name = "Narad_label";
			this.Narad_label.Size = new System.Drawing.Size(58, 13);
			this.Narad_label.TabIndex = 1;
			this.Narad_label.Text = "Наряды:";
			// 
			// Narad_Park_Box
			// 
			this.Narad_Park_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Narad_Park_Box.FormattingEnabled = true;
			this.Narad_Park_Box.Location = new System.Drawing.Point(11, 233);
			this.Narad_Park_Box.Name = "Narad_Park_Box";
			this.Narad_Park_Box.Size = new System.Drawing.Size(136, 21);
			this.Narad_Park_Box.TabIndex = 0;
			this.Narad_Park_Box.SelectedIndexChanged += new System.EventHandler(this.Narad_Park_Box_SelectedIndexChanged);
			// 
			// Narad_Runs_Run_Box
			// 
			this.Narad_Runs_Run_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Narad_Runs_Run_Box.FormattingEnabled = true;
			this.Narad_Runs_Run_Box.Location = new System.Drawing.Point(11, 407);
			this.Narad_Runs_Run_Box.Name = "Narad_Runs_Run_Box";
			this.Narad_Runs_Run_Box.Size = new System.Drawing.Size(136, 21);
			this.Narad_Runs_Run_Box.TabIndex = 0;
			this.Narad_Runs_Run_Box.SelectedIndexChanged += new System.EventHandler(this.Narad_Runs_Run_Box_SelectedIndexChanged);
			// 
			// Narad_Runs_Box
			// 
			this.Narad_Runs_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Narad_Runs_Box.FormattingEnabled = true;
			this.Narad_Runs_Box.Location = new System.Drawing.Point(11, 296);
			this.Narad_Runs_Box.Name = "Narad_Runs_Box";
			this.Narad_Runs_Box.Size = new System.Drawing.Size(136, 21);
			this.Narad_Runs_Box.TabIndex = 0;
			this.Narad_Runs_Box.SelectedIndexChanged += new System.EventHandler(this.Narad_Runs_Box_SelectedIndexChanged);
			// 
			// Narad_Box
			// 
			this.Narad_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.Narad_Box.FormattingEnabled = true;
			this.Narad_Box.Location = new System.Drawing.Point(11, 36);
			this.Narad_Box.Name = "Narad_Box";
			this.Narad_Box.Size = new System.Drawing.Size(136, 21);
			this.Narad_Box.TabIndex = 0;
			this.Narad_Box.SelectedIndexChanged += new System.EventHandler(this.Narad_Box_SelectedIndexChanged);
			// 
			// Editor
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(1008, 873);
			this.Controls.Add(this.narad_panel);
			this.Controls.Add(this.statusBar);
			this.Controls.Add(this.edit_panel);
			this.Controls.Add(this.Sizable_Panel);
			this.Controls.Add(this.toolBar);
			this.Font = new Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Icon = ((Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Menu = this.mainMenu;
			this.Name = "Editor";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.WindowState = FormWindowState.Maximized;
			this.Activated += new System.EventHandler(this.EditorActivated);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Editor_Form_Closing);
			this.Deactivate += new System.EventHandler(this.EditorDeactivate);
			this.Load += new System.EventHandler(this.Editor_Form_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Editor_Form_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Editor_Form_KeyUp);
			((ISupportInitialize)(this.Cursor_x_Status)).EndInit();
			((ISupportInitialize)(this.Cursor_y_Status)).EndInit();
			((ISupportInitialize)(this.SeparatorPanel1)).EndInit();
			((ISupportInitialize)(this.Coord_x1_Status)).EndInit();
			((ISupportInitialize)(this.Coord_y1_Status)).EndInit();
			((ISupportInitialize)(this.Angle1_Status)).EndInit();
			((ISupportInitialize)(this.SeparatorPanel2)).EndInit();
			((ISupportInitialize)(this.Coord_x2_Status)).EndInit();
			((ISupportInitialize)(this.Coord_y2_Status)).EndInit();
			((ISupportInitialize)(this.Angle2_Status)).EndInit();
			((ISupportInitialize)(this.SeparatorPanel3)).EndInit();
			((ISupportInitialize)(this.Length_Status)).EndInit();
			((ISupportInitialize)(this.Radius_Status)).EndInit();
			((ISupportInitialize)(this.Angle_Status)).EndInit();
			((ISupportInitialize)(this.Wide0_Status)).EndInit();
			((ISupportInitialize)(this.Wide1_Status)).EndInit();
			((ISupportInitialize)(this.Height0_Status)).EndInit();
			((ISupportInitialize)(this.Height1_Status)).EndInit();
			((ISupportInitialize)(this.SeparatorPanel4)).EndInit();
			((ISupportInitialize)(this.Maschtab)).EndInit();
			((ISupportInitialize)(this.SeparatorPanel5)).EndInit();
			((ISupportInitialize)(this.Ugol)).EndInit();
			this.object_panel.ResumeLayout(false);
			this.object_panel.PerformLayout();
			this.route_panel.ResumeLayout(false);
			this.route_panel.PerformLayout();
			((ISupportInitialize)(this.Route_Runs_ToParkIndex_UpDown)).EndInit();
			this.svetofor_panel.ResumeLayout(false);
			this.svetofor_panel.PerformLayout();
			this.splines_panel.ResumeLayout(false);
			this.splines_panel.PerformLayout();
			this.signals_panel.ResumeLayout(false);
			this.signals_panel.PerformLayout();
			((ISupportInitialize)(this.Signals_Bound_UpDown)).EndInit();
			this.stops_panel.ResumeLayout(false);
			this.stops_panel.PerformLayout();
			this.TypeOfTransportBox.ResumeLayout(false);
			this.TypeOfTransportBox.PerformLayout();
			this.park_panel.ResumeLayout(false);
			this.park_panel.PerformLayout();
			this.Sizable_Panel.ResumeLayout(false);
			this.narad_panel.ResumeLayout(false);
			this.narad_panel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private Label Rail_Box_dist_Label;
		private Common.NumericBox Rail_Box_NumericBox;
//		private ToolBarButton Rail_Build_Direct_Button;
		private ToolBarButton Rail_Build_Curve_Button;
		private Label Stops_Model_label;
		private ComboBox Stops_Model_Box;
		private Label Svetofor_Model_label;
		private ComboBox Svetofor_Model_Box;
		private Label Signals_Model_label;
		private ComboBox Signals_Model_Box;
		private CheckBox Spline_Select_mode_Box;
		private Label Splines_label;
		private ComboBox Splines_Models_Box;
		private Button Splines_Remove_Button;
		private Button Splines_ChangeModel_Button;
		private Label Splines_Location_label;
		private Button Splines_ShowLocation_Button;
		private Label Splines_Instance_label;
		private ComboBox Splines_Instance_Box;
		private Panel splines_panel;
		private ToolBarButton Info;

        private Panel object_panel;
        private Label Objects_label;
        private ComboBox Objects_Box;
        private Button Objects_Add_Button;
        private Label Objects_Location_label;
        private Button Objects_Remove_Button;
        private Button Objects_EditLocation_Button;
        private Button Objects_ShowLocation_Button;
        private ComboBox Objects_Instance_Box;
        private Label Objects_Instance_label;
        private ComboBox RollingStockBox;
        private Label Transport_label;
        private StatusBarPanel SeparatorPanel4;
        private StatusBarPanel Maschtab;
        private StatusBarPanel SeparatorPanel5;
        private StatusBarPanel Ugol;
        //private ToolBarButton SeparatorButton7;
        private ToolBarButton SeparatorButton8;
        private ToolBarButton Rail_Build_попутки_Button;
        private ToolBarButton Rail_Build_попутки1_Button;
        private ToolBarButton Rail_Build_попутки2_Button;
        private ToolBarButton Rail_Build_попутки3_Button;
        private ToolBarButton Rail_Build_встречки_Button;
        private ToolBarButton Rail_Build_встречки1_Button;
        private ToolBarButton Rail_Build_встречки2_Button;
        private ToolBarButton Rail_Build_встречки3_Button;
        private ToolBarButton toolBarButton3;
        private GroupBox TypeOfTransportBox;
        private CheckBox BusBox;
        private CheckBox TrolleybusBox;
        private CheckBox TramwayBox;
        private Button StopsButton;
        private ToolBarButton Troll_lines_Doblue;
        private ToolBarButton Troll_lines_Against;
        private ToolBarButton TramWireOverRail;
        private ToolBarButton TrollWireOverRoad;
	}
}
