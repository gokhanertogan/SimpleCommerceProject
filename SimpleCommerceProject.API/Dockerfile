#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SimpleCommerceProject.API/SimpleCommerceProject.API.csproj", "SimpleCommerceProject.API/"]
COPY ["SimpleCommerceProject.Service/SimpleCommerceProject.Service.csproj", "SimpleCommerceProject.Service/"]
COPY ["SimpleCommerceProject.Shared/SimpleCommerceProject.Shared.csproj", "SimpleCommerceProject.Shared/"]
COPY ["SimpleCommerceProject.Models/SimpleCommerceProject.Models.csproj", "SimpleCommerceProject.Models/"]
COPY ["SimpleCommerceProject.Data.Models/SimpleCommerceProject.Data.Models.csproj", "SimpleCommerceProject.Data.Models/"]
RUN dotnet restore "SimpleCommerceProject.API/SimpleCommerceProject.API.csproj"
COPY . .
WORKDIR "/src/SimpleCommerceProject.API"
RUN dotnet build "SimpleCommerceProject.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleCommerceProject.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleCommerceProject.API.dll"]