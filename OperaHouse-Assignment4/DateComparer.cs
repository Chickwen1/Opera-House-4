using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouse_Assignment4
{
    public class DateComparer: IComparer<Event>
    {
        public int Compare (Event e1, Event e2)
        {
            return e1.EventTime.CompareTo(e2.EventTime);
        }
    }
}
