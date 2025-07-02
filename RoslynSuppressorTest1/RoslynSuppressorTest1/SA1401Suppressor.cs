using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace RoslynSuppressorTest1
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class SA1401Suppressor : DiagnosticSuppressor
    {
        private static readonly SuppressionDescriptor Descriptor = new SuppressionDescriptor(
            id: "XYZ0001",
            suppressedDiagnosticId: "SA1401",
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
