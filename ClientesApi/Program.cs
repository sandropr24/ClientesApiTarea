using ClientesApi.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;


//Construir la aplicacion(motor)
var builder = WebApplication.CreateBuilder(args);

//Registrar el DBContext
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Otros servicios
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
