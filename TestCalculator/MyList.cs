using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TestCalculator
{
    class MyList<T> : IList<T>
    {
        List<T> placeHolderList = new List<T>();
        public T this[int index] { get =>
                placeHolderList[index]; set => placeHolderList[index] = value; }

        public int Count => placeHolderList.Count;

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            placeHolderList.Add(item);
        }

        public void Clear()
        {
            placeHolderList.Clear();
        }

        public bool Contains(T item)
        {
            return placeHolderList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return placeHolderList.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return placeHolderList.IndexOf(item);
            throw new NotImplementedException();
        }

        public void Insert(int index, T item)
        {
            placeHolderList.Insert(index, item);
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            return placeHolderList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            placeHolderList.RemoveAt(index);
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
