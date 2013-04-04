using MiCS.Builders;
using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS = ScriptSharp.ScriptModel;

namespace MiCS.Mappers
{

    public static class Symbols
    {
        static internal SS.ParameterSymbol Map(this ParameterSyntax p)
        {
            return new SS.ParameterSymbol(p.Identifier.ValueText, null, null, SS.ParameterMode.InOut);
        }

        static internal SS.MethodSymbol Map(this MethodDeclarationSyntax methodDeclaration, 
            SS.ClassSymbol parentClassReference, 
            SS.NamespaceSymbol parentNamespaceReference)
        {
            // Todo: Should this random namespace symbol be used here or should the actual namespace be applied!
            // Namespace is apparently only required for return type! Should it then be the namespase of the return type?
            //var parentNamespace = new ScriptSharp.ScriptModel.NamespaceSymbol("ns", null);

            var returnTypeStr = "";

            if (methodDeclaration.ReturnType is IdentifierNameSyntax)      // Custom complex types.
                returnTypeStr = ((IdentifierNameSyntax)methodDeclaration.ReturnType).Identifier.ValueText;
            else if (methodDeclaration.ReturnType is PredefinedTypeSyntax) // Predefined types like void and string
                returnTypeStr = ((PredefinedTypeSyntax)methodDeclaration.ReturnType).Keyword.ValueText;
            else
                throw new NotSupportedException("Method declaration return type is currently not supported.");

            var returnType = new ClassSymbol(returnTypeStr, parentNamespaceReference);
            var name = methodDeclaration.Identifier.ValueText;

            var method = new SS.MethodSymbol(name, parentClassReference, returnType);

            var implementationStatements = new List<Statement>();
            foreach (var roslynStatement in methodDeclaration.Body.Statements)
            {
                implementationStatements.Add(StatementBuilder.Map(roslynStatement, parentClassReference));
            }
            var sI = new SymbolImplementation(implementationStatements, null, "symbolImplementationThisIdentifier_" + method.GeneratedName);
            method.AddImplementation(sI);
            return method;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roslynClass"></param>
        /// <param name="ssParentNamespace">
        /// Parent namespace should be provided so its
        /// not required to map the parent namespace in
        /// in the ClassDeclarationSyntax.Map(...)
        /// function as this creates cyclic mapping in
        /// n infinte loop.
        /// </param>
        /// <returns></returns>
        static internal SS.ClassSymbol Map(this ClassDeclarationSyntax @class, SS.NamespaceSymbol ssParentNamespace)
        {
            if (ssParentNamespace == null)
                throw new Exception("Parent namespace reference is required by ScriptSharp infrastructure.");
            
            return new ClassSymbol(@class.Identifier.ValueText, ssParentNamespace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace"></param>
        /// <returns></returns>
        static internal SS.NamespaceSymbol Map(this NamespaceDeclarationSyntax @namespace)
        {
            return new SS.NamespaceSymbol(@namespace.NameText(), null);
        }

    }
}
