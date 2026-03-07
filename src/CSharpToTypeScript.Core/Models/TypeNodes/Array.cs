using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes;

internal class Array(TypeNode of, int rank) : TypeNode
{
    public TypeNode Of { get; } = of;
    public int Rank { get; } = rank;

    public override IEnumerable<string> Requires => Of.Requires;

    public override string WriteTypeScript(CodeConversionOptions options, Context context)
        => // underlying type
        Of.WriteTypeScript(options, context).TransformIf(Of.IsUnionType(options), StringUtilities.Parenthesize)
        // brackets
        + "[]".Repeat(Rank);
}