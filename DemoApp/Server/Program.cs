namespace DemoApp.Server;

using BlazorDynamicIndex;

public class Program
{
	private const string pathBase = "/test/";

	public static void Main(string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		builder.Services.AddControllersWithViews();
		builder.Services.AddRazorPages();

		builder.AddDynamicIndex(null, opt => opt.Base = Program.pathBase);

		WebApplication app = builder.Build();

		app.UsePathBase(Program.pathBase);

		// Configure the HTTP request pipeline.
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
