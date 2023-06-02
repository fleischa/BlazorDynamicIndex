using BlazorDynamicIndex;

namespace DemoApp.Server;

public static class Program
{
	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages();

		builder.AddDynamicIndex();

		WebApplication app = builder.Build();

		if (app.Environment.IsDevelopment())
		{
			app.UseWebAssemblyDebugging();
		}
		else
		{
			app.UseExceptionHandler("/Error");
			app.UseHsts();
		}

		app.UseHttpsRedirection();

		app.UseBlazorFrameworkFiles();
		app.UseStaticFiles();

		app.UseRouting();

		app.MapRazorPages();
		app.MapControllers();

		app.MapFallbackToDynamicIndex();

		app.Run();
	}
}