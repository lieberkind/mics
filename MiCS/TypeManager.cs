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
        private ScriptTypeManager scriptTypeManager;
        private CoreTypeManager coreTypeManager;

        private TypeManager instance;

        public TypeManager(ScriptTypeManager scriptTypeManager, CoreTypeManager coreTypeManager)
        {
            this.scriptTypeManager = scriptTypeManager;
            this.coreTypeManager = coreTypeManager;
        }

        public TypeSymbol GetReturnType(SimpleNameSyntax simpleName)
        {
            return scriptTypeManager.GetReturnType(simpleName);
        }

        //public bool IsDOMType(ClassDeclarationSyntax classDeclaration)
        //{ 
        //    return 
        //}
    }
}
