#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/NeZoviReg.WebApi/NeZoviReg.WebApi.csproj", "src/NeZoviReg.WebApi/"]
COPY ["src/NeZoviReg.Abstractions/NeZoviReg.Abstractions.csproj", "src/NeZoviReg.Abstractions/"]
COPY ["src/NeZoviReg.Domain/NeZoviReg.Domain.csproj", "src/NeZoviReg.Domain/"]
COPY ["src/NeZoviReg.Auth/NeZoviReg.Auth.csproj", "src/NeZoviReg.Auth/"]
COPY ["src/NeZoviReg.Composition/NeZoviReg.Composition.csproj", "src/NeZoviReg.Composition/"]
COPY ["src/NeZoviReg.Application/NeZoviReg.Application.csproj", "src/NeZoviReg.Application/"]
COPY ["src/NeZoviReg.Persistence.Ef/NeZoviReg.Persistence.Ef.csproj", "src/NeZoviReg.Persistence.Ef/"]
RUN dotnet restore "src/NeZoviReg.WebApi/NeZoviReg.WebApi.csproj"
COPY . .
WORKDIR "/src/src/NeZoviReg.WebApi"
RUN dotnet build "NeZoviReg.WebApi.csproj" -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS migration
WORKDIR /src
COPY ["src/NeZoviReg.Migrations.Ef/NeZoviReg.Migrations.Ef.csproj", "src/NeZoviReg.Migrations.Ef/"]
RUN dotnet restore "src/NeZoviReg.Migrations.Ef/NeZoviReg.Migrations.Ef.csproj"
COPY . .
WORKDIR "/src/src/NeZoviReg.Migrations.Ef"
RUN dotnet build "NeZoviReg.Migrations.Ef.csproj" -c Release -o /app/migration


FROM build AS publish
RUN dotnet publish "NeZoviReg.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=migration /app/migration .
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NeZoviReg.WebApi.dll"]