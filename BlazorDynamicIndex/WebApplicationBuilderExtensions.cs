namespace BlazorDynamicIndex;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System.Text;

public static class WebApplicationBuilderExtensions
{
	public static async Task GenerateDynamicIndex(this WebApplicationBuilder builder, Action<DynamicIndexConfiguration>? overrideIndexConfiguration = null, Action<DynamicIndexOptions>? configureOptions = null)
	{
		DynamicIndexOptions options = new();

		if (configureOptions != null)
		{
			configureOptions(options);
		}
		else
		{
			builder.Configuration.GetSection(DynamicIndexOptions.ConfigurationSection).Bind(options);
		}

		string outputPath = Path.Combine(builder.Environment.WebRootPath, options.OutputFile);
		DynamicIndexConfiguration? indexConfiguration = await DynamicIndexConfiguration.FromFileAsync(options.ConfigurationFile);

		if (overrideIndexConfiguration != null)
		{
			overrideIndexConfiguration(indexConfiguration);
		}

		string indexPageHtml = await DynamicIndexGenerator.Generate(indexConfiguration);
		await File.WriteAllTextAsync(outputPath, indexPageHtml, Encoding.UTF8);
	}
}
