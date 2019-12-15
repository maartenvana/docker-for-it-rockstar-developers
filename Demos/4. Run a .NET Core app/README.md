# Goal
The goal of this demo is to show how a web server is run inside of a container

# Steps

## Create docker file
```
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0

WORKDIR /app

COPY out .

ENTRYPOINT [ "dotnet", "Example.dll" ]
```

## Add .dockerignore
```
**/bin
**/obj
**/properties
Dockerfile
*.csproj.user
```

## Publish the project
```
dotnet publish -o out
```

## Build the container
```
docker build -t netcorelocal .
```

## Run the container
```
docker run -it --rm --name netcorecontainer netcorelocal
```

## Cant reach it
Explain that the application is running inside of the container on localhost, or 0.0.0.0 and we cannot just go to the port we need to bind it

## Run the container properly
```
docker run -it --rm -p 5000:80 --name netcorecontainer netcorelocal
```

## This sucks balls, let's improve on it

## Multi-stage that docker file
```
FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as build

WORKDIR /app

COPY . .

RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT [ "dotnet", "Example.dll" ]
```

## Change something
```
Change the Home/Index.cshtml to show that fancy HQ gif
```

## Explain how this improves 2 of things
- You don't need to have a certain version of the SDK installed (on your build agent)
- The final image size is drastically smaller
```
```