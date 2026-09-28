namespace BookingService;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Настраиваем сервер на прослушивание порта 8080 внутри контейнера
        builder.WebHost.UseUrls("http://+:8080");

        var app = builder.Build();

        // 1. Endpoint проверки здоровья API
        app.MapGet("/health", () => Results.Ok(new 
        { 
            status = "Healthy", 
            message = "Система Бронирования Сервис v1.0 запущена и готова к работе",
            timestamp = DateTime.UtcNow 
        }));

        // 2. Endpoint по теме проекта BookingService
        app.MapGet("/api/bookings", () => Results.Ok(new[]
        {
            new { Id = 1, Room = "Конференц-зал А", User = "Иван Иванов", Date = "2026-10-01", Status = "Confirmed" },
            new { Id = 2, Room = "Переговорка Б", User = "Диколенко Евгения", Date = "2026-10-02", Status = "Pending" },
            new { Id = 3, Room = "Рабочее место №12", User = "Морозов Дмитрий", Date = "2026-10-03", Status = "Confirmed" }
        }));

        app.Run();
    }
}
