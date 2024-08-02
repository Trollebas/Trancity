namespace Common
{
	using System;
	using System.IO;
	using System.Text;
	using System.Collections;
	using System.Collections.Generic;
	//    using System.Runtime.InteropServices;

	public class Ini : StreamWork // : IDisposable
	{
		/*public static string Read(string filename, string section, string key, string default_value)
        {
            charbuffer charbuffer = new charbuffer();
            Win32_GetString(section, key, default_value, ref charbuffer, 0x100, filename);
            return charbuffer.ToString();
        }

        public static bool ReadBool(string filename, string section, string key, bool default_value)
        {
            try
            {
                return bool.Parse(Read(filename, section, key, default_value ? "true" : "false"));
            }
            catch
            {
                return default_value;
            }
        }

        public static int ReadInt(string filename, string section, string key, int default_value)
        {
            return Win32_GetInt(section, key, default_value, filename);
        }
        
        public static double ReadDouble(string filename, string section, string key, double default_value)
        {
            charbuffer charbuffer = new charbuffer();
            Win32_GetString(section, key, default_value.ToString(), ref charbuffer, 0x100, filename);
            string s1 = charbuffer.ToString();
            return double.Parse((string)s1);
        }

        [DllImport("kernel32.dll", EntryPoint="GetPrivateProfileIntA")]
        private static extern int Win32_GetInt(string section, string key, int default_value, string filename);
        [DllImport("kernel32.dll", EntryPoint="GetPrivateProfileStringA")]
        private static extern ushort Win32_GetString(string section, string key, string default_value, ref charbuffer return_value, int num_char, string filename);
        [DllImport("kernel32.dll", EntryPoint="WritePrivateProfileStringA")]
        private static extern bool Win32_SetString(string section, string key, string str, string filename);
        public static bool Write(string filename, string section, string key, bool value)
        {
            return Win32_SetString(section, key, value ? "true" : "false", filename);
        }

        public static bool Write(string filename, string section, string key, int value)
        {
            return Win32_SetString(section, key, value.ToString(), filename);
        }

        public static bool Write(string filename, string section, string key, string value)
        {
            return Win32_SetString(section, key, value, filename);
        }*/
		
		///////////////////////////////////////////////////////////////////////////////////////////

		private string filename;
		private StreamWorkMode mode;
		//        private Stream str = null;
		private StreamReader strr = null;
		private StreamWriter strw = null;
		
		private string[] outstr;
		
		private string[] mpoints;
		private int[] mindexes;
		
		public Ini(string _filename, StreamWorkMode _mode)
		{
			this.filename = _filename;
			this.mode = _mode;
			if (_mode == StreamWorkMode.Read)
			{
				this.OpenFileToRead(_filename);
			}
			else
			{
				this.OpenFileToWrite(_filename);
			}
		}
		
		/*~Ini()
        {
        	this.Close();
        }*/
		
		//read section
		
		private void OpenFileToRead(string filename)
		{
			try
			{
				str = File.Open(filename, FileMode.Open);
				strr = new StreamReader(str, Encoding.Default);
			}
			catch (Exception e)
			{
//				throw new Exception(e.Message + "\nCouldn't load file: " + filename);
				Logging.WriteExc(e);
				return;
			}
			outstr = this.GetStringsFromFile(filename);
			this.GetManagedPoints();
		}
		
		public bool ReadBool(string section, string key, bool default_value)
		{
			try
			{
				return bool.Parse(this.Read(section, key, default_value ? "true" : "false"));
			}
			catch
			{
				return default_value;
			}
		}
		
		public double ReadDouble(string section, string key, double default_value)
		{
			try
			{
				return double.Parse(this.Read(section, key, default_value.ToString()));
			}
			catch
			{
				return default_value;
			}
		}
		
		public int ReadInt(string section, string key, int default_value)
		{
			try
			{
				return int.Parse(this.Read(section, key, default_value.ToString()));
			}
			catch
			{
				return default_value;
			}
		}
		
		public string Read(string section, string key, string default_value)
		{
			try {
				int ind = -1;
				string[] separator = { "=" };
				if (!this.UnderGroup(key, section, ref ind)) return default_value;
				var strings = outstr[ind].Split(separator, StringSplitOptions.None);
				return strings[strings.Length - 1];
			}
			catch {
				return default_value;
			}
		}
		
		private string[] GetStringsFromFile(string filename)
		{
			if (!CanRead) return null;
			var buffer = new List<string>();
			try
			{
				string instr;
				do
				{
					instr = strr.ReadLine();
					if (instr != null) buffer.Add(instr);
				}
				while (instr != null);
			}
			catch (IOException) { return null; }
			finally {
				if (strr != null) strr.Close();
				if (str != null) str.Close();
			}
			return buffer.ToArray();
		}
		
		private string GetAllFromFile(string filename)
		{
			if (!CanRead) return null;
			string buffer;
			try
			{
				buffer = strr.ReadToEnd();
			}
			catch (IOException) { return null; }
			finally {
				if (strr != null) strr.Close();
				if (str != null) str.Close();
			}
			return buffer;
		}
		
		private void GetManagedPoints()
		{
			var list = new List<string>();
			var list2 = new List<int>();
			for (var i = 0; i < outstr.Length; i++)
			{
				if (outstr[i][0] == '[') {
					list.Add(outstr[i].Trim(new char[] { '[', ']' }));
					list2.Add(i);
				}
			}
			this.mpoints = list.ToArray();
			this.mindexes = list2.ToArray();
		}
		
		private bool UnderGroup(string _string, string group, ref int string_index)
		{
			int index = -1;
			int index2 = -1;
			int index3 = -1;
			string[] separator = { "=" };
			string[] strings;
			for (int k = 0; k < mpoints.Length; k++)
			{
				if (mpoints[k].ToLower().Equals(group.ToLower()))
				{
					index2 = mindexes[k];
					index3 = k;
					break;
				}
			}
			if ((index2 == -1) || (index3 == -1))
			{
				string_index = -1;
				return false;
			}
			for (int i = index2; i < ((index3 != (mindexes.Length - 1)) ? mindexes[index3 + 1] : outstr.Length); i++)
			{
				strings = outstr[i].Split(separator, StringSplitOptions.None);
				if (_string.ToLower().Equals(strings[0].ToLower())) {
					index = i;
					break;
				}
			}
			if (index == -1)
			{
				string_index = -1;
				return false;
			}
			string_index = index;
			return ((index > index2) && (((index3 + 1) == mpoints.Length) || (mindexes[index3 + 1] > index)));
		}
		
		//--------------------------------------------------------------------------------
		//write section
		private void OpenFileToWrite(string filename)
		{
			try
			{
				str = File.Open(filename, FileMode.Create);
				strw = new StreamWriter(str, Encoding.Default);
			}
			catch (Exception e) { throw new Exception(e.Message + "\nCouldn't create file: " + filename); }
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
			List<string> list;
			if (outstr != null) list = new List<string>(outstr);
			else list = new List<string>();
			bool exist = false;
			if (mpoints != null)
			{
				for (int i = 0; i < mpoints.Length; i++)
				{
					exist = (mpoints[i] == section);
				}
			}
			if (!exist)
			{
				list.Add("[" + section + "]");
			}
			list.Add(key + "=" + value);
			outstr = list.ToArray();
			this.GetManagedPoints();
		}
		
		public void SimpleWrite(string value)
		{
			List<string> list;
			if (outstr != null) list = new List<string>(outstr);
			else list = new List<string>();
			list.Add(value);
			outstr = list.ToArray();
		}
		
		public override void Dispose()///Close()
		{
			if (strw != null)
			{
				try {
					foreach (var str2 in outstr)
					{
						strw.WriteLine(str2);
					}
				}
				catch (Exception e) { throw new Exception(e.Message + "\nCouldn't write to file: " + filename); }
				finally {
					strw.Close();
					strw.Dispose();
				}
			}
			if (strr != null) {
				strr.Close();
				strr.Dispose();
			}
			base.Dispose();
		}
		
		private bool CanWrite
		{
			get
			{
				return ((mode == StreamWorkMode.Write) && (strw != null));
			}
		}
		
		private bool CanRead
		{
			get
			{
				return ((mode == StreamWorkMode.Read) && (strr != null));
			}
		}
	}
}