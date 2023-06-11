namespace BlazorDynamicIndex;

[UsedImplicitly]
public class StyleSheetReference : ContentReference
{
	public override string HtmlElement => $"<link rel=\"stylesheet\" href=\"{this.PrefixedSource}\"/>";
}