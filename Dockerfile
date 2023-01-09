FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /source

COPY KlondikeApp.sln .
COPY KlondikeLibrary/*.csproj ./KlondikeLibrary/
COPY KlondikeTests/*.csproj ./KlondikeTests/
COPY KlondikeWebApi/*.csproj ./KlondikeWebApi/
RUN dotnet restore

COPY KlondikeLibrary/. ./KlondikeLibrary/
COPY KlondikeTests/. ./KlondikeTests/
COPY KlondikeWebApi/. ./KlondikeWebApi/
WORKDIR /source/KlondikeWebApi
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app ./
EXPOSE 80
ENTRYPOINT ["dotnet", "KlondikeWebApi.dll"]
