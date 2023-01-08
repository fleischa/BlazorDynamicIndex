namespace BlazorDynamicIndex;

public class ScriptReference : ContentReference
{
	public override string HtmlElement
	{
		get { return $"<script src=\"{this.PrefixedSource}\"></script>"; }
	}
}
