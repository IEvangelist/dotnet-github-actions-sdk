#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["samples/Actions.Glob.Sample/Actions.Glob.Sample.csproj", "samples/Actions.Glob.Sample/"]
COPY ["src/Actions.Core/Actions.Core.csproj", "src/Actions.Core/"]
COPY ["src/Actions.GitHub/Actions.GitHub.csproj", "src/Actions.GitHub/"]
COPY ["src/Actions.Glob/Actions.Glob.csproj", "src/Actions.Glob/"]
RUN dotnet restore "samples/Actions.Glob.Sample/Actions.Glob.Sample.csproj"
COPY . .
WORKDIR "/src/samples/Actions.Glob.Sample"
RUN dotnet build "Actions.Glob.Sample.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Actions.Glob.Sample.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Actions.Glob.Sample.dll"]