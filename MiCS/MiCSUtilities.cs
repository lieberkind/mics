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

        private static string GetCurrentSolutionDirectory()
        {
            // Solution found here: http://stackoverflow.com/questions/2054182/programmatically-getting-the-current-visual-studio-ide-solution-directory-from-a
            EnvDTE.DTE dte = (EnvDTE.DTE)System.Runtime.InteropServices.Marshal.GetActiveObject("VisualStudio.DTE");
            return dte.Solution.FullName;
        }

        public static SyntaxTree GetSyntaxTree(string fileName)
        {
            string currentSolutionDir = MiCSUtilities.GetCurrentSolutionDirectory();

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

        public static SyntaxTree GetSyntaxTree(string solutionPath, string fileName)
        {
            string currentSolutionDir = solutionPath;

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
