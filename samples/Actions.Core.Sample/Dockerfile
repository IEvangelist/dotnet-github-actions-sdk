#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["samples/Actions.Core.Sample/Actions.Core.Sample.csproj", "samples/Actions.Core.Sample/"]
COPY ["src/Actions.Core/Actions.Core.csproj", "src/Actions.Core/"]
COPY ["src/Actions.Octokit/Actions.Octokit.csproj", "src/Actions.Octokit/"]
RUN dotnet restore "samples/Actions.Core.Sample/Actions.Core.Sample.csproj"
COPY . .
WORKDIR "/src/samples/Actions.Core.Sample"
RUN dotnet build "Actions.Core.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Actions.Core.Sample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Actions.Core.Sample.dll"]