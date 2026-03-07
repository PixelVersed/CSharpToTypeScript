namespace CSharpToTypeScript.Core.Models.TypeNodes;

internal abstract class NamedTypeNode(string name) : TypeNode
{
    public string Name { get; } = name;
}