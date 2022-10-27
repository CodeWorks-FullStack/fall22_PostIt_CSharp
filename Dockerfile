FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["fall22_PostIt_CSharp.csproj", "./"]
RUN dotnet restore "fall22_PostIt_CSharp.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "fall22_PostIt_CSharp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "fall22_PostIt_CSharp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "fall22_PostIt_CSharp.dll"]
