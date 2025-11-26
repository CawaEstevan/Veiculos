#!/bin/bash
cd src/LojaCamisaGamer.Web
dotnet build
dotnet exec ~/.nuget/packages/dotnet-ef/8.0.11/tools/net8.0/any/ef.dll migrations add InitialCreate --project ../LojaCamisaGamer.Infrastructure/LojaCamisaGamer.Infrastructure.csproj
dotnet exec ~/.nuget/packages/dotnet-ef/8.0.11/tools/net8.0/any/ef.dll database update --project ../LojaCamisaGamer.Infrastructure/LojaCamisaGamer.Infrastructure.csproj
