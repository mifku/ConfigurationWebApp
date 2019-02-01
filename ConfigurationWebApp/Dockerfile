#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 3507
EXPOSE 44379

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY ["ConfigurationWebApp/ConfigurationWebApp.csproj", "ConfigurationWebApp/"]
RUN dotnet restore "ConfigurationWebApp/ConfigurationWebApp.csproj"
COPY . .
WORKDIR "/src/ConfigurationWebApp"
RUN dotnet build "ConfigurationWebApp.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ConfigurationWebApp.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ConfigurationWebApp.dll"]