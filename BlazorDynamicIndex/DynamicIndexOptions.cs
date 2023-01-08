namespace BlazorDynamicIndex;

public class DynamicIndexOptions
{
	public const string ConfigurationSection = "DynamicIndex";

	public string? ConfigurationFile { get; set; }

	public string? OutputFile { get; set; }
}
