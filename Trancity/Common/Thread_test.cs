/*
 * Сделано в SharpDevelop.
 * Пользователь: serg
 * Дата: 11.09.2011
 * Время: 20:27
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Threading;
using System.Windows.Forms;
using Trancity;

namespace Common
{
	public class ThreadTest
	{
		private Thread thrd;
		/*private MainForm main;
		private bool flag;
		private static object lockobj = new object();*/
		
		/*public ThreadTest(MainForm mainform)//render thread test
		{
			main = mainform;
			thrd = new Thread(this.Run);
			thrd.Name = "render";
			thrd.Start();
		}*/
			
		public ThreadTest(ThreadStart start)
		{
			thrd = new Thread(start);
			thrd.Priority = ThreadPriority.Normal;
			thrd.Start();
		}
		
		/*public ThreadTest(MainForm mainform, bool sflag)//render thread test
		{
			main = mainform;
			flag = sflag;
			thrd = new Thread(this.FullRun);
			thrd.Name = "stuff";
			thrd.Start();
		}*/
		
		~ThreadTest()
		{
			thrd.Abort();
		}
		
		/*public void Run()
		{
			do
			{
				try {
//				Thread.Sleep(20);
//				lock (MeshObject.lockon)
					lock (main) main._игра.Render();}
				catch (Exception exception)
                {
//              	if ((flag2) && (testThread != null)) testThread.thrd.Abort();
                    var str2 = "Unhandled exception of type " + exception.GetType() + "\n" + exception;
                    main.Close();
                    MessageBox.Show(str2 + "\n\nПожалуйста, сообщите создателю игры об этой ошибке.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
			}
			while (!MyDirectInput.alt_f4);
		}
		
		public void FullRun()
		{
			do
			{
				try {
				lock (main)
				{
					if (!MyDirectInput.Process() && MyDirectInput.alt_f4)
                	{
                		MyDirectInput.Free();
                    	MyDirectSound.Free();
                    	main.Close();
                    	//thrd.Abort();
                 	}
	                main._игра.Process_Input();
	                if (main._игра.активна)
	                {
	                	main._игра.мир.Обновить(main._игра.игроки);
	                }
	                else
	                {
	                	main._игра.мир.Обновить_время();
	                }
	                if (flag)
	                {
	                 	main._игра.мир.UpdateSound(main._игра.игроки, main._игра.активна);
	                }
				}
				Thread.Sleep(20);}
				catch (Exception exception)
                {
//              	if ((flag2) && (testThread != null)) testThread.thrd.Abort();
                    var str2 = "Unhandled exception of type " + exception.GetType() + "\n" + exception;
                    main.Close();
                    MessageBox.Show(str2 + "\n\nПожалуйста, сообщите создателю игры об этой ошибке.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
			}
			while (!MyDirectInput.alt_f4);
		}*/
	}
}
