using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes;

internal class Custom(string name) : NamedTypeNode(name)
{
    public override IEnumerable<string> Requires => [Name];

    public override string WriteTypeScript(CodeConversionOptions options, Context context)
        => Name.TransformIf(options.RemoveInterfacePrefix && context.GenericTypeParameters != null && !context.GenericTypeParameters.Contains(Name), StringUtilities.RemoveInterfacePrefix);
}