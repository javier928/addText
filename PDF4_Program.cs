using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

class Program
{
    static void Main()
    {
        QuestPDF.Settings.License = LicenseType.Community;

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== PDF Generator Menu ===");
            Console.WriteLine(" ");
            Console.WriteLine("1. Create PDF with centered image");
            Console.WriteLine("2. Create PDF with 2 cm left/right margins");
            Console.WriteLine("3. Exit");
            Console.WriteLine(" ");
            Console.Write("Choose an option: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    CreateCenteredPdf();
                    break;

                case "2":
                    CreateMarginPdf();
                    break;

                case "3":
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid option. Press Enter to continue.");
                    Console.ReadLine();
                    break;
            }
        }
    }

    // -----------------------------
    // IMAGE SELECTION SUBMENU
    // -----------------------------
    static string? ChooseImage()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Choose an Image ===");
            Console.WriteLine("Enter an image file path or a folder path.");
            Console.WriteLine("Leave empty to use the current application folder.");
            Console.Write("Path: ");

            var input = Console.ReadLine()?.Trim();
            var path = string.IsNullOrWhiteSpace(input) ? AppContext.BaseDirectory : input!;

            if (File.Exists(path))
            {
                if (IsSupportedImage(path))
                    return path;

                Console.WriteLine("The selected file is not a supported image format.");
                Console.WriteLine("Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            if (!Directory.Exists(path))
            {
                Console.WriteLine("The specified path does not exist.");
                Console.WriteLine("Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            var images = Directory.GetFiles(path, "*.png")
                                  .Concat(Directory.GetFiles(path, "*.jpg"))
                                  .Concat(Directory.GetFiles(path, "*.jpeg"))
                                  .ToList();

            if (images.Count == 0)
            {
                Console.WriteLine("No supported images found in the selected folder.");
                Console.WriteLine("Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            Console.Clear();
            Console.WriteLine("=== Choose an Image ===");
            for (int i = 0; i < images.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(images[i])}");
            }

            Console.Write("Select an image number: ");
            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice >= 1 && choice <= images.Count)
            {
                return images[choice - 1];
            }

            Console.WriteLine("Invalid selection. Press Enter to try again.");
            Console.ReadLine();
        }
    }

    static bool IsSupportedImage(string path)
    {
        var extension = Path.GetExtension(path).ToLowerInvariant();
        return extension == ".png" || extension == ".jpg" || extension == ".jpeg";
    }

    static string? ChooseDestinationFolder()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Choose a destination folder for the resulting PDF ===");
            Console.WriteLine("Enter a folder path, or leave empty to use the current application folder.");
            Console.Write("Destination folder: ");

            var input = Console.ReadLine()?.Trim();
            var path = string.IsNullOrWhiteSpace(input) ? AppContext.BaseDirectory : input!;

            if (Directory.Exists(path))
            {
                return path;
            }

            if (File.Exists(path))
            {
                Console.WriteLine("The specified path is a file, not a folder.");
                Console.WriteLine("Press Enter to try again.");
                Console.ReadLine();
                continue;
            }

            Console.WriteLine("The specified folder does not exist.");
            Console.WriteLine("Press Enter to try again.");
            Console.ReadLine();
        }
    }

    // -----------------------------
    // AUTO-SCALING HELPER
    // -----------------------------
    static void AutoScaledImage(IContainer container, byte[] imageData)
    {
        container
            .AlignCenter()
            .AlignMiddle()
            .Image(imageData)
            .FitArea();
    }

    // -----------------------------
    // OPTION 1: CENTERED IMAGE
    // -----------------------------
    static void CreateCenteredPdf()
    {
        var destinationFolder = ChooseDestinationFolder();
        if (destinationFolder == null) return;

        var imagePath = ChooseImage();
        if (imagePath == null) return;

        var imageData = File.ReadAllBytes(imagePath);
        var outputPath = Path.Combine(destinationFolder, "centered.pdf");

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(20);
                page.PageColor(Colors.White);

                page.Content().Element(c => AutoScaledImage(c, imageData));
            });
        })
        .GeneratePdf(outputPath);

        Console.WriteLine($"PDF created: {outputPath}");
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }

    // -----------------------------
    // OPTION 2: 2 CM MARGINS
    // -----------------------------
    static void CreateMarginPdf()
    {
        var destinationFolder = ChooseDestinationFolder();
        if (destinationFolder == null) return;

        var imagePath = ChooseImage();
        if (imagePath == null) return;

        var imageData = File.ReadAllBytes(imagePath);
        var outputPath = Path.Combine(destinationFolder, "margin2cm.pdf");

        float marginCm = 56.7f; // 2 cm in points

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.MarginLeft(marginCm);
                page.MarginRight(marginCm);
                page.MarginTop(20);
                page.MarginBottom(20);
                page.PageColor(Colors.White);

                page.Content().Element(c => AutoScaledImage(c, imageData));
            });
        })
        .GeneratePdf(outputPath);

        Console.WriteLine($"PDF created: {outputPath}");
        Console.WriteLine("Press Enter to continue.");
        Console.ReadLine();
    }
}
