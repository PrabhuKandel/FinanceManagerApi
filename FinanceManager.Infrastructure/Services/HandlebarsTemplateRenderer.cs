

using System.Reflection;
using FinanceManager.Application.Features.TransactionRecords.Queries.ExportToPdf;
using FinanceManager.Application.Interfaces.Services;
using HandlebarsDotNet;

namespace FinanceManager.Infrastructure.Services
{
    public class HandlebarsTemplateRenderer : ITemplateRenderer
    {
        private readonly Assembly _templateAssembly;

        public HandlebarsTemplateRenderer(Assembly templateAssembly)
        {
            _templateAssembly = templateAssembly;
        }
        public async Task<string> RenderTemplateAsync(string templateName, object data)
        {
            var assembly = typeof(ExportTransactionRecordsToPdfQuery).Assembly;
            Console.WriteLine("Embedded resources in assembly:");
            foreach (var r in assembly.GetManifestResourceNames())
                Console.WriteLine(r);


            var resourceName = _templateAssembly.GetManifestResourceNames()
         .FirstOrDefault(r => r.EndsWith(templateName, StringComparison.OrdinalIgnoreCase))
         ?? throw new FileNotFoundException($"Embedded template '{templateName}' not found.");


            using var stream = _templateAssembly.GetManifestResourceStream(resourceName)!;
            using var reader = new StreamReader(stream);
            var templateSource = await reader.ReadToEndAsync();

            var template = Handlebars.Compile(templateSource);
            return template(data);
        }
    
    }
}
