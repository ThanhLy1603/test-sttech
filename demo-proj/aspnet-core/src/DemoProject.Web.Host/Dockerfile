FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY ["src/DemoProject.Web.Host/DemoProject.Web.Host.csproj", "src/DemoProject.Web.Host/"]
COPY ["src/DemoProject.Web.Core/DemoProject.Web.Core.csproj", "src/DemoProject.Web.Core/"]
COPY ["src/DemoProject.Application/DemoProject.Application.csproj", "src/DemoProject.Application/"]
COPY ["src/DemoProject.Core/DemoProject.Core.csproj", "src/DemoProject.Core/"]
COPY ["src/DemoProject.EntityFrameworkCore/DemoProject.EntityFrameworkCore.csproj", "src/DemoProject.EntityFrameworkCore/"]
WORKDIR "/src/src/DemoProject.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/DemoProject.Web.Host", "src/DemoProject.Web.Host"]
COPY ["src/DemoProject.Web.Core", "src/DemoProject.Web.Core"]
COPY ["src/DemoProject.Application", "src/DemoProject.Application"]
COPY ["src/DemoProject.Core", "src/DemoProject.Core"]
COPY ["src/DemoProject.EntityFrameworkCore", "src/DemoProject.EntityFrameworkCore"]
WORKDIR "/src/src/DemoProject.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "DemoProject.Web.Host.dll"]
