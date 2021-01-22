using System.Windows.Forms;
using System.Drawing;
using System;
using System.ComponentModel;
namespace Trancity
{
    partial class Options
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
        	this.Launch_Buttton = new System.Windows.Forms.Button();
        	this.Exit_Button = new System.Windows.Forms.Button();
        	this.AutoControl_Box = new System.Windows.Forms.CheckBox();
        	this.Time_label = new System.Windows.Forms.Label();
        	this.TransportCount_label = new System.Windows.Forms.Label();
        	this.RotateCamera_Box = new System.Windows.Forms.CheckBox();
        	this.Tab_Control = new System.Windows.Forms.TabControl();
        	this.Options_Page = new System.Windows.Forms.TabPage();
        	this.Options_Group = new System.Windows.Forms.GroupBox();
        	this.Lang_Box = new System.Windows.Forms.ComboBox();
        	this.Compute_TCount_label = new System.Windows.Forms.Label();
        	this.InParkCount_label = new System.Windows.Forms.Label();
        	this.OnRouteCount_label = new System.Windows.Forms.Label();
        	this.City_Name_label = new System.Windows.Forms.Label();
        	this.LoadCity_Button = new System.Windows.Forms.Button();
        	this.StartTime_Box = new TimeBox();
        	this.InvArrows_Box = new System.Windows.Forms.CheckBox();
        	this.City_label = new System.Windows.Forms.Label();
        	this.Langugage_label = new System.Windows.Forms.Label();
        	this.Players_Page = new System.Windows.Forms.TabPage();
        	this.Players_Box = new System.Windows.Forms.GroupBox();
        	this.label1 = new System.Windows.Forms.Label();
        	this.comboBox1 = new System.Windows.Forms.ComboBox();
        	this.Управление_Box = new System.Windows.Forms.ComboBox();
        	this.Control_label = new System.Windows.Forms.Label();
        	this.Order_label = new System.Windows.Forms.Label();
        	this.Route_label = new System.Windows.Forms.Label();
        	this.Наряд_Box = new System.Windows.Forms.ComboBox();
        	this.Маршрут_Box = new System.Windows.Forms.ComboBox();
        	this.ПодвижнойСостав_Box = new System.Windows.Forms.ComboBox();
        	this.Transport_label = new System.Windows.Forms.Label();
        	this.Remove_Button = new System.Windows.Forms.Button();
        	this.Name_Box = new System.Windows.Forms.TextBox();
        	this.Add_Button = new System.Windows.Forms.Button();
        	this.Players_List = new System.Windows.Forms.CheckedListBox();
        	this.DirectX_Page = new System.Windows.Forms.TabPage();
        	this.DirectX_Box = new System.Windows.Forms.GroupBox();
        	this.Control_button = new System.Windows.Forms.Button();
        	this.EnableShaders_Box = new System.Windows.Forms.CheckBox();
        	this.Screen_Size_label = new System.Windows.Forms.Label();
        	this.NonExclusiveMouse_Box = new System.Windows.Forms.CheckBox();
        	this.Splines_Cond_label = new System.Windows.Forms.Label();
        	this.NonExclusiveKeyboard_Box = new System.Windows.Forms.CheckBox();
        	this.Vertex_label = new System.Windows.Forms.Label();
        	this.NoStops_Box = new System.Windows.Forms.CheckBox();
        	this.VertexProcessing_Box = new System.Windows.Forms.ComboBox();
        	this.EnableSound_Box = new System.Windows.Forms.CheckBox();
        	this.Screen_Box = new System.Windows.Forms.ComboBox();
        	this.Volume_TrackBar = new System.Windows.Forms.TrackBar();
        	this.Rail_Box = new System.Windows.Forms.TrackBar();
        	this.Editor_Button = new System.Windows.Forms.Button();
        	this.LoadCity_Dialog = new System.Windows.Forms.OpenFileDialog();
        	this.Tab_Control.SuspendLayout();
        	this.Options_Page.SuspendLayout();
        	this.Options_Group.SuspendLayout();
        	this.Players_Page.SuspendLayout();
        	this.Players_Box.SuspendLayout();
        	this.DirectX_Page.SuspendLayout();
        	this.DirectX_Box.SuspendLayout();
        	((System.ComponentModel.ISupportInitialize)(this.Volume_TrackBar)).BeginInit();
        	((System.ComponentModel.ISupportInitialize)(this.Rail_Box)).BeginInit();
        	this.SuspendLayout();
        	// 
        	// Launch_Buttton
        	// 
        	this.Launch_Buttton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.Launch_Buttton.BackColor = System.Drawing.SystemColors.ActiveCaption;
        	this.Launch_Buttton.DialogResult = System.Windows.Forms.DialogResult.OK;
        	this.Launch_Buttton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.Launch_Buttton.Location = new System.Drawing.Point(45, 360);
        	this.Launch_Buttton.Name = "Launch_Buttton";
        	this.Launch_Buttton.Size = new System.Drawing.Size(115, 35);
        	this.Launch_Buttton.TabIndex = 0;
        	this.Launch_Buttton.Text = "Запустить!";
        	this.Launch_Buttton.UseVisualStyleBackColor = false;
        	// 
        	// Exit_Button
        	// 
        	this.Exit_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.Exit_Button.BackColor = System.Drawing.SystemColors.ActiveCaption;
        	this.Exit_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.Exit_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.Exit_Button.Location = new System.Drawing.Point(315, 360);
        	this.Exit_Button.Name = "Exit_Button";
        	this.Exit_Button.Size = new System.Drawing.Size(115, 35);
        	this.Exit_Button.TabIndex = 1;
        	this.Exit_Button.Text = "Выход";
        	this.Exit_Button.UseVisualStyleBackColor = false;
        	// 
        	// AutoControl_Box
        	// 
        	this.AutoControl_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.AutoControl_Box.Location = new System.Drawing.Point(30, 215);
        	this.AutoControl_Box.Name = "AutoControl_Box";
        	this.AutoControl_Box.Size = new System.Drawing.Size(320, 17);
        	this.AutoControl_Box.TabIndex = 3;
        	this.AutoControl_Box.Text = "Начать с автоматическим управлением";
        	// 
        	// Time_label
        	// 
        	this.Time_label.AutoSize = true;
        	this.Time_label.Location = new System.Drawing.Point(30, 65);
        	this.Time_label.Name = "Time_label";
        	this.Time_label.Size = new System.Drawing.Size(115, 13);
        	this.Time_label.TabIndex = 0;
        	this.Time_label.Text = "Начальное время:";
        	// 
        	// TransportCount_label
        	// 
        	this.TransportCount_label.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        	this.TransportCount_label.AutoSize = true;
        	this.TransportCount_label.Location = new System.Drawing.Point(30, 95);
        	this.TransportCount_label.Name = "TransportCount_label";
        	this.TransportCount_label.Size = new System.Drawing.Size(101, 13);
        	this.TransportCount_label.TabIndex = 0;
        	this.TransportCount_label.Text = "Количество ПС:";
        	// 
        	// RotateCamera_Box
        	// 
        	this.RotateCamera_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.RotateCamera_Box.Checked = true;
        	this.RotateCamera_Box.CheckState = System.Windows.Forms.CheckState.Checked;
        	this.RotateCamera_Box.Location = new System.Drawing.Point(30, 245);
        	this.RotateCamera_Box.Name = "RotateCamera_Box";
        	this.RotateCamera_Box.Size = new System.Drawing.Size(320, 17);
        	this.RotateCamera_Box.TabIndex = 3;
        	this.RotateCamera_Box.Text = "Поворачивать камеру вместе с ПС";
        	// 
        	// Tab_Control
        	// 
        	this.Tab_Control.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.Tab_Control.Appearance = System.Windows.Forms.TabAppearance.Buttons;
        	this.Tab_Control.Controls.Add(this.Options_Page);
        	this.Tab_Control.Controls.Add(this.Players_Page);
        	this.Tab_Control.Controls.Add(this.DirectX_Page);
        	this.Tab_Control.Cursor = System.Windows.Forms.Cursors.Hand;
        	this.Tab_Control.ItemSize = new System.Drawing.Size(131, 21);
        	this.Tab_Control.Location = new System.Drawing.Point(36, 12);
        	this.Tab_Control.Multiline = true;
        	this.Tab_Control.Name = "Tab_Control";
        	this.Tab_Control.SelectedIndex = 0;
        	this.Tab_Control.Size = new System.Drawing.Size(404, 325);
        	this.Tab_Control.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
        	this.Tab_Control.TabIndex = 3;
        	// 
        	// Options_Page
        	// 
        	this.Options_Page.BackColor = System.Drawing.Color.LightGray;
        	this.Options_Page.Controls.Add(this.Options_Group);
        	this.Options_Page.ForeColor = System.Drawing.SystemColors.ControlText;
        	this.Options_Page.Location = new System.Drawing.Point(4, 25);
        	this.Options_Page.Name = "Options_Page";
        	this.Options_Page.Size = new System.Drawing.Size(396, 296);
        	this.Options_Page.TabIndex = 0;
        	this.Options_Page.Text = "Общие настройки";
        	// 
        	// Options_Group
        	// 
        	this.Options_Group.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.Options_Group.BackColor = System.Drawing.Color.Transparent;
        	this.Options_Group.Controls.Add(this.Lang_Box);
        	this.Options_Group.Controls.Add(this.Compute_TCount_label);
        	this.Options_Group.Controls.Add(this.InParkCount_label);
        	this.Options_Group.Controls.Add(this.OnRouteCount_label);
        	this.Options_Group.Controls.Add(this.City_Name_label);
        	this.Options_Group.Controls.Add(this.LoadCity_Button);
        	this.Options_Group.Controls.Add(this.StartTime_Box);
        	this.Options_Group.Controls.Add(this.InvArrows_Box);
        	this.Options_Group.Controls.Add(this.AutoControl_Box);
        	this.Options_Group.Controls.Add(this.City_label);
        	this.Options_Group.Controls.Add(this.Langugage_label);
        	this.Options_Group.Controls.Add(this.Time_label);
        	this.Options_Group.Controls.Add(this.TransportCount_label);
        	this.Options_Group.Controls.Add(this.RotateCamera_Box);
        	this.Options_Group.Cursor = System.Windows.Forms.Cursors.Default;
        	this.Options_Group.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        	this.Options_Group.Location = new System.Drawing.Point(6, 4);
        	this.Options_Group.Name = "Options_Group";
        	this.Options_Group.Size = new System.Drawing.Size(385, 285);
        	this.Options_Group.TabIndex = 2;
        	this.Options_Group.TabStop = false;
        	// 
        	// Lang_Box
        	// 
        	this.Lang_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.Lang_Box.FormattingEnabled = true;
        	this.Lang_Box.Location = new System.Drawing.Point(217, 165);
        	this.Lang_Box.Name = "Lang_Box";
        	this.Lang_Box.Size = new System.Drawing.Size(138, 21);
        	this.Lang_Box.TabIndex = 11;
        	this.Lang_Box.SelectedIndexChanged += new System.EventHandler(this.Lang_BoxSelectedIndexChanged);
        	// 
        	// Compute_TCount_label
        	// 
        	this.Compute_TCount_label.Location = new System.Drawing.Point(308, 88);
        	this.Compute_TCount_label.Name = "Compute_TCount_label";
        	this.Compute_TCount_label.Size = new System.Drawing.Size(42, 26);
        	this.Compute_TCount_label.TabIndex = 10;
        	this.Compute_TCount_label.Text = "0";
        	this.Compute_TCount_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// InParkCount_label
        	// 
        	this.InParkCount_label.Location = new System.Drawing.Point(217, 101);
        	this.InParkCount_label.Name = "InParkCount_label";
        	this.InParkCount_label.Size = new System.Drawing.Size(85, 13);
        	this.InParkCount_label.TabIndex = 10;
        	this.InParkCount_label.Text = "в депо:";
        	this.InParkCount_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// OnRouteCount_label
        	// 
        	this.OnRouteCount_label.Location = new System.Drawing.Point(217, 88);
        	this.OnRouteCount_label.Name = "OnRouteCount_label";
        	this.OnRouteCount_label.Size = new System.Drawing.Size(85, 13);
        	this.OnRouteCount_label.TabIndex = 10;
        	this.OnRouteCount_label.Text = "на линии:";
        	this.OnRouteCount_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        	// 
        	// City_Name_label
        	// 
        	this.City_Name_label.AutoSize = true;
        	this.City_Name_label.Location = new System.Drawing.Point(68, 35);
        	this.City_Name_label.Name = "City_Name_label";
        	this.City_Name_label.Size = new System.Drawing.Size(87, 13);
        	this.City_Name_label.TabIndex = 9;
        	this.City_Name_label.Text = "не загружена";
        	this.City_Name_label.Click += new System.EventHandler(this.City_Name_labelClick);
        	// 
        	// LoadCity_Button
        	// 
        	this.LoadCity_Button.Cursor = System.Windows.Forms.Cursors.Hand;
        	this.LoadCity_Button.Location = new System.Drawing.Point(217, 30);
        	this.LoadCity_Button.Name = "LoadCity_Button";
        	this.LoadCity_Button.Size = new System.Drawing.Size(138, 23);
        	this.LoadCity_Button.TabIndex = 8;
        	this.LoadCity_Button.Text = "Загрузить...";
        	this.LoadCity_Button.UseVisualStyleBackColor = true;
        	this.LoadCity_Button.Click += new System.EventHandler(this.LoadCity_Button_Click);
        	// 
        	// StartTime_Box
        	// 
        	this.StartTime_Box.Cursor = System.Windows.Forms.Cursors.IBeam;
        	this.StartTime_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        	this.StartTime_Box.Hours = 0;
        	this.StartTime_Box.Location = new System.Drawing.Point(217, 62);
        	this.StartTime_Box.MinimumSize = new System.Drawing.Size(40, 21);
        	this.StartTime_Box.Minutes = 0;
        	this.StartTime_Box.Name = "StartTime_Box";
        	this.StartTime_Box.Seconds = 0;
        	this.StartTime_Box.Size = new System.Drawing.Size(138, 21);
        	this.StartTime_Box.TabIndex = 6;
        	this.StartTime_Box.Time = new System.DateTime(((long)(0)));
        	this.StartTime_Box.Time_Minutes = 0;
        	this.StartTime_Box.Time_Seconds = 0;
        	this.StartTime_Box.ViewSeconds = true;
        	this.StartTime_Box.TimeChanged += new System.EventHandler(this.StartTime_Box_TimeChanged);
        	this.StartTime_Box.Load += new System.EventHandler(this.StartTime_BoxLoad);
        	// 
        	// InvArrows_Box
        	// 
        	this.InvArrows_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.InvArrows_Box.Checked = true;
        	this.InvArrows_Box.CheckState = System.Windows.Forms.CheckState.Checked;
        	this.InvArrows_Box.Location = new System.Drawing.Point(30, 125);
        	this.InvArrows_Box.Name = "InvArrows_Box";
        	this.InvArrows_Box.Size = new System.Drawing.Size(320, 17);
        	this.InvArrows_Box.TabIndex = 3;
        	this.InvArrows_Box.Text = "Переключение стрелок под током налево";
        	// 
        	// City_label
        	// 
        	this.City_label.AutoSize = true;
        	this.City_label.Location = new System.Drawing.Point(30, 35);
        	this.City_label.Name = "City_label";
        	this.City_label.Size = new System.Drawing.Size(41, 13);
        	this.City_label.TabIndex = 0;
        	this.City_label.Text = "Карта";
        	// 
        	// Langugage_label
        	// 
        	this.Langugage_label.AutoSize = true;
        	this.Langugage_label.Location = new System.Drawing.Point(30, 168);
        	this.Langugage_label.Name = "Langugage_label";
        	this.Langugage_label.Size = new System.Drawing.Size(42, 13);
        	this.Langugage_label.TabIndex = 0;
        	this.Langugage_label.Text = "Язык:";
        	// 
        	// Players_Page
        	// 
        	this.Players_Page.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        	this.Players_Page.Controls.Add(this.Players_Box);
        	this.Players_Page.Controls.Add(this.Remove_Button);
        	this.Players_Page.Controls.Add(this.Name_Box);
        	this.Players_Page.Controls.Add(this.Add_Button);
        	this.Players_Page.Controls.Add(this.Players_List);
        	this.Players_Page.Location = new System.Drawing.Point(4, 25);
        	this.Players_Page.Name = "Players_Page";
        	this.Players_Page.Size = new System.Drawing.Size(396, 296);
        	this.Players_Page.TabIndex = 1;
        	this.Players_Page.Text = "Настройки игроков";
        	// 
        	// Players_Box
        	// 
        	this.Players_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.Players_Box.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        	this.Players_Box.Controls.Add(this.label1);
        	this.Players_Box.Controls.Add(this.comboBox1);
        	this.Players_Box.Controls.Add(this.Управление_Box);
        	this.Players_Box.Controls.Add(this.Control_label);
        	this.Players_Box.Controls.Add(this.Order_label);
        	this.Players_Box.Controls.Add(this.Route_label);
        	this.Players_Box.Controls.Add(this.Наряд_Box);
        	this.Players_Box.Controls.Add(this.Маршрут_Box);
        	this.Players_Box.Controls.Add(this.ПодвижнойСостав_Box);
        	this.Players_Box.Controls.Add(this.Transport_label);
        	this.Players_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        	this.Players_Box.Location = new System.Drawing.Point(6, 59);
        	this.Players_Box.Name = "Players_Box";
        	this.Players_Box.Size = new System.Drawing.Size(385, 230);
        	this.Players_Box.TabIndex = 3;
        	this.Players_Box.TabStop = false;
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(30, 154);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(41, 13);
        	this.label1.TabIndex = 3;
        	this.label1.Text = "Парк:";
        	this.label1.Visible = false;
        	this.label1.Click += new System.EventHandler(this.Label1Click);
        	// 
        	// comboBox1
        	// 
        	this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.comboBox1.Items.AddRange(new object[] {
			"Нет"});
        	this.comboBox1.Location = new System.Drawing.Point(166, 154);
        	this.comboBox1.Name = "comboBox1";
        	this.comboBox1.Size = new System.Drawing.Size(189, 21);
        	this.comboBox1.TabIndex = 2;
        	this.comboBox1.Visible = false;
        	// 
        	// Управление_Box
        	// 
        	this.Управление_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.Управление_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.Управление_Box.Location = new System.Drawing.Point(166, 32);
        	this.Управление_Box.Name = "Управление_Box";
        	this.Управление_Box.Size = new System.Drawing.Size(189, 21);
        	this.Управление_Box.TabIndex = 1;
        	// 
        	// Control_label
        	// 
        	this.Control_label.AutoSize = true;
        	this.Control_label.Location = new System.Drawing.Point(30, 35);
        	this.Control_label.Name = "Control_label";
        	this.Control_label.Size = new System.Drawing.Size(83, 13);
        	this.Control_label.TabIndex = 0;
        	this.Control_label.Text = "Управление:";
        	// 
        	// Order_label
        	// 
        	this.Order_label.AutoSize = true;
        	this.Order_label.Location = new System.Drawing.Point(30, 125);
        	this.Order_label.Name = "Order_label";
        	this.Order_label.Size = new System.Drawing.Size(49, 13);
        	this.Order_label.TabIndex = 0;
        	this.Order_label.Text = "Наряд:";
        	// 
        	// Route_label
        	// 
        	this.Route_label.AutoSize = true;
        	this.Route_label.Location = new System.Drawing.Point(30, 95);
        	this.Route_label.Name = "Route_label";
        	this.Route_label.Size = new System.Drawing.Size(63, 13);
        	this.Route_label.TabIndex = 0;
        	this.Route_label.Text = "Маршрут:";
        	// 
        	// Наряд_Box
        	// 
        	this.Наряд_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.Наряд_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.Наряд_Box.Items.AddRange(new object[] {
			"Нет",
			"Случайный",
			"1",
			"2",
			"3",
			"4",
			"5"});
        	this.Наряд_Box.Location = new System.Drawing.Point(166, 122);
        	this.Наряд_Box.Name = "Наряд_Box";
        	this.Наряд_Box.Size = new System.Drawing.Size(189, 21);
        	this.Наряд_Box.TabIndex = 1;
        	// 
        	// Маршрут_Box
        	// 
        	this.Маршрут_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.Маршрут_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.Маршрут_Box.Items.AddRange(new object[] {
			"Нет",
			"Случайный",
			"А",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9"});
        	this.Маршрут_Box.Location = new System.Drawing.Point(166, 92);
        	this.Маршрут_Box.Name = "Маршрут_Box";
        	this.Маршрут_Box.Size = new System.Drawing.Size(189, 21);
        	this.Маршрут_Box.TabIndex = 1;
        	this.Маршрут_Box.SelectedIndexChanged += new System.EventHandler(this.Маршрут_Box_SelectedIndexChanged);
        	// 
        	// ПодвижнойСостав_Box
        	// 
        	this.ПодвижнойСостав_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.ПодвижнойСостав_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.ПодвижнойСостав_Box.Location = new System.Drawing.Point(166, 62);
        	this.ПодвижнойСостав_Box.Name = "ПодвижнойСостав_Box";
        	this.ПодвижнойСостав_Box.Size = new System.Drawing.Size(189, 21);
        	this.ПодвижнойСостав_Box.TabIndex = 1;
        	this.ПодвижнойСостав_Box.SelectedIndexChanged += new System.EventHandler(this.ПодвижнойСостав_Box_SelectedIndexChanged);
        	this.ПодвижнойСостав_Box.TextChanged += new System.EventHandler(this.ПодвижнойСостав_Box_TextChanged);
        	// 
        	// Transport_label
        	// 
        	this.Transport_label.AutoSize = true;
        	this.Transport_label.Location = new System.Drawing.Point(30, 65);
        	this.Transport_label.Name = "Transport_label";
        	this.Transport_label.Size = new System.Drawing.Size(121, 13);
        	this.Transport_label.TabIndex = 0;
        	this.Transport_label.Text = "Подвижной состав:";
        	// 
        	// Remove_Button
        	// 
        	this.Remove_Button.BackColor = System.Drawing.SystemColors.ActiveCaption;
        	this.Remove_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.Remove_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.Remove_Button.Location = new System.Drawing.Point(200, 25);
        	this.Remove_Button.Name = "Remove_Button";
        	this.Remove_Button.Size = new System.Drawing.Size(96, 27);
        	this.Remove_Button.TabIndex = 2;
        	this.Remove_Button.Text = "Удалить";
        	this.Remove_Button.UseVisualStyleBackColor = false;
        	this.Remove_Button.Click += new System.EventHandler(this.Remove_Button_Click);
        	// 
        	// Name_Box
        	// 
        	this.Name_Box.Location = new System.Drawing.Point(200, 0);
        	this.Name_Box.Name = "Name_Box";
        	this.Name_Box.Size = new System.Drawing.Size(196, 21);
        	this.Name_Box.TabIndex = 1;
        	this.Name_Box.Text = "Игрок 3";
        	this.Name_Box.TextChanged += new System.EventHandler(this.UpdatePlayers);
        	this.Name_Box.Enter += new System.EventHandler(this.Name_Box_Enter);
        	this.Name_Box.Leave += new System.EventHandler(this.Name_Box_Leave);
        	// 
        	// Add_Button
        	// 
        	this.Add_Button.BackColor = System.Drawing.SystemColors.ActiveCaption;
        	this.Add_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.Add_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.Add_Button.Location = new System.Drawing.Point(300, 25);
        	this.Add_Button.Name = "Add_Button";
        	this.Add_Button.Size = new System.Drawing.Size(96, 27);
        	this.Add_Button.TabIndex = 2;
        	this.Add_Button.Text = "Добавить";
        	this.Add_Button.UseVisualStyleBackColor = false;
        	this.Add_Button.Click += new System.EventHandler(this.Add_Button_Click);
        	// 
        	// Players_List
        	// 
        	this.Players_List.Items.AddRange(new object[] {
			"Игрок 1",
			"Игрок 2"});
        	this.Players_List.Location = new System.Drawing.Point(0, 0);
        	this.Players_List.Name = "Players_List";
        	this.Players_List.ScrollAlwaysVisible = true;
        	this.Players_List.Size = new System.Drawing.Size(196, 52);
        	this.Players_List.TabIndex = 2;
        	this.Players_List.ThreeDCheckBoxes = true;
        	this.Players_List.SelectedIndexChanged += new System.EventHandler(this.UpdatePlayers);
        	this.Players_List.DoubleClick += new System.EventHandler(this.UpdatePlayers);
        	// 
        	// DirectX_Page
        	// 
        	this.DirectX_Page.Controls.Add(this.DirectX_Box);
        	this.DirectX_Page.Location = new System.Drawing.Point(4, 25);
        	this.DirectX_Page.Name = "DirectX_Page";
        	this.DirectX_Page.Padding = new System.Windows.Forms.Padding(3);
        	this.DirectX_Page.Size = new System.Drawing.Size(396, 296);
        	this.DirectX_Page.TabIndex = 2;
        	this.DirectX_Page.Text = "Настройки DirectX";
        	this.DirectX_Page.UseVisualStyleBackColor = true;
        	// 
        	// DirectX_Box
        	// 
        	this.DirectX_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.DirectX_Box.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
        	this.DirectX_Box.Controls.Add(this.Control_button);
        	this.DirectX_Box.Controls.Add(this.EnableShaders_Box);
        	this.DirectX_Box.Controls.Add(this.Screen_Size_label);
        	this.DirectX_Box.Controls.Add(this.NonExclusiveMouse_Box);
        	this.DirectX_Box.Controls.Add(this.Splines_Cond_label);
        	this.DirectX_Box.Controls.Add(this.NonExclusiveKeyboard_Box);
        	this.DirectX_Box.Controls.Add(this.Vertex_label);
        	this.DirectX_Box.Controls.Add(this.NoStops_Box);
        	this.DirectX_Box.Controls.Add(this.VertexProcessing_Box);
        	this.DirectX_Box.Controls.Add(this.EnableSound_Box);
        	this.DirectX_Box.Controls.Add(this.Screen_Box);
        	this.DirectX_Box.Controls.Add(this.Volume_TrackBar);
        	this.DirectX_Box.Controls.Add(this.Rail_Box);
        	this.DirectX_Box.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        	this.DirectX_Box.Location = new System.Drawing.Point(6, 4);
        	this.DirectX_Box.Name = "DirectX_Box";
        	this.DirectX_Box.Size = new System.Drawing.Size(385, 285);
        	this.DirectX_Box.TabIndex = 12;
        	this.DirectX_Box.TabStop = false;
        	// 
        	// Control_button
        	// 
        	this.Control_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        	this.Control_button.Location = new System.Drawing.Point(217, 241);
        	this.Control_button.Name = "Control_button";
        	this.Control_button.Size = new System.Drawing.Size(138, 23);
        	this.Control_button.TabIndex = 13;
        	this.Control_button.Text = "Управление";
        	this.Control_button.UseVisualStyleBackColor = true;
        	this.Control_button.Click += new System.EventHandler(this.Control_buttonClick);
        	// 
        	// EnableShaders_Box
        	// 
        	this.EnableShaders_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.EnableShaders_Box.Checked = true;
        	this.EnableShaders_Box.CheckState = System.Windows.Forms.CheckState.Checked;
        	this.EnableShaders_Box.Location = new System.Drawing.Point(30, 245);
        	this.EnableShaders_Box.Name = "EnableShaders_Box";
        	this.EnableShaders_Box.Size = new System.Drawing.Size(320, 17);
        	this.EnableShaders_Box.TabIndex = 12;
        	this.EnableShaders_Box.Text = "Рисовать скайбокс";
        	// 
        	// Screen_Size_label
        	// 
        	this.Screen_Size_label.AutoSize = true;
        	this.Screen_Size_label.Location = new System.Drawing.Point(30, 35);
        	this.Screen_Size_label.Name = "Screen_Size_label";
        	this.Screen_Size_label.Size = new System.Drawing.Size(129, 13);
        	this.Screen_Size_label.TabIndex = 7;
        	this.Screen_Size_label.Text = "Разрешение экрана:";
        	// 
        	// NonExclusiveMouse_Box
        	// 
        	this.NonExclusiveMouse_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.NonExclusiveMouse_Box.Location = new System.Drawing.Point(30, 215);
        	this.NonExclusiveMouse_Box.Name = "NonExclusiveMouse_Box";
        	this.NonExclusiveMouse_Box.Size = new System.Drawing.Size(320, 17);
        	this.NonExclusiveMouse_Box.TabIndex = 11;
        	this.NonExclusiveMouse_Box.Text = "Не захватывать мышь";
        	// 
        	// Splines_Cond_label
        	// 
        	this.Splines_Cond_label.AutoSize = true;
        	this.Splines_Cond_label.Location = new System.Drawing.Point(30, 95);
        	this.Splines_Cond_label.Name = "Splines_Cond_label";
        	this.Splines_Cond_label.Size = new System.Drawing.Size(166, 13);
        	this.Splines_Cond_label.TabIndex = 6;
        	this.Splines_Cond_label.Text = "Качество кривых рельсов:";
        	// 
        	// NonExclusiveKeyboard_Box
        	// 
        	this.NonExclusiveKeyboard_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.NonExclusiveKeyboard_Box.Location = new System.Drawing.Point(30, 185);
        	this.NonExclusiveKeyboard_Box.Name = "NonExclusiveKeyboard_Box";
        	this.NonExclusiveKeyboard_Box.Size = new System.Drawing.Size(320, 17);
        	this.NonExclusiveKeyboard_Box.TabIndex = 11;
        	this.NonExclusiveKeyboard_Box.Text = "Не захватывать клавиатуру";
        	// 
        	// Vertex_label
        	// 
        	this.Vertex_label.AutoSize = true;
        	this.Vertex_label.Location = new System.Drawing.Point(30, 65);
        	this.Vertex_label.Name = "Vertex_label";
        	this.Vertex_label.Size = new System.Drawing.Size(114, 13);
        	this.Vertex_label.TabIndex = 5;
        	this.Vertex_label.Text = "Vertex processing:";
        	// 
        	// NoStops_Box
        	// 
        	this.NoStops_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.NoStops_Box.Location = new System.Drawing.Point(30, 155);
        	this.NoStops_Box.Name = "NoStops_Box";
        	this.NoStops_Box.Size = new System.Drawing.Size(320, 17);
        	this.NoStops_Box.TabIndex = 11;
        	this.NoStops_Box.Text = "Не рисовать текст на значках остановок";
        	// 
        	// VertexProcessing_Box
        	// 
        	this.VertexProcessing_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.VertexProcessing_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.VertexProcessing_Box.Items.AddRange(new object[] {
			"Аппаратная",
			"Программная",
			"Смешанная"});
        	this.VertexProcessing_Box.Location = new System.Drawing.Point(217, 62);
        	this.VertexProcessing_Box.Name = "VertexProcessing_Box";
        	this.VertexProcessing_Box.Size = new System.Drawing.Size(138, 21);
        	this.VertexProcessing_Box.TabIndex = 9;
        	this.VertexProcessing_Box.SelectedIndexChanged += new System.EventHandler(this.VertexProcessing_BoxSelectedIndexChanged);
        	// 
        	// EnableSound_Box
        	// 
        	this.EnableSound_Box.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
        	this.EnableSound_Box.Checked = true;
        	this.EnableSound_Box.CheckState = System.Windows.Forms.CheckState.Checked;
        	this.EnableSound_Box.Location = new System.Drawing.Point(30, 125);
        	this.EnableSound_Box.Name = "EnableSound_Box";
        	this.EnableSound_Box.Size = new System.Drawing.Size(166, 17);
        	this.EnableSound_Box.TabIndex = 11;
        	this.EnableSound_Box.Text = "Звук";
        	// 
        	// Screen_Box
        	// 
        	this.Screen_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.Screen_Box.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        	this.Screen_Box.Items.AddRange(new object[] {
			"640x480",
			"800x600",
			"1024x768",
			"1152x864",
			"1280x960",
			"1280x1024",
			"1600x900",
			"Настроить...",
			"1280x1024, 75Hz"});
        	this.Screen_Box.Location = new System.Drawing.Point(217, 32);
        	this.Screen_Box.Name = "Screen_Box";
        	this.Screen_Box.Size = new System.Drawing.Size(138, 21);
        	this.Screen_Box.TabIndex = 8;
        	this.Screen_Box.SelectedIndexChanged += new System.EventHandler(this.Screen_Box_SelectedIndexChanged);
        	// 
        	// Volume_TrackBar
        	// 
        	this.Volume_TrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.Volume_TrackBar.AutoSize = false;
        	this.Volume_TrackBar.LargeChange = 10;
        	this.Volume_TrackBar.Location = new System.Drawing.Point(217, 121);
        	this.Volume_TrackBar.Maximum = 100;
        	this.Volume_TrackBar.Name = "Volume_TrackBar";
        	this.Volume_TrackBar.Size = new System.Drawing.Size(138, 24);
        	this.Volume_TrackBar.SmallChange = 5;
        	this.Volume_TrackBar.TabIndex = 10;
        	this.Volume_TrackBar.Value = 80;
        	// 
        	// Rail_Box
        	// 
        	this.Rail_Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
        	this.Rail_Box.AutoSize = false;
        	this.Rail_Box.Location = new System.Drawing.Point(217, 91);
        	this.Rail_Box.Maximum = 400;
        	this.Rail_Box.Minimum = 100;
        	this.Rail_Box.Name = "Rail_Box";
        	this.Rail_Box.Size = new System.Drawing.Size(138, 24);
        	this.Rail_Box.TabIndex = 10;
        	this.Rail_Box.Value = 400;
        	// 
        	// Editor_Button
        	// 
        	this.Editor_Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        	this.Editor_Button.BackColor = System.Drawing.SystemColors.ActiveCaption;
        	this.Editor_Button.DialogResult = System.Windows.Forms.DialogResult.Ignore;
        	this.Editor_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        	this.Editor_Button.Location = new System.Drawing.Point(180, 360);
        	this.Editor_Button.Name = "Editor_Button";
        	this.Editor_Button.Size = new System.Drawing.Size(115, 35);
        	this.Editor_Button.TabIndex = 1;
        	this.Editor_Button.Text = "Редактор";
        	this.Editor_Button.UseVisualStyleBackColor = false;
        	this.Editor_Button.Click += new System.EventHandler(this.Editor_ButtonClick);
        	// 
        	// LoadCity_Dialog
        	// 
        	this.LoadCity_Dialog.DefaultExt = "city";
        	this.LoadCity_Dialog.Filter = "Trancity Maps (*.city)|*.city|Все файлы (*.*)|*.*";
        	this.LoadCity_Dialog.InitialDirectory = "..\\Cities";
        	this.LoadCity_Dialog.Title = "Загрузить карту";
        	// 
        	// Options
        	// 
        	this.AcceptButton = this.Launch_Buttton;
        	this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
        	this.BackColor = System.Drawing.SystemColors.InactiveCaption;
        	this.CancelButton = this.Exit_Button;
        	this.ClientSize = new System.Drawing.Size(469, 427);
        	this.Controls.Add(this.Tab_Control);
        	this.Controls.Add(this.Exit_Button);
        	this.Controls.Add(this.Launch_Buttton);
        	this.Controls.Add(this.Editor_Button);
        	this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.MaximizeBox = false;
        	this.MinimizeBox = false;
        	this.Name = "Options";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.Text = "Trancity 0.6.3.3.1";
        	this.Closing += new System.ComponentModel.CancelEventHandler(this.Options_Form_Closing);
        	this.Load += new System.EventHandler(this.Options_Form_Load);
        	this.Tab_Control.ResumeLayout(false);
        	this.Options_Page.ResumeLayout(false);
        	this.Options_Group.ResumeLayout(false);
        	this.Options_Group.PerformLayout();
        	this.Players_Page.ResumeLayout(false);
        	this.Players_Page.PerformLayout();
        	this.Players_Box.ResumeLayout(false);
        	this.Players_Box.PerformLayout();
        	this.DirectX_Page.ResumeLayout(false);
        	this.DirectX_Box.ResumeLayout(false);
        	this.DirectX_Box.PerformLayout();
        	((System.ComponentModel.ISupportInitialize)(this.Volume_TrackBar)).EndInit();
        	((System.ComponentModel.ISupportInitialize)(this.Rail_Box)).EndInit();
        	this.ResumeLayout(false);

        }
        private System.Windows.Forms.TrackBar Volume_TrackBar;
        private System.Windows.Forms.CheckBox EnableSound_Box;
        private System.Windows.Forms.Button Control_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;

        #endregion
    }
}