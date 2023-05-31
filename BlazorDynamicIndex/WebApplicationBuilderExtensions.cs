namespace BlazorDynamicIndex;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class WebApplicationBuilderExtensions
{
	public static WebApplicationBuilder AddDynamicIndex(this WebApplicationBuilder builder,
		Action<DynamicIndexOptions>? configureOptions = null,
		Action<DynamicIndexConfiguration>? overrideIndexConfiguration = null)
	{
		DynamicIndexOptions dynamicIndexOptions = builder.Configuration.GetSection(DynamicIndexOptions.ConfigurationSection).Get<DynamicIndexOptions>() ??
												new DynamicIndexOptions();
		configureOptions?.Invoke(dynamicIndexOptions);
		builder.Services.AddSingleton(dynamicIndexOptions);
		builder.Services.AddSingleton(new DynamicIndexCache(overrideIndexConfiguration));

		return builder;
	}
}
