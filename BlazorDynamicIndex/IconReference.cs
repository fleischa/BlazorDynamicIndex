using JetBrains.Annotations;

namespace BlazorDynamicIndex;

[UsedImplicitly]
public class IconReference : ContentReference
{
	public override string HtmlElement => $"<link rel=\"icon\" href=\"{this.PrefixedSource}\" />";
}