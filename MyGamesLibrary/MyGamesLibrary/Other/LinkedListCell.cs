using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGamesLibrary.Other
{
    public class LinkedListCell<T>
    {
        public T Data { get; set; }
        public LinkedListCell<T> Next { get; set; }
    }
}
