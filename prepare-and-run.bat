cd DemoServer
cmd /c npm install
start npm start
cd ..
cmd /c dotnet restore
cmd /c dotnet publish --configuration Release
cd Admin-ka\bin\Release\netcoreapp1.1\win7-x64\publish
start Admin-ka.exe
start http://localhost:5001