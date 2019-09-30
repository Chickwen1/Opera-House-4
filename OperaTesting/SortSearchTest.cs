using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OperaHouse_Assignment4;
using System.Collections.Generic;

namespace OperaTesting
{
    [TestClass]
    public class SortSearchTest
    {

        Event shrek, deathShow, belushiShow, grease, midSummer;
        Stage main, lounge;
        Performer drDeath;
        Performer belushi;
        Performer osawaHigh;
        Performer oshkoshTheatre;
        EventList events;

        [TestInitialize]
        public void SetUp()
        {
            osawaHigh = new Performer("Osawa High School", 0);
            drDeath = new Performer("Dr Death", 1500);
            belushi = new Performer("Jim Belushi", 3500);

            main = new Stage("Main Stage", 100, 150);
            lounge = new Stage("The Lounge", 75, 50);

            shrek = new Event("1", "Shrek", osawaHigh, 150, 12, new DateTime(2015, 4, 18, 19, 30, 0), 60, true, main);
            deathShow = new Event("2", "Dr. Death's Musical Adventures", drDeath, 200, 20, new DateTime(2015, 4, 25, 19, 0, 0), 90, true, main);
            belushiShow = new Event("3", "Belushi and the Board of Comedy", belushi, 160, 33, new DateTime(2015, 3, 4, 19, 45, 0), 120, false, lounge);

            events = new EventList();
            events.AddEvent(new List<Event> { shrek, deathShow, belushiShow });
        }


        [TestMethod]
        public void TestSortDefaultSort()
        {
            //sort events by date
            events.Sort();

            string[] expected = new string[] { "3", "1", "2" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestSortReverseSort()
        {
            events.ReverseSort();

            string[] expected = new string[] { "3", "2", "1" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSortByDate()
        {
            //sort events by date
            events.SortByDate();

            string[] expected = new string[] { "3", "1", "2" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
            //Add more test cases
            midSummer = new Event("4", "Midsummer Night", osawaHigh, 100, 10, new DateTime(2015, 9, 30, 19, 30, 0), 99, true, lounge);
            grease = new Event("5", "Grease", oshkoshTheatre, 100, 10, new DateTime(2015, 4, 30, 19, 30, 0), 50, true, main);
            events.AddEvent(midSummer);
            events.AddEvent(grease);
            events.SortByDate();

            expected = new string[] { "3", "1", "2", "5", "4" };
            actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);

        }

        [TestMethod]
        public void TestSortByTitle()
        {
            //sort events by title
            events.SortByTitle();
            string[] expected = new string[] { "3", "2", "1" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
            //Add more test cases
            midSummer = new Event("4", "Midsummer Night", osawaHigh, 100, 10, new DateTime(2015, 9, 30, 19, 30, 0), 99, true, lounge);
            grease = new Event("5", "Grease", oshkoshTheatre, 100, 10, new DateTime(2015, 4, 30, 19, 30, 0), 50, true, main);
            events.AddEvent(midSummer);
            events.AddEvent(grease);
            events.SortByTitle();

            expected = new string[] { "3", "2", "5", "4", "1" };
            actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSortByPerformer()
        {
            //sort events by title
            events.SortByTitle();
            string[] expected = new string[] { "3", "2", "1" };
            string[] actual = events.eventIDs();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSearchPerformer()
        {
            events.AddEvent(new Event("4", "Midsummer Night", osawaHigh, 100, 10, new DateTime(2015, 9, 30, 19, 30, 0), 90, true, lounge));

            List<Event> osawaEvents = events.SearchByPerformer("Osawa High School");

            string[] expected = new string[] { "1", "4" };
            string[] actual = eventIDs(osawaEvents);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestSoldOutShow()
        {
            deathShow.SellTickets(200); //sell out show
            belushiShow.SellTickets(10); //sell some tickets


            Assert.IsTrue(deathShow.IsSoldOut());
            Assert.IsFalse(belushiShow.IsSoldOut());
            Assert.IsFalse(shrek.IsSoldOut());
            //Add Test cases
            grease = new Event("5", "Grease", oshkoshTheatre, 100, 10, new DateTime(2015, 4, 18, 19, 30, 0), 50, true, main);
            events.AddEvent(grease);
            grease.SellTickets(100);
            Assert.IsTrue(grease.IsSoldOut());
        }

        [TestMethod]
        public void TestShortShows()
        {
            midSummer = new Event("4", "Midsummer Night", osawaHigh, 100, 10, new DateTime(2015, 9, 30, 19, 30, 0), 99, true, lounge);
            grease = new Event("5", "Grease", oshkoshTheatre, 100, 10, new DateTime(2015, 4, 18, 19, 30, 0), 50, true, main);
            events.AddEvent(midSummer);
            List<Event> shortShows = events.ShowsShorterThan(100); //Get shows shorter than 100 minutes

            string[] expected = new string[] { "1", "2", "4"};
            string[] actual = eventIDs(shortShows);

            CollectionAssert.AreEqual(expected, actual);
            //Add more test cases

            events.AddEvent(grease);
            List<Event> shortShowsNew = events.ShowsShorterThan(100);
            expected = new string[] { "1", "2", "4", "5" };
            actual = eventIDs(shortShowsNew);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestAvailableTickets()
        {
            deathShow.SellTickets(200); //sell out show
            midSummer = new Event("4", "Midsummer Night", osawaHigh, 100, 10, new DateTime(2015, 2, 28, 19, 30, 0), 99, true, lounge);
            grease = new Event("5", "Grease", oshkoshTheatre, 100, 10, new DateTime(2015, 4, 18, 19, 30, 0), 110, true, main);

            List<Event> openShows = events.OpenShows();

            string[] expected = new string[] { "1", "3" };
            string[] actual = eventIDs(openShows);

            CollectionAssert.AreEqual(expected, actual);
            //Add more test cases
            grease.SellTickets(100);
            midSummer.SellTickets(5);
            events.AddEvent(grease);
            events.AddEvent(midSummer);
            List<Event> openShowsNew = events.OpenShows();
            expected = new string[] { "1", "3", "4" };
            actual = eventIDs(openShowsNew);

            CollectionAssert.AreEqual(expected, actual);

        }

        //Helper method that returns the IDs of all the events in the order they currently appear in the List.
        //This is for testing purposes only
        private string[] eventIDs(List<Event> events)
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
        [TestMethod]
        public void TestLongShows()
        {
            midSummer = new Event("4", "Midsummer Night", osawaHigh, 100, 10, new DateTime(2015, 2, 28, 19, 30, 0), 99, true, lounge);
            grease = new Event("5", "Grease", oshkoshTheatre, 100, 10, new DateTime(2015, 4, 18, 19, 30, 0), 110, true, main);

            List<Event> longShows = events.ShowsLongerThan(100); //Get shows longer than 100 minutes

            string[] expected = new string[] { "3" };
            string[] actual = eventIDs(longShows);

            CollectionAssert.AreEqual(expected, actual);
            //Add more test cases
            events.AddEvent(grease);
            events.AddEvent(midSummer);
            List<Event> longShowsNew = events.ShowsLongerThan(100);

            expected = new string[] { "3", "5" };
            actual = eventIDs(longShowsNew);

            CollectionAssert.AreEqual(expected, actual);
            
        }
        [TestMethod]
        public void TestSearchStage()
        {
            List<Event> mainStage = events.SearchByStage("Main Stage");

            string[] expected = new string[] { "1", "2" };
            string[] actual = eventIDs(mainStage);

            CollectionAssert.AreEqual(expected, actual);
            //test added events
            events.AddEvent(new Event("4", "Midsummer Night", osawaHigh, 100, 10, new DateTime(2015, 2, 28, 19, 30, 0), 99, true, lounge));
            events.AddEvent(new Event("5", "Grease", oshkoshTheatre, 100, 10, new DateTime(2015, 4, 18, 19, 30, 0), 99, true, main));
            List<Event> mainStageNew = events.SearchByStage("Main Stage");

            expected = new string[] { "1", "2", "5" };
            actual = eventIDs(mainStageNew);

            CollectionAssert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestDateRangeSearch()
        {
            DateTime lowDate = new DateTime(2015, 3, 1, 19, 45, 0);
            DateTime highDate = new DateTime(2015, 4, 19, 19, 0, 0);
            
            List<Event> range = events.SearchByDateRange(lowDate, highDate);

            string[] expected = new string[] { "3", "1" };
            string[] actual = eventIDs(range);

            CollectionAssert.AreEqual(expected, actual);

            //test added events
            events.AddEvent(new Event("4", "Midsummer Night", osawaHigh, 100, 10, new DateTime(2015, 2, 28, 19, 30, 0), 99, true, lounge));
            events.AddEvent(new Event("5", "Grease", oshkoshTheatre, 100, 10, new DateTime(2015, 4, 18, 19, 30, 0), 99, true, lounge));
            List<Event> rangeNew = events.SearchByDateRange(lowDate, highDate);

            expected = new string[] { "3", "5", "1" };
            actual = eventIDs(rangeNew);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
