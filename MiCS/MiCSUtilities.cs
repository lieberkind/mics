using System;
using EnvDTE;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roslyn.Services;
using Roslyn.Compilers.CSharp;
using System.IO;

namespace MiCS
{
    public static class MiCSUtilities
    {
        public static SyntaxTree GetSyntaxTree(string solutionFileName, string fileName)
        {
            string currentSolutionDir = solutionFileName;

            IWorkspace ws = Workspace.LoadSolution(currentSolutionDir);
            ISolution s = ws.CurrentSolution;

            IEnumerable<IProject> projects = s.Projects;

            foreach (var project in projects)
            {
                var documents = project.Documents;

                foreach (var document in documents)
                {
                    if (document.Name == fileName)
                    {
                        SyntaxTree syntaxTree = (SyntaxTree)document.GetSyntaxTree();
                        return syntaxTree;
                    }
                }
            }
            throw new FileNotFoundException("File " + fileName + " was not found in solution");
        }
    }
}
