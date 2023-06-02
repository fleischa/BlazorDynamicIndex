using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace BlazorDynamicIndex;

public class DynamicIndexCache
{
	public DynamicIndexCache(Action<DynamicIndexConfiguration>? overrideIndexConfiguration = null)
	{
		this.OverrideIndexConfiguration = overrideIndexConfiguration;
	}

	public Action<DynamicIndexConfiguration>? OverrideIndexConfiguration { get; }

	public DynamicIndexResponse? CachedIndexResponse { get; private set; }

	public async Task<DynamicIndexResponse> GetIndex(HttpContext httpContext)
	{
		if (this.CachedIndexResponse != null)
		{
			return this.CachedIndexResponse;
		}

		IServiceProvider serviceProvider = httpContext.RequestServices;
		DynamicIndexOptions dynamicIndexOptions = serviceProvider.GetRequiredService<DynamicIndexOptions>();
		IWebHostEnvironment environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

		IFileProvider webRootFileProvider = environment.WebRootFileProvider;
		IFileInfo? indexConfigurationFile = webRootFileProvider.GetFileInfo(dynamicIndexOptions.ConfigurationFile);

		if (indexConfigurationFile is { Exists: true })
		{
			DynamicIndexConfiguration indexConfiguration = await DynamicIndexConfiguration.FromFileAsync(indexConfigurationFile.PhysicalPath);
			this.OverrideIndexConfiguration?.Invoke(indexConfiguration);

			indexConfiguration.Base ??= $"{httpContext.Request.PathBase.ToString().TrimEnd('/')}/";

			string indexContent = await DynamicIndexGenerator.Generate(indexConfiguration, webRootFileProvider);
			this.CachedIndexResponse = new DynamicIndexResponse(indexContent, Encoding.UTF8.GetByteCount(indexContent));
		}
		else
		{
			throw new FileNotFoundException("Could not find index configuration file", indexConfigurationFile?.PhysicalPath ?? string.Empty);
		}

		return this.CachedIndexResponse;
	}
}