using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    public class RouteStopSchedulesDm
    {
        public int RouteStopId { get; set; }
        public List<DateTime> Schedules { get; set; }
    }
}
