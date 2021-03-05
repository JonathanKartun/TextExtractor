using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TextExtractor.Helpers
{
    public class FileHandler
    {
        public static byte[] GetResourceFileBytes(string filepath)
        { //Example: "OCRCheck.Assets.TestFile.txt"; -> Must be 'EmbeddedResource' for this to work.
            var bytes = default(byte[]);
            var assembly = typeof(MainPage).Assembly;

            using (var fileStream = assembly.GetManifestResourceStream(filepath))
            {
                using (var memstream = new MemoryStream())
                {
                    fileStream.CopyTo(memstream);
                    bytes = memstream.ToArray();
                }
            }

            return bytes;
        }

        public static byte[] GetFilepathFileBytes(string filepath)
        { //Example: MUST NOT BE A RESOURCE FILE
            var bytes = default(byte[]);
            var assembly = typeof(MainPage).Assembly;

            using (var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read))
            {
                using (var memstream = new MemoryStream())
                {
                    fileStream.CopyTo(memstream);
                    bytes = memstream.ToArray();
                }
            }

            return bytes;
        }

        public static MemoryStream GetFilepathMemoryStream(string filepath)
        { //Example: MUST NOT BE A RESOURCE FILE
            var assembly = typeof(MainPage).Assembly;

            var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            var memstream = new MemoryStream();
            fileStream.CopyTo(memstream);

            return memstream;
        }

        public static MemoryStream GetResourceAsMemoryStream(string filepath)
        { //Example: "OCRCheck.Assets.TestFile.txt"; -> Must be 'EmbeddedResource' for this to work.
            var assembly = typeof(MainPage).Assembly;

            var fileStream = assembly.GetManifestResourceStream(filepath);
            var memstream = new MemoryStream();
            fileStream.CopyTo(memstream);
            fileStream.Close();
            return memstream;
        }

        public static bool CheckFileIsPCLResource(string check)
        {
            var fileList = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            return fileList.Contains(check);
        }

    }
}
