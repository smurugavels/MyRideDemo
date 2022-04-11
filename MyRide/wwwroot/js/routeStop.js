"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("https://localhost:44348/routeSchedulesHub").build();

document.getElementById("getSchedules").disabled = true;

connection.on("ServeSchedules", onSchedulesReceived);

connection.start().then(function () {
    document.getElementById("getSchedules").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("getSchedules").addEventListener("click", function (event) {
    var stopId = document.getElementById("stopId").value;

    connection.invoke("routeSchedules", stopId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

function formatDate(dateString) {
    var dateObj = new Date(dateString);
    var formattedDate = dateObj.getFullYear() + "/" + (dateObj.getMonth() + 1) + "/" + dateObj.getDate();
    var formattedTime = dateObj.getHours() + ":" + dateObj.getMinutes() + ":" + dateObj.getSeconds();
    return formattedDate + " " + formattedTime;
}

function onSchedulesReceived(routeStops) {
    var schedulesList = document.getElementById("schedulesList");
    schedulesList.innerHTML = "";

    var routeCounter = 0;
    routeStops.forEach(data => {
        var li = document.createElement("li");
        li.textContent = "Route " + ++routeCounter;
        schedulesList.appendChild(li);
        var counter = 0;

        var subUl = document.createElement("ul");
        li.appendChild(subUl);
        var subLi = document.createElement("li");
        subLi.textContent = data.routeStopsSchedule
            .map(routeDate => "Service " + ++counter + " ::  " + formatDate(routeDate)).join(",  ");
        subUl.appendChild(subLi);
    });
    routeCounter = 0;
}