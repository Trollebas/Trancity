/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 14.02.2016
 * Time: 19:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using SlimDX.XAudio2;
using SlimDX.Multimedia;

namespace Engine.Sound
{
	/// <summary>
	/// XA2Sound2D.
	/// </summary>
	public class XA2Sound2D : ISound2D
	{
		private SourceVoice _voice;
		private WaveFormat _waveFormat;
		private AudioBuffer _buffer;
		
		// TODO: переместить сюда хранение AudioBuffer из Sound3D
		internal XA2Sound2D(SourceVoice voice, WaveFormat format, AudioBuffer buffer)
		{
			_voice = voice;
			_waveFormat = format;
			_buffer = buffer;
		}
		
		internal static XA2Sound2D FromFile(XA2SoundDevice device, string filename)
		{
			WaveFormat format;
			// Переместить создание буффера в девайс - done?
			var audioBuffer = device.CreateBufferFromFile(filename, out format);
			if (audioBuffer == null)
			{
				return null;
			}
			audioBuffer.LoopCount = XAudio2.LoopInfinite;
			// СорсВойс, видимо, туда же
			SourceVoice svoice = new SourceVoice(device.RawDevice, format, 0, 2f);
			svoice.SubmitSourceBuffer(audioBuffer);
			var sound2d = new XA2Sound2D(svoice, format, audioBuffer);
			return sound2d;
		}
		
		// Костыли-костылики
		internal SourceVoice Voice
		{
			get
			{
				return _voice;
			}
		}
		
		// Костыли-костылики vol. 2
		internal WaveFormat Format
		{
			get
			{
				return _waveFormat;
			}
		}
		
		#region ISound2D 
		
		public void Play()
		{
			_voice.Start();
		}
		
		public void Stop()
		{
			_voice.Stop();
		}
		
		public float Frequency
		{
			get
			{
				return _voice.FrequencyRatio;
			}
			set
			{
				_voice.FrequencyRatio = value;
			}
		}
		
		public float Volume
		{
			get
			{
				return _voice.Volume;
			}
			set
			{
				_voice.Volume = value;
			}
		}
		
		#endregion
	}
}
