using AccountingPayment.CrossCutting.DependencyInjection;
using AccountingPayment.WepApi.Configuration;
using AccountingPayment.WepApi.Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerService();
builder.Services.ConfigureDependencyGeralInjection(configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("corsPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseCors("corsPolicy");
app.UseHttpsRedirection();

app.UseAuthorization();

app.AddSwaggerBuilder();
app.MapControllers();

app.Run();
