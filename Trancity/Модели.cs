using Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Trancity
{
    public static class ������
    {
        public static readonly List<����������������> �������� = new List<����������������>();
        public static readonly List<����������������> ����������� = new List<����������������>();
        public static readonly List<����������������> ������� = new List<����������������>();
        public static readonly List<����������������> ���������� = new List<����������������>();
        public static readonly List<����������������> ������� = new List<����������������>();

        static ������()
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
                    Logger.Log("ModelLoader", "model.xml �� ��������� � " + directory1);
                    continue;
                }
                var element = document["Trancity"];
                if (element == null) continue;
                var element1 = element["Doors"];
                if (element1 == null) continue;
                //����� = new ������������[element2.ChildNodes.Count];
                var ��������� = new ����������������();
                for (var i = 0; i < element1.ChildNodes.Count; i++)
                {
                    var node = element1.ChildNodes[i];
                    if (node == null) continue;
                    var ��� = (������������.���)((int)Xml.GetDouble(node["type"]));
                    var innerText = directory1 + node["dir"].InnerText;
                    var filename = node["filename"].InnerText;
                    var num2 = Xml.GetDouble(node["length"]);
                    var num3 = Xml.GetDouble(node["height"]);
                    var num4 = Xml.GetDouble(node["width"]);
                    if (���������.������������ != null)
                        ���������.������������.Add(new ������������(���, innerText, filename, num2, num3, num4));
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
                            LoadTrolleybus(���������, directory1, element3);
                            �����������.Add(���������);
                            break;
                        case "bus":
                            LoadTrolleybus(���������, directory1, element3);
                            ��������.Add(���������);
                            break;
                        case "electrobus":
                            LoadTrolleybus(���������, directory1, element3);
                            ����������.Add(���������);
                            break;
                        case "traffic":
                            LoadTrolleybus(���������, directory1, element3);
                            �������.Add(���������);
                            break;
                        case "tramway":
                            LoadTramway(���������, directory1, element3);
                            �������.Add(���������);
                            break;
                    }

                }
                catch (Exception)
                {
                    Logger.Log("ModelLoader", "������ � " + directory1 + "model.xml");
                    continue;
                }
            }
        }

        private static void LoadTrolleybus(���������������� ���������, string directory1, XmlNode element3)
        {
            ���������.name = element3["name"].InnerText;
            ���������.dir = directory1 + element3["dir"].InnerText;
            ���������.filename = element3["filename"].InnerText;
            //	        ���������.����������������� = (int)Xml.GetDouble(element3["tail_count"]);
            ���������.�����Filename = LoadStrings(element3["tail_filename"]);
            ���������.�����Dist1 = LoadDoubles(element3["tail_dist_1"]);
            ���������.�����Dist2 = LoadDoubles(element3["tail_dist_2"]);
            ���������.����������Filename = LoadStrings(element3["middle_filename"]);
            ���������.���������� = LoadAdditions(element3["additions"]);
            ���������.���������������� = (int)Xml.GetDouble(element3["door_count"]);
            ���������.����� = LoadDoors(���������, element3["doors"]);
            ���������.���������� = Xml.GetDouble(element3["wheel_radius"]);
            ���������.����������� = LoadWheels(directory1, element3["wheels"]);
            ���������.������Dir = directory1 + element3["shtangi_dir"].InnerText;
            ���������.������Filename = element3["shtangi_filename"].InnerText;
            ���������.����������������� = Xml.GetDouble(element3["shtangi_full_length"]);
            ���������.����������Min = Xml.GetDouble(element3["shtangi_angle_min"]);
            ���������.������ = LoadShtangi(element3["shtangi"]);
            ���������.���� = LoadSteering(directory1, element3["steering"]);
            ���������.�� = LoadStandaloneMotion(element3["standalone_motion"]);
            ���������.�������� = Load_VPark_Tabl(element3["tabl_v_park"]);
            ���������.�����Pos = LoadDouble_3DPoint(element3["narad_pos"]);
            ���������.cameras = LoadCameras(element3["cameras"]);
            try//for tests
            {
                //���������.bbox = LoadDouble_3DPoints(element3["bounding_box"]);
                //���������.tails_bbox = LoadArrayOfDouble_3DPoints(element3["tails_bounding_box"]);
                ���������.bsphere = LoadBSphere(element3["bounding_sphere"]);
                ���������.tails_bsphere = LoadBSpheres(element3["tails_bounding_sphere"]);
            }
            catch
            {
                ���������.hasnt_bbox = true;
                Logger.Log("ModelLoader", "Bounding spheres not loaded for " + ���������.name);
            }
            ���������.���������������� = LoadDoublePoints(element3["occupied_locations"]);
            ���������.����������������������� = LoadArrayOfDoublePoints(element3["tails_occupied_locations"]);
            ���������.����������������� = element3["control_system"].InnerText;
        }

        private static void LoadTramway(���������������� �������, string directory1, XmlNode element3)
        {
            �������.name = element3["name"].InnerText;
            �������.dir = directory1 + element3["dir"].InnerText;
            �������.filename = element3["filename"].InnerText;
            //			�������.����������������� = (int)Xml.GetDouble(element3["tails_count"]);
            �������.tails = LoadTails(element3["tails"]);
            try
            {
                //			�������.�������������������� = (int)Xml.GetDouble(element3["middles_count"]);
                �������.���������� = LoadNewMiddles(element3["middles"]);
            }
            catch { };
            �������.���������� = LoadAdditions(element3["additions"]);
            �������.���������������� = (int)Xml.GetDouble(element3["door_count"]);
            �������.����� = LoadDoors(�������, element3["doors"]);
            �������.axisfilename = element3["axis_filename"].InnerText;
            �������.telegafilename = element3["telegi_filename"].InnerText;
            �������.����������_�����_����� = (double)Xml.GetDouble(element3["axis_dist"]);
            �������.axis_radius = (double)Xml.GetDouble(element3["axis_radius"]);
            //������:
            //			�������.����������_�����_��������� = (double)Xml.GetDouble(element3["telegi_dist"]);
            �������.������� = LoadBoogies(element3["telegi"]);
            �������.��������� = LoadPantograph(directory1, element3["pantograph"]);
            /*�������.������Dir = directory1 + element3["shtangi_dir"].InnerText;
            �������.������Filename = element3["shtangi_filename"].InnerText;
            �������.����������������� = Xml.GetDouble(element3["shtangi_full_length"]);
            �������.����������Min = Xml.GetDouble(element3["shtangi_angle_min"]);
            �������.������ = LoadShtangi(element3["shtangi"]);*/
            �������.�������� = Load_VPark_Tabl(element3["tabl_v_park"]);
            �������.�����Pos = LoadDouble_3DPoint(element3["narad_pos"]);
            �������.cameras = LoadCameras(element3["cameras"]);
            try//for tests
            {
                //�������.bbox = LoadDouble_3DPoints(element3["bounding_box"]);
                //�������.tails_bbox = LoadArrayOfDouble_3DPoints(element3["tails_bounding_box"]);
                �������.bsphere = LoadBSphere(element3["bounding_sphere"]);
                �������.tails_bsphere = LoadBSpheres(element3["tails_bounding_sphere"]);
            }
            catch
            {
                �������.hasnt_bbox = true;
                Logger.Log("ModelLoader", "Bounding spheres not loaded for " + �������.name);
            }
            �������.���������������� = LoadDoublePoints(element3["occupied_locations"]);
            �������.����������������������� = LoadArrayOfDoublePoints(element3["tails_occupied_locations"]);
            �������.����������������� = element3["control_system"].InnerText;
        }

        private static ����������������.���� LoadSteering(string directory, XmlNode node)
        {
            if (node != null)
            {
                var d = directory + node["dir"].InnerText;
                var f = node["filename"].InnerText;
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                var z = Xml.GetDouble(node["z"]);
                var a = Xml.GetDouble(node["angle"]);
                return new ����������������.����(d, f, x, y, z, a);
            }
            return null;
        }

        private static ����������������.�� LoadStandaloneMotion(XmlNode node)
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
                return new ����������������.��(z, a, h);
            }
            return null;
        }

        private static ����������������.����������[] LoadAdditions(XmlNode items)
        {
            var ����������Array = new ����������������.����������[items.ChildNodes.Count];
            for (var i = 0; i < ����������Array.Length; i++)
            {
                var node = items.ChildNodes[i];
                var num2 = (int)Xml.GetDouble(node["part"]);
                var innerText = node["filename"].InnerText;
                var num3 = (int)Xml.GetDouble(node["type"]);
                ����������Array[i] = new ����������������.����������(num2, innerText, (Transport.���_����������)num3);
            }
            return ����������Array;
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

        private static ����������������.�����[] LoadDoors(���������������� ����������, XmlNode items)
        {
            var �����Array = new ����������������.�����[items.ChildNodes.Count];
            for (var i = 0; i < �����Array.Length; i++)
            {
                var node = items.ChildNodes[i];
                var index = (int)Xml.GetDouble(node["model"]);
                var ����� = ����������.������������[index];
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
                �����Array[i] = new ����������������.�����(�����, num2, num3, num7, num4, num8, num5, num6, flag, flag2, num9);
            }
            return �����Array;
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

        private static ����������������.������[] LoadShtangi(XmlNode items)
        {
            var ������Array = new ����������������.������[items.ChildNodes.Count];
            for (var i = 0; i < ������Array.Length; i++)
            {
                var node = items.ChildNodes[i];
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                var z = Xml.GetDouble(node["z"]);
                ������Array[i] = new ����������������.������(x, y, z);
            }
            return ������Array;
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

        private static ����������������.�����������[] LoadWheels(string directory, XmlNode items)
        {
            var ����Array = new ����������������.�����������[items.ChildNodes.Count];
            for (var i = 0; i < ����Array.Length; i++)
            {
                var node = items.ChildNodes[i];
                var innerText = directory + node["dir"].InnerText;
                var filename = node["filename"].InnerText;
                var num2 = (int)Xml.GetDouble(node["part"]);
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                ����Array[i] = new ����������������.�����������(innerText, filename, num2, x, y);
            }
            return ����Array;
        }

        private static ����������������.�����[] LoadTails(XmlNode items)
        {
            var tailsArray = new ����������������.�����[items.ChildNodes.Count];
            for (var i = 0; i < tailsArray.Length; i++)
            {
                XmlNode node = items.ChildNodes[i];
                double dist = (double)Xml.GetDouble(node["distance"]);
                string filename = node["filename"].InnerText;
                //            	var flag = Xml.GetDouble(node["have_middle"]) != 0.0;
                //            	string mfilename = node["middle_filename"].InnerText;
                //            	double ad = (double)Xml.GetDouble(node["axis_dist"]);
                //            	double td = (double)Xml.GetDouble(node["telegi_dist"]);
                tailsArray[i] = new ����������������.�����(dist, filename);//, td);//mfilename, flag,
            }
            return tailsArray;
        }

        private static ����������������.����������_new[] LoadNewMiddles(XmlNode items)
        {
            var middlesArray = new ����������������.����������_new[items.ChildNodes.Count];
            for (var i = 0; i < middlesArray.Length; i++)
            {
                XmlNode node = items.ChildNodes[i];
                int ind = (int)Xml.GetDouble(node["index"]);
                int tar = (int)Xml.GetDouble(node["target"]);
                double dist = (double)Xml.GetDouble(node["distance"]);
                string filename = node["filename"].InnerText;
                //            	var flag = Xml.GetDouble(node["reverse"]) != 0.0;
                middlesArray[i] = new ����������������.����������_new(dist, filename, ind, tar);//, flag);
            }
            return middlesArray;
        }

        /*private static ����������������.��������� LoadPantograph(string directory, XmlNode node)
        {
        	����������������.���������._Type t;
        	try
        	{
        		t = (����������������.���������._Type)((int)Xml.GetDouble(node["type"]));
        	}
        	catch
        	{
        		t = (����������������.���������._Type)0;
        	}
            var d = directory + node["dir"].InnerText;
            var f = node["osn_filename"].InnerText;
            var pr = node["part_filename"].InnerText;
            var pn = node["duga_filename"].InnerText;
            var x = Xml.GetDouble(node["x"]);
            var y = Xml.GetDouble(node["y"]);
            var z = Xml.GetDouble(node["z"]);
        	return new ����������������.���������(t, d, f, pr, pn, x, y, z);
        }*/

        private static ����������������.��������� LoadPantograph(string directory, XmlNode node)
        {
            var d = directory + node["dir"].InnerText;
            var x = Xml.GetDouble(node["x"]);
            var y = Xml.GetDouble(node["y"]);
            var z = Xml.GetDouble(node["z"]);
            var min_h = Xml.GetDouble(node["min_height"]);
            var max_h = Xml.GetDouble(node["max_height"]);
            var dist = Math.Abs(Xml.GetDouble(node["dist"]));
            return new ����������������.���������(d, x, y, z, min_h, max_h, dist, LoadPantographsParts(node["parts"]));
        }

        private static ����������������.�����_����������[] LoadPantographsParts(XmlNode node)
        {
            var partsArray = new ����������������.�����_����������[node.ChildNodes.Count];
            for (var i = 0; i < partsArray.Length; i++)
            {
                XmlNode node1 = node.ChildNodes[i];
                //            	int ind = (int)Xml.GetDouble(node1["index"]);
                var height = Xml.GetDouble(node1["height"]);
                string filename = node1["filename"].InnerText;
                var width = Xml.GetDouble(node1["width"]);
                var length = Xml.GetDouble(node1["length"]);
                var ang = Xml.GetDouble(node1["norm_angel"]);
                partsArray[i] = new ����������������.�����_����������(filename, height, width, length, ang);
            }
            return partsArray;
        }

        private static ����������������.�������� Load_VPark_Tabl(XmlNode node)
        {
            if (node != null)
            {
                var f = node["filename"].InnerText;
                var x = Xml.GetDouble(node["x"]);
                var y = Xml.GetDouble(node["y"]);
                var z = Xml.GetDouble(node["z"]);
                return new ����������������.��������(f, x, y, z);
            }
            return null;
        }

        private static ����������������.SphereModel LoadBSphere(XmlNode node)
        {
            /*if (node != null)
        	{*/
            var r = Xml.GetDouble(node["r"]);
            var x = Xml.GetDouble(node["x"]);
            var y = Xml.GetDouble(node["y"]);
            var z = Xml.GetDouble(node["z"]);
            return new ����������������.SphereModel(r, x, y, z);
            /*}
        	return null;*/
        }

        private static ����������������.SphereModel[] LoadBSpheres(XmlNode node)
        {
            /*if (node != null)
        	{*/
            var spheres = new ����������������.SphereModel[node.ChildNodes.Count];
            for (int i = 0; i < spheres.Length; i++)
            {
                spheres[i] = LoadBSphere(node.ChildNodes[i]);
            }
            return spheres;
            /*}
        	return null;*/
        }

        private static ����������������.�������[] LoadBoogies(XmlNode node)
        {
            var boogieArray = new ����������������.�������[node.ChildNodes.Count];
            for (var i = 0; i < boogieArray.Length; i++)
            {
                XmlNode node1 = node.ChildNodes[i];
                int ind = (int)Xml.GetDouble(node1["index"]);
                string filename = Xml.GetString(node1["filename"]);
                double dist = Xml.GetDouble(node1["dist"]);
                boogieArray[i] = new ����������������.�������(ind, dist, filename);
            }
            return boogieArray;
        }

        private static ����������������.Camera[] LoadCameras(XmlNode node)
        {
            if (node != null)
            {
                var cameras = new ����������������.Camera[node.ChildNodes.Count];
                for (int i = 0; i < cameras.Length; i++)
                {
                    XmlNode node1 = node.ChildNodes[i];
                    var x = Xml.GetDouble(node1["x"]);
                    var y = Xml.GetDouble(node1["y"]);
                    var z = Xml.GetDouble(node1["z"]);
                    var rx = Xml.GetDouble(node1["rot_y"]);
                    var ry = Xml.GetDouble(node1["rot_z"]);
                    cameras[i] = new ����������������.Camera(x, y, z, rx, ry);
                }
                return cameras;
            }
            return null;
        }
    }
}