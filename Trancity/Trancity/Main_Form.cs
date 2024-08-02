namespace Trancity
{
    using Common;
//    using Microsoft.DirectX.Direct3D;
//    using Microsoft.DirectX.DirectInput;
    using SlimDX.Direct3D9;
    using SlimDX.DirectInput;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class MainForm : Form
    {
        //private readonly IContainer _components;
        public static int ticklast;
        public Game _игра;
        public НастройкиЗапуска настройки;
//        private static ThreadTest testThread = null;
//        private static ThreadTest testThread2 = null;
        public static bool debug;
        public static bool no_thread = true;
        public static bool in_editor = false;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
        	/*
            if (disposing && (_components != null))
            {
                _components.Dispose();
            }
            base.Dispose(disposing);
            */
        }

        private void InitializeComponent()
        {
        	this.SuspendLayout();
        	// 
        	// MainForm
        	// 
        	this.ClientSize = new System.Drawing.Size(346, 345);
        	this.Name = "MainForm";
        	this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        	this.ResumeLayout(false);
        }

        [STAThread]
        // ReSharper disable SuggestBaseTypeForParameter
        // ReSharper disable UnusedMember.Local
        private static void Main(string[] args)
        // ReSharper restore UnusedMember.Local
        // ReSharper restore SuggestBaseTypeForParameter
        {
            if (args == null) throw new ArgumentNullException("args");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                bool sound_flag = false;
                Logging.Init();
                MyFeatures.CheckFolders(Application.StartupPath);
                var app = new MainForm { настройки = new НастройкиЗапуска() };
                app.ЗагрузитьНастройки();
                foreach (var str in args)
                {
                	switch (str)
                	{
                			case "-limited":
	                		{
	                			Cheats.limited = true;
	                			break;
	                		}
                			case "-debug_strings":
                			{
                				debug = true;
                				break;
                			}
                			case "-thread_test"://"-no_thread":
                			{
                				no_thread = false;
                				Logging.Write("Additional render thread enabled!");
                				break;
                			}
                			default:
                			{
                				app.настройки.cityFilename = str;
                				break;
                			}
                	}
                }
                MyDirectInput.EnumerateDevices();
                var result = new Options(app).ShowDialog();
                if (result != DialogResult.OK)
                {
                    app.Close();
                    if (result == DialogResult.Ignore)
                    {
                        Application.Run(new Editor());
                    }
                    return;
                }
                app.СохранитьНастройки();
                if (app.настройки.игроки.Length > 1)
                {
                	var num = 0;
                    foreach (var игрока in app.настройки.игроки)
                    {
                    	if (игрока.вИгре) num++;
                    }
                    var игрокаArray = new НастройкиЗапускаИгрока[num];
                    var index = 0;
                    foreach (var игрока2 in app.настройки.игроки)
                    {
                        if (!игрока2.вИгре) continue;
                        игрокаArray[index] = игрока2;
                        index++;
                    }
                    app.настройки.игроки = игрокаArray;
                    app.настройки.количествоИгроков = игрокаArray.Length;
                }
                sound_flag = !app.настройки.noSound;
                Рельс.качество_рельсов = app.настройки.качествоРельсов;
//                MyDirect3D.zfarplane = app.настройки.zfarplane;
                Road.качествоДороги = app.настройки.качествоРельсов;
                Рельс.стрелки_наоборот = app.настройки.стрелкиНаоборот;
                Stop.неЗагружаемКартинки = app.настройки.noStops;
                SkyBox.draw = app.настройки.enableShaders;
//                World.deleteObj = app.настройки.deleteFarObject;
                try
                {
                	MyDirect3D.Window_Width = app.настройки.размерЭкрана.Width;
                	MyDirect3D.Window_Height = app.настройки.размерЭкрана.Height;
                    Directory.SetCurrentDirectory(Application.StartupPath + @"\Data");
                    app.Show();
                    if (!MyDirect3D.Initialize(app, app.настройки.deviceType, app.настройки.createFlags, false))
                    {
                        MessageBox.Show("Could not initialize Direct3D.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    }
                    else
                    {
                        MyGUI.Splash();
                        if (sound_flag && !MyDirectSound.Initialize(app))
	                    {
	                        MessageBox.Show("Could not initialize DirectSound.\nПрограмма будет работать без звука.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
	                        sound_flag = false;
	                    }
                        if (!MyDirectInput.Initialize(app, !app.настройки.nonExclusiveKeyboard, !app.настройки.nonExclusiveMouse))
                        {
                            throw new DirectInputException("Could not initialize DirectInput.");
                        }
                        app._игра = new Game();
                        MyGUI.splash_title = "Trancity";
                        MyGUI.Splash();//0xffffff);
                        MyGUI.load_status = 0;
                        MyGUI.status_string = Localization.current_.load_city;//"Загрузка города...";
                        MyGUI.Splash();
                        app._игра.menu = new MyMenu();
                        app._игра.мир = new World();
                        app._игра.мир.ЗагрузитьГород(app.настройки.cityFilename);
                        app._игра.мир.time = app.настройки.начальноеВремя;
                        if (app._игра.мир.time < 10800.0)
                        {
                            app._игра.мир.time += 86400.0;
                        }
                        app._игра.мир.ДобавитьТранспорт(app.настройки, app._игра);
                        app._игра.мир.Create_Meshes();
//                        if (!no_thread) MeshObject.BeginManager();
                        if (sound_flag)
                        {
                            /*try
                            {*/
                                if (MyDirectInput.Acquire())
                                {
                                    app._игра.мир.CreateSound();
//									MyDirectSound.TestInt();
                                }
                                else
                                {
                                    MessageBox.Show("Sound buffers создавались, когда приложение было неактивно.\nПрограмма будет работать без звука.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    sound_flag = false;
                                }
                            /*}
                            catch
                            {
                                MessageBox.Show("Could create sound buffers.\nПрограмма будет работать без звука.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                flag = false;
                            }*/
                        }
                        else MyDirectInput.Acquire();
                        Logging.Write(sound_flag ? "Sound enabled" : "Sound disabled");
                        Logging.Write("Game started");
                        while (app.Created)
                        {
                            if (!MyDirectInput.Process() && MyDirectInput.alt_f4)
                            {
                                MyDirectInput.Free();
                                MyDirectSound.Free();
                                app.Close();
                                return;
                            }
                            app._игра.Process_Input();
                            if (app._игра.активна)
                            {
                                app._игра.мир.Обновить(app._игра.игроки);
                            }
                            else
                            {
                                app._игра.мир.Обновить_время();
                            }
                            if (sound_flag)
                            {
                                app._игра.мир.UpdateSound(app._игра.игроки, app._игра.активна);
                            }
                            /*lock (MeshObject.timer) */app._игра.Render();
                            //WaitUntilNextFrame();
                        }
                    }
                }
                catch (Exception exception)
                {
                  	Logging.WriteExc(exception);
                    var str2 = "Unhandled exception of type " + exception.GetType() + "\n" + exception;
                    app.Close(); MyDirectInput.alt_f4 = true;
                    MessageBox.Show(str2 + "\n\nПожалуйста, сообщите создателю игры об этой ошибке.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            catch (Exception exception2)
            {
                try
                {
                    MessageBox.Show(("Произошла ошибка во время операций с настройками или в окне опций.\nUnhandled exception of type " + exception2.GetType() + "\n" + exception2) + "\n\nПожалуйста, сообщите создателю игры об этой ошибке.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                catch
                {
                    MessageBox.Show("Произошла неизвестная ошибка.\n\nПожалуйста, сообщите создателю игры об этой ошибке.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
            finally
            {
            	Logging.End();
            }
        }

        //public static void WaitUntilNextFrame()
        //{
        //    int tickCount;
        //    do
        //    {
        //        Msg message = new Msg();
        //        if (Common.Windows.Windows.GetMessage(ref message, IntPtr.Zero, 0, 0))
        //        {
        //            Common.Windows.Windows.TranslateMessage(ref message);
        //            Common.Windows.Windows.DispatchMessage(ref message);
        //        }
        //        Application.DoEvents();
        //        tickCount = Environment.TickCount;
        //    }
        //    while ((tickCount - ticklast) < _frameTicks);
        //    ticklast = tickCount;
        //}
        
        /*protected override void OnDeactivate(EventArgs e)
        {
        	if (this.WindowState == FormWindowState.Minimized)
		    {
		    	this.ShowInTaskbar = false;
		    	this.Visible = false;
		    }
        	base.OnDeactivate(e);
        }
        
		protected override void OnActivated(EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
		    {
		    	this.ShowInTaskbar = true;
		    	this.Visible = true;
		    	this.WindowState = FormWindowState.Normal;
		    }
			base.OnActivated(e);
		}*/

        private void ЗагрузитьНастройки()
        {
            Directory.SetCurrentDirectory(Application.StartupPath);
            using (var ini = new Ini(@".\options.ini", StreamWorkMode.Read))
            {
	            настройки.размерЭкрана = new Size(ini.ReadInt("Common", "displayWidth", 0x500), ini.ReadInt("Common", "displayHeight", 960));
	            настройки.начальноеВремя = ini.ReadInt("Common", "startupTime", 0x6270);//"начальноеВремя"//"размер_экрана_x""размер_экрана_y"
				настройки.автоматическоеУправление = ini.ReadBool("Common", "autoControl", false);//"автоматическоеУправление"
	            настройки.поворачиватьКамеру = ini.ReadBool("Common", "rotateCam", true);//"поворачиватьКамеру"
	            настройки.deviceType = (SlimDX.Direct3D9.DeviceType)ini.ReadInt("Common", "deviceType", 1);
	            настройки.createFlags = (CreateFlags)ini.ReadInt("Common", "createFlags", 0x40);
	            настройки.качествоРельсов = ini.ReadDouble("Common", "splinesQuality", 4.0);//"качествоРельсов"
	            настройки.cityFilename = ini.Read("Common", "cityFilename", string.Empty);
	            настройки.стрелкиНаоборот = ini.ReadBool("Common", "invRailArrows", false);//"стрелкиНаоборот"
	            настройки.noSound = ini.ReadBool("Common", "noSound", false);
	            настройки.noStops = ini.ReadBool("Common", "noStops", false);
	            настройки.nonExclusiveKeyboard = ini.ReadBool("Common", "nonExclusiveKeyboard", false);
	            настройки.nonExclusiveMouse = ini.ReadBool("Common", "nonExclusiveMouse", false);
	            настройки.enableShaders = ini.ReadBool("Common", "enableShaders", false);
	            настройки.langugage = ini.Read("Common", "langugage", "Russian");
	            настройки.количествоИгроков = ini.ReadInt("Common", "playersCount", 0);//"количествоИгроков"
	            настройки.игроки = new НастройкиЗапускаИгрока[настройки.количествоИгроков];
	            for (var i = 0; i < настройки.количествоИгроков; i++)
	            {
	                var section = string.Format("Player {0}", i);
	                настройки.игроки[i].имя = ini.Read(section, "name", "Игрок " + i);//"имя"
	                настройки.игроки[i].inputGuid = new Guid(ini.Read(section, "inputGuid", string.Empty));//SystemGuid.Keyboard.ToString()));
	                настройки.игроки[i].подвижнойСостав = ini.Read(section, "transport", "");//"подвижнойСостав"
	                настройки.игроки[i].маршрут = ini.ReadInt(section, "route", 0);//"маршрут"
	                настройки.игроки[i].наряд = ini.ReadInt(section, "order", 0);//"наряд"
	                настройки.игроки[i].вИгре = ini.ReadBool(section, "inGame", false);//"вИгре"
	            }
            }
        }

        private void СохранитьНастройки()
        {
            Directory.SetCurrentDirectory(Application.StartupPath);
            using (var ini = new Ini(@".\options.ini", StreamWorkMode.Write))
            {
	            ini.Write("Common", "displayWidth", настройки.размерЭкрана.Width.ToString());//"размер_экрана_x"
	            ini.Write("Common", "displayHeight", настройки.размерЭкрана.Height.ToString());//"размер_экрана_y"
	            ini.Write("Common", "startupTime", настройки.начальноеВремя.ToString());//"начальноеВремя"
	            ini.Write("Common", "autoControl", настройки.автоматическоеУправление.ToString());//"автоматическоеУправление"
	            ini.Write("Common", "rotateCam", настройки.поворачиватьКамеру.ToString());//"поворачиватьКамеру"
	            ini.Write("Common", "deviceType", ((int)настройки.deviceType).ToString());
	            ini.Write("Common", "createFlags", ((int)настройки.createFlags).ToString());
	            ini.Write("Common", "splinesQuality", настройки.качествоРельсов.ToString());//"качествоРельсов"
	            ini.Write("Common", "cityFilename", (настройки.cityFilename != null) ? настройки.cityFilename : "");
	            ini.Write("Common", "invRailArrows", настройки.стрелкиНаоборот.ToString());//"стрелкиНаоборот"
	            ini.Write("Common", "noSound", настройки.noSound.ToString());
	            ini.Write("Common", "noStops", настройки.noStops.ToString());
	            ini.Write("Common", "nonExclusiveKeyboard", настройки.nonExclusiveKeyboard.ToString());
	            ini.Write("Common", "nonExclusiveMouse", настройки.nonExclusiveMouse.ToString());
	            ini.Write("Common", "enableShaders", настройки.enableShaders.ToString());
	            ini.Write("Common", "langugage", настройки.langugage.ToString());
	            ini.Write("Common", "playersCount", настройки.количествоИгроков.ToString());//"количествоИгроков"
	            for (var i = 0; i < настройки.количествоИгроков; i++)
	            {
	                var section = string.Format("Player {0}", i);
	                ini.Write(section, "name", настройки.игроки[i].имя);//"имя"
	                ini.Write(section, "inputGuid", настройки.игроки[i].inputGuid.ToString());
	                ini.Write(section, "transport", настройки.игроки[i].подвижнойСостав);//"подвижнойСостав"
	                ini.Write(section, "route", настройки.игроки[i].маршрут.ToString());//"маршрут"
	                ini.Write(section, "order", настройки.игроки[i].наряд.ToString());//"наряд"
	                ini.Write(section, "inGame", настройки.игроки[i].вИгре.ToString());//"вИгре"
	            }
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct НастройкиЗапуска
        {
            public int начальноеВремя;
            public Size размерЭкрана;
            public bool автоматическоеУправление;
            public bool поворачиватьКамеру;
            public SlimDX.Direct3D9.DeviceType deviceType;
            public CreateFlags createFlags;
            public int количествоИгроков;
            public double качествоРельсов;
            public НастройкиЗапускаИгрока[] игроки;
            public string cityFilename;
            public bool стрелкиНаоборот;
            public bool noSound;
            public bool noStops;
            public bool nonExclusiveKeyboard;
            public bool nonExclusiveMouse;
            public bool enableShaders;
            public string langugage;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct НастройкиЗапускаИгрока
        {
            public string имя;
            public Guid inputGuid;
            public string подвижнойСостав;
            public int маршрут;
            public int наряд;
            public bool вИгре;
        }
    }
}

