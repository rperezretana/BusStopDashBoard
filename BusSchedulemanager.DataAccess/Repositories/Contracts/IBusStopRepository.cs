using BusSchedulemanager.Database.Models;
using System.Collections.Generic;

namespace BusSchedulemanager.DataAccess.Repositories.Contracts
{
    public interface IBusStopRepository
    {
        IEnumerable<BusStop> GetAll();
    }
}
