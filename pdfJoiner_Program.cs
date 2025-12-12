//*
// This code requires the PdfSharp library.
// Run the following command in your terminal or Package Manager Console to install the core version of 
// PdfSharp (compatible with .NET Core and .NET 6/7/8):
// dotnet add package PdfSharp --version 6.1.1

// This script creates a new PDF, adds the first page of file2.pdf, and
// then appends all pages of file1.pdf, effectively "joining" them in that specific order.
// Files file1.pdf and file2.pdf must exist in the folder c:\dbase\ for this script to work properly.

// Type this in the terminal in Visual Studio Code in order to compile and generate an .exe file:
// dotnet publish -r win-x64 -c Release /p:PublishSingleFile=true /p:IncludeAllContent=true


using System;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

class Program
{
    static void Main()
    {
        // Settings
        Console.WriteLine(" ");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("PDF Page Joiner - Combining pages of file2.pdf with one-page-only file1.pdf");
        // Reset the color back to default (important so later text isn't cyan)
        Console.ResetColor();
        Console.WriteLine(" ");
        Console.WriteLine("Files file1.pdf and file2.pdf must exist in the folder c:/dbase/ for this script to work properly. ");
        Console.WriteLine(" ");
        Console.WriteLine("Processing...");
        // Use verbatim strings for Windows paths and ensure the filename is separated by a backslash
        // string file1Path =  @"C:\Users\ACER\Documents\EjemploC95\file2.pdf"; // The file containing one or more pages
        // string file2Path =  @"C:\Users\ACER\Documents\EjemploC95\file1.pdf"; // The file we need the FIRST page from
        // string outputName = @"C:\Users\ACER\Documents\EjemploC95\joined_output.pdf";
        string file1Path =  @"C:\dbase\file2.pdf"; // The file containing one or more pages
        string file2Path =  @"C:\dbase\file1.pdf"; // The file we need the FIRST page from
        string outputName = @"C:\dbase\joined_output.pdf";
        // Validate files exist
        if (!File.Exists(file1Path) || !File.Exists(file2Path))
        {
            Console.WriteLine("Error: One or both input files could not be found.");
            return;
        }

        try
        {
            // Create the output document
            using (PdfDocument outputDocument = new PdfDocument())
            {
                // STEP 1: Open file2 and get the FIRST page only
                // We use 'Import' mode to open files we aren't modifying directly
                using (PdfDocument pdf2 = PdfReader.Open(file2Path, PdfDocumentOpenMode.Import))
                {
                    if (pdf2.PageCount > 0)
                    {
                        // Add the first page (Index 0) of file2 to the output
                        outputDocument.AddPage(pdf2.Pages[0]);
                        Console.WriteLine($" ");
                        Console.WriteLine($"Added Page 1 from {file2Path}");
                    }
                }

                // STEP 2: Open file1 and get ALL pages
                using (PdfDocument pdf1 = PdfReader.Open(file1Path, PdfDocumentOpenMode.Import))
                {
                    int pagesAdded = 0;
                    foreach (PdfPage page in pdf1.Pages)
                    {
                        // Add every page from file1 to the output
                        outputDocument.AddPage(page);
                        pagesAdded++;
                    }
                    Console.WriteLine($" ");
                    Console.WriteLine($"Added {pagesAdded} pages from {file1Path}");
                }

                // STEP 3: Save the result
                outputDocument.Save(outputName);
                //Console.WriteLine($" ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"\nSuccess! Created {outputName}");
                // Reset the color back to default (important so later text isn't cyan)
                Console.ResetColor();
                Console.WriteLine($" ");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}



