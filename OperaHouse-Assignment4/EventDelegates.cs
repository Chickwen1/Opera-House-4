using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouse_Assignment4
{
    class EventDelegates
    {
        public static int DateCompare(Event e1, Event e2)
        {
            return e1.EventTime.CompareTo(e2.EventTime);
        }
        public static int TitleCompare(Event e1, Event e2)
        {
            return e1.Title.CompareTo(e2.Title);
        }
        public static int PerformerCompare(Event e1, Event e2)
        {
            return e1.Performer.Name.CompareTo(e2.Performer.Name);
        }
        public static bool ShortDuration(Event target)
        {
            return (target.DurationMinutes < 100);
        }
        public static bool LongDuration(Event target)
        {
            return (target.DurationMinutes > 100);
        }
        public static int AvailableTickets(Event e1, Event e2)
        {
            return e1.NumAvailableTickets.CompareTo(e2.NumAvailableTickets);
        }
    }
}

