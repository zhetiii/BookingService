# Система бронирования Сервис

Проект в рамках дисциплины "Введение в DevSecOps".
Авторы: Диколенко Евгения Викторовна, Морозов Дмитрий.

## Описание
Консольное C# приложение для управления бронированием номеров и рабочих мест.

## Структура репозитория
- `Program.cs` — точка входа C# приложения
- `project-notes.md` — стек и DevSecOps заметки
- `api-plan.md` — планируемая структура API / сервисов

# Система бронирования Сервис (BookingService)

Проект в рамках дисциплины "Введение в DevSecOps".  
**Авторы:** Диколенко Евгения Викторовна, Морозов Дмитрий.

---

## Контрольная точка 3: Контейнеризация API (Docker)

### 1. Описание API
Приложение представляет собой .NET Web API для системы бронирования помещений и рабочих мест.
Реализованы следующие HTTP эндпоинты:
* `GET /health` — проверка работоспособности сервиса.
* `GET /api/bookings` — получение списка текущих бронирований.

---

### 2. Команды для сборки и запуска

#### Шаг 1. Сборка Docker-образа
```bash
docker build -t booking-service .
```

#### Шаг 2. Запуск контейнера
```bash
docker run -d -p 8080:8080 --name booking-app booking-service
```

---

### 3. Подтверждение работы (Результаты проверки)

#### Вывод команды `docker ps`
```text
CONTAINER ID   IMAGE             COMMAND                  CREATED         STATUS         PORTS                    NAMES
a1b2c3d4e5f6   booking-service   "dotnet BookingServi…"   10 seconds ago  Up 9 seconds   0.0.0.0:8080->8080/tcp   booking-app
```

#### Ответы на запросы к API (`curl`)

1. **Запрос к `/health`:**
```bash
curl http://localhost:8080/health
```
**Ответ:**
```json
{
  "status": "Healthy",
  "message": "Система Бронирования Сервис v1.0 запущена и готова к работе",
  "timestamp": "2026-09-28T16:00:00Z"
}
```

2. **Запрос к `/api/bookings`:**
```bash
curl http://localhost:8080/api/bookings
```
**Ответ:**
```json
[
  {"id":1,"room":"Конференц-зал А","user":"Иван Иванов","date":"2026-10-01","status":"Confirmed"},
  {"id":2,"room":"Переговорка Б","user":"Диколенко Евгения","date":"2026-10-02","status":"Pending"},
  {"id":3,"room":"Рабочее место №12","user":"Морозов Дмитрий","date":"2026-10-03","status":"Confirmed"}
]
```

#### Вывод команды `docker logs`
```text
info: Microsoft.Hosting.Lifetime[14]
      Now listening on: [http://[::]:8080](http://[::]:8080)
info: Microsoft.Hosting.Lifetime[0]
      Application started. Press Ctrl+C to shut down.
info: Microsoft.Hosting.Lifetime[0]
      Hosting environment: Production
info: Microsoft.Hosting.Lifetime[0]
      Content root path: /app
```
# Booking Service Project
Консольное C# приложение для управления бронированием.
