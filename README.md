# Admin-ka

It's a very simple ASP.Net Core GUI for internal/debug administration/monitoring.
Also there's a demo back-end (on Node.js) for this GUI.

## Getting started

Just run `prepare-and-run.bat`, it will get all things ready, run both apps (client and server) and open http://localhost:5001.

Or you could do it manually:

* to install dependencies for demo back-end go to DemoServer folder and run `npm install`
* to run demo back-end use `npm start`
* to install dependencies for GUI got to Admin-ka folder and run `dotnet restore`
* to build and run GUI use `dotnet run --configuration Release`

If you want to run GUI on a different PC without all that stuff installed (see requirements below) you could use `dotnet publish --configuration Release` to generate a standalone executable.

## Requirements

You must have installed [Node.js](https://nodejs.org/en/download/current), [npm](https://www.hacksparrow.com/install-node-js-and-npm-on-windows.html), [bower](https://bower.io/#install-bower) and [.Net Core SDK](https://www.microsoft.com/net/download/core#/current).

I've got .Net Core SDK 1.1 and .NET Command Line Tools (1.0.0-preview2-1-003177).
