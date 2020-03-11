FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY *.sln ./

COPY Thankies.Bot.Api/*.csproj Thankies.Bot.Api/
COPY Thankies.Core.Domain/*.csproj Thankies.Core.Domain/
#COPY Thankies.Core.Entity/*.csproj Thankies.Core.Entity/
COPY Thankies.Infrastructure.Contract/*.csproj Thankies.Infrastructure.Contract/
COPY Thankies.Infrastructure.Implementation/*.csproj Thankies.Infrastructure.Implementation/
#COPY Thankies.Persistence/*.csproj Thankies.Persistence/
# COPY Thankies.Persistence.Migration/*.csproj Thankies.Persistence.Migration/

RUN dotnet restore -r linux-musl-x64

COPY . .
WORKDIR /src/Thankies
RUN dotnet publish -c Release -o /app -r linux-musl-x64 --self-contained false --no-restore ../Thankies.sln 

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build /app ./

ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS http://*:5100
EXPOSE 5100

ENTRYPOINT ["dotnet", "Thankies.Bot.Api.dll"]
