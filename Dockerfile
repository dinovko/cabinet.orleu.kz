# билдим react
FROM node:23-alpine AS client-build
WORKDIR /app
COPY client/package*.json ./
RUN npm install
COPY client/ ./
RUN npm run build

# билдим .NET
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS server-build
WORKDIR /src
COPY server/*.csproj ./server/
RUN dotnet restore ./server/server.cabinet.orleu.kz.csproj

COPY . .
RUN dotnet publish ./server/server.cabinet.orleu.kz.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=server-build /app/publish .
COPY --from=client-build /app/dist ./wwwroot

# копируем wait-for-it внутрь образа
COPY wait-for-it.sh /wait-for-it.sh
RUN chmod +x /wait-for-it.sh

EXPOSE 80

#ENTRYPOINT ["dotnet", "server.cabinet.orleu.kz.dll"]
ENTRYPOINT ["/wait-for-it.sh", "db:5432", "--", "dotnet", "server.cabinet.orleu.kz.dll"]