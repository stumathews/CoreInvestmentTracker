FROM microsoft/dotnet:2.0.0-preview1-runtime

WORKDIR /app

COPY . .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet CoreInvestmentTracker.dll
