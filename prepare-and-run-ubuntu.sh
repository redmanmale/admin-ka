 #!/bin/bash
x-terminal-emulator -e bash -c 'cd DemoServer; npm install; npm start; read line' &
x-terminal-emulator -e bash -c 'cd Admin-ka; dotnet restore; dotnet publish --configuration Release -r ubuntu.16.04-x64; cd bin/Release/netcoreapp1.1/ubuntu.16.04-x64/publish; ./Admin-ka read line' &
xdg-open http://localhost:5001