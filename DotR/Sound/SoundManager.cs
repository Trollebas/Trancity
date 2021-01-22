/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 27.08.2015
 * Time: 17:02
 * 
 * This file is part of ODE_Test project.
 * 
 */
#if false
using System;
using System.IO;
using System.Collections.Generic;
using SlimDX.XAudio2;
using SlimDX.Multimedia;

namespace Engine.Sound
{
	/// <summary>
	/// SoundManager - хранит ссылки на WaveBuffer'ы
	/// </summary>
	public class SoundManager
	{
		private static Dictionary<string, WaveStream> buffers = new Dictionary<string, WaveStream>();
		
		// TODO: SoundManager - см. список:
		// оптимизация памяти
		// (см. примеры записи нескольких файлов в 
		// один буффер)
		public static AudioBuffer CreateBufferFromFile(string filename)
		{
			AudioBuffer buffer;
			WaveStream stream = null;
			if ((!buffers.TryGetValue(filename, out stream)))
			{
				//^there is no buffer in dictionary
				//now check if file is unaviable
				if (!File.Exists(filename))
				{
					Logger.Log("SoundManager", string.Format("Can not load sound {0}", filename));
					buffers.Add(filename, null);
					return null;
				}
				stream = new WaveStream(filename);
				buffers.Add(filename, stream);
			}
			else if (stream == null)
			{
				return null;
			}
			buffer = new AudioBuffer();
			buffer.AudioBytes = (int)stream.Length;
			buffer.AudioData = stream;
			buffer.Flags = BufferFlags.EndOfStream;
			return buffer;
		}
		
		public static int BuffersCount
		{
			get
			{
				return buffers.Count;
			}
		}
		
		/// <summary>
		/// In KB
		/// </summary>
		public static int MemoryUsage
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
	}
}
#endif