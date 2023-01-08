namespace BlazorDynamicIndex;

public class IconReference : ContentReference
{
	public override string HtmlElement
	{
		get { return $"<link rel=\"icon\" href=\"{this.PrefixedSource}\" />"; }
	}
}
