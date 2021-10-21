namespace Collage.Engine
{
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    internal static class ImageExtensions
    {
        public static void RotateFlipRandom(this Image image, IRandomGenerator random)
        {
            image.RotateFlip((RotateFlipType)random.Next(0, 7));
        }
               
        public static Bitmap Scale(this Image image, Percentage percentage)
        {
            if (percentage.Value == 100)
            {
                return (Bitmap)image;
            }

            var nPercent = percentage.ValueAsFloat;

            var sourceWidth = image.Width;
            var sourceHeight = image.Height;
            const int sourceX = 0;
            const int sourceY = 0;

            const int destX = 0;
            const int destY = 0;
            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bitmap = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
         
                graphics.DrawImage(image,
                    new Rectangle(destX, destY, destWidth, destHeight),
                    new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                    GraphicsUnit.Pixel);
            }
            return bitmap;
        }

        public static Graphics CreateGraphics(this Bitmap bitmap)
        {
            var graphics = Graphics.FromImage(bitmap);

            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            return graphics;
        }
    }
}
