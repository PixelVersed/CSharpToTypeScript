using CSharpToTypeScript.Core.Utilities;
using CSharpToTypeScript.Core.Models.TypeNodes;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models;

internal class FieldNode(string name, TypeNode type, string? jsonPropertyName = null) : IWritableNode, IDependentNode
{
    public string Name { get; } = name;
    public TypeNode Type { get; } = type;
    public string? JsonPropertyName { get; set; } = jsonPropertyName;

    public IEnumerable<string> Requires => Type.Requires;

    public string WriteTypeScript(CodeConversionOptions options, Context context)
        => // name
        (JsonPropertyName?
            .EscapeBackslashes()
            .EscapeQuotes(options.QuotationMark)
            .TransformIf(!JsonPropertyName.IsValidIdentifier(), StringUtilities.InQuotes(options.QuotationMark))
        ?? Name.TransformIf(options.ToCamelCase, StringUtilities.ToCamelCase))
        // separator
        + "?".If(Type.IsOptional(options, out _)) + ": "
        // type
        + (Type.IsOptional(options, out var of) ? of?.WriteTypeScript(options, context) : Type.WriteTypeScript(options, context)) + ";";
}