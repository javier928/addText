using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using PdfSharp.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Create a single A4 PDF named page0cover.pdf containing centered name/surname
        string nameAndSurname = (args != null && args.Length > 0)
            ? string.Join(" ", args)
            : "Javier_Garcia";
        string baseDir = AppContext.BaseDirectory;
        string outputPath = Path.Combine(baseDir, "page0cover.pdf");

        try
        {
            using (PdfDocument doc = new PdfDocument())
            {
                PdfPage page = doc.AddPage();
                page.Size = PdfSharp.PageSize.A4;

                using (XGraphics gfx = XGraphics.FromPdfPage(page))
                {
                    XFont font = new XFont("Courier New", 16);
                    XRect layout = new XRect(0, 0, page.Width.Point, page.Height.Point);
                    XStringFormat format = new XStringFormat { Alignment = XStringAlignment.Center, LineAlignment = XLineAlignment.Center };
                    gfx.DrawString(nameAndSurname, font, XBrushes.Black, layout, format);
                }

                doc.Save(outputPath);
            }

            Console.WriteLine($" ");
            Console.WriteLine($"Creating a one-page-PDF-file with centered text:");
            Console.WriteLine($" ");
            Console.WriteLine($"Working folder: {baseDir}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nSuccess! File created {outputPath}");
            Console.WriteLine($" ");
            Console.ResetColor();
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"An error occurred: {ex.Message}");
            Console.ResetColor();
        }
    }
}