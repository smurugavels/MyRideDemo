using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    //A model that maps SQL master tables Routes and Stops
    //Routes Table > {id: 1, Name: "Route 1"}, {id: 2, Name: "Route 2"}, {id: 2, Name: "Route 3"}
    //Stops Table > {id: 1, Name: "Stop 1"}, {id: 2, Name: "Stop 2"}, .... {id: 10, Name: "Stop 10"}

    //Route	    1	  2 	  3
    // Stop 1	R1S1 R2S1    R3S1
    // Stop 2	R1S2 R2S2    R3S2
    // Stop 3	R1S3 R2S3    R3S3
    // Stop 4	R1S4 R2S4    R3S4
    // Stop 5	R1S5 R2S5    R3S5
    // Stop 6	R1S6 R2S6    R3S6
    // Stop 7	R1S7 R2S7    R3S7
    // Stop 8	R1S8 R2S8    R3S8
    // Stop 9	R1S9 R2S9    R3S9
    // Stop 10	R1S10 R2S10   R3S10

    public class RouteStopRm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RouteId { get; set; }
        public int StopId { get; set; }
        public string StopName { get; set; }//Needed for client
        public string RouteName { get; set; }//Needed for client
        public List<DateTime> RouteStopsSchedule { get; set; }
    }
}
