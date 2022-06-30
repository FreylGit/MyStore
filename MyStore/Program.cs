using MyStore.Data;
using MyStore.Models.Interface;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IProductRepository, EFProductRepository>();

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: null,
        pattern: "Page{productPage:int}",
        defaults: new { Controller = "Home", action = "Index" });

    endpoints.MapControllerRoute(
        name: null,
        pattern: "Page{productPage:int}",
        defaults: new { Controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute(
       name: null,
       pattern: "{category}",
       defaults: new { Controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute(
       name: null,
       pattern: "",
       defaults: new { Controller = "Home", action = "Index", productPage = 1 });

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
