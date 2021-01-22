/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 12.12.2012
 * Time: 18:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Engine;

namespace Trancity
{
	[StructLayout(LayoutKind.Sequential)]
	public class ObjectModel //TODO: объединить всё одним абстрактом, дабы не копипастить мусор
	{
		public string dir;
		public string name;
		public string filename;
		public SphereModel bsphere;
		public Dictionary<string, string> args;
		
		[StructLayout(LayoutKind.Sequential)]
        public class SphereModel
        {
           	public Double3DPoint pos;
           	public double radius;
            public SphereModel(double _radius, double x, double y, double z)
            {
                pos = new Double3DPoint(x, y, z);
                radius = _radius;
            }
        }
        
        public string FindStringArg(string key, string default_value)
        {
        	if (args.ContainsKey(key))
        	{
        		return args[key];
        	}
        	return default_value;
        }
        
        public int FindNumericArg(string key, int default_value)
        {
        	try
        	{
        		return Convert.ToInt32(FindStringArg(key, string.Empty));
        	}
        	catch
        	{
        		return default_value;
        	}
        }
	}
}