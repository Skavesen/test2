using System.Collections.Generic;

namespace Collage.Engine
{
    using System.IO;

    public class Settings
    {
        public Settings( 
            DimensionSettings collageDimensionSettings, 
            List<FileInfo> inputFiles, 
            DirectoryInfo outputDirectory)
        {
            this.Dimensions = collageDimensionSettings;
            this.InputFiles = inputFiles;
            this.OutputDirectory = outputDirectory;
        }


        public DimensionSettings Dimensions { get; private set; }

        public List<FileInfo> InputFiles { get; private set; }

        public DirectoryInfo OutputDirectory { get; private set; }
    }
}
