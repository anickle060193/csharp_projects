using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingVisualizer
{
    // Summary:
    //     Represents a collection of objects that can be individually accessed by index.
    //
    // Type parameters:
    //   T:
    //     The type of elements in the list.
    public interface IIndexable<T> : IEnumerable<int>
    {
        // Summary:
        //     Gets the number of elements contained in the System.Collections.Generic.ICollection<T>.
        //
        // Returns:
        //     The number of elements contained in the System.Collections.Generic.ICollection<T>.
        int Length { get; }

        // Summary:
        //     Gets or sets the element at the specified index.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to get or set.
        //
        // Returns:
        //     The element at the specified index.
        //
        // Exceptions:
        //   System.ArgumentOutOfRangeException:
        //     index is not a valid index in the System.Collections.Generic.IList<T>.
        //
        //   System.NotSupportedException:
        //     The property is set and the IIndexable<T> is read-only.
        T this[ int index ] { get; set; }
    }
}
