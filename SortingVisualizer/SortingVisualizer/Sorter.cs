using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    public abstract class Sorter
    {
        private static readonly List<Type> SorterTypes = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany( assembly => assembly.GetTypes() )
            .Where( type => type.IsSubclassOf( typeof( Sorter ) ) )
            .ToList();
        static Sorter()
        {
            SorterTypes.Sort( ( t1, t2 ) => t1.Name.CompareTo( t2.Name ) );
        }

        public abstract bool IsWorking { get; }

        public abstract void Sort( SortingArray array );

        public override string ToString()
        {
            return this.GetType().Name;
        }

        public override bool Equals( object obj )
        {
            return obj != null && obj.GetType() == this.GetType();
        }

        public static IList<Sorter> GetSorters()
        {
            return SorterTypes.ConvertAll( t => (Sorter)Activator.CreateInstance( t ) );
        }
    }
}
