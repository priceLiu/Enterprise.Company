// -----------------------------------------------------------------------
// <copyright file="ImageResizer.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace CN100.EnterprisePlatform.ImageHelper
{

    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.IO;

    /// <summary>
    /// 压缩图片，生成缩略图
    /// </summary>
    public class ImageResizer
    {
        private static ImageCodecInfo jpgEncoder;

        /// <summary>
        /// Resizes the image.
        /// </summary>
        /// <param name="inFile">The in file.</param>
        /// <param name="outFile">The out file.</param>
        /// <param name="maxDimension">The max dimension.</param>
        /// <param name="level">The level.</param>
        public static void ResizeImage(string inFile, string outFile, double maxDimension, long level)
        {
            using (Stream stream=new FileStream(inFile,FileMode.Open))
            {
                using (Image inImage=Image.FromStream(stream))
                {
                    double width;
                    double height;
                    if (inImage.Height<inImage.Width)
                    {
                        width = maxDimension;
                        height = (maxDimension / (double)inImage.Width) * inImage.Height;
                    }
                    else
                    {
                        height = maxDimension;
                        width = (maxDimension / (double)inImage.Height) * inImage.Width;
                    }
                    using (Bitmap bitmap=new Bitmap((int)width,(int)height))
                    {
                        using (Graphics graphics=Graphics.FromImage(bitmap))
                        {
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.DrawImage(inImage, 0, 0, bitmap.Width, bitmap.Height);
                            if (inImage.RawFormat.Guid== ImageFormat.Jpeg.Guid)
                            {
                                if (jpgEncoder==null)
                                {
                                    ImageCodecInfo[] ici = ImageCodecInfo.GetImageDecoders();
                                    foreach (ImageCodecInfo info in ici)
                                    {
                                        if (info.FormatID== ImageFormat.Jpeg.Guid)
                                        {
                                            jpgEncoder = info;
                                            break;
                                        }
                                    }
                                }

                                if (jpgEncoder!=null)
                                {
                                    EncoderParameters ep = new EncoderParameters(1);
                                    ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, level);
                                    bitmap.Save(outFile, jpgEncoder, ep);
                                }
                                else
                                {
                                    bitmap.Save(outFile, inImage.RawFormat);
                                }
                            }
                            else
                            {
                                graphics.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
                                bitmap.Save(outFile, inImage.RawFormat);
                            }
                        }
                    }
                }
            }
        }
    }
}
