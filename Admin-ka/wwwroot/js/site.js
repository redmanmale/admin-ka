"use strict";

var connection = new signalR.HubConnectionBuilder()
    .withUrl("/adminHub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

connection.on("pushStats", function (dict) {
    for (var key in Object.keys(dict)) {
        var value = dict[key];

        /* use key/value for intended purpose */
    }
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});
