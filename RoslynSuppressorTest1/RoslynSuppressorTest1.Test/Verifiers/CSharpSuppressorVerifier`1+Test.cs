using Microsoft.CodeAnalysis.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RoslynSuppressorTest1.Test.Verifiers;

public static class CSharpSuppressorVerifier<TSuppressor>
    where TSuppressor : DiagnosticSuppressor, new()
{
    public class Test : CSharpAnalyzerVerifier<TSuppressor>.Test
    {
        private readonly DiagnosticAnalyzer analyzer;

        public Test(string assemblyName, string analyzerTypeName)
        {
            var assembly = Assembly.LoadFrom(assemblyName);
            var analyzerType = assembly.GetType(analyzerTypeName)!;
            analyzer = (DiagnosticAnalyzer)Activator.CreateInstance(analyzerType)!;
        }

        protected override IEnumerable<DiagnosticAnalyzer> GetDiagnosticAnalyzers()
        {
            return base.GetDiagnosticAnalyzers().Concat([analyzer]);
        }
    }
}
