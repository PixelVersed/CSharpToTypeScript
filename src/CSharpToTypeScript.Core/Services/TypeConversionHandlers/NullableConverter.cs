using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Nullable = CSharpToTypeScript.Core.Models.TypeNodes.Nullable;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers;

internal class NullableConverter(TypeConversionHandler converter) : TypeConversionHandler
{
    private readonly TypeConversionHandler _converter = converter;

    public override TypeNode Handle(TypeSyntax type)
    {
        if (type is NullableTypeSyntax nullable)
        {
            return new Nullable(of: _converter.Handle(nullable.ElementType));
        }
        else if (type is GenericNameSyntax generic && generic.Identifier.Text == nameof(System.Nullable)
            && generic.TypeArgumentList.Arguments.Count == 1)
        {
            return new Nullable(of: _converter.Handle(generic.TypeArgumentList.Arguments.Single()));
        }

        return base.Handle(type);
    }
}