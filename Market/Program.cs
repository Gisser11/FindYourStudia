using Market.DAL;
using Market.DAL.Interfaces;
using Market.DAL.Interfaces.IServices;
using Market.DAL.Repositories;
using Market.Service.Implementation;
using Market.Service.Interfaces;
using Microsoft.EntityFrameworkCore; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<ApplicationDbContext> (
    options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Market"))
    );

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStudiaRepository, StudiaRepository>();
builder.Services.AddScoped<IStudiaService, StudiaService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAssortmentRepository, AssortmentRepository>();
builder.Services.AddScoped<IAssortmentService, AssortmentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IManagmentService, ManagmentService>();

builder.Services.AddEndpointsApiExplorer();
 

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
            .WithOrigins("http://localhost:5173", "http://localhost:5500", "http://127.0.0.1:5500")
            .AllowAnyMethod()
            .AllowCredentials()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    
}

app.UseHttpsRedirection();
app.UseCors();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAuthentication();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Admin}/{action=Index}/{id?}");
});
app.Run();