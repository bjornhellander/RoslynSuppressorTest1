using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

using Verifier = RoslynSuppressorTest1.Test.Verifiers.CSharpSuppressorVerifier<
    RoslynSuppressorTest1.SA1401Suppressor>;

namespace RoslynSuppressorTest1.Test
{
    [TestClass]
    public class SA1401SuppressorTests
    {
        private const string AssemblyName = "StyleCop.Analyzers.dll";
        private const string AnalyzerTypeName = "StyleCop.Analyzers.MaintainabilityRules.SA1401FieldsMustBePrivate";

        private static readonly DiagnosticResult Diagnostic = new("SA1401", DiagnosticSeverity.Warning);

        [TestMethod]
        public async Task TestSA1401()
        {
            const string testCode = @"
public class TestClass
{
    public int {|#0:value|};
}";

            var test = new Verifier.Test(AssemblyName, AnalyzerTypeName)
            {
                TestCode = testCode,
                ExpectedDiagnostics =
                {
                    Diagnostic.WithLocation(0).WithIsSuppressed(true),
                },
            };

            await test.RunAsync().ConfigureAwait(false);
        }
    }
}
