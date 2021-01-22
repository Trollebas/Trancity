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
using System.Collections.Generic;
using System.Windows.Forms;
using SlimDX.Direct3D9;
using System.Drawing;
using Engine;

namespace SlimDX
{
    public class MyMatrix
    {
        public static readonly Matrix Zero = new Matrix();
    }
}

namespace Common
{
    public class MyFeatures
    {
        public static double Lerp(double a, double b, double t)
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
                    Engine.Logger.LogException(e);
                    return;
                }
            }
        }
        
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
//            var surface = MyDirect3D.device.GetRenderTarget(0);
//            var surface = MyDirect3D.device.GetBackBuffer(0, 0);
            Surface surface = null;
            MyDirect3D.device.GetFrontBufferData(0, surface);
            Surface.ToFile(surface, screenshot, ImageFileFormat.Png);
            surface.Dispose();
        }
        
        //
        
        public static Vector3 ToVector3(Double3DPoint a)
        {
            return new Vector3((float)a.x, (float)a.y, (float)a.z);
        }
        
        public static Double3DPoint ToDouble3DPoint(Vector3 a)
        {
            return new Double3DPoint(a.X, a.Y, a.Z);
        }
    }
}