using SkiaSharp;
using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var bmp = Load(Path.Combine(ThisDir(), "testimage.png"));
            Save(bmp, Path.Combine(ThisDir(), "testimage_resave.png"));
        }

        static SKBitmap Load(string filename)
        {
            using (var file = File.OpenRead(filename))
            {
                using (var skStream = new SKManagedStream(file))
                {
                    return SKBitmap.Decode(skStream);
                }
            }
        }

        static void Save(SKBitmap bmp, string filename)
        {
            // create an image and then get the PNG (or any other) encoded data
            using (var image = SKImage.FromBitmap(bmp))
            {
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                {
                    // save the data to a stream
                    using (var stream = File.OpenWrite(filename))
                    {
                        data.SaveTo(stream);
                    }
                }
            }
        }

        private static string ThisDir([System.Runtime.CompilerServices.CallerFilePath] string path = null)
        {
            return System.IO.Path.GetDirectoryName(path);
        }
    }
}