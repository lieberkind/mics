using MiCS.Walkers;
using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Mappers
{

    public static class Symbols
    {
        static internal ScriptSharp.ScriptModel.ParameterSymbol Map(this ParameterSyntax p)
        {
            return new ScriptSharp.ScriptModel.ParameterSymbol(p.Identifier.ValueText, null, null, ParameterMode.InOut);
        }

        static internal ScriptSharp.ScriptModel.MethodSymbol Map(this MethodDeclarationSyntax methodDeclaration, 
            ScriptSharp.ScriptModel.ClassSymbol parentClassReference, 
            ScriptSharp.ScriptModel.NamespaceSymbol parentNamespaceReference)
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
            var method = new ScriptSharp.ScriptModel.MethodSymbol(name, parentClassReference, returnType);



            var i = new List<Statement>();
            foreach (var statement in methodDeclaration.Body.Statements)
            {
                i.Add(StatementWalker.Map(statement, parentClassReference));
            }
            var sI = new SymbolImplementation(i, null, "symbolImplementationThisIdentifier_" + method.GeneratedName);
            method.AddImplementation(sI);
            return method;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roslynClass"></param>
        /// <param name="parentNamespaceReference">
        /// Parent namespace should be provided so its
        /// not required to map the parent namespace in
        /// in the ClassDeclarationSyntax.Map(...)
        /// function as this creates cyclic mapping in
        /// n infinte loop.
        /// </param>
        /// <returns></returns>
        static internal ScriptSharp.ScriptModel.ClassSymbol Map(this ClassDeclarationSyntax roslynClass, ScriptSharp.ScriptModel.NamespaceSymbol parentNamespaceReference)
        {
            if (parentNamespaceReference == null) 
                throw new Exception("Parent namespace reference is required by ScriptSharp infrastructure.");
            
            var scriptSharpClass = new ClassSymbol(roslynClass.Identifier.ValueText, parentNamespaceReference);
            var scriptSharpMethods = MethodWalker.GetMethodsIn(roslynClass, scriptSharpClass, parentNamespaceReference);
            scriptSharpClass.Members.AddRange(scriptSharpMethods);

            return scriptSharpClass;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roslynNamespace"></param>
        /// <returns></returns>
        static internal ScriptSharp.ScriptModel.NamespaceSymbol Map(this NamespaceDeclarationSyntax roslynNamespace)
        {
            var scriptSharpNamespace = new ScriptSharp.ScriptModel.NamespaceSymbol(roslynNamespace.NameText(), null);
            var scriptSharpClasses = ClassWalker.GetClassesIn(roslynNamespace, requiredNamespaceReference: scriptSharpNamespace);

            scriptSharpNamespace.Types.AddRange(scriptSharpClasses);

            return scriptSharpNamespace;
        }

    }
}
