#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5002
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Event Service/Event Service.csproj", "Event Service/"]
RUN dotnet restore "Event Service/Event Service.csproj"
COPY . .
WORKDIR "/src/Event Service"
RUN dotnet build "Event Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Event Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Event Service.dll"]