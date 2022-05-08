#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TransactionsAluraCSV.Presentation/TransactionsAluraCSV.Presentation.csproj", "TransactionsAluraCSV.Presentation/"]
COPY ["TransactionsAluraCSV.Domain/TransactionsAluraCSV.Domain.csproj", "TransactionsAluraCSV.Domain/"]
COPY ["TransactionsAluraCSV.Infra.Data/TransactionsAluraCSV.Infra.Data.csproj", "TransactionsAluraCSV.Infra.Data/"]
RUN dotnet restore "TransactionsAluraCSV.Presentation/TransactionsAluraCSV.Presentation.csproj"
COPY . .
WORKDIR "/src/TransactionsAluraCSV.Presentation"
RUN dotnet build "TransactionsAluraCSV.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TransactionsAluraCSV.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TransactionsAluraCSV.Presentation.dll"]