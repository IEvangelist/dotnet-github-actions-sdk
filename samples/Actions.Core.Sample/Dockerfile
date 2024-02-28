FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0-jammy AS build
ARG TARGETARCH
WORKDIR /source

COPY *.props *.targets ./
COPY samples/*.props samples/*.targets ./samples/
COPY samples/Actions.Core.Sample/Actions.Core.Sample.csproj samples/Actions.Core.Sample/
COPY src/*.props src/*.targets ./src/
COPY src/Actions.Core/Actions.Core.csproj src/Actions.Core/
COPY src/Actions.Octokit/Actions.Octokit.csproj src/Actions.Octokit/
RUN dotnet restore "samples/Actions.Core.Sample/Actions.Core.Sample.csproj" -a $TARGETARCH

COPY . .
RUN dotnet publish "samples/Actions.Core.Sample/Actions.Core.Sample.csproj" -a $TARGETARCH --no-restore -o /app

FROM mcr.microsoft.com/dotnet/runtime:8.0-jammy-chiseled
WORKDIR /app
COPY --from=build /app .
USER root
ENTRYPOINT ["/app/Actions.Core.Sample"]
