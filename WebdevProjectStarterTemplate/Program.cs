WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddSession();
WebApplication app = builder.Build();
Configuration = app.Configuration;
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.MapRazorPages();
app.Run();

partial class Program
{
	public static IConfiguration Configuration { get; set; } = null!;
}