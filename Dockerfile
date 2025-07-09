FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY server/JaMoveo.csproj ./server/
RUN dotnet restore ./server/JaMoveo.csproj

COPY server/. ./server/
WORKDIR /src/server
RUN dotnet publish JaMoveo.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .

COPY server/app.db /app/

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "JaMoveo.dll"]
