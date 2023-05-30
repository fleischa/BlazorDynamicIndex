namespace BlazorDynamicIndex;

public class DeferScriptReference : ContentReference
{
	public override string HtmlElement => $"<script src=\"{this.PrefixedSource}\" defer></script>";
}