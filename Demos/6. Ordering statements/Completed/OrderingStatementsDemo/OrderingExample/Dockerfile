FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["OrderingExample/OrderingExample.csproj", "OrderingExample/"]
COPY ["ClassLibrary1/ClassLibrary1.csproj", "ClassLibrary1/"]
RUN dotnet restore "OrderingExample/OrderingExample.csproj"
COPY . .
WORKDIR /src/OrderingExample
RUN dotnet build "OrderingExample.csproj" -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish "OrderingExample.csproj" -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderingExample.dll"]