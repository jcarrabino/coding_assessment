# Use the official .NET 6 SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY quiz.csproj ./
RUN dotnet restore

# Copy the remaining source code
COPY . ./

# Build the application
RUN dotnet publish -c Release -o out

# Use the official .NET 6 runtime image to run the application
FROM mcr.microsoft.com/dotnet/runtime:6.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/out ./

# Expose any required ports (optional, not needed for a console app)

# Set the entry point to run the application
ENTRYPOINT ["dotnet", "quiz.dll"]