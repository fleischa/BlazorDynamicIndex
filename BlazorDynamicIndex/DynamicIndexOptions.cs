namespace BlazorDynamicIndex;

public class DynamicIndexOptions
{
	public const string ConfigurationSection = "DynamicIndex";

	public string? ConfigurationFile { get; set; }

	public string? IndexFileName { get; set; } = "index.html";
}
