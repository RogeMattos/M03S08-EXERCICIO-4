FROM mcr.microsoft.com/dotnet/sdk:7.0 as build-env
WORKDIR /src
COPY src/*.csproj .
RUN dotnet restore "/src/Semana_08.csproj" --use-current-runtime
COPY src .
RUN dotnet publish Semana_08_Ex_04.sln -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 as runtime
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Semana_08.dll"]