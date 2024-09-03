# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . .
RUN dotnet restore
#RUN dotnet publish -c Release -o out
RUN dotnet build -c Release

# Stage 2: Publish
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS publish
WORKDIR /app
COPY --from=build /app .
RUN dotnet publish -c Release -o out

# Stage 3: Final stage - runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/out .
COPY airports.csv /app/airports.csv
EXPOSE 8080
ENTRYPOINT ["dotnet", "FlyCheap.API.dll"]
