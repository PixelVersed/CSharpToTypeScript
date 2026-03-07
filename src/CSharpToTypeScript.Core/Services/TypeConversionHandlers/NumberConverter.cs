using CSharpToTypeScript.Core.Constants;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers;

internal class NumberConverter : BasicTypeConverterBase<Number>
{
    protected override IEnumerable<string> ConvertibleFromPredefined =>
    [
        PredefinedTypes.Byte, PredefinedTypes.SByte, PredefinedTypes.Int, PredefinedTypes.UInt,
        PredefinedTypes.Long, PredefinedTypes.ULong, PredefinedTypes.Float, PredefinedTypes.Double,
        PredefinedTypes.Decimal, PredefinedTypes.Short, PredefinedTypes.UShort
    ];

    protected override IEnumerable<string> ConvertibleFromIdentified =>
    [
        nameof(Byte), nameof(SByte), nameof(Decimal), nameof(Double), nameof(Single),
        nameof(Int32), nameof(UInt32), nameof(Int64), nameof(UInt64),
        nameof(Int16), nameof(UInt16)
    ];
}