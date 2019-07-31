using BusScheduleManager.Queries;
using GraphQL;

namespace BusScheduleManager.Schema
{
    public class BusManagerSchema : GraphQL.Types.Schema
    {
        public BusManagerSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<BusRouteQuery>();
            //Mutation = resolver.Resolve<BusStopQuery>();
        }
    }
}
