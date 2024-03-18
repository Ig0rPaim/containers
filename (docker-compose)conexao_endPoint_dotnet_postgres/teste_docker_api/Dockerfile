# Estágio de construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /App

EXPOSE 5022

# Copia tudo para o diretório de trabalho
COPY . ./

# Restaura as dependências
RUN dotnet restore

# Compila e publica a aplicação
RUN dotnet publish -c Release -o out

# Estágio de execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /App

# Copia os artefatos de compilação do estágio de construção
COPY --from=build-env /App/out .

# Define o ponto de entrada para executar a aplicação
ENTRYPOINT ["dotnet", "teste_docker_api.dll"]
