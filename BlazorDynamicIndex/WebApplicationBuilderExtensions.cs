namespace BlazorDynamicIndex;

using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

public static class WebApplicationBuilderExtensions
{
	public static async Task GenerateDynamicIndex(this WebApplicationBuilder builder,
		Action<DynamicIndexConfiguration>? overrideIndexConfiguration = null,
		Action<DynamicIndexOptions>? configureOptions = null)
	{
		DynamicIndexOptions options = new();
		builder.Configuration.GetSection(DynamicIndexOptions.ConfigurationSection).Bind(options);
		configureOptions?.Invoke(options);

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
