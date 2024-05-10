#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Insttantt.StepManagement.Api/Insttantt.StepManagement.Api.csproj", "Insttantt.StepManagement.Api/"]
COPY ["Insttant.StepManagement.Infrastructure/Insttantt.StepManagement.Infrastructure.csproj", "Insttant.StepManagement.Infrastructure/"]
COPY ["Insttantt.StepManagement.Application/Insttantt.StepManagement.Application.csproj", "Insttantt.StepManagement.Application/"]
COPY ["Insttantt.StepManagement.Domain/Insttantt.StepManagement.Domain.csproj", "Insttantt.StepManagement.Domain/"]
RUN dotnet restore "./Insttantt.StepManagement.Api/Insttantt.StepManagement.Api.csproj"
COPY . .
WORKDIR "/src/Insttantt.StepManagement.Api"
RUN dotnet build "./Insttantt.StepManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Insttantt.StepManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Insttantt.StepManagement.Api.dll"]