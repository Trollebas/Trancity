using Common;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;
using Engine;
using ODE_Test;

namespace Trancity
{
    public class MainForm : Engine.Controls.RenderForm//Form
    {
        //private readonly IContainer _components;
        public static int ticklast;
        public Game _игра;
        public НастройкиЗапуска настройки;
        public static bool debug;
        public static bool thread_test = false;
        public static bool in_editor = false;

        public MainForm()
        {
            InitializeComponent();
        }
        public void ApplyLocalization()
        {
            Localization.ApplyLocalization(this);
           
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

        public void InitializeComponent()
        {
        	this.SuspendLayout();
        	// 
        	// MainForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.ClientSize = new System.Drawing.Size(284, 261);
        	this.Name = "MainForm";
        	this.ResumeLayout(false);

        }

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // now we'll catch every exception. mb...
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.ThreadException += new ThreadExceptionEventHandler(OnUIThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(OnCurrentDomainUnhandledException);
			
            bool sound_flag = false;
            bool nolog = false;
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
                	case "-thread_test":
                		{
                			thread_test = true;
                			Logger.Log("Additional render thread enabled!");
                			break;
                		}
                	case "-nolog":
                		{
                			nolog = true;
                			break;
                		}
                	default:
                		{
                			app.настройки.cityFilename = str;
                			break;
                		}
                }
            }
            if (!nolog)
                Logger.Initialize(Assembly.GetExecutingAssembly());
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
               	var enabledPlayers = new List<НастройкиЗапускаИгрока>();
               	foreach (var игрока in app.настройки.игроки)
                {
                   	if (игрока.вИгре)
                   		enabledPlayers.Add(игрока);
                }
               	app.настройки.игроки = enabledPlayers.ToArray();
                app.настройки.количествоИгроков = app.настройки.игроки.Length;
            }
            sound_flag = !app.настройки.noSound;
            Рельс.качество_рельсов = app.настройки.качествоРельсов;
            Road.качествоДороги = app.настройки.качествоРельсов;
            Рельс.стрелки_наоборот = app.настройки.стрелкиНаоборот;
            Stop.неЗагружаемКартинки = app.настройки.noStops;
            SkyBox.draw = app.настройки.enableShaders;
            
            // TODO: убрать размеры экрана из глобальных переменных и из ini-файла.
            MyDirect3D.Window_Width = app.настройки.размерЭкрана.Width;
            MyDirect3D.Window_Height = app.настройки.размерЭкрана.Height;
            Directory.SetCurrentDirectory(Application.StartupPath + @"\Data");
            if (!MyDirect3D.Initialize2(app))
            {
            	throw new Exception(Localization.current_.initializewopt);
            }
            app.Show();
            Common.MyGUI.Splash();
            if (sound_flag)
            {
              	MyXAudio2.Initialize(Engine.Sound.SoundDeviceType.XAudio2);
               	MyXAudio2.Device.MasterVolume = app.настройки.soundVolume / 100.0f;
            }
            if (!MyDirectInput.Initialize(app, !app.настройки.nonExclusiveKeyboard, !app.настройки.nonExclusiveMouse))
            {
                throw new Exception(Localization.current_.initializewdi);
            }
            app._игра = new Game();
            Common.MyGUI.splash_title = Localization.current_.namegame;
            Common.MyGUI.Splash();
            Common.MyGUI.load_status = 0;
            Common.MyGUI.status_string = Localization.current_.load_city;
            Common.MyGUI.Splash();
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
            if (sound_flag)
            {
                app._игра.мир.CreateSound();
//              Logger.Log("Sounds", string.Format("Total: {0} with size {1}", SoundManager.BuffersCount, SoundManager.MemoryUsage));
            }
            else //Localization.current_.souen Localization.current_.soudis Localization.current_.souplay
            {
                MyDirectInput.Acquire();
            }
            Logger.Log(sound_flag ? Localization.current_.souen : Localization.current_.soudis);
            Logger.Log(Localization.current_.souplay);
			if (thread_test)
			{
				ThreadPoolTest.RunGameProcess(app._игра, sound_flag);
				app.Close();
				return;
			}
            while (app.Created)
            {
                if (!MyDirectInput.Process() && MyDirectInput.alt_f4)
                {
                    MyDirectInput.Free();
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
                app._игра.Render();
                Application.DoEvents();
            }

        }
        
        protected override void OnDeactivate(EventArgs e)
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
		}
        
        private static void OnUIThreadException(object sender, ThreadExceptionEventArgs t)
		{
        	var exception = t.Exception;
        	Logger.LogException(exception, "UI thread");
			var result = ExceptionHandlerForm.ShowHandlerDialog(exception, "UI thread", false);
			if (result != DialogResult.Abort) return;
			Environment.Exit(0);
		}
		
		private static void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			var exception = (Exception)e.ExceptionObject;
			Logger.LogException(exception, "CurrentDomain.UnhandledException");
			var result = ExceptionHandlerForm.ShowHandlerDialog(exception, "CurrentDomain.UnhandledException event", e.IsTerminating);
			if (result == DialogResult.Retry) return;
			Environment.Exit(0);
		}

        private void ЗагрузитьНастройки()
        {
            Directory.SetCurrentDirectory(Application.StartupPath);
            using (var ini = new Ini(@".\options.ini", StreamWorkMode.Read))
            {
	            настройки.размерЭкрана = new Size(ini.ReadInt("Common", "displayWidth", 0x500), ini.ReadInt("Common", "displayHeight", 960));
	            настройки.начальноеВремя = ini.ReadInt("Common", "startupTime", 0x6270);
				настройки.автоматическоеУправление = ini.ReadBool("Common", "autoControl", false);
	            настройки.поворачиватьКамеру = ini.ReadBool("Common", "rotateCam", true);
	            настройки.deviceType = (SlimDX.Direct3D9.DeviceType)ini.ReadInt("Common", "deviceType", 1);
	            настройки.createFlags = (SlimDX.Direct3D9.CreateFlags)ini.ReadInt("Common", "createFlags", 0x40);
	            настройки.качествоРельсов = ini.ReadDouble("Common", "splinesQuality", 4.0);
	            настройки.cityFilename = ini.Read("Common", "cityFilename", string.Empty);
	            настройки.стрелкиНаоборот = ini.ReadBool("Common", "invRailArrows", false);
	            настройки.noSound = ini.ReadBool("Common", "noSound", false);
	            настройки.soundVolume = ini.ReadInt("Common", "soundVolume", 80);
	            настройки.noStops = ini.ReadBool("Common", "noStops", false);
	            настройки.nonExclusiveKeyboard = ini.ReadBool("Common", "nonExclusiveKeyboard", false);
	            настройки.nonExclusiveMouse = ini.ReadBool("Common", "nonExclusiveMouse", false);
	            настройки.enableShaders = ini.ReadBool("Common", "enableShaders", false);
	            настройки.language = ini.Read("Common", "language", "Russian");
	            настройки.количествоИгроков = ini.ReadInt("Common", "playersCount", 0);
	            настройки.игроки = new НастройкиЗапускаИгрока[настройки.количествоИгроков];
	            for (var i = 0; i < настройки.количествоИгроков; i++)
	            {
	                var section = string.Format("Player {0}", i);
	                настройки.игроки[i].имя = ini.Read(section, "name", "Игрок " + i);
	                настройки.игроки[i].inputGuid = new Guid(ini.Read(section, "inputGuid", string.Empty));
	                настройки.игроки[i].подвижнойСостав = ini.Read(section, "transport", "");
	                настройки.игроки[i].маршрут = ini.ReadInt(section, "route", 0);
	                настройки.игроки[i].наряд = ini.ReadInt(section, "order", 0);
	                настройки.игроки[i].вИгре = ini.ReadBool(section, "inGame", false);
	            }
            }
        }

        private void СохранитьНастройки()
        {
            Directory.SetCurrentDirectory(Application.StartupPath);
            using (var ini = new Ini(@".\options.ini", StreamWorkMode.Write))
            {
	            ini.Write("Common", "displayWidth", настройки.размерЭкрана.Width.ToString());
	            ini.Write("Common", "displayHeight", настройки.размерЭкрана.Height.ToString());
	            ini.Write("Common", "startupTime", настройки.начальноеВремя.ToString());
	            ini.Write("Common", "autoControl", настройки.автоматическоеУправление.ToString());
	            ini.Write("Common", "rotateCam", настройки.поворачиватьКамеру.ToString());
	            ini.Write("Common", "deviceType", ((int)настройки.deviceType).ToString());
	            ini.Write("Common", "createFlags", ((int)настройки.createFlags).ToString());
	            ini.Write("Common", "splinesQuality", настройки.качествоРельсов.ToString());
	            ini.Write("Common", "cityFilename", (настройки.cityFilename != null) ? настройки.cityFilename : "");
	            ini.Write("Common", "invRailArrows", настройки.стрелкиНаоборот.ToString());
	            ini.Write("Common", "noSound", настройки.noSound.ToString());
	            ini.Write("Common", "soundVolume", настройки.soundVolume.ToString());
	            ini.Write("Common", "noStops", настройки.noStops.ToString());
	            ini.Write("Common", "nonExclusiveKeyboard", настройки.nonExclusiveKeyboard.ToString());
	            ini.Write("Common", "nonExclusiveMouse", настройки.nonExclusiveMouse.ToString());
	            ini.Write("Common", "enableShaders", настройки.enableShaders.ToString());
	            ini.Write("Common", "language", настройки.language.ToString());
	            ini.Write("Common", "playersCount", настройки.количествоИгроков.ToString());
	            for (var i = 0; i < настройки.количествоИгроков; i++)
	            {
	                var section = string.Format("Player {0}", i);
	                ini.Write(section, "name", настройки.игроки[i].имя);//"имя"
	                ini.Write(section, "inputGuid", настройки.игроки[i].inputGuid.ToString());
	                ini.Write(section, "transport", настройки.игроки[i].подвижнойСостав);
	                ini.Write(section, "route", настройки.игроки[i].маршрут.ToString());
	                ini.Write(section, "order", настройки.игроки[i].наряд.ToString());
	                ini.Write(section, "inGame", настройки.игроки[i].вИгре.ToString());
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
            public SlimDX.Direct3D9.CreateFlags createFlags;
            public int количествоИгроков;
            public double качествоРельсов;
            public НастройкиЗапускаИгрока[] игроки;
            public string cityFilename;
            public bool стрелкиНаоборот;
            public bool noSound;
            public int soundVolume;
            public bool noStops;
            public bool nonExclusiveKeyboard;
            public bool nonExclusiveMouse;
            public bool enableShaders;
            public string language;
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

