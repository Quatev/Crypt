using System;
using System.Drawing;

namespace Crypt.Helpers
{
    public static class ImageExtensions
    {
        public static Image InvertColors(this Image image)
        {
            if (image == null)
                return null;

            Bitmap bitmap = new Bitmap(image);

            for (int y = 0; y < bitmap.Height; y++)
            {
                for (int x = 0; x < bitmap.Width; x++)
                {
                    Color original = bitmap.GetPixel(x, y);
                    Color inverted = Color.FromArgb(
                        original.A,
                        255 - original.R,
                        255 - original.G,
                        255 - original.B
                    );
                    bitmap.SetPixel(x, y, inverted);
                }
            }

            return bitmap;
        }
    }

}
