using CSharpToTypeScript.Core.Models.TypeNodes;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models;

internal class RootTypeNode(string name, IEnumerable<FieldNode> fields, IEnumerable<string> genericTypeParameters,
    IEnumerable<TypeNode> baseTypes, bool fromInterface) : RootNode
{
    public override string Name { get; } = name;
    public IEnumerable<FieldNode> Fields { get; } = fields;
    public IEnumerable<string> GenericTypeParameters { get; } = genericTypeParameters;
    public IEnumerable<TypeNode> BaseTypes { get; } = baseTypes;
    public bool FromInterface { get; } = fromInterface;

    public override IEnumerable<string> Requires
        => Fields.SelectMany(f => f.Requires)
            .Concat(BaseTypes.SelectMany(b => b.Requires))
            .Except(GenericTypeParameters)
            .Distinct();

    public override string WriteTypeScript(CodeConversionOptions options, Context context)
    {
        context = context.Clone();
        context.GenericTypeParameters = GenericTypeParameters;

        // keywords
        return "export ".If(options.Export)
            // type
            + (!FromInterface && options.OutputType == OutputType.Class ? "class" : "interface") + " "
            // name
            + Name.TransformIf(options.RemoveInterfacePrefix, StringUtilities.RemoveInterfacePrefix)
            // generic type parameters
            + ("<" + GenericTypeParameters.ToCommaSeparatedList() + ">").If(GenericTypeParameters.Any())
            // base types
            + (" extends " + BaseTypes.WriteTypeScript(options, context).ToCommaSeparatedList()).If(BaseTypes.Any())
            // body
            + " {" + NewLine
            // fields
            + Fields.WriteTypeScript(options, context).Indent(options.UseTabs, options.TabSize).LineByLine() + NewLine
            + "}";
    }
}