FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ResupplyStops/ResupplyStops.csproj", "ResupplyStops/"]
RUN dotnet restore "ResupplyStops/ResupplyStops.csproj"
COPY . .
WORKDIR "/src/ResupplyStops"
RUN dotnet build "ResupplyStops.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ResupplyStops.csproj" -c Release -o /app/publish
RUN cp ResupplyStops.xml /app/publish/ResupplyStops.xml

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ResupplyStops.dll"]