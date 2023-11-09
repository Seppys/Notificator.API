FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /notificator-api
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Notificator.API/Notificator.API.csproj", "Notificator.API/"]
RUN dotnet restore "Notificator.API/Notificator.API.csproj"
COPY . .
WORKDIR "/src/Notificator.API"
RUN dotnet build "Notificator.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Notificator.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notificator.API.dll"]