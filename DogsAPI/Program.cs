using DogsAPI.Filters;
using DogsAPI.Middlewares;
using DogsAPI.Startup;


var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterApplicationServices(builder.Configuration);


builder.Services.AddControllers(options => options.Filters.Add<DogExceptionFilterAttribute>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRateLimiting(builder.Configuration);
var app = builder.Build();
app.UseRateLimiting();
app.ConfigureMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
