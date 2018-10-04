# Admin-ka

It's a very simple ASP.Net Core GUI for internal administration/monitoring.
Also there's a demo back-end (on Node.js) for this GUI.

It allows you to monitor the whole your system at once. Metrics (from external services) which exceed the configurable thresholds would be highlighted and attract attention.

![admin-ka](https://puu.sh/v0uaD/a9340e1c99.png)

## Getting started

Just run `prepare-and-run.bat` (or `prepare-and-run-ubuntu.sh`), it will get all things ready, run both apps (client and server) and open [default start page](http://localhost:5001).

Or you could do it manually:

* to install dependencies for demo back-end go to DemoServer folder and run `npm install`
* to run demo back-end use `npm start`
* to install dependencies for GUI got to Admin-ka folder and run `dotnet restore`
* to build and run GUI use `dotnet run --configuration Release`

If you want to run GUI on a different PC without all that stuff installed (see requirements below) you could use `dotnet publish --configuration Release` to generate a standalone executable.

## Requirements

You must have installed [Node.js, npm](https://nodejs.org/en/download/current), [bower](https://bower.io/#install-bower) and [.Net Core SDK](https://www.microsoft.com/net/download/core#/current).

I've got .NET Core SDK 2.1.302.
