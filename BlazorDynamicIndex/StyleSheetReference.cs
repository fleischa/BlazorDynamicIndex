namespace BlazorDynamicIndex;

public class StyleSheetReference : ContentReference
{
	public override string HtmlElement
	{
		get { return $"<link rel=\"stylesheet\" href=\"{this.PrefixedSource}\"/>"; }
	}
}
