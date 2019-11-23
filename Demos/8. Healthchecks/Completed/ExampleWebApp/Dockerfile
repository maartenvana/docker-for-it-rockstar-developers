FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build

WORKDIR /src

COPY ["ExampleWebApp/ExampleWebApp.csproj", "ExampleWebApp/"]
COPY ["ExampleWebApp.Business/ExampleWebApp.Business.csproj", "ExampleWebApp.Business/"]

RUN dotnet restore "ExampleWebApp/ExampleWebApp.csproj"

COPY . .

RUN dotnet publish "ExampleWebApp/ExampleWebApp.csproj" -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS final

RUN apt-get update &&\
    apt-get install curl

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 80
EXPOSE 443

HEALTHCHECK --interval=5s --retries=3 --start-period=5s --timeout=3s \
  CMD curl -f http://localhost || exit 1

ENTRYPOINT ["dotnet", "ExampleWebApp.dll"]