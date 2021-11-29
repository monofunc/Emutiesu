FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Emutiesu/Emutiesu.csproj", "Emutiesu/"]
RUN dotnet restore "src/Emutiesu/Emutiesu.csproj"
COPY . .
WORKDIR "/src/Emutiesu"
RUN dotnet build "Emutiesu.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Emutiesu.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Emutiesu.dll"]
