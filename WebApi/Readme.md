# Scope for improvement!

## Setup
1. Restore packages from Nuget
2. Code developed in .net Core 5 web-Api
3. RouteStopDa holds generated Data and returns results from Controller
4. 



## Backend Layer: //Improvement
1. Add Caching layer:
    1. Cache Top two Schedules for all route stops given a Time.
    2. Clean up past entries from cache
2. Use BackgroundService to populate Cache
3. Add more test coverage
4. SignalR: With Timer auto send Schedules to connected clients on a 15 minute timer.
5. Remove disconnected clients from SignalR Groups (by StopId)

## Frontend:
1. Add more test coverage

## Additional Instructions: Focus on Backend

## Terms used
1. Route: A Route 
	1. Model name: RouteRm(.cs)
2. Stop: A Bus stop
	1. Model name: StopRm(.cs)
3. RouteStop: A Route that services a stop. 
	1. Model name: RouteStopRm(.cs) 
	2. Sample Data generated using above Model
		|     Route|	1|	     2| 	   3|
		| ----------- | ----------- |----------- |----------- |
	    | Stop 1|	R1S1 |R2S1  |  R3S1|
	    | Stop 2|	R1S2| R2S2  |  R3S2|

4. RouteStopSchedulesRm: A model for GET response for a stop Id to return routes.
5. 

