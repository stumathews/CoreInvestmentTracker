#Image(build) that is used to compile/publish ASP.NET Core applications inside the container. 
FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

#Copy BUILD_DIR\*csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
CMD dotnet publish -c Release -o out
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CoreInvestmentTracker.dll

# Build runtime image by adding the compiled output above to a runtime image(aspnetcore)

#FROM microsoft/aspnetcore:2.0
#WORKDIR /app
#COPY --from=build-env /app/out .

# Expose port 5000 on container to the world outside (container host)
#EXPOSE $PORT/tcp

# Ask Kestral to listen on 5000
#ENV ASPNETCORE_URLS http://*:$PORT
#ENTRYPOINT ["dotnet", "CoreInvestmentTracker.dll"]
