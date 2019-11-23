# Goals
1) Showing how to order statements effectively using best practices
2) Explain why

# Steps

## Show you added the business project and the docker ignore file
Do it

## Copy the csproj files first
```
COPY ["ExampleWebApp/ExampleWebApp.csproj", "ExampleWebApp/"]
COPY ["ExampleWebApp.Business/ExampleWebApp.Business.csproj", "ExampleWebApp.Business/"]
```

## Restore the nuget packages for the main project
```
RUN dotnet restore "ExampleWebApp/ExampleWebApp.csproj"
```

## Publish the main project with --no-restore
```
RUN dotnet publish "ExampleWebApp/ExampleWebApp.csproj" -c Release -o /app/publish --no-restore
```

## Add the EXPOSE keywords to keep things clear/neat
```
EXPOSE 80
EXPOSE 443
```

## Build the image
```
docker build -f ./ExampleWebApp/Dockerfile -t example-web-app-ordered .
```

## Show how changing only the content not rebuilds the entire image
Change the index.cshtml with them gif!