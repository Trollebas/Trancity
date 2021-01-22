/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 18.08.2012
 * Time: 0:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using Engine;

namespace Common
{
	public class StreamWork : IDisposable
	{
		public Stream str = null;
		private Dictionary<string, bool> ready = new Dictionary<string, bool>();
		private Dictionary<string, List<byte>> data = new Dictionary<string, List<byte>>();
		private List<string> toDelete = new List<string>();
		
		// TODO: переделать чтение из файла
		public byte[] LoadData(string filename)
		{
			List<byte> bytes = new List<byte>();
			if (str == null)
			{
				try
				{
					str = new FileStream(filename, FileMode.Open);
				}
				catch (Exception e)
				{
					Logger.LogException(e, "Couldn't load file: " + filename);
					return new byte[0];
				}
			}
			int rindex;
			do
			{
				rindex = str.ReadByte();
				if (rindex != -1) bytes.Add((byte)rindex);
			}
			while (rindex != -1);
			this.Dispose();
			return bytes.ToArray();
		}
		
		public virtual void Dispose()
		{
			if (str != null) {
        		str.Close();
        		str.Dispose();
        	}
		}
		
		//new section:
		
		public byte[] StartAsyncLoad(string filename)
		{
			try 
			{
				if (ready.ContainsKey(filename))
				{
					if (ready[filename])
					{
						toDelete.Add(filename);
						return data[filename].ToArray();
					}
					return new byte[0];
				}
				FileStream stream = File.OpenRead(filename);
				if (!stream.CanRead)
				{
				    MessageBox.Show("Cannot read stream.");
				    stream.Close();
				    return new byte[0];
				}
				foreach (var str in toDelete)
				{
					ready.Remove(str);
					data.Remove(str);
				}
				toDelete.Clear();
				Byte[] myByteArray = new Byte[1024];
				ready.Add(filename, false);
				data.Add(filename, new List<byte>());
				stream.BeginRead(myByteArray, 0, myByteArray.Length, 
				ReadAsyncCallback, new MyAsyncInfo(myByteArray, stream, filename));
				return new byte[0];
			}
			catch (Exception exc)
		  	{
//		  		MessageBox.Show("Error in StartAsyncLoad!");
		  		Logger.LogException(exc);
		  		return new byte[0];
		  	}
		}
		
		private void ReadAsyncCallback(IAsyncResult ar)
		{
			MyAsyncInfo info = ar.AsyncState as MyAsyncInfo;
			try
			{
				int amountRead = info.MyStream.EndRead(ar);
			  	for (int i = 0; i < amountRead; i++)
			  	{
			  		data[info.Name].Add(info.ByteArray[i]);
			  	}
			  	if (info.MyStream.Position < info.MyStream.Length)
			  	{
			  		info.MyStream.BeginRead(info.ByteArray, 0,
			    	info.ByteArray.Length, ReadAsyncCallback, info);
			  	}
			  	else
			  	{
			  		ready[info.Name] = true;
			    	info.MyStream.Close();
			  	}
			}
		  	catch (Exception exc)
		  	{
		  		Logger.LogException(exc);
		  		info.MyStream.Close();
		  		return;
		  	}
		}
		
		public class MyAsyncInfo
		{
			public Byte[] ByteArray;
			public Stream MyStream;
			public string Name;
			
			public MyAsyncInfo(Byte[] array, Stream stream, string name)
			{
				ByteArray = array;
				MyStream = stream;
				Name = name;
			}
		}
	}
	
	public enum StreamWorkMode
    {
    	Read,
    	Write,
    	Append
    }
}
