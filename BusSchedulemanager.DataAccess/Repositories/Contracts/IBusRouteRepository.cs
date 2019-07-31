using BusSchedulemanager.Database.Models;
using System.Collections.Generic;

namespace BusSchedulemanager.DataAccess.Repositories.Contracts
{
    public interface IBusRouteRepository
    {
        IEnumerable<BusRoute> GetAll();
        IEnumerable<BusRoute> GetRoutsForStop(int hour, int minute, int stopId, int howManyStopsInFuture);
        IEnumerable<BusRoute> GetRoutsForAllTheStops(int hour, int minute);
    }
}
