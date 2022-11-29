#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Microsoft.GitHub.SampleAction/Microsoft.GitHub.SampleAction.csproj", "Microsoft.GitHub.SampleAction/"]
RUN dotnet restore "Microsoft.GitHub.SampleAction/Microsoft.GitHub.SampleAction.csproj"
COPY . .
WORKDIR "/src/Microsoft.GitHub.SampleAction"
RUN dotnet build "Microsoft.GitHub.SampleAction.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microsoft.GitHub.SampleAction.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microsoft.GitHub.SampleAction.dll"]