using Warehouse.Extensions;
using Warehouse.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWarehouseDb(builder.Configuration);
builder.Services.AddScoped<CoilService>();
builder.Services.AddScoped<CoilStatisticsService>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseGlobalExceptionHandler();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();