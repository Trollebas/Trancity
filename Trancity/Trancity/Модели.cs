namespace Trancity
{
    using Common;
    using System;
    using System.Windows.Forms;
    using System.Xml;

    public static class Модели
    {
        public static Модель_троллейбуса[] автобусы = new Модель_троллейбуса[0];
        public static Модель_дверей[] двери = new Модель_дверей[0];
        public static Модель_троллейбуса[] троллейбусы = new Модель_троллейбуса[0];

        static Модели()
        {
            XmlDocument document = new XmlDocument();
            Xml.document = document;
            document.Load(Application.StartupPath + @"\models.xml");
            XmlElement element = document["Models"];
            XmlElement element2 = element["Doors"];
            двери = new Модель_дверей[element2.ChildNodes.Count];
            for (int i = 0; i < element2.ChildNodes.Count; i++)
            {
                XmlNode node = element2.ChildNodes[i];
                Модель_дверей.Тип тип = (Модель_дверей.Тип) ((int) Xml.GetDouble(node["type"]));
                string innerText = node["dir"].InnerText;
                string filename = node["filename"].InnerText;
                double num2 = Xml.GetDouble(node["length"]);
                double num3 = Xml.GetDouble(node["height"]);
                double num4 = Xml.GetDouble(node["width"]);
                двери[i] = new Модель_дверей(тип, innerText, filename, num2, num3, num4);
            }
            XmlElement element3 = element["Trolls"];
            троллейбусы = new Модель_троллейбуса[element3.ChildNodes.Count];
            for (int j = 0; j < element3.ChildNodes.Count; j++)
            {
                XmlNode troll = element3.ChildNodes[j];
                троллейбусы[j] = load_troll(troll);
            }
            XmlElement element4 = element["Buses"];
            автобусы = new Модель_троллейбуса[element4.ChildNodes.Count];
            for (int k = 0; k < element4.ChildNodes.Count; k++)
            {
                XmlNode node3 = element4.ChildNodes[k];
                автобусы[k] = load_troll(node3);
            }
        }

        private static Модель_троллейбуса.Дополнение[] load_additions(XmlNode items)
        {
            Модель_троллейбуса.Дополнение[] дополнениеArray = new Модель_троллейбуса.Дополнение[items.ChildNodes.Count];
            for (int i = 0; i < дополнениеArray.Length; i++)
            {
                XmlNode node = items.ChildNodes[i];
                int num2 = (int) Xml.GetDouble(node["part"]);
                string innerText = node["filename"].InnerText;
                int num3 = (int) Xml.GetDouble(node["type"]);
                дополнениеArray[i] = new Модель_троллейбуса.Дополнение(num2, innerText, (Троллейбус.Обычный_троллейбус.Дополнение.Тип) num3);
            }
            return дополнениеArray;
        }

        private static Double_Point[][] load_array_of_double_points(XmlNode items)
        {
            Double_Point[][] pointArray = new Double_Point[items.ChildNodes.Count][];
            for (int i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = load_double_points(items.ChildNodes[i]);
            }
            return pointArray;
        }

        private static Модель_троллейбуса.Дверь[] load_doors(XmlNode items)
        {
            Модель_троллейбуса.Дверь[] дверьArray = new Модель_троллейбуса.Дверь[items.ChildNodes.Count];
            for (int i = 0; i < дверьArray.Length; i++)
            {
                XmlNode node = items.ChildNodes[i];
                Модель_дверей _дверей = двери[(int) Xml.GetDouble(node["model"])];
                int num2 = (int) Xml.GetDouble(node["part"]);
                double num3 = Xml.GetDouble(node["x0"]);
                double num4 = Xml.GetDouble(node["x1"]);
                double num5 = Xml.GetDouble(node["y0"]);
                double num6 = Xml.GetDouble(node["y1"]);
                double num7 = Xml.GetDouble(node["z0"]);
                double num8 = Xml.GetDouble(node["z1"]);
                bool flag = Xml.GetDouble(node["right"]) != 0.0;
                bool flag2 = Xml.GetDouble(node["driver"]) != 0.0;
                int num9 = (int) Xml.GetDouble(node["index"]);
                дверьArray[i] = new Модель_троллейбуса.Дверь(_дверей, num2, num3, num7, num4, num8, num5, num6, flag, flag2, num9);
            }
            return дверьArray;
        }

        private static Double_3D_Point load_double_3D_point(XmlNode items)
        {
            return new Double_3D_Point(Xml.GetDouble(items["x"]), Xml.GetDouble(items["y"]), Xml.GetDouble(items["z"]));
        }

        private static Double_Point load_double_point(XmlNode items)
        {
            return new Double_Point(Xml.GetDouble(items["x"]), Xml.GetDouble(items["y"]));
        }

        private static Double_Point[] load_double_points(XmlNode items)
        {
            Double_Point[] pointArray = new Double_Point[items.ChildNodes.Count];
            for (int i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = load_double_point(items.ChildNodes[i]);
            }
            return pointArray;
        }

        private static double[] load_doubles(XmlNode items)
        {
            double[] numArray = new double[items.ChildNodes.Count];
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = Xml.GetDouble(items.ChildNodes[i]);
            }
            return numArray;
        }

        private static Модель_троллейбуса.Штанга[] load_shtangi(XmlNode items)
        {
            Модель_троллейбуса.Штанга[] штангаArray = new Модель_троллейбуса.Штанга[items.ChildNodes.Count];
            for (int i = 0; i < штангаArray.Length; i++)
            {
                XmlNode node = items.ChildNodes[i];
                double x = Xml.GetDouble(node["x"]);
                double y = Xml.GetDouble(node["y"]);
                double z = Xml.GetDouble(node["z"]);
                штангаArray[i] = new Модель_троллейбуса.Штанга(x, y, z);
            }
            return штангаArray;
        }

        private static string[] load_strings(XmlNode items)
        {
            string[] strArray = new string[items.ChildNodes.Count];
            for (int i = 0; i < strArray.Length; i++)
            {
                strArray[i] = items.ChildNodes[i].InnerText;
            }
            return strArray;
        }

        private static Модель_троллейбуса load_troll(XmlNode troll)
        {
            Модель_троллейбуса _троллейбуса = new Модель_троллейбуса();
            _троллейбуса.name = troll["name"].InnerText;
            _троллейбуса.dir = troll["dir"].InnerText;
            _троллейбуса.filename = troll["filename"].InnerText;
            _троллейбуса.количество_хвостов = (int) Xml.GetDouble(troll["tail_count"]);
            _троллейбуса.хвост_filename = load_strings(troll["tail_filename"]);
            _троллейбуса.хвост_dist_1 = load_doubles(troll["tail_dist_1"]);
            _троллейбуса.хвост_dist_2 = load_doubles(troll["tail_dist_2"]);
            _троллейбуса.сочленение_filename = load_strings(troll["middle_filename"]);
            _троллейбуса.дополнения = load_additions(troll["additions"]);
            _троллейбуса.количество_дверей = (int) Xml.GetDouble(troll["door_count"]);
            _троллейбуса.двери = load_doors(troll["doors"]);
            _троллейбуса.радиус_колёс = Xml.GetDouble(troll["wheel_radius"]);
            _троллейбуса.колёсные_пары = load_wheels(troll["wheels"]);
            _троллейбуса.штанги_dir = troll["shtangi_dir"].InnerText;
            _троллейбуса.штанги_filename = troll["shtangi_filename"].InnerText;
            _троллейбуса.штанги_полная_длина = Xml.GetDouble(troll["shtangi_full_length"]);
            _троллейбуса.штанги_угол_min = Xml.GetDouble(troll["shtangi_angle_min"]);
            _троллейбуса.штанги = load_shtangi(troll["shtangi"]);
            _троллейбуса.наряд_pos = load_double_3D_point(troll["narad_pos"]);
            _троллейбуса.занятые_положения = load_double_points(troll["occupied_locations"]);
            _троллейбуса.занятые_положения_хвостов = load_array_of_double_points(troll["tails_occupied_locations"]);
            _троллейбуса.система_управления = troll["control_system"].InnerText;
            return _троллейбуса;
        }

        private static Модель_троллейбуса.Колёсная_пара[] load_wheels(XmlNode items)
        {
            Модель_троллейбуса.Колёсная_пара[] _параArray = new Модель_троллейбуса.Колёсная_пара[items.ChildNodes.Count];
            for (int i = 0; i < _параArray.Length; i++)
            {
                XmlNode node = items.ChildNodes[i];
                string innerText = node["dir"].InnerText;
                string filename = node["filename"].InnerText;
                int num2 = (int) Xml.GetDouble(node["part"]);
                double x = Xml.GetDouble(node["x"]);
                double y = Xml.GetDouble(node["y"]);
                _параArray[i] = new Модель_троллейбуса.Колёсная_пара(innerText, filename, num2, x, y);
            }
            return _параArray;
        }
    }
}

