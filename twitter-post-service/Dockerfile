FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env 
# FROM mcr.microsoft.com/dotnet/nightly/sdk:8.0-preview
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN build --platform linux/amd64 -t app .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "twitter-post-service.dll" ]
