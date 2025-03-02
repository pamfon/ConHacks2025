using TBDName.Services;
using OllamaSharp;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<StorageService>();    // Registers the StorageService
builder.Services.AddSingleton<QuestionService>();   // Registers the QuestionService
builder.Services.AddSingleton<EvaluationService>(); // Registers the EvaluationService
builder.Services.AddSingleton<GameEngineService>(); // Registers the GameEngineService
													// Register HttpClient and OllamaService

builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;
string endPoint = configuration["Ollama:Endpoint"] ?? "http://localhost:11434/";
string model = configuration["Ollama:Model"] ?? "llama3.1:8b";

builder.Services.AddSingleton<OllamaApiClient>(provider =>
{
    var client = new OllamaApiClient(new Uri(endPoint));
    client.SelectedModel = model;
    return client;
});

builder.Services.AddSession(options =>
{
	// Set session timeout (adjust as needed)
	options.IdleTimeout = TimeSpan.FromMinutes(30);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});

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
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
