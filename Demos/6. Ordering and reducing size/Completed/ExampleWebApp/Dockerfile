FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build

WORKDIR /src

COPY ["ExampleWebApp/ExampleWebApp.csproj", "ExampleWebApp/"]
COPY ["ExampleWebApp.Business/ExampleWebApp.Business.csproj", "ExampleWebApp.Business/"]

RUN dotnet restore "ExampleWebApp/ExampleWebApp.csproj"

COPY . .

RUN dotnet publish "ExampleWebApp/ExampleWebApp.csproj" -c Release -o out --no-restore

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS final

WORKDIR /app

COPY --from=build /app/out .

EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "ExampleWebApp.dll"]