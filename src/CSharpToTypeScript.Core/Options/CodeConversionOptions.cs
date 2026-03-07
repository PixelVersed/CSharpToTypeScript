namespace CSharpToTypeScript.Core.Options;

public class CodeConversionOptions(bool export, bool useTabs, int? tabSize = null,
    DateOutputType convertDatesTo = DateOutputType.String, NullableOutputType convertNullablesTo = NullableOutputType.Null,
    bool toCamelCase = true, bool removeInterfacePrefix = true, ImportGenerationMode importGenerationMode = ImportGenerationMode.None,
    bool useKebabCase = false, bool appendModelSuffix = false, QuotationMark quotationMark = QuotationMark.Double,
    bool appendNewLine = false, bool stringEnums = false, bool enumStringToCamelCase = false,
    OutputType outputType = OutputType.Interface) : ModuleNameConversionOptions(useKebabCase, appendModelSuffix, removeInterfacePrefix)
{
    public bool Export { get; set; } = export;
    public bool UseTabs { get; set; } = useTabs;
    public int? TabSize { get; set; } = tabSize;
    public DateOutputType ConvertDatesTo { get; set; } = convertDatesTo;
    public NullableOutputType ConvertNullablesTo { get; set; } = convertNullablesTo;
    public bool ToCamelCase { get; set; } = toCamelCase;
    public ImportGenerationMode ImportGenerationMode { get; set; } = importGenerationMode;
    public QuotationMark QuotationMark { get; set; } = quotationMark;
    public bool AppendNewLine { get; set; } = appendNewLine;
    public bool StringEnums { get; set; } = stringEnums;
    public bool EnumStringToCamelCase { get; set; } = enumStringToCamelCase;
    public OutputType OutputType { get; set; } = outputType;
}