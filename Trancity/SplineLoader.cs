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

namespace Trancity
{
	public static class SplineLoader
    {
        public static readonly List<SplineModel> splines = new List<SplineModel>();
        
        static SplineLoader()
        {
            var document = new XmlDocument();
            Xml.document = document;
            var path = Application.StartupPath + @"\Data\Splines\";
            if (!Directory.Exists(path))
            {
            	Logging.Write("SplineLoader", "Directory " + path + " not found!");
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
                	Logging.Write("SplineLoader", "spline.xml not found in directory " + directory1);
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
                	model.type = (int)Xml.GetDouble(node["type"]);
                    model.noscale = Xml.GetDouble(node["noscale"]) != 0.0;
                    model.length = Xml.GetDouble(node["length"]);
                    model.texture_filename = node["texture_filename"].InnerText;
                    model.points = LoadSplinePoints(node["points"]);
                    model.mesh_filename = node["mesh_filename"].InnerText;
                    splines.Add(model);
                }
                catch (Exception)
                {
                	Logging.Write("SplineLoader", "Error in " + directory1 + "spline.xml");
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
            return new Double3DPoint(Xml.GetDouble(items["x"]), Xml.GetDouble(items["y"]), Xml.GetDouble(items["texv"]));
        }
    }
}