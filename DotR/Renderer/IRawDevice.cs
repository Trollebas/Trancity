/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 15.11.2015
 * Time: 21:10
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Engine
{
	/// <summary>
	/// Description of IRawDevice.
	/// </summary>
	public interface IRawDevice<T>
	{
		T RawDevice { get; }
	}
}
