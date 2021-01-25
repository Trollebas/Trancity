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
    using Engine;
	
    public static class Localization
    {
        public static ПсевдоЛокализация current_;
// = null;
        public static readonly List<ПсевдоЛокализация> localizations = new List<ПсевдоЛокализация>();
		
        static Localization()
        {
//			var document = new XmlDocument();
            var path = Application.StartupPath + @"\Data\Localization\";
            if (!Directory.Exists(path)) {
                Logger.Log("Localization", "Директория " + path + " не найдена!");
                return;
            }
            string fileext;
            foreach (var file in Directory.GetFiles(path)) {
                fileext = Path.GetExtension(file);
                if (fileext != ".xml")
                    continue;
                try {
                    var document = Engine.Xml.TryOpenDocument(file);
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
                    var auto = element2["Auto"];
                    //----reserve:
                    localization.windowed = Engine.Xml.GetString(reserve["Windowed"], "в окне");
                    localization.empty = Engine.Xml.GetString(reserve["Empty"], "Нет");
                    localization.random = Engine.Xml.GetString(reserve["Random"], "Случайный");
                    localization.shtangi_loosed = Engine.Xml.GetString(reserve["Shtangi_loosed"], "штанги слетели!");
                    localization.of = Engine.Xml.GetString(reserve["Of"], "из");
                    localization.edit = Engine.Xml.GetString(reserve["Edit"], "Настроить...");
                    localization.city = Engine.Xml.GetString(reserve["City"], "Карты Trancity");
                    localization.allfiles = Engine.Xml.GetString(reserve["AllFiles"], "Все файлы");
                    localization.namegame = Engine.Xml.GetString(reserve["TrancityName"], "Trancity 0.6.3.3.1");
                    localization.nametopeditor = Engine.Xml.GetString(reserve["NameTopEditor"], "Редактор Trancity");
                    localization.сontinuous = Engine.Xml.GetString(reserve["Continuous"], "Продолжить");
                    localization.exit = Engine.Xml.GetString(reserve["Exit"], "Выйти");
                    localization.initializewopt = Engine.Xml.GetString(reserve["InitializeWOpt"], "Не удалось инициализировать Direct3D.");
                    localization.initializewdi = Engine.Xml.GetString(reserve["InitializeWDI"], "Не удалось инициализировать DirectInput.");
                    localization.angle = Engine.Xml.GetString(reserve["Angle"], "Угол: ");
                    localization.scale = Engine.Xml.GetString(reserve["Scale"], "масштаб: ");
                    localization.initializetreadtest = Engine.Xml.GetString(reserve["InitializeWDI"], "Включен дополнительный поток рендеринга!");
                    localization.nulltran = Engine.Xml.GetString(reserve["NullTran"], "Не удалось найти модель ");
                    localization.forplayer = Engine.Xml.GetString(reserve["ForPlayer"], " для игрока ");
                    localization.nullrandom = Engine.Xml.GetString(reserve["NullRandom"], "Случайный маршрут не выбран для игрока ");
                    localization.nullorder = Engine.Xml.GetString(reserve["NullOrder"], "Случайный наряд не выбран для игрока ");
                    localization.tramofrali = Engine.Xml.GetString(reserve["TramOfRail"], "Попытка разместить трамвай на дороге для игрока ");
                    localization.tramofraliroute = Engine.Xml.GetString(reserve["TramOfRailRoute"], "Попытка разместить трамвай (маршрут ");
                    localization.trannull = Engine.Xml.GetString(reserve["TranNull"], "Попытка разместить трамвай (маршрут ");
                    localization.tramofralirouteend = Engine.Xml.GetString(reserve["TramOfRailRouteEnd"], ") на дороге.");
                    localization.souen = Engine.Xml.GetString(reserve["SouEn"], "Звук активирован");
                    localization.soudis = Engine.Xml.GetString(reserve["SouDis"], "Звук неактивен");
                    localization.souplay = Engine.Xml.GetString(reserve["SouPlay"], "Старт звуков");
                    localization.load_city = Engine.Xml.GetString(reserve["Load_city"], "Загрузка города...");
                    localization.load_models = Engine.Xml.GetString(reserve["Load_models"], "Загрузка моделей...");
                    localization.load_shaders = Engine.Xml.GetString(reserve["Load_shaders"], "Загрузка шейдеров...");
                    localization.load_objects = Engine.Xml.GetString(reserve["Load_objects"], "Загрузка объектов...");
                    localization.load_stops = Engine.Xml.GetString(reserve["Load_stops"], "Загрузка остановок...");
                    localization.load_sounds = Engine.Xml.GetString(reserve["Load_sounds"], "Загрузка звуков...");
                    localization.save_city = Engine.Xml.GetString(reserve["Save_city"], "Сохранене города...");
                    localization.mainform = Engine.Xml.GetString(optionsform["MainForm"], "Game Trancity");
                    localization.functionnowork = Engine.Xml.GetString(optionsform["FunctionNoWork"], "Данная функция ещё пока недоступна.");
                    //----messages:
                    localization.joints_begin_end = Engine.Xml.GetString(messages["Joints_no_begin_no_end"]);
                    localization.joints_begin = Engine.Xml.GetString(messages["Joints_no_begin"]);
                    localization.joints_end = Engine.Xml.GetString(messages["Joints_no_end"]);
                    localization.joints_checked = Engine.Xml.GetString(messages["Joints_checked"]);
                    localization.routes_computed = Engine.Xml.GetString(messages["Routes_already_computed"]);
                    localization.route_failed = Engine.Xml.GetString(messages["Route_failed"]);
                    localization.save_quit = Engine.Xml.GetString(messages["Save_city_quit"]);
                    localization.save_run = Engine.Xml.GetString(messages["Save_city_run"]);
                    localization.save_only = Engine.Xml.GetString(messages["Save_city"]);
                    localization.save_failed = Engine.Xml.GetString(messages["Save_city_failed"]);
                    localization.min_radius = Engine.Xml.GetString(messages["Min_curve_radius"]);
                    localization.no_curves = Engine.Xml.GetString(messages["Curve_not_found"]);
                    localization.nnull = Engine.Xml.GetString(messages["The_pending_actionis_not_null"]);
                    localization.mapnotfound = Engine.Xml.GetString(messages["Map_not_found"]);
                    //----options, another...
                    if (optionsform != null)
                        foreach (XmlElement el in optionsform.ChildNodes) {
                            localization.controllist.Add(new TextListStruct() {
                                name = el.Name,
                                text = el.InnerText
                            });
                        }
                    if (editor_menu != null)
                        foreach (XmlElement el in editor_menu.ChildNodes) {
                            localization.menulist.Add(new TextListStruct() {
                                name = el.Name,
                                text = el.InnerText
                            });
                        }
                    if (tooltips != null)
                        foreach (XmlElement el in tooltips.ChildNodes) {
                            localization.tipslist.Add(new TextListStruct() {
                                name = el.Name,
                                text = el.InnerText
                            });
                        }
                    //----
                    localization.ctrl_a = Engine.Xml.GetString(general["Control_auto"]);
                    localization.ctrl_s = Engine.Xml.GetString(general["Control_semiauto"]);
                    localization.ctrl_m = Engine.Xml.GetString(general["Control_manual"]);
                    localization.ctrl_pos = Engine.Xml.GetString(general["Controller_position"]);
                    localization.reverse = Engine.Xml.GetString(general["Reverse"]);
                    localization.parking_brake = Engine.Xml.GetString(general["Parking_brake"]);
                    localization.speed = Engine.Xml.GetString(general["Speed"]);
                    localization.speed_km = Engine.Xml.GetString(general["Speed_kmh"]);
                    localization.route = Engine.Xml.GetString(general["Route"]);
                    localization.order = Engine.Xml.GetString(general["Order"]);
                    localization.route_in_park = Engine.Xml.GetString(general["To_park"]);
                    localization.nr = Engine.Xml.GetString(general["Next_road"]);
                    localization.nr_pryamo = Engine.Xml.GetString(general["Nr_forward"]);
                    localization.nr_right = Engine.Xml.GetString(general["Nr_right"]);
                    localization.nr_left = Engine.Xml.GetString(general["Nr_left"]);
                    localization.departure_time = Engine.Xml.GetString(general["Departure_time"]);
                    localization.arrival_time = Engine.Xml.GetString(general["Arrival_time"]);
                    localization.sterling = Engine.Xml.GetString(general["Steering"]);
                    localization.ster_l = Engine.Xml.GetString(general["Steering_left"]);
                    localization.ster_r = Engine.Xml.GetString(general["Steering_right"]);
                    localization.forward = Engine.Xml.GetString(general["Forward"]);
                    localization.back = Engine.Xml.GetString(general["Back"]);
                    localization.enable = Engine.Xml.GetString(general["Enabled"]);
                    localization.disable = Engine.Xml.GetString(general["Disabled"]);
                    localization.b1 = Engine.Xml.GetString(general["Brake1"]);
                    localization.b2 = Engine.Xml.GetString(general["Brake2"]);
                    localization.b3 = Engine.Xml.GetString(general["Brake3"]);
                    localization.b4 = Engine.Xml.GetString(general["Brake4"]);
                    localization.bp = Engine.Xml.GetString(general["BrakePnev"]);
                    localization.m = Engine.Xml.GetString(general["Shunting"]);
                    localization.x1 = Engine.Xml.GetString(general["Move1"]);
                    localization.x2 = Engine.Xml.GetString(general["Move2"]);
                    localization.x3 = Engine.Xml.GetString(general["Move3"]);
                    //----
                    localization.tram = Engine.Xml.GetString(tramway["Tramway"]);
                    localization.tram_control = Engine.Xml.GetString(tramway["Control"]);
                    localization.tk_on = Engine.Xml.GetString(tramway["Pantograph_raised"]);
                    localization.tk_off = Engine.Xml.GetString(tramway["Pantograph_omitted"]);
                    //----
                    localization.trol = Engine.Xml.GetString(trolleybus["Trolleybus"]);
                    localization.trol_control = Engine.Xml.GetString(trolleybus["Control"]);
                    localization.st_on = Engine.Xml.GetString(trolleybus["Shtangi_raised"]);
                    localization.st_off = Engine.Xml.GetString(trolleybus["Shtangi_omitted"]);
                    localization.air_brake = Engine.Xml.GetString(trolleybus["Air_brake"]);
                    localization.ax = Engine.Xml.GetString(trolleybus["Standalone_motion"]);
                    localization.ax_power = Engine.Xml.GetString(trolleybus["Battery_power"]);
                    //----
                    localization.engine = Engine.Xml.GetString(bus["Engine"]);
                    localization.bus_control = Engine.Xml.GetString(bus["Control"]);
                    localization.auto_control = Engine.Xml.GetString(bus["AutoControl"]);
                    localization.gmod = Engine.Xml.GetString(bus["Gearbox_mode"]);
                    localization.cur_pos = Engine.Xml.GetString(bus["Current_gear"]);
                    localization.pedal_pos = Engine.Xml.GetString(bus["Pedals_position"]);
                    localization.gas = Engine.Xml.GetString(bus["Gas"]);
                    localization.brake = Engine.Xml.GetString(bus["Brake"]);
                    localizations.Add(localization); 
                } catch (Exception exc) {
                    Logger.LogException(exc, "Localization");
                    Logger.Log("Localization", "Error in file " + file);
                    continue;
                }
            }
        }
		
        public static void ApplyLocalization(Control basecontrol)
        {
            List<Control> finded_ctrl = new List<Control>();
            foreach (var textl in current_.controllist) {
                finded_ctrl = Common.MyGUI.FindControl(basecontrol, textl.name);
                foreach (var control in finded_ctrl) {
                    control.Text = textl.text;
                }
            }
            if (((Form)basecontrol).Menu != null) {
                List<MenuItem> items;
                var menu = ((Form)basecontrol).Menu;
                foreach (var textl in current_.menulist) {
                    items = Common.MyGUI.FindMenuItems(menu, textl.name);
                    foreach (var item in items) {
                        item.Text = textl.text;
                    }
                }
            }
        }
		
        public static void ApplyLocalizationToolBar(ToolBar toolbar)
        {
            foreach (var textl in current_.tipslist) {
                foreach (ToolBarButton button in toolbar.Buttons) {
                    if (button.Name == textl.name)
                        button.ToolTipText = textl.text;
                }
            }
        }
    }
	
    [StructLayout(LayoutKind.Sequential)]
    public struct ПсевдоЛокализация
    {
        public string name;
        public string initializetreadtest;
        public string namegame;
        public string nametopeditor;
        public string сontinuous;
        public string exit;
        public string initializewopt;
        public string initializewdi;
        public string angle;
        public string scale;
        public string souen;
        public string soudis;
        public string souplay;
        public string nulltran;
        public string nullorder;
        public string nullrandom;
        public string tramofrali;
        public string tramofraliroute;
        public string tramofralirouteend;
        public string trannull;
        public string forplayer;
        public string tram;
        public string trol;
        public string bus;
        public string tram_control;
        public string trol_control;
        public string bus_control;
        public string auto_control;
        public string stop;
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
        public string departure_time;
//отправление
        public string arrival_time;
//прибытие
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
        public string b1;
        public string b2;
        public string b3;
        public string b4;
        public string bp;
        public string m;
        public string x1;
        public string x2;
        public string x3;
        //reserve:
        public string windowed;
        public string empty;
        public string random;
        public string shtangi_loosed;
        public string edit;
        public string of;
        public string mainform;
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
        public string city;
        public string allfiles;
        public string nnull;
        public string functionnowork;
        public string mapnotfound;
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
