using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

namespace CSharpToTypeScript.Core.Models.TypeNodes;

internal class Tuple(IEnumerable<Tuple.Element> elements) : TypeNode
{
    public IEnumerable<Element> Elements { get; } = elements;

    public class Element(string name, TypeNode type) : IWritableNode, IDependentNode
    {
        public string Name { get; } = name;
        public TypeNode Type { get; } = type;

        public IEnumerable<string> Requires => Type.Requires;

        public string WriteTypeScript(CodeConversionOptions options, Context context)
            => // name
            Name.TransformIf(options.ToCamelCase, StringUtilities.ToCamelCase)
            // separator
            + "?".If(Type.IsOptional(options, out _)) + ": "
            // type
            + (Type.IsOptional(options, out var of) ? of?.WriteTypeScript(options, context) : Type.WriteTypeScript(options, context)) + ";";
    }

    public override IEnumerable<string> Requires => Elements.SelectMany(e => e.Requires).Distinct();

    public override string WriteTypeScript(CodeConversionOptions options, Context context)
        => "{ " + Elements.WriteTypeScript(options, context).ToSpaceSeparatedList() + " }";
}