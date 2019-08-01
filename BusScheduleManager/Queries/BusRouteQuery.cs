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
    /// <summary>
    /// This class takes care of the queries, mutations etc.
    /// </summary>
    public class BusRouteQuery : ObjectGraphType
    {
        public BusRouteQuery(IBusRouteRepository busRouteRepository)
        {
            Field<ListGraphType<BusRouteType>>("BusStops",
                //arguments for the current query.
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "hour" }, new QueryArgument<IntGraphType> { Name = "minute" }),
                resolve: context => {
                        var hour = context.GetArgument<int>("hour");
                        var minute = context.GetArgument<int>("minute");
                        return busRouteRepository.GetRoutsForAllTheStops(hour, minute);
                    });
        }
    }
}
