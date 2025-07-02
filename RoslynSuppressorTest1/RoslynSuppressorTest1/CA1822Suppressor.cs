using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace RoslynSuppressorTest1
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class CA1822Suppressor : DiagnosticSuppressor
    {
        private static readonly SuppressionDescriptor Descriptor = new SuppressionDescriptor(
            id: "XYZ0002",
            suppressedDiagnosticId: "CA1822",
            justification: "Because");

        public override ImmutableArray<SuppressionDescriptor> SupportedSuppressions =>
            ImmutableArray.Create(Descriptor);

        public override void ReportSuppressions(SuppressionAnalysisContext context)
        {
            foreach (var diagnostic in context.ReportedDiagnostics)
            {
                context.ReportSuppression(Suppression.Create(Descriptor, diagnostic));
            }
        }
    }
}
