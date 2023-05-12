using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorDynamicIndex;

public static class EndpointRouteBuilderExtensions
{
	public static IEndpointConventionBuilder MapDynamicIndex(this IEndpointRouteBuilder endpointRouteBuilder, string? pattern = null)
	{
		DynamicIndexCache dynamicIndexCache = endpointRouteBuilder.ServiceProvider.GetRequiredService<DynamicIndexCache>();

		if (pattern == null)
		{
			pattern = dynamicIndexCache.IndexFileName;
		}

		return endpointRouteBuilder.Map(pattern, httpContext => EndpointRouteBuilderExtensions.HandleRequest(httpContext, dynamicIndexCache));
	}

	public static IEndpointConventionBuilder MapFallbackToDynamicIndex(this IEndpointRouteBuilder endpointRouteBuilder)
	{
		DynamicIndexCache dynamicIndexCache = endpointRouteBuilder.ServiceProvider.GetRequiredService<DynamicIndexCache>();
		return endpointRouteBuilder.MapFallback(httpContext => EndpointRouteBuilderExtensions.HandleRequest(httpContext, dynamicIndexCache));
	}

	private static Task HandleRequest(HttpContext httpContext, DynamicIndexCache? dynamicIndexCache)
	{
		HttpResponse response = httpContext.Response;
		response.ContentType = "text/html";

		if (dynamicIndexCache != null)
		{
			response.ContentLength = dynamicIndexCache.ContentLength;
			return response.WriteAsync(dynamicIndexCache.IndexContent);
		}

		response.StatusCode = StatusCodes.Status404NotFound;
		return response.WriteAsync("Not Found");
	}
}