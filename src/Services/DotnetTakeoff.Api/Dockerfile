FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS base
WORKDIR /app
EXPOSE 80

RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS build
WORKDIR /app
COPY ["src/Services/DotnetTakeoff.Api/DotnetTakeoff.Api.csproj", "src/Services/DotnetTakeoff.Api/"]
RUN dotnet restore "src/Services/DotnetTakeoff.Api/DotnetTakeoff.Api.csproj"
COPY . .
WORKDIR "/app/src/Services/DotnetTakeoff.Api"
RUN dotnet build -c Release -o /app/build --no-restore

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotnetTakeoff.Api.dll"]
