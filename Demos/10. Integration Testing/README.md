# Goals
1) Show how easy it is to start integration testing your app once its integrated with docker

# Steps

## Show project
Show how I use logging and environment variables so I can run them in my debugger and inside of containers

## Add the dockerfile to integration tests
```
FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build

WORKDIR /src

COPY ["ExampleWebApp.IntegrationTests/ExampleWebApp.IntegrationTests.csproj", "ExampleWebApp.IntegrationTests/"]

RUN dotnet restore "ExampleWebApp.IntegrationTests/ExampleWebApp.IntegrationTests.csproj"

COPY ExampleWebApp.IntegrationTests ExampleWebApp.IntegrationTests

RUN dotnet build "ExampleWebApp.IntegrationTests/ExampleWebApp.IntegrationTests.csproj"

ENTRYPOINT ["dotnet", "test", "ExampleWebApp.IntegrationTests", "--logger", "trx", "--results-directory", "/var/temp"]
```

## Add the docker compose file
```
version: '3.3'

services:
  integration_test:
    build:
      context: .
      dockerfile: ./ExampleWebApp.IntegrationTests/Dockerfile
    environment:
      - BUSINESS_API_URL=http://webapp/api/business
    depends_on:
      - db
      - webapp
```

## Build all the things!
```
docker-compose -f .\docker-compose.yaml -f .\docker-compose.test.yaml build
```

## Up all the things with --abort-on-container-exit
```
docker-compose -f .\docker-compose.yaml -f .\docker-compose.test.yaml up --abort-on-container-exit
docker-compose -f .\docker-compose.yaml -f .\docker-compose.test.yaml up --abort-on-container-exit integration_test
```

## Add another docker-compose file to store the results
```
version: '3.3'

services:
  integration_test:
    volumes:
      - "c:/temp/testresults:/var/temp"
```

## Store your test results
```
docker-compose -f .\docker-compose.yaml -f .\docker-compose.test.yaml -f .\docker-compose.test.win.yaml up --abort-on-container-exit integration_test
```