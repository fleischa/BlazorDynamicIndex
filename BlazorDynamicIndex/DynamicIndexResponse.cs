namespace BlazorDynamicIndex;

public class DynamicIndexResponse
{
	public DynamicIndexResponse(string indexContent, long contentLength)
	{
		this.IndexContent = indexContent;
		this.ContentLength = contentLength;
	}

	public string IndexContent { get; }

	public long ContentLength { get; }
}