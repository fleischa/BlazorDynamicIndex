namespace BlazorDynamicIndex;

public class AsyncScriptReference : ContentReference
{
	public override string HtmlElement => $"<script src=\"{this.PrefixedSource}\" async></script>";
}
