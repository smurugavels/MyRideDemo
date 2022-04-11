using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class SchedulesDm
    {
        public string RouteName { get; set; }
        public List<DateTime> RouteStopsSchedule { get; set; }
    }
}
