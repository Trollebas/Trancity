/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 17.10.2015
 * Time: 21:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using Engine.Controls;

namespace Engine
{
	/// <summary>
	/// RenderDevice - abstract class with dx9device-style structure
	/// </summary>
	public abstract class RenderDevice
	{
		// TODO: RenderDevice - см. список:
		// сделать абстракции на всевозможные методы устройства:
		// begin, present, getadaptermode(s), ...
		
		private DeviceOptions _deviceOptions;
		
		private int _fps;
		private int _frameCount;
		private int _lastTicks;
		
		internal RenderDevice(IInternalRenderControl control, DeviceOptions parameters)
		{
			_deviceOptions = parameters;
			
			//for our awesome fps counter, aga
			_fps = 0;
			_frameCount = 0;
			_lastTicks = Environment.TickCount;
		}
		
		#region some abstract/virtual funcs:
		
		public abstract bool IsDeviceLost { get; }
		
		public virtual void BeginScene()
		{
			//do nothing at the moment
		}
		
		public abstract void Clear(DeviceClearFlags flags, Color color);
		
		// TODO: check for some args avail here
		public virtual void EndScene()
		{
			// TODO: call FPS counter here
			ComputeFPS();
		}
		
		#endregion
		
		#region Вполне себе нормальные функции 
		
		public int FPS
		{
			get
			{
				// UNDONE: how bout thread-safe here?
				return _fps;
			}
		}
		
		#endregion
		
		#region Private methods
		
		private void ComputeFPS()
		{
			_frameCount++;
			int _currentTicks = Environment.TickCount;
			if (_currentTicks - _lastTicks > 1000)
			{
				_fps = _frameCount * 1000 / (_currentTicks - _lastTicks);
				_frameCount = 0;
				_lastTicks = _currentTicks;
			}
		}
		
		#endregion
	}
	
	public enum DeviceClearFlags
	{
		Target,
		ZBuffer,
		Stencil
	}
}
