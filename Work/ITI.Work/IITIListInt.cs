using System;
using System.Collections.Generic;
using System.Text;

namespace ITI.Work
{
    public interface IITIReadOnlyListInt
    {
        int Count { get; }

        int this[int index] { get; }

        int IndexOf( int i );
    }

    public interface IITIListInt : IITIReadOnlyListInt
    {
        new int this[ int index ] { get; set; }

        void Add( int i );

        void RemoveAt( int index );

        void InsertAt( int index, int value );
    }

}
