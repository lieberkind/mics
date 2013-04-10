//using Roslyn.Compilers.CSharp;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MiCS.Validators
//{
//    public class SyntaxTreeValidator : SyntaxWalker
//    {
//        public readonly Dictionary<string, TypeDeclarationSyntax> MixedSideTypes = new Dictionary<string, TypeDeclarationSyntax>();
//        public readonly Dictionary<string, TypeDeclarationSyntax> ClientSideTypes = new Dictionary<string, TypeDeclarationSyntax>();

//        public readonly Dictionary<string, MethodDeclarationSyntax> MixedSideMethods = new Dictionary<string, MethodDeclarationSyntax>();
//        public readonly Dictionary<string, MethodDeclarationSyntax> ClientSideMethods = new Dictionary<string, MethodDeclarationSyntax>();
        
//        CompilationUnitSyntax rootNode;

//        public SyntaxTreeValidator(CompilationUnitSyntax mixedSideRoot, CompilationUnitSyntax clientSideRoot)
//        {
//            this.rootNode = rootNode;

//            var classCollector = new ClassCollector();
//            var methodCollector = new MethodCollector();

//            classCollector.Visit(rootNode);
//            foreach (var @class in classCollector.Classes)
//            {
//                MappedTypes.Add(@class.Identifier.ValueText, @class);
//            }

//            methodCollector.Visit(rootNode);
//            foreach (var method in methodCollector.Methods)
//            {
//                MappedMethods.Add(method.Identifier.ValueText, method);
//            }
//        }

//        public void ValidateTree()
//        {
//            Visit(rootNode);
//        }

//        public override void VisitMemberAccessExpression(MemberAccessExpressionSyntax node)
//        {
//            if (!MappedMethods.ContainsKey(node.Name.Identifier.ValueText))
//                throw new Exception("Access to member without correct attribute!");
//        }
//    }
//}