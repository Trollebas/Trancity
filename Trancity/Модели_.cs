using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using Common;

namespace Trancity
{
    public static class Модели
    {
        public static readonly List<МодельТроллейбуса> Автобусы = new List<МодельТроллейбуса>();
        //private static readonly List<МодельДверей> Двери = new List<МодельДверей>();
        public static readonly List<МодельТроллейбуса> Троллейбусы = new List<МодельТроллейбуса>();
        
        static Модели()
        {
            var document = new XmlDocument();
            Xml.document = document;
            var path = Application.StartupPath + @"\Data\Transport\";
            //var s = Directory.GetDirectories(path);
            foreach (var directory in Directory.GetDirectories(path))
            {
                var directory1 = directory+ @"\";
                try
                {
                    document.Load(directory1 + "model.xml");
                }
                catch (Exception)
                {
                    continue;
                }
                var element = document["Trancity"];
                if (element == null) continue;
                var element1 = element["Doors"];
                if (element1 == null) continue;
                //Двери = new МодельДверей[element2.ChildNodes.Count];
                var троллейбус = new МодельТроллейбуса();                
                for (var i = 0; i < element1.ChildNodes.Count; i++)
                {
                    var node = element1.ChildNodes[i];
                    if (node == null) continue;
                    var тип = (МодельДверей.Тип)((int)Xml.GetDouble(node["type"]));
                    var innerText = directory1+node["dir"].InnerText;
                    var filename = node["filename"].InnerText;
                    var num2 = Xml.GetDouble(node["length"]);
                    var num3 = Xml.GetDouble(node["height"]);
                    var num4 = Xml.GetDouble(node["width"]);
                    if (троллейбус.модельДверей != null)
                        троллейбус.модельДверей.Add(new МодельДверей(тип, innerText, filename, num2, num3, num4));
                }
                var element2 = element["Model"];
                if (element2 == null) continue;
                var element3 = element2.FirstChild;
                if (element3 == null) continue;
                try
                {
                    троллейбус.name = element3["name"].InnerText;
                    троллейбус.dir = directory1 + element3["dir"].InnerText;
                    троллейбус.filename = element3["filename"].InnerText;
                    троллейбус.количествоХвостов = (int)Xml.GetDouble(element3["tail_count"]);
                    троллейбус.хвостFilename = LoadStrings(element3["tail_filename"]);
                    троллейбус.хвостDist1 = LoadDoubles(element3["tail_dist_1"]);
                    троллейбус.хвостDist2 = LoadDoubles(element3["tail_dist_2"]);
                    троллейбус.сочленениеFilename = LoadStrings(element3["middle_filename"]);
                    троллейбус.дополнения = LoadAdditions(element3["additions"]);
                    троллейбус.количествоДверей = (int)Xml.GetDouble(element3["door_count"]);
                    троллейбус.двери = LoadDoors(троллейбус,element3["doors"]);
                    троллейбус.радиусКолёс = Xml.GetDouble(element3["wheel_radius"]);
                    троллейбус.колёсныеПары = LoadWheels(directory1,element3["wheels"]);
                    троллейбус.штангиDir = directory1 + element3["shtangi_dir"].InnerText;
                    троллейбус.штангиFilename = element3["shtangi_filename"].InnerText;
                    троллейбус.штангиПолнаяДлина = Xml.GetDouble(element3["shtangi_full_length"]);
                    троллейбус.штангиУголMin = Xml.GetDouble(element3["shtangi_angle_min"]);
                    троллейбус.штанги = LoadShtangi(element3["shtangi"]);
                    XmlNode node = element3["steering"];
                    троллейбус.руль = LoadSteering(directory1, node);
                    троллейбус.нарядPos = LoadDouble_3DPoint(element3["narad_pos"]);
//                    троллейбус.табличка = Load_VPark_Tabl(element3["tabl_v_park"]);
                    троллейбус.занятыеПоложения = LoadDoublePoints(element3["occupied_locations"]);
                    троллейбус.занятыеПоложенияХвостов = LoadArrayOfDoublePoints(element3["tails_occupied_locations"]);
                    троллейбус.системаУправления = element3["control_system"].InnerText;                   
                    
                }
                catch (Exception)
                {
                    continue;
                }
                finally
                {
                    switch (element3.Name.ToLower())
                    {
                        case "trolleybus":
                            Троллейбусы.Add(троллейбус);
                            break;
                        case "bus":
                            Автобусы.Add(троллейбус);
                            break;
                    }
                }
            }
        }

        private static МодельТроллейбуса.Руль LoadSteering(string directory, XmlNode node)
        {
            if (node != null)
            {
                var d = directory + node["dir"].InnerText;
                var f = node["filename"].InnerText;
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                var z = Xml.GetDouble(node["z"]);
                var a = Xml.GetDouble(node["angle"]);
                return new МодельТроллейбуса.Руль(d, f, x, y, z, a);
            }
            return null;
        }
      
        private static МодельТроллейбуса.Дополнение[] LoadAdditions(XmlNode items)
        {
            var дополнениеArray = new МодельТроллейбуса.Дополнение[items.ChildNodes.Count];
            for (var i = 0; i < дополнениеArray.Length; i++)
            {
                var node = items.ChildNodes[i];
                var num2 = (int)Xml.GetDouble(node["part"]);
                var innerText = node["filename"].InnerText;
                var num3 = (int)Xml.GetDouble(node["type"]);
                дополнениеArray[i] = new МодельТроллейбуса.Дополнение(num2, innerText, (Троллейбус.ОбычныйТроллейбус.Дополнение.Тип)num3);
            }
            return дополнениеArray;
        }

        private static DoublePoint[][] LoadArrayOfDoublePoints(XmlNode items)
        {
            var pointArray = new DoublePoint[items.ChildNodes.Count][];
            for (var i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = LoadDoublePoints(items.ChildNodes[i]);
            }
            return pointArray;
        }

        private static МодельТроллейбуса.Дверь[] LoadDoors(МодельТроллейбуса троллейбус, XmlNode items)
        {
            var дверьArray = new МодельТроллейбуса.Дверь[items.ChildNodes.Count];
            for (var i = 0; i < дверьArray.Length; i++)
            {
                var node = items.ChildNodes[i];
                var index = (int)Xml.GetDouble(node["model"]);
                var дверь = троллейбус.модельДверей[index];
                var num2 = (int)Xml.GetDouble(node["part"]);
                var num3 = Xml.GetDouble(node["x0"]);
                var num4 = Xml.GetDouble(node["x1"]);
                var num5 = Xml.GetDouble(node["y0"]);
                var num6 = Xml.GetDouble(node["y1"]);
                var num7 = Xml.GetDouble(node["z0"]);
                var num8 = Xml.GetDouble(node["z1"]);
                var flag = Xml.GetDouble(node["right"]) != 0.0;
                var flag2 = Xml.GetDouble(node["driver"]) != 0.0;
                var num9 = (int)Xml.GetDouble(node["index"]);
                дверьArray[i] = new МодельТроллейбуса.Дверь(дверь, num2, num3, num7, num4, num8, num5, num6, flag, flag2, num9);
            }
            return дверьArray;
        }

        private static Double3DPoint LoadDouble_3DPoint(XmlNode items)
        {
            return new Double3DPoint(Xml.GetDouble(items["x"]), Xml.GetDouble(items["y"]), Xml.GetDouble(items["z"]));
        }

        private static DoublePoint LoadDoublePoint(XmlNode items)
        {
            return new DoublePoint(Xml.GetDouble(items["x"]), Xml.GetDouble(items["y"]));
        }

        private static DoublePoint[] LoadDoublePoints(XmlNode items)
        {
            var pointArray = new DoublePoint[items.ChildNodes.Count];
            for (var i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = LoadDoublePoint(items.ChildNodes[i]);
            }
            return pointArray;
        }

        private static double[] LoadDoubles(XmlNode items)
        {
            var numArray = new double[items.ChildNodes.Count];
            for (var i = 0; i < numArray.Length; i++)
            {
                numArray[i] = Xml.GetDouble(items.ChildNodes[i]);
            }
            return numArray;
        }

        private static МодельТроллейбуса.Штанга[] LoadShtangi(XmlNode items)
        {
            var штангаArray = new МодельТроллейбуса.Штанга[items.ChildNodes.Count];
            for (var i = 0; i < штангаArray.Length; i++)
            {
                var node = items.ChildNodes[i];
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                var z = Xml.GetDouble(node["z"]);
                штангаArray[i] = new МодельТроллейбуса.Штанга(x, y, z);
            }
            return штангаArray;
        }

        private static string[] LoadStrings(XmlNode items)
        {
        	if (items != null)
        	{
            var strArray = new string[items.ChildNodes.Count];
            for (var i = 0; i < strArray.Length; i++)
            {
                strArray[i] = items.ChildNodes[i].InnerText;
            }
            return strArray;
        	}
        	return null;
        }
        
        private static МодельТроллейбуса.КолёснаяПара[] LoadWheels(string directory, XmlNode items)
        {
            var параArray = new МодельТроллейбуса.КолёснаяПара[items.ChildNodes.Count];
            for (var i = 0; i < параArray.Length; i++)
            {
                var node = items.ChildNodes[i];
                var innerText = directory + node["dir"].InnerText;
                var filename = node["filename"].InnerText;
                var num2 = (int)Xml.GetDouble(node["part"]);
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                параArray[i] = new МодельТроллейбуса.КолёснаяПара(innerText, filename, num2, x, y);
            }
            return параArray;
        }
        
//        private static МодельТроллейбуса.Табличка Load_VPark_Tabl(XmlNode node)
//        {
//                var f = node["filename"].InnerText;
//                var x = Xml.GetDouble(node["x"]);
//                var y = Xml.GetDouble(node["y"]);
//                var z = Xml.GetDouble(node["z"]);
//                return new МодельТроллейбуса.Табличка(f, x, y, z);
//        }
    }
}