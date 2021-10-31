#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["AwsWebApi/AwsWebApi.csproj", "AwsWebApi/"]
RUN dotnet restore "AwsWebApi/AwsWebApi.csproj"
COPY . .
WORKDIR "/src/AwsWebApi"
RUN dotnet build "AwsWebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AwsWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AwsWebApi.dll"]
