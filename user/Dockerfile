FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 81

RUN apt update && apt install -y sqlite3

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["user/user.csproj", "user/"]
RUN dotnet restore "user/user.csproj"
COPY . .
WORKDIR "/src/user"
RUN dotnet build "user.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "user.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD sqlite3 ./db/user.db < ./db/init.sql && dotnet user.dll
