/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 14.02.2016
 * Time: 19:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Engine.Sound
{
	/// <summary>
	/// ISound2D - интерфейс замена SourceVoice из XA2.
	/// </summary>
	public interface ISound2D
	{
		void Play();
		
		void Stop();
		
		float Frequency { get; set; }
		
		float Volume { get; set; }
	}
}
