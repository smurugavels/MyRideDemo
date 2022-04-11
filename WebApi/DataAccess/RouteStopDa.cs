using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WebApi.Models;

namespace WebApi.DataAccess
{
    public class RouteStopDa: IRouteStopDa
    {
        private IDictionary<int, string[]> _routeStop;
        private readonly IDictionary<string, IList<long>> _routeStopSchedules;

        public RouteStopDa()
        {
            _routeStopSchedules = new Dictionary<string, IList<long>>();
            GenerateRouteStopTable();
            GenerateSchedulesData();
        }

        public Dictionary<string, IList<long>> GetSchedulesByStopIdAsync(int id)
        {
            var stops = _routeStop[id];
            return  _routeStopSchedules.Where(x => stops.Contains(x.Key)).ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public IList<SchedulesDm> GetRouteSchedulesByStopIdAsync(int id)
        {
            var schedules = GetSchedulesByStopIdAsync(id);

            var currentDate = DateTime.Today;
            var minutes = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, 0).Ticks;

            //debug
            // var time = new TimeSpan(23, 58, 0).Ticks;

            var result = new List<SchedulesDm>();


            foreach (var keyValuePair in schedules)
            {
                //Alternate approach: compute skip option for a known set of schedules, say for every 15 minutes schedules = 4* hour
                //Note: This approach is bit risky. Using Where filter is safest
                // var data = keyValuePair.Value.skip(computeScheduleSkips).Take(2);

                var routeSchedules = keyValuePair.Value.Where(x => x >= minutes).Take(2).ToList();
                //debug
                // var routeSchedules = keyValuePair.Value.Where(x => x >= time).Take(2).ToList();

                //the last schedule spans to next day
                if (routeSchedules.Count < 2)
                {
                    //Select the first schedule for that Route / Stop
                    //Add one day to represent next day
                    routeSchedules.Add(keyValuePair.Value.First() + TimeSpan.TicksPerDay);
                }

                //Get schedules by adding Ticks to Current Date for a RouteStop
                var collection = routeSchedules.Select(x => currentDate.AddTicks(x)).ToList();
                result.Add(new SchedulesDm
                {
                    RouteName = keyValuePair.Key,
                    RouteStopsSchedule = collection
                });
            }
            return result;
        }

        /// <summary>
        /// Data Generator: Route Table. 
        /// </summary>
        private void GenerateRouteStopTable()
        {
            _routeStop = new Dictionary<int, string[]>//stopId, RouteN-StopN Mapped Table
            {
                {1, new[] {"R1S1", "R2S1", "R3S1"}},
                {2, new[] {"R1S2", "R2S2", "R3S2"}},
                {3, new[] {"R1S3", "R2S3", "R3S3"}},
                {4, new[] {"R1S4", "R2S4", "R3S4"}},
                {5, new[] {"R1S5", "R2S5", "R3S5"}},
                {6, new[] {"R1S6", "R2S6", "R3S6"}},
                {7, new[] {"R1S7", "R2S7", "R3S7"}},
                {8, new[] {"R1S8", "R2S8", "R3S8"}},
                {9, new[] {"R1S9", "R2S9", "R3S9"}},
                {10, new[] {"R1S10", "R2S10", "R3S10"}}
            };
        }

        /// <summary>
        /// Data Generator: Route Stop Schedules Table. 
        /// </summary>
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
            const int routesServed = 3;
            const int totalStops = 10;

            for (var routes = 0; routes < routesServed; routes++)
            {
                for (var stops = 0; stops < totalStops; stops++)
                {
                    var routeSchedule = _routeStop[stops + 1];
                    var schedules = new List<long>();

                    for (var service = 0; service < servicesPerDay; service++)
                    {
                        schedules.Add(currentTime.Ticks);
                        currentTime += nextService;//adds 15 minutes to a time
                    }
                    //Stop 1	0:00	0:15	0:30	0:45	1:00...
                    //Stop 2  0:02    0:17    0:32    0:47    1:02...
                    _routeStopSchedules.Add(routeSchedule[routes], schedules); //routeSchedule[routes] provides route/stop mapping name from above

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
