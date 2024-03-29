﻿using System.Text;
using Microsoft.Extensions.FileProviders;

namespace BlazorDynamicIndex;

public static class DynamicIndexGenerator
{
	public static async Task<string> Generate(DynamicIndexConfiguration configuration, IFileProvider webRootFileProvider)
	{
		StringBuilder builder = new("<!DOCTYPE html>");

		if (string.IsNullOrEmpty(configuration.Lang))
		{
			builder.AppendLine("<html>");
		}
		else
		{
			builder.AppendLine($"<html lang=\"{configuration.Lang}\">");
		}

		builder.AppendLine("<head>");

		builder.AppendLine("<meta charset=\"utf-8\" />");

		if (configuration.Viewport != string.Empty)
		{
			string viewport = configuration.Viewport ?? "width=device-width, initial-scale=1";
			builder.AppendLine($"<meta name=\"viewport\" content=\"{viewport}\" />");
		}

		if (!string.IsNullOrEmpty(configuration.Title))
		{
			builder.AppendLine($"<title>{configuration.Title}</title>");
		}

		if (string.IsNullOrEmpty(configuration.Base))
		{
			builder.AppendLine("<base href=\"/\" />");
		}
		else
		{
			builder.AppendLine($"<base href=\"{configuration.Base}\" />");
		}

		foreach (IconReference icon in configuration.Icons)
		{
			builder.AppendLine(icon.HtmlElement);
		}

		foreach (StyleSheetReference styleSheet in configuration.StyleSheets)
		{
			builder.AppendLine(styleSheet.HtmlElement);
		}

		foreach (AsyncScriptReference script in configuration.AsyncScripts)
		{
			builder.AppendLine(script.HtmlElement);
		}

		foreach (DeferScriptReference script in configuration.DeferScripts)
		{
			builder.AppendLine(script.HtmlElement);
		}

		builder.AppendLine("</head>");

		if (!string.IsNullOrEmpty(configuration.BodyFile))
		{
			IFileInfo? bodyFile = webRootFileProvider.GetFileInfo(configuration.BodyFile);

			if (bodyFile is { Exists: true })
			{
				await using FileStream bodyFileStream = File.OpenRead(bodyFile.PhysicalPath);
				using StreamReader bodyStreamReader = new(bodyFileStream, Encoding.UTF8);
				builder.AppendLine((await bodyStreamReader.ReadToEndAsync()).Trim());
			}
			else
			{
				throw new InvalidOperationException();
			}
		}
		else
		{
			builder.AppendLine("<body id=\"app\"></body>");
		}

		builder.AppendLine("</html>");

		return builder.ToString();
	}
}