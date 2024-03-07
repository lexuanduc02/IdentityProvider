FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_ENVIRONMENT=Development

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["IP.Model/IP.Model/.csproj", "IP.Model/"]
COPY ["IP.Common/IP.Common.csproj", "IP.Common/"]
COPY ["IP.Domain/IP.Domain.csproj", "IP.Domain/"]
COPY ["IP.Services/IP.Services.csproj", "IP.Services/"]
COPY ["IdentityProvider/IdentityProvider.csproj", "IdentityProvider/"]
RUN dotnet restore "IdentityProvider/IdentityProvider.csproj"
COPY . .
WORKDIR "/src/IdentityProvider"
RUN dotnet build "IdentityProvider.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "IdentityProvider.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityProvider.dll"]
