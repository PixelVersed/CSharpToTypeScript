using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Utilities;

using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models;

internal class RootEnumNode(string name, IEnumerable<EnumMemberNode> members) : RootNode
{
    public override string Name { get; } = name;
    public IEnumerable<EnumMemberNode> Members { get; } = members;

    public override string WriteTypeScript(CodeConversionOptions options, Context context)
        =>  // keywords
        "export ".If(options.Export) + "enum "
         // name
         + Name
        // body
        + " {" + NewLine
        // members
        + Members.WriteTypeScript(options, context).Indent(options.UseTabs, options.TabSize).LineByLine(separator: ",") + NewLine
        + "}";
}