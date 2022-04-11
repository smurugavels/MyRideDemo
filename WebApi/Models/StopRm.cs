using System;

namespace WebApi.Models
{
    //A model that maps SQL master tables Routes and Stops
    //Routes Table > {id: 1, Name: "Route 1"}, {id: 2, Name: "Route 2"}, {id: 2, Name: "Route 3"}
    //Stops Table > {id: 1, Name: "Stop 1"}, {id: 2, Name: "Stop 2"}, .... {id: 10, Name: "Stop 10"}
    public class StopRm
    {
        public int Id { get; set; }
        // public TimeSpan offsetTime { get; set; }
        public string Name { get; set; }
    }
}
