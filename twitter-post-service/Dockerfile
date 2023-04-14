FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env 
WORKDIR /src/twitter-post-service

COPY twitter-post-service.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet build
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /src
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "twitter-post-service.dll" ]
