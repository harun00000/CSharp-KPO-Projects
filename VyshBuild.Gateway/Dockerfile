FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY VyshBuild.Gateway.csproj ./
RUN dotnet restore VyshBuild.Gateway.csproj

COPY *.cs ./
RUN dotnet publish VyshBuild.Gateway.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:6765
EXPOSE 6765

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "VyshBuild.Gateway.dll"]
