FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY VyshBuild.Store.csproj ./
RUN dotnet restore VyshBuild.Store.csproj

COPY *.cs ./
RUN dotnet publish VyshBuild.Store.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:6767
EXPOSE 6767

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "VyshBuild.Store.dll"]
