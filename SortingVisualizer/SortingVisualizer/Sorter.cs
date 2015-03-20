using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    public abstract class Sorter
    {
        public abstract void Sort( SortingArray array );

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals( object obj )
        {
            return obj != null && obj.GetType() == this.GetType();
        }
    }
}
