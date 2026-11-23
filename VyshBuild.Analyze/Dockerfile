FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY VyshBuild.Analyze.csproj ./
RUN dotnet restore VyshBuild.Analyze.csproj

COPY *.cs ./
RUN dotnet publish VyshBuild.Analyze.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app

ENV ASPNETCORE_URLS=http://0.0.0.0:6766
EXPOSE 6766

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "VyshBuild.Analyze.dll"]
