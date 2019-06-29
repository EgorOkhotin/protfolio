FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["profile/profile.csproj", "profile/"]
RUN dotnet restore "profile/profile.csproj"
COPY . .
WORKDIR "/src/profile"
RUN dotnet build "profile.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "profile.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "profile.dll"]