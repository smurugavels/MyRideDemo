using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApi.Models;

namespace WebApi.DataAccess
{
    public class RoutesDataHelper
    {
        public RoutesDataHelper()
        {
            Routes = new List<RoutesRm>
            {
                new()
                {
                    Id = 1,
                    Name = "Route 1",
                    StartTime = TimeSpan.Zero,
                    isActive = true
                },
                new()
                {
                    Id = 2,
                    Name = "Route 2",
                    StartTime = TimeSpan.FromMinutes(2),
                    isActive = true
                },
                new()
                {
                    Id = 3,
                    Name = "Route 3",
                    StartTime = TimeSpan.FromMinutes(4),
                    isActive = true
                }
            };
        }

        public IList<RoutesRm> Routes { get; }
    }
}
