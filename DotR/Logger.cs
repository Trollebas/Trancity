/*
 * Сделано в SharpDevelop.
 * Пользователь: serg
 * Дата: 18.02.2012
 * Время: 22:05
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
//for exception handling:
using ODE_Test;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Engine
{
    /// <summary>
    /// Now it's near thread-safe!
    /// </summary>
    public class Logger
    {
        //и вообще уж больно кривой класс, особенно конструктор и переписывание файла
        private const string logfileExtension = ".log";
        private static string _filename;
        private static bool _unavail = true;
        private static bool _initialized = false;
        private static Mutex _mutex;

        public static void Initialize(Assembly app)
        {
            if (_initialized) return;
            try
            {
                _filename = Application.StartupPath + @"\" + Application.ProductName + logfileExtension;

                _unavail = false;
                _mutex = new Mutex(false);
                ResetFile();
                //это всё можно выкинуть из обработчика:
                var thisAssembly = Assembly.GetExecutingAssembly();
                var fileVersion = FileVersionInfo.GetVersionInfo(thisAssembly.Location);
                RawLog(string.Format("{0} v{1} (x{2})", Application.ProductName, Application.ProductVersion, IntPtr.Size == 4 ? "86" : "64"));
                var date = Utilities.GetBuildDate(app);
                RawLog(string.Format("Дата сборки: {0}, {1}", date.ToShortDateString(), date.ToLongTimeString()));
                date = Utilities.GetBuildDate(thisAssembly);
                RawLog(string.Format("Используется {0} v{1} (Дата сборки: {2}, {3})", thisAssembly.GetName().Name, fileVersion.FileVersion, date.ToShortDateString(), date.ToLongTimeString()));
                DebugLog(thisAssembly.GetName().Name, "скомпилирован в этом режиме");
                RawLog(string.Format("Работает на: {0}", Environment.OSVersion.VersionString));
                RawLog("Путь к исполняемому файлу: " + Application.StartupPath);
                RawLog("Дата: " + DateTime.Now.ToShortDateString());
                //				Log("Log started");
                _initialized = true;
            }
            catch (Exception exc)
            {
                _unavail = true;
                // H-A-C-K: ловим эксепшн окошечком. А не переделать в ловлю через ExceptionHandlerForm?
                //				MessageBox.Show("Cannot start logger. Reason:\n\n" + exc.ToString());
                // считай что сделано:
                ExceptionHandlerForm.ShowException(exc, "Не удается запустить регистратор!");
            }
        }

        public static void LogException(Exception exception)
        {
            Log(exception.GetType().ToString(), exception.ToString());
        }

        public static void LogException(Exception exception, string str)
        {
            Log(str, Environment.NewLine + exception.ToString());
        }

        public static void Log(string value)
        {
            Log(string.Empty, value);
        }

        public static void Log(string sectionName, string message)
        {
            var now = DateTime.Now.ToLongTimeString();
            RawLog(string.Format("{0}: {1} - {2}", now, sectionName, message));
        }

        [Conditional("DEBUG")]
        public static void DebugLog(string value)
        {
            DebugLog(string.Empty, value);
        }

        [Conditional("DEBUG")]
        public static void DebugLog(string sectionName, string message)
        {
            RawLog(string.Format("DEBUG: {0} - {1}", sectionName, message));
        }

        public static void RawLog(string value)
        {
            if (!_unavail)
            {
                if (!_mutex.WaitOne(100))
                    return;
                try
                {
                    using (StreamWriter strw = new StreamWriter(_filename, true, Encoding.Default))
                    {
                        strw.WriteLine(value);
                    }
                }
                catch { };
                _mutex.ReleaseMutex();
            }
        }

        private static void ResetFile()
        {
            try
            {
                // HACK: слишком костыльно, переделать?
                using (StreamWriter strw = new StreamWriter(_filename, false, Encoding.Default))
                {
                }
            }
            catch
            {
                _unavail = true;
            }
        }


    }
}