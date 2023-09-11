FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["RemindAPi/RemindAPi.csproj", "RemindAPi/"]
COPY ["Remind.Core/Remind.Core.csproj", "Remind.Core/"]
COPY ["Remind.Data/Remind.Data.csproj", "Remind.Data/"]
COPY ["Remind.Services/Remind.Services.csproj", "Remind.Services/"]
RUN dotnet dotnet dev-certs https --trust
RUN dotnet restore "RemindAPi/RemindAPi.csproj"
COPY . .
WORKDIR "/src/RemindAPi"
RUN dotnet build "RemindAPi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RemindAPi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=build /root/.dotnet/corefx/cryptography/x509stores/my/* /root/.dotnet/corefx/cryptography/x509stores/my/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RemindAPi.dll"]
