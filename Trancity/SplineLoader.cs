/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 09.12.2012
 * Time: 18:05
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
	public static class SplineLoader
    {
        public static readonly List<SplineModel> splines = new List<SplineModel>();
        
        static SplineLoader()
        {
            var document = new XmlDocument();
            var path = Application.StartupPath + @"\Data\Splines\";
            if (!Directory.Exists(path))
            {
            	Logger.Log("SplineLoader", "Directory " + path + " не найден!");
            	return;
            }
            foreach (var directory in Directory.GetDirectories(path))
            {
                var directory1 = directory+ @"\";
                try
                {
                    document.Load(directory1 + "spline.xml");
                }
                catch (Exception)
                {
                	Logger.Log("SplineLoader", "spline.xml не обнаружен в " + directory1);
                    continue;
                }
                var element = document["Trancity"];
                if (element == null) continue;
                var node = element["Spline"];
                if (node == null) continue;
                var model = new SplineModel();
                try
                {
                	model.dir = directory1 + node["dir"].InnerText;
                	model.name = node["name"].InnerText;
                	model.type = (int)Engine.Xml.GetDouble(node["type"]);
                    model.noscale = Engine.Xml.GetDouble(node["noscale"]) != 0.0;
                    model.length = Engine.Xml.GetDouble(node["length"]);
                    model.texture_filename = node["texture_filename"].InnerText;
                    model.points = LoadSplinePoints(node["points"]);
                    model.mesh_filename = node["mesh_filename"].InnerText;
                    splines.Add(model);
                }
                catch (Exception)
                {
                	Logger.Log("SplineLoader", "Ошибка в " + directory1 + "spline.xml");
                    continue;
                }
            }
        }
        
        private static Double3DPoint[] LoadSplinePoints(XmlNode items)
        {
            var pointArray = new Double3DPoint[items.ChildNodes.Count];
            for (var i = 0; i < pointArray.Length; i++)
            {
                pointArray[i] = LoadSplinePoint(items.ChildNodes[i]);
            }
            return pointArray;
        }
        
        private static Double3DPoint LoadSplinePoint(XmlNode items)
        {
            return new Double3DPoint(Engine.Xml.GetDouble(items["x"]), Engine.Xml.GetDouble(items["y"]), Engine.Xml.GetDouble(items["texv"]));
        }
    }
}