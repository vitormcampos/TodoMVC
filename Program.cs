using Microsoft.EntityFrameworkCore;
using TarefasMvc.Context;
using TarefasMvc.Repositories;
using TarefasMvc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("SqlServer");
    opt.UseSqlServer(connectionString);
});
builder.Services.AddScoped<TodoService>();
builder.Services.AddScoped<TodoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todo}/{action=Index}/{id?}");

app.Run();
