FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

#Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .

# Expose port 5000 on container to the world outside (container host)
EXPOSE 5000/tcp

# Ask Kestral to listen on 5000
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "CoreInvestmentTracker.dll"]
