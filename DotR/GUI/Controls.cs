/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 29.12.2012
 * Time: 17:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
#if false
using System;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.DirectInput;
using System.Drawing;
using System.Collections.Generic;

namespace Engine
{
	//это был небольшой образец
	/*public class MyMenu// : MyControlPage
	{
		private MyControlPage current_page;
//		private TestTexture bg;
//		private TestTexture btn_bg;
		
		public MyMenu()
		{
			current_page = new MyControlPage();
			current_page.Add(new MyButton("Continue", 50, (MyDirect3D.Window_Height / 2) - 64, new EventHandler(Continuous)));
			current_page.Add(new MyButton("Exit", 50, (MyDirect3D.Window_Height / 2) + 32, new EventHandler(Exit)));
//			btn_bg = new TestTexture("buttons_background.png");
//			bg = new TestTexture("menu_background.png");
		}
		
		public void Draw()
		{
			MyDirect3D.Alpha = true;
			MyGUI.sprite.Begin(SpriteFlags.None);
//			MyGUI.DrawFSTexture(bg, 0, 0, false, new Color4(Color.FromArgb(0xff, Color.FromArgb(0xffffff))));
//			MyGUI.DrawCSTexture(btn_bg, 0, 0, 400, MyDirect3D.Window_Height, new Color4(Color.FromArgb(0xff, Color.FromArgb(0xffffff))));
			MyGUI.sprite.End();
			current_page.Draw();
		}
		
		public void Refresh()
		{
			if (MyDirectInput.Key_State[Key.UpArrow])
			{
				current_page.selectedpos--;
			}
			if (MyDirectInput.Key_State[Key.DownArrow])
			{
				current_page.selectedpos++;
			}
			current_page.selectedpos %= current_page.childs.Count;
			current_page.selectedpos = Math.Abs(current_page.selectedpos);
			current_page.Refresh();
		}
		
		private void Continuous(object sender, EventArgs e)
		{
			MyDirectInput.Key_State[Key.Escape] = true;
		}
		
		private void Exit(object sender, EventArgs e)
		{
			MyDirectInput.alt_f4 = true;
		}
	}*/
	
	public class MyButton : MyControl
	{
		public string text;
		public bool selected = false;
		private SlimDX.Direct3D9.Font font;
		private static Color4 selected_color = new Color4(Color.FromArgb(0xff, Color.FromArgb(0xffffff)));
		private static Color4 unselected_color = new Color4(Color.FromArgb(0xff, Color.FromArgb(0xacacac)));
		public event EventHandler Click;
		
		public MyButton(string _text, int _x, int _y, EventHandler _event)
		{
//			text = _text;
//			font = new SlimDX.Direct3D9.Font(MyDirect3D.device, new System.Drawing.Font("Verdana", 32f));
//			Left = _x;
//			Top = _y;
//			Click += _event;
		}
		
		public override void Refresh()
		{
			if ((selected) && (MyDirectInput.Key_State[Key.Return]))
			{
				Click(null, null);
			}
		}
		
		public override void Draw()
		{
			font.DrawString(null, text, Left, Top, selected ? selected_color : unselected_color);
		}
	}
	
	public class MyProgressBar : MyControl
	{
		public int MaxValue;
		public int Value;
		
		public MyProgressBar()
		{
			Value = 0;
			MaxValue = 1;
		}
		
		public override void Refresh()
		{
			throw new NotImplementedException();
		}
		
		public override void Draw()
		{
			
		}
	}
	
	#region Страница
	
	public class MyControlPage : MyControl
	{
		public List<MyControl> childs = new List<MyControl>();
		public int selectedpos = -1;
		
		public override void Draw()
		{
			foreach (var child in childs)
			{
				child.Draw();
			}
		}
		
		public override void Refresh()
		{
			for (int i = 0; i < childs.Count; i++)
			{
				childs[i].Refresh();
				if (childs[i] is MyButton)
				{
					((MyButton)childs[i]).selected = (selectedpos == i);
				}
			}
		}
		
		public void Add(MyControl child)
		{
			childs.Add(child);
			child.parent = this;
			if (selectedpos < 0) selectedpos = 0;
		}
	}
	#endregion
	
	#region Abstract...:
	
	public abstract class MyControl
	{
		public MyControl parent;
		
		public abstract void Refresh();
		
		public abstract void Draw();
		
		public int Width, Height, Left, Top;
		
	}
	#endregion
}
#endif