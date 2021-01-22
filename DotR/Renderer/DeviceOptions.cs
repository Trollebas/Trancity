/*
 * Created by SharpDevelop.
 * User: Sergey
 * Date: 01.05.2015
 * Time: 18:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Runtime.InteropServices;

namespace Engine
{
	/// <summary>
	/// DeviceOptions - so u now wat is it
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct DeviceOptions //: IEquatable<DeviceOptions>
	{
		public int adapterID;
		//only for dx?
    	public int vertexProcessingMode;
    	//device type
    	public int deviceType;
    	public int fullscreenRate;
        public int fullscreenX;
        public int fullscreenY;
        public bool windowed;
        public int windowedX;
        public int windowedY;
        public bool vSync;
		
		#region Equals and GetHashCode implementation
		// The code in this region is useful if you want to use this structure in collections.
		// If you don't need it, you can just remove the region and the ": IEquatable<DeviceOptions>" declaration.
		/*
		public override bool Equals(object obj)
		{
			if (obj is DeviceOptions)
				return Equals((DeviceOptions)obj); // use Equals method below
			else
				return false;
		}
		
		public bool Equals(DeviceOptions other)
		{
			// add comparisions for all members here
			return this.member == other.member;
		}
		
		public override int GetHashCode()
		{
			// combine the hash codes of all members here (e.g. with XOR operator ^)
			return member.GetHashCode();
		}
		
		public static bool operator ==(DeviceOptions left, DeviceOptions right)
		{
			return left.Equals(right);
		}
		
		public static bool operator !=(DeviceOptions left, DeviceOptions right)
		{
			return !left.Equals(right);
		}*/
		#endregion
	}
}
