using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ITI.Work
{
    public class ITIList<T> : IITIList<T>
    {
        T[] _tab;
        int _count;
        int _version;

        public ITIList()
        {
            _tab = new T[4];
        }

        public ITIList( int initialCapacity )
        {
            if( initialCapacity <= 0 ) throw new ArgumentNullException( nameof( initialCapacity ), "Must be positive." );
            _tab = new T[initialCapacity];
        }

        public int Count => _count;

        public T this[ int index ]
        {
            get
            {
                if( index < 0 || index >= _count ) throw new IndexOutOfRangeException();
                return _tab[index];
            }
            set
            {
                if( index < 0 || index >= _count ) throw new IndexOutOfRangeException();
                _version++;
                _tab[index] = value;
            }
        }

        public void RemoveAt( int index )
        {
            _version++;
            if( index < 0 || index >= _count ) throw new IndexOutOfRangeException();
            Array.Copy( _tab, index + 1, _tab, index, _count - index );
            --_count;
        }

        public void InsertAt( int index, T value )
        {
            if( index < 0 || index > _count ) throw new IndexOutOfRangeException();
            _version++;

            if( _count == _tab.Length ) ResizeInternalArray();
            Array.Copy( _tab, index, _tab, index + 1, _count - index );
            _tab[index] = value;
            _count++;
        }

        public int IndexOf( T i )
        {
            for( int x = 0; x < _count; ++x )
            {
                if( _tab[x].Equals( i ) ) return x;
            }
            return -1;
        }

        public void Add( T i )
        {
            _version++;
            if( _count == _tab.Length ) ResizeInternalArray();
            _tab[_count++] = i;
        }

        void ResizeInternalArray()
        {
            var newTab = new T[_tab.Length * 2];
            Array.Copy( _tab, newTab, _count );
            _tab = newTab;
        }

        class E : IEnumerator<T>
        {
            readonly ITIList<T> _papa;
            readonly int _papaVersion;
            int _current;
            T _currentValue;

            public E( ITIList<T> papa )
            {
                _papa = papa;
                _papaVersion = papa._version;
            }

            public T Current => _currentValue;

            object IEnumerator.Current => Current;

            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                if( _papaVersion != _papa._version )
                {
                    throw new InvalidOperationException();
                }
                if( ++_current >= _papa._count ) return false;
                _currentValue = _papa._tab[_current];
                return true;
            }

            public void Reset() => throw new NotSupportedException();
        }


        public IEnumerator<T> GetEnumerator() => new E( this );

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
