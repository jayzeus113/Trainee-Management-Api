using Microsoft.EntityFrameworkCore;
using TraineeManagement.Data;
using TraineeManagement.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // options.UseInMemoryDatabase("TraineeManagementDb");
    options.UseMySQL(connectionString);
});

builder.Services.AddOpenApi();
builder.Services.AddScoped<ITraineeService, TraineeService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();