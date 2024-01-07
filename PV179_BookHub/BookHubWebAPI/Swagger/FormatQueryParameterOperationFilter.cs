using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookHubWebAPI.Swagger;

public class FormatQueryParameterOperationFilter : IOperationFilter
{
    private readonly string _name;
    private readonly string _description;
    private readonly string _defaultValue;
    private readonly IEnumerable<string> _options;
    private readonly bool _required;

    public FormatQueryParameterOperationFilter(string name, string description,
                                               string defaultValue, IEnumerable<string> options, 
                                               bool required)
    {
        _name = name;
        _description = description;
        _defaultValue = defaultValue;
        _required = required;
        _options = options;
    }
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = _name,
            In = ParameterLocation.Query,
            Description = _description,
            Required = _required,
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = _defaultValue != null ? new OpenApiString(_defaultValue) : null,
                Enum = _options?.Select(v => new OpenApiString(v)).ToList<IOpenApiAny>()
            }
        });
    }
}
