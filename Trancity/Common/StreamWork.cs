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

namespace Common
{
	public class StreamWork : IDisposable
	{
		public Stream str = null;
		private Dictionary<string, bool> ready = new Dictionary<string, bool>();
		private Dictionary<string, List<byte>> data = new Dictionary<string, List<byte>>();
		private List<string> toDelete = new List<string>();
		
		public byte[] LoadData(string filename)
		{
			List<byte> bytes = new List<byte>();
			if (str == null) {
				try {
					str = new FileStream(filename, FileMode.Open);
				}
				catch (Exception e) {
//					throw new Exception(e.Message + "\nCouldn't load file: " + filename);
					Logging.WriteExc(e, "Couldn't load file: " + filename);
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
			try {
			if (ready.ContainsKey(filename))
			{
				if (ready[filename])
				{
					toDelete.Add(filename);
					return data[filename].ToArray();
				}
				return new byte[0];
			}
			FileStream stream = null;
			try
			{
				stream = File.OpenRead(filename);
			}
			catch (IOException)
			{
				MessageBox.Show("Unable to open file.");
				return new byte[0];
			}
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
			try
			{
				ready.Add(filename, false);
				data.Add(filename, new List<byte>());
			    stream.BeginRead(myByteArray, 0, myByteArray.Length, 
			    ReadAsyncCallback, new MyAsyncInfo(myByteArray, stream, filename));
			}
			catch (IOException)
			{
			    MessageBox.Show("Unable to start read.");
			    stream.Close();
			}
			return new byte[0];}
			catch (Exception exc)
		  	{
//		  		MessageBox.Show("Error in StartAsyncLoad!");
		  		Logging.WriteExc(exc);
		  		return new byte[0];
		  	}
		}
		
		private void ReadAsyncCallback(IAsyncResult ar)
		{
			try {
			MyAsyncInfo info = ar.AsyncState as MyAsyncInfo;
			int amountRead = 0;
		  	try
		  	{
				amountRead = info.MyStream.EndRead(ar);
			}
		  	catch (IOException)
		  	{
		    	MessageBox.Show("Unable to complete read.");
		  		info.MyStream.Close();
		  		return;
			}
		  	for (int i = 0; i < amountRead; i++)
		  	{
		  		data[info.Name].Add(info.ByteArray[i]);
		  	}
		  	if (info.MyStream.Position < info.MyStream.Length)
		  	{
		  		try
		  		{
		  			info.MyStream.BeginRead(info.ByteArray, 0,
		        	info.ByteArray.Length, ReadAsyncCallback, info);
		  		}
		  		catch (IOException)
		  		{
		        	MessageBox.Show("Unable to start next read.");
		      		info.MyStream.Close();
		    	}
		  	}
		  	else
		  	{
		  		ready[info.Name] = true;
		    	info.MyStream.Close();
//		    	MessageBox.Show("Done reading!");
		  	}}
		  	catch (Exception exc)
		  	{
		  		Logging.WriteExc(exc);
//		  		MessageBox.Show("Error in ReadAsyncCallback!");
		  		return;
		  	}
		}
		
		public class MyAsyncInfo
		{
			public Byte[] ByteArray { get; set; }
			public Stream MyStream { get; set; }
			public string Name { get; set; }
			
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
    	Write
    }
}
