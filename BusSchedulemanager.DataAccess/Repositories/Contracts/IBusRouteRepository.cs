using BusSchedulemanager.Database.Models;
using System.Collections.Generic;

namespace BusSchedulemanager.DataAccess.Repositories.Contracts
{
    /// <summary>
    /// Interfaces to use as connections to use dependency injection on the API.
    /// This facilitate implementing unit test.
    /// </summary>
    public interface IBusRouteRepository
    {
        IEnumerable<BusRoute> GetAll();
        IEnumerable<BusRoute> GetRoutsForStop(int hour, int minute, int stopId, int howManyStopsInFuture);
        IEnumerable<BusRoute> GetRoutsForAllTheStops(int hour, int minute);
    }
}
