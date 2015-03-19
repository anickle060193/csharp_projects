using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    public abstract class Sorter
    {
        public abstract void Sort( IList<int> collection );

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public static void Swap( int i, int j, IList<int> collection )
        {
            int temp = collection[ i ];
            collection[ i ] = collection[ j ];
            collection[ j ] = temp;
        }

        public override bool Equals( object obj )
        {
            return obj != null && obj.GetType() == this.GetType();
        }
    }
}
