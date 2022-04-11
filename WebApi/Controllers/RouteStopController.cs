using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using WebApi.Hubs;
using WebApi.DataAccess;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    public class RouteStopController : Controller
    {
        private ILogger<string> _logger;
        private readonly IRouteStopDa _routeStopDa;
        private readonly IHubContext<RouteStopSchedulesHub> _schedulesHubContext;
        public RouteStopController(ILogger<string> logger, IRouteStopDa routeStopDa, IHubContext<RouteStopSchedulesHub> schedulesHubContext)
        {
            _logger = logger;
            _routeStopDa = routeStopDa;
            _schedulesHubContext = schedulesHubContext;
        }
        
        [HttpGet("")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetByIdAsync([FromQuery] int id)
        {
            var result = _routeStopDa.GetRouteSchedulesByStopIdAsync(id);
            return Ok(result);
        }
    }
}
