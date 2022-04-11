using System;

namespace WebApi.Models
{
    public class RoutesRm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public bool isActive { get; set; }
    }
}
