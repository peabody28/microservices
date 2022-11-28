FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

RUN apt update && apt install -y sqlite3

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["payment/payment.csproj", "payment/"]
RUN dotnet restore "payment/payment.csproj"
COPY . .
WORKDIR "/src/payment"
RUN dotnet build "payment.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "payment.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD sqlite3 ./db/payment.db < ./db/init.sql && dotnet payment.dll
