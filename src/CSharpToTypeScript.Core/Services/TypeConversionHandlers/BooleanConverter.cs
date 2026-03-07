
using CSharpToTypeScript.Core.Constants;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers;

internal class BooleanConverter : BasicTypeConverterBase<Models.TypeNodes.Boolean>
{
    protected override IEnumerable<string> ConvertibleFromPredefined =>
    [
        PredefinedTypes.Bool
    ];

    protected override IEnumerable<string> ConvertibleFromIdentified =>
    [
        nameof(System.Boolean)
    ];
}