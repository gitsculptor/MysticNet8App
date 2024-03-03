using MysticAppNet8App.Application;
using MysticNet8App.Contracts.Interfaces;
using MysticNet8App.Extensions;
using MysticNet8App.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.ConfigureCors();
builder.Services.ConfigureLoggerService();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddServices();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
app.UseHttpsRedirection();
app.MapControllers();

app.UseCors("CorsPolicy");
app.UseAuthorization();
app.Run();






