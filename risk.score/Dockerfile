FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

RUN apt update && apt install -y sqlite3

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["risk.score/risk.score.csproj", "risk.score/"]
RUN dotnet restore "risk.score/risk.score.csproj"
COPY . .
WORKDIR "/src/risk.score"
RUN dotnet build "risk.score.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "risk.score.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD sqlite3 ./db/risk.score.db < ./db/init.sql && dotnet risk.score.dll