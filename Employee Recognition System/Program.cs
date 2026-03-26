
using Employee_Recognition_System.Services.Implementations;
using Employee_Recognition_System.Services.Interfaces;
using Employee_Recognition_System.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAwardService, AwardService>();
builder.Services.AddScoped<IEmailService, EmailService>();


// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Services
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAppreciationService, AppreciationService>();
builder.Services.AddScoped<INominationService, NominationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowReact");

app.UseAuthorization();

app.MapControllers();

app.Run();