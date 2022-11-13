using BanGiay.Data;
using BanGiay.RepositoryPattern;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
 
// Cors
builder.Services.AddCors(p =>p.AddPolicy("corspolicy",build =>{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));
builder.Services.AddDbContext<BanGiayContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("BangiayAPI")));
builder.Services.AddAutoMapper(typeof(Program));
//life cycle DI: AddSingleton(),addTransient,addScoped
builder.Services.AddScoped<IMenu, MenuRes>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("corspolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
