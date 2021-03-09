/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 23.06.2012
 * Time: 23:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Reflection;

namespace Engine
{
    public class Utilities
    {
        public readonly static Random Random = new Random();



        /// <summary>
        /// http://www.cyberforum.ru/post6102903.html
        /// </summary>
        public static DateTime GetBuildDate(Assembly assembly)
        {
            var version = assembly.GetName().Version;
            return new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
        }

        /*public static double Lerp(double a, double b, double t)
        {
        	//return a - (a * t) + (b * t);
        	return a + t * (b - a);
        }
		
		public static int[] GetPos(ref DoublePoint pos)
		{
			var _pos = new int[2];
			_pos[0] = (int)Math.Floor((pos.x + Ground.grid_size / 2.0) / Ground.grid_size);
			_pos[1] = (int)Math.Floor((pos.y + Ground.grid_size / 2.0) / Ground.grid_size);
			pos.x -= _pos[0] * Ground.grid_size;
			pos.y -= _pos[1] * Ground.grid_size;
			return _pos;
		}*/
        /*
		//screenshots...
		private static bool screenshot_requested = false;
		
		public static void MakeScreenshot(bool request)
		{
			if (!screenshot_requested)
			{
				screenshot_requested = request;
				return;
			}
			screenshot_requested = request;
			var now = DateTime.Now;
            var path = Application.StartupPath + @"\Screenshots\";
            var screenshot = string.Format(@"{0}\Trancity {1:00}-{2:00}-{3} {4:00}-{5:00}-{6:00}-{7:000}.png", path, now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond);
			Surface surface = null;
			MyDirect3D.device.GetFrontBufferData(0, surface);
            Surface.ToFile(surface, screenshot, ImageFileFormat.Png);
            surface.Dispose();
		}*/
    }
}