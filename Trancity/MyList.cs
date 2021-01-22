using System;
using System.Collections;
using System.Collections.Generic;

namespace Common
{
    public class MyList : ICollection, IEnumerable
    {
        public object[] array;
        private MyList[] type_lists;
        private List<Type> types;

        public event Event changed;

        public MyList(params Type[] object_types)
        {
            this.array = new object[0];
            this.types = new List<Type>();
            this.type_lists = new MyList[0];
            if (object_types.Length > 0)
            {
                this.types.AddRange(object_types);
                this.type_lists = new MyList[this.types.Count];
                for (int i = 0; i < this.type_lists.Length; i++)
                {
                    this.type_lists[i] = new MyList(new Type[0]);
                }
            }
        }

        public MyList(ICollection c, params Type[] object_types)
        {
            this.array = new object[0];
            this.types = new List<Type>();
            this.type_lists = new MyList[0];
            ArrayList list = new ArrayList(c);
            this.array = list.ToArray();
            if (object_types.Length > 0)
            {
                this.types.AddRange(object_types);
                this.type_lists = new MyList[this.types.Count];
                for (int i = 0; i < this.type_lists.Length; i++)
                {
                    this.type_lists[i] = new MyList(new Type[0]);
                }
                foreach (object obj2 in list)
                {
                    int typeIndex = this.types.IndexOf(obj2.GetType());
                    if (typeIndex >= 0)
                    {
                        this.type_lists[typeIndex].Add(obj2);
                    }
                }
            }
        }

        public virtual void Add(object value)
        {
            ArrayList list = new ArrayList(this.array);
            list.Add(value);
            this.array = list.ToArray();
            int typeIndex = this.types.IndexOf(value.GetType());
            if (typeIndex >= 0)
            {
                this.type_lists[typeIndex].Add(value);
            }
            if (this.changed != null)
            {
                this.changed();
            }
        }

        public virtual void AddRange(ICollection c)
        {
            ArrayList list = new ArrayList(this.array);
            list.AddRange(c);
            this.array = list.ToArray();
            if (this.types.Count > 0)
            {
                foreach (object obj2 in c)
                {
                    int typeIndex = this.types.IndexOf(obj2.GetType());
                    if (typeIndex >= 0)
                    {
                        this.type_lists[typeIndex].Add(obj2);
                    }
                }
            }
            if (this.changed != null)
            {
                this.changed();
            }
        }

        public void Clear()
        {
            this.array = new object[0];
            foreach (MyList list in this.type_lists)
            {
                list.Clear();
            }
            if (this.changed != null)
            {
                this.changed();
            }
        }

        public MyList Clone()
        {
            return new MyList(this.array, this.types.ToArray());
        }

        public bool Contains(object item)
        {
            ArrayList list = new ArrayList(this.array);
            return list.Contains(item);
        }

        public void CopyTo(Array array, int index)
        {
            this.array.CopyTo(array, index);
        }

        public T[] Get_array<T>()
        {
            int typeIndex = this.types.IndexOf(typeof(T));
            if (typeIndex >= 0)
            {
                return (T[]) this.type_lists[typeIndex].ToArray(typeof(T));
            }
            return new T[0];
        }

        public IEnumerator GetEnumerator()
        {
            return this.array.GetEnumerator();
        }

        public int IndexOf(object item)
        {
            ArrayList list = new ArrayList(this.array);
            return list.IndexOf(item);
        }

        public void Insert(int index, object value)
        {
            ArrayList list = new ArrayList(this.array);
            list.Insert(index, value);
            this.array = list.ToArray();
            int typeIndex = this.types.IndexOf(value.GetType());
            if (typeIndex >= 0)
            {
                this.type_lists[typeIndex].Insert(index, value);
            }
            if (this.changed != null)
            {
                this.changed();
            }
        }

        public static implicit operator MyList(object[] list)
        {
            return new MyList(list, new Type[0]);
        }

        public static implicit operator object[](MyList list)
        {
            return list.array;
        }

        public void Remove(object value)
        {
            ArrayList list = new ArrayList(this.array);
            list.Remove(value);
            this.array = list.ToArray();
            int typeIndex = this.types.IndexOf(value.GetType());
            if (typeIndex >= 0)
            {
                this.type_lists[typeIndex].Remove(value);
            }
            if (this.changed != null)
            {
                this.changed();
            }
        }

        public void RemoveAt(int index)
        {
            object value = this.array[index];
            int typeIndex = this.types.IndexOf(value.GetType());
            if (typeIndex >= 0)
            {
                this.type_lists[typeIndex].RemoveAt(index);
            }
            ArrayList list = new ArrayList(this.array);
            list.RemoveAt(index);
            this.array = list.ToArray();
            if (this.changed != null)
            {
                this.changed();
            }
        }

        public object[] ToArray()
        {
            ArrayList list = new ArrayList(this.array);
            return list.ToArray();
        }

        public Array ToArray(Type type)
        {
            ArrayList list = new ArrayList(this.array);
            return list.ToArray(type);
        }

        public int Count
        {
            get
            {
                return this.array.Length;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return this.array.IsSynchronized;
            }
        }

        public object this[int index]
        {
            get
            {
                if ((index >= 0) && (index < this.array.Length))
                {
                    return this.array[index];
                }
                return null;
            }
            set
            {
                if ((index >= 0) && (index < this.array.Length))
                {
                    this.array[index] = value;
                }
            }
        }

        public object SyncRoot
        {
            get
            {
                return this.array.SyncRoot;
            }
        }

        public delegate void Event();
    }
}

