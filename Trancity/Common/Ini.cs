using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Common
{
	public class Ini : IDisposable
	{
		private string filename;
		
		public Ini(string _filename, StreamWorkMode _mode)
		{
			this.filename = _filename;
		}
		
		public bool ReadBool(string section, string key, bool default_value)
		{
			bool value;
			if (bool.TryParse(this.Read(section, key, default_value.ToString()), out value))
			{
				return value;
			}
			return default_value;
		}
		
		public double ReadDouble(string section, string key, double default_value)
		{
			double value;
			if (double.TryParse(this.Read(section, key, default_value.ToString()), out value))
			{
				return value;
			}
			return default_value;
		}
		
		public int ReadInt(string section, string key, int default_value)
		{
			int value;
			if (int.TryParse(this.Read(section, key, default_value.ToString()), out value))
			{
				return value;
			}
			return default_value;
		}
		
		public string Read(string section, string key, string default_value)
		{
			var temp_buffer = new StringBuilder(256);
			GetPrivateProfileStringA(section, key, default_value, temp_buffer, 255, filename);
			return temp_buffer.ToString();
		}
		
		public void Write(string section, string key, int value)
		{
			this.Write(section, key, value.ToString());
		}
		
		public void Write(string section, string key, bool value)
		{
			this.Write(section, key, value.ToString());
		}
		
		public void Write(string section, string key, string value)
		{
			WritePrivateProfileStringA(section, key, value, filename);
		}
		
		[DllImport("kernel32.dll", EntryPoint="GetPrivateProfileIntA")]
        private static extern int GetPrivateProfileIntA(string section, string key, int default_value, string filename);
		
		[DllImport("kernel32.dll", EntryPoint="GetPrivateProfileStringA")]
        private static extern ushort GetPrivateProfileStringA(string section, string key, string default_value, StringBuilder buffer, int char_count, string filename);
        
        [DllImport("kernel32.dll", EntryPoint="WritePrivateProfileStringA")]
        private static extern bool WritePrivateProfileStringA(string section, string key, string value, string filename);
		
        public void Dispose()
		{
			//hmm, wat to do?
		}
	}
}