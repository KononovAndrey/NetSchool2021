#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systems/Worker/DSRNetSchool.Worker/DSRNetSchool.Worker.csproj", "Systems/Worker/DSRNetSchool.Worker/"]
COPY ["Services/DSRNetSchool.RabbitMQService/DSRNetSchool.RabbitMqService.csproj", "Services/DSRNetSchool.RabbitMQService/"]
COPY ["Services/DSRNetSchool.Settings/DSRNetSchool.Settings.csproj", "Services/DSRNetSchool.Settings/"]
COPY ["Data/DSRNetSchool.Db.Domain/DSRNetSchool.Db.Entities.csproj", "Data/DSRNetSchool.Db.Domain/"]
COPY ["Services/DSRNetSchool.SMTPService/DSRNetSchool.EmailService.csproj", "Services/DSRNetSchool.SMTPService/"]
COPY ["Data/DSRNetSchool.Db.Context/DSRNetSchool.Db.Context.csproj", "Data/DSRNetSchool.Db.Context/"]
RUN dotnet restore "Systems/Worker/DSRNetSchool.Worker/DSRNetSchool.Worker.csproj"
COPY . .
WORKDIR "/src/Systems/Worker/DSRNetSchool.Worker"
RUN dotnet build "DSRNetSchool.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DSRNetSchool.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DSRNetSchool.Worker.dll"]