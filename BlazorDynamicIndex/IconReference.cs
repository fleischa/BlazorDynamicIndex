namespace BlazorDynamicIndex;

public class IconReference : ContentReference
{
	public override string HtmlElement => $"<link rel=\"icon\" href=\"{this.PrefixedSource}\" />";
}
