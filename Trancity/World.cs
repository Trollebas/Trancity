using System;
using System.Collections.Generic;
using System.Xml;
using Common;
using System.Windows.Forms;

namespace Trancity
{
    public class World
    {
        public string filename;
        public MyList listДороги = new MyList(new[] { typeof(Road), typeof(Рельс) });
        private static readonly List<SlimDX.DirectSound.PrimarySoundBuffer> SoundBuffers = new List<SlimDX.DirectSound.PrimarySoundBuffer>();
        public double time;
        public Ground земля = new Ground();
        public Контактный_провод[] контактныеПровода = new Контактный_провод[0];
        public Трамвайный_контактный_провод[] контактныеПровода2 = new Трамвайный_контактный_провод[0];
        public Route[] маршруты = new Route[0];
        public List<Stop> остановки = new List<Stop>();
        public Парк[] парки = new[] { new Парк("Парк") };
        public static double прошлоВремени;
        public Светофорная_система[] светофорныеСистемы = new Светофорная_система[0];
        public Сигнальная_система[] сигнальныеСистемы = new Сигнальная_система[0];
        public double системноеВремя;
        public MyList транспорты = new MyList(new Type[0]);
        public List<Объект> объекты = new List<Объект>();
        public double time_speed = 1000.0;
//        public SimpleTimer simple_timer = new SimpleTimer(0.175);
        public SkyBox skybox = new SkyBox();
        public static double dtmax = 0.0;
        public static double fps = 0.0;
        private byte frametick = 0;
        private double fps_temp = 0.0;

        public void Create_Meshes()
        {
			MyGUI.status_string = Localization.current_.load_models;//"Загрузка моделей...";
            MyGUI.Splash();
            for (var i = 0; i < ВсеДороги.Length; i++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    var num2 = ((MyGUI.load_max / 20) * i) / ВсеДороги.Length;//MyDirect3D.
                    if (num2 > MyGUI.load_status)
                    {
                        MyGUI.load_status = num2;
                        MyGUI.Splash();
                    }
                }
                ВсеДороги[i].CreateMesh();
//                ВсеДороги[i].Test();
                if (ВсеДороги[i] is Рельс)
                {
                	if ((!MainForm.in_editor) && (((Рельс)ВсеДороги[i]).следующие_рельсы.Length <= 1)) continue;
                    ((Рельс)ВсеДороги[i]).добавочные_провода.CreateMesh();
                }
            }
            for (var j = 0; j < контактныеПровода.Length; j++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    var num4 = (((MyGUI.load_max / 20) * j) / контактныеПровода.Length) + (MyGUI.load_max / 20);
                    if (num4 > MyGUI.load_status)
                    {
                        MyGUI.load_status = num4;
                        MyGUI.Splash();
                    }
                }
                контактныеПровода[j].CreateMesh();
            }
            for (var j = 0; j < контактныеПровода2.Length; j++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    var num5 = (((MyGUI.load_max / 20) * j) / контактныеПровода2.Length) + (MyGUI.load_max / 20);
                    if (num5 > MyGUI.load_status)
                    {
                        MyGUI.load_status = num5;
                        MyGUI.Splash();
                    }
                }
                контактныеПровода2[j].CreateMesh();
            }
            for (var k = 0; k < транспорты.Count; k++)
            {
                var num6 = ((((MyGUI.load_max * 0x12) / 20) * k) / транспорты.Count) + (MyGUI.load_max / 20);
                if (num6 > MyGUI.load_status)
                {
                    MyGUI.load_status = num6;
                    MyGUI.Splash();
                }
                ((Transport)this.транспорты[k]).CreateMesh(this);
            }
            MyGUI.load_status = MyGUI.load_max;
            MyGUI.Splash();
            foreach (var система in сигнальныеСистемы)
            {
                система.CreateMesh();
            }
            foreach (var _система2 in this.светофорныеСистемы)
            {
                _система2.CreateMesh();
            }
            this.земля.CreateMesh();
            if ((SkyBox.draw) && (!MainForm.in_editor))
            {
            	MyGUI.status_string = Localization.current_.load_shaders;//"Загрузка шейдеров...";
            	MyGUI.Splash();
            	this.skybox.CreateMesh();
            }
            MyGUI.load_status = 0;
            MyGUI.status_string = Localization.current_.load_objects;//"Загрузка объектов...";
            MyGUI.Splash();
            int obj_count = объекты.Count > 1 ? объекты.Count - 1 : объекты.Count;
            for (var z = 0; z < объекты.Count; z++)
            {
            	if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    int num21 = (MyGUI.load_max * z) / (obj_count);
                    if (num21 > MyGUI.load_status)
                    {
                        MyGUI.load_status = num21;
                        MyGUI.Splash();
                    }
                }
                объекты[z].CreateMesh();
            }
            MyGUI.load_status = MyGUI.load_max;
            MyGUI.Splash();
            MyGUI.load_status = 0;
            MyGUI.status_string = Localization.current_.load_stops;//"Загрузка остановок...";
            MyGUI.Splash();
            int stops_count = this.остановки.Count > 1 ? this.остановки.Count - 1 : this.остановки.Count;
            for (int m = 0; m < this.остановки.Count; m++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    int num8 = (MyGUI.load_max * m) / (stops_count);
                    if (num8 > MyGUI.load_status)
                    {
                        MyGUI.load_status = num8;
                        MyGUI.Splash();
                    }
                }
                this.остановки[m].CreateMesh();
                this.остановки[m].ОбновитьКартинку();
                if (!MainForm.in_editor) this.остановки[m].ComputeMatrix();
            }
//            MeshObject.AfterCreating();
            MyGUI.load_status = MyGUI.load_max;
            MyGUI.Splash();
        }
        
        public void CreateSound()
        {
            /*foreach (Transport транспорт in this.транспорты)
            {
                транспорт.CreateSoundBuffers();
            }*/
            MyGUI.load_status = 0;
            MyGUI.status_string = Localization.current_.load_sounds;//"Загрузка звуков...";
            MyGUI.Splash();
            int trs_count = this.транспорты.Count > 1 ? this.транспорты.Count - 1 : this.транспорты.Count;
            for (int m = 0; m < this.транспорты.Count; m++)
            {
                if ((Environment.TickCount - MainForm.ticklast) > 50)
                {
                    int num8 = (MyGUI.load_max * m) / (trs_count);
                    if (num8 > MyGUI.load_status)
                    {
                        MyGUI.load_status = num8;
                        MyGUI.Splash();
                    }
                }
                ((Transport)this.транспорты[m]).CreateSoundBuffers();
            }
            MyGUI.load_status = MyGUI.load_max;
            MyGUI.Splash();
        }

        public void RenderMeshes()
        {
            foreach (var дорога in Рельсы)
            {
            	if (дорога.следующие_рельсы.Length <= 1) continue;
                дорога.добавочные_провода.Render();
            }
            foreach (var провод in контактныеПровода)
            {
                провод.Render();
            }
            foreach (var провод2 in контактныеПровода2)
            {
                провод2.Render();
            }
            foreach (Transport транспорт in транспорты)
            {
                транспорт.Render();
            }
            foreach (var система in сигнальныеСистемы)
            {
                система.Render();
            }
            foreach (var система2 in светофорныеСистемы)
            {
                система2.Render();
            }
            foreach (var остановка in остановки)
            {
            	остановка.Render();
            }
            foreach (var объект in объекты)
            {
            	объект.Render();
            }
            земля.Render();
//            skybox.Render();
        }
        
        public void RenderMeshesA()
        {
            foreach (var дорога in ВсеДороги)
            {
            	дорога.Render();
            	/*if ((!(дорога is Рельс)) || (((Рельс)дорога).следующие_рельсы.Length <= 1)) continue;
                ((Рельс)дорога).добавочные_провода.Render();*/
            }
            skybox.Render();
        }

        public void UpdateSound(Игрок[] игроки, bool игра_активна)
        {
            foreach (Transport транспорт in this.транспорты)
            {
                транспорт.UpdateSound(игроки, игра_активна);
            }
            using (IEnumerator<SlimDX.DirectSound.PrimarySoundBuffer> enumerator2 = SoundBuffers.GetEnumerator())//Microsoft.DirectX.DirectSound.Buffer
            {
                while (enumerator2.MoveNext())
                {
                    SlimDX.DirectSound.PrimarySoundBuffer current = (SlimDX.DirectSound.PrimarySoundBuffer)enumerator2.Current;
                }
            }
        }

        public void ДобавитьТранспорт(MainForm.НастройкиЗапуска настройки, Game игра)
        {
            if (игра == null)
            {
                игра = new Game();
            }
            var list = ВсеНаряды;
            for (var i = 0; i < list.Count; i++)
            {
                var index = list[i].рейсы.Length - 1;
                if ((index >= 0) && (list[i].рейсы[index].время_прибытия >= time)) continue;
                list.RemoveAt(i);
                i--;
            }
            var tr_by_parks = new List<Transport>[парки.Length];
            for (int i = 0; i < парки.Length; i++)
            {
            	tr_by_parks[i] = new List<Transport>();
            }
            игра.игроки = new Игрок[настройки.количествоИгроков];
            for (var j = 0; j < настройки.количествоИгроков; j++)
            {
                Transport транспорт;
                Road дорога;
                var игрока = настройки.игроки[j];
                игра.игроки[j] = new Игрок();
                игра.игроки[j].cameraPositionChange = Double3DPoint.Zero;
                игра.игроки[j].cameraRotationChange = DoublePoint.Zero;
                игра.игроки[j].inputGuid = игрока.inputGuid;
                игра.игроки[j].поворачиватьКамеру = настройки.поворачиватьКамеру;
//                var транспорта = TypeOfTransport.Tramway;
//                var троллейбуса = new МодельТранспорта();
                МодельТранспорта троллейбуса = null;
                int транспорта = -1;
                foreach (var трамвай2 in Модели.Трамваи)
                {
                	if (игрока.подвижнойСостав != трамвай2.name) continue;
                	транспорта = TypeOfTransport.Tramway;
                	троллейбуса = трамвай2;
                }
                foreach (var троллейбуса2 in Модели.Троллейбусы)
                {
                    if (игрока.подвижнойСостав != троллейбуса2.name) continue;
                    транспорта = TypeOfTransport.Trolleybus;
                    троллейбуса = троллейбуса2;
                }
                foreach (var троллейбуса3 in Модели.Автобусы)
                {
                    if (игрока.подвижнойСостав != троллейбуса3.name) continue;
                    транспорта = TypeOfTransport.Bus;
                    троллейбуса = троллейбуса3;
                }
                if ((троллейбуса == null) || (транспорта == -1))
                {
//                	MessageBox.Show("Couldn't find model " + игрока.подвижнойСостав + " for player " + игрока.имя);
					Logging.Write("World", "Couldn't find model " + игрока.подвижнойСостав + " for player " + игрока.имя);
                	continue;
                }
                var управление = настройки.автоматическоеУправление ? Управление.Автоматическое : Управление.Ручное;
                var маршрут = new Route(транспорта, "-");
                if ((игрока.маршрут > 0) && (маршруты.Length > 0))
                {
//                    маршрут = маршруты[(игрока.маршрут > 1) ? (игрока.маршрут - 2) : Cheats._random.Next(маршруты.Length)];
                    if ((игрока.маршрут > 1) && ((игрока.маршрут - 2) < маршруты.Length))
                    {
                    	маршрут = маршруты[(игрока.маршрут - 2)];
                    }
                    else
                    {
                    	try
                    	{
                    		List<Route> current_routes = new List<Route>();
	                    	foreach (var route in маршруты)
	                    	{
	                    		if (route.typeOfTransport != транспорта) continue;
	                    		current_routes.Add(route);
	                    	}
	                    	маршрут = current_routes[Cheats._random.Next(current_routes.Count)];
                    	}
                    	catch
                    	{
                    		Logging.Write("World", "Random route not selected for player " + игрока.имя);
                    	}
                    }
                }
                var парк = парки[Cheats._random.Next(парки.Length)];
                Order item = new Order(парк, маршрут, "-", игрока.подвижнойСостав);//null;
                if ((игрока.наряд > 1) && ((игрока.наряд - 2) < маршрут.orders.Length))
                {
                    item = маршрут.orders[игрока.наряд - 2];
                }
                else if (игрока.наряд == 1)
                {
                	try
                	{
                		item = маршрут.orders[Cheats._random.Next(маршрут.orders.Length)];
                	}
                	catch
                	{
                		Logging.Write("World", "Random order not selected for player " + игрока.имя);
                	}
                }
                /*else if (игрока.наряд == 0)
                {
                    item = new Order(парк, маршрут, "-", игрока.подвижнойСостав);
                }*/
                if (транспорта != TypeOfTransport.Tramway)// && (Дороги.Length > 0))
                {
                	try
                	{
                		дорога = Дороги[Cheats._random.Next(Дороги.Length)];
                	}
                	catch
                	{
                		дорога = new Road(0.0, 0.0, 20.0, 0.0, 0.0, true, 1.0, 1.0);
                		дорога.следующиеДороги = new[] { дорога };
                    	дорога.предыдущиеДороги = new[] { дорога };
                    	дорога.соседниеДороги = new[] { дорога };// Road[0];
                	}
                }
                else// if (Рельсы.Length > 0)
                {
                	try
                	{
                		дорога = Рельсы[Cheats._random.Next(Рельсы.Length)];
                	}
                	catch
                	{
                		дорога = new Рельс(0.0, 0.0, 20.0, 0.0, 0.0, true);
                    	дорога.следующиеДороги = new[] { дорога };
                    	дорога.предыдущиеДороги = new[] { дорога };
                    	дорога.соседниеДороги = new[] { дорога };// Road[0];
                	}
                }
                var num4 = Cheats._random.NextDouble() * дорога.Длина;
                Trip рейс = null;
                bool from_park = false;
                if (item != null)
                {
                    list.Remove(item);
                    парк = item.парк;
                    Найти_положение_наряда(item, ref рейс, ref дорога, ref num4, ref from_park);
                }
                else if (маршрут.trips.Count > 0)
                {
                    рейс = маршрут.trips[0];
                }
                switch (игрока.подвижнойСостав)
                {
                    default:
                        switch (транспорта)
                        {
                            case TypeOfTransport.Tramway:
                        		{
                        			if (!(дорога is Рельс))
                        			{
//                        				MessageBox.Show("Attempt to place your tramway on road!", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        				Logging.Write("World", "Attempt to place tramway on road for player " + игрока.имя);
                        				break;
                        			}
                        			транспорт = new Трамвай.ОбычныйТрамвай(троллейбуса, (Рельс)дорога, num4, управление, парк, маршрут, item);//, true);
                        			goto Label_0596;
                        		}
                        	case TypeOfTransport.Trolleybus:
                            case TypeOfTransport.Bus:
                                {
                                    var point = new Double3DPoint
                                                    {
                                                        xz_point = дорога.НайтиКоординаты(num4, 0.0),
                                                        y = дорога.НайтиВысоту(num4)
                                                    };
                                    var point2 = new DoublePoint(дорога.НайтиНаправление(num4), дорога.НайтиНаправлениеY(num4));
                                    транспорт = new Троллейбус.ОбычныйТроллейбус(троллейбуса, point, point2, управление, парк, маршрут, item);
                                    транспорт.SetPosition(дорога, num4, 0.0, Double3DPoint.Zero, DoublePoint.Zero, this);
                                    goto Label_0596;
                                }
                        }
                        игра.игроки[j].cameraPosition = new Double3DPoint(0.0, 2.0, 0.0);
                        игра.игроки[j].cameraRotation = new DoublePoint(0.0, -0.1);
                        continue;
                }
            Label_0596:
                игра.игроки[j].управляемыйОбъект = транспорт;
                игра.игроки[j].объектПривязки = транспорт;
                транспорт.наряд = item;
                транспорт.рейс = рейс;
                if (транспорт is Троллейбус)
                {
                    foreach (var штанга in ((Троллейбус)транспорт).штанги)
                    {
                        штанга.НайтиПровод(контактныеПровода);
                        if (штанга.Провод != null)
                        {
                        	штанга.поднимается = true;
                        	штанга.Обновить(false);//, 0.0f);
                            штанга.угол = штанга.уголNormal;
                        }
                        /*while ((штанга.Провод != null) && !штанга.Поднята)
                        {
                            штанга.поднимается = true;
                            штанга.Обновить(false, 0.0f);
                        }*/
                    }
                }
                else
                {
                	var pant = ((Трамвай)транспорт).токоприёмник;
                	((Трамвай)транспорт).Обновить(this, new Игрок[0]);//особенность текущей механики
                	pant.НайтиПровод(контактныеПровода2);
                	if (pant.Провод != null)
                	{
	                	pant.поднимается = true;
	                	pant.высота = pant.обычная_высота_max;
                	}
                }
//                var point4 = транспорт.position + new DoublePoint(транспорт.direction) * 9.5;//9.5
//                игра.игроки[j].cameraPosition = new Double3DPoint(point4.x, 2.5, point4.y);
				if (!транспорт.SetCamera(0, игра.игроки[j]))
				{
                	игра.игроки[j].cameraRotation = new DoublePoint(транспорт.direction, транспорт.НаправлениеY - 0.1);
                	игра.игроки[j].cameraPosition = Double3DPoint.Multiply(new Double3DPoint(8.0, 2.5, 0.0), транспорт.Координаты3D, игра.игроки[j].cameraRotation);
                }
                транспорты.Add(транспорт);
                if (from_park)
                {
                	for (int i = 0; i < парки.Length; i++)
		            {
                		if (парк != парки[i]) continue;
		            	tr_by_parks[i].Add(транспорт);
		            }
                }
            }
            for (var k = 0; k < list.Count; k++)
            {
                Road дорога2 = null;
                var distance = 0.0;
                Trip рейс2 = null;
                bool from_park = false;
                Найти_положение_наряда(list[k], ref рейс2, ref дорога2, ref distance, ref from_park);
                if ((рейс2 == null) || (дорога2 == null)) continue;
                Transport транспорт2 = null;
                switch (list[k].маршрут.typeOfTransport)
                {
                    case TypeOfTransport.Tramway:
                		{
                			if (Модели.Трамваи.Count == 0) continue;
                			МодельТранспорта трам4 = null;
                			if (list[k].transport == "" || list[k].transport == "Случайный")
                            {
                                трам4 = Модели.Трамваи[Cheats._random.Next(0, Модели.Трамваи.Count)];
                            }
                			else
                			{
                                трам4 = null;
                                foreach (var трамвай in Модели.Трамваи)
                                {
                                    if (трамвай.name == list[k].transport)
                                    {
                                        трам4 = трамвай;
                                        break;//Модели.Трамваи[i];
                                    }
                                }
                                if (трам4 == null)
                                {
                                    трам4 = Модели.Трамваи[Cheats._random.Next(0, Модели.Трамваи.Count)];
                                }
                			}
                			if (!(дорога2 is Рельс))
                			{
//                				MessageBox.Show("Attempt to place some tramway on road!", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
								Logging.Write("World", "Attempt to place tramway (route " + list[k].номер + ") on road");
                				break;
                			}
                			транспорт2 = new Трамвай.ОбычныйТрамвай(трам4, (Рельс)дорога2, distance, Управление.Автоматическое, list[k].парк, list[k].маршрут, list[k]);
                		}
                		break;
                    case TypeOfTransport.Trolleybus:
                        {
                			if (Модели.Троллейбусы.Count == 0) continue;
                            var point5 = new Double3DPoint
                                             {
                                                 xz_point = дорога2.НайтиКоординаты(distance, 0.0),
                                                 y = дорога2.НайтиВысоту(distance)
                                             };
                            var point6 = new DoublePoint(дорога2.НайтиНаправление(distance), дорога2.НайтиНаправлениеY(distance));
                            МодельТранспорта троллейбуса4 = null;
                            if (list[k].transport == "" || list[k].transport == "Случайный")
                            {
                                троллейбуса4 = Модели.Троллейбусы[Cheats._random.Next(0, Модели.Троллейбусы.Count)];
                            }
                            else
                            {
                                троллейбуса4 = null;
                                foreach (var троллейбус in Модели.Троллейбусы)
                                {
                                    if (троллейбус.name == list[k].transport)
                                    {
                                        троллейбуса4 = троллейбус;
                                        break;//Модели.Троллейбусы[i];
                                    }
                                }
                                if (троллейбуса4 == null)
                                {
                                    троллейбуса4 = Модели.Троллейбусы[Cheats._random.Next(0, Модели.Троллейбусы.Count)];
                                }
                            }
                            транспорт2 = new Троллейбус.ОбычныйТроллейбус(троллейбуса4, point5, point6, Управление.Автоматическое, list[k].парк, list[k].маршрут, list[k]);
                            транспорт2.SetPosition(дорога2, distance, 0.0, Double3DPoint.Zero, DoublePoint.Zero, this);
//                            ((Троллейбус.ОбычныйТроллейбус)транспорт2).ОбновитьПоложение(this);
                        }
                        break;
                    default:
                        {
                            if (list[k].маршрут.typeOfTransport != TypeOfTransport.Bus)
                            {
                                throw new Exception("Такого вида транспорта не существует!!!");
                            }
                            if (Модели.Автобусы.Count == 0) continue;
                            var point7 = new Double3DPoint
                                             {
                                                 xz_point = дорога2.НайтиКоординаты(distance, 0.0),
                                                 y = дорога2.НайтиВысоту(distance)
                                             };
                            var point8 = new DoublePoint(дорога2.НайтиНаправление(distance), дорога2.НайтиНаправлениеY(distance));
                            МодельТранспорта автобус = null;
                            if (list[k].transport == "" || list[k].transport == "Случайный")
                            {
                                автобус = Модели.Автобусы[Cheats._random.Next(0, Модели.Автобусы.Count)];
                            }
                            else
                            {
                                автобус = null;
                                foreach (var модельАвтобуса in Модели.Автобусы)
                                {
                                    if (модельАвтобуса.name == list[k].transport)
                                    {
                                        автобус = модельАвтобуса;
                                        break;//Модели.Автобусы[i];
                                    }
                                }
                                if (автобус == null)
                                {
                                    автобус = Модели.Автобусы[Cheats._random.Next(0, Модели.Автобусы.Count)];
                                }
                            }
                            транспорт2 = new Троллейбус.ОбычныйТроллейбус(автобус, point7, point8, Управление.Автоматическое, list[k].парк, list[k].маршрут, list[k]);
                            транспорт2.SetPosition(дорога2, distance, 0.0, Double3DPoint.Zero, DoublePoint.Zero, this);
//                            ((Троллейбус.ОбычныйТроллейбус)транспорт2).ОбновитьПоложение(this);
                        }
                        break;
                }
                if (транспорт2 == null) continue;
                транспорт2.наряд = list[k];
                транспорт2.рейс = рейс2;
                if (транспорт2 is Троллейбус)
                {
                    foreach (var штанга in ((Троллейбус)транспорт2).штанги)
                    {
                        штанга.НайтиПровод(контактныеПровода);
                        if (штанга.Провод != null)
                        {
                        	штанга.поднимается = true;
                        	штанга.Обновить(false);//, 0.0f);
                            штанга.угол = штанга.уголNormal;
                        }
                    }
                }
                else
                {
                	var pant = ((Трамвай)транспорт2).токоприёмник;
                	((Трамвай)транспорт2).Обновить(this, new Игрок[0]);
                	pant.НайтиПровод(контактныеПровода2);
                	if (pant.Провод != null)
                	{
	                	pant.поднимается = true;
	                	pant.высота = pant.обычная_высота_max;
                	}
                }
                транспорты.Add(транспорт2);
                if (from_park)
                {
                	for (int i = 0; i < парки.Length; i++)
		            {
                		if (транспорт2.парк != парки[i]) continue;
		            	tr_by_parks[i].Add(транспорт2);
		            }
                }
            }
//            игра.транспортArray = (Transport[]) транспорты.ToArray(typeof(Transport));
			//Спауним по паркам с сортировкой по времени выхода:
			for (int i = 0; i < парки.Length; i++)
			{
				if (парки[i].пути_стоянки.Length == 0) continue;
				int pathpos = 0;
				double dist = парки[i].пути_стоянки[0].Длина;
				while (tr_by_parks[i].Count > 0)
				{
					Transport transport = tr_by_parks[i][0];
					foreach (var trs in tr_by_parks[i])
					{
						if (transport.рейс.время_отправления > trs.рейс.время_отправления) transport = trs;
					}
					while (((dist - transport.length0) < -1.0) && (pathpos < парки[i].пути_стоянки.Length - 1))
					{
						pathpos++;
						dist = парки[i].пути_стоянки[pathpos].Длина;
					}
					transport.SetPosition(парки[i].пути_стоянки[pathpos], dist - transport.length0 + 1.0, 0.0, Double3DPoint.Zero, DoublePoint.Zero, this);
					dist -= transport.length0 + transport.length1;
					//TODO: пообъединять лишнее:
					for (int j = 0; j < игра.игроки.Length; j++)
					{
						if (игра.игроки[j].управляемыйОбъект != transport) continue;
						if (!transport.SetCamera(0, игра.игроки[j]))
						{
	                		игра.игроки[j].cameraRotation = new DoublePoint(transport.direction, transport.НаправлениеY - 0.1);
	                		игра.игроки[j].cameraPosition = Double3DPoint.Multiply(new Double3DPoint(8.0, 2.5, 0.0), transport.Координаты3D, игра.игроки[j].cameraRotation);
						}
					}
					if (transport is Троллейбус)
	                {
	                    foreach (var штанга in ((Троллейбус)transport).штанги)
	                    {
	                        штанга.НайтиПровод(контактныеПровода);
	                        if (штанга.Провод != null)
	                        {
	                        	штанга.поднимается = true;
	                        	штанга.Обновить(false);//, 0.0f);
	                            штанга.угол = штанга.уголNormal;
	                        }
	                    }
	                }
	                else
	                {
	                	var pant = ((Трамвай)transport).токоприёмник;
	                	((Трамвай)transport).Обновить(this, new Игрок[0]);
	                	pant.НайтиПровод(контактныеПровода2);
	                	if (pant.Провод != null)
	                	{
		                	pant.поднимается = true;
		                	pant.высота = pant.обычная_высота_max;
	                	}
	                }
					tr_by_parks[i].Remove(transport);
				}
			}
        }

        public void ЗагрузитьГород(string filename)
        {
        	if (string.IsNullOrEmpty(filename)) return;
            var document = new XmlDocument();
            Xml.document = document;
            document.Load(filename);
            var element = document["City"];
            listДороги.Clear();
            if (element == null) return;
            var element2 = element["Rails"];
            if (element2 != null)
            {
            	for (var i = 0; i < element2.ChildNodes.Count; i++)
            	{
            		XmlElement element3 = element2["rail" + i];
            		listДороги.Add(new Рельс(Xml.GetDouble(element3["x0"]), Xml.GetDouble(element3["y0"]), Xml.GetDouble(element3["x1"]), Xml.GetDouble(element3["y1"]), Xml.GetDouble(element3["angle0"]), Xml.GetDouble(element3["angle1"])));
            		if ((element3["height0"] != null) && (element3["height1"] != null))
            		{
            			Рельсы[i].высота[0] = Xml.GetDouble(element3["height0"]);
            			Рельсы[i].высота[1] = Xml.GetDouble(element3["height1"]);
            		}
            		this.Рельсы[i].расстояние_добавочных_проводов = Xml.GetDouble(element3["d_strel"]);
            		this.Рельсы[i].кривая = Xml.GetDouble(element3["iskriv"]) != 0.0;
            		this.Рельсы[i].name = Xml.GetString(element3["name"], "Rails");
            	}
            }
            XmlElement element4 = element["Roads"];
            if (element4 != null)
            {
                for (int j = 0; j < element4.ChildNodes.Count; j++)
                {
                	XmlElement element5 = element4["road" + j.ToString()];
                    this.listДороги.Add(new Road(Xml.GetDouble(element5["x0"]), Xml.GetDouble(element5["y0"]), Xml.GetDouble(element5["x1"]), Xml.GetDouble(element5["y1"]), Xml.GetDouble(element5["angle0"]), Xml.GetDouble(element5["angle1"]), Xml.GetDouble(element5["wide0"]), Xml.GetDouble(element5["wide1"])));
                    if ((element5["height0"] != null) && (element5["height1"] != null))
                    {
                        this.Дороги[j].высота[0] = Xml.GetDouble(element5["height0"]);
                        this.Дороги[j].высота[1] = Xml.GetDouble(element5["height1"]);
                    }
                    this.Дороги[j].кривая = Xml.GetDouble(element5["iskriv"]) != 0.0;
                    this.Дороги[j].name = Xml.GetString(element5["name"], "Road");
                }
            }
            foreach (Road дорога in this.ВсеДороги)
            {
                дорога.ОбновитьСледующиеДороги(this.ВсеДороги);
            }
            //TODO: проблема с расчётом MatricesCount рельсов
            foreach (Road дорога in this.ВсеДороги)
            {
//                дорога.ComputeMatrix();
                дорога.CreateBoundingSphere();
            }
            XmlElement element6 = element["Trolleybus_lines"];
            if (element6 != null)
            {
                this.контактныеПровода = new Контактный_провод[element6.ChildNodes.Count];
                for (int k = 0; k < element6.ChildNodes.Count; k++)
                {
                	XmlElement element7 = element6["line" + k.ToString()];
                    this.контактныеПровода[k] = new Контактный_провод(Xml.GetDouble(element7["x0"]), Xml.GetDouble(element7["y0"]), Xml.GetDouble(element7["x1"]), Xml.GetDouble(element7["y1"]), Xml.GetDouble(element7["right"]) != 0.0);
                    if ((element7["height0"] != null) && (element7["height1"] != null))
                    {
                        this.контактныеПровода[k].высота[0] = Xml.GetDouble(element7["height0"]);
                        this.контактныеПровода[k].высота[1] = Xml.GetDouble(element7["height1"]);
                    }
                    this.контактныеПровода[k].обесточенный = Xml.GetDouble(element7["no_contact"]) != 0.0;
                }
            }
            foreach (Контактный_провод _провод in this.контактныеПровода)
            {
                _провод.обновить_следующие_провода(this.контактныеПровода);
                _провод.ComputeMatrix();
            }
            XmlElement element6_1 = element["Tramway_lines"];
            if (element6_1 != null)
            {
                this.контактныеПровода2 = new Трамвайный_контактный_провод[element6_1.ChildNodes.Count];
                for (int k = 0; k < element6_1.ChildNodes.Count; k++)
                {
                	XmlElement element7 = element6_1["line" + k.ToString()];
                    this.контактныеПровода2[k] = new Трамвайный_контактный_провод(Xml.GetDouble(element7["x0"]), Xml.GetDouble(element7["y0"]), Xml.GetDouble(element7["x1"]), Xml.GetDouble(element7["y1"]));
                    if ((element7["height0"] != null) && (element7["height1"] != null))
                    {
                        this.контактныеПровода2[k].высота[0] = Xml.GetDouble(element7["height0"]);
                        this.контактныеПровода2[k].высота[1] = Xml.GetDouble(element7["height1"]);
                    }
                    this.контактныеПровода2[k].обесточенный = Xml.GetDouble(element7["no_contact"]) != 0.0;
                }
            }
            foreach (Трамвайный_контактный_провод _провод2 in this.контактныеПровода2)
            {
                _провод2.обновить_следующие_провода(this.контактныеПровода2);
                _провод2.ComputeMatrix();
            }
            XmlElement element8 = element["Parks"];
            if (element8 != null)
            {
                this.парки = new Парк[element8.ChildNodes.Count];
                for (int m = 0; m < element8.ChildNodes.Count; m++)
                {
                    XmlElement element9 = element8["park" + m.ToString()];
                    this.парки[m] = new Парк(element9["name"].InnerText);
                    int index = (int)Xml.GetDouble(element9["in"]);
                    if (index >= 0)
                    {
                        this.парки[m].въезд = this.ВсеДороги[index];
                    }
                    int num6 = (int)Xml.GetDouble(element9["out"]);
                    if (num6 >= 0)
                    {
                        this.парки[m].выезд = this.ВсеДороги[num6];
                    }
                    XmlElement element10 = element9["park_rails"];
                    this.парки[m].пути_стоянки = new Road[element10.ChildNodes.Count];
                    for (int n = 0; n < element10.ChildNodes.Count; n++)
	                {
	                    this.парки[m].пути_стоянки[n] = this.ВсеДороги[(int)Xml.GetDouble(element10["park_rail" + n.ToString()])];
	                }
                }
            }
            var element23 = element["Stops"];
            if (element23 != null)
            {
                //остановки = new Stop[element23.ChildNodes.Count];
                остановки = new List<Stop>();
                for (var num17 = 0; num17 < element23.ChildNodes.Count; num17++)
                {
                    var element24 = element23[string.Format("stop{0}", num17)];
                    var name = element24["name"].InnerText;
                    try
                    {
                    	this.остановки.Add(new Stop(Xml.GetString(element24["model"], "Stop (4 routes)"), new TypeOfTransport(TypeOfTransport.Tramway), this.ВсеДороги[(int)Xml.GetDouble(element24["rail"])], Xml.GetDouble(element24["distance"])));
                    }
                    catch
                    {
                    	throw new IndexOutOfRangeException("Can't load stop[" + num17 + "] " + name);
                    }
                    XmlElement type = element24["type"];
                    if (type != null)
                    {
                        if (type.InnerText == "0" || type.InnerText == "1" || type.InnerText == "2")
                        {
                            остановки[num17].typeOfTransport[(int)Xml.GetDouble(element24["type"])] = true;
                        }
                        else
                        {
                            XmlNodeList nodes = type.ChildNodes;
                            остановки[num17].typeOfTransport[TypeOfTransport.Tramway] = nodes[TypeOfTransport.Tramway].InnerText == "True" ? true : false;
                            остановки[num17].typeOfTransport[TypeOfTransport.Trolleybus] = nodes[TypeOfTransport.Trolleybus].InnerText == "True" ? true : false;
                            остановки[num17].typeOfTransport[TypeOfTransport.Bus] = nodes[TypeOfTransport.Bus].InnerText == "True" ? true : false;
                        }
                    }
                    this.остановки[num17].название = name;//element24["name"].InnerText;
                    XmlElement element25 = element24["stop_path"];
                    this.остановки[num17].частьПути = new Road[element25.ChildNodes.Count];
                    for (int num18 = 0; num18 < element25.ChildNodes.Count; num18++)
	                {
	                    this.остановки[num17].частьПути[num18] = this.ВсеДороги[(int)Xml.GetDouble(element25["stop_rail" + num18.ToString()])];
	                }
//                    this.остановки[num17].ОбновитьМаршруты(this.маршруты);
                    this.остановки[num17].UpdatePosition(this);
                }
            }
            var element11 = element["Routes"];
            if (element11 != null)
            {
                маршруты = new Route[element11.ChildNodes.Count];
                for (var num8 = 0; num8 < element11.ChildNodes.Count; num8++)
                {
                    XmlElement element13 = null;
                    var element12 = element11[string.Format("route{0}", num8)];
                    if (element12 != null)
                    {
                        маршруты[num8] = new Route(TypeOfTransport.Tramway, element12["name"].InnerText);
                        if (element12["type"] != null)
                        {
                            маршруты[num8].typeOfTransport = ((int)Xml.GetDouble(element12["type"]));
                        }
                        element13 = element12["route_runs"];
                    }
                    if (element13 != null)
                        for (var num9 = 0; num9 < element13.ChildNodes.Count; num9++)
                        {
                            var element14 = element13[string.Format("run{0}", num9)];
                            if (element14 == null) continue;
                            var item = new Trip
                                           {
                                               время_прибытия = Xml.GetDouble(element14["time"])
                                           };
                            var element15 = element14["run_rails"];
                            if (element15 != null)
                            {
                                item.pathes = new Road[element15.ChildNodes.Count];
                                for (var num10 = 0; num10 < element15.ChildNodes.Count; num10++)
                                {
                                    item.pathes[num10] = ВсеДороги[(int)Xml.GetDouble(element15["run_rail" + num10])];
                                }
                            }
                            var tripStopsElement = element14["Stops"];
                            if (tripStopsElement != null)
                            {
                                item.tripStopList = new List<TripStop>();
                                int parse = 0;
                                for (int i = 0; i<tripStopsElement.ChildNodes.Count; i++)
                                {
                                    XmlNode node = tripStopsElement.ChildNodes[i];
                                    parse = int.Parse(node.Name.Substring(4));
                                    if (parse < 0) continue;
                                    Stop stop = остановки[parse];
                                    item.tripStopList.Add(new TripStop(stop, (node.InnerText == "Да") || (node.InnerText == "1")));
                                }
                            }
                            else
                            {
                                item.InitTripStopList(маршруты[num8]);
                            }
                            маршруты[num8].trips.Add(item);
                        }
                    if (element12 != null)
                    {
                        var element16 = element12["park_runs"];
                        if (element16 != null)
                            for (var num11 = 0; num11 < element16.ChildNodes.Count; num11++)
                            {
                                var element17 = element16["run" + num11];
                                if (element17 == null) continue;
                                var рейс2 = new Trip
                                                {
                                                    inPark = Xml.GetDouble(element17["to_park"]) != 0.0,
                                                    inParkIndex = (int)Xml.GetDouble(element17["to_park_index"]),
                                                    время_прибытия = Xml.GetDouble(element17["time"])
                                                };
                                var element18 = element17["run_rails"];
                                if (element18 != null)
                                {
                                    рейс2.pathes = new Road[element18.ChildNodes.Count];
                                    for (var num12 = 0; num12 < element18.ChildNodes.Count; num12++)
                                    {
                                        рейс2.pathes[num12] = ВсеДороги[(int)Xml.GetDouble(element18["run_rail" + num12])];
                                    }
                                }
                                var tripStopsElement = element17["Stops"];
                                if (tripStopsElement != null)
                                {
                                    рейс2.tripStopList = new List<TripStop>();
                                    int parse = 0;
                                    for (int i = 0; i < tripStopsElement.ChildNodes.Count; i++)
                                    {
                                        XmlNode node = tripStopsElement.ChildNodes[i];
                                        parse = int.Parse(node.Name.Substring(4));
                                    	if (parse < 0) continue;
                                        Stop stop = остановки[parse];
                                        рейс2.tripStopList.Add(new TripStop(stop, (node.InnerText == "Да") || (node.InnerText == "1")));
                                    }
                                }
                                else
                                {
                                    рейс2.InitTripStopList(маршруты[num8]);
                                }
                                маршруты[num8].parkTrips.Add(рейс2);
                            }
                    }
                    if (element12 == null) continue;
                    var element19 = element12["Narads"];
                    if (element19 == null) continue;
                    маршруты[num8].orders = new Order[element19.ChildNodes.Count];
                    XmlElement element21 = null;
                    for (var num13 = 0; num13 < element19.ChildNodes.Count; num13++)
                    {
                        var element20 = element19[string.Format("narad{0}", num13)];
                        if (element20 != null)
                        {
                            маршруты[num8].orders[num13] = new Order(парки[(int)Xml.GetDouble(element20["park"])],
                                                                     маршруты[num8], element20["name"].InnerText,
                                                                     Xml.GetString(element20["transport"]))
                            {
                                поРабочим =
                                    Xml.GetDouble(element20["po_rabochim"]) != 0.0,
                                поВыходным =
                                    Xml.GetDouble(element20["po_vihodnim"]) != 0.0
                            };
                            element21 = element20["runs"];
                        }
                        if (element21 == null) continue;
                        маршруты[num8].orders[num13].рейсы = new Trip[element21.ChildNodes.Count];
                        for (var num14 = 0; num14 < element21.ChildNodes.Count; num14++)
                        {
                            var element22 = element21[string.Format("run{0}", num14)];
                            if (element22 == null) continue;
                            var num15 = (int)Xml.GetDouble(element22["index"]);
                            var num16 = Xml.GetDouble(element22["time"]);
                            маршруты[num8].orders[num13].рейсы[num14] = Xml.GetDouble(element22["park"]) == 0.0 ? маршруты[num8].trips[num15].Clone(num16) : маршруты[num8].parkTrips[num15].Clone(num16);
                        }
                    }
                }
                if (element23 != null)
                {
	                for (var num171 = 0; num171 < element23.ChildNodes.Count; num171++)
	                {
	                	this.остановки[num171].ОбновитьМаршруты(this.маршруты);
	                }
                }
            }

            XmlElement element26 = element["Signals"];
            if (element26 != null)
            {
                this.сигнальныеСистемы = new Сигнальная_система[element26.ChildNodes.Count];
                for (int num19 = 0; num19 < element26.ChildNodes.Count; num19++)
                {
                    XmlElement element27 = element26["signal" + num19.ToString()];
                    this.сигнальныеСистемы[num19] = new Сигнальная_система((int)Xml.GetDouble(element27["bound"]), (int)Xml.GetDouble(element27["status"]));
                    XmlElement element28 = element27["elements"];
                    for (int num20 = 0; num20 < element28.ChildNodes.Count; num20++)
                    {
                        XmlElement element29 = element28["element" + num20.ToString()];
                        Road дорога2 = this.ВсеДороги[(int)Xml.GetDouble(element29["rail"])];
                        double num21 = Xml.GetDouble(element29["distance"]);
                        string innerText = element29["type"].InnerText;
                        if (innerText != null)
                        {
                        	switch (innerText)
                        	{
                        		case "Контакт":
                        			new Сигнальная_система.Контакт(this.сигнальныеСистемы[num19], дорога2, num21, Xml.GetDouble(element29["minus"]) != 0.0);
                        			break;
                        		
                        		case "Сигнал":
                        			{
//                        				new Сигнальная_система.Сигнал(this.сигнальныеСистемы[num19], дорога2, num21, "Signal");
                        				var signal = new Visual_Signal(this.сигнальныеСистемы[num19], Xml.GetString(element29["model"], "Signal"));
                        				signal.положение = new Положение();
                        				signal.road = дорога2;
                        				signal.положение.расстояние = num21;
                        				signal.положение.отклонение = Xml.GetDouble(element29["place"], -1.4 - (дорога2.НайтиШирину(num21) / 2.0));
                        				signal.положение.высота = Xml.GetDouble(element29["height"]);
                        				signal.CreateBoundingSphere();
                        				break;
                        			}
                        	}
                        }
                    }
                }
            }
            else
            {
            	// TODO: новая загрузка сигналов
            	var sinals_element = element["Signal_systems"];
            	this.сигнальныеСистемы = new Сигнальная_система[sinals_element.ChildNodes.Count];
                for (int num19 = 0; num19 < sinals_element.ChildNodes.Count; num19++)
                {
                    XmlElement element27 = sinals_element["signal_system" + num19.ToString()];
                    this.сигнальныеСистемы[num19] = new Сигнальная_система((int)Xml.GetDouble(element27["bound"]), (int)Xml.GetDouble(element27["status"]));
                    var items_element = element27["signals"];
                    for (int i = 0; i < items_element.ChildNodes.Count; i++)
                    {
                    	var temp_signal = items_element["signal" + i.ToString()];
                    	string name = Xml.GetString(temp_signal["model"]);
                    	var road = this.ВсеДороги[(int)Xml.GetDouble(temp_signal["path"])];
                    	var dist = Xml.GetDouble(temp_signal["distance"]);
                    	var shift = Xml.GetDouble(temp_signal["place"]);
                    	var signal = new Visual_Signal(this.сигнальныеСистемы[num19], name);
                    	signal.положение = new Положение();
                    	signal.road = road;
                    	signal.положение.расстояние = dist;
                    	signal.положение.отклонение = shift;
                    	signal.положение.высота = Xml.GetDouble(temp_signal["height"]);
                    	signal.CreateBoundingSphere();
                    }
                    XmlElement element28 = element27["contacts"];
                    for (int num20 = 0; num20 < element28.ChildNodes.Count; num20++)
                    {
                        XmlElement element29 = element28["element" + num20.ToString()];
                        new Сигнальная_система.Контакт(this.сигнальныеСистемы[num19], this.ВсеДороги[(int)Xml.GetDouble(element29["rail"])], Xml.GetDouble(element29["distance"]), Xml.GetDouble(element29["minus"]) != 0.0);
                    }
                }
            }
            XmlElement element30 = element["Svetofor_systems"];
            if (element30 != null)
            {
                this.светофорныеСистемы = new Светофорная_система[element30.ChildNodes.Count];
                for (int num22 = 0; num22 < element30.ChildNodes.Count; num22++)
                {
                    XmlElement element31 = element30["svetofor_system" + num22.ToString()];
                    this.светофорныеСистемы[num22] = new Светофорная_система();
                    this.светофорныеСистемы[num22].начало_работы = Xml.GetDouble(element31["begin"]);
                    this.светофорныеСистемы[num22].окончание_работы = Xml.GetDouble(element31["end"]);
                    this.светофорныеСистемы[num22].цикл = Xml.GetDouble(element31["cycle"]);
                    this.светофорныеСистемы[num22].время_переключения_на_зелёный = Xml.GetDouble(element31["time_to_green"]);
                    this.светофорныеСистемы[num22].время_зелёного = Xml.GetDouble(element31["time_of_green"]);
                    XmlElement element32 = element31["svetofors"];
                    for (int num23 = 0; num23 < element32.ChildNodes.Count; num23++)
                    {
                        XmlElement element33 = element32["svetofor" + num23.ToString()];
                        bool flag = Xml.GetDouble(element33["arrow"]) != 0.0;
                        this.светофорныеСистемы[num22].светофоры.Add(new Светофор(Xml.GetString(element33["model"], flag ? "Tr. light (arrow)" : "Tr. light")));
                        this.светофорныеСистемы[num22].светофоры[num23].положение.Дорога = this.ВсеДороги[(int)Xml.GetDouble(element33["rail"])];
                        this.светофорныеСистемы[num22].светофоры[num23].положение.расстояние = Xml.GetDouble(element33["distance"]);
                        this.светофорныеСистемы[num22].светофоры[num23].положение.отклонение = Xml.GetDouble(element33["place"]);
                        this.светофорныеСистемы[num22].светофоры[num23].положение.высота = Xml.GetDouble(element33["height"]);
//                        this.светофорныеСистемы[num22].светофоры[num23].стрелка = flag;
                        this.светофорныеСистемы[num22].светофоры[num23].зелёная_стрелка = /*(Светофор.Стрелки)*/((int)Xml.GetDouble(element33["arrow_green"]));
                        this.светофорныеСистемы[num22].светофоры[num23].жёлтая_стрелка = /*(Светофор.Стрелки)*/((int)Xml.GetDouble(element33["arrow_yellow"]));
                        this.светофорныеСистемы[num22].светофоры[num23].красная_стрелка = /*(Светофор.Стрелки)*/((int)Xml.GetDouble(element33["arrow_red"]));
                        this.светофорныеСистемы[num22].светофоры[num23].CreateBoundingSphere();
                    }
                    XmlElement element34 = element31["svetofor_signals"];
                    for (int num24 = 0; num24 < element34.ChildNodes.Count; num24++)
                    {
                        XmlElement element35 = element34["svetofor_signal" + num24.ToString()];
                        Road дорога3 = this.ВсеДороги[(int)Xml.GetDouble(element35["rail"])];
                        double num25 = Xml.GetDouble(element35["distance"]);
                        this.светофорныеСистемы[num22].светофорные_сигналы.Add(new Светофорный_сигнал(дорога3, num25));
                    }
                }
            }
            XmlElement objects = element["Objects"];
            if (objects != null)
            {
                for (int i = 0; i < objects.ChildNodes.Count; i++)
                {
                    XmlElement obj = objects["object" + i.ToString()];
                    объекты.Add(new Объект(obj["filename"].InnerText, Xml.GetDouble(obj["x0"]), Xml.GetDouble(obj["y0"]), Xml.GetDouble(obj["angle0"]), Xml.GetDouble(obj["height0"])));
                }
            }
            this.filename = filename;
        }
        
        public void LoadCitySimple(string filename)
        {
        	if (filename == string.Empty) return;
            var document = new XmlDocument();
            Xml.document = document;
            document.Load(filename);
            var element = document["City"];
            listДороги.Clear();
            if (element == null) return;
            var element2 = element["Rails"];
            if (element2 != null)
            {
            	for (var i = 0; i < element2.ChildNodes.Count; i++)
            	{
            		listДороги.Add(new Рельс(0.0, 0.0, 0.0, 0.0, 0.0, 0.0));
            	}
            }
            XmlElement element4 = element["Roads"];
            if (element4 != null)
            {
                for (int j = 0; j < element4.ChildNodes.Count; j++)
                {
                    this.listДороги.Add(new Road(0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0));
                }
            }
            XmlElement element8 = element["Parks"];
            if (element8 != null)
            {
                this.парки = new Парк[element8.ChildNodes.Count];
                for (int m = 0; m < element8.ChildNodes.Count; m++)
                {
                    XmlElement element9 = element8["park" + m.ToString()];
                    this.парки[m] = new Парк(element9["name"].InnerText);
                    var index = (int)Xml.GetDouble(element9["out"]);
                    if (index >= 0) this.парки[m].выезд = this.ВсеДороги[index];
                }
            }
            var element11 = element["Routes"];
            if (element11 != null)
            {
                маршруты = new Route[element11.ChildNodes.Count];
                for (var num8 = 0; num8 < element11.ChildNodes.Count; num8++)
                {
                    XmlElement element13 = null;
                    var element12 = element11[string.Format("route{0}", num8)];
                    if (element12 != null)
                    {
                        маршруты[num8] = new Route(TypeOfTransport.Tramway, element12["name"].InnerText);
                        if (element12["type"] != null)
                        {
                            маршруты[num8].typeOfTransport = ((int)Xml.GetDouble(element12["type"]));
                        }
                        element13 = element12["route_runs"];
                    }
                    if (element13 != null)
                    {
                        for (var num9 = 0; num9 < element13.ChildNodes.Count; num9++)
                        {
                            var element14 = element13[string.Format("run{0}", num9)];
                            if (element14 == null) continue;
                            var item = new Trip
                                           {
                                               время_прибытия = Xml.GetDouble(element14["time"])
                                           };
                            var element15 = element14["run_rails"];
                            if (element15 != null)
                            {
                                item.pathes = new Road[Math.Min(element15.ChildNodes.Count, 1)];
                                for (var num10 = 0; num10 < item.pathes.Length; num10++)
                                {
                                    item.pathes[num10] = ВсеДороги[(int)Xml.GetDouble(element15["run_rail" + num10])];
                                }
                            }
                            маршруты[num8].trips.Add(item);
                        }
                	}
                    if (element12 != null)
                    {
                        var element16 = element12["park_runs"];
                        if (element16 != null)
                            for (var num11 = 0; num11 < element16.ChildNodes.Count; num11++)
                            {
                                var element17 = element16["run" + num11];
                                if (element17 == null) continue;
                                var рейс2 = new Trip
                                                {
                                                    inPark = Xml.GetDouble(element17["to_park"]) != 0.0,
                                                    inParkIndex = (int)Xml.GetDouble(element17["to_park_index"]),
                                                    время_прибытия = Xml.GetDouble(element17["time"])
                                                };
                                var element18 = element17["run_rails"];
                                if (element18 != null)
                                {
                                    рейс2.pathes = new Road[Math.Min(element18.ChildNodes.Count, 1)];
                                    for (var num12 = 0; num12 < рейс2.pathes.Length; num12++)
                                    {
                                        рейс2.pathes[num12] = ВсеДороги[(int)Xml.GetDouble(element18["run_rail" + num12])];
                                    }
                                }
                                маршруты[num8].parkTrips.Add(рейс2);
                            }
                    }
                    if (element12 == null) continue;
                    var element19 = element12["Narads"];
                    if (element19 == null) continue;
                    маршруты[num8].orders = new Order[element19.ChildNodes.Count];
                    XmlElement element21 = null;
                    for (var num13 = 0; num13 < element19.ChildNodes.Count; num13++)
                    {
                        var element20 = element19[string.Format("narad{0}", num13)];
                        if (element20 != null)
                        {
                            маршруты[num8].orders[num13] = new Order(парки[(int)Xml.GetDouble(element20["park"])],
                                                                     маршруты[num8], element20["name"].InnerText,
                                                                     Xml.GetString(element20["transport"]))
                            {
                                поРабочим =
                                    Xml.GetDouble(element20["po_rabochim"]) != 0.0,
                                поВыходным =
                                    Xml.GetDouble(element20["po_vihodnim"]) != 0.0
                            };
                            element21 = element20["runs"];
                        }
                        if (element21 == null) continue;
                        маршруты[num8].orders[num13].рейсы = new Trip[element21.ChildNodes.Count];
                        for (var num14 = 0; num14 < element21.ChildNodes.Count; num14++)
                        {
                            var element22 = element21[string.Format("run{0}", num14)];
                            if (element22 == null) continue;
                            var num15 = (int)Xml.GetDouble(element22["index"]);
                            var num16 = Xml.GetDouble(element22["time"]);
                            маршруты[num8].orders[num13].рейсы[num14] = Xml.GetDouble(element22["park"]) == 0.0 ? маршруты[num8].trips[num15].Clone(num16) : маршруты[num8].parkTrips[num15].Clone(num16);
                        }
                    }
                }
            }
            this.filename = filename;
        }

        public Положение Найти_ближайшее_положение(DoublePoint pos)
        {
            return this.Найти_ближайшее_положение(pos, this.ВсеДороги);
        }

        public Положение Найти_ближайшее_положение(DoublePoint pos, Road[] нужные_дороги)
        {
            List<Положение> list = new List<Положение>();
            List<double> list2 = new List<double>();
            foreach (Road дорога in нужные_дороги)
            {
                if (дорога.кривая)
                {
                    DoublePoint point = pos - дорога.структура.центр0;
                    DoublePoint point2 = дорога.концы[0] - дорога.структура.центр0;
                    double num = ((Math.Sign(дорога.структура.угол0) * (point.угол - point2.угол)) + 12.566370614359173) % 6.2831853071795862;
                    if (num < Math.Abs(дорога.структура.угол0))
                    {
                        list.Add(new Положение(дорога, (дорога.структура.длина0 * num) / Math.Abs(дорога.структура.угол0), -Math.Sign(дорога.структура.угол0) * (point.модуль - дорога.АбсолютныйРадиус)));
                        list2.Add(Math.Abs((double)(point.модуль - дорога.АбсолютныйРадиус)));
                    }
                    point = pos - дорога.структура.центр1;
                    point2 = дорога.структура.серединка - дорога.структура.центр1;
                    num = ((Math.Sign(дорога.структура.угол1) * (point.угол - point2.угол)) + 12.566370614359173) % 6.2831853071795862;
                    if (num < Math.Abs(дорога.структура.угол1))
                    {
                        list.Add(new Положение(дорога, дорога.структура.длина0 + ((дорога.структура.длина1 * num) / Math.Abs(дорога.структура.угол1)), -Math.Sign(дорога.структура.угол1) * (point.модуль - дорога.АбсолютныйРадиус)));
                        list2.Add(Math.Abs((double)(point.модуль - дорога.АбсолютныйРадиус)));
                    }
                }
                else
                {
                    DoublePoint point3 = pos - дорога.концы[0];
                    point3.угол -= дорога.направления[0];
                    DoublePoint point4 = дорога.концы[1] - дорога.концы[0];
                    point4.угол -= дорога.направления[0];
                    if ((point3.x >= 0.0) && (point3.x < point4.x))
                    {
                        point3.y -= (point4.y * point3.x) / point4.x;
                        point3.x *= дорога.Длина / point4.x;
                        list.Add(new Положение(дорога, point3.x, point3.y));
                        list2.Add(Math.Abs(point3.y));
                    }
                }
            }
            double num2 = 4.0;
            Положение положение = new Положение();
            for (int i = 0; i < list.Count; i++)
            {
                if (list2[i] < num2)
                {
                    num2 = list2[i];
                    положение = list[i];
                }
            }
            return положение;
        }

        public Положение[] Найти_все_положения(params Double3DPoint[] pos)
        {
            List<Положение> list = new List<Положение>();
            double num = 0.0;
            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = i + 1; j < pos.Length; j++)
                {
                    Double3DPoint point = pos[i] - pos[j];
                    num = Math.Max(num, point.модуль);
                }
            }
            foreach (Road дорога in this.ВсеДороги)
            {
                double num4 = дорога.Длина + Math.Max(дорога.ширина[0], дорога.ширина[1]);
                DoublePoint point2 = pos[0].xz_point - дорога.концы[0];
                if (point2.модуль <= (num4 + num))
                {
                    DoublePoint point3 = pos[0].xz_point - дорога.концы[1];
                    if (point3.модуль <= (num4 + num))
                    {
                        for (int k = 0; k < pos.Length; k++)
                        {
                            Положение item = this.Найти_положение(pos[k], дорога);
                            if (item.Дорога != null)
                            {
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            return list.ToArray();
        }

        public int Найти_индекс(Road дорога)
        {
            return this.listДороги.IndexOf(дорога);
        }

        public int Найти_индекс(Парк парк)
        {
            for (int i = 0; i < this.парки.Length; i++)
            {
                if (парк == this.парки[i])
                {
                    return i;
                }
            }
            return -1;
        }

        public int Найти_индекс(Trip рейс, Route маршрут, ref bool парковый)
        {
            for (int i = 0; i < маршрут.trips.Count; i++)
            {
                if (рейс.pathes == маршрут.trips[i].pathes)
                {
                    парковый = false;
                    return i;
                }
            }
            for (int j = 0; j < маршрут.parkTrips.Count; j++)
            {
                if (рейс.pathes == маршрут.parkTrips[j].pathes)
                {
                    парковый = true;
                    return j;
                }
            }
            return -1;
        }

        public int Найти_индекс_для_сохранения(Road дорога)
        {
            List<Road> list = new List<Road>(this.Рельсы);
            list.AddRange(this.Дороги);
            return list.IndexOf(дорога);
        }

        public Положение Найти_положение(Double3DPoint pos, Road дорога)
        {
            double num = дорога.Длина + (Math.Max(дорога.ширина[0], дорога.ширина[1]) / 2.0);
            DoublePoint point5 = pos.xz_point - дорога.концы[0];
            if (point5.модуль <= num)
            {
                DoublePoint point6 = pos.xz_point - дорога.концы[1];
                if (point6.модуль <= num)
                {
                    if (дорога.кривая)
                    {
                        DoublePoint point = pos.xz_point - дорога.структура.центр0;
                        DoublePoint point2 = дорога.концы[0] - дорога.структура.центр0;
                        double num2 = ((Math.Sign(дорога.структура.угол0) * (point.угол - point2.угол)) + 12.566370614359173) % 6.2831853071795862;
                        if (num2 < Math.Abs(дорога.структура.угол0))
                        {
                            double num3 = (дорога.структура.длина0 * num2) / Math.Abs(дорога.структура.угол0);
                            double num4 = pos.y - дорога.НайтиВысоту(num3);
                            if (((Math.Abs((double)(point.модуль - дорога.АбсолютныйРадиус)) < (дорога.НайтиШирину(num3) / 2.0)) && (num4 >= -1.0)) && (num4 < 5.0))
                            {
                                return new Положение(дорога, num3, -Math.Sign(дорога.структура.угол0) * (point.модуль - дорога.АбсолютныйРадиус), num4);
                            }
                        }
                        point = pos.xz_point - дорога.структура.центр1;
                        point2 = дорога.структура.серединка - дорога.структура.центр1;
                        num2 = ((Math.Sign(дорога.структура.угол1) * (point.угол - point2.угол)) + 12.566370614359173) % 6.2831853071795862;
                        if (num2 < Math.Abs(дорога.структура.угол1))
                        {
                            double num5 = дорога.структура.длина0 + ((дорога.структура.длина1 * num2) / Math.Abs(дорога.структура.угол1));
                            double num6 = pos.y - дорога.НайтиВысоту(num5);
                            if (((Math.Abs((double)(point.модуль - дорога.АбсолютныйРадиус)) < (дорога.НайтиШирину(num5) / 2.0)) && (num6 >= -1.0)) && (num6 < 5.0))
                            {
                                return new Положение(дорога, num5, -Math.Sign(дорога.структура.угол1) * (point.модуль - дорога.АбсолютныйРадиус), num6);
                            }
                        }
                    }
                    else
                    {
                        DoublePoint point3 = pos.xz_point - дорога.концы[0];
                        point3.угол -= дорога.направления[0];
                        DoublePoint point4 = дорога.концы[1] - дорога.концы[0];
                        point4.угол -= дорога.направления[0];
                        if ((point3.x >= 0.0) && (point3.x < point4.x))
                        {
                            point3.y -= (point4.y * point3.x) / point4.x;
                            point3.x *= дорога.Длина / point4.x;
                            double num7 = pos.y - дорога.НайтиВысоту(point3.x);
                            if (((Math.Abs(point3.y) < (дорога.НайтиШирину(point3.x) / 2.0)) && (num7 >= -1.0)) && (num7 < 5.0))
                            {
                                return new Положение(дорога, point3.x, point3.y, num7);
                            }
                        }
                    }
                    return new Положение();
                }
            }
            return new Положение();
        }

        public Положение Найти_положение(DoublePoint pos, Road дорога)
        {
            return this.Найти_положение(new Double3DPoint(pos.x, 0.0, pos.y), дорога);
        }

        public void Найти_положение_наряда(Order наряд, ref Trip рейс, ref Road дорога, ref double расстояние_по_дороге, ref bool from_park)
        {
            for (int i = 0; i < наряд.рейсы.Length; i++)
            {
                if (this.time < наряд.рейсы[i].время_прибытия)
                {
                    рейс = наряд.рейсы[i];
                    if (this.time < наряд.рейсы[i].время_отправления)
                    {
                        if (наряд.рейсы[i].дорога_отправления == наряд.парк.выезд)
                        {
                            /*int index = 0;
                            for (int k = 0; k < 200; k++)
                            {
                            	bool f1 = false;
	                            for (int j = 0; j < наряд.парк.пути_стоянки.Length; j++)
	                            {
	                                //index = random.Next(0, наряд.парк.пути_стоянки.Length);
	                                if (наряд.парк.пути_стоянки[j].objects.Count == k)
	                                {
	                                	index = j;
	                                	f1 = true;
	                                	break;
	                                }
	                            }
	                            if (f1) break;
                            }*/
                            from_park = true;
                            дорога = наряд.парк.выезд;//наряд.парк.пути_стоянки[index];
                            расстояние_по_дороге = Cheats._random.NextDouble() * Math.Min(Math.Abs((дорога.Длина - 20.0)), дорога.Длина);
                            return;
                        }
                        дорога = наряд.рейсы[i].дорога_отправления;
                        расстояние_по_дороге = Cheats._random.NextDouble() * (дорога.Длина * 0.4);//10.0;
                        return;
                    }
                    double num1 = рейс.длина_пути;
                    double num4 = (num1 * (this.time - наряд.рейсы[i].время_отправления)) / (наряд.рейсы[i].время_прибытия - наряд.рейсы[i].время_отправления);
                    foreach (Road дорога2 in рейс.pathes)
                    {
                        if (num4 < дорога2.Длина)
                        {
                            дорога = дорога2;
                            расстояние_по_дороге = num4;
                            if (((дорога2 is Рельс) && (((Рельс)дорога2).следующие_рельсы.Length > 1)) && (расстояние_по_дороге > (дорога2.Длина - ((Рельс)дорога2).расстояние_добавочных_проводов)))
                            {
                            	расстояние_по_дороге -= ((Рельс)дорога2).расстояние_добавочных_проводов + (Cheats._random.NextDouble() * 5.0);
                                return;
                            }
                            return;
                        }
                        num4 -= дорога2.Длина;
                    }
                    return;
                }
                if (i == (наряд.рейсы.Length - 1))
                {
                    рейс = наряд.рейсы[i];
                    дорога = наряд.рейсы[i].дорога_прибытия;
                    расстояние_по_дороге = Cheats._random.NextDouble() * (дорога.Длина * 0.4);//10.0;
                }
            }
        }

        public void Обновить(Игрок[] игроки)
        {
            this.Обновить_время();
            this.time += прошлоВремени;
            if (this.time >= 97200.0)
            {
                this.time -= 86400.0;
            }
            foreach (Светофорная_система _система in this.светофорныеСистемы)
            {
                _система.Обновить(this);
            }
            foreach (Transport транспорт in this.транспорты)
            {
                транспорт.Обновить(this, игроки);
            }
        }

        public void Обновить_время()
        {
        	var ftime = ((double)Environment.TickCount) / time_speed;
            if (this.системноеВремя == 0.0)
            {
                this.системноеВремя = ftime;//((double)Environment.TickCount) / time_speed;//0x3e8;
            }
            прошлоВремени = ftime - this.системноеВремя;//(((double)Environment.TickCount) / time_speed) - this.системноеВремя;
            dtmax = Math.Max(dtmax, прошлоВремени);
            if ((прошлоВремени > 0.0) && (frametick <= 5))
            {
            	fps_temp += 1 / прошлоВремени;
            }
            if (frametick == 5) 
            {
            	fps = fps_temp / 6.0;
            	fps_temp = 0.0;
            	frametick = 0;
            }
            else frametick++;
            if (!MainForm.in_editor) прошлоВремени = Math.Min(прошлоВремени, 0.25);
            this.системноеВремя = ftime;//((double)Environment.TickCount) / time_speed;//1000
//            MeshObject.timer.Refresh();
        }

        public void Сохранить_город(string filename)
        {
            XmlDocument parent = new XmlDocument();
            Xml.document = parent;
            XmlElement element = Xml.AddElement(parent, "City");
            XmlElement element2 = Xml.AddElement(element, "Rails");
            for (int i = 0; i < this.Рельсы.Length; i++)
            {
                XmlElement element3 = Xml.AddElement(element2, "rail" + i.ToString());
                Xml.AddElement(element3, "x0", this.Рельсы[i].концы[0].x);
                Xml.AddElement(element3, "y0", this.Рельсы[i].концы[0].y);
                Xml.AddElement(element3, "x1", this.Рельсы[i].концы[1].x);
                Xml.AddElement(element3, "y1", this.Рельсы[i].концы[1].y);
                Xml.AddElement(element3, "angle0", this.Рельсы[i].направления[0]);
                Xml.AddElement(element3, "angle1", this.Рельсы[i].направления[1]);
                Xml.AddElement(element3, "height0", this.Рельсы[i].высота[0]);
                Xml.AddElement(element3, "height1", this.Рельсы[i].высота[1]);
                Xml.AddElement(element3, "d_strel", this.Рельсы[i].расстояние_добавочных_проводов);
                Xml.AddElement(element3, "iskriv", this.Рельсы[i].кривая ? ((double)1) : ((double)0));
                Xml.AddElement(element3, "name", this.Рельсы[i].name);
            }
            XmlElement element4 = Xml.AddElement(element, "Roads");
            for (int j = 0; j < this.Дороги.Length; j++)
            {
                XmlElement element5 = Xml.AddElement(element4, "road" + j.ToString());
                Xml.AddElement(element5, "x0", this.Дороги[j].концы[0].x);
                Xml.AddElement(element5, "y0", this.Дороги[j].концы[0].y);
                Xml.AddElement(element5, "x1", this.Дороги[j].концы[1].x);
                Xml.AddElement(element5, "y1", this.Дороги[j].концы[1].y);
                Xml.AddElement(element5, "angle0", this.Дороги[j].направления[0]);
                Xml.AddElement(element5, "angle1", this.Дороги[j].направления[1]);
                Xml.AddElement(element5, "wide0", this.Дороги[j].ширина[0]);
                Xml.AddElement(element5, "wide1", this.Дороги[j].ширина[1]);
                Xml.AddElement(element5, "height0", this.Дороги[j].высота[0]);
                Xml.AddElement(element5, "height1", this.Дороги[j].высота[1]);
                Xml.AddElement(element5, "iskriv", this.Дороги[j].кривая ? ((double)1) : ((double)0));
                Xml.AddElement(element5, "name", this.Дороги[j].name);
            }
            XmlElement element6 = Xml.AddElement(element, "Trolleybus_lines");
            for (int k = 0; k < this.контактныеПровода.Length; k++)
            {
                XmlElement element7 = Xml.AddElement(element6, "line" + k.ToString());
                Xml.AddElement(element7, "x0", this.контактныеПровода[k].начало.x);
                Xml.AddElement(element7, "y0", this.контактныеПровода[k].начало.y);
                Xml.AddElement(element7, "x1", this.контактныеПровода[k].конец.x);
                Xml.AddElement(element7, "y1", this.контактныеПровода[k].конец.y);
                Xml.AddElement(element7, "height0", this.контактныеПровода[k].высота[0]);
                Xml.AddElement(element7, "height1", this.контактныеПровода[k].высота[1]);
                Xml.AddElement(element7, "right", this.контактныеПровода[k].правый ? ((double)1) : ((double)0));
                Xml.AddElement(element7, "no_contact", this.контактныеПровода[k].обесточенный ? ((double)1) : ((double)0));
            }
            XmlElement element6_1 = Xml.AddElement(element, "Tramway_lines");
            for (int k = 0; k < this.контактныеПровода2.Length; k++)
            {
                XmlElement element7 = Xml.AddElement(element6_1, "line" + k.ToString());
                Xml.AddElement(element7, "x0", this.контактныеПровода2[k].начало.x);
                Xml.AddElement(element7, "y0", this.контактныеПровода2[k].начало.y);
                Xml.AddElement(element7, "x1", this.контактныеПровода2[k].конец.x);
                Xml.AddElement(element7, "y1", this.контактныеПровода2[k].конец.y);
                Xml.AddElement(element7, "height0", this.контактныеПровода2[k].высота[0]);
                Xml.AddElement(element7, "height1", this.контактныеПровода2[k].высота[1]);
                Xml.AddElement(element7, "no_contact", this.контактныеПровода2[k].обесточенный ? ((double)1) : ((double)0));
            }
            XmlElement element8 = Xml.AddElement(element, "Parks");
            for (int m = 0; m < this.парки.Length; m++)
            {
                XmlElement element9 = Xml.AddElement(element8, "park" + m.ToString());
                Xml.AddElement(element9, "name", this.парки[m].название);
                Xml.AddElement(element9, "in", (double)this.Найти_индекс_для_сохранения(this.парки[m].въезд));
                Xml.AddElement(element9, "out", (double)this.Найти_индекс_для_сохранения(this.парки[m].выезд));
                XmlElement element10 = Xml.AddElement(element9, "park_rails");
                int index = 0;
                for (int num6 = 0; index < this.парки[m].пути_стоянки.Length; num6++)
                {
                    int num7 = this.Найти_индекс_для_сохранения(this.парки[m].пути_стоянки[index]);
                    if (num7 < 0)
                    {
                        num6--;
                    }
                    else
                    {
                        Xml.AddElement(element10, "park_rail" + num6.ToString(), (double)num7);
                    }
                    index++;
                }
            }
            XmlElement element11 = Xml.AddElement(element, "Routes");
            for (int n = 0; n < this.маршруты.Length; n++)
            {
                XmlElement element12 = Xml.AddElement(element11, "route" + n.ToString());
                Xml.AddElement(element12, "name", this.маршруты[n].number);
                Xml.AddElement(element12, "type", (double)this.маршруты[n].typeOfTransport);
                XmlElement element13 = Xml.AddElement(element12, "route_runs");
                for (int num9 = 0; num9 < this.маршруты[n].trips.Count; num9++)
                {
                    XmlElement element14 = Xml.AddElement(element13, "run" + num9.ToString());
                    Xml.AddElement(element14, "time", this.маршруты[n].trips[num9].время_прибытия);
                    XmlElement element15 = Xml.AddElement(element14, "run_rails");
                    for (int num10 = 0; num10 < this.маршруты[n].trips[num9].pathes.Length; num10++)
                    {
                        int num11 = this.Найти_индекс_для_сохранения(this.маршруты[n].trips[num9].pathes[num10]);
                        if (num11 < 0)
                        {
                            throw new IndexOutOfRangeException("Маршрут " + this.маршруты[n].number + " (рейс " + num9.ToString() + ") проходит по несуществующему пути!");
                        }
                        Xml.AddElement(element15, "run_rail" + num10.ToString(), (double)num11);
                    }
                    XmlElement stops = Xml.AddElement(element14, "Stops");
                    if (маршруты[n].trips[num9].tripStopList != null && маршруты[n].trips[num9].tripStopList.Count > 0)
                    {
                        for (int i = 0; i < маршруты[n].trips[num9].tripStopList.Count; i++)
                        {
                            TripStop tripStop = маршруты[n].trips[num9].tripStopList[i];
                            if (!tripStop.stop.typeOfTransport[маршруты[n].typeOfTransport]) continue;
                            int stops_ind = остановки.IndexOf(tripStop.stop);
                            if (stops_ind < 0)
                            {
                            	continue;//throw new IndexOutOfRangeException("Один из элементов списока остановок маршрута " + this.маршруты[n].number + " (рейса " + num9.ToString() + ") не существует!");
                            }
                            Xml.AddElement(stops, "Stop" + stops_ind, tripStop.flag ? ((double)1) : ((double)0));//"Да" : "Нет");
                        }
                    }
                }
                XmlElement element16 = Xml.AddElement(element12, "park_runs");
                for (int num12 = 0; num12 < this.маршруты[n].parkTrips.Count; num12++)
                {
                    XmlElement element17 = Xml.AddElement(element16, "run" + num12.ToString());
                    Xml.AddElement(element17, "to_park", this.маршруты[n].parkTrips[num12].inPark ? ((double)1) : ((double)0));
                    Xml.AddElement(element17, "to_park_index", (double)this.маршруты[n].parkTrips[num12].inParkIndex);
                    Xml.AddElement(element17, "time", this.маршруты[n].parkTrips[num12].время_прибытия);
                    XmlElement element18 = Xml.AddElement(element17, "run_rails");
                    for (int num13 = 0; num13 < this.маршруты[n].parkTrips[num12].pathes.Length; num13++)
                    {
                        int num14 = this.Найти_индекс_для_сохранения(this.маршруты[n].parkTrips[num12].pathes[num13]);
                        if (num14 < 0)
                        {
                            throw new IndexOutOfRangeException("Маршрут " + this.маршруты[n].number + " (парковый рейс " + num12.ToString() + ") проходит по несуществующему пути!");
                        }
                        Xml.AddElement(element18, "run_rail" + num13.ToString(), (double)num14);
                    }
                    XmlElement stops = Xml.AddElement(element17, "Stops");
                    if (маршруты[n].parkTrips[num12].tripStopList != null && маршруты[n].parkTrips[num12].tripStopList.Count > 0)
                    {
                        for (int i = 0; i < маршруты[n].parkTrips[num12].tripStopList.Count; i++)
                        {
                            TripStop tripStop = маршруты[n].parkTrips[num12].tripStopList[i];
                            if (!tripStop.stop.typeOfTransport[маршруты[n].typeOfTransport]) continue;
                            int stops_ind = остановки.IndexOf(tripStop.stop);
                            if (stops_ind < 0)
                            {
                            	continue;//throw new IndexOutOfRangeException("Один из элементов списока остановок маршрута " + this.маршруты[n].number + " (паркового рейса " + num12.ToString() + ") не существует!");
                            }
                            Xml.AddElement(stops, "Stop" + stops_ind, tripStop.flag ? ((double)1) : ((double)0));//"Да" : "Нет");
                        }
                    }
                }
                XmlElement element19 = Xml.AddElement(element12, "Narads");
                for (int num15 = 0; num15 < this.маршруты[n].orders.Length; num15++)
                {
                    XmlElement element20 = Xml.AddElement(element19, "narad" + num15.ToString());
                    Xml.AddElement(element20, "name", this.маршруты[n].orders[num15].номер);
                    int num16 = this.Найти_индекс(this.маршруты[n].orders[num15].парк);
                    if (num16 < 0)
                    {
                        throw new IndexOutOfRangeException("В наряде " + this.маршруты[n].number + "/" + this.маршруты[n].orders[num15].номер + " не указан парк!");
                    }
                    Xml.AddElement(element20, "park", (double)num16);
                    Xml.AddElement(element20, "transport", this.маршруты[n].orders[num15].transport);
                    Xml.AddElement(element20, "po_rabochim", this.маршруты[n].orders[num15].поРабочим ? ((double)1) : ((double)0));
                    Xml.AddElement(element20, "po_vihodnim", this.маршруты[n].orders[num15].поВыходным ? ((double)1) : ((double)0));
                    XmlElement element21 = Xml.AddElement(element20, "runs");
                    for (int num17 = 0; num17 < this.маршруты[n].orders[num15].рейсы.Length; num17++)
                    {
                        Trip рейс = this.маршруты[n].orders[num15].рейсы[num17];
                        XmlElement element22 = Xml.AddElement(element21, "run" + num17.ToString());
                        bool flag = false;
                        int num18 = this.Найти_индекс(рейс, this.маршруты[n], ref flag);
                        if (num18 < 0)
                        {
                            num18 = 0;
                        }
                        Xml.AddElement(element22, "park", flag ? ((double)1) : ((double)0));
                        Xml.AddElement(element22, "index", (double)num18);
                        Xml.AddElement(element22, "time", this.маршруты[n].orders[num15].рейсы[num17].время_отправления);
                    }
                }
            }
            XmlElement element23 = Xml.AddElement(element, "Stops");
            for (int num19 = 0; num19 < this.остановки.Count; num19++)
            {
                XmlElement element24 = Xml.AddElement(element23, "stop" + num19.ToString());
                Xml.AddElement(element24, "name", this.остановки[num19].название);
                Xml.AddElement(element24, "model", this.остановки[num19].name);
                XmlElement element240 = Xml.AddElement(element24, "type");
                Xml.AddElement(element240, "Tramway", остановки[num19].typeOfTransport[TypeOfTransport.Tramway].ToString());
                Xml.AddElement(element240, "Trolleybus", остановки[num19].typeOfTransport[TypeOfTransport.Trolleybus].ToString());
                Xml.AddElement(element240, "Bus", остановки[num19].typeOfTransport[TypeOfTransport.Bus].ToString());
                //Xml.AddElement(element24, "type", (double) this.остановки[num19].typeOfTransport);
                int num20 = this.Найти_индекс_для_сохранения(this.остановки[num19].road);
                if (num20 < 0)
                {
                    throw new IndexOutOfRangeException("Остановка \"" + this.остановки[num19].название + "\" находится на несуществующем пути!");
                }
                Xml.AddElement(element24, "rail", (double)num20);
                Xml.AddElement(element24, "distance", this.остановки[num19].distance);
                XmlElement element25 = Xml.AddElement(element24, "stop_path");
                for (int num21 = 0; num21 < this.остановки[num19].частьПути.Length; num21++)
                {
                    int num22 = this.Найти_индекс_для_сохранения(this.остановки[num19].частьПути[num21]);
                    if (num22 < 0)
                    {
                        break;
                    }
                    Xml.AddElement(element25, "stop_rail" + num21.ToString(), (double)num22);
                }
            }
            XmlElement element26 = Xml.AddElement(element, "Signals");
            for (int num23 = 0; num23 < this.сигнальныеСистемы.Length; num23++)
            {
                XmlElement element27 = Xml.AddElement(element26, "signal" + num23.ToString());
                Xml.AddElement(element27, "status", (double)this.сигнальныеСистемы[num23].состояние);
                Xml.AddElement(element27, "bound", (double)this.сигнальныеСистемы[num23].граница_переключения);
                XmlElement element28 = Xml.AddElement(element27, "elements");
                for (int i = 0; i < this.сигнальныеСистемы[num23].vsignals.Count; i++)
                {
                	XmlElement element29 = Xml.AddElement(element28, "element" + i.ToString());
                    Xml.AddElement(element29, "type", "Сигнал");
                    Xml.AddElement(element29, "model", this.сигнальныеСистемы[num23].vsignals[i].name);
                    int num25 = this.Найти_индекс_для_сохранения(this.сигнальныеСистемы[num23].vsignals[i].положение.Дорога);
                    if (num25 < 0)
                    {
                        throw new IndexOutOfRangeException("Сигнал №" + i.ToString() + " сигнальной системы №" + num23.ToString() + " находится на несуществующем пути!");
                    }
                    Xml.AddElement(element29, "rail", (double)num25);
                    Xml.AddElement(element29, "distance", this.сигнальныеСистемы[num23].vsignals[i].положение.расстояние);
                    Xml.AddElement(element29, "place", this.сигнальныеСистемы[num23].vsignals[i].положение.отклонение);
                    Xml.AddElement(element29, "height", this.сигнальныеСистемы[num23].vsignals[i].положение.высота);
                }
                for (int num24 = 0; num24 < this.сигнальныеСистемы[num23].элементы.Count; num24++)
                {
                	XmlElement element29 = Xml.AddElement(element28, "element" + (num24 + this.сигнальныеСистемы[num23].vsignals.Count).ToString());
                    Xml.AddElement(element29, "type", this.сигнальныеСистемы[num23].элементы[num24].GetType().Name);
                    int num25 = this.Найти_индекс_для_сохранения(this.сигнальныеСистемы[num23].элементы[num24].дорога);
                    if (num25 < 0)
                    {
                        throw new IndexOutOfRangeException("Элемент №" + num24.ToString() + " сигнальной системы №" + num23.ToString() + " находится на несуществующем пути!");
                    }
                    Xml.AddElement(element29, "rail", (double)num25);
                    Xml.AddElement(element29, "distance", this.сигнальныеСистемы[num23].элементы[num24].расстояние);
//                    if (this.сигнальныеСистемы[num23].элементы[num24] is Сигнальная_система.Контакт)
//                    {
//                        Сигнальная_система.Контакт контакт = (Сигнальная_система.Контакт)this.сигнальныеСистемы[num23].элементы[num24];
                        Xml.AddElement(element29, "minus", /*контакт*/this.сигнальныеСистемы[num23].элементы[num24].минус ? ((double)1) : ((double)0));
//                    }
                }
            }
            XmlElement element30 = Xml.AddElement(element, "Svetofor_systems");
            for (int num26 = 0; num26 < this.светофорныеСистемы.Length; num26++)
            {
                XmlElement element31 = Xml.AddElement(element30, "svetofor_system" + num26.ToString());
                Xml.AddElement(element31, "begin", this.светофорныеСистемы[num26].начало_работы);
                Xml.AddElement(element31, "end", this.светофорныеСистемы[num26].окончание_работы);
                Xml.AddElement(element31, "cycle", this.светофорныеСистемы[num26].цикл);
                Xml.AddElement(element31, "time_to_green", this.светофорныеСистемы[num26].время_переключения_на_зелёный);
                Xml.AddElement(element31, "time_of_green", this.светофорныеСистемы[num26].время_зелёного);
                XmlElement element32 = Xml.AddElement(element31, "svetofors");
                for (int num27 = 0; num27 < this.светофорныеСистемы[num26].светофоры.Count; num27++)
                {
                    XmlElement element33 = Xml.AddElement(element32, "svetofor" + num27.ToString());
                    int num28 = this.Найти_индекс_для_сохранения(this.светофорныеСистемы[num26].светофоры[num27].положение.Дорога);
                    if (num28 < 0)
                    {
                        throw new IndexOutOfRangeException("Светофор №" + num27.ToString() + " светофорной системы №" + num26.ToString() + " находится на несуществующем пути!");
                    }
                    Xml.AddElement(element33, "model", this.светофорныеСистемы[num26].светофоры[num27].name);
                    Xml.AddElement(element33, "rail", (double)num28);
                    Xml.AddElement(element33, "distance", this.светофорныеСистемы[num26].светофоры[num27].положение.расстояние);
                    Xml.AddElement(element33, "place", this.светофорныеСистемы[num26].светофоры[num27].положение.отклонение);
                    Xml.AddElement(element33, "height", this.светофорныеСистемы[num26].светофоры[num27].положение.высота);
//                    Xml.AddElement(element33, "arrow", this.светофорныеСистемы[num26].светофоры[num27].стрелка ? ((double)1) : ((double)0));
                    Xml.AddElement(element33, "arrow_green", (double)this.светофорныеСистемы[num26].светофоры[num27].зелёная_стрелка);
                    Xml.AddElement(element33, "arrow_yellow", (double)this.светофорныеСистемы[num26].светофоры[num27].жёлтая_стрелка);
                    Xml.AddElement(element33, "arrow_red", (double)this.светофорныеСистемы[num26].светофоры[num27].красная_стрелка);
                }
                XmlElement element34 = Xml.AddElement(element31, "svetofor_signals");
                for (int num29 = 0; num29 < this.светофорныеСистемы[num26].светофорные_сигналы.Count; num29++)
                {
                    XmlElement element35 = Xml.AddElement(element34, "svetofor_signal" + num29.ToString());
                    int num30 = this.Найти_индекс_для_сохранения(this.светофорныеСистемы[num26].светофорные_сигналы[num29].дорога);
                    if (num30 < 0)
                    {
                        throw new IndexOutOfRangeException("Сигнал №" + num29.ToString() + " светофорной системы №" + num26.ToString() + " находится на несуществующем пути!");
                    }
                    Xml.AddElement(element35, "rail", (double)num30);
                    Xml.AddElement(element35, "distance", this.светофорныеСистемы[num26].светофорные_сигналы[num29].расстояние);
                }
            }
            XmlElement objects = Xml.AddElement(element, "Objects");
            for (int i = 0; i < объекты.Count; i++)
            {
                XmlElement obj = Xml.AddElement(objects, "object" + i.ToString());
                Xml.AddElement(obj, "filename", this.объекты[i].filename);
                Xml.AddElement(obj, "x0", this.объекты[i].x0);
                Xml.AddElement(obj, "y0", this.объекты[i].y0);
                Xml.AddElement(obj, "angle0", this.объекты[i].angle0);
                Xml.AddElement(obj, "height0", this.объекты[i].height0);
            }
            parent.Save(filename);
        }
        
        public double GetHeight(DoublePoint pos)
        {
        	return земля.GetHeight(pos);
        }

        public Road[] ВсеДороги
        {
            get
            {
                return (Road[])this.listДороги.ToArray(typeof(Road));
            }
        }

        private List<Order> ВсеНаряды
        {
            get
            {
                var list = new List<Order>();
                foreach (Route маршрут in this.маршруты)
                {
                    list.AddRange(маршрут.orders);
                }
                return list;
            }
        }

        public Road[] Дороги
        {
            get
            {
                return listДороги.Get_array<Road>();
            }
        }

        public Рельс[] Рельсы
        {
            get
            {
                return listДороги.Get_array<Рельс>();
            }
        }
    }
}