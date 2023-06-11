#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Docker-PersonalProject/PersonalProject.csproj", "PersonalProject/"]
COPY ["Docker-PersonalProject.BLL/PersonalProject.BLL.csproj", "PersonalProject.BLL/"]
RUN dotnet restore "Docker-PersonalProject/PersonalProject.csproj"
COPY . .
WORKDIR "/src/Docker-PersonalProject"
RUN dotnet build "PersonalProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PersonalProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PersonalProject.dll"]