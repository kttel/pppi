FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base

RUN apk update && apk add libgdiplus
RUN apk add ttf-freefont && apk add libssl1.1
RUN apk update; apk add fontconfig ttf-liberation
RUN fc-cache -f -v

ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["LabProject.csproj", "./"]

RUN dotnet restore "./LabProject.csproj"
COPY . .
RUN dotnet build "LabProject.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LabProject.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "LabProject.dll"]
