using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Tuple = CSharpToTypeScript.Core.Models.TypeNodes.Tuple;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers;

internal class TupleConverter(TypeConversionHandler converter) : TypeConversionHandler
{
    private readonly TypeConversionHandler _converter = converter;

    public override TypeNode Handle(TypeSyntax type)
    {
        static string Name(int index) => $"Item{index + 1}";

        if (type is TupleTypeSyntax tuple)
        {
            return new Tuple(
                elements: tuple.Elements.Select((element, index) => new Tuple.Element(
                    name: !string.IsNullOrEmpty(element.Identifier.Text) ? element.Identifier.Text : Name(index),
                    type: _converter.Handle(element.Type))));
        }
        else if (type is GenericNameSyntax generic && generic.Identifier.Text == nameof(System.Tuple))
        {
            return new Tuple(
                elements: generic.TypeArgumentList.Arguments.Select((argument, index) => new Tuple.Element(
                    name: Name(index),
                    type: _converter.Handle(argument))));
        }

        return base.Handle(type);
    }
}