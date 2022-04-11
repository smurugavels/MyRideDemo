using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.DataAccess
{
    public interface IRouteStopDa
    {
        IList<SchedulesDm> GetRouteSchedulesByStopIdAsync(int id);
    }
}
