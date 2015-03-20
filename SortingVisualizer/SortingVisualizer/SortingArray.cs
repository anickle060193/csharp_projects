using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    class SortingArray : IIndexable<int>
    {
        private int[] _array;
        private int _modifications;

        public int Reads { get; private set; }
        public int Writes { get; private set; }
        public int Comparisons { get; private set; }
        public bool RecordEdits { get; set; }
        public int Count { get; private set; }
        public bool IsReadOnly { get { return false; } }
        public List<SortEdit> History { get; private set; }

        public SortingArray()
        {
            History = new List<SortEdit>();
            _array = new int[ 10 ];
        }

        public int CompareValues( int val1, int val2 )
        {
            Comparisons++;
            return val1.CompareTo( val2 );
        }

        public int this[ int index ]
        {
            get
            {
                Reads++;
                return _array[ index ];
            }
            set
            {
                Writes++;
                _array[ index ] = value;
                _modifications++;
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            int mods = _modifications;
            for( int i = 0; i < Count; i++ )
            {
                if( mods != _modifications )
                {
                    throw new InvalidOperationException( "SortingArray was modified while enumeratoring." );
                }
                yield return _array[ i ];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            int mods = _modifications;
            for( int i = 0; i < Count; i++ )
            {
                if( mods != _modifications )
                {
                    throw new InvalidOperationException( "SortingArray was modified while enumeratoring." );
                }
                yield return _array[ i ];
            }
        }
    }
}
