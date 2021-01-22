/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 20.08.2012
 * Time: 15:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#if false
using System;
using SlimDX;
using SlimDX.Direct3D9;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Engine
{
	public class MyGUI
	{
		public static int load_max = 600;
		public static int load_status = -1;
		public static int progress_color = 0x34ea2;
		public static SlimDX.Direct3D9.Font default_font;
		public static SlimDX.Direct3D9.Font splash_font_big;
		public static SlimDX.Direct3D9.Font splash_font_status;
		public static string splash_title = "";
		public static Sprite sprite;
		public static string status_string = "";
//		public static TestTexture white_tex;
//		public static TestTexture splash_tex;
		
		public static void Initialize()
		{
//			default_font = new SlimDX.Direct3D9.Font(MyDirect3D.device, new System.Drawing.Font("Verdana", 16f));
//			splash_font_big = new SlimDX.Direct3D9.Font(MyDirect3D.device, new System.Drawing.Font("Verdana", 80f, FontStyle.Bold));
//			splash_font_status = new SlimDX.Direct3D9.Font(MyDirect3D.device, new System.Drawing.Font("Verdana", 30f));
//			sprite = new Sprite(MyDirect3D.device);
//			splash_tex = new TestTexture("splash.png");
//			white_tex = new TestTexture("white.bmp");
		}
		
		private static void DrawSplash(Color4 color)
		{
//			sprite.Draw2D(splash_tex, new Rectangle(0, 0, Window_Height, Window_Height), new Rectangle(0, 0, Window_Width, Window_Height), new Point(0, 0), color);
			/*var proper = (float)splash_size.Height / (float)splash_size.Width;
			var scale = 0.8f * Math.Max((float)MyDirect3D.Window_Height / (float)splash_size.Height, (float)MyDirect3D.Window_Width / (float)splash_size.Width);
			sprite.Transform = Matrix.Scaling(scale, scale * proper, 1);// * Matrix.Identity;
			sprite.Draw(splash_tex.texture, color);
			sprite.Transform = Matrix.Identity;*/
//			DrawFSTexture(splash_tex, 0, 0, false, color);
		}
		
		public static void Splash()
		{
			Splash(0xffffff);
		}

		public static void Splash(int color)
		{
			/*Color4 color1 = new Color4(color);
			MyDirect3D.device.Clear(ClearFlags.ZBuffer | ClearFlags.Target, 0, 1f, 0);
			MyDirect3D.device.BeginScene();
			sprite.Begin(SpriteFlags.None);
//			sprite.Draw2D(splash_tex, Rectangle.Empty, new Rectangle(0, 0, Window_Width, Window_Height), new Point(0, 0), color);
			DrawSplash(color1);
			Color color2 = Color.FromArgb(0xff, Color.FromArgb(progress_color));
			Color4 color3 = new Color4(color2);
//			splash_font_big.DrawString(null, splash_title, new Rectangle(0, 0, Window_Width, 300), DrawTextFormat.VerticalCenter | DrawTextFormat.Center, Color.White);
//			splash_font_status.DrawString(null, status_string, new Rectangle(0, Window_Height - 300, Window_Width, 250), DrawTextFormat.VerticalCenter | DrawTextFormat.Center, color2);
			if (load_status >= 0)
			{
				int h = 5;
				int num2 = 30;
				int x = (MyDirect3D.Window_Width / 2) - (load_max / 2);
				int y = MyDirect3D.Window_Height - 100;
//				DrawCSTexture(white_tex, x - h, y - h, (2 * h) + load_max, h, color3);
//				DrawCSTexture(white_tex, x - h, y + num2, (2 * h) + load_max, h, color3);
//				DrawCSTexture(white_tex, x - h, y, h, num2, color3);
//				DrawCSTexture(white_tex, x + load_max, y, h, num2, color3);
//				if (load_status > 0)
//				{
//					DrawCSTexture(white_tex, x, y, load_status, num2, color3);
//				}
			}
			sprite.End();
			splash_font_big.DrawString(null, splash_title, new Rectangle(0, 0, MyDirect3D.Window_Width, 300), DrawTextFormat.VerticalCenter | DrawTextFormat.Center, Color.Black);
			splash_font_status.DrawString(null, status_string, new Rectangle(0, MyDirect3D.Window_Height - 300, MyDirect3D.Window_Width, 250), DrawTextFormat.VerticalCenter | DrawTextFormat.Center, color2);
			MyDirect3D.device.EndScene();
			MyDirect3D.device.Present();*/
		}
		
		/// <summary>
		/// Rendering custom-size texture
		/// </summary>
//		public static void DrawCSTexture(TestTexture uitex, int x, int y, int w, int h, Color4 color)
//		{
//			sprite.Draw(uitex.texture, new Rectangle(0, 0, w, h), null, new Vector3(x, y, 0), color);
//		}
		
		/// <summary>
		/// Rendering scaled texture (doesnt work)
		/// </summary>
//		public static void DrawFSTexture(TestTexture uitex, int x, int y, bool vhscale, Color4 color)
//		{
//			var proper = (float)uitex.size.Height / (float)uitex.size.Width;
//			// TODO: найти ошибку
//			var scale = ((float)uitex.size.Height / 768.0f) * Math.Max(((float)(MyDirect3D.Window_Height - y)) / (float)uitex.size.Height, ((float)(MyDirect3D.Window_Width - x)) / (float)uitex.size.Width);
//			//(vhscale ? (((float)(MyDirect3D.Window_Height - y)) / (float)uitex.size.Height) : Math.Max(((float)(MyDirect3D.Window_Height - y)) / (float)uitex.size.Height, ((float)(MyDirect3D.Window_Width - x)) / (float)uitex.size.Width));
//			sprite.Transform = Matrix.Scaling(scale, scale * proper, 1);
//			sprite.Draw(uitex.texture, null, new Vector3(x, y, 0), color);
//			sprite.Transform = Matrix.Identity;
//		}
		
		/*public static void OnLostDevice(object sender, EventArgs e)
		{
			//effect.OnLostDevice();
			if (MainForm.in_editor) return;
//			default_font.OnLostDevice();
			splash_font_big.OnLostDevice();
			splash_font_status.OnLostDevice();
		}*/
	}
}
#endif
