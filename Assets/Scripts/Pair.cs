using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AwesomeGame.UtilityMgmt
{
    public class Pair<T, V>
    {
        public T First { get; set; }
        public V Second { get; set; }

        public Pair( ) {

        }

        public Pair( T first, V second ) {
            First = first;
            Second = second;
        }
    }
}
