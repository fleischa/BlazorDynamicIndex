namespace BlazorDynamicIndex;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
	public static async Task GenerateDynamicIndex(this WebApplicationBuilder builder,
		Action<DynamicIndexConfiguration>? overrideIndexConfiguration = null,
		Action<DynamicIndexOptions>? configureOptions = null)
	{
		DynamicIndexOptions options = new();
		builder.Configuration.GetSection(DynamicIndexOptions.ConfigurationSection).Bind(options);
		configureOptions?.Invoke(options);

		string configurationFilePath = Path.Combine(builder.Environment.ContentRootPath, options.ConfigurationFile);

		DynamicIndexConfiguration? indexConfiguration = await DynamicIndexConfiguration.FromFileAsync(configurationFilePath);

		if (overrideIndexConfiguration != null)
		{
			overrideIndexConfiguration(indexConfiguration);
		}

		string indexContent = await DynamicIndexGenerator.Generate(indexConfiguration, builder.Environment.ContentRootPath);
		builder.Services.AddSingleton(new DynamicIndexCache(options.IndexFileName, indexContent));
	}
}
