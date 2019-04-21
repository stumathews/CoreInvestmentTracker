FROM microsoft/aspnetcore-build:2.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -r linux-musl-x64 -o out

FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build-env /app/out .


# Ask Kestral to listen on 5000
ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet", "CoreInvestmentTracker.dll"]
