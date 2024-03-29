﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorDynamicIndex;

public static class EndpointRouteBuilderExtensions
{
	public static IEndpointConventionBuilder MapFallbackToDynamicIndex(this IEndpointRouteBuilder endpointRouteBuilder)
	{
		return endpointRouteBuilder.MapFallback(EndpointRouteBuilderExtensions.HandleRequest);
	}

	public static IEndpointConventionBuilder MapFallbackToDynamicIndex(this IEndpointRouteBuilder endpointRouteBuilder, string pattern)
	{
		return endpointRouteBuilder.MapFallback(pattern, EndpointRouteBuilderExtensions.HandleRequest);
	}

	private static async Task<Task> HandleRequest(HttpContext httpContext)
	{
		DynamicIndexCache dynamicIndexCache = httpContext.RequestServices.GetRequiredService<DynamicIndexCache>();
		DynamicIndexResponse index = await dynamicIndexCache.GetIndex(httpContext);

		HttpResponse response = httpContext.Response;
		response.ContentType = "text/html";
		response.ContentLength = index.ContentLength;
		return response.WriteAsync(index.IndexContent);
	}
}