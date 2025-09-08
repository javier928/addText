using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class ImageTextOverlay
{
    public static void Main(string[] args)
    {
        // Print the working directory
        Console.WriteLine(" ");
        Console.WriteLine("Current working directory: " + Directory.GetCurrentDirectory());
        Console.WriteLine(" ");

        // Determine image file path and text to insert
        string imageFilePath;
        string textToInsert;
        string outputFilePath = "output.png"; // Output file name

        // Check if arguments are provided, otherwise use defaults
        if (args.Length == 0)
        {
            imageFilePath = "background.png"; // Default image file
            textToInsert = "javier928@gmail.com"; // Default text
        }
        else if (args.Length == 1)
        {
            imageFilePath = args[0];
            textToInsert = "javier928@gmail.com"; // Default text if only image is provided
        }
        else
        {
            imageFilePath = args[0];
            textToInsert = args[1];
        }

        // Display image filename to be processed
        Console.WriteLine($"Image file to be processed: {imageFilePath}");
        Console.WriteLine(" ");

        // Display image size (in pixels)
        try
        {
            using (Bitmap tempImage = new Bitmap(imageFilePath))
            {
                Console.WriteLine($"Image size: {tempImage.Width} x {tempImage.Height} pixels");
                Console.WriteLine(" ");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Unable to load image to determine its size.");
            Console.WriteLine(" ");
        }

        // Ask user for text alignment
        Console.WriteLine("Press <1> to display text in the left top corner of the image.");
        Console.WriteLine("Press <2> to display text in the center of the top part of the image.");
        Console.WriteLine("Press <3> to display text in the right top corner of the image.");
        Console.Write("Your choice: ");
        string alignmentChoice = Console.ReadLine();

        StringAlignment horizontalAlignment = StringAlignment.Center;
        float x = 0;

        switch (alignmentChoice)
        {
            case "1":
                horizontalAlignment = StringAlignment.Near;
                break;
            case "2":
                horizontalAlignment = StringAlignment.Center;
                break;
            case "3":
                horizontalAlignment = StringAlignment.Far;
                break;
            default:
                Console.WriteLine("Invalid choice. Defaulting to center.");
                horizontalAlignment = StringAlignment.Center;
                break;
        }

        try
        {
            // Check if the default image exists, if not, throw an exception
            if (!File.Exists(imageFilePath) && imageFilePath == "background.png")
            {
                Console.WriteLine("Error: Default image 'background.png' not found.  Please provide an image or create a 'background.png' file in the same directory as the executable.");
                return;
            }

            // Load the image
            using (Bitmap image = new Bitmap(imageFilePath))
            {
                using (Graphics graphics = Graphics.FromImage(image))
                {
                    using (Font font = new Font("Arial", 24, FontStyle.Bold))
                    {
                        Brush brush = Brushes.White;
                        StringFormat stringFormat = new StringFormat()
                        {
                            Alignment = horizontalAlignment,
                            LineAlignment = StringAlignment.Near
                        };

                        float y = 10; // Top of the image
                        float xPos = image.Width / 2;
                        if (horizontalAlignment == StringAlignment.Near)
                            xPos = 0;
                        else if (horizontalAlignment == StringAlignment.Far)
                            xPos = image.Width;

                        graphics.DrawString(textToInsert, font, brush, new PointF(xPos, y), stringFormat);
                    }
                }

                // Save the modified image as PNG
                image.Save(outputFilePath, ImageFormat.Png);
                Console.WriteLine(" ");
                Console.WriteLine($"Image with text saved to: {outputFilePath}");
                Console.WriteLine(" ");
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: Image file '{imageFilePath}' not found.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

