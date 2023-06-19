using Hangfire;
using log4net.Config;
using Log4Net.LogUtility;
XmlConfigurator.Configure(new FileInfo("Log4Net.config"));

var builder = WebApplication.CreateBuilder(args);
// Setup Host
using IHost host = Host.CreateDefaultBuilder().Build();

// Ask service provider for configuration
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

// Get connection string
string ConnectString = config.GetValue<string>("Logging:ConnectionStrings:DefaultConnection");
builder.Logging.AddLog4Net();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Logging.AddLog4Net();
builder.Services.AddScoped<ILog4Logger, Log4Logger>();
// Add Hangfire services.
builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(ConnectString));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseHangfireDashboard();
//backgroundJobs.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
