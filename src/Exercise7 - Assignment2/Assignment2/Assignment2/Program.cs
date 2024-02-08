using Microsoft.EntityFrameworkCore;
using CodeInBlue.Models;
using Repository.Data;
using AutoMapper;
using Service.Profiles;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Services;
using Service.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<AppDBContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("LoginRazorConnection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddDbContext<Assignment2Context>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Assignment2Connection");
    options.UseSqlServer(connectionString);
});

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDBContext>();

// Register services
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddTransient<IOrderDetailService, OrderDetailService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<ISupplierService, SupplierService>();

// Add AutoMapper
builder.Services.AddScoped<IMapper>(sp =>
{
    var config = new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new AutoMapperProfile());
    });
    return config.CreateMapper();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
