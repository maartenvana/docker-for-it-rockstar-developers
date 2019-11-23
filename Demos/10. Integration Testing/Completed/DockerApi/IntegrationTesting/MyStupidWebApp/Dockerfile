FROM mcr.microsoft.com/dotnet/core/sdk:3.0 as build

WORKDIR /app

COPY . .

RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT [ "dotnet", "MyStupidWebApp.dll" ]