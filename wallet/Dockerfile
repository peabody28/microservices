FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 82

RUN apt update && apt install -y sqlite3

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["wallet/wallet.csproj", "wallet/"]
RUN dotnet restore "wallet/wallet.csproj"
COPY . .
WORKDIR "/src/wallet"
RUN dotnet build "wallet.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "wallet.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD sqlite3 ./db/wallet.db < ./db/init.sql && dotnet wallet.dll
