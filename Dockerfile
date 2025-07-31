FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY /src .
RUN dotnet restore "Calls.Web/Calls.Web.csproj"
RUN dotnet publish "Calls.Web/Calls.Web.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Calls.Web.dll"]