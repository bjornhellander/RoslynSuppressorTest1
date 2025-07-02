using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace RoslynSuppressorTest1.Test.Verifiers;

public static class CSharpSuppressorVerifier<TAnalyzer, TSuppressor>
    where TSuppressor : DiagnosticSuppressor, new()
    where TAnalyzer : DiagnosticAnalyzer, new()
{
    public class Test : CSharpAnalyzerVerifier<TSuppressor>.Test
    {
        private readonly DiagnosticAnalyzer analyzer;

        public Test()
        {
            analyzer = new TAnalyzer();
        }

        protected override IEnumerable<DiagnosticAnalyzer> GetDiagnosticAnalyzers()
        {
            return base.GetDiagnosticAnalyzers().Concat([analyzer]);
        }
    }
}
