var express = require('express');

for(var i = 0; i < 4; i++) (function(i) {
    var app = express();
    var fs = require("fs");

    app.get('/stats', function (req, res) {
        fs.readFile( __dirname + "/" + 'stats-' + i + '.json', 'utf8', function (err, data) {
            res.end(data);
            console.log('Send stats');
        });
    })

    app.get('/info', function (req, res) {
        fs.readFile( __dirname + "/" + 'info-' + i + '.json', 'utf8', function (err, data) {
            res.end(data);
            console.log('Send info');
        });
    })

    var server = app.listen(8083 + i, '127.0.0.1', function () {
        var host = server.address().address
        var port = server.address().port

        console.log("Listening at http://%s:%s", host, port)
    });
}) (i);
