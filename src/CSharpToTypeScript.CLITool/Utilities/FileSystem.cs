using System.Text.RegularExpressions;

namespace CSharpToTypeScript.CLITool.Utilities;

public static class FileSystem
{
    public static bool IsSameOrParentDirectory(this string parent, string child)
        => Path.GetFullPath(child).StartsWith(Path.GetFullPath(parent));

    public static string? ContainingDirectory(this string filePath)
        => new FileInfo(filePath).DirectoryName;

    public static IEnumerable<string> GetFilesWithExtension(string directoryPath, string extension)
       => Directory.GetFiles(directoryPath, $"*.{extension}", SearchOption.AllDirectories);

    public static bool EndsWithFileExtension(this string text)
        => Regex.IsMatch(text, @"\.\w+$");
}