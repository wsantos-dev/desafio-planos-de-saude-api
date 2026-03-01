# Stage 1 - Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Instala EF CLI
ENV PATH="$PATH:/root/.dotnet/tools"

COPY --from=build /out .

EXPOSE 8080

ENTRYPOINT ["dotnet", "PlanosSaude.API.dll"]