#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systems/Identity/DSRNetSchool.Identity/DSRNetSchool.Identity.csproj", "Systems/Identity/DSRNetSchool.Identity/"]
COPY ["Data/DSRNetSchool.Db.Domain/DSRNetSchool.Db.Entities.csproj", "Data/DSRNetSchool.Db.Domain/"]
COPY ["Services/DSRNetSchool.Settings/DSRNetSchool.Settings.csproj", "Services/DSRNetSchool.Settings/"]
COPY ["Shared/DSRNetSchool.Common/DSRNetSchool.Common.csproj", "Shared/DSRNetSchool.Common/"]
COPY ["Data/DSRNetSchool.Db.Context/DSRNetSchool.Db.Context.csproj", "Data/DSRNetSchool.Db.Context/"]
RUN dotnet restore "Systems/Identity/DSRNetSchool.Identity/DSRNetSchool.Identity.csproj"
COPY . .
WORKDIR "/src/Systems/Identity/DSRNetSchool.Identity"
RUN dotnet build "DSRNetSchool.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DSRNetSchool.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DSRNetSchool.Identity.dll"]