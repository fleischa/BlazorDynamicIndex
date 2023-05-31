namespace BlazorDynamicIndex;

public abstract class ContentReference
{
	public string? Source { get; set; }

	public string? Assembly { get; set; }

	public bool IsFramework { get; set; }

	public string PrefixedSource
	{
		get
		{
			if (string.IsNullOrEmpty(this.Source))
			{
				throw new ArgumentOutOfRangeException();
			}

			if (this.IsFramework)
			{
				return $"_framework/{this.Source}";
			}

			if (!string.IsNullOrEmpty(this.Assembly))
			{
				return $"_content/{this.Assembly}/{this.Source}";
			}

			return this.Source;
		}
	}

	public abstract string HtmlElement { get; }
}
