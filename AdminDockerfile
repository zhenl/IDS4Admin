FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/IDS4Admin.Admin/IDS4Admin.Admin.csproj", "src/IDS4Admin.Admin/"]
COPY ["src/IDS4Admin.Admin.EntityFramework.Shared/IDS4Admin.Admin.EntityFramework.Shared.csproj", "src/IDS4Admin.Admin.EntityFramework.Shared/"]
COPY ["src/IDS4Admin.Admin.EntityFramework.SqlServer/IDS4Admin.Admin.EntityFramework.SqlServer.csproj", "src/IDS4Admin.Admin.EntityFramework.SqlServer/"]
COPY ["src/IDS4Admin.Admin.EntityFramework.PostgreSQL/IDS4Admin.Admin.EntityFramework.PostgreSQL.csproj", "src/IDS4Admin.Admin.EntityFramework.PostgreSQL/"]
COPY ["src/IDS4Admin.Shared/IDS4Admin.Shared.csproj", "src/IDS4Admin.Shared/"]
COPY ["src/IDS4Admin.Admin.EntityFramework.MySql/IDS4Admin.Admin.EntityFramework.MySql.csproj", "src/IDS4Admin.Admin.EntityFramework.MySql/"]
RUN dotnet restore "src/IDS4Admin.Admin/IDS4Admin.Admin.csproj"
COPY . .
WORKDIR "/src/src/IDS4Admin.Admin"
RUN dotnet build "IDS4Admin.Admin.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IDS4Admin.Admin.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_FORWARDEDHEADERS_ENABLED=true
ENTRYPOINT ["dotnet", "IDS4Admin.Admin.dll"]