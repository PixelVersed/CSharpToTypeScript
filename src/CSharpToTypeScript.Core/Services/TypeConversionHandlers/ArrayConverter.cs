using System.Collections;
using System.Collections.ObjectModel;
using CSharpToTypeScript.Core.Models.TypeNodes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Array = CSharpToTypeScript.Core.Models.TypeNodes.Array;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers;

internal class ArrayConverter(TypeConversionHandler converter) : TypeConversionHandler
{
    private readonly TypeConversionHandler _converter = converter;

    private IEnumerable<string> ConvertibleFromIdentified =>
    [
        nameof(System.Array), nameof(Enumerable), nameof(IEnumerable), nameof(ICollection), nameof(IList)
    ];

    private IEnumerable<string> ConvertibleFromGeneric =>
    [
        nameof(List<object>), nameof(IList<object>), nameof(ICollection<object>), nameof(Collection<object>),
        nameof(IEnumerable<object>), nameof(ReadOnlyCollection<object>), nameof(IReadOnlyCollection<object>),
        nameof(IReadOnlyList<object>)
    ];

    public override TypeNode Handle(TypeSyntax type)
    {
        if (type is ArrayTypeSyntax array)
        {
            return new Array(
                of: _converter.Handle(array.ElementType),
                rank: array.RankSpecifiers.Aggregate(0, (total, specifier) => total + specifier.Rank));
        }
        else if (type is IdentifierNameSyntax identified && ConvertibleFromIdentified.Contains(identified.Identifier.Text))
        {
            return new Array(
                of: new Any(),
                rank: 1);
        }
        else if (type is GenericNameSyntax generic && ConvertibleFromGeneric.Contains(generic.Identifier.Text)
            && generic.TypeArgumentList.Arguments.Count == 1)
        {
            return new Array(
                of: _converter.Handle(generic.TypeArgumentList.Arguments.Single()),
                rank: 1);
        }

        return base.Handle(type);
    }
}