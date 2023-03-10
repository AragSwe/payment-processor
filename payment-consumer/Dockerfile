FROM mcr.microsoft.com/dotnet/runtime:6.0-focal AS base
WORKDIR /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["payment-consumer/payment-consumer.csproj", "payment-consumer/"]
RUN dotnet restore "payment-consumer/payment-consumer.csproj"
COPY . .
WORKDIR "/src/payment-consumer"
RUN dotnet build "payment-consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "payment-consumer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "payment-consumer.dll"]
