using EmployeeManagementSystem.Repository;
using EmployeeManagementSystem.Repository.Employees;
using EmployeeManagementSystem.Services.Employees;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

// Add DbContext
var _memoryCache = Guid.NewGuid().ToString("N");
builder.Services.AddDbContext<EmployeeDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: _memoryCache);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
