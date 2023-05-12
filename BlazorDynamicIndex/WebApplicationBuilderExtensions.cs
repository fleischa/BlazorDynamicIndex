using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace BlazorDynamicIndex;

public static class WebApplicationBuilderExtensions
{
	public static async Task AddDynamicIndex(this WebApplicationBuilder builder,
		Action<DynamicIndexOptions>? configureOptions = null,
		Action<DynamicIndexConfiguration>? overrideIndexConfiguration = null)
	{
		DynamicIndexOptions dynamicIndexOptions = builder.Configuration.GetSection(DynamicIndexOptions.ConfigurationSection).Get<DynamicIndexOptions>() ??
												new DynamicIndexOptions();
		configureOptions?.Invoke(dynamicIndexOptions);

		IFileProvider webRootFileProvider = builder.Environment.WebRootFileProvider;
		IFileInfo? indexConfigurationFile = webRootFileProvider.GetFileInfo(dynamicIndexOptions.ConfigurationFile);

		if (indexConfigurationFile is { Exists: true })
		{
			DynamicIndexConfiguration indexConfiguration = await DynamicIndexConfiguration.FromFileAsync(indexConfigurationFile.PhysicalPath);
			overrideIndexConfiguration?.Invoke(indexConfiguration);

			string indexContent = await DynamicIndexGenerator.Generate(indexConfiguration, webRootFileProvider);
			builder.Services.AddSingleton(new DynamicIndexCache(dynamicIndexOptions.IndexFileName, indexContent));
		}
	}
}