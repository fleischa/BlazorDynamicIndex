namespace BlazorDynamicIndex;

public class ScriptReference : ContentReference
{
	public override string HtmlElement => $"<script src=\"{this.PrefixedSource}\"></script>";
}