using System.Text;

namespace BlazorDynamicIndex;

public class DynamicIndexCache
{
	public DynamicIndexCache(string indexFileName, string indexContent)
	{
		this.IndexFileName = indexFileName;
		this.IndexContent = indexContent;
		this.ContentLength = Encoding.UTF8.GetByteCount(indexContent);
	}

	public string IndexFileName { get; }

	public string IndexContent { get; }

	public long ContentLength { get; }
}