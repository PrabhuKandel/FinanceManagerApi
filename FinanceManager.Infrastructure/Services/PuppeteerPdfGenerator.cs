
using FinanceManager.Application.Interfaces.Services;
using PuppeteerSharp;
using PuppeteerSharp.Media;

namespace FinanceManager.Infrastructure.Services
{
    public class PuppeteerPdfGenerator : IPdfGenerator
    {
        public async Task<byte[]> GeneratePdfAsync(string htmlContent)
        {
            var chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";

            // Launch headless Chrome using the system-installed executable
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = chromePath
            });

            await using var page = await browser.NewPageAsync();

            // Set HTML content
            await page.SetContentAsync(htmlContent);

            // Generate PDF
            var pdfBytes = await page.PdfDataAsync(new PdfOptions
            {
                Format = PaperFormat.A4,
                PrintBackground = true,
                MarginOptions = new MarginOptions
                {
                    Top = "20px",
                    Bottom = "20px",
                    Left = "15px",
                    Right = "15px"
                }
            });

            return pdfBytes;
        }
    }
}
