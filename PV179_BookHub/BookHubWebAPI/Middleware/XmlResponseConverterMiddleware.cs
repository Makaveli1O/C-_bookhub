using System.Xml.Linq;
using Infrastructure.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BookHubWebAPI.Middleware;

public class XmlResponseConverterMiddleware
{
    private const string responseKey = "format";
    private const string xmlExtension = "xml";
    private const string xmlContentType = "application/xml";

    private readonly RequestDelegate _next;

    public XmlResponseConverterMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (TransformToXmlNotNeeded(context))
        {
            await _next(context);
            return;
        }

        var originalBodyStream = context.Response.Body;

        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();

        var modifiedResponse = ConvertToXml(responseBodyText);

        if (modifiedResponse == null)
        {
            throw new XmlResponseConverterMiddlewareExcaption("Unable to convert JSON response to XML.");
        }

        context.Response.Body = originalBodyStream;
        context.Response.ContentType = xmlContentType;

        await context.Response.WriteAsync(modifiedResponse);

    }

    private bool TransformToXmlNotNeeded(HttpContext context)
        => (!context.Request.Query.ContainsKey(responseKey))
            || (context.Request.Query[responseKey].ToString().ToLower() != xmlExtension);

    private string? ConvertToXml(string responseBodyText)
    {
        try
        {
            var token = JToken.Parse(responseBodyText);

            var rootElementName = "Object";

            if (token is JArray)
            {
                responseBodyText = "{\"Object\":" + responseBodyText + "}";
                rootElementName = "List";
            }

            XNode? node = JsonConvert.DeserializeXNode(responseBodyText, rootElementName);

            var response = node?.ToString(SaveOptions.DisableFormatting);

            return response;
        }
        catch (Exception)
        {
            return null;
        }
    }
}
