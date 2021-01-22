/*
 * Сделано в SharpDevelop.
 * Пользователь: serg
 * Дата: 14.03.2012
 * Время: 20:00
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
#if XA2
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using SlimDX;
using SlimDX.XAudio2;
using SlimDX.X3DAudio;
using SlimDX.Multimedia;
using System.Windows.Forms;

namespace Engine.Sound
{
	public class XA2Sound3D : ISound3D
	{
		// TODO: Свой Emitter - см. список:
		// кеширование
		// в теории - независимсоть от хАудио, но не будет такого
		// обработка ошибок
		// больше кастомизируемых пар-ров (например, кривая затухания,
		// дистанция затухания и т.п.)
		// Идея: тут мб более одного источника на одном эмитторе!
		// ещё надо хранить аудиобуфферы
		// да и вэйвбуфферы: на их основе пилить аудиобуффер
		// с нужными настрройками и хранить его в эмиторе(?)
		private XA2Sound2D _soundVoice;
		private Emitter _emitter;
		private XA2SoundDevice _device;
		
		private Double3DPoint _lastPos;
		private int _lastTickCount = 0;
		
		internal XA2Sound3D(XA2SoundDevice device, float distance, string filename)
		{
			_device = device;
			_emitter = new Emitter();
			
			_emitter.ChannelCount = 1;
			_emitter.ChannelRadius = 1f;
			_emitter.InnerRadius = 1f;
			_emitter.InnerRadiusAngle = (float)(Math.PI / 4.0);
			var curve = new CurvePoint[2];
			curve[0] = new CurvePoint { Distance = 0f, DspSetting = 1f };
			curve[1] = new CurvePoint { Distance = 1f, DspSetting = 0f };
			_emitter.VolumeCurve = curve;
			_emitter.CurveDistance = distance;
			
			_emitter.OrientFront = Vector3.UnitX;
			_emitter.OrientTop = Vector3.UnitY;
			/*var cone2 = new Cone();
			cone2.InnerVolume = 1f;
			cone2.OuterVolume = 1f;
			cone2.InnerLpf = 1f;
			cone2.OuterLpf = 1f;
			cone2.InnerReverb = 1f;
			cone2.OuterReverb = 1f;
			_emitter.Cone = cone2;*/
			
			// loading sound
			_soundVoice = XA2Sound2D.FromFile(device, filename);
		}
		
		public void Update(ref Double3DPoint position/*, ref DoublePoint rotation*/)
		{
			if (_soundVoice == null)
				return;
			int ticks = Environment.TickCount;
			int dt = 0;
			unchecked
			{
				dt = ticks - _lastTickCount;
			}
			// find velocity
			_lastPos.Subtract(ref position);
			if (dt > 0)
				_lastPos.Divide(dt);
			// and apply all
			// TODO: convert D3DPoint to Vector3!
			_emitter.Velocity = new Vector3((float)Math.Abs(_lastPos.x), (float)Math.Abs(_lastPos.y), (float)Math.Abs(_lastPos.z));
			_emitter.Position = new Vector3((float)position.x, (float)position.y, (float)position.z);
//			_emitter.OrientFront = new Vector3((float)Math.Cos(rotation.x), 0f/*(float)Math.Sin(rotation.y)*/, (float)Math.Sin(rotation.x));
			var dsp = _device.GetActualDSP(_emitter, _soundVoice.Format.Channels);
			
			_soundVoice.Voice.SetOutputMatrix(_device.MasterVoice, _soundVoice.Format.Channels, _device.MVInputChannels, dsp.MatrixCoefficients);
			_soundVoice.Voice.FrequencyRatio = dsp.DopplerFactor;
			
			// save prev values
			position.CopyTo(ref _lastPos);
			_lastTickCount = ticks;
		}
		
		#region ISound2D
		
		public void Play()
		{
			if (_soundVoice != null)
				_soundVoice.Play();
		}
		
		public void Stop()
		{
			if (_soundVoice != null)
				_soundVoice.Stop();
		}
		
		public float Frequency
		{
			get
			{
				return (_soundVoice != null) ? _soundVoice.Frequency : 0.0f;
			}
			set
			{
				if (_soundVoice != null)
					_soundVoice.Frequency = value;
			}
		}
		
		public float Volume
		{
			get
			{
				return (_soundVoice != null) ? _soundVoice.Volume : 0.0f;
			}
			set
			{
				if (_soundVoice != null)
					_soundVoice.Volume = value;
			}
		}
		
		#endregion
	}
}
#endif