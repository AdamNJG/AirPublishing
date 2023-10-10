using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStorage.Test.ImageVerification.helpers
{
    public class FileHelpers
    {
        public static byte[] GetValidJpeg(int height, int width)
        {
            using (Image<Rgba32> image = new Image<Rgba32>(height, width))
            {
                return ImageToByteAray(image, new JpegEncoder());
            }
        }

        public static byte[] GetInvalidImage()
        {
            return new byte[] {
                0xFF // soi incomplete
            };
        }

        public static byte[] GetValidPng(int height, int width)
        {
            using (Image<Rgba32> image = new Image<Rgba32>(height, width))
            {
                return ImageToByteAray(image, new PngEncoder());
            }
        }

        private static byte[] ImageToByteAray(Image image, ImageEncoder encoder)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, encoder);
                return stream.ToArray();
            }
        }
    }
}
