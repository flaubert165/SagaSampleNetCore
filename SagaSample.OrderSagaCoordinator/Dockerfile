#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["SagaSample.OrderSagaCoordinator/SagaSample.OrderSagaCoordinator.csproj", "SagaSample.OrderSagaCoordinator/"]
COPY ["SagaSample.OrderSagaCoordinator.IoC/SagaSample.OrderSagaCoordinator.IoC.csproj", "SagaSample.OrderSagaCoordinator.IoC/"]
COPY ["SagaSample.OrderSagaCoordinator.Infrastructure.Events/SagaSample.OrderSagaCoordinator.Infrastructure.Events.csproj", "SagaSample.OrderSagaCoordinator.Infrastructure.Events/"]
COPY ["SagaSample.Messages/SagaSample.Messages.csproj", "SagaSample.Messages/"]
COPY ["SagaSample.Common/SagaSample.Common.csproj", "SagaSample.Common/"]
RUN dotnet restore "SagaSample.OrderSagaCoordinator/SagaSample.OrderSagaCoordinator.csproj"
COPY . .
WORKDIR "/src/SagaSample.OrderSagaCoordinator"
RUN dotnet build "SagaSample.OrderSagaCoordinator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SagaSample.OrderSagaCoordinator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SagaSample.OrderSagaCoordinator.dll"]