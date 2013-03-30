using Roslyn.Compilers.CSharp;
using ScriptSharp;
using ScriptSharp.Generator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Mappers
{
    public class CSharpWalker : SyntaxWalker
    {
        ScriptTextWriter scriptTextWriter;
        ScriptGenerator scriptGenerator;

        public CSharpWalker() {
            scriptTextWriter = new ScriptTextWriter(new StringWriter());
            scriptGenerator = new ScriptGenerator(scriptTextWriter, new CompilerOptions(), null);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            Console.WriteLine("Visited namespace");
            base.VisitNamespaceDeclaration(node);
        }
    }
}
