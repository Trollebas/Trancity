/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 01.12.2014
 * Time: 20:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using SlimDX;
using SlimDX.RawInput;

namespace Common
{
	/// <summary>
	/// Attempt to use RawInput
	/// </summary>
	public class MyRawInput
	{
//		private static KeyboardInfo[] Keyboards = null;
		private static KeyboardInfo Keyboard = null;
		private static MouseInfo Mouse = null;//мб несколько поддерживать?
		private static RawMouseState _mouseState = new RawMouseState();
		private static bool reset_mouse = false;
		
		public bool Initialize()
		{
			Device.KeyboardInput += new EventHandler<KeyboardInputEventArgs>(Device_KeyboardInput);
			Device.MouseInput += new EventHandler<MouseInputEventArgs>(Device_MouseInput);
			
			
			return true;
		}
		
		//events
		private void Device_KeyboardInput(object sender, KeyboardInputEventArgs args)
		{
			/*int index = -1;
			for (int i = 0; i < Keyboards.Length; i++)
			{
				if (args.Device != Keyboards[i].Handle) continue;
				index = i;
				break;
			}
			if (index < 0) return;*/
			if (args.Device != Keyboard.Handle) return;
//			args.
		}
		
		private void Device_MouseInput(object sender, MouseInputEventArgs args)
		{
			if (args.Device != Mouse.Handle) return;
			if (reset_mouse)//убрать быдлокод
			{
				for (int i = 0; i < 5; i++)
				{
					_mouseState.RawMoseButtons[i] = false;
				}
				reset_mouse = false;
			}
			_mouseState.X = args.X;
			_mouseState.Y = args.Y;
			if (args.ButtonFlags == MouseButtonFlags.None) return;
			switch (args.ButtonFlags)
			{
				case MouseButtonFlags.LeftDown:
					_mouseState.RawMoseButtons[0] = true;
					break;
				case MouseButtonFlags.RightDown:
					_mouseState.RawMoseButtons[1] = true;
					break;
				case MouseButtonFlags.MiddleDown:
					_mouseState.RawMoseButtons[2] = true;
					break;
				case MouseButtonFlags.Button4Down:
					_mouseState.RawMoseButtons[3] = true;
					break;
				case MouseButtonFlags.Button5Down:
					_mouseState.RawMoseButtons[4] = true;
					break;
				case MouseButtonFlags.MouseWheel:
					_mouseState.Z = args.WheelDelta;
					break;
			}
		}
		
		public RawMouseState GetMouseState()
		{
			reset_mouse = true;
			return _mouseState;
		}
	}
	
	//TODO: выпилить в отдельный файл
	public class RawMouseState
	{
		public bool[] RawMoseButtons = new bool[5];//left-right-middle-4-5
		public int X, Y, Z;
		
		public RawMouseState()
		{
			X = 0;
			Y = 0;
			Z = 0;
		}
	}
}
