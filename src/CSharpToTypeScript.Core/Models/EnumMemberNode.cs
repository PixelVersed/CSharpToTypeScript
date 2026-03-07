using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models;

internal class EnumMemberNode(string name, string? value) : IWritableNode
{
    public string Name { get; } = name;
    public string? Value { get; } = value;

    public string WriteTypeScript(CodeConversionOptions options, Context context)
    {
        var value = options.StringEnums
            ? Name.TransformIf(options.EnumStringToCamelCase, StringUtilities.ToCamelCase)
                  .InQuotes(options.QuotationMark)
            : Value?.SquashWhiteSpace();

        return Name + (" = " + value).If(!string.IsNullOrWhiteSpace(value));
    }
}