/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 09.12.2012
 * Time: 18:03
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
	public class SplineModel
	{
		public string dir;
		public string name;
		public bool noscale;
		public double length;
		public string texture_filename;
		public Double3DPoint[] points = new Double3DPoint[0];
		public string mesh_filename;
		public int type;
	}
}