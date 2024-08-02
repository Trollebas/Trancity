/*
 * Сделано в SharpDevelop.
 * Пользователь: serg
 * Дата: 18.02.2012
 * Время: 22:05
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows.Forms;
using Trancity;

namespace Common
{
	public class Logging
	{
		private const string logfile = "Trancity.log";
		private const string logfile_editor = "Transedit.log";
		private static string filename;
//		private static string sectionname;
		private static Ini stream;
		
		public static void Init()
		{
			try
			{
				filename = Application.StartupPath + @"\" + (MainForm.in_editor ? logfile_editor : logfile);
				stream = new Ini(filename, StreamWorkMode.Write);
				SimpleWrite(Application.ProductName + " v" + Application.ProductVersion);
				SimpleWrite("Executable path: " + Application.StartupPath);
				SimpleWrite("Date: " + DateTime.Now.ToShortDateString());
				Write("Log started");
			}
			catch
			{
				MessageBox.Show("Cannot start logging!");
				stream = null;
			}
		}
		
		public static void End()
		{
			if (stream != null)
			{
				Write("Log finished");
				stream.Dispose();
				stream = null;
			}
		}
		
		public static void WriteExc(Exception exception)
		{
			Write(exception.GetType().ToString(), exception.ToString());
		}
		
		public static void WriteExc(Exception exception, string str)
		{
			Write(str, "\n" + exception.ToString());
		}
		
		public static void Write(string value)
		{
			Write("", value);
		}
		
		public static void Write(string sname, string msg)
		{
			if (stream == null) return;
			var now = DateTime.Now.ToLongTimeString();
            stream.SimpleWrite(now + ": " + sname + " - " + msg);
//            Ini.Write(filename, sectionname, sname + str, msg);
		}
		
		public static void SimpleWrite(string value)
		{
			if (stream == null) return;
			stream.SimpleWrite(value);
		}
	}
}