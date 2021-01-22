/*
 * Сделано в SharpDevelop.
 * Пользователь: serg
 * Дата: 14.03.2012
 * Время: 20:00
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
//using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
//using System.Windows.Forms;
//using SlimDX.Direct3D9;
using SlimDX.Multimedia;
using Trancity;

namespace Common
{
	public class SoundLoader : StreamWork// : IDisposable// : ResourceManager
	{
		public WaveFormat Format = null;
		public byte[] OutBytes = null;
//		private WaveStream str = null;
		private static ArrayList _soundStructs = new ArrayList();
		
		public SoundLoader(string filename)
		{
			str = new WaveStream(filename);
			/*var bytes = new List<byte>();
			int rindex;
			do
			{
				rindex = str.ReadByte();
				if (rindex != -1) bytes.Add((byte)rindex);
			}
			while (rindex != -1);*/
			OutBytes = base.LoadData(filename);//bytes.ToArray();
			Format = ((WaveStream)str).Format;
			foreach (SoundStruct strct in _soundStructs)
			{
				if (filename != strct.filename) continue;
				OutBytes = null;
				OutBytes = (byte[])strct.bytes.Clone();
				Format = strct.Format;
				goto label_slend;
			}
			_soundStructs.Add(new SoundStruct { filename = filename, bytes = OutBytes, Format = Format });
			label_slend:;
			this.Dispose();
		}
		
		/*public virtual void Dispose()
		{
			if (str != null)
			{
				str.Close();
				str.Dispose();
			}
		}*/
		
		[StructLayout(LayoutKind.Sequential)]
        private struct SoundStruct
        {
            public string filename;
            public byte[] bytes;
            public WaveFormat Format;
        }
	}
}