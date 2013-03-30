﻿using Roslyn.Compilers.CSharp;
using ScriptSharp.ScriptModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS.Extensions
{

    public static class Symbols
    {
        static internal ScriptSharp.ScriptModel.ParameterSymbol Map(this ParameterSyntax p)
        {
            return new ScriptSharp.ScriptModel.ParameterSymbol(p.Identifier.ValueText, null, null, ParameterMode.InOut);
        }

        static internal ScriptSharp.ScriptModel.MethodSymbol Map(this MethodDeclarationSyntax mD, ScriptSharp.ScriptModel.TypeSymbol parent = null)
        {
            // Todo: Should this random namespace symbol be used here or should the actual namespace be applied!
            var parentNamespace = new ScriptSharp.ScriptModel.NamespaceSymbol("ns", null);

            var returnTypeStr = "";

            if (mD.ReturnType is IdentifierNameSyntax)      // Custom complex types.
                returnTypeStr = ((IdentifierNameSyntax)mD.ReturnType).Identifier.ValueText;
            else if (mD.ReturnType is PredefinedTypeSyntax) // Predefined types like void and string
                returnTypeStr = ((PredefinedTypeSyntax)mD.ReturnType).Keyword.ValueText;
            else
                throw new NotSupportedException();

            var returnType = new ClassSymbol(returnTypeStr, parentNamespace);
            var method = new ScriptSharp.ScriptModel.MethodSymbol(mD.Identifier.ValueText, parent, returnType);

            var i = new List<Statement>();
            foreach (var item in mD.Body.Statements)
            {
                i.Add(item.Map(parent));
            }
            var sI = new SymbolImplementation(i, null, "symbolImplementationThisIdentifier_" + method.GeneratedName);
            method.AddImplementation(sI);
            return method;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cD"></param>
        /// <param name="parentNamespace">
        /// Parent namespace should be provided so its
        /// not required to map the parent namespace in
        /// in the ClassDeclarationSyntax.Map(...)
        /// function as this creates cyclic mapping in
        /// n infinte loop.
        /// </param>
        /// <returns></returns>
        static internal ScriptSharp.ScriptModel.ClassSymbol Map(this ClassDeclarationSyntax cD, ScriptSharp.ScriptModel.NamespaceSymbol parentNamespace)
        {
            if (parentNamespace == null) throw new Exception("Parent namespace must be defined!");
            var cl = new ClassSymbol(cD.Identifier.ValueText, parentNamespace);

            foreach (var methodMember in cD.DescendantNodes().Where(m => m.Kind == SyntaxKind.MethodDeclaration))
            {
                var mD = (MethodDeclarationSyntax)methodMember;
                var mapMethod = false;
                if (mD.AttributeLists.Any())
                {
                    foreach (var attList in mD.AttributeLists)
                    {
                        foreach (AttributeSyntax att in attList.Attributes)
                        {
                            if (((IdentifierNameSyntax)att.Name).Identifier.ValueText.Equals("MixedSide"))
                            {
                                mapMethod = true;
                            }
                        }
                    }
                }
                if (mapMethod)
                {
                    cl.AddMember(mD.Map(cl));
                }
            }

            return cl;
        }

        static internal ScriptSharp.ScriptModel.NamespaceSymbol Map(this NamespaceDeclarationSyntax ns)
        {
            // Todo: Implement so that members are mapped as well!
            var SSNamespace = new ScriptSharp.ScriptModel.NamespaceSymbol(((IdentifierNameSyntax)ns.Name).Identifier.ValueText, null);
            foreach (var member in ns.Members)
            {
                if (member.Kind == SyntaxKind.ClassDeclaration)
                {
                    SSNamespace.Types.Add(((ClassDeclarationSyntax)member).Map(SSNamespace));
                }
            }
            return SSNamespace;
        }

    }
}
