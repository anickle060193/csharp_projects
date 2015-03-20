using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    public static class SorterRecorder
    {
        public static SortEdit[] RecordSort( int[] array, Sorter sorter )
        {
            List<SortEdit> history = new List<SortEdit>();
            ObservableCollection<int> collection = new ObservableCollection<int>( array );
            collection.CollectionChanged += (NotifyCollectionChangedEventHandler)delegate( object sender, NotifyCollectionChangedEventArgs e )
            {
                history.Add( new SortEdit( e.NewStartingIndex, (int)e.OldItems[ 0 ], (int)e.NewItems[ 0 ] ) );
            };
            sorter.Sort( collection );
            return history.ToArray();
        }
    }
}
