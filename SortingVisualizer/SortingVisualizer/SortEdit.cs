using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    public class SortEdit
    {
        public int EditNumber { get; set; }

        public int Index { get; set; }
        public int OldValue { get; set; }
        public int NewValue { get; set; }

        public int Reads { get; set; }
        public int Writes { get; set; }
        public int Comparisons { get; set; }

        public long ElapsedTime { get; set; }

        public void ApplyRecord( int[] array )
        {
            array[ Index ] = NewValue;
        }
    }
}
