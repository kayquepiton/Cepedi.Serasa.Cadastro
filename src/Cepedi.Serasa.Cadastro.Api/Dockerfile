#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Cepedi.Serasa.Cadastro.Api/Cepedi.Serasa.Cadastro.Api.csproj", "src/Cepedi.Serasa.Cadastro.Api/"]
COPY ["src/Cepedi.Serasa.Cadastro.IoC/Cepedi.Serasa.Cadastro.IoC.csproj", "src/Cepedi.Serasa.Cadastro.IoC/"]
COPY ["src/Cepedi.Serasa.Cadastro.Data/Cepedi.Serasa.Cadastro.Data.csproj", "src/Cepedi.Serasa.Cadastro.Data/"]
COPY ["src/Cepedi.Serasa.Cadastro.Domain/Cepedi.Serasa.Cadastro.Domain.csproj", "src/Cepedi.Serasa.Cadastro.Domain/"]
COPY ["src/Cepedi.Serasa.Cadastro.Shared/Cepedi.Serasa.Cadastro.Shared.csproj", "src/Cepedi.Serasa.Cadastro.Shared/"]
RUN dotnet restore "./src/Cepedi.Serasa.Cadastro.Api/Cepedi.Serasa.Cadastro.Api.csproj"
COPY . .
WORKDIR "/src/src/Cepedi.Serasa.Cadastro.Api"
RUN dotnet build "./Cepedi.Serasa.Cadastro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Cepedi.Serasa.Cadastro.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cepedi.Serasa.Cadastro.Api.dll"]