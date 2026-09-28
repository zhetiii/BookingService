# Этап 1: Сборка и публикация приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Копируем файл проекта и восстанавливаем зависимости
COPY ["BookingService.csproj", "./"]
RUN dotnet restore "BookingService.csproj"

# Копируем остальной исходный код и собираем
COPY . .
RUN dotnet publish "BookingService.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Этап 2: Финальный образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Копируем собранное приложение из этапа сборки
COPY --from=build /app/publish .

# Указываем порт приложения
EXPOSE 8080

# Команда запуска контейнера
ENTRYPOINT ["dotnet", "BookingService.dll"]
