namespace BlazorDynamicIndex;

public abstract class ContentReference
{
	public string? Source { get; set; }

	public string? Assembly { get; set; }

	public string PrefixedSource
	{
		get { return (string.IsNullOrEmpty(this.Assembly) ? this.Source : $"_content/{this.Assembly}/{this.Source}") ?? string.Empty; }
	}

	public abstract string HtmlElement { get; }
}
