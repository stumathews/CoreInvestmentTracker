FROM microsoft/dotnet:2.1-sdk-alpine AS build-env
WORKDIR /source
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -r linux-musl-x64 -o /app

FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine
WORKDIR /app
COPY --from=build-env /app .


# Ask Kestral to listen on 5000
ENV ASPNETCORE_URLS http://*:$PORT
ENTRYPOINT ["dotnet", "CoreInvestmentTracker.dll"]
