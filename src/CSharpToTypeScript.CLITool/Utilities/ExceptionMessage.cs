namespace CSharpToTypeScript.CLITool.Utilities;

public static class ExceptionMessage
{
    public static IEnumerable<string?> Flatten(Exception? exception)
    {
        do
        {
            yield return exception?.Message;
            exception = exception?.InnerException;
        } while (exception != null);
    }
}