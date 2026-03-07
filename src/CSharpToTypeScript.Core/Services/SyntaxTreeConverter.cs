using CSharpToTypeScript.Core.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpToTypeScript.Core.Services;

internal class SyntaxTreeConverter(RootTypeConverter rootTypeConverter, RootEnumConverter rootEnumConverter)
{
    private readonly RootTypeConverter _rootTypeConverter = rootTypeConverter;
    private readonly RootEnumConverter _rootEnumConverter = rootEnumConverter;

    public FileNode Convert(CompilationUnitSyntax root)
        => new FileNode(ConvertRootNodes(root));

    private IEnumerable<RootNode> ConvertRootNodes(CompilationUnitSyntax root)
        => root.DescendantNodes()
            .Where(node => (node is TypeDeclarationSyntax type && IsNotStatic(type)) || node is EnumDeclarationSyntax)
            .Select(node => node switch
            {
                TypeDeclarationSyntax type => (RootNode)_rootTypeConverter.Convert(type),
                EnumDeclarationSyntax @enum => _rootEnumConverter.Convert(@enum),
                _ => throw new ArgumentException("Unknown syntax type.")
            })
            .ToList();

    private bool IsNotStatic(TypeDeclarationSyntax type)
        => type.Modifiers.All(m => !m.IsKind(SyntaxKind.StaticKeyword));
}