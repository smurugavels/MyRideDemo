using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebApi.Models;

namespace WebApi.DataAccess
{
    public class StopsDataHelper
    {
        public StopsDataHelper()
        {
            Stops = new List<StopRm>();
            for (var i = 1; i < 11; i++)
            {
                Stops.Add(new StopRm
                {
                    Id = i,
                    Name = "Stop " + i
                });
            }
        }

        public IList<StopRm> Stops { get; }
    }
}
