using BitMiracle.Docotic.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace compressallthethings
{
    class Fileutils
    {
        internal static void CompressPDF(string fileName, string outputName, int compression)
        {
            if (compression < 0 || compression > 100)
            {
                throw new
                    ArgumentOutOfRangeException("compression must be between 0 and 100.");
            }
            Console.WriteLine("beginning compression on " + fileName);
            using (PdfDocument doc = new PdfDocument(fileName))
            {
                foreach (PdfImage image in doc.Images)
                {
                    image.RecompressWithJpeg(compression);
                }
                // Save the file
                doc.Save(outputName);

                // Get the file image sizes.
                var prevSize = new FileInfo(fileName).Length;
                var nextSize = new FileInfo(outputName).Length;
                var size = (prevSize - nextSize / 1024f) / 1024f;
                Console.WriteLine("PDF compressed with {0} mb saved. New file is {1}", size, outputName);
            }
        }
        public void CompressImage(string fileName, string outputName, int compression)
        {
            {
                if (compression < 0 || compression > 100)
                {
                    throw new
                        ArgumentOutOfRangeException("compression must be between 0 and 100.");
                }
                //using (var image = Image.FromFile(fileName))
                //{
                //    // Encoder parameter for image quality
                //    var qualityParam =
                //        new EncoderParameter(Encoder.Quality, compression);
                //    // Jpeg image codec
                //    var jpegCodec = ImageCodecInfo.GetImageEncoders()
                //        .Where(imageCodecInfo => imageCodecInfo.MimeType == "image/jpeg")
                //        .FirstOrDefault();
                //    var encoderParams = new EncoderParameters(1);
                //    encoderParams.Param[0] = qualityParam;

                //    //Save the compressed image.
                //    image.Save(outputName, jpegCodec, encoderParams);

                //    //Getting the file image sizes.
                //    var prevImageSize = new FileInfo(fileName).Length;
                //    var nextImageSize = new FileInfo(outputName).Length;
                //    Console.WriteLine("Image compressed. Size saved :{0} bytes", prevImageSize - nextImageSize);
                //}
            }
        }        
    }
}
