using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.SignalR;
using WebApi.DataAccess;
using WebApi.Models;

namespace WebApi.Hubs
{
    public class RouteStopSchedulesHub: Hub
    {
        private readonly IRouteStopDa _routeStopDa;
        public RouteStopSchedulesHub(IRouteStopDa routeStopDa)
        {
            _routeStopDa = routeStopDa;
        }

        [HubMethodName(HubResources.RouteSchedules)]
        public async Task SendMessage(int StopId)
        {
            //Improvement: Add a Timer to auto send Schedules on a 15 minute timer
            await Groups.AddToGroupAsync(Context.ConnectionId, StopId.ToString());
            
            var results = GetStopSchedules(StopId);
            await Clients.Groups(StopId.ToString()).SendAsync("ServeSchedules", results);
        }

        private IList<SchedulesDm> GetStopSchedules(int stopId)
        {
            return _routeStopDa.GetRouteSchedulesByStopIdAsync(stopId);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            // Improvement
            // await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await base.OnDisconnectedAsync(exception);
        }
    }
}
