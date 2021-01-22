/*
 * Created by SharpDevelop.
 * User: sergey
 * Date: 25.06.2017
 * Time: 0:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
	/// <summary>
	/// RestrictedStack - funny bicycle primary aimed to store editor actions.<br/>
	/// Note: Thread-unsafe
	/// </summary>
	public class RestrictedStack<T> : IEnumerable, ICollection
	{
		private readonly LinkedList<T> _underhoodItemList;
		private int _capacity;
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="capacity"></param>
		public RestrictedStack(int capacity)
		{
			if (capacity < 1)
				throw new ArgumentOutOfRangeException("capacity", "Capacity must be >= 0");
			
			_underhoodItemList = new LinkedList<T>();
			_capacity = capacity;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		public virtual void Push(T value)
		{
			var count = this.Count;
			while (count >= this.Capacity)
			{
				_underhoodItemList.RemoveLast();
				count--;
			}
			
			_underhoodItemList.AddFirst(value);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>First value from stack</returns>
		public virtual T Pop()
		{
			if (this.Count == 0)
				throw new InvalidOperationException("Can not pop from empty stack");
			
			var value = _underhoodItemList.First.Value;
			_underhoodItemList.RemoveFirst();
			
			return value;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <returns>First value from stack</returns>
		public virtual T Peek()
		{
			if (this.Count == 0)
				throw new InvalidOperationException("Can not peek from empty stack");
			
			return _underhoodItemList.First.Value;
		}
		
		/// <summary>
		/// 
		/// </summary>
		public void Clear()
		{
			_underhoodItemList.Clear();
		}
		
		/// <summary>
		/// Obvious
		/// </summary>
		public int Capacity
		{
			get
			{
				return _capacity;
			}
		}

		#region ICollection implementation

		/// <summary>
		/// Just a facade to underhood collection
		/// </summary>
		/// <param name="array"></param>
		/// <param name="index"></param>
		public void CopyTo(Array array, int index)
		{
			var tarray = array as T[];
			_underhoodItemList.CopyTo(tarray, index);
		}

		/// <summary>
		/// Fasade
		/// </summary>
		public int Count
		{
			get
			{
				return _underhoodItemList.Count;
			}
		}

		/// <summary>
		/// Not supported
		/// </summary>
		public object SyncRoot
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>
		/// Not supported
		/// </summary>
		public bool IsSynchronized
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		#endregion

		#region IEnumerable implementation

		/// <summary>
		/// Just a facade to underhood collection
		/// </summary>
		/// <returns>IEnumerator object</returns>
		public IEnumerator GetEnumerator()
		{
			return _underhoodItemList.GetEnumerator();
		}

		#endregion
	}
}
