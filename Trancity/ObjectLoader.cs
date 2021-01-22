/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 12.12.2012
 * Time: 18:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using Common;
using Engine;

namespace Trancity
{
	// TODO: обновить чтение XML
	public static class ObjectLoader
    {
		public static readonly List<ObjectModel>[] objects = new List<ObjectModel>[4];
        //0 - obj, 1 - stops, 2- traffic lights, 3 - signals
        static ObjectLoader()
        {
            var document = new XmlDocument();
            var path = Application.StartupPath + @"\Data\Objects\";
            for (int i = 0; i < objects.Length; i++)
            {
            	objects[i] = new List<ObjectModel>();
            }
            foreach (var directory in Directory.GetDirectories(path))
            {
                var directory1 = directory+ @"\";
                try
                {
                    document.Load(directory1 + "object.xml");
                }
                catch (Exception)
                {
                	Logger.Log("ObjectLoader", "object.xml not found in directory " + directory1);
                    continue;
                }
                var element = document["Trancity"];
                if (element == null) continue;
                var node = element["Object"];
                if (node == null) continue;
                var model = new ObjectModel();
                try
                {
                	model.dir = directory1 + node["dir"].InnerText;
                	model.name = node["name"].InnerText;
                	model.filename = node["filename"].InnerText;
                    model.bsphere = LoadBSphere(node["bounding_sphere"]);
                    int index = (int)Engine.Xml.GetDouble(node["type"]);
                    try
		        	{
		        		model.args = new Dictionary<string, string>();
		        		foreach (XmlNode child in node["args"].ChildNodes)
		        		{
		        			model.args.Add(child.LocalName, child.InnerText);
		        		}
		       		}
		        	catch { } ;
                    objects[index].Add(model);
                }
                catch (Exception)
                {
                	Logger.Log("ObjectLoader", "Error in " + directory1 + "object.xml");
                    continue;
                }
            }
        }
        
        private static void ParseArgs(XmlNode node, ref Dictionary<string, string> args)
        {
        	try
        	{
        		args = new Dictionary<string, string>();
        		foreach (XmlNode child in node.ChildNodes)
        		{
        			args.Add(child.LocalName, child.InnerText);
        		}
       		}
        	catch { } ;
        }
        
        private static ObjectModel.SphereModel LoadBSphere(XmlNode node)
        {
        	/*if (node != null)
        	{*/
                var r = Engine.Xml.GetDouble(node["r"]);
                var x = Engine.Xml.GetDouble(node["x"]);
                var y = Engine.Xml.GetDouble(node["y"]);
                var z = Engine.Xml.GetDouble(node["z"]);
                return new ObjectModel.SphereModel(r, x, y, z);
        	/*}
        	return null;*/
        }
        
        public static void FindModel(byte index, string name, ref ObjectModel model, ref string dir)
        {
        	try
        	{
        		model = null;
        		foreach (var _model in ObjectLoader.objects[index])
				{
					if (_model.name == name)
					{
						model = _model;
						break;
					}
				}
				dir = model.dir;
        	}
        	catch
        	{
        		Logger.Log("ObjectLoader", "Object " + name + " not found!");
//        		dir = string.Empty;
        	}
        }
    }
}