cd DemoServer
cmd /c npm install
start npm start
cd ..
cd Admin-ka
cmd /c dotnet restore
start dotnet run --configuration Release
cd ..