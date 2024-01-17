FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DAL.Migrator.Console/DAL.Migrator.Console.csproj", "DAL.Migrator.Console/"]
RUN dotnet restore "DAL.Migrator.Console/DAL.Migrator.Console.csproj"
COPY . .
WORKDIR "/src/DAL.Migrator.Console"
RUN dotnet build "DAL.Migrator.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DAL.Migrator.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DAL.Migrator.Console.dll"]
