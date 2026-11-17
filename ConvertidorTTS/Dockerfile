FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ConvertidorTTS.csproj ./
COPY . ./
RUN dotnet restore ConvertidorTTS.csproj
RUN dotnet build ConvertidorTTS.csproj --no-restore -c Release -o /app/build

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080
COPY --from=build /app/build ./
USER $APP_UID
ENTRYPOINT ["dotnet", "ConvertidorTTS.dll"]