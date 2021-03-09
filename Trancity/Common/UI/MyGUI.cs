/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 20.08.2012
 * Time: 15:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Common
{
    public class MyGUI
    {
        public static int load_max = 600;
        public static int load_status = -1;
        public static int progress_color = 0x34ea2;
        public static SlimDX.Direct3D9.Font default_font;
        public static SlimDX.Direct3D9.Font splash_font_big;
        public static SlimDX.Direct3D9.Font splash_font_status;
        public static TestTexture splash_tex;
        public static string splash_title = "";
        public static Sprite sprite;
        public static string status_string = "";
        public static string[] stringlist = new string[13];
        public static TestTexture white_tex;

        public static void Initialize()
        {
            default_font = new SlimDX.Direct3D9.Font(MyDirect3D.device, new System.Drawing.Font("Verdana", 16f));
            splash_font_big = new SlimDX.Direct3D9.Font(MyDirect3D.device, new System.Drawing.Font("Verdana", 80f, FontStyle.Bold));
            splash_font_status = new SlimDX.Direct3D9.Font(MyDirect3D.device, new System.Drawing.Font("Verdana", 30f));
            sprite = new Sprite(MyDirect3D.device);
            splash_tex = new TestTexture("splash.png");
            white_tex = new TestTexture("white.bmp");
        }

        private static void DrawSplash(Color4 color)
        {
            DrawFSTexture(splash_tex, 0, 0, color);
        }

        public static void Splash()
        {
            Splash(0xffffff);
        }

        public static void Splash(int color)
        {
            Color4 color1 = new Color4(color);
            MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
            MyDirect3D.device.BeginScene();
            sprite.Begin(SpriteFlags.None);
            DrawSplash(color1);
            Color color2 = Color.FromArgb(0xff, Color.FromArgb(progress_color));
            Color4 color3 = new Color4(color2);
            if (load_status >= 0)
            {
                int h = 5;
                int num2 = 30;
                int x = (MyDirect3D.Window_Width / 2) - (load_max / 2);
                int y = MyDirect3D.Window_Height - 100;
                DrawCSTexture(white_tex, x - h, y - h, (2 * h) + load_max, h, color3);
                DrawCSTexture(white_tex, x - h, y + num2, (2 * h) + load_max, h, color3);
                DrawCSTexture(white_tex, x - h, y, h, num2, color3);
                DrawCSTexture(white_tex, x + load_max, y, h, num2, color3);
                if (load_status > 0)
                {
                    DrawCSTexture(white_tex, x, y, load_status, num2, color3);
                }
            }
            sprite.End();
            splash_font_big.DrawString(null, splash_title, new Rectangle(0, 0, MyDirect3D.Window_Width, 300), DrawTextFormat.VerticalCenter | DrawTextFormat.Center, Color.Black);
            splash_font_status.DrawString(null, status_string, new Rectangle(0, MyDirect3D.Window_Height - 300, MyDirect3D.Window_Width, 250), DrawTextFormat.VerticalCenter | DrawTextFormat.Center, color2);
            MyDirect3D.device.EndScene();
            MyDirect3D.device.Present();
        }

        /// <summary>
        /// Rendering custom-size texture
        /// </summary>
        public static void DrawCSTexture(TestTexture uitex, int x, int y, int w, int h, Color4 color)
        {
            sprite.Draw(uitex.texture, new Rectangle(0, 0, w, h), null, new Vector3(x, y, 0), color);
        }

        /// <summary>
        /// Rendering scaled texture (doesnt work)
        /// </summary>
        public static void DrawFSTexture(TestTexture uitex, int x, int y, Color4 color)
        {
            var proper = (float)uitex.size.Height / (float)uitex.size.Width;
            // TODO: найти ошибку
            var scale = ((float)uitex.size.Height / 768.0f) * Math.Max(((float)(MyDirect3D.Window_Height - y)) / (float)uitex.size.Height, ((float)(MyDirect3D.Window_Width - x)) / (float)uitex.size.Width);
            sprite.Transform = Matrix.Scaling(scale, scale * proper, 1);
            sprite.Draw(uitex.texture, null, new Vector3(x, y, 0), color);
            sprite.Transform = Matrix.Identity;
        }

        public static List<Control> FindControl(Control ctrl, string target)
        {
            List<Control> list = new List<Control>();
            if (ctrl.Name == target)
            {
                list.Add(ctrl);
            }
            foreach (Control control in ctrl.Controls)
            {
                list.AddRange(FindControl(control, target));
            }
            return list;
        }

        public static List<MenuItem> FindMenuItems(Menu menu, string target)
        {
            List<MenuItem> list = new List<MenuItem>();
            foreach (MenuItem subitem in menu.MenuItems)
            {
                list.AddRange(FindMenuItemsPr(subitem, target));
            }
            return list;
        }

        private static List<MenuItem> FindMenuItemsPr(MenuItem menu, string target)
        {
            List<MenuItem> list = new List<MenuItem>();
            if (menu.Name == target)
            {
                list.Add(menu);
            }
            foreach (MenuItem subitem in menu.MenuItems)
            {
                list.AddRange(FindMenuItemsPr(subitem, target));
            }
            return list;
        }

        private void Exit(object sender, EventArgs e)
        {
            MyDirectInput.Key_State[Key.Escape] = true;
            MyDirectInput.alt_f4 = true;
        }
    }
}
