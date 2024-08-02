/*
 * Сделано в SharpDevelop.
 * Пользователь: serg
 * Дата: 14.12.2011
 * Время: 22:34
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
namespace Trancity
{
	using System;
	using Common;
	using System.IO;
	using System.Windows.Forms;
	using System.Runtime.InteropServices;
	using System.Collections.Generic;
	using System.Xml;
	
	public static class Localization
	{
		public static ПсевдоЛокализация current_;// = null;
		public static readonly List<ПсевдоЛокализация> localizations = new List<ПсевдоЛокализация>();
		
		static Localization()
		{
//			var document = new XmlDocument();
            var path = Application.StartupPath + @"\Data\Localization\";
            if (!Directory.Exists(path))
            {
            	Logging.Write("Localization", "Directory " + path + " not found!");
            	return;
            }
            string fileext;
            foreach (var file in Directory.GetFiles(path))
            {
            	fileext = Path.GetExtension(file);
            	if (fileext != ".xml") continue;
            	try 
            	{
//	            	document.Load(file);//path + "localization.xml");
//	            	Xml.document = document;
            		var document = Xml.Connect(file);
	            	//----
	            	var localization = new ПсевдоЛокализация();
	            	localization.name = Path.GetFileNameWithoutExtension(file);
	            	localization.controllist = new List<TextListStruct>();
	            	localization.menulist = new List<TextListStruct>();
	            	localization.tipslist = new List<TextListStruct>();
	            	var element = document["Trancity"];
	            	var element2 = element["Localization"];
	            	var reserve = element2["Reserve"];
	            	var messages = element2["Messages"];
	            	var optionsform = element2["Forms"];
	            	var editor_menu = element2["Menu_Editor"];
	            	var tooltips = element2["Tips"];            	
	            	var general = element2["General"];
	            	var tramway = element2["Tramway"];
	            	var trolleybus = element2["Trolleybus"];
	            	var bus = element2["Bus"];
	            	//----reserve:
	            	localization.windowed = Xml.GetString(reserve["Windowed"], "в окне");
	            	localization.empty = Xml.GetString(reserve["Empty"], "Нет");
	            	localization.random = Xml.GetString(reserve["Random"], "Случайный");
	            	localization.shtangi_loosed = Xml.GetString(reserve["Shtangi_loosed"], "штанги слетели!");
	            	localization.of = Xml.GetString(reserve["Of"], "из");
	            	localization.edit = Xml.GetString(reserve["Edit"], "Настроить...");
	            	localization.load_city = Xml.GetString(reserve["Load_city"], "Загрузка города...");
	            	localization.load_models = Xml.GetString(reserve["Load_models"], "Загрузка моделей...");
	            	localization.load_shaders = Xml.GetString(reserve["Load_shaders"], "Загрузка шейдеров...");
	            	localization.load_objects = Xml.GetString(reserve["Load_objects"], "Загрузка объектов...");
	            	localization.load_stops = Xml.GetString(reserve["Load_stops"], "Загрузка остановок...");
	            	localization.load_sounds = Xml.GetString(reserve["Load_sounds"], "Загрузка звуков...");
	            	localization.save_city = Xml.GetString(reserve["Save_city"], "Сохранене города...");
	            	//----messages:
	            	localization.joints_begin_end = Xml.GetString(messages["Joints_no_begin_no_end"]);
	            	localization.joints_begin = Xml.GetString(messages["Joints_no_begin"]);
	            	localization.joints_end = Xml.GetString(messages["Joints_no_end"]);
	            	localization.joints_checked = Xml.GetString(messages["Joints_checked"]);
	            	localization.routes_computed = Xml.GetString(messages["Routes_already_computed"]);
	            	localization.route_failed = Xml.GetString(messages["Route_failed"]);
	            	localization.save_quit = Xml.GetString(messages["Save_city_quit"]);
	            	localization.save_run = Xml.GetString(messages["Save_city_run"]);
	            	localization.save_only = Xml.GetString(messages["Save_city"]);
	            	localization.save_failed = Xml.GetString(messages["Save_city_failed"]);
	            	localization.min_radius = Xml.GetString(messages["Min_curve_radius"]);
	            	localization.no_curves = Xml.GetString(messages["Curve_not_found"]);
	            	//----options, another...
	            	if (optionsform != null) foreach (XmlElement el in optionsform.ChildNodes)
	            	{
	            		localization.controllist.Add(new TextListStruct(){ name = el.Name, text = el.InnerText });
	            	}
	            	if (editor_menu != null) foreach (XmlElement el in editor_menu.ChildNodes)
	            	{
	            		localization.menulist.Add(new TextListStruct(){ name = el.Name, text = el.InnerText });
	            	}
	            	if (tooltips != null) foreach (XmlElement el in tooltips.ChildNodes)
	            	{
	            		localization.tipslist.Add(new TextListStruct(){ name = el.Name, text = el.InnerText });
	            	}
	            	//----
	            	localization.ctrl_a = Xml.GetString(general["Control_auto"]);
	            	localization.ctrl_s = Xml.GetString(general["Control_semiauto"]);
	            	localization.ctrl_m = Xml.GetString(general["Control_manual"]);
	            	localization.ctrl_pos = Xml.GetString(general["Controller_position"]);
	            	localization.reverse = Xml.GetString(general["Reverse"]);
	            	localization.parking_brake = Xml.GetString(general["Parking_brake"]);
	            	localization.speed = Xml.GetString(general["Speed"]);
	            	localization.speed_km = Xml.GetString(general["Speed_kmh"]);
	            	localization.route = Xml.GetString(general["Route"]);
	                localization.order = Xml.GetString(general["Order"]);
	                localization.route_in_park = Xml.GetString(general["To_park"]);
	                localization.nr = Xml.GetString(general["Next_road"]);
	                localization.nr_pryamo = Xml.GetString(general["Nr_forward"]);
	                localization.nr_right = Xml.GetString(general["Nr_right"]);
	                localization.nr_left = Xml.GetString(general["Nr_left"]);
	                localization.departure_time = Xml.GetString(general["Departure_time"]);
	                localization.arrival_time = Xml.GetString(general["Arrival_time"]);
	                localization.sterling = Xml.GetString(general["Steering"]);
	                localization.ster_l = Xml.GetString(general["Steering_left"]);
	                localization.ster_r = Xml.GetString(general["Steering_right"]);
	                localization.forward = Xml.GetString(general["Forward"]);
	                localization.back = Xml.GetString(general["Back"]);
	                localization.enable = Xml.GetString(general["Enabled"]);
	                localization.disable = Xml.GetString(general["Disabled"]);
	                //----
	                localization.tram = Xml.GetString(tramway["Tramway"]);
	                localization.tram_control = Xml.GetString(tramway["Control"]);
	                localization.tk_on = Xml.GetString(tramway["Pantograph_raised"]);
	                localization.tk_off = Xml.GetString(tramway["Pantograph_omitted"]);
	                //----
	                localization.trol = Xml.GetString(trolleybus["Trolleybus"]);
	                localization.trol_control = Xml.GetString(trolleybus["Control"]);
	                localization.st_on = Xml.GetString(trolleybus["Shtangi_raised"]);
	                localization.st_off = Xml.GetString(trolleybus["Shtangi_omitted"]);
	                localization.air_brake = Xml.GetString(trolleybus["Air_brake"]);
	                localization.ax = Xml.GetString(trolleybus["Standalone_motion"]);
	                localization.ax_power = Xml.GetString(trolleybus["Battery_power"]);
	                //----
	                localization.engine = Xml.GetString(bus["Engine"]);
	                localization.bus_control = Xml.GetString(bus["Control"]);
	                localization.gmod = Xml.GetString(bus["Gearbox_mode"]);
	                localization.cur_pos = Xml.GetString(bus["Current_gear"]);
	                localization.pedal_pos = Xml.GetString(bus["Pedals_position"]);
	                localization.gas = Xml.GetString(bus["Gas"]);
	                localization.brake = Xml.GetString(bus["Brake"]);
	                localizations.Add(localization); 
            	}
            	catch (Exception exc)
            	{
            		Logging.WriteExc(exc, "Localization");
            		Logging.Write("Localization", "Error in file " + file);
            		continue;
            	}
            }
		}
		
		public static void ApplyLocalization(Control basecontrol)
		{
			List<Control> finded_ctrl = new List<Control>();
			foreach (var textl in current_.controllist)
			{
				finded_ctrl = MyGUI.FindControl(basecontrol, textl.name);
				foreach (var control in finded_ctrl)
				{
					control.Text = textl.text;
				}
			}
			if (((Form)basecontrol).Menu != null)
			{
				List<MenuItem> items;
				var menu = ((Form)basecontrol).Menu;
				foreach (var textl in current_.menulist)
				{
					items = MyGUI.FindMenuItems(menu, textl.name);
					foreach (var item in items)
					{
						item.Text = textl.text;
					}
				}
			}
		}
		
		public static void ApplyLocalizationToolBar(ToolBar toolbar)
		{
			foreach (var textl in current_.tipslist)
			{
				foreach (ToolBarButton button in toolbar.Buttons)
				{
					if (button.Name == textl.name) button.ToolTipText = textl.text;
				}
			}
		}
	}
	
		[StructLayout(LayoutKind.Sequential)]
        public struct ПсевдоЛокализация
        {
        	public string name;
        	
        	public string tram;
        	public string trol;
        	public string bus;
        	public string tram_control;
        	public string trol_control;
        	public string bus_control;
        	public string ctrl_a;
        	public string ctrl_m;
        	public string ctrl_s;
        	public string ctrl_pos;
        	public string reverse;
        	public string parking_brake;
        	public string speed;
        	public string speed_km;
        	public string route;
            public string order;
            public string departure_time;//отправление
            public string arrival_time;//прибытие
            public string route_in_park;
            public string nr_pryamo;
            public string nr_left;
            public string nr_right;
            public string nr;
            public string sterling;
            public string ster_l;
            public string ster_r;
            public string forward;
            public string back;
            public string enable;
            public string disable;
            public string engine;
            public string pedal_pos;
            public string gmod;
            public string cur_pos;
            public string gas;
            public string brake;
            public string st_on;
            public string st_off;
            public string tk_on;
            public string tk_off;
            public string air_brake;
            public string ax;
            public string ax_power;
            //reserve:
            public string windowed;
            public string empty;
            public string random;
            public string shtangi_loosed;
            public string edit;
            public string of;
            //
            public string load_city;
            public string load_models;
            public string load_shaders;
            public string load_objects;
            public string load_stops;
            public string load_sounds;
            public string save_city;
            //
            public string joints_begin_end;
            public string joints_begin;
            public string joints_end;
            public string joints_checked;
            public string routes_computed;
            public string route_failed;
            public string save_quit;
            public string save_run;
            public string save_only;
            public string save_failed;
            public string min_radius;
            public string no_curves;
            //all:
            public List<TextListStruct> controllist;
            public List<TextListStruct> menulist;
            public List<TextListStruct> tipslist;
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct TextListStruct
        {
        	public string name;
        	public string text;
        }
}
