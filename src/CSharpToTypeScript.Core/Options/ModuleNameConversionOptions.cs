namespace CSharpToTypeScript.Core.Options;

public class ModuleNameConversionOptions(bool useKebabCase, bool appendModelSuffix, bool removeInterfacePrefix = true)
{
    public bool UseKebabCase { get; set; } = useKebabCase;
    public bool AppendModelSuffix { get; set; } = appendModelSuffix;
    public bool RemoveInterfacePrefix { get; set; } = removeInterfacePrefix;
}