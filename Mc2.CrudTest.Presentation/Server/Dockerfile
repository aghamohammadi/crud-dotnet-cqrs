# Base image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files for restoring dependencies
COPY ["Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj", "Mc2.CrudTest.Presentation/Server/"]
COPY ["Mc2.CrudTest.Presentation/Client/Mc2.CrudTest.Presentation.Client.csproj", "Mc2.CrudTest.Presentation/Client/"]
COPY ["Mc2.CrudTest.Presentation/Shared/Mc2.CrudTest.Presentation.Shared.csproj", "Mc2.CrudTest.Presentation/Shared/"]
COPY ["Mc2.CrudTest.Application/Mc2.CrudTest.Application.csproj", "Mc2.CrudTest.Application/"]
COPY ["Mc2.CrudTest.Infrastructure/Mc2.CrudTest.Infrastructure.csproj", "Mc2.CrudTest.Infrastructure/"]
COPY ["Mc2.CrudTest.Domain/Mc2.CrudTest.Domain.csproj", "Mc2.CrudTest.Domain/"]

# Restore dependencies
RUN dotnet restore "Mc2.CrudTest.Presentation/Server/Mc2.CrudTest.Presentation.Server.csproj"

# Copy the remaining source code
COPY . .

# Build the project
WORKDIR "/src/Mc2.CrudTest.Presentation/Server"
RUN dotnet build "Mc2.CrudTest.Presentation.Server.csproj" -c Release -o /app/build

# Publish the server application to the root folder
FROM build AS publish-server
RUN dotnet publish "Mc2.CrudTest.Presentation.Server.csproj" -c Release -o /app/publish

# Publish the client application to the /client folder
FROM build AS publish-client
WORKDIR "/src/Mc2.CrudTest.Presentation/Client"
RUN dotnet publish "Mc2.CrudTest.Presentation.Client.csproj" -c Release -o /app/client

# Final stage
FROM base AS final
WORKDIR /app

# Copy the published server application to the root folder
COPY --from=publish-server /app/publish .


# Set the entry point for the application
ENTRYPOINT ["dotnet", "Mc2.CrudTest.Presentation.Server.dll"]
