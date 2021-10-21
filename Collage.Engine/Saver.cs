namespace Collage.Engine
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    internal class Saver
    {
        private readonly FileNameCreator fileNameCreator;

        public Saver(DirectoryInfo outputDirectory)
        {
            if (outputDirectory == null)
            {
                throw new ArgumentNullException("outputDirectory");
            }
            
            this.fileNameCreator = new FileNameCreator(outputDirectory);
        }

        public FileInfo Save(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }

            var fileName = this.fileNameCreator.CreateFileName();

            bitmap.Save(fileName, ImageFormat.Jpeg);
            bitmap.Dispose();

            return new FileInfo(fileName);
        }
    }
}
