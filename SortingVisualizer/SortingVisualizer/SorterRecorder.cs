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
        public static SortRecord[] RecordSort( int[] array, Sorter sorter )
        {
            List<SortRecord> history = new List<SortRecord>();
            ObservableCollection<int> collection = new ObservableCollection<int>( array );
            collection.CollectionChanged += (NotifyCollectionChangedEventHandler)delegate( object sender, NotifyCollectionChangedEventArgs e )
            {
                history.Add( new SortRecord( e.NewStartingIndex, (int)e.OldItems[ 0 ], (int)e.NewItems[ 0 ] ) );
            };
            sorter.Sort( collection );
            return history.ToArray();
        }
    }
}
