using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

using Verifier = RoslynSuppressorTest1.Test.Verifiers.CSharpSuppressorVerifier<
    Microsoft.CodeQuality.Analyzers.QualityGuidelines.MarkMembersAsStaticAnalyzer,
    RoslynSuppressorTest1.CA1822Suppressor>;

namespace RoslynSuppressorTest1.Test
{
    [TestClass]
    public class CA1822SuppressorTests
    {
        private static readonly DiagnosticResult Diagnostic = new("CA1822", DiagnosticSeverity.Info);

        [TestMethod]
        public async Task TestCA1822()
        {
            const string testCode = @"
public class TestClass
{
    public TestClass {|#0:TestProperty|} => new TestClass();
}";

            var test = new Verifier.Test()
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
