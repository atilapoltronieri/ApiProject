# Usar uma imagem do SDK do .NET para compilar e publicar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar o arquivo .csproj e restaurar as dependências
COPY ["ApiProject/ApiProject.Api.csproj", "ApiProject/"]

# Restaurar as dependências
RUN dotnet restore "ApiProject/ApiProject.Api.csproj"

# Copiar todo o código da aplicação
COPY . .

# Publicar a aplicação
WORKDIR "/src/ApiProject"
RUN dotnet build "ApiProject.Api.csproj" -c Release -o /app/build

# Publicar o projeto
RUN dotnet publish "ApiProject.Api.csproj" -c Release -o /app/publish

# Usar uma imagem do ASP.NET para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApiProject.Api.dll"]