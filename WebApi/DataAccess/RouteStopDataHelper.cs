using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using WebApi.Models;

namespace WebApi.DataAccess
{
    public class RouteStopDataHelper
    {
        public RouteStopDataHelper(RoutesDataHelper routesModel, StopsDataHelper stopsModel)
        {
            RouteStops = new List<RouteStopRm>();
            var totalRoutes = routesModel.Routes.Count + 1;
            var totalStops = stopsModel.Stops.Count + 1;
            var counter = 0;
            for (var routeId = 1; routeId < totalRoutes; routeId++)
            {
                var routeName = routesModel.Routes.First(x => x.Id == routeId).Name;
                for (var stopId = 1; stopId < totalStops; stopId++)
                {
                    var stopName = stopsModel.Stops.First(x => x.Id == stopId).Name;
                    RouteStops.Add(new RouteStopRm
                    {
                        Id = ++counter,
                        RouteId = routeId,
                        StopId = stopId,
                        RouteName = routeName,
                        StopName = stopName,
                        Name = $"{routeName}-{stopName}"
                    });
                };
            }
        }

        public IList<RouteStopRm> RouteStops { get; }
    }
}
