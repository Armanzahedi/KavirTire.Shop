using KavirTire.Shop.Application;
using KavirTire.Shop.Infrastructure;
using KavirTire.Shop.Infrastructure.Cache;
using KavirTire.Shop.Infrastructure.Persistence.Common;
using KavirTire.Shop.Infrastructure.RecurringJob;
using KavirTire.Shop.Presentation.Middlewares.WebFileHandler;
using Serilog;
using Serilog.Events;
using Serilog.Ui.MsSqlServerProvider;
using Serilog.Ui.Web;

var builder = WebApplication.CreateBuilder(args);


var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddApplicationServices(builder.Configuration)
    .AddInfrastructureServices(builder.Configuration);

builder.Services.AddWebOptimizer(pipeline =>
{
    pipeline
        .AddCssBundle("/css/bundle.css",
            // "/css/glyphicons-font-awesome-migrate.min.css",
            "/css/default.bundle.css",
            // "/css/account.css",
            // "/css/fa-custom.css",
            "/css/custom.css",
            "toastr/toastr.min.css"
        ).MinifyCss();

    pipeline
        .AddJavaScriptBundle("/js/bundle.js",
            "/lib/jquery/dist/jquery.min.js",
            "/toastr/toastr.min.js",
            "/toastr/toastr.custom.js",
            "/js/site.js",
            "/js/cart.js"
        ).MinifyJavaScript();
});


builder.Services.AddSerilogUi(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), "SystemLogs"); });

builder.Services.AddAuthentication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseWebOptimizer();
// app.UseHttpsRedirection();


app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// app.UseHangfireDashboard("/jobs");
app.UseCors(builder =>
    builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
);

app.UseSerilogUi(options =>
{
    options.RoutePrefix = "serilog-ui";
    options.HomeUrl = "/";
});
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default-area",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "home",
        pattern: "{action=Index}/{id?}",
        defaults: new { controller = "Home" });

    endpoints.MapRazorPages();
    // endpoints.MapHangfireDashboard();
});
using (var scope = app.Services.CreateScope())
{
    await scope.InitializeDb();
    await scope.FlushCache();
}

app.UseWebFileHandler();
app.StartRecurringJobs();
app.Run();