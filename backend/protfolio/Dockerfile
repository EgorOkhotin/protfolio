FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["protfolio/protfolio.csproj", "protfolio/"]
RUN dotnet restore "protfolio/protfolio.csproj"
COPY . .
WORKDIR "/src/protfolio"
RUN dotnet build "protfolio.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "protfolio.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "protfolio.dll"]