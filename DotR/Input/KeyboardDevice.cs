/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 03.02.2016
 * Time: 23:08
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Engine.Input
{
	/// <summary>
	/// KeyboardDevice - отвечает за инициализацию клавиатуры.
	/// Базовый класс?.
	/// </summary>
	public abstract class KeyboardDevice : IInputDevice
	{
		//TODO: KeyboardDevice - см список:
		//абстрактный класс, в т.ч. с шаблоном конструктора (никаких статиков, как в рендере!)
		//обязательные методы IsPressed, IsReleased	<<-- вынести в отдельный абстрактный класс?	
		
		public KeyboardDevice()
		{
		}
		
		public abstract void RefreshState();
		
		public abstract bool IsKeyPressed(int keyCode);
		
		public abstract bool IsKeyReleased(int keyCode);
	}
}
