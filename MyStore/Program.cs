using MyStore.Data;
using MyStore.Models;
using MyStore.Models.Interface;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IProductRepository, EFProductRepository>();
builder.Services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
builder.Services.AddTransient<IOrderRepository,EFOrderRepository>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    

    endpoints.MapControllerRoute(
       name: null,
       pattern: "{category}/Page{productPage:int}",
       defaults: new { controller = "Home", action = "Index" });

    endpoints.MapControllerRoute(
       name: null,
                    pattern: "Page{productPage:int}",
                    defaults: new { controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute(
       name: null,
                    pattern: "{category}",
                    defaults: new { controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute(
       name: null,
                    pattern: "",
                    defaults: new { controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute(
        name: null, pattern: "{controller}/{action}/{id?}");
});

app.Run();
