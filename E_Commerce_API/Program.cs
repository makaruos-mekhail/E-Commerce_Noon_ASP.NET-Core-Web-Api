using Application.Contracts;
using AutoMapper;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.DTOs;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DContext>(options =>
    {
       // options.UseLazyLoadingProxies();
        options.UseSqlServer(builder.Configuration.GetConnectionString("DbContextConnectionFatmaAhmed"));
    }); 

// Add services to the container.

builder.Services.AddIdentity<User, IdentityRole<long>>(options => { 
    options.SignIn.RequireConfirmedAccount = false;
 })
    .AddEntityFrameworkStores<DContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IWishListRepository, WishListRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IProductColorRepository, ProductColorRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
 
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
                    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

//object value = builder.Services.AddAutoMapper(typeof(Program).Assembly);
var config = new MapperConfiguration(cfg => { cfg.AddProfile<UserProfile>(); });

IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);




builder.Services.AddEndpointsApiExplorer();
//IMapper mapper = mapperConfig.CreateMapper();
// builder.Services.AddSingleton(mapper);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
