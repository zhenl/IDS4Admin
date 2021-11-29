FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/IDS4Admin.STS.Identity/IDS4Admin.STS.Identity.csproj", "src/IDS4Admin.STS.Identity/"]
COPY ["src/IDS4Admin.Admin.EntityFramework.Shared/IDS4Admin.Admin.EntityFramework.Shared.csproj", "src/IDS4Admin.Admin.EntityFramework.Shared/"]
COPY ["src/IDS4Admin.Shared/IDS4Admin.Shared.csproj", "src/IDS4Admin.Shared/"]
RUN dotnet restore "src/IDS4Admin.STS.Identity/IDS4Admin.STS.Identity.csproj"
COPY . .
WORKDIR "/src/src/IDS4Admin.STS.Identity"
RUN dotnet build "IDS4Admin.STS.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IDS4Admin.STS.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "IDS4Admin.STS.Identity.dll"]