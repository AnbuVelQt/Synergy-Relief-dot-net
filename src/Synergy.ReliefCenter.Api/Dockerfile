#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
ARG NUGET_SRC
ARG NUGET_NS
ARG NUGET_USER
ARG NUGET_SECRET
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
ARG NUGET_SRC
ARG NUGET_NS
ARG NUGET_USER
ARG NUGET_SECRET
WORKDIR /src
COPY ["src/Synergy.ReliefCenter.Api/Synergy.ReliefCenter.Api.csproj", "src/Synergy.ReliefCenter.Api/"]
COPY ["src/Synergy.AdobeSign/Synergy.AdobeSign.csproj", "src/Synergy.AdobeSign/"]
COPY ["src/Synergy.ReliefCenter.Services/Synergy.ReliefCenter.Services.csproj", "src/Synergy.ReliefCenter.Services/"]
COPY ["src/Synergy.ReliefCenter.Data/Synergy.ReliefCenter.Data.csproj", "src/Synergy.ReliefCenter.Data/"]
COPY ["src/Synergy.ReliefCenter.Core/Synergy.ReliefCenter.Core.csproj", "src/Synergy.ReliefCenter.Core/"]
RUN dotnet restore "src/Synergy.ReliefCenter.Api/Synergy.ReliefCenter.Api.csproj"
COPY . .
WORKDIR "/src/src/Synergy.ReliefCenter.Api"
RUN dotnet build "Synergy.ReliefCenter.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Synergy.ReliefCenter.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Synergy.ReliefCenter.Api.dll"]