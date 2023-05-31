namespace BlazorDynamicIndex;

using System.Text.Json;
using JetBrains.Annotations;

public class DynamicIndexConfiguration
{
	public string? Lang { get; set; }

	public string? Viewport { get; set; }

	public string? Base { get; set; }

	public string? Title { get; set; }

	public string? BodyFile { get; set; }

	[UsedImplicitly]
	public List<StyleSheetReference> StyleSheets { get; set; } = new();

	[UsedImplicitly]
	public List<IconReference> Icons { get; set; } = new();

	[UsedImplicitly]
	public List<AsyncScriptReference> AsyncScripts { get; set; } = new();

	[UsedImplicitly]
	public List<DeferScriptReference> DeferScripts { get; set; } = new();

	public static ValueTask<DynamicIndexConfiguration?> FromJsonAsync(Stream utf8Json, CancellationToken cancellationToken = default)
	{
		return JsonSerializer.DeserializeAsync<DynamicIndexConfiguration>(utf8Json, cancellationToken: cancellationToken);
	}

	public static async ValueTask<DynamicIndexConfiguration> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
	{
		await using Stream utf8Json = File.OpenRead(filePath);
		DynamicIndexConfiguration configuration = (await DynamicIndexConfiguration.FromJsonAsync(utf8Json, cancellationToken))!;
		return configuration;
	}
}
