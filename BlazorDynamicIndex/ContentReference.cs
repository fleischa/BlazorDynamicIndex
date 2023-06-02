using JetBrains.Annotations;

namespace BlazorDynamicIndex;

public abstract class ContentReference
{
	public string? Source
	{
		get;
		[UsedImplicitly]
		set;
	}

	public string? Assembly
	{
		get;
		[UsedImplicitly]
		set;
	}

	public bool IsFramework
	{
		get;
		[UsedImplicitly]
		set;
	}

	protected string PrefixedSource
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