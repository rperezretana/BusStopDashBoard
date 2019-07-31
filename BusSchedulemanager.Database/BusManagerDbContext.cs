using BusSchedulemanager.Database.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusSchedulemanager.Database
{
    public class BusManagerDbContext //: DbContext //(inherit any other type of db framework)
    {
        /// <summary>
        /// for a fake database application for now
        /// </summary>
        public BusManagerDbContext()
        {
            Console.WriteLine("Context Initialized");

            BusStops = new List<BusStop>();
            BusRoutes = new List<BusRoute>();

            //Generate the 10 bus stops oobjects.
            for (int indexRoute = 0; indexRoute < 10; indexRoute++)
                BusStops.Add(new BusStop()
                {
                    Id = indexRoute,
                    Name = $"Bus Stop {indexRoute}"
                });

                ///Hardcoding the routs specified in the example.
                BusRoutes.AddRange(new List<BusRoute>() {
                        new BusRoute {
                            Name = "Route One",
                            Id = 1
                        },
                        new BusRoute {
                            Name = "Route Two",
                            Id = 2
                        },
                        new BusRoute {
                            Name = "Route Three",
                            Id = 3
                        }
                });
        }


        /// <summary>
        /// Returns all the bus stops recorded in the database.
        /// </summary>
        public List<BusStop> BusStops { get; set; }
        /// <summary>
        /// Returns all the BusRoutes stored in the database.
        /// </summary>
        public List<BusRoute> BusRoutes { get; set; }

    }
}
