using Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Trancity
{
    public static class Модели
    {
        public static readonly List<МодельТранспорта> Автобусы = new List<МодельТранспорта>();
        public static readonly List<МодельТранспорта> Троллейбусы = new List<МодельТранспорта>();
        public static readonly List<МодельТранспорта> Трамваи = new List<МодельТранспорта>();
        public static readonly List<МодельТранспорта> Электробус = new List<МодельТранспорта>();
        public static readonly List<МодельТранспорта> Траффик = new List<МодельТранспорта>();

        static Модели()
        {
            var document = new XmlDocument();
            var path = Application.StartupPath + @"\Data\Transport\";
            //            var s = Directory.GetDirectories(path);
            foreach (var directory in Directory.GetDirectories(path))
            {
                var directory1 = directory + @"\";
                try
                {
                    document.Load(directory1 + "model.xml");
                }
                catch (Exception)
                {
                    Logger.Log("ModelLoader", "model.xml не обнаружен в " + directory1);
                    continue;
                }
                var element = document["Trancity"];
                if (element == null) continue;
                var element1 = element["Doors"];
                if (element1 == null) continue;
                //Двери = new МодельДверей[element2.ChildNodes.Count];
                var транспорт = new МодельТранспорта();
                for (var i = 0; i < element1.ChildNodes.Count; i++)
                {
                    var node = element1.ChildNodes[i];
                    if (node == null) continue;
                    var тип = (МодельДверей.Тип)((int)Xml.GetDouble(node["type"]));
                    var innerText = directory1 + node["dir"].InnerText;
                    var filename = node["filename"].InnerText;
                    var num2 = Xml.GetDouble(node["length"]);
                    var num3 = Xml.GetDouble(node["height"]);
                    var num4 = Xml.GetDouble(node["width"]);
                    if (транспорт.модельДверей != null)
                        транспорт.модельДверей.Add(new МодельДверей(тип, innerText, filename, num2, num3, num4));
                }
                var element2 = element["Model"];
                if (element2 == null) continue;
                var element3 = element2.FirstChild;
                if (element3 == null) continue;
                try
                {
                    switch (element3.Name.ToLower())
                    {
                        case "trolleybus":
                            LoadTrolleybus(транспорт, directory1, element3);
                            Троллейбусы.Add(транспорт);
                            break;
                        case "bus":
                            LoadTrolleybus(транспорт, directory1, element3);
                            Автобусы.Add(транспорт);
                            break;
                        case "electrobus":
                            LoadTrolleybus(транспорт, directory1, element3);
                            Электробус.Add(транспорт);
                            break;
                        case "traffic":
                            LoadTrolleybus(транспорт, directory1, element3);
                            Траффик.Add(транспорт);
                            break;
                        case "tramway":
                            LoadTramway(транспорт, directory1, element3);
                            Трамваи.Add(транспорт);
                            break;
                    }

                }
                catch (Exception)
                {
                    Logger.Log("ModelLoader", "Ошибка в " + directory1 + "model.xml");
                    continue;
                }
            }
        }

        private static void LoadTrolleybus(МодельТранспорта транспорт, string directory1, XmlNode element3)
        {
            транспорт.name = element3["name"].InnerText;
            транспорт.dir = directory1 + element3["dir"].InnerText;
            транспорт.filename = element3["filename"].InnerText;
            //	        транспорт.количествоХвостов = (int)Xml.GetDouble(element3["tail_count"]);
            транспорт.хвостFilename = LoadStrings(element3["tail_filename"]);
            транспорт.хвостDist1 = LoadDoubles(element3["tail_dist_1"]);
            транспорт.хвостDist2 = LoadDoubles(element3["tail_dist_2"]);
            транспорт.сочленениеFilename = LoadStrings(element3["middle_filename"]);
            транспорт.дополнения = LoadAdditions(element3["additions"]);
            транспорт.количествоДверей = (int)Xml.GetDouble(element3["door_count"]);
            транспорт.двери = LoadDoors(транспорт, element3["doors"]);
            транспорт.радиусКолёс = Xml.GetDouble(element3["wheel_radius"]);
            транспорт.колёсныеПары = LoadWheels(directory1, element3["wheels"]);
            транспорт.штангиDir = directory1 + element3["shtangi_dir"].InnerText;
            транспорт.штангиFilename = element3["shtangi_filename"].InnerText;
            транспорт.штангиПолнаяДлина = Xml.GetDouble(element3["shtangi_full_length"]);
            транспорт.штангиУголMin = Xml.GetDouble(element3["shtangi_angle_min"]);
            транспорт.штанги = LoadShtangi(element3["shtangi"]);
            транспорт.руль = LoadSteering(directory1, element3["steering"]);
            транспорт.ах = LoadStandaloneMotion(element3["standalone_motion"]);
            транспорт.табличка = Load_VPark_Tabl(element3["tabl_v_park"]);
            транспорт.нарядPos = LoadDouble_3DPoint(element3["narad_pos"]);
            транспорт.cameras = LoadCameras(element3["cameras"]);
            try//for tests
            {
                //транспорт.bbox = LoadDouble_3DPoints(element3["bounding_box"]);
                //транспорт.tails_bbox = LoadArrayOfDouble_3DPoints(element3["tails_bounding_box"]);
                транспорт.bsphere = LoadBSphere(element3["bounding_sphere"]);
                транспорт.tails_bsphere = LoadBSpheres(element3["tails_bounding_sphere"]);
            }
            catch
            {
                транспорт.hasnt_bbox = true;
                Logger.Log("ModelLoader", "Bounding spheres not loaded for " + транспорт.name);
            }
            транспорт.занятыеПоложения = LoadDoublePoints(element3["occupied_locations"]);
            транспорт.занятыеПоложенияХвостов = LoadArrayOfDoublePoints(element3["tails_occupied_locations"]);
            транспорт.системаУправления = element3["control_system"].InnerText;
        }

        private static void LoadTramway(МодельТранспорта трамвай, string directory1, XmlNode element3)
        {
            трамвай.name = element3["name"].InnerText;
            трамвай.dir = directory1 + element3["dir"].InnerText;
            трамвай.filename = element3["filename"].InnerText;
            //			трамвай.количествоХвостов = (int)Xml.GetDouble(element3["tails_count"]);
            трамвай.tails = LoadTails(element3["tails"]);
            try
            {
                //			трамвай.количествоСочленений = (int)Xml.GetDouble(element3["middles_count"]);
                трамвай.сочленения = LoadNewMiddles(element3["middles"]);
            }
            catch { };
            трамвай.дополнения = LoadAdditions(element3["additions"]);
            трамвай.количествоДверей = (int)Xml.GetDouble(element3["door_count"]);
            трамвай.двери = LoadDoors(трамвай, element3["doors"]);
            трамвай.axisfilename = element3["axis_filename"].InnerText;
            трамвай.telegafilename = element3["telegi_filename"].InnerText;
            трамвай.расстояние_между_осями = (double)Xml.GetDouble(element3["axis_dist"]);
            трамвай.axis_radius = (double)Xml.GetDouble(element3["axis_radius"]);
            //Убрать:
            //			трамвай.расстояние_между_тележками = (double)Xml.GetDouble(element3["telegi_dist"]);
            трамвай.тележки = LoadBoogies(element3["telegi"]);
            трамвай.пантограф = LoadPantograph(directory1, element3["pantograph"]);
            /*трамвай.штангиDir = directory1 + element3["shtangi_dir"].InnerText;
            трамвай.штангиFilename = element3["shtangi_filename"].InnerText;
            трамвай.штангиПолнаяДлина = Xml.GetDouble(element3["shtangi_full_length"]);
            трамвай.штангиУголMin = Xml.GetDouble(element3["shtangi_angle_min"]);
            трамвай.штанги = LoadShtangi(element3["shtangi"]);*/
            трамвай.табличка = Load_VPark_Tabl(element3["tabl_v_park"]);
            трамвай.нарядPos = LoadDouble_3DPoint(element3["narad_pos"]);
            трамвай.cameras = LoadCameras(element3["cameras"]);
            try//for tests
            {
                //трамвай.bbox = LoadDouble_3DPoints(element3["bounding_box"]);
                //трамвай.tails_bbox = LoadArrayOfDouble_3DPoints(element3["tails_bounding_box"]);
                трамвай.bsphere = LoadBSphere(element3["bounding_sphere"]);
                трамвай.tails_bsphere = LoadBSpheres(element3["tails_bounding_sphere"]);
            }
            catch
            {
                трамвай.hasnt_bbox = true;
                Logger.Log("ModelLoader", "Bounding spheres not loaded for " + трамвай.name);
            }
            трамвай.занятыеПоложения = LoadDoublePoints(element3["occupied_locations"]);
            трамвай.занятыеПоложенияХвостов = LoadArrayOfDoublePoints(element3["tails_occupied_locations"]);
            трамвай.системаУправления = element3["control_system"].InnerText;
        }

        private static МодельТранспорта.Руль LoadSteering(string directory, XmlNode node)
        {
            if (node != null)
            {
                var d = directory + node["dir"].InnerText;
                var f = node["filename"].InnerText;
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                var z = Xml.GetDouble(node["z"]);
                var a = Xml.GetDouble(node["angle"]);
                return new МодельТранспорта.Руль(d, f, x, y, z, a);
            }
            return null;
        }

        private static МодельТранспорта.АХ LoadStandaloneMotion(XmlNode node)
        {
            if (node != null)
            {
                var z = Xml.GetDouble(node["battery_power"]);
                var a = Xml.GetDouble(node["acceleration"]);
                var h = Xml.GetDouble(node["charge_consumption"]);
                if (h == null)
                {
                    h = 0.20;
                }
                return new МодельТранспорта.АХ(z, a, h);
            }
            return null;
        }

        private static МодельТранспорта.Дополнение[] LoadAdditions(XmlNode items)
        {
            var дополнениеArray = new МодельТранспорта.Дополнение[items.ChildNodes.Count];
            for (var i = 0; i < дополнениеArray.Length; i++)
            {
                var node = items.ChildNodes[i];
                var num2 = (int)Xml.GetDouble(node["part"]);
                var innerText = node["filename"].InnerText;
                var num3 = (int)Xml.GetDouble(node["type"]);
                дополнениеArray[i] = new МодельТранспорта.Дополнение(num2, innerText, (Transport.Тип_дополнения)num3);
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

        private static Double3DPoint[][] LoadArrayOfDouble_3DPoints(XmlNode items)
        {
            var pointArray = new Double3DPoint[items.ChildNodes.Count][];
            for (var i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = LoadDouble_3DPoints(items.ChildNodes[i]);
            }
            return pointArray;
        }

        private static МодельТранспорта.Дверь[] LoadDoors(МодельТранспорта троллейбус, XmlNode items)
        {
            var дверьArray = new МодельТранспорта.Дверь[items.ChildNodes.Count];
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
                дверьArray[i] = new МодельТранспорта.Дверь(дверь, num2, num3, num7, num4, num8, num5, num6, flag, flag2, num9);
            }
            return дверьArray;
        }

        private static Double3DPoint LoadDouble_3DPoint(XmlNode items)
        {
            return new Double3DPoint(Xml.GetDouble(items["x"]), Xml.GetDouble(items["y"]), Xml.GetDouble(items["z"]));
        }

        private static Double3DPoint[] LoadDouble_3DPoints(XmlNode items)
        {
            var pointArray = new Double3DPoint[items.ChildNodes.Count];
            for (var i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = LoadDouble_3DPoint(items.ChildNodes[i]);
            }
            return pointArray;
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

        private static МодельТранспорта.Штанга[] LoadShtangi(XmlNode items)
        {
            var штангаArray = new МодельТранспорта.Штанга[items.ChildNodes.Count];
            for (var i = 0; i < штангаArray.Length; i++)
            {
                var node = items.ChildNodes[i];
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                var z = Xml.GetDouble(node["z"]);
                штангаArray[i] = new МодельТранспорта.Штанга(x, y, z);
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

        private static МодельТранспорта.КолёснаяПара[] LoadWheels(string directory, XmlNode items)
        {
            var параArray = new МодельТранспорта.КолёснаяПара[items.ChildNodes.Count];
            for (var i = 0; i < параArray.Length; i++)
            {
                var node = items.ChildNodes[i];
                var innerText = directory + node["dir"].InnerText;
                var filename = node["filename"].InnerText;
                var num2 = (int)Xml.GetDouble(node["part"]);
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                параArray[i] = new МодельТранспорта.КолёснаяПара(innerText, filename, num2, x, y);
            }
            return параArray;
        }

        private static МодельТранспорта.Хвост[] LoadTails(XmlNode items)
        {
            var tailsArray = new МодельТранспорта.Хвост[items.ChildNodes.Count];
            for (var i = 0; i < tailsArray.Length; i++)
            {
                XmlNode node = items.ChildNodes[i];
                double dist = (double)Xml.GetDouble(node["distance"]);
                string filename = node["filename"].InnerText;
                //            	var flag = Xml.GetDouble(node["have_middle"]) != 0.0;
                //            	string mfilename = node["middle_filename"].InnerText;
                //            	double ad = (double)Xml.GetDouble(node["axis_dist"]);
                //            	double td = (double)Xml.GetDouble(node["telegi_dist"]);
                tailsArray[i] = new МодельТранспорта.Хвост(dist, filename);//, td);//mfilename, flag,
            }
            return tailsArray;
        }

        private static МодельТранспорта.Сочленение_new[] LoadNewMiddles(XmlNode items)
        {
            var middlesArray = new МодельТранспорта.Сочленение_new[items.ChildNodes.Count];
            for (var i = 0; i < middlesArray.Length; i++)
            {
                XmlNode node = items.ChildNodes[i];
                int ind = (int)Xml.GetDouble(node["index"]);
                int tar = (int)Xml.GetDouble(node["target"]);
                double dist = (double)Xml.GetDouble(node["distance"]);
                string filename = node["filename"].InnerText;
                //            	var flag = Xml.GetDouble(node["reverse"]) != 0.0;
                middlesArray[i] = new МодельТранспорта.Сочленение_new(dist, filename, ind, tar);//, flag);
            }
            return middlesArray;
        }

        /*private static МодельТранспорта.Пантограф LoadPantograph(string directory, XmlNode node)
        {
        	МодельТранспорта.Пантограф._Type t;
        	try
        	{
        		t = (МодельТранспорта.Пантограф._Type)((int)Xml.GetDouble(node["type"]));
        	}
        	catch
        	{
        		t = (МодельТранспорта.Пантограф._Type)0;
        	}
            var d = directory + node["dir"].InnerText;
            var f = node["osn_filename"].InnerText;
            var pr = node["part_filename"].InnerText;
            var pn = node["duga_filename"].InnerText;
            var x = Xml.GetDouble(node["x"]);
            var y = Xml.GetDouble(node["y"]);
            var z = Xml.GetDouble(node["z"]);
        	return new МодельТранспорта.Пантограф(t, d, f, pr, pn, x, y, z);
        }*/

        private static МодельТранспорта.Пантограф LoadPantograph(string directory, XmlNode node)
        {
            var d = directory + node["dir"].InnerText;
            var x = Xml.GetDouble(node["x"]);
            var y = Xml.GetDouble(node["y"]);
            var z = Xml.GetDouble(node["z"]);
            var min_h = Xml.GetDouble(node["min_height"]);
            var max_h = Xml.GetDouble(node["max_height"]);
            var dist = Math.Abs(Xml.GetDouble(node["dist"]));
            return new МодельТранспорта.Пантограф(d, x, y, z, min_h, max_h, dist, LoadPantographsParts(node["parts"]));
        }

        private static МодельТранспорта.Часть_пантографа[] LoadPantographsParts(XmlNode node)
        {
            var partsArray = new МодельТранспорта.Часть_пантографа[node.ChildNodes.Count];
            for (var i = 0; i < partsArray.Length; i++)
            {
                XmlNode node1 = node.ChildNodes[i];
                //            	int ind = (int)Xml.GetDouble(node1["index"]);
                var height = Xml.GetDouble(node1["height"]);
                string filename = node1["filename"].InnerText;
                var width = Xml.GetDouble(node1["width"]);
                var length = Xml.GetDouble(node1["length"]);
                var ang = Xml.GetDouble(node1["norm_angel"]);
                partsArray[i] = new МодельТранспорта.Часть_пантографа(filename, height, width, length, ang);
            }
            return partsArray;
        }

        private static МодельТранспорта.Табличка Load_VPark_Tabl(XmlNode node)
        {
            if (node != null)
            {
                var f = node["filename"].InnerText;
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                var z = Xml.GetDouble(node["z"]);
                return new МодельТранспорта.Табличка(f, x, y, z);
            }
            return null;
        }

        private static МодельТранспорта.SphereModel LoadBSphere(XmlNode node)
        {
            /*if (node != null)
        	{*/
            var r = Xml.GetDouble(node["r"]);
            var x = Xml.GetDouble(node["x"]);
            var y = Xml.GetDouble(node["y"]);
            var z = Xml.GetDouble(node["z"]);
            return new МодельТранспорта.SphereModel(r, x, y, z);
            /*}
        	return null;*/
        }

        private static МодельТранспорта.SphereModel[] LoadBSpheres(XmlNode node)
        {
            /*if (node != null)
        	{*/
            var spheres = new МодельТранспорта.SphereModel[node.ChildNodes.Count];
            for (int i = 0; i < spheres.Length; i++)
            {
                spheres[i] = LoadBSphere(node.ChildNodes[i]);
            }
            return spheres;
            /*}
        	return null;*/
        }

        private static МодельТранспорта.Тележка[] LoadBoogies(XmlNode node)
        {
            var boogieArray = new МодельТранспорта.Тележка[node.ChildNodes.Count];
            for (var i = 0; i < boogieArray.Length; i++)
            {
                XmlNode node1 = node.ChildNodes[i];
                int ind = (int)Xml.GetDouble(node1["index"]);
                string filename = Xml.GetString(node1["filename"]);
                double dist = Xml.GetDouble(node1["dist"]);
                boogieArray[i] = new МодельТранспорта.Тележка(ind, dist, filename);
            }
            return boogieArray;
        }

        private static МодельТранспорта.Camera[] LoadCameras(XmlNode node)
        {
            if (node != null)
            {
                var cameras = new МодельТранспорта.Camera[node.ChildNodes.Count];
                for (int i = 0; i < cameras.Length; i++)
                {
                    XmlNode node1 = node.ChildNodes[i];
                    var x = Xml.GetDouble(node1["x"]);
                    var y = Xml.GetDouble(node1["y"]);
                    var z = Xml.GetDouble(node1["z"]);
                    var rx = Xml.GetDouble(node1["rot_y"]);
                    var ry = Xml.GetDouble(node1["rot_z"]);
                    cameras[i] = new МодельТранспорта.Camera(x, y, z, rx, ry);
                }
                return cameras;
            }
            return null;
        }
    }
}