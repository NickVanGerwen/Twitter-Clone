FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env 
WORKDIR /app

COPY twitter-post-service/twitter-post-service.csproj ./twitter-post-service/
RUN dotnet restore ./twitter-post-service/

COPY . .
RUN dotnet build ./twitter-post-service/
RUN dotnet publish ./twitter-post-service/ -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "twitter-post-service.dll" ]
