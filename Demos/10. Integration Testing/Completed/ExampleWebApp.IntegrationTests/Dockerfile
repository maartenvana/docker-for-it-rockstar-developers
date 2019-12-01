FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build

WORKDIR /src

COPY ["ExampleWebApp.IntegrationTests/ExampleWebApp.IntegrationTests.csproj", "ExampleWebApp.IntegrationTests/"]

RUN dotnet restore "ExampleWebApp.IntegrationTests/ExampleWebApp.IntegrationTests.csproj"

COPY ExampleWebApp.IntegrationTests ExampleWebApp.IntegrationTests

RUN dotnet build "ExampleWebApp.IntegrationTests/ExampleWebApp.IntegrationTests.csproj"

ENTRYPOINT ["dotnet", "test", "ExampleWebApp.IntegrationTests", "--logger", "trx", "--results-directory", "/var/temp"]

