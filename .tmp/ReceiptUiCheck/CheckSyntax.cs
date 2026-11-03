using System;
using System.IO;
using System.Linq;
using System.Reflection;

internal static class CheckReceiptSyntax
{
    private const string CompilerDirectory = @"C:\Program Files (x86)\MSBuild\14.0\Bin";
    private static int Main(string[] paths)
    {
        AppDomain.CurrentDomain.AssemblyResolve += (sender, e) => {
            string path = Path.Combine(CompilerDirectory, new AssemblyName(e.Name).Name + ".dll");
            return File.Exists(path) ? Assembly.LoadFrom(path) : null;
        };
        foreach (string path in paths) if (!Check(path)) return 1;
        return 0;
    }

    private static bool Check(string path)
    {
        var tree = Microsoft.CodeAnalysis.VisualBasic.VisualBasicSyntaxTree.ParseText(File.ReadAllText(path));
        var errors = tree.GetDiagnostics().Where(d => d.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error).ToArray();
        foreach (var error in errors) Console.Error.WriteLine(error.ToString());
        if (errors.Length != 0) return false;
        Console.WriteLine("PASS Visual Basic syntax: " + path);
        return true;
    }
}
