/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 23.06.2012
 * Time: 23:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using SlimDX;
using Trancity;
using System.IO;

namespace SlimDX
{
	public class MyMatrix
	{
		private static Matrix _matrix = new Matrix();
		
		public static Matrix Zero
		{
			get
			{
				return _matrix;
			}
		}
	}
}

namespace Common
{
	public class MyFeatures
	{
		public const double halfPI = Math.PI / 2.0;
		
		public static bool ByteEquals(byte[] a, byte[] b)
		{
			if (a.Length != b.Length) return false;
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i] != b[i]) return false;
			}
			return true;
		}
		
		public static double Lerp(double a, double b, double t)
        {
        	return a - (a * t) + (b * t);
        }
		
		public static int[] GetPos(ref DoublePoint pos)
		{
			var _pos = new int[2];
			_pos[0] = (int)Math.Floor((pos.x + Ground.grid_size / 2.0) / Ground.grid_size);
			_pos[1] = (int)Math.Floor((pos.y + Ground.grid_size / 2.0) / Ground.grid_size);
			pos.x -= _pos[0] * Ground.grid_size;
			pos.y -= _pos[1] * Ground.grid_size;
			return _pos;
		}
		
		public static void CheckFolders(string startup_path)
		{
			TryToCreateFolder(startup_path + @"\Cities\");
			TryToCreateFolder(startup_path + @"\Data\Splines\");
			TryToCreateFolder(startup_path + @"\Data\Skybox\");
			TryToCreateFolder(startup_path + @"\Data\Localization\");
			TryToCreateFolder(startup_path + @"\Data\Transport\");
			TryToCreateFolder(startup_path + @"\Data\Objects\");
			TryToCreateFolder(startup_path + @"\Screenshots\");
		}
		
		private static void TryToCreateFolder(string path)
		{
			if (!Directory.Exists(path))
            {
            	try { Directory.CreateDirectory(path);
            	}
				catch (Exception e) {
					Logging.WriteExc(e);
            		return;
            	}
            }
		}
	}
}