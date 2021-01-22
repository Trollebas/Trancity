/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 04.02.2016
 * Time: 0:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Engine.Input
{
	/// <summary>
	/// DInputKeyboard
	/// </summary>
	public class DInputKeyboard : KeyboardDevice
	{
		public DInputKeyboard()
		{
		}
		
		public override void RefreshState()
		{
			
		}
		
		public override bool IsKeyPressed(int keyCode)
		{
			return false;
		}
		
		public override bool IsKeyReleased(int keyCode)
		{
			return false;
		}
	}
}
