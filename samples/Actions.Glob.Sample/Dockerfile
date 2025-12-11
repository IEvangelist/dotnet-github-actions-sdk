FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-noble AS build
ARG TARGETARCH
WORKDIR /source

COPY *.props *.targets ./
COPY samples/*.props samples/*.targets ./samples/
COPY samples/Actions.Glob.Sample/Actions.Glob.Sample.csproj samples/Actions.Glob.Sample/
COPY src/*.props src/*.targets ./src/
COPY src/Actions.Core/Actions.Core.csproj src/Actions.Core/
COPY src/Actions.Octokit/Actions.Octokit.csproj src/Actions.Octokit/
COPY src/Actions.Glob/Actions.Glob.csproj src/Actions.Glob/
RUN dotnet restore "samples/Actions.Glob.Sample/Actions.Glob.Sample.csproj" -a $TARGETARCH

COPY . .
RUN dotnet publish "samples/Actions.Glob.Sample/Actions.Glob.Sample.csproj" -a $TARGETARCH --no-restore -o /app

FROM mcr.microsoft.com/dotnet/runtime:10.0-noble-chiseled
WORKDIR /app
COPY --from=build /app .
USER root
ENTRYPOINT ["/app/Actions.Glob.Sample"]
