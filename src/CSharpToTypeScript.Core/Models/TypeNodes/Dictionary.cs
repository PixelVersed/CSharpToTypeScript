using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models.TypeNodes;

internal class Dictionary(TypeNode key, TypeNode value) : TypeNode
{
    public TypeNode Key { get; } = key;
    public TypeNode Value { get; } = value;

    public override IEnumerable<string> Requires => Key.Requires.Concat(Value.Requires).Distinct();

    public override string WriteTypeScript(CodeConversionOptions options, Context context)
        => "{ [key: " + Key.WriteTypeScript(options, context) + "]: " + Value.WriteTypeScript(options, context) + "; }";
}