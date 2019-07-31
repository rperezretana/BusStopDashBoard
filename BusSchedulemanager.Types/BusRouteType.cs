using BusSchedulemanager.DataAccess.Repositories.Contracts;
using BusSchedulemanager.Database.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusSchedulemanager.Types
{
    public class BusRouteType : ObjectGraphType<BusRoute>
    {
        public BusRouteType(IBusRouteRepository busRouteRepository)
        {
            Field(x => x.Name);
            Field(x => x.NextStop);
            Field(x => x.ForStop);
        }
    }
}
