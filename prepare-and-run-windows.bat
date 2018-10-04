cd DemoServer
cmd /c npm install
start npm start
cd ../Admin-ka
cmd /c dotnet restore
cmd /c dotnet publish --configuration Release -r win10-x64
cd bin\Release\netcoreapp2.1\win10-x64\publish
start Admin-ka.exe
start http://localhost:5001