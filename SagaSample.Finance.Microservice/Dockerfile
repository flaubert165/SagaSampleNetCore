#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SagaSample.Finance.Microservice/SagaSample.Finance.Microservice.csproj", "SagaSample.Finance.Microservice/"]
COPY ["SagaSample.Finance.Microservice.IoC/SagaSample.Finance.Microservice.IoC.csproj", "SagaSample.Finance.Microservice.IoC/"]
RUN dotnet restore "SagaSample.Finance.Microservice/SagaSample.Finance.Microservice.csproj"
COPY . .
WORKDIR "/src/SagaSample.Finance.Microservice"
RUN dotnet build "SagaSample.Finance.Microservice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SagaSample.Finance.Microservice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SagaSample.Finance.Microservice.dll"]