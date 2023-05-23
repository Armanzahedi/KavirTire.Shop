FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Bmsd.KavirTire.IdentityServer/Bmsd.KavirTire.IdentityServer.csproj", "Bmsd.KavirTire.IdentityServer/"]
RUN dotnet restore "Bmsd.KavirTire.IdentityServer/Bmsd.KavirTire.IdentityServer.csproj"
COPY . .
WORKDIR "/src/Bmsd.KavirTire.IdentityServer"
RUN dotnet build "Bmsd.KavirTire.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bmsd.KavirTire.IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bmsd.KavirTire.IdentityServer.dll"]
