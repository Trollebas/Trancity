namespace Trancity
{
    using Common;
    using Microsoft.DirectX.DirectSound;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Xml;

    public class Мир
    {
        public string filename;
        public MyList list_дороги = new MyList(new Type[] { typeof(Дорога), typeof(Рельс) });
        public static List<Microsoft.DirectX.DirectSound.Buffer> SoundBuffers = new List<Microsoft.DirectX.DirectSound.Buffer>();
        public double время = 0.0;
        public Земля земля = new Земля();
        public Контактный_провод[] контактные_провода = new Контактный_провод[0];
        public Маршрут[] маршруты = new Маршрут[0];
        public Остановка[] остановки = new Остановка[0];
        public Парк[] парки = new Парк[] { new Парк("Парк") };
        public static double прошло_времени;
        public Светофорная_система[] светофорные_системы = new Светофорная_система[0];
        public Сигнальная_система[] сигнальные_системы = new Сигнальная_система[0];
        public double системное_время;
        public MyList транспорты = new MyList(new Type[0]);
        public List<Объект> объекты = new List<Объект>();

        public void Create_Meshes()
        {
            for (int i = 0; i < this.все_дороги.Length; i++)
            {
                if ((Environment.TickCount - Main_Form.ticklast) > 50)
                {
                    int num2 = ((MyDirect3D.load_max / 20) * i) / this.все_дороги.Length;
                    if (num2 > MyDirect3D.load_status)
                    {
                        MyDirect3D.load_status = num2;
                        MyDirect3D.Splash();
                    }
                }
                this.все_дороги[i].CreateMesh();
                if (this.все_дороги[i] is Рельс)
                {
                    ((Рельс) this.все_дороги[i]).добавочные_провода.CreateMesh();
                }
            }
            for (int j = 0; j < this.контактные_провода.Length; j++)
            {
                if ((Environment.TickCount - Main_Form.ticklast) > 50)
                {
                    int num4 = (((MyDirect3D.load_max / 20) * j) / this.контактные_провода.Length) + (MyDirect3D.load_max / 20);
                    if (num4 > MyDirect3D.load_status)
                    {
                        MyDirect3D.load_status = num4;
                        MyDirect3D.Splash();
                    }
                }
                this.контактные_провода[j].CreateMesh();
            }
            for (int k = 0; k < this.транспорты.Count; k++)
            {
                int num6 = ((((MyDirect3D.load_max * 0x12) / 20) * k) / this.транспорты.Count) + (MyDirect3D.load_max / 20);
                if (num6 > MyDirect3D.load_status)
                {
                    MyDirect3D.load_status = num6;
                    MyDirect3D.Splash();
                }
                ((Транспорт) this.транспорты[k]).CreateMesh(this);
            }
            MyDirect3D.load_status = MyDirect3D.load_max;
            MyDirect3D.Splash();
            foreach (Сигнальная_система _система in this.сигнальные_системы)
            {
                _система.CreateMesh();
            }
            foreach (Светофорная_система _система2 in this.светофорные_системы)
            {
                _система2.CreateMesh();
            }
            foreach (Объект объект in объекты)
            {
                объект.CreateMesh();
            }
            this.земля.CreateMesh();
            MyDirect3D.load_status = 0;
            MyDirect3D.status_string = "Загрузка остановок...";
            MyDirect3D.Splash();
            for (int m = 0; m < this.остановки.Length; m++)
            {
                if ((Environment.TickCount - Main_Form.ticklast) > 50)
                {
                    int num1 = MyDirect3D.load_max;
                    int num8 = (((MyDirect3D.load_max * 20) / 20) * m) / (this.остановки.Length - 1);
                    if (num8 > MyDirect3D.load_status)
                    {
                        MyDirect3D.load_status = num8;
                        MyDirect3D.Splash();
                    }
                }
                this.остановки[m].CreateMesh();
                this.остановки[m].Обновить_картинку();
            }

            MyDirect3D.load_status = MyDirect3D.load_max;
            MyDirect3D.Splash();
        }

        public void CreateSound()
        {
            foreach (Транспорт транспорт in this.транспорты)
            {
                транспорт.CreateSoundBuffers();
            }
        }

        public void Render_Meshes()
        {
            foreach (Дорога дорога in this.все_дороги)
            {
                дорога.Render();
                if (дорога is Рельс)
                {
                    Рельс рельс = (Рельс)дорога;
                    if (рельс.следующие_рельсы.Length > 1)
                    {
                        Double_Point point = new Double_Point(рельс.добавочные_провода.координаты.x - MyDirect3D.Camera_Position.x, рельс.добавочные_провода.координаты.y - MyDirect3D.Camera_Position.z);
                        if (point.модуль < 200.0)
                        {
                            рельс.добавочные_провода.Render();
                        }
                    }
                }
            }
            foreach (Контактный_провод _провод in this.контактные_провода)
            {
                _провод.Render();
            }
            foreach (Транспорт транспорт in this.транспорты)
            {
                транспорт.Render();
            }
            foreach (Сигнальная_система _система in this.сигнальные_системы)
            {
                _система.Render();
            }
            foreach (Светофорная_система _система2 in this.светофорные_системы)
            {
                _система2.Render();
            }
            foreach (Остановка остановка in this.остановки)
            {
                остановка.Render();
            }
            foreach (Объект объект in объекты)
            {
                объект.Render();
            }
            this.земля.Render();
        }

        public void UpdateSound(Игрок[] игроки, bool игра_активна)
        {
            foreach (Транспорт транспорт in this.транспорты)
            {
                транспорт.UpdateSound(игроки, игра_активна);
            }
            using (IEnumerator<Microsoft.DirectX.DirectSound.Buffer> enumerator2 = SoundBuffers.GetEnumerator())
            {
                while (enumerator2.MoveNext())
                {
                    Microsoft.DirectX.DirectSound.Buffer current = (Microsoft.DirectX.DirectSound.Buffer) enumerator2.Current;
                }
            }
        }

        public void Добавить_трамваи(Main_Form.Настройки_Запуска настройки, Игра игра)
        {
            Random random = new Random();
            if (игра == null)
            {
                игра = new Игра();
            }
            List<Наряд> list = this.все_наряды;
            for (int i = 0; i < list.Count; i++)
            {
                int index = list[i].рейсы.Length - 1;
                if ((index < 0) || (list[i].рейсы[index].время_прибытия < this.время))
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
            игра.игроки = new Игрок[настройки.количество_игроков];
            for (int j = 0; j < настройки.количество_игроков; j++)
            {
                Транспорт транспорт;
                Дорога дорога;
                Main_Form.Настройки_Запуска_Игрока игрока = настройки.игроки[j];
                игра.игроки[j] = new Игрок();
                Вид_транспорта _транспорта = Вид_транспорта.Трамвай;
                Модель_троллейбуса _троллейбуса = new Модель_троллейбуса();
                foreach (Модель_троллейбуса _троллейбуса2 in Модели.троллейбусы)
                {
                    if (игрока.подвижной_состав == _троллейбуса2.name)
                    {
                        _транспорта = Вид_транспорта.Троллейбус;
                        _троллейбуса = _троллейбуса2;
                    }
                }
                foreach (Модель_троллейбуса _троллейбуса3 in Модели.автобусы)
                {
                    if (игрока.подвижной_состав == _троллейбуса3.name)
                    {
                        _транспорта = Вид_транспорта.Автобус;
                        _троллейбуса = _троллейбуса3;
                    }
                }
                Управление управление = настройки.автоматическое_управление ? Управление.Автоматическое : Управление.Ручное;
                Маршрут маршрут = new Маршрут(_транспорта, "-");
                if (игрока.маршрут > 0)
                {
                    маршрут = this.маршруты[(игрока.маршрут > 1) ? (игрока.маршрут - 2) : random.Next(this.маршруты.Length)];
                }
                Парк парк = this.парки[random.Next(this.парки.Length)];
                Наряд item = null;
                if (игрока.наряд >= 2)
                {
                    item = маршрут.наряды[игрока.наряд - 2];
                }
                else if (игрока.наряд == 1)
                {
                    item = маршрут.наряды[random.Next(маршрут.наряды.Length)];
                }
                if ((_транспорта != Вид_транспорта.Трамвай) && (this.дороги.Length > 0))
                {
                    дорога = this.дороги[random.Next(this.дороги.Length)];
                }
                else if (this.рельсы.Length > 0)
                {
                    дорога = this.рельсы[random.Next(this.рельсы.Length)];
                }
                else
                {
                    дорога = new Рельс(0.0, 0.0, 20.0, 0.0, 0.0, true);
                    дорога.следующие_дороги = new Дорога[] { дорога };
                    дорога.предыдущие_дороги = new Дорога[] { дорога };
                    дорога.соседние_дороги = new Дорога[0];
                }
                double num4 = random.NextDouble() * дорога.длина;
                Рейс рейс = null;
                if (item != null)
                {
                    list.Remove(item);
                    парк = item.парк;
                    this.Найти_положение_наряда(item, ref рейс, ref дорога, ref num4);
                }
                else if (маршрут.рейсы.Count > 0)
                {
                    рейс = маршрут.рейсы[0];
                }
                if (игрока.подвижной_состав == "ЛВС-89")
                {
                    транспорт = new Трамвай.ЛВС_86((Рельс) дорога, num4, управление, парк, маршрут, 3, 0);
                }
                else if (игрока.подвижной_состав == "ЛВС-86")
                {
                    транспорт = new Трамвай.ЛВС_86((Рельс) дорога, num4, управление, парк, маршрут, 2, 0);
                }
                else if (игрока.подвижной_состав == "2 ЛВС-86")
                {
                    транспорт = new Трамвай.ЛВС_86((Рельс) дорога, num4, управление, парк, маршрут, 2, 1);
                }
                else if (игрока.подвижной_состав == "ЛМ-68М")
                {
                    транспорт = new Трамвай.ЛМ_68М((Рельс) дорога, num4, управление, парк, маршрут, 0);
                }
                else if (игрока.подвижной_состав == "2 ЛМ-68М")
                {
                    транспорт = new Трамвай.ЛМ_68М((Рельс) дорога, num4, управление, парк, маршрут, 1);
                }
                else if (игрока.подвижной_состав == "ЛМ-68М + ЛП-83")
                {
                    транспорт = new Трамвай.ЛМ_68М((Рельс) дорога, num4, управление, парк, маршрут, 1);
                    ((Трамвай) транспорт).вагоны[0].токоприёмник.есть = false;
                    ((Трамвай) транспорт).вагоны[0].двери[1].дверь_водителя = false;
                }
                else
                {
                    switch (_транспорта)
                    {
                        case Вид_транспорта.Троллейбус:
                        case Вид_транспорта.Автобус:
                        {
                            Double_3D_Point point = new Double_3D_Point();
                            point.xz_point = дорога.найти_координаты(num4, 0.0);
                            point.y = дорога.найти_высоту(num4);
                            Double_Point point2 = new Double_Point(дорога.найти_направление(num4), дорога.найти_направление_y(num4));
                            транспорт = new Троллейбус.Обычный_троллейбус(_троллейбуса, point, point2, управление, парк, маршрут);
                            goto Label_0596;
                        }
                    }
                    игра.игроки[j].Camera_Position = new Double_3D_Point(0.0, 2.0, 0.0);
                    игра.игроки[j].Camera_Rotation = new Double_Point(0.0, -0.1);
                    игра.игроки[j].Camera_Position_Change = new Double_3D_Point(0.0, 0.0, 0.0);
                    игра.игроки[j].Camera_Rotation_Change = new Double_Point(0.0, 0.0);
                    игра.игроки[j].InputGuid = игрока.InputGuid;
                    игра.игроки[j].поворачивать_камеру = настройки.поворачивать_камеру;
                    continue;
                }
            Label_0596:
                игра.игроки[j].управляемый_объект = транспорт;
                игра.игроки[j].объект_привязки = транспорт;
                транспорт.наряд = item;
                транспорт.рейс = рейс;
                if (транспорт is Трамвай)
                {
                    Double_Point point3 = ((Трамвай) транспорт).координаты_токоприёмника + ((Double_Point) (new Double_Point(((Трамвай) транспорт).направление_токоприёмника) * 4.0));
                    игра.игроки[j].Camera_Position = new Double_3D_Point(point3.x, 2.5, point3.y);
                    игра.игроки[j].Camera_Rotation = new Double_Point(((Трамвай) транспорт).направление_токоприёмника, -0.1);
                }
                if (транспорт is Троллейбус)
                {
                    foreach (Троллейбус.Штанга штанга in ((Троллейбус) транспорт).штанги)
                    {
                        штанга.Найти_провод(this.контактные_провода);
                        while ((штанга.провод != null) && !штанга.поднята)
                        {
                            штанга.поднимается = true;
                            штанга.Обновить(false);
                        }
                    }
                    Double_Point point4 = ((Троллейбус) транспорт).координаты + ((Double_Point) (new Double_Point(((Троллейбус) транспорт).направление) * 9.5));
                    игра.игроки[j].Camera_Position = new Double_3D_Point(point4.x, 2.5, point4.y);
                    игра.игроки[j].Camera_Rotation = new Double_Point(((Троллейбус) транспорт).направление, -0.1);
                }
                игра.игроки[j].Camera_Position_Change = new Double_3D_Point(0.0, 0.0, 0.0);
                игра.игроки[j].Camera_Rotation_Change = new Double_Point(0.0, 0.0);
                игра.игроки[j].InputGuid = игрока.InputGuid;
                игра.игроки[j].поворачивать_камеру = настройки.поворачивать_камеру;
                this.транспорты.Add(транспорт);
            }
            for (int k = 0; k < list.Count; k++)
            {
                Дорога дорога2 = null;
                double num6 = 0.0;
                Рейс рейс2 = null;
                this.Найти_положение_наряда(list[k], ref рейс2, ref дорога2, ref num6);
                if ((рейс2 != null) && (дорога2 != null))
                {
                    Транспорт транспорт2;
                    if (list[k].маршрут.вид_транспорта == Вид_транспорта.Трамвай)
                    {
                        if (random.Next(2) == 1)
                        {
                            транспорт2 = new Трамвай.ЛВС_86((Рельс) дорога2, num6, Управление.Автоматическое, list[k].парк, list[k].маршрут, 2, random.Next(3) / 2);
                        }
                        else
                        {
                            транспорт2 = new Трамвай.ЛМ_68М((Рельс) дорога2, num6, Управление.Автоматическое, list[k].парк, list[k].маршрут, random.Next(2));
                        }
                    }
                    else if (list[k].маршрут.вид_транспорта == Вид_транспорта.Троллейбус)
                    {
                        Double_3D_Point point5 = new Double_3D_Point();
                        point5.xz_point = дорога2.найти_координаты(num6, 0.0);
                        point5.y = дорога2.найти_высоту(num6);
                        Double_Point point6 = new Double_Point(дорога2.найти_направление(num6), дорога2.найти_направление_y(num6));
                        Модель_троллейбуса _троллейбуса4 = Модели.троллейбусы[random.Next(Модели.троллейбусы.Length)];
                        транспорт2 = new Троллейбус.Обычный_троллейбус(_троллейбуса4, point5, point6, Управление.Автоматическое, list[k].парк, list[k].маршрут);
                    }
                    else
                    {
                        if (list[k].маршрут.вид_транспорта != Вид_транспорта.Автобус)
                        {
                            throw new Exception("Такого вида транспорта не существует!!!");
                        }
                        Double_3D_Point point7 = new Double_3D_Point();
                        point7.xz_point = дорога2.найти_координаты(num6, 0.0);
                        point7.y = дорога2.найти_высоту(num6);
                        Double_Point point8 = new Double_Point(дорога2.найти_направление(num6), дорога2.найти_направление_y(num6));
                        Модель_троллейбуса _троллейбуса5 = Модели.автобусы[random.Next(Модели.автобусы.Length)];
                        транспорт2 = new Троллейбус.Обычный_троллейбус(_троллейбуса5, point7, point8, Управление.Автоматическое, list[k].парк, list[k].маршрут);
                    }
                    транспорт2.наряд = list[k];
                    транспорт2.рейс = рейс2;
                    this.транспорты.Add(транспорт2);
                }
            }
        }

        public void Загрузить_город(string filename)
        {
            XmlDocument document = new XmlDocument();
            Xml.document = document;
            document.Load(filename);
            XmlElement element = document["City"];
            this.list_дороги.Clear();
            XmlElement element2 = element["Rails"];
            if (element2 != null)
            {
                for (int i = 0; i < element2.ChildNodes.Count; i++)
                {
                    XmlElement element3 = element2["rail" + i.ToString()];
                    this.list_дороги.Add(new Рельс(Xml.GetDouble(element3["x0"]), Xml.GetDouble(element3["y0"]), Xml.GetDouble(element3["x1"]), Xml.GetDouble(element3["y1"]), Xml.GetDouble(element3["angle0"]), Xml.GetDouble(element3["angle1"])));
                    if ((element3["height0"] != null) && (element3["height1"] != null))
                    {
                        this.рельсы[i].высота[0] = Xml.GetDouble(element3["height0"]);
                        this.рельсы[i].высота[1] = Xml.GetDouble(element3["height1"]);
                    }
                    this.рельсы[i].расстояние_добавочных_проводов = Xml.GetDouble(element3["d_strel"]);
                    this.рельсы[i].кривая = Xml.GetDouble(element3["iskriv"]) != 0.0;
                }
            }
            XmlElement element4 = element["Roads"];
            if (element4 != null)
            {
                for (int j = 0; j < element4.ChildNodes.Count; j++)
                {
                    XmlElement element5 = element4["road" + j.ToString()];
                    this.list_дороги.Add(new Дорога(Xml.GetDouble(element5["x0"]), Xml.GetDouble(element5["y0"]), Xml.GetDouble(element5["x1"]), Xml.GetDouble(element5["y1"]), Xml.GetDouble(element5["angle0"]), Xml.GetDouble(element5["angle1"]), Xml.GetDouble(element5["wide0"]), Xml.GetDouble(element5["wide1"])));
                    if ((element5["height0"] != null) && (element5["height1"] != null))
                    {
                        this.дороги[j].высота[0] = Xml.GetDouble(element5["height0"]);
                        this.дороги[j].высота[1] = Xml.GetDouble(element5["height1"]);
                    }
                    this.дороги[j].кривая = Xml.GetDouble(element5["iskriv"]) != 0.0;
                }
            }
            foreach (Дорога дорога in this.все_дороги)
            {
                дорога.обновить_следующие_дороги(this.все_дороги);
            }
            XmlElement element6 = element["Trolleybus_lines"];
            if (element6 != null)
            {
                this.контактные_провода = new Контактный_провод[element6.ChildNodes.Count];
                for (int k = 0; k < element6.ChildNodes.Count; k++)
                {
                    XmlElement element7 = element6["line" + k.ToString()];
                    this.контактные_провода[k] = new Контактный_провод(Xml.GetDouble(element7["x0"]), Xml.GetDouble(element7["y0"]), Xml.GetDouble(element7["x1"]), Xml.GetDouble(element7["y1"]), Xml.GetDouble(element7["right"]) != 0.0);
                    if ((element7["height0"] != null) && (element7["height1"] != null))
                    {
                        this.контактные_провода[k].высота[0] = Xml.GetDouble(element7["height0"]);
                        this.контактные_провода[k].высота[1] = Xml.GetDouble(element7["height1"]);
                    }
                    this.контактные_провода[k].обесточенный = Xml.GetDouble(element7["no_contact"]) != 0.0;
                }
            }
            foreach (Контактный_провод _провод in this.контактные_провода)
            {
                _провод.обновить_следующие_провода(this.контактные_провода);
            }
            XmlElement element8 = element["Parks"];
            if (element8 != null)
            {
                this.парки = new Парк[element8.ChildNodes.Count];
                for (int m = 0; m < element8.ChildNodes.Count; m++)
                {
                    XmlElement element9 = element8["park" + m.ToString()];
                    this.парки[m] = new Парк(element9["name"].InnerText);
                    int index = (int) Xml.GetDouble(element9["in"]);
                    if (index >= 0)
                    {
                        this.парки[m].въезд = this.все_дороги[index];
                    }
                    int num6 = (int) Xml.GetDouble(element9["out"]);
                    if (num6 >= 0)
                    {
                        this.парки[m].выезд = this.все_дороги[num6];
                    }
                    XmlElement element10 = element9["park_rails"];
                    this.парки[m].пути_стоянки = new Дорога[element10.ChildNodes.Count];
                    for (int n = 0; n < element10.ChildNodes.Count; n++)
                    {
                        this.парки[m].пути_стоянки[n] = this.все_дороги[(int) Xml.GetDouble(element10["park_rail" + n.ToString()])];
                    }
                }
            }
            XmlElement element11 = element["Routes"];
            if (element11 != null)
            {
                this.маршруты = new Маршрут[element11.ChildNodes.Count];
                for (int num8 = 0; num8 < element11.ChildNodes.Count; num8++)
                {
                    XmlElement element12 = element11["route" + num8.ToString()];
                    this.маршруты[num8] = new Маршрут(Вид_транспорта.Трамвай, element12["name"].InnerText);
                    if (element12["type"] != null)
                    {
                        this.маршруты[num8].вид_транспорта = (Вид_транспорта) ((int) Xml.GetDouble(element12["type"]));
                    }
                    XmlElement element13 = element12["route_runs"];
                    for (int num9 = 0; num9 < element13.ChildNodes.Count; num9++)
                    {
                        XmlElement element14 = element13["run" + num9.ToString()];
                        Рейс item = new Рейс();
                        item.время_прибытия = Xml.GetDouble(element14["time"]);
                        XmlElement element15 = element14["run_rails"];
                        item.путь = new Дорога[element15.ChildNodes.Count];
                        for (int num10 = 0; num10 < element15.ChildNodes.Count; num10++)
                        {
                            item.путь[num10] = this.все_дороги[(int) Xml.GetDouble(element15["run_rail" + num10.ToString()])];
                        }
                        this.маршруты[num8].рейсы.Add(item);
                    }
                    XmlElement element16 = element12["park_runs"];
                    for (int num11 = 0; num11 < element16.ChildNodes.Count; num11++)
                    {
                        XmlElement element17 = element16["run" + num11.ToString()];
                        Рейс рейс2 = new Рейс();
                        рейс2.в_парк = Xml.GetDouble(element17["to_park"]) != 0.0;
                        рейс2.в_парк_index = (int) Xml.GetDouble(element17["to_park_index"]);
                        рейс2.время_прибытия = Xml.GetDouble(element17["time"]);
                        XmlElement element18 = element17["run_rails"];
                        рейс2.путь = new Дорога[element18.ChildNodes.Count];
                        for (int num12 = 0; num12 < element18.ChildNodes.Count; num12++)
                        {
                            рейс2.путь[num12] = this.все_дороги[(int) Xml.GetDouble(element18["run_rail" + num12.ToString()])];
                        }
                        this.маршруты[num8].парковые_рейсы.Add(рейс2);
                    }
                    XmlElement element19 = element12["Narads"];
                    this.маршруты[num8].наряды = new Наряд[element19.ChildNodes.Count];
                    for (int num13 = 0; num13 < element19.ChildNodes.Count; num13++)
                    {
                        XmlElement element20 = element19["narad" + num13.ToString()];
                        this.маршруты[num8].наряды[num13] = new Наряд(this.парки[(int) Xml.GetDouble(element20["park"])], this.маршруты[num8], element20["name"].InnerText);
                        this.маршруты[num8].наряды[num13].по_рабочим = Xml.GetDouble(element20["po_rabochim"]) != 0.0;
                        this.маршруты[num8].наряды[num13].по_выходным = Xml.GetDouble(element20["po_vihodnim"]) != 0.0;
                        XmlElement element21 = element20["runs"];
                        this.маршруты[num8].наряды[num13].рейсы = new Рейс[element21.ChildNodes.Count];
                        for (int num14 = 0; num14 < element21.ChildNodes.Count; num14++)
                        {
                            XmlElement element22 = element21["run" + num14.ToString()];
                            int num15 = (int) Xml.GetDouble(element22["index"]);
                            double num16 = Xml.GetDouble(element22["time"]);
                            if (Xml.GetDouble(element22["park"]) == 0.0)
                            {
                                this.маршруты[num8].наряды[num13].рейсы[num14] = this.маршруты[num8].рейсы[num15].Clone(num16);
                            }
                            else
                            {
                                this.маршруты[num8].наряды[num13].рейсы[num14] = this.маршруты[num8].парковые_рейсы[num15].Clone(num16);
                            }
                        }
                    }
                }
            }
            XmlElement element23 = element["Stops"];
            if (element23 != null)
            {
                this.остановки = new Остановка[element23.ChildNodes.Count];
                for (int num17 = 0; num17 < element23.ChildNodes.Count; num17++)
                {
                    XmlElement element24 = element23["stop" + num17.ToString()];
                    this.остановки[num17] = new Остановка(Вид_транспорта.Трамвай, this.все_дороги[(int) Xml.GetDouble(element24["rail"])], Xml.GetDouble(element24["distance"]));
                    if (element24["type"] != null)
                    {
                        this.остановки[num17].вид_транспорта = (Вид_транспорта) ((int) Xml.GetDouble(element24["type"]));
                    }
                    this.остановки[num17].название = element24["name"].InnerText;
                    XmlElement element25 = element24["stop_path"];
                    this.остановки[num17].часть_пути = new Дорога[element25.ChildNodes.Count];
                    for (int num18 = 0; num18 < element25.ChildNodes.Count; num18++)
                    {
                        this.остановки[num17].часть_пути[num18] = this.все_дороги[(int) Xml.GetDouble(element25["stop_rail" + num18.ToString()])];
                    }
                    this.остановки[num17].Обновить_маршруты(this.маршруты);
                    this.остановки[num17].Обновить_положение(this);
                }
            }
            XmlElement element26 = element["Signals"];
            if (element26 != null)
            {
                this.сигнальные_системы = new Сигнальная_система[element26.ChildNodes.Count];
                for (int num19 = 0; num19 < element26.ChildNodes.Count; num19++)
                {
                    XmlElement element27 = element26["signal" + num19.ToString()];
                    this.сигнальные_системы[num19] = new Сигнальная_система((int) Xml.GetDouble(element27["bound"]), (int) Xml.GetDouble(element27["status"]));
                    XmlElement element28 = element27["elements"];
                    for (int num20 = 0; num20 < element28.ChildNodes.Count; num20++)
                    {
                        XmlElement element29 = element28["element" + num20.ToString()];
                        Дорога дорога2 = this.все_дороги[(int) Xml.GetDouble(element29["rail"])];
                        double num21 = Xml.GetDouble(element29["distance"]);
                        string innerText = element29["type"].InnerText;
                        if (innerText != null)
                        {
                            if (!(innerText == "Контакт"))
                            {
                                if (innerText == "Сигнал")
                                {
                                    goto Label_0DFB;
                                }
                            }
                            else
                            {
                                new Сигнальная_система.Контакт(this.сигнальные_системы[num19], дорога2, num21, Xml.GetDouble(element29["minus"]) != 0.0);
                            }
                        }
                        continue;
                    Label_0DFB:
                        new Сигнальная_система.Сигнал(this.сигнальные_системы[num19], дорога2, num21);
                    }
                }
            }
            XmlElement element30 = element["Svetofor_systems"];
            if (element30 != null)
            {
                this.светофорные_системы = new Светофорная_система[element30.ChildNodes.Count];
                for (int num22 = 0; num22 < element30.ChildNodes.Count; num22++)
                {
                    XmlElement element31 = element30["svetofor_system" + num22.ToString()];
                    this.светофорные_системы[num22] = new Светофорная_система();
                    this.светофорные_системы[num22].начало_работы = Xml.GetDouble(element31["begin"]);
                    this.светофорные_системы[num22].окончание_работы = Xml.GetDouble(element31["end"]);
                    this.светофорные_системы[num22].цикл = Xml.GetDouble(element31["cycle"]);
                    this.светофорные_системы[num22].время_переключения_на_зелёный = Xml.GetDouble(element31["time_to_green"]);
                    this.светофорные_системы[num22].время_зелёного = Xml.GetDouble(element31["time_of_green"]);
                    XmlElement element32 = element31["svetofors"];
                    for (int num23 = 0; num23 < element32.ChildNodes.Count; num23++)
                    {
                        XmlElement element33 = element32["svetofor" + num23.ToString()];
                        this.светофорные_системы[num22].светофоры.Add(new Светофор());
                        this.светофорные_системы[num22].светофоры[num23].положение.дорога = this.все_дороги[(int) Xml.GetDouble(element33["rail"])];
                        this.светофорные_системы[num22].светофоры[num23].положение.расстояние = Xml.GetDouble(element33["distance"]);
                        this.светофорные_системы[num22].светофоры[num23].положение.отклонение = Xml.GetDouble(element33["place"]);
                        this.светофорные_системы[num22].светофоры[num23].стрелка = Xml.GetDouble(element33["arrow"]) != 0.0;
                        this.светофорные_системы[num22].светофоры[num23].зелёная_стрелка = (Светофор.Стрелки) ((int) Xml.GetDouble(element33["arrow_green"]));
                        this.светофорные_системы[num22].светофоры[num23].жёлтая_стрелка = (Светофор.Стрелки) ((int) Xml.GetDouble(element33["arrow_yellow"]));
                        this.светофорные_системы[num22].светофоры[num23].красная_стрелка = (Светофор.Стрелки) ((int) Xml.GetDouble(element33["arrow_red"]));
                    }
                    XmlElement element34 = element31["svetofor_signals"];
                    for (int num24 = 0; num24 < element34.ChildNodes.Count; num24++)
                    {
                        XmlElement element35 = element34["svetofor_signal" + num24.ToString()];
                        Дорога дорога3 = this.все_дороги[(int) Xml.GetDouble(element35["rail"])];
                        double num25 = Xml.GetDouble(element35["distance"]);
                        this.светофорные_системы[num22].светофорные_сигналы.Add(new Светофорный_сигнал(дорога3, num25));
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
                    //if ((element7["height0"] != null) && (element7["height1"] != null))
                    //{
                    //    this.контактные_провода[k].высота[0] = Xml.GetDouble(element7["height0"]);
                    //    this.контактные_провода[k].высота[1] = Xml.GetDouble(element7["height1"]);
                    //}
                    //this.контактные_провода[k].обесточенный = Xml.GetDouble(element7["no_contact"]) != 0.0;
                }
            }
            this.filename = filename;
        }

        public Положение Найти_ближайшее_положение(Double_Point pos)
        {
            return this.Найти_ближайшее_положение(pos, this.все_дороги);
        }

        public Положение Найти_ближайшее_положение(Double_Point pos, Дорога[] нужные_дороги)
        {
            List<Положение> list = new List<Положение>();
            List<double> list2 = new List<double>();
            foreach (Дорога дорога in нужные_дороги)
            {
                if (дорога.кривая)
                {
                    Double_Point point = pos - дорога.структура.центр_0;
                    Double_Point point2 = дорога.концы[0] - дорога.структура.центр_0;
                    double num = ((Math.Sign(дорога.структура.угол_0) * (point.угол - point2.угол)) + 12.566370614359173) % 6.2831853071795862;
                    if (num < Math.Abs(дорога.структура.угол_0))
                    {
                        list.Add(new Положение(дорога, (дорога.структура.длина_0 * num) / Math.Abs(дорога.структура.угол_0), -Math.Sign(дорога.структура.угол_0) * (point.модуль - дорога.радиус_abs)));
                        list2.Add(Math.Abs((double) (point.модуль - дорога.радиус_abs)));
                    }
                    point = pos - дорога.структура.центр_1;
                    point2 = дорога.структура.серединка - дорога.структура.центр_1;
                    num = ((Math.Sign(дорога.структура.угол_1) * (point.угол - point2.угол)) + 12.566370614359173) % 6.2831853071795862;
                    if (num < Math.Abs(дорога.структура.угол_1))
                    {
                        list.Add(new Положение(дорога, дорога.структура.длина_0 + ((дорога.структура.длина_1 * num) / Math.Abs(дорога.структура.угол_1)), -Math.Sign(дорога.структура.угол_1) * (point.модуль - дорога.радиус_abs)));
                        list2.Add(Math.Abs((double) (point.модуль - дорога.радиус_abs)));
                    }
                }
                else
                {
                    Double_Point point3 = pos - дорога.концы[0];
                    point3.угол -= дорога.направления[0];
                    Double_Point point4 = дорога.концы[1] - дорога.концы[0];
                    point4.угол -= дорога.направления[0];
                    if ((point3.x >= 0.0) && (point3.x < point4.x))
                    {
                        point3.y -= (point4.y * point3.x) / point4.x;
                        point3.x *= дорога.длина / point4.x;
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

        public Положение[] Найти_все_положения(params Double_3D_Point[] pos)
        {
            List<Положение> list = new List<Положение>();
            double num = 0.0;
            for (int i = 0; i < pos.Length; i++)
            {
                for (int j = i + 1; j < pos.Length; j++)
                {
                    Double_3D_Point point = pos[i] - pos[j];
                    num = Math.Max(num, point.модуль);
                }
            }
            foreach (Дорога дорога in this.все_дороги)
            {
                double num4 = дорога.длина + Math.Max(дорога.ширина[0], дорога.ширина[1]);
                Double_Point point2 = pos[0].xz_point - дорога.концы[0];
                if (point2.модуль <= (num4 + num))
                {
                    Double_Point point3 = pos[0].xz_point - дорога.концы[1];
                    if (point3.модуль <= (num4 + num))
                    {
                        for (int k = 0; k < pos.Length; k++)
                        {
                            Положение item = this.Найти_положение(pos[k], дорога);
                            if (item.дорога != null)
                            {
                                list.Add(item);
                            }
                        }
                    }
                }
            }
            return list.ToArray();
        }

        public int Найти_индекс(Дорога дорога)
        {
            return this.list_дороги.IndexOf(дорога);
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

        public int Найти_индекс(Рейс рейс, Маршрут маршрут, ref bool парковый)
        {
            for (int i = 0; i < маршрут.рейсы.Count; i++)
            {
                if (рейс.путь == маршрут.рейсы[i].путь)
                {
                    парковый = false;
                    return i;
                }
            }
            for (int j = 0; j < маршрут.парковые_рейсы.Count; j++)
            {
                if (рейс.путь == маршрут.парковые_рейсы[j].путь)
                {
                    парковый = true;
                    return j;
                }
            }
            return -1;
        }

        public int Найти_индекс_для_сохранения(Дорога дорога)
        {
            List<Дорога> list = new List<Дорога>(this.рельсы);
            list.AddRange(this.дороги);
            return list.IndexOf(дорога);
        }

        public Положение Найти_положение(Double_3D_Point pos, Дорога дорога)
        {
            double num = дорога.длина + (Math.Max(дорога.ширина[0], дорога.ширина[1]) / 2.0);
            Double_Point point5 = pos.xz_point - дорога.концы[0];
            if (point5.модуль <= num)
            {
                Double_Point point6 = pos.xz_point - дорога.концы[1];
                if (point6.модуль <= num)
                {
                    if (дорога.кривая)
                    {
                        Double_Point point = pos.xz_point - дорога.структура.центр_0;
                        Double_Point point2 = дорога.концы[0] - дорога.структура.центр_0;
                        double num2 = ((Math.Sign(дорога.структура.угол_0) * (point.угол - point2.угол)) + 12.566370614359173) % 6.2831853071795862;
                        if (num2 < Math.Abs(дорога.структура.угол_0))
                        {
                            double num3 = (дорога.структура.длина_0 * num2) / Math.Abs(дорога.структура.угол_0);
                            double num4 = pos.y - дорога.найти_высоту(num3);
                            if (((Math.Abs((double) (point.модуль - дорога.радиус_abs)) < (дорога.найти_ширину(num3) / 2.0)) && (num4 >= -1.0)) && (num4 < 5.0))
                            {
                                return new Положение(дорога, num3, -Math.Sign(дорога.структура.угол_0) * (point.модуль - дорога.радиус_abs), num4);
                            }
                        }
                        point = pos.xz_point - дорога.структура.центр_1;
                        point2 = дорога.структура.серединка - дорога.структура.центр_1;
                        num2 = ((Math.Sign(дорога.структура.угол_1) * (point.угол - point2.угол)) + 12.566370614359173) % 6.2831853071795862;
                        if (num2 < Math.Abs(дорога.структура.угол_1))
                        {
                            double num5 = дорога.структура.длина_0 + ((дорога.структура.длина_1 * num2) / Math.Abs(дорога.структура.угол_1));
                            double num6 = pos.y - дорога.найти_высоту(num5);
                            if (((Math.Abs((double) (point.модуль - дорога.радиус_abs)) < (дорога.найти_ширину(num5) / 2.0)) && (num6 >= -1.0)) && (num6 < 5.0))
                            {
                                return new Положение(дорога, num5, -Math.Sign(дорога.структура.угол_1) * (point.модуль - дорога.радиус_abs), num6);
                            }
                        }
                    }
                    else
                    {
                        Double_Point point3 = pos.xz_point - дорога.концы[0];
                        point3.угол -= дорога.направления[0];
                        Double_Point point4 = дорога.концы[1] - дорога.концы[0];
                        point4.угол -= дорога.направления[0];
                        if ((point3.x >= 0.0) && (point3.x < point4.x))
                        {
                            point3.y -= (point4.y * point3.x) / point4.x;
                            point3.x *= дорога.длина / point4.x;
                            double num7 = pos.y - дорога.найти_высоту(point3.x);
                            if (((Math.Abs(point3.y) < (дорога.найти_ширину(point3.x) / 2.0)) && (num7 >= -1.0)) && (num7 < 5.0))
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

        public Положение Найти_положение(Double_Point pos, Дорога дорога)
        {
            return this.Найти_положение(new Double_3D_Point(pos.x, 0.0, pos.y), дорога);
        }

        public void Найти_положение_наряда(Наряд наряд, ref Рейс рейс, ref Дорога дорога, ref double расстояние_по_дороге)
        {
            Random random = new Random();
            for (int i = 0; i < наряд.рейсы.Length; i++)
            {
                if (this.время < наряд.рейсы[i].время_прибытия)
                {
                    рейс = наряд.рейсы[i];
                    if (this.время < наряд.рейсы[i].время_отправления)
                    {
                        if (наряд.рейсы[i].дорога_отправления == наряд.парк.выезд)
                        {
                            int index = 0;
                            for (int j = 0; j < 200; j++)
                            {
                                index = random.Next(наряд.парк.пути_стоянки.Length);
                                if (наряд.парк.пути_стоянки[index].объекты.Count == 0)
                                {
                                    break;
                                }
                            }
                            дорога = наряд.парк.пути_стоянки[index];
                            расстояние_по_дороге = random.NextDouble() * (дорога.длина - 20.0);
                            return;
                        }
                        дорога = наряд.рейсы[i].дорога_отправления;
                        расстояние_по_дороге = random.NextDouble() * 10.0;
                        return;
                    }
                    double num1 = рейс.длина_пути;
                    double num4 = (рейс.длина_пути * (this.время - наряд.рейсы[i].время_отправления)) / (наряд.рейсы[i].время_прибытия - наряд.рейсы[i].время_отправления);
                    foreach (Дорога дорога2 in рейс.путь)
                    {
                        if (num4 < дорога2.длина)
                        {
                            дорога = дорога2;
                            расстояние_по_дороге = num4;
                            if (((дорога2 is Рельс) && (((Рельс) дорога2).следующие_рельсы.Length > 1)) && (расстояние_по_дороге > (дорога2.длина - ((Рельс) дорога2).расстояние_добавочных_проводов)))
                            {
                                расстояние_по_дороге -= ((Рельс) дорога2).расстояние_добавочных_проводов;
                                return;
                            }
                            return;
                        }
                        num4 -= дорога2.длина;
                    }
                    return;
                }
                if (i == (наряд.рейсы.Length - 1))
                {
                    рейс = наряд.рейсы[i];
                    дорога = наряд.рейсы[i].дорога_прибытия;
                    расстояние_по_дороге = random.NextDouble() * 10.0;
                }
            }
        }

        public void Обновить(Игрок[] игроки)
        {
            this.Обновить_время();
            this.время += прошло_времени;
            if (this.время >= 97200.0)
            {
                this.время -= 86400.0;
            }
            foreach (Светофорная_система _система in this.светофорные_системы)
            {
                _система.Обновить(this);
            }
            foreach (Транспорт транспорт in this.транспорты)
            {
                транспорт.Обновить(this, игроки);
            }
        }

        public void Обновить_время()
        {
            if (this.системное_время == 0.0)
            {
                this.системное_время = Environment.TickCount / 0x3e8;
            }
            прошло_времени = (((double) Environment.TickCount) / 1000.0) - this.системное_время;
            this.системное_время = ((double) Environment.TickCount) / 1000.0;
        }

        public void Сохранить_город(string filename)
        {
            XmlDocument parent = new XmlDocument();
            Xml.document = parent;
            XmlElement element = Xml.AddElement(parent, "City");
            XmlElement element2 = Xml.AddElement(element, "Rails");
            for (int i = 0; i < this.рельсы.Length; i++)
            {
                XmlElement element3 = Xml.AddElement(element2, "rail" + i.ToString());
                Xml.AddElement(element3, "x0", this.рельсы[i].концы[0].x);
                Xml.AddElement(element3, "y0", this.рельсы[i].концы[0].y);
                Xml.AddElement(element3, "x1", this.рельсы[i].концы[1].x);
                Xml.AddElement(element3, "y1", this.рельсы[i].концы[1].y);
                Xml.AddElement(element3, "angle0", this.рельсы[i].направления[0]);
                Xml.AddElement(element3, "angle1", this.рельсы[i].направления[1]);
                Xml.AddElement(element3, "height0", this.рельсы[i].высота[0]);
                Xml.AddElement(element3, "height1", this.рельсы[i].высота[1]);
                Xml.AddElement(element3, "d_strel", this.рельсы[i].расстояние_добавочных_проводов);
                Xml.AddElement(element3, "iskriv", this.рельсы[i].кривая ? ((double) 1) : ((double) 0));
            }
            XmlElement element4 = Xml.AddElement(element, "Roads");
            for (int j = 0; j < this.дороги.Length; j++)
            {
                XmlElement element5 = Xml.AddElement(element4, "road" + j.ToString());
                Xml.AddElement(element5, "x0", this.дороги[j].концы[0].x);
                Xml.AddElement(element5, "y0", this.дороги[j].концы[0].y);
                Xml.AddElement(element5, "x1", this.дороги[j].концы[1].x);
                Xml.AddElement(element5, "y1", this.дороги[j].концы[1].y);
                Xml.AddElement(element5, "angle0", this.дороги[j].направления[0]);
                Xml.AddElement(element5, "angle1", this.дороги[j].направления[1]);
                Xml.AddElement(element5, "wide0", this.дороги[j].ширина[0]);
                Xml.AddElement(element5, "wide1", this.дороги[j].ширина[1]);
                Xml.AddElement(element5, "height0", this.дороги[j].высота[0]);
                Xml.AddElement(element5, "height1", this.дороги[j].высота[1]);
                Xml.AddElement(element5, "iskriv", this.дороги[j].кривая ? ((double) 1) : ((double) 0));
            }
            XmlElement element6 = Xml.AddElement(element, "Trolleybus_lines");
            for (int k = 0; k < this.контактные_провода.Length; k++)
            {
                XmlElement element7 = Xml.AddElement(element6, "line" + k.ToString());
                Xml.AddElement(element7, "x0", this.контактные_провода[k].начало.x);
                Xml.AddElement(element7, "y0", this.контактные_провода[k].начало.y);
                Xml.AddElement(element7, "x1", this.контактные_провода[k].конец.x);
                Xml.AddElement(element7, "y1", this.контактные_провода[k].конец.y);
                Xml.AddElement(element7, "height0", this.контактные_провода[k].высота[0]);
                Xml.AddElement(element7, "height1", this.контактные_провода[k].высота[1]);
                Xml.AddElement(element7, "right", this.контактные_провода[k].правый ? ((double) 1) : ((double) 0));
                Xml.AddElement(element7, "no_contact", this.контактные_провода[k].обесточенный ? ((double) 1) : ((double) 0));
            }
            XmlElement element8 = Xml.AddElement(element, "Parks");
            for (int m = 0; m < this.парки.Length; m++)
            {
                XmlElement element9 = Xml.AddElement(element8, "park" + m.ToString());
                Xml.AddElement(element9, "name", this.парки[m].название);
                Xml.AddElement(element9, "in", (double) this.Найти_индекс_для_сохранения(this.парки[m].въезд));
                Xml.AddElement(element9, "out", (double) this.Найти_индекс_для_сохранения(this.парки[m].выезд));
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
                        Xml.AddElement(element10, "park_rail" + num6.ToString(), (double) num7);
                    }
                    index++;
                }
            }
            XmlElement element11 = Xml.AddElement(element, "Routes");
            for (int n = 0; n < this.маршруты.Length; n++)
            {
                XmlElement element12 = Xml.AddElement(element11, "route" + n.ToString());
                Xml.AddElement(element12, "name", this.маршруты[n].номер);
                Xml.AddElement(element12, "type", (double) this.маршруты[n].вид_транспорта);
                XmlElement element13 = Xml.AddElement(element12, "route_runs");
                for (int num9 = 0; num9 < this.маршруты[n].рейсы.Count; num9++)
                {
                    XmlElement element14 = Xml.AddElement(element13, "run" + num9.ToString());
                    Xml.AddElement(element14, "time", this.маршруты[n].рейсы[num9].время_прибытия);
                    XmlElement element15 = Xml.AddElement(element14, "run_rails");
                    for (int num10 = 0; num10 < this.маршруты[n].рейсы[num9].путь.Length; num10++)
                    {
                        int num11 = this.Найти_индекс_для_сохранения(this.маршруты[n].рейсы[num9].путь[num10]);
                        if (num11 < 0)
                        {
                            throw new IndexOutOfRangeException("Маршрут " + this.маршруты[n].номер + " (рейс " + num9.ToString() + ") проходит по несуществующему пути!");
                        }
                        Xml.AddElement(element15, "run_rail" + num10.ToString(), (double) num11);
                    }
                }
                XmlElement element16 = Xml.AddElement(element12, "park_runs");
                for (int num12 = 0; num12 < this.маршруты[n].парковые_рейсы.Count; num12++)
                {
                    XmlElement element17 = Xml.AddElement(element16, "run" + num12.ToString());
                    Xml.AddElement(element17, "to_park", this.маршруты[n].парковые_рейсы[num12].в_парк ? ((double) 1) : ((double) 0));
                    Xml.AddElement(element17, "to_park_index", (double) this.маршруты[n].парковые_рейсы[num12].в_парк_index);
                    Xml.AddElement(element17, "time", this.маршруты[n].парковые_рейсы[num12].время_прибытия);
                    XmlElement element18 = Xml.AddElement(element17, "run_rails");
                    for (int num13 = 0; num13 < this.маршруты[n].парковые_рейсы[num12].путь.Length; num13++)
                    {
                        int num14 = this.Найти_индекс_для_сохранения(this.маршруты[n].парковые_рейсы[num12].путь[num13]);
                        if (num14 < 0)
                        {
                            throw new IndexOutOfRangeException("Маршрут " + this.маршруты[n].номер + " (парковый рейс " + num12.ToString() + ") проходит по несуществующему пути!");
                        }
                        Xml.AddElement(element18, "run_rail" + num13.ToString(), (double) num14);
                    }
                }
                XmlElement element19 = Xml.AddElement(element12, "Narads");
                for (int num15 = 0; num15 < this.маршруты[n].наряды.Length; num15++)
                {
                    XmlElement element20 = Xml.AddElement(element19, "narad" + num15.ToString());
                    Xml.AddElement(element20, "name", this.маршруты[n].наряды[num15].номер);
                    int num16 = this.Найти_индекс(this.маршруты[n].наряды[num15].парк);
                    if (num16 < 0)
                    {
                        throw new IndexOutOfRangeException("В наряде " + this.маршруты[n].номер + "/" + this.маршруты[n].наряды[num15].номер + " не указан парк!");
                    }
                    Xml.AddElement(element20, "park", (double) num16);
                    Xml.AddElement(element20, "po_rabochim", this.маршруты[n].наряды[num15].по_рабочим ? ((double) 1) : ((double) 0));
                    Xml.AddElement(element20, "po_vihodnim", this.маршруты[n].наряды[num15].по_выходным ? ((double) 1) : ((double) 0));
                    XmlElement element21 = Xml.AddElement(element20, "runs");
                    for (int num17 = 0; num17 < this.маршруты[n].наряды[num15].рейсы.Length; num17++)
                    {
                        Рейс рейс = this.маршруты[n].наряды[num15].рейсы[num17];
                        XmlElement element22 = Xml.AddElement(element21, "run" + num17.ToString());
                        bool flag = false;
                        int num18 = this.Найти_индекс(рейс, this.маршруты[n], ref flag);
                        if (num18 < 0)
                        {
                            num18 = 0;
                        }
                        Xml.AddElement(element22, "park", flag ? ((double) 1) : ((double) 0));
                        Xml.AddElement(element22, "index", (double) num18);
                        Xml.AddElement(element22, "time", this.маршруты[n].наряды[num15].рейсы[num17].время_отправления);
                    }
                }
            }
            XmlElement element23 = Xml.AddElement(element, "Stops");
            for (int num19 = 0; num19 < this.остановки.Length; num19++)
            {
                XmlElement element24 = Xml.AddElement(element23, "stop" + num19.ToString());
                Xml.AddElement(element24, "name", this.остановки[num19].название);
                Xml.AddElement(element24, "type", (double) this.остановки[num19].вид_транспорта);
                int num20 = this.Найти_индекс_для_сохранения(this.остановки[num19].дорога);
                if (num20 < 0)
                {
                    throw new IndexOutOfRangeException("Остановка \"" + this.остановки[num19].название + "\" находится на несуществующем пути!");
                }
                Xml.AddElement(element24, "rail", (double) num20);
                Xml.AddElement(element24, "distance", this.остановки[num19].расстояние);
                XmlElement element25 = Xml.AddElement(element24, "stop_path");
                for (int num21 = 0; num21 < this.остановки[num19].часть_пути.Length; num21++)
                {
                    int num22 = this.Найти_индекс_для_сохранения(this.остановки[num19].часть_пути[num21]);
                    if (num22 < 0)
                    {
                        break;
                    }
                    Xml.AddElement(element25, "stop_rail" + num21.ToString(), (double) num22);
                }
            }
            XmlElement element26 = Xml.AddElement(element, "Signals");
            for (int num23 = 0; num23 < this.сигнальные_системы.Length; num23++)
            {
                XmlElement element27 = Xml.AddElement(element26, "signal" + num23.ToString());
                Xml.AddElement(element27, "status", (double) this.сигнальные_системы[num23].состояние);
                Xml.AddElement(element27, "bound", (double) this.сигнальные_системы[num23].граница_переключения);
                XmlElement element28 = Xml.AddElement(element27, "elements");
                for (int num24 = 0; num24 < this.сигнальные_системы[num23].элементы.Count; num24++)
                {
                    XmlElement element29 = Xml.AddElement(element28, "element" + num24.ToString());
                    Xml.AddElement(element29, "type", this.сигнальные_системы[num23].элементы[num24].GetType().Name);
                    int num25 = this.Найти_индекс_для_сохранения(this.сигнальные_системы[num23].элементы[num24].дорога);
                    if (num25 < 0)
                    {
                        throw new IndexOutOfRangeException("Элемент №" + num24.ToString() + " сигнальной сиснемы №" + num23.ToString() + " находится на несуществующем пути!");
                    }
                    Xml.AddElement(element29, "rail", (double) num25);
                    Xml.AddElement(element29, "distance", this.сигнальные_системы[num23].элементы[num24].расстояние);
                    if (this.сигнальные_системы[num23].элементы[num24] is Сигнальная_система.Контакт)
                    {
                        Сигнальная_система.Контакт контакт = (Сигнальная_система.Контакт) this.сигнальные_системы[num23].элементы[num24];
                        Xml.AddElement(element29, "minus", контакт.минус ? ((double) 1) : ((double) 0));
                    }
                }
            }
            XmlElement element30 = Xml.AddElement(element, "Svetofor_systems");
            for (int num26 = 0; num26 < this.светофорные_системы.Length; num26++)
            {
                XmlElement element31 = Xml.AddElement(element30, "svetofor_system" + num26.ToString());
                Xml.AddElement(element31, "begin", this.светофорные_системы[num26].начало_работы);
                Xml.AddElement(element31, "end", this.светофорные_системы[num26].окончание_работы);
                Xml.AddElement(element31, "cycle", this.светофорные_системы[num26].цикл);
                Xml.AddElement(element31, "time_to_green", this.светофорные_системы[num26].время_переключения_на_зелёный);
                Xml.AddElement(element31, "time_of_green", this.светофорные_системы[num26].время_зелёного);
                XmlElement element32 = Xml.AddElement(element31, "svetofors");
                for (int num27 = 0; num27 < this.светофорные_системы[num26].светофоры.Count; num27++)
                {
                    XmlElement element33 = Xml.AddElement(element32, "svetofor" + num27.ToString());
                    int num28 = this.Найти_индекс_для_сохранения(this.светофорные_системы[num26].светофоры[num27].положение.дорога);
                    if (num28 < 0)
                    {
                        throw new IndexOutOfRangeException("Светофор №" + num27.ToString() + " светофорной сиснемы №" + num26.ToString() + " находится на несуществующем пути!");
                    }
                    Xml.AddElement(element33, "rail", (double) num28);
                    Xml.AddElement(element33, "distance", this.светофорные_системы[num26].светофоры[num27].положение.расстояние);
                    Xml.AddElement(element33, "place", this.светофорные_системы[num26].светофоры[num27].положение.отклонение);
                    Xml.AddElement(element33, "arrow", this.светофорные_системы[num26].светофоры[num27].стрелка ? ((double) 1) : ((double) 0));
                    Xml.AddElement(element33, "arrow_green", (double) this.светофорные_системы[num26].светофоры[num27].зелёная_стрелка);
                    Xml.AddElement(element33, "arrow_yellow", (double) this.светофорные_системы[num26].светофоры[num27].жёлтая_стрелка);
                    Xml.AddElement(element33, "arrow_red", (double) this.светофорные_системы[num26].светофоры[num27].красная_стрелка);
                }
                XmlElement element34 = Xml.AddElement(element31, "svetofor_signals");
                for (int num29 = 0; num29 < this.светофорные_системы[num26].светофорные_сигналы.Count; num29++)
                {
                    XmlElement element35 = Xml.AddElement(element34, "svetofor_signal" + num29.ToString());
                    int num30 = this.Найти_индекс_для_сохранения(this.светофорные_системы[num26].светофорные_сигналы[num29].дорога);
                    if (num30 < 0)
                    {
                        throw new IndexOutOfRangeException("Сигнал №" + num29.ToString() + " светофорной сиснемы №" + num26.ToString() + " находится на несуществующем пути!");
                    }
                    Xml.AddElement(element35, "rail", (double) num30);
                    Xml.AddElement(element35, "distance", this.светофорные_системы[num26].светофорные_сигналы[num29].расстояние);
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

        public Дорога[] все_дороги
        {
            get
            {
                return (Дорога[]) this.list_дороги.ToArray(typeof(Дорога));
            }
        }

        public List<Наряд> все_наряды
        {
            get
            {
                List<Наряд> list = new List<Наряд>();
                foreach (Маршрут маршрут in this.маршруты)
                {
                    list.AddRange(маршрут.наряды);
                }
                return list;
            }
        }

        public Дорога[] дороги
        {
            get
            {
                return this.list_дороги.Get_array<Дорога>();
            }
        }

        public Рельс[] рельсы
        {
            get
            {
                return this.list_дороги.Get_array<Рельс>();
            }
        }
    }
}

