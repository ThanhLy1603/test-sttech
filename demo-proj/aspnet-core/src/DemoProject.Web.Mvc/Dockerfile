FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

RUN dotnet tool install -g Microsoft.Web.LibraryManager.Cli
RUN ln -s ~/.dotnet/tools/libman /usr/local/bin/libman

WORKDIR /src
COPY ["src/DemoProject.Web.Mvc/DemoProject.Web.Mvc.csproj", "src/DemoProject.Web.Mvc/"]
COPY ["src/DemoProject.Web.Core/DemoProject.Web.Core.csproj", "src/DemoProject.Web.Core/"]
COPY ["src/DemoProject.Application/DemoProject.Application.csproj", "src/DemoProject.Application/"]
COPY ["src/DemoProject.Core/DemoProject.Core.csproj", "src/DemoProject.Core/"]
COPY ["src/DemoProject.EntityFrameworkCore/DemoProject.EntityFrameworkCore.csproj", "src/DemoProject.EntityFrameworkCore/"]
WORKDIR "/src/src/DemoProject.Web.Mvc"
RUN dotnet restore 

WORKDIR /src
COPY ["src/DemoProject.Web.Mvc", "src/DemoProject.Web.Mvc"]
COPY ["src/DemoProject.Web.Core", "src/DemoProject.Web.Core"]
COPY ["src/DemoProject.Application", "src/DemoProject.Application"]
COPY ["src/DemoProject.Core", "src/DemoProject.Core"]
COPY ["src/DemoProject.EntityFrameworkCore", "src/DemoProject.EntityFrameworkCore"]
WORKDIR "/src/src/DemoProject.Web.Mvc"
RUN libman restore
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "DemoProject.Web.Mvc.dll"]
