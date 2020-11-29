#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SagaSample.Safekeeping.Microservice/SagaSample.Safekeeping.Microservice.csproj", "SagaSample.Safekeeping.Microservice/"]
COPY ["SagaSample.Safekeeping.Microservice.IoC/SagaSample.Safekeeping.Microservice.IoC.csproj", "SagaSample.Safekeeping.Microservice.IoC/"]
RUN dotnet restore "SagaSample.Safekeeping.Microservice/SagaSample.Safekeeping.Microservice.csproj"
COPY . .
WORKDIR "/src/SagaSample.Safekeeping.Microservice"
RUN dotnet build "SagaSample.Safekeeping.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SagaSample.Safekeeping.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SagaSample.Safekeeping.Microservice.dll"]