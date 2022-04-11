using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models;

namespace WebApi.DataAccess
{
    public class SchedulesDataHelper
    {
        private readonly RouteStopDataHelper _routeStopData;
        public SchedulesDataHelper(RouteStopDataHelper routeStopData)
        {
            _routeStopData = routeStopData;
            Schedules = new List<RouteStopRm>();
            SchedulesV2 = new ConcurrentDictionary<int, IList<SchedulesDm>>();
            SchedulesV3 = new List<SchedulesDm>();
            GenerateSchedulesData();
        }

        public IList<RouteStopRm> Schedules { get; }
        public IList<SchedulesDm> SchedulesV3 { get; }
        public IDictionary<int, IList<SchedulesDm>> SchedulesV2 { get; }

        private void GenerateSchedulesData()
        {
            var startTime = new TimeSpan(0, 0, 0, 0);
            var currentTime = new TimeSpan(0, 0, 0, 0);

            var nextService = new TimeSpan(0, 0, 15, 0);
            var twoMinutesSpan = new TimeSpan(0, 0, 2, 0);
            var nextStopServiceTime = twoMinutesSpan;

            //One day = 24  hours
            //Each stop was serviced 15 minutes apart in one hour = 4 

            const int servicesPerDay = 24 * 4; //96
            var routesServed = _routeStopData.RouteStops.Select(x => x.RouteId).Distinct().Count() + 1;
            var totalStops = _routeStopData.RouteStops.Select(x => x.StopId).Distinct().Count() + 1;
            var identityKey = 0;
            for (var route = 1; route < routesServed; route++)
            {
                for (var stop = 1; stop < totalStops; stop++)
                {
                    var routeSchedule = _routeStopData.RouteStops.First(x => x.StopId == stop && x.RouteId == route);
                    // var schedules = new List<SchedulesDm>();

                    for (var service = 0; service < servicesPerDay; service++)
                    {
                        SchedulesV3.Add(new SchedulesDm
                        {
                            Id = ++identityKey,
                            RouteStopId = routeSchedule.Id,
                            RouteStopSchedules = currentTime.Ticks
                        });
                        currentTime += nextService;//adds 15 minutes to a time
                    }
                    //Stop 1	0:00	0:15	0:30	0:45	1:00...
                    //Stop 2  0:02    0:17    0:32    0:47    1:02...
                    // SchedulesV2.Add(routeSchedule.Id, schedules); //routeSchedule[routes] provides route/stop mapping name from above

                    //Route N and Stop N Schedule is now generated, so prep for Route N and Stop N +1
                    currentTime = startTime.Add(nextStopServiceTime);
                    nextStopServiceTime = nextStopServiceTime.Add(twoMinutesSpan);
                }

                //Route N and Stop 1 to 10 Schedules are now generated, so prep for Route N+1 data generation
                startTime = startTime.Add(twoMinutesSpan);
                currentTime = startTime;
                nextStopServiceTime = twoMinutesSpan;
            }
        }
        private void GenerateSchedulesDataV1()
        {
            IDictionary<string, IList<long>> routeStopSchedulesV2 = new ConcurrentDictionary<string, IList<long>>();
            var startTime = new TimeSpan(0, 0, 0, 0);
            var currentTime = new TimeSpan(0, 0, 0, 0);

            var nextService = new TimeSpan(0, 0, 15, 0);
            var twoMinutesSpan = new TimeSpan(0, 0, 2, 0);
            var nextStopServiceTime = twoMinutesSpan;

            //One day = 24  hours
            //Each stop was serviced 15 minutes apart in one hour = 4 

            const int servicesPerDay = 24 * 4; //96
            var routesServed = _routeStopData.RouteStops.Select(x => x.RouteId).Distinct().Count() + 1;
            var totalStops = _routeStopData.RouteStops.Select(x => x.StopId).Distinct().Count() + 1;
            var counter = 0;
            for (var route = 1; route < routesServed; route++)
            {
                for (var stop = 1; stop < totalStops; stop++)
                {
                    var routeSchedule = _routeStopData.RouteStops.First(x => x.StopId == stop && x.RouteId == route);
                    var schedules = new List<long>();

                    for (var service = 0; service < servicesPerDay; service++)
                    {
                        schedules.Add(currentTime.Ticks);
                        currentTime += nextService;//adds 15 minutes to a time
                    }
                    //Stop 1	0:00	0:15	0:30	0:45	1:00...
                    //Stop 2  0:02    0:17    0:32    0:47    1:02...
                    routeStopSchedulesV2.Add(routeSchedule.Name, schedules); //routeSchedule[routes] provides route/stop mapping name from above

                    //Route N and Stop N Schedule is now generated, so prep for Route N and Stop N +1
                    currentTime = startTime.Add(nextStopServiceTime);
                    nextStopServiceTime = nextStopServiceTime.Add(twoMinutesSpan);
                }

                //Route N and Stop 1 to 10 Schedules are now generated, so prep for Route N+1 data generation
                startTime = startTime.Add(twoMinutesSpan);
                currentTime = startTime;
                nextStopServiceTime = twoMinutesSpan;
            }
        }
    }
}
