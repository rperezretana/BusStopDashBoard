using BusSchedulemanager.DataAccess.Repositories.Contracts;
using BusSchedulemanager.Database;
using BusSchedulemanager.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusSchedulemanager.DataAccess.Repositories
{
    public class BusStopRepository : IBusStopRepository
    {
        private readonly BusManagerDbContext _dbContext;

        public BusStopRepository(BusManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<BusStop> GetAll()
        {
            return _dbContext.BusStops;
        }
    }
}
