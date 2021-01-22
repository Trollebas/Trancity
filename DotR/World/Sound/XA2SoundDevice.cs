/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 18.08.2015
 * Time: 15:14
 * 
 * This file is part of ODE_Test project.
 * 
 */
#if XA2
using System;
using System.IO;
using System.Collections.Generic;
using SlimDX;
using SlimDX.Multimedia;
using SlimDX.XAudio2;
using SlimDX.X3DAudio;

namespace Engine.Sound
{
	/// <summary>
	/// Base overlay for XAudio2/X3DAudio features
	/// </summary>
	public class XA2SoundDevice : SoundDevice, IRawDevice<XAudio2>//, IEmiUpdater<Emitter>
	{
		private XAudio2 _xAudioDevice;
		private X3DAudio _x3DAudioDevice;
		private MasteringVoice _masteringVoice;
		private Listener _listner;
		// for listner update
		private Double3DPoint _lastListnerPos;
		private int _lastTickCount = 0;
		// saved from SoundManager
		private Dictionary<string, WaveStream> buffers;
		
		internal XA2SoundDevice() : base()
		{
			_xAudioDevice = new XAudio2(XAudio2Flags.None, ProcessorSpecifier.DefaultProcessor);
			_x3DAudioDevice = new X3DAudio(Speakers.Stereo, 343.0f);
			_masteringVoice = new MasteringVoice(_xAudioDevice, 2);
			_xAudioDevice.StartEngine();
			Logger.Log("Engine", "Устройство XAudio2 успешно создан");
			Logger.Log("XAudio2", "Доступное устройство:");
			DeviceDetails details;
			int deviceCount = _xAudioDevice.DeviceCount;
			for (int i = 0; i < deviceCount; i++)
			{
				details = _xAudioDevice.GetDeviceDetails(i);
				Logger.Log(i.ToString(), string.Format("{0} с ролью {1}", details.DisplayName, details.Role.ToString()));
			}
			// и это тоже прибито гвоздями:
			Logger.Log("XAudio2", string.Format("Использует устройство с идентификатором = {0}", 0));
			// listner пока жёстко прибит
			_listner = new Listener();
			// его характеристики тоже
			_listner.OrientTop = Vector3.UnitY;
			var cone = new Cone();
			cone.InnerAngle = (float)(0.8 * Math.PI);
			cone.OuterAngle = (float)(1.8 * Math.PI);
			cone.InnerVolume = 1f;
			cone.OuterVolume = 0.75f;
			cone.InnerLpf = 0.0f;
			cone.OuterLpf = 0.45f;
			cone.InnerReverb = 0.7f;
			cone.OuterReverb = 1f;
			_listner.Cone = cone;
			//
			buffers = new Dictionary<string, WaveStream>();
		}
		
		public override void Dispose()
		{
			if (_xAudioDevice != null)
			{
				_xAudioDevice.Dispose();
			}
			if (_x3DAudioDevice != null)
			{
				_x3DAudioDevice.Dispose();
			}
		}
		
		public override void UpdateListner(ref Double3DPoint position, ref DoublePoint rotation)
		{
			int ticks = Environment.TickCount;
			int dt = 0;
			unchecked
			{
				dt = ticks - _lastTickCount;
			}
			// find velocity
			_lastListnerPos.Subtract(ref position);
			if (dt > 0)
				_lastListnerPos.Divide(dt);
			// and apply all
			// TODO: convert D3DPoint to Vector3!
			_listner.Velocity = new Vector3((float)Math.Abs(_lastListnerPos.x), (float)Math.Abs(_lastListnerPos.y), (float)Math.Abs(_lastListnerPos.z));
			_listner.Position = new Vector3((float)position.x, (float)position.y, (float)position.z);
			_listner.OrientFront = new Vector3((float)Math.Cos(rotation.x), 0f/*(float)Math.Sin(rotation.y)*/, (float)Math.Sin(rotation.x));
			// save prev values
			position.CopyTo(ref _lastListnerPos);
			_lastTickCount = ticks;
		}
		
		public override ISound3D CreateEmitter(float distance, string filename)
		{
			return new XA2Sound3D(this, distance, filename);
		}
		
		#region Захардкоженный мусор!
		
		internal DspSettings GetActualDSP(Emitter emitter, int sourceChannelCount)
		{
			return _x3DAudioDevice.Calculate(_listner, emitter, CalculateFlags.Matrix | CalculateFlags.Doppler | CalculateFlags.LpfDirect, sourceChannelCount, MVInputChannels);
		}
		
		public MasteringVoice MasterVoice
		{
			get
			{
				return _masteringVoice;
			}
		}
		
		internal int MVInputChannels
		{
			get
			{
				return _masteringVoice.VoiceDetails.InputChannels;
			}
		}
		
		#endregion
		
		#region Ex-SoundManager
		
		// TODO: SoundManager - см. список:
		// оптимизация памяти
		// (см. примеры записи нескольких файлов в 
		// один буффер)
		internal AudioBuffer CreateBufferFromFile(string filename, out WaveFormat format)
		{
			AudioBuffer buffer;
			WaveStream stream = null;
			format = null;
			if ((!buffers.TryGetValue(filename, out stream)))
			{
				// ^there is no buffer in dictionary
				// now check if file is unaviable
				if (!File.Exists(filename))
				{
					Logger.Log("SoundManager", string.Format("Не удается загрузить звук {0}", filename));
					buffers.Add(filename, null);
					return null;
				}
				Logger.DebugLog("SoundManager", string.Format("Загружены {0}...", filename));
				stream = new WaveStream(filename);
				buffers.Add(filename, stream);
			}
			else if (stream == null)
			{
				return null;
			}
			stream.Position = 0;
			buffer = new AudioBuffer();
			buffer.AudioBytes = (int)stream.Length;
			buffer.AudioData = stream;
			buffer.Flags = BufferFlags.EndOfStream;
			format = stream.Format;
			return buffer;
		}
		
		public int BuffersCount
		{
			get
			{
				return buffers.Count;
			}
		}
		
		/// <summary>
		/// In KB
		/// </summary>
		public int MemoryUsage
		{
			get
			{
				double result = 0;
				var values = buffers.Values;
				foreach (var val in values)
				{
					if (val == null) continue;
					result += val.Length / 1024.0;
				}
				return (int)Math.Floor(result);
			}
		}
		
		#endregion
		
		public override float MasterVolume
		{
			get
			{
				return _masteringVoice.Volume;
			}
			set
			{
				_masteringVoice.Volume = value;
			}
		}
		
		public XAudio2 RawDevice
		{
			get
			{
				return _xAudioDevice;
			}
		}
	}
}
#endif