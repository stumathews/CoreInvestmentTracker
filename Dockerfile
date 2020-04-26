#Image(build) that is used to compile/publish ASP.NET Core applications inside the container. 
FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

#Copy BUILD_DIR\*csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image by adding the compiled output above to a runtime image(aspnetcore)

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/out .


# Ask Kestral to listen on 5000
ENV ASPNETCORE_URLS http://*:$PORT
#ENTRYPOINT ["dotnet", "CoreInvestmentTracker.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet CoreInvestmentTracker.dll
