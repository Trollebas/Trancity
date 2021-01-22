/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 10.01.2016
 * Time: 15:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Engine.Sound
{
	/// <summary>
	/// SoundDevice.
	/// </summary>
	public abstract class SoundDevice : IDisposable
	{
		internal SoundDevice()
		{
		}
		
		public abstract void Dispose();
		
		public abstract float MasterVolume { get; set; }
		
		public abstract void UpdateListner(ref Double3DPoint position, ref DoublePoint rotation);
		
		public abstract ISound3D CreateEmitter(float distance, string filename);
		
		#region Static device creation
		
		public static SoundDevice CreateDevice(SoundDeviceType deviceType)
		{
			SoundDevice device = null;
			//...
			
			switch (deviceType)
			{
				case SoundDeviceType.XAudio2:
					#if XA2
						device = new XA2SoundDevice();
						break;
					#else
						throw new NotImplementedException("XAudio2 not included in this version!");
					#endif
					
				case SoundDeviceType.OpenAL:
					#if OAL
						device = new OALSoundDevice();
						break;
					#else
						throw new NotImplementedException("OpenGL не входит в эту версию!");
					#endif
				
				default:
					throw new NotSupportedException(string.Format("Указанный тип устройства {0} недоступен!", deviceType.ToString()));
			}
			
			//...
			
			return device;
		}
		
		#endregion
	}
	
	/// <summary>
	/// 
	/// </summary>
	public enum SoundDeviceType
	{
		/// <summary>
		/// Provided by SlimDX
		/// </summary>
		XAudio2,
		/// <summary>
		/// Provided by OpenTK
		/// <remarks>orly?</remarks>
		/// </summary>
		OpenAL
	}
}
