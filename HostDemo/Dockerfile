# Use the official .NET SDK image to build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY HostDemo.csproj ./
RUN dotnet restore

# Copy the remaining files and build
COPY . ./
RUN dotnet publish -c Release -o out

# Use the ASP.NET runtime to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "HostDemo.dll"]
