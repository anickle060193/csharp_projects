using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    public class SortRecord
    {
        public int Index { get; set; }
        public int OldValue { get; set; }
        public int NewValue { get; set; }

        public SortRecord( int index, int oldValue, int newValue )
        {
            Index = index;
            OldValue = oldValue;
            NewValue = newValue;
        }

        public void ApplyRecord( int[] array )
        {
            array[ Index ] = NewValue;
        }
    }
}
