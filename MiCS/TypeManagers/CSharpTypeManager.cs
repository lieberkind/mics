﻿using MiCS.Validators;
using Roslyn.Compilers;
using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Html;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    class CSharpTypeManager
    {
        /// <summary>
        /// Helper to get TypeSymbols
        /// </summary>
        private TypeSymbolWalker typeSymbolWalker;

        /// <summary>
        /// The semantic model that contains user tree and DOM types
        /// </summary>
        public SemanticModel SemanticModel
        {
            get;
            private set;
        }

        /// <summary>
        /// Mixed side members.
        /// </summary>
        public Dictionary<string, Dictionary<string, List<string>>> MixedSideMembers
        {
            get;
            private set;
        }

        /// <summary>
        /// Client side members.
        /// </summary>
        public Dictionary<string, Dictionary<string, List<string>>> ClientSideMembers
        {
            get;
            private set;
        }

        /// <summary>
        /// User tree compilation unit
        /// </summary>
        public CompilationUnitSyntax CompilationUnit
        {
            get;
            private set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CSharpTypeManager"/> class.
        /// </summary>
        /// <param name="userTree">User syntax tree.</param>
        public CSharpTypeManager(SyntaxTree userTree)
        {
            var tree = SyntaxTree.ParseText(userTree.GetText() + ScriptSharp.TextSources.Web.Text);

            // Collects all members from ScriptSharp.Web
            var builtInCollector = new Collector(SyntaxTree.ParseText(ScriptSharp.TextSources.Web.Text).GetRoot());

            CompilationUnit = tree.GetRoot();

            var mixedSideCollector = new Collector(CompilationUnit, new List<string>() { "MixedSide" });
            var clientSideCollector = new Collector(CompilationUnit, new List<string>() { "ClientSide" });

            builtInCollector.Collect();
            mixedSideCollector.Collect();
            clientSideCollector.Collect();

            MixedSideMembers = mixedSideCollector.Members;
            ClientSideMembers = clientSideCollector.Members;
            ClientSideMembers.AddRange(builtInCollector.Members);

            var mscorlib = new MetadataFileReference(typeof(String).Assembly.Location);
            var systemTextRegularExpression = new MetadataFileReference(typeof(System.Text.RegularExpressions.Regex).Assembly.Location);

            var compilation = Compilation.Create("Compilation", syntaxTrees: new[] { tree }, references: new[] { mscorlib, systemTextRegularExpression });
            SemanticModel = compilation.GetSemanticModel(tree);

            typeSymbolWalker = new TypeSymbolWalker(SemanticModel);

        }

        /// <summary>
        /// Returns true if the specified type declaration is
        /// a user defined type.
        /// </summary>
        public bool IsUserType(ClassDeclarationSyntax classDeclaration)
        {
            foreach (var member in classDeclaration.Members)
            {
                if (member is MethodDeclarationSyntax)
                {
                    var declaration = ((MethodDeclarationSyntax)member);
                    if (declaration.HasAttribute("MixedSide") ||
                        declaration.HasAttribute("ClientSide"))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a user defined type.
        /// </summary>
        public bool IsUserType(TypeSymbol typeSymbol)
        {
            if (typeSymbol.DeclaringSyntaxNodes.Count != 1)
                return false;

            var declaration = typeSymbol.DeclaringSyntaxNodes[0];

            if (declaration is ClassDeclarationSyntax)
                return IsUserType((ClassDeclarationSyntax)declaration);

            return false;
        }

        /// <summary>
        /// Determines if a type is client side from a namespace name and a type name
        /// </summary>
        /// <param name="namespaceName">Namespace name</param>
        /// <param name="typeName">Type name</param>
        /// <returns>True if type is client side, false otherwise</returns>
        public bool IsClientSideType(string namespaceName, string typeName)
        {
            return ClientSideMembers.ContainsKey(namespaceName) && ClientSideMembers[namespaceName].ContainsKey(typeName);
        }

        /// <summary>
        /// Determines if a type is mixed side from a namespace name and a type name
        /// </summary>
        /// <param name="namespaceName">Namespace name</param>
        /// <param name="typeName">Type name</param>
        /// <returns>True if type is mixed side, false otherwise</returns>
        public bool IsMixedSideType(string namespaceName, string typeName)
        {
            return MixedSideMembers.ContainsKey(namespaceName) && MixedSideMembers[namespaceName].ContainsKey(typeName);
        }

        /// <summary>
        /// Determines if a member is client side from a namespace name, a type name and a member name
        /// </summary>
        /// <param name="namespaceName">Namespaace name</param>
        /// <param name="typeName">Type name</param>
        /// <param name="memberName">Member name</param>
        /// <returns>Returns true if member is client side, false otherwise</returns>
        public bool IsClientSideMethod(string namespaceName, string typeName, string memberName)
        {
            return 
                ClientSideMembers.ContainsKey(namespaceName) &&
                ClientSideMembers[namespaceName].ContainsKey(typeName) &&
                ClientSideMembers[namespaceName][typeName].Contains(memberName);
        }

        /// <summary>
        /// Determines if a member is mixed side from a namespace name, a type name and a member name
        /// </summary>
        /// <param name="namespaceName">Namespace name</param>
        /// <param name="typeName">Type name</param>
        /// <param name="memberName">Member name</param>
        /// <returns>Returns true if member is mixed side, false otherwise</returns>
        public bool IsMixedSideMethod(string namespaceName, string typeName, string memberName)
        {
            return 
                MixedSideMembers.ContainsKey(namespaceName) &&
                MixedSideMembers[namespaceName].ContainsKey(typeName) &&
                MixedSideMembers[namespaceName][typeName].Contains(memberName);
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a DOM type from the ScriptSharp namespace System.Html.
        /// </summary>
        public bool IsDOMType(TypeSymbol typeSymbol)
        {
            if (typeSymbol.DeclaringSyntaxNodes.Count != 1)
                return false;

            var declaration = typeSymbol.DeclaringSyntaxNodes[0];
            if (declaration is ClassDeclarationSyntax)
            {
                var @class = (ClassDeclarationSyntax)declaration;
                return @class.IsDOMType();
            }
            else
                throw new NotSupportedException();
        }

        public TypeSymbol GetTypeSymbol(SyntaxNode node)
        {
            typeSymbolWalker.Visit(node);
            return typeSymbolWalker.TypeSymbol;
        }

        public TypeSymbol GetContainingType(IdentifierNameSyntax node)
        {
            var symbol = SemanticModel.GetSymbolInfo(node).Symbol;
            return symbol.ContainingType;
        }

        public TypeSymbol GetTypeSymbol(ExpressionSyntax expression)
        {
            return typeSymbolWalker.GetTypeSymbol(expression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public TypeSymbol GetReturnType(SimpleNameSyntax node)
        {
            var symbol = SemanticModel.GetSymbolInfo(node).Symbol;

            if (symbol == null)
                throw new Exception("Symbol is null. Can be caused by invalid C# syntax.");
            else if (!(symbol is MethodSymbol))
                throw new NotSupportedException();

            return ((MethodSymbol)symbol).ReturnType;
        }

        internal SymbolInfo GetSymbolInfo(SimpleNameSyntax simpleName)
        {
            return SemanticModel.GetSymbolInfo(simpleName);
        }
    }
}
