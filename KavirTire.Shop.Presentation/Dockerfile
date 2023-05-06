#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KavirTire.Shop.Presentation/KavirTire.Shop.Presentation.csproj", "KavirTire.Shop.Presentation/"]
COPY ["KavirTire.Shop.Application/KavirTire.Shop.Application.csproj", "KavirTire.Shop.Application/"]
COPY ["KavirTire.Shop.Domain/KavirTire.Shop.Domain.csproj", "KavirTire.Shop.Domain/"]
COPY ["KavirTire.Shop.Infrastructure/KavirTire.Shop.Infrastructure.csproj", "KavirTire.Shop.Infrastructure/"]
RUN dotnet restore "KavirTire.Shop.Presentation/KavirTire.Shop.Presentation.csproj"
COPY . .
WORKDIR "/src/KavirTire.Shop.Presentation"
RUN dotnet build "KavirTire.Shop.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KavirTire.Shop.Presentation.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
RUN ls -l
ENTRYPOINT ["dotnet", "KavirTire.Shop.Presentation.dll"]