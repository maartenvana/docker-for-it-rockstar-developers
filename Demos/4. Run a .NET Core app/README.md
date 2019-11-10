## Goal
The goal of this demo is to show how a web server is run inside of a container

## Create project
```
dotnet new mvc -n Example
```

## Move to directory
```
cd ./Example
```

## Delete the HTTPS redirection
```
startup.cs
app.UseHttpsRedirection();
```

## Create docker file
```
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0

COPY out .

ENTRYPOINT [ "dotnet", "Example.dll" ]
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
docker run -it --rm --name -p 5000:80 netcorecontainer netcorelocal
```