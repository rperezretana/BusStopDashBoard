using BusSchedulemanager.DataAccess.Repositories.Contracts;
using BusSchedulemanager.Database;
using BusSchedulemanager.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusSchedulemanager.DataAccess.Repositories
{
    public class BusRouteRepository : IBusRouteRepository
    {

        /// <summary>
        /// Each stop is serviced every 15 minutes per route.
        /// This shoul be in a configuration file oir a constants file.
        /// </summary>
        private int SERVICED_EVERY_X_MINUTES = 15;
        private int TIME_BETWEEN_STOPS = 2;

        private readonly BusManagerDbContext _dbContext;

        public BusRouteRepository(BusManagerDbContext dbContext)
        {
            _dbContext = dbContext;
            RoutsSchedule = new List<BusRoute>[_dbContext.BusStops.Count];
            CalculatedRoutsCached = new Dictionary<string, List<BusRoute>>();

            //If the solution reqired is the fastest on execution for an improved user experience, 
            //this will help to transform each request in to a O(1) after the set up is done.
            // So we set up the schedules in a dictionary based on 15 minute chunks.
            //so when the users request the information, everything will be calcualted faster and from memory.
            //depending of the scalability expected this solution may not work
            var stopIndex = 0;
            foreach (var stop in _dbContext.BusStops)
            {
                var nextStops = new List<BusRoute>();
                var routeIndex = 0;
                foreach (var route in _dbContext.BusRoutes)
                {
                    if (RoutsSchedule[stopIndex] == null)
                    {
                        RoutsSchedule[stopIndex] = new List<BusRoute>();
                    }
                    RoutsSchedule[stopIndex].Add(route);
                    routeIndex++;
                }
                stopIndex++;
            }
        }
        public IEnumerable<BusRoute> GetAll()
        {
            return _dbContext.BusRoutes;
        }

        /// <summary>
        /// This caches only the schedule.
        /// </summary>
        private static List<BusRoute>[] RoutsSchedule { get; set; }

        /// <summary>
        /// This caches by time specific, just to make faster the most common searched hours, like the rush hour.
        /// </summary>
        private static Dictionary<string, List<BusRoute>> CalculatedRoutsCached{get;set;}

        /// <summary>
        /// Get a routes for one specific Stop
        /// </summary>
        /// <param name="stopId"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <param name="stopNumber">The stop number</param>
        /// <param name="howManyStopsInFuture">How many stops want to be calculated for the future.</param>
        /// <returns></returns>
        public IEnumerable<BusRoute> GetRoutsForStop(int hour, int minute, int stopNumber, int howManyStopsInFuture)
        {
            //we add up the total minuntes of the day.
            var totalMinutes = hour * 60 + minute ;
            var keyDictionary = $"{totalMinutes}_{stopNumber}";

            //This will save time/cpu on high traffic hours. Avoiding to recalculate.
            if (CalculatedRoutsCached.ContainsKey(keyDictionary))
                return CalculatedRoutsCached[keyDictionary];

            //Using the minutes we get the hash index of the schedule
            var stopIndex = GetStopIndex(totalMinutes);
            //create the list result to be returned.
            var result = new List<BusRoute>();

            for(int routeIndex = 0; routeIndex < _dbContext.BusRoutes.Count; routeIndex++)
            {
                var stopInList = RoutsSchedule[stopIndex][routeIndex];
                //The next is the calcuation of the minutes left for the next route, based on the current time, current Route, SERVICED_EVERY_X_MINUTES and TIME_BETWEEN_STOPS
                //This could be calculated by route, independenly too, if each rout had different numbers or per stop.
                //It would be necessary to store them in the DB but easily accesible too.
                var calculatedDifference = (SERVICED_EVERY_X_MINUTES - (totalMinutes % SERVICED_EVERY_X_MINUTES) + (routeIndex * TIME_BETWEEN_STOPS) + (stopNumber * TIME_BETWEEN_STOPS)) % SERVICED_EVERY_X_MINUTES;

                //For the sake of te exaple a simple calculation of the second arrival time.
                //This could be different based on howManyStopsInFuture, I just decided to take only 2 as requested.
                var listOfStops = new List<int>() { calculatedDifference, calculatedDifference + SERVICED_EVERY_X_MINUTES };
                
                // I create a new object to prevent to modify the original, for instance, if we return the original and modify "NextStop" 
                // when being used by 2 users at different hours it could have undesired consequences. 
                // A user could see the value that was intended for other user/time, depends of the usage or design.
                result.Add(new BusRoute {
                    Id = stopInList.Id,
                    Name =  stopInList.Name,
                    NextStop = listOfStops, //difference in time calculated ( next arrivals)
                    ForStop = stopNumber +1 //for the display index+1
                });
            }

            return CalculatedRoutsCached[keyDictionary] = result;
        }


        /// <summary>
        /// This returns all the routes for all the stops.
        /// </summary>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        public IEnumerable<BusRoute> GetRoutsForAllTheStops(int hour, int minute)
        {
            var allCurrentSchedule = new List<BusRoute>();
            //if the stops dont cahnge regularly this could be cached too.
            foreach (var bStop in _dbContext.BusStops)
            {
                allCurrentSchedule.AddRange(GetRoutsForStop(hour, minute, bStop.Id, 2 /*2 stops*/));
            }
            return allCurrentSchedule;
        }

        private int GetStopIndex(int totalMinutes)
        {
            //The maximum value in the matrix we are going to use.
            var maxThreshold = SERVICED_EVERY_X_MINUTES * _dbContext.BusStops.Count;
            totalMinutes = totalMinutes % maxThreshold;

            //Depending of the number of routes we treat it as columns.
            return totalMinutes / SERVICED_EVERY_X_MINUTES; //number from 0 to 10 for the currrent 10 stops.
        }

    }
}
