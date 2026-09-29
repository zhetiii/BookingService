using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") 
                       ?? "Host=db;Port=5432;Database=app;Username=postgres;Password=1234";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

app.MapGet("/bookings", async (AppDbContext db) => 
    await db.Bookings.ToListAsync());

app.MapGet("/bookings/{id:int}", async (int id, AppDbContext db) =>
    await db.Bookings.FindAsync(id) is Booking booking ? Results.Ok(booking) : Results.NotFound());

app.MapPost("/bookings", async (Booking booking, AppDbContext db) =>
{
    db.Bookings.Add(booking);
    await db.SaveChangesAsync();
    return Results.Created($"/bookings/{booking.Id}", booking);
});

app.MapPut("/bookings/{id:int}", async (int id, Booking updated, AppDbContext db) =>
{
    var booking = await db.Bookings.FindAsync(id);
    if (booking is null) return Results.NotFound();
    
    booking.CustomerName = updated.CustomerName;
    booking.RoomType = updated.RoomType;
    booking.Days = updated.Days;
    
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/bookings/{id:int}", async (int id, AppDbContext db) =>
{
    if (await db.Bookings.FindAsync(id) is Booking booking)
    {
        db.Bookings.Remove(booking);
        await db.SaveChangesAsync();
        return Results.Ok(booking);
    }
    return Results.NotFound();
});

app.Run();

public class Booking
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string RoomType { get; set; } = string.Empty;
    public int Days { get; set; }
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Booking> Bookings => Set<Booking>();
}
