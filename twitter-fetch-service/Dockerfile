FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env 
WORKDIR /app

COPY twitter-fetch-service/twitter-fetch-service.csproj ./twitter-fetch-service/
RUN dotnet restore ./twitter-fetch-service/

COPY . .
RUN dotnet build ./twitter-fetch-service/
RUN dotnet publish ./twitter-fetch-service/ -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "twitter-fetch-service.dll" ]
