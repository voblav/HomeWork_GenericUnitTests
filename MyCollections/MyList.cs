using System;
using System.Collections;
using System.Collections.Generic;

namespace MyCollections
{
    public class MyList<T> : IList<T>, ICollection
    {
        #region fields
        private T[] _arr;
        private int _count = 0;
        private int _position = -1;
        #endregion

        #region properties and indexer
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _count) throw new ArgumentOutOfRangeException("InnerIndex");
                return _arr[index];
            }
            set
            {
                if (index < 0 || index > _count) throw new ArgumentOutOfRangeException("InnerIndex");
                _arr[index] = value;
                _count++;
            }
        }

        public int Count
        {
            get
            {
                return _count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsSynchronized => false;

        public object SyncRoot => null;
        #endregion

        #region ctors
        public MyList()
        {
            _arr = new T[4];
        }

        public MyList(ICollection<T> list)
        {
            int index = 0;
            _arr = new T[list.Count];

            foreach (var item in list)
            {
                _arr[index++] = item;
                _count++;
            }
        }
        #endregion

        public void GrowArray()
        {
            int newLength = _arr.Length == 0 ? 4 : _arr.Length << 1;

            T[] newArray = new T[newLength];
            for (int i = 0; i < _arr.Length; i++)
            {
                newArray[i] = _arr[i];
            }
            _arr = newArray;
        }

        public void Add(T val)
        {
            if (_count == _arr.Length)
            {
                GrowArray();
            }
            _arr[_count++] = val;
        }

        public void Clear()
        {
            _count = 0;
        }

        public void Reset()
        {
            _position = -1;
        }

        public bool Contains(T val)
        {
            foreach (var item in _arr)
            {
                if (item.Equals(val))
                    return true;
            }
            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentException("NULL");

            for (int i = 0; i < array.Length; i++)
            {
                array[arrayIndex++] = _arr[i];
            }
        }

        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                if (_position < _arr.Length - 1)
                {
                    _position++;
                    yield return _arr[_position];
                }
                else
                {
                    Reset();
                    yield break;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        public int IndexOf(T item)
        {
            for (int i = 0; i < _arr.Length; i++)
            {
                if (_arr[i].Equals(item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index > Count)
                throw new IndexOutOfRangeException();

            if (_arr.Length == Count)
                GrowArray();

            for (int i = Count; i < index; i--)
            {
                _arr[i] = _arr[i - 1];
            }
            _arr[index] = item;
            _count++;
        }

        public bool Remove(T item)
        {
            if (!Contains(item))
                return false;
            RemoveAt(IndexOf(item));
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException();
            if (index < Count - 1)
            {
                for (int i = index; i < Count - 1; i++)
                {
                    _arr[i] = _arr[i + 1];
                }
            }
            _count--;
        }

        public void CopyTo(Array array, int userArrayIndex)
        {
            var arr = array as object[];

            if (arr == null)
                throw new ArgumentException("Expecting array to be object[]");

            for (int i = 0; i < array.Length; i++)
            {
                arr[userArrayIndex++] = arr[i];
            }
        }
    }
}