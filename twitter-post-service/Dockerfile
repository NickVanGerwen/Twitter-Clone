FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env 
WORKDIR /app


# COPY ["twitter-post-service.csproj", "./"]

COPY . ./
RUN dotnet restore
RUN dotnet build
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "twitter-post-service.dll" ]
