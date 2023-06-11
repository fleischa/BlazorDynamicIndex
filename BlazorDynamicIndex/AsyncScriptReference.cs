namespace BlazorDynamicIndex;

[UsedImplicitly]
public class AsyncScriptReference : ContentReference
{
	public override string HtmlElement => $"<script src=\"{this.PrefixedSource}\" async></script>";
}