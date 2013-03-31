using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using MiCS.Mappers;

namespace MiCS
{
    public class MiCSGenerator
    {
        public void Create(string filePath, ScriptManager scriptManager)
        {
            var sourceStr = File.ReadAllText(filePath);
            var syntaxTree = SyntaxTree.ParseText(sourceStr);
            CompilationUnitSyntax root = syntaxTree.GetRoot();

            var specs = GetMappingSpecifications(root);


            var SSNamespaces = new HashSet<ScriptSharp.ScriptModel.NamespaceSymbol>();
            foreach (NamespaceDeclarationSyntax nspace in root.Members.Where(m => m.Kind == SyntaxKind.NamespaceDeclaration))
            {
                if (specs.IsMixedSide(nspace))
                    SSNamespaces.Add(nspace.Map());

            }
        }



        private MappingSpecifications GetMappingSpecifications(CompilationUnitSyntax root)
        {
            // Todo: Nested namespaces are not supported currently!
            var specs = new MappingSpecifications();
            foreach (NamespaceDeclarationSyntax nspace in root.Members.Where(m => m.Kind == SyntaxKind.NamespaceDeclaration))
            {
                foreach (var classMember in nspace.DescendantNodes().Where(m => m.Kind == SyntaxKind.ClassDeclaration))
                {
                    var cD = (ClassDeclarationSyntax)classMember;
                    foreach (var methodMember in cD.DescendantNodes().Where(m => m.Kind == SyntaxKind.MethodDeclaration))
                    {
                        var mD = (MethodDeclarationSyntax)methodMember;
                        if (mD.AttributeLists.Any())
                        {
                            foreach (var attList in mD.AttributeLists)
                            {
                                foreach (AttributeSyntax att in attList.Attributes)
                                {
                                    if (((IdentifierNameSyntax)att.Name).Identifier.ValueText.Equals("MixedSide"))
                                    {
                                        specs.Add(nspace, cD, mD);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return specs;
        }



    }

    public class MappingSpecifications
    {
        public bool IsMixedSide(NamespaceDeclarationSyntax namespaceDeclaration)
        {
            return MixedSideNamespaces.Contains(namespaceDeclaration.Name.ToString());
        }
        public void Add(NamespaceDeclarationSyntax namespaceDeclaration, ClassDeclarationSyntax classDeclaration, MethodDeclarationSyntax methodDeclaration)
        {
            var namespaceName = namespaceDeclaration.Name.ToString();
            if (!MixedSideNamespaces.Contains(namespaceName))
                MixedSideNamespaces.Add(namespaceName);

            var className = namespaceName + "." + classDeclaration.Identifier.ValueText;
            if (!MixedSideClasses.Contains(className))
                MixedSideClasses.Add(className);

            var methodName = namespaceName + "." + className + "." + methodDeclaration.Identifier.ValueText;
            if (!MixedSideMethods.Contains(methodName))
                MixedSideMethods.Add(methodName);
        }
        
        private readonly HashSet<string> MixedSideNamespaces = new HashSet<string>();
        private readonly HashSet<string> MixedSideClasses = new HashSet<string>();
        private readonly HashSet<string> MixedSideMethods = new HashSet<string>();
    }
}
