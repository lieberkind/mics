using Roslyn.Compilers.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiCS
{
    class TypeManager
    {
        private CSharpTypeManager scriptTypeManager;
        private ScriptSharpTypeManager coreTypeManager;

        private static TypeManager instance;

        private TypeManager(CSharpTypeManager scriptTypeManager, ScriptSharpTypeManager coreTypeManager)
        {
            this.scriptTypeManager = scriptTypeManager;
            this.coreTypeManager = coreTypeManager;
        }


        // Todo: Find out what to do with this
        public static void Initiate(CSharpTypeManager scriptTypeManager, ScriptSharpTypeManager coreTypeManager)
        {
            //if (instance == null)            
            instance = new TypeManager(scriptTypeManager, coreTypeManager);
        }

        #region CSharpTypeManager functionality
        public static TypeSymbol GetReturnType(SimpleNameSyntax simpleName)
        {
            return instance.scriptTypeManager.GetReturnType(simpleName);
        }

        public static bool IsUserType(ClassDeclarationSyntax classDeclaration)
        {
            return instance.scriptTypeManager.IsUserType(classDeclaration);
        }

        public static bool IsUserType(TypeSymbol typeSymbol)
        {
            return instance.scriptTypeManager.IsUserType(typeSymbol);
        }

        /// <summary>
        /// Returns true if the specified type is
        /// a DOM type from the ScriptSharp namespace System.Html.
        /// </summary>
        public static bool IsDOMType(ClassDeclarationSyntax classDeclaration)
        {
            return classDeclaration.HasAttribute("ScriptName") || classDeclaration.HasAttribute("ScriptImport");
        }

        public static bool IsDOMType(TypeSymbol typeSymbol)
        {
            return instance.scriptTypeManager.IsDOMType(typeSymbol);
        }

        public bool IsClientSideType(string namespaceName, string typeName)
        {
            return instance.scriptTypeManager.IsClientSideType(namespaceName, typeName);
        }

        public static bool IsMixedSideMethod(string namespaceName, string typeName, string memberName)
        {
            return instance.scriptTypeManager.IsMixedSideMethod(namespaceName, typeName, memberName);
        }

        public static bool IsClientSideMethod(string namespaceName, string typeName, string memberName)
        {
            return instance.scriptTypeManager.IsClientSideMethod(namespaceName, typeName, memberName);
        }

        public static bool IsMixedSideType(string namespaceName, string typeName)
        {
            return instance.scriptTypeManager.IsMixedSideType(namespaceName, typeName);
        }

        public static TypeSymbol GetTypeSymbol(SyntaxNode node)
        {
            return instance.scriptTypeManager.GetTypeSymbol(node);
        }

        public static TypeSymbol GetTypeSymbol(ExpressionSyntax expression)
        {
            return instance.scriptTypeManager.GetTypeSymbol(expression);
        }
        #endregion
    }
}
