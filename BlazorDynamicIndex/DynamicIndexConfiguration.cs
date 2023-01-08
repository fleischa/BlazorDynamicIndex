namespace BlazorDynamicIndex;

using System.Text.Json;

public class DynamicIndexConfiguration
{
	public string? Lang { get; set; }

	public string? Viewport { get; set; }

	public string? Base { get; set; }

	public string? Title { get; set; }

	public string? BodyFile { get; set; }

	public List<StyleSheetReference> StyleSheets { get; set; } = new();

	public List<IconReference> Icons { get; set; } = new();

	public List<ScriptReference> HeadScripts { get; set; } = new();

	public List<ScriptReference> PreBodyScripts { get; set; } = new();

	public List<ScriptReference> PreFrameworkScripts { get; set; } = new();

	public ScriptReference? FrameworkScript { get; set; }

	public List<ScriptReference> PostFrameworkScripts { get; set; } = new();

	public static ValueTask<DynamicIndexConfiguration?> FromJsonAsync(Stream utf8Json, CancellationToken cancellationToken = default)
	{
		return JsonSerializer.DeserializeAsync<DynamicIndexConfiguration>(utf8Json, cancellationToken: cancellationToken);
	}

	public static async ValueTask<DynamicIndexConfiguration?> FromFileAsync(string filePath, CancellationToken cancellationToken = default)
	{
		await using Stream utf8Json = File.OpenRead(filePath);
		DynamicIndexConfiguration? configuration = await DynamicIndexConfiguration.FromJsonAsync(utf8Json, cancellationToken);
		return configuration;
	}
}
