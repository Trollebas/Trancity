/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 07.02.2016
 * Time: 15:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Engine.Input
{
	/// <summary>
	/// IInputDevice
	/// </summary>
	public interface IInputDevice
	{
		void RefreshState();
		
		bool IsKeyPressed(int keyCode);
		
		bool IsKeyReleased(int keyCode);
	}
}
