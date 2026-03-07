using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes;

internal class Generic(string name, IEnumerable<TypeNode> arguments) : NamedTypeNode(name)
{
    public override IEnumerable<string> Requires
        => new[] { Name }.Concat(Arguments.SelectMany(a => a.Requires)).Distinct();

    public IEnumerable<TypeNode> Arguments { get; } = arguments;

    public override string WriteTypeScript(CodeConversionOptions options, Context context)
    {
        var x =  // name
           Name.TransformIf(options.RemoveInterfacePrefix && context.GenericTypeParameters != null && !context.GenericTypeParameters.Contains(Name), StringUtilities.RemoveInterfacePrefix)
           // generic type parameters
           + "<" + Arguments.WriteTypeScript(options, context).ToCommaSeparatedList() + ">";
        return x;
    }
}