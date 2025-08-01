using JaMoveo.DATA;
using JaMoveo.DB;
using JaMoveo.Hubs;
using JaMoveo.Models;
using JaMoveo.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

//options.UseSqlite(
//    builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<DBAuth>();
builder.Services.AddScoped<AuthRepository>();
builder.Services.AddScoped<DBInstruments>();
builder.Services.AddScoped<InstrumentsRepository>();
builder.Services.AddScoped<SongsRepository>();

builder.Services.AddSingleton<SessionStateService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://ja-moveo-task-client.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseCors("AllowSpecificOrigin");

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<RehearsalHub>("/rehearsalHub");
    endpoints.MapControllers();
});


app.MapControllers();

app.Run();
