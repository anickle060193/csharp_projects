using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    public class SortingArray : IIndexable<int>
    {
        private int[] _array;
        private int _modifications;
        private int _editNumber;
        private DateTime _startTime;

        public int Reads { get; private set; }
        public int Writes { get; private set; }
        public int Comparisons { get; private set; }
        public bool RecordEdits { get; set; }
        public List<SortEdit> History { get; private set; }

        public int Length { get { return _array.Length; } }

        public bool IsSorted
        {
            get
            {
                if( _array == null )
                {
                    return true;
                }
                for( int i = 1; i < Length; i++ )
                {
                    if( _array[ i - 1 ] > _array[ i ] )
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public int this[ int index ]
        {
            get
            {
                if( RecordEdits )
                {
                    Reads++;
                }
                return _array[ index ];
            }
            set
            {
                int oldValue = _array[ index ];
                _array[ index ] = value;
                _modifications++;
                if( RecordEdits )
                {
                    Writes++;
                    History.Add( CreateSortEdit( index, oldValue, value ) );
                }
            }
        }

        public SortingArray()
        {
            RecordEdits = true;
            History = new List<SortEdit>();
        }

        private SortEdit CreateSortEdit( int index, int oldValue, int newValue )
        {
            SortEdit e = new SortEdit();
            e.EditNumber = _editNumber++;
            e.Reads = Reads;
            e.Writes = Writes;
            e.Comparisons = Comparisons;
            e.Index = index;
            e.OldValue = oldValue;
            e.NewValue = newValue;
            if( History.Count == 0 )
            {
                _startTime = DateTime.Now;
            }
            e.ElapsedTime = ( DateTime.Now - _startTime ).Ticks;
            return e;
        }

        private void ResetHistory()
        {
            RecordEdits = true;
            Reads = 0;
            Writes = 0;
            Comparisons = 0;
            History.Clear();
            _editNumber = 0;
        }

        public IEnumerator<SortEdit> Sort( Sorter sorter, int[] array )
        {
            ResetHistory();
            _array = (int[])array.Clone();
            _startTime = DateTime.Now;
            sorter.Sort( this );
            return History.GetEnumerator();
        }

        public int CompareValuesAt( int index1, int index2 )
        {
            return CompareValues( this[ index1 ], this[ index2 ] );
        }

        public void Swap( int index1, int index2 )
        {
            int temp = this[ index1 ];
            this[ index1 ] = this[ index2 ];
            this[ index2 ] = temp;
        }

        public int CompareValues( int val1, int val2 )
        {
            if( RecordEdits )
            {
                Comparisons++;
            }
            return val1.CompareTo( val2 );
        }

        public IEnumerator<int> GetEnumerator()
        {
            int mods = _modifications;
            for( int i = 0; i < Length; i++ )
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
            for( int i = 0; i < Length; i++ )
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
