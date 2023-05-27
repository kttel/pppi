FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base

RUN apk update && apk add libgdiplus
RUN apk add ttf-freefont && apk add libssl1.1
RUN apk update; apk add fontconfig ttf-liberation
RUN fc-cache -f -v

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MyLabProject.csproj", "./"]
RUN dotnet restore "MyLabProject.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "MyLabProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MyLabProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyLabProject.dll"]
