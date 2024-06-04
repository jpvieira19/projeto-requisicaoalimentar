using Microsoft.EntityFrameworkCore;

using Application.Services;
using DataModel.Repository;
using DataModel.Mapper;
using Domain.Factory;
using Domain.IRepository;
using WebApi.Controllers;


var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration;
var pedidosQueueName = config["PedidosCommands:" + args[0]];

var connection = config["ConnectionStrings:" + args[0]];

var port = getPort(pedidosQueueName);
// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<AbsanteeContext>(opt =>
    //opt.UseInMemoryDatabase("AbsanteeList")
    //opt.UseSqlite("Data Source=AbsanteeDatabase.sqlite")
    opt.UseSqlite(Host.CreateApplicationBuilder().Configuration.GetConnectionString(pedidosQueueName))
    );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPedidosRepository, PedidosRepository>();
builder.Services.AddTransient<IPedidosFactory, PedidosFactory>();
builder.Services.AddTransient<PedidosMapper>();
builder.Services.AddTransient<PedidosService>();
builder.Services.AddScoped<EmailService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); 

app.UseAuthorization();

app.MapControllers();

app.Run($"https://localhost:{port}");

int getPort(string name)
{
    // Implement logic to map queue name to a unique port number
    // Example: Assign a unique port number based on the queue name suffix
    int basePort = 5500; // Start from port 5000
    int queueIndex = int.Parse(name.Substring(2)); // Extract the numeric part of the queue name (assuming it starts with 'Q')
    return basePort + queueIndex;
}