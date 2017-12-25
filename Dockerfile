FROM microsoft/aspnetcore-build:2.0 AS build-env
COPY src /app
WORKDIR /app

RUN dotnet restore --configfile ../NuGet.Config
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/coreinvestmenttracker/out .
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "CoreInvestmentTracker.dll"]


###
#FROM microsoft/aspnetcore-build:2.0 AS build-env
#
#WORKDIR /app
#
# Copy csproj and restore as distinct layers
#COPY *.csproj ./
#RUN dotnet restore
#
# Copy everything else and build
#COPY . ./
#RUN dotnet publish -c Release -o out
#
# Build runtime image
#FROM microsoft/aspnetcore:2.0
#WORKDIR /app
#COPY --from=build-env /app/out .
#ENTRYPOINT ["dotnet", "CoreInvestmentTracker.dll"]
