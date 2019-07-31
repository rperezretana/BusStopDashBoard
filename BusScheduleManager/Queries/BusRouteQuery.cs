using BusSchedulemanager.DataAccess.Repositories.Contracts;
using BusSchedulemanager.Database.Models;
using BusSchedulemanager.Types;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusScheduleManager.Queries
{
    public class BusRouteQuery : ObjectGraphType
    {
        public BusRouteQuery(IBusRouteRepository busRouteRepository)
        {
            Field<ListGraphType<BusRouteType>>("BusStops",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "hour" }, new QueryArgument<IntGraphType> { Name = "minute" }/*, new QueryArgument<IntGraphType> { Name = "stopId" }*/),
                resolve: context => {
                        var hour = context.GetArgument<int>("hour");
                        var minute = context.GetArgument<int>("minute");
                        //var stopId = context.GetArgument<int>("stopId");
                        // return busRouteRepository.GetRoutsForStop(hour, minute, stopId); this could be used in a different implementation to get an specific ID stop, but for the example we skip it.
                        return busRouteRepository.GetRoutsForAllTheStops(hour, minute);
                    });
        }
    }
}
