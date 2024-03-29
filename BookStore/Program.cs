using AutoMapper;
using BookStore.Model;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

builder.Services.AddScoped<IBook, BookService>();
builder.Services.AddScoped<IBookWeb, BookWebService>();
builder.Services.AddAutoMapper(typeof(Program));
// Auto Mapper Configurations  
var mappingConfig = new MapperConfiguration(mc => {
    mc.AddProfile(new CP());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseStaticFiles();
app.Run();
