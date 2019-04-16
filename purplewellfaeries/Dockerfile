FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["purplewellfaeries/purplewellfaeries.csproj", "purplewellfaeries/"]
RUN dotnet restore "purplewellfaeries/purplewellfaeries.csproj"
COPY . .
WORKDIR "/src/purplewellfaeries"
RUN dotnet build "purplewellfaeries.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "purplewellfaeries.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "purplewellfaeries.dll"]