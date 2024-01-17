dotnet build ..\DAL.EF.Migrator.Console\DAL.EF.Migrator.Console.csproj

if %ERRORLEVEL% NEQ 0 EXIT /B 1

SET currentPath=%~dp0
CD ..

SET pathToConfigs=%cd%\Web.API

CD %currentPath%

CD ..\DAL.EF.Migrator.Console\bin\Debug\net8.0\

dotnet DAL.EF.Migrator.Console.dll pathToConfigs=%pathToConfigs% environmentName=%1% cleanDb=false