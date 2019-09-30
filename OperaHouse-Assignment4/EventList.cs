using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouse_Assignment4
{
    public class EventList
    {
        public List<Event> events;

        public EventList()
        {
            events = new List<Event>();
        }
        
        public void AddEvent(Event e)
        {
            events.Add(e);
        }

        public void AddEvent(List<Event> eventList)
        {

            foreach (Event e in eventList)
            {
                events.Add(e);
            }
        }
        public List<string> ListOfEvents()
        {
            List<string> result = new List<string>();
            foreach (Event e in events)
            {
                string s = string.Format("{0} - {1} - {2}", e.Title, e.EventTime, e.Performer);
                result.Add(s);
            }
            return result;
        }

        public void SortByDate()
        {
            events.Sort(new DateComparer());
        }

        public void SortByTitle()
        {
            Comparison<Event> sorter = new Comparison<Event>(EventDelegates.TitleCompare);
            events.Sort(sorter);
        }

        public List<Event> SearchByPerformer(string performerName)
        {
            List<Event> result = new List<Event>();
            foreach (Event e in events)
            {
                if (e.Performer.Name == performerName)
                {
                    result.Add(e);
                }
            }
            return result;
        }
        public List<Event> SearchByStage(string stageName)
        {
            List<Event> result = new List<Event>();
            foreach (Event e in events)
            {
                if (e.Stage.StageName == stageName)
                {
                    result.Add(e);
                }
            }
            return result;
        }

        public List<Event> OpenShows()
        {
            List<Event> result = new List<Event>();
            foreach (Event e in events)
            {
                if (e.NumAvailableTickets > 0)
                {
                    result.Add(e);
                }
            }
            return result;
        }

        public void Sort()
        {
            events.Sort(new DateComparer());
        }
        public void ReverseSort()
        {
            events.Reverse();
        }

        public List<Event> ShowsShorterThan(int minutes)
        {
            Predicate<Event> searcher = new Predicate<Event>(EventDelegates.ShortDuration);
            List<Event> results = events.FindAll(searcher);
            return results;
        }
        public List<Event> ShowsLongerThan(int minutes)
        {
            Predicate<Event> searcher = new Predicate<Event>(EventDelegates.LongDuration);
            List<Event> results = events.FindAll(searcher);
            return results;
        }
        public List<Event> SearchByDateRange(DateTime lowDate, DateTime highDate)
        {
            List<Event> result = new List<Event>();
            foreach (Event e in events)
            {
                if (e.EventTime > lowDate && e.EventTime < highDate)
                {
                    result.Add(e);
                }
                Comparison<Event> sorter = new Comparison<Event>(EventDelegates.TitleCompare);
                result.Sort(sorter);
            }
            return result;
        }

        //Helper method that returns the IDs of all the events in the order listed in the List 
        //This is for testing purposes only
        public string[] eventIDs()
        {
            string[] ids = new string[events.Count];
            int i = 0;
            foreach (Event e in events)
            {
                ids[i] = e.ID;
                i++;
            }
            return ids;
        }

    }
}
