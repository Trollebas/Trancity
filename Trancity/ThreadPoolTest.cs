/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 01.12.2014
 * Time: 18:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Windows.Forms;
using Trancity;
using Common;
using Engine;

namespace Trancity
{
	/// <summary>
	/// Тестирование возможностей ThreadPool
	/// </summary>
	public class ThreadPoolTest
	{
		private static Game _game;
		private static Mutex mutex;
		private static bool mutex2 = false;
		private static object locker = new object();
		private static bool sound_flag = false;
		
		public static void RunGameProcess(Game game, bool sound)
		{
			ManualResetEvent endofgame = new ManualResetEvent(false);
			//performing thread pool...
			_game = game;
			sound_flag = sound;
			mutex = new Mutex(false);
//			mutex2 = new Mutex(false);
			ThreadPool.QueueUserWorkItem(new WaitCallback(MainThread), endofgame);
			ThreadPool.QueueUserWorkItem(new WaitCallback(RenderThread), endofgame);
			endofgame.WaitOne();
			MyDirectInput.Free();
			//closing...
		}
		
		private static void MainThread(object arg)
		{
			try
			{
				do
				{
					lock (locker)
					{
						if (!MyDirectInput.Process() && MyDirectInput.alt_f4)
		                {
		                	break;
		                }
		                _game.Process_Input();
		                if (_game.активна)
		                {
		                	_game.мир.Обновить(_game.игроки);
		                }
		                else
		                {
		                	_game.мир.Обновить_время();
		                }
		                if (sound_flag)
		                {
		                	_game.мир.UpdateSound(_game.игроки, _game.активна);
		                }
						mutex.WaitOne();
					}
					_game.RenderMain();
					mutex2 = true;
					mutex.ReleaseMutex();
	                Thread.Sleep(1);
				}
				while (true);
			}
			catch (Exception e)
			{
				Logger.LogException(e, "MainThread");
				MessageBox.Show("Необработанное исключение типа " + e.GetType() + "\n" + e + "\n\nПожалуйста, сообщите создателю игры об этой ошибке.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			((ManualResetEvent)arg).Set();
		}
		
		private static void RenderThread(object arg)
		{
			try
			{
				do
				{
					lock (locker) mutex.WaitOne();
					if (mutex2) _game.RenderThread();
					mutex2 = false;
					mutex.ReleaseMutex();
	                Thread.Sleep(1);
				}
				while (true);
			}
			catch (Exception e)
			{
				Logger.LogException(e, "RenderThread");
				MessageBox.Show("Необработанное исключение типа " + e.GetType() + "\n" + e + "\n\nПожалуйста, сообщите создателю игры об этой ошибке.", "Trancity", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			((ManualResetEvent)arg).Set();
		}
	}
}
