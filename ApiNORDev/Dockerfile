# Étape 1 : Construire l'application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copier le fichier .csproj et restaurer les dépendances
COPY ["ApiNORDev/ApiNORDev.csproj", "ApiNORDev/"]
RUN dotnet restore "ApiNORDev/ApiNORDev.csproj"

# Copier le reste du code source et publier l'application
COPY . .
WORKDIR "/app/ApiNORDev"
RUN dotnet publish "ApiNORDev.csproj" -c Release -o /app/publish

# Étape 2 : Créer l'image finale pour l'exécution
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
COPY --from=build /app/publish .

# Démarrer l'application
ENTRYPOINT ["dotnet", "ApiNORDev.dll"]
