FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY techninxa/techninxa.csproj techninxa/
RUN dotnet restore techninxa/techninxa.csproj
COPY techninxa/ techninxa/
RUN dotnet publish techninxa/techninxa.csproj -c Release --no-restore -o /out /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /out/ .
RUN mkdir -p /app/wwwroot/uploads /var/lib/techninxa/keys && chown -R app:app /app/wwwroot/uploads /var/lib/techninxa
USER app
ENV ASPNETCORE_HTTP_PORTS=8080 ASPNETCORE_ENVIRONMENT=Production TZ=Asia/Dhaka
EXPOSE 8080
ENTRYPOINT ["dotnet", "techninxa.dll"]
