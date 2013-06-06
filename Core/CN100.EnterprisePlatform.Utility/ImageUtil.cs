using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace CN100.EnterprisePlatform.Utility
{
    public class ImageUtil
    {
        public static string ConvertImageToBase64String(string strImageFile)
        {
            try
            {
                byte[] byteImg;
                using (FileStream fs = File.OpenRead(strImageFile))
                {
                    byteImg = new byte[fs.Length];
                    fs.Read(byteImg, 0, byteImg.Length);
                    fs.Close();
                }

                string strRtn = Convert.ToBase64String(byteImg);

                return strRtn;
            }
            catch (Exception ex)
            {
                LogClass.WriteLog("ConvertImageToBase64String Exception: " + ex.Message);
                return string.Empty;
            }
        }

        public static Image ConvertBase64StringToImage(string strBase64String)
        {
            try
            {
                Image img;

                if (string.IsNullOrEmpty(strBase64String))
                    return null;

                byte[] byteImage = Convert.FromBase64String(strBase64String);

                using (MemoryStream ms = new MemoryStream(byteImage))
                {
                    img = Image.FromStream(ms);
                }

                return img;
            }
            catch (Exception ex)
            {
                LogClass.WriteLog("ConvertBase64StringToImage Exception: " + ex.Message);
                return null;
            }

        }

        public static bool FunIsImageFile(string strFileName, bool blnCheckFileExtension)
        {
            byte[] byteJpg = new byte[] { ConvertToByte("0xFF"), ConvertToByte("0xD8") };
            byte[] byteBmp = new byte[] { ConvertToByte("0x42"), ConvertToByte("0x4D") };
            byte[] bytePng = new byte[] { ConvertToByte("0x89"), ConvertToByte("0x50"), ConvertToByte("0x4E"), ConvertToByte("0x47") };
            byte[] byteGif = new byte[] { ConvertToByte("0x47"), ConvertToByte("0x49"), ConvertToByte("0x46"), ConvertToByte("0x39"), ConvertToByte("0x38"), ConvertToByte("0x61") };

            bool blnRtn = false;

            string strExt = Path.GetExtension(strFileName);

            if (blnCheckFileExtension)
            {
                if (".jpg|.jpeg|.bmp|.gif|.png".IndexOf(strExt.ToLower()) < 0)
                {
                    return false;
                }
            }

            using (FileStream fs = File.OpenRead(strFileName))
            {
                byte[] byteRead = new byte[6];
                if (blnCheckFileExtension)
                {
                    switch (strExt.ToLower())
                    {
                        case ".jpg":
                            byteRead = new byte[2];
                            fs.Read(byteRead, 0, 2);
                            fs.Close();
                            if (byteRead[0] == byteJpg[0] && byteRead[1] == byteJpg[1])
                            {
                                blnRtn = true;
                            }
                            else
                            {
                                blnRtn = false;
                            }
                            break;
                        case ".jpeg":
                            byteRead = new byte[2];
                            fs.Read(byteRead, 0, 2);
                            fs.Close();
                            if (byteRead[0] == byteJpg[0] && byteRead[1] == byteJpg[1])
                            {
                                blnRtn = true;
                            }
                            else
                            {
                                blnRtn = false;
                            }
                            break;
                        case ".bmp":
                            byteRead = new byte[2];
                            fs.Read(byteRead, 0, 2);
                            fs.Close();
                            if (byteRead[0] == byteBmp[0] && byteRead[1] == byteBmp[1])
                            {
                                blnRtn = true;
                            }
                            else
                            {
                                blnRtn = false;
                            }
                            break;
                        case ".png":
                            byteRead = new byte[4];
                            fs.Read(byteRead, 0, 4);
                            fs.Close();
                            if (byteRead[0] == bytePng[0] && byteRead[1] == bytePng[1] && byteRead[2] == bytePng[2] && byteRead[3] == bytePng[3])
                            {
                                blnRtn = true;
                            }
                            else
                            {
                                blnRtn = false;
                            }
                            break;
                        case ".gif":
                            byteRead = new byte[6];
                            fs.Read(byteRead, 0, 6);
                            fs.Close();
                            if (byteRead[0] == byteGif[0] && byteRead[1] == byteGif[1] && byteRead[2] == byteGif[2] && byteRead[3] == byteGif[3] && byteRead[4] == byteGif[4] && byteRead[5] == byteGif[5])
                            {
                                blnRtn = true;
                            }
                            else
                            {
                                blnRtn = false;
                            }
                            break;
                    }
                }
                else
                {
                    byteRead = new byte[6];
                    fs.Read(byteRead, 0, byteRead.Length);
                    fs.Close();
                    if ((byteRead[0] == byteJpg[0] && byteRead[1] == byteJpg[1]) || (byteRead[0] == byteBmp[0] && byteRead[1] == byteBmp[1]) || (byteRead[0] == bytePng[0] && byteRead[1] == bytePng[1] && byteRead[2] == bytePng[2] && byteRead[3] == bytePng[3]) || (byteRead[0] == byteGif[0] && byteRead[1] == byteGif[1] && byteRead[2] == byteGif[2] && byteRead[3] == byteGif[3] && byteRead[4] == byteGif[4] && byteRead[5] == byteGif[5]))
                    {
                        blnRtn = true;
                    }
                    else
                    {
                        blnRtn = false;
                    }
                }

            }
            return blnRtn;
        }

        public static bool FunIsImageFile(string strFileName, bool blnCheckFileExtension, byte[] byteRead)
        {
            byte[] byteJpg = new byte[] { ConvertToByte("0xFF"), ConvertToByte("0xD8") };
            byte[] byteBmp = new byte[] { ConvertToByte("0x42"), ConvertToByte("0x4D") };
            byte[] bytePng = new byte[] { ConvertToByte("0x89"), ConvertToByte("0x50"), ConvertToByte("0x4E"), ConvertToByte("0x47") };
            byte[] byteGif = new byte[] { ConvertToByte("0x47"), ConvertToByte("0x49"), ConvertToByte("0x46"), ConvertToByte("0x39"), ConvertToByte("0x38"), ConvertToByte("0x61") };

            bool blnRtn = false;

            string strExt = Path.GetExtension(strFileName);

            if (blnCheckFileExtension)
            {
                if (".jpg|.jpeg|.bmp|.gif|.png".IndexOf(strExt.ToLower()) < 0)
                {
                    return false;
                }
            }

            if (blnCheckFileExtension)
            {
                switch (strExt.ToLower())
                {
                    case ".jpg":
                        if (byteRead[0] == byteJpg[0] && byteRead[1] == byteJpg[1])
                        {
                            blnRtn = true;
                        }
                        else
                        {
                            blnRtn = false;
                        }
                        break;
                    case ".jpeg":
                        if (byteRead[0] == byteJpg[0] && byteRead[1] == byteJpg[1])
                        {
                            blnRtn = true;
                        }
                        else
                        {
                            blnRtn = false;
                        }
                        break;
                    case ".bmp":
                        if (byteRead[0] == byteBmp[0] && byteRead[1] == byteBmp[1])
                        {
                            blnRtn = true;
                        }
                        else
                        {
                            blnRtn = false;
                        }
                        break;
                    case ".png":
                        if (byteRead[0] == bytePng[0] && byteRead[1] == bytePng[1] && byteRead[2] == bytePng[2] && byteRead[3] == bytePng[3])
                        {
                            blnRtn = true;
                        }
                        else
                        {
                            blnRtn = false;
                        }
                        break;
                    case ".gif":
                        if (byteRead[0] == byteGif[0] && byteRead[1] == byteGif[1] && byteRead[2] == byteGif[2] && byteRead[3] == byteGif[3] && byteRead[4] == byteGif[4] && byteRead[5] == byteGif[5])
                        {
                            blnRtn = true;
                        }
                        else
                        {
                            blnRtn = false;
                        }
                        break;
                }
            }
            else
            {
                if ((byteRead[0] == byteJpg[0] && byteRead[1] == byteJpg[1]) || (byteRead[0] == byteBmp[0] && byteRead[1] == byteBmp[1]) || (byteRead[0] == bytePng[0] && byteRead[1] == bytePng[1] && byteRead[2] == bytePng[2] && byteRead[3] == bytePng[3]) || (byteRead[0] == byteGif[0] && byteRead[1] == byteGif[1] && byteRead[2] == byteGif[2] && byteRead[3] == byteGif[3] && byteRead[4] == byteGif[4] && byteRead[5] == byteGif[5]))
                {
                    blnRtn = true;
                }
                else
                {
                    blnRtn = false;
                }
            }

            return blnRtn;
        }

        private static byte ConvertToByte(string str16)
        {
            return Convert.ToByte(Convert.ToInt32(str16, 16));
        }

        public static void AddWatermarkImage(Graphics graphics, int imageWidth, int imageHeight, string watermarImagePath, WatermarkPosition watermarkPosition, float alpha)
        {
            if ((graphics != null) && File.Exists(watermarImagePath))
            {
                Image image = new Bitmap(watermarImagePath);
                ImageAttributes imageAttr = new ImageAttributes();
                ColorMap map = new ColorMap {
                    OldColor = Color.FromArgb(0xff, 0, 0xff, 0),
                    NewColor = Color.FromArgb(0, 0, 0, 0)
                };
                ColorMap[] mapArray = new ColorMap[] { map };
                imageAttr.SetRemapTable(mapArray, ColorAdjustType.Bitmap);
                float[][] numArray2 = new float[5][];
                float[] numArray3 = new float[5];
                numArray3[0] = 1f;
                numArray2[0] = numArray3;
                numArray3 = new float[5];
                numArray3[1] = 1f;
                numArray2[1] = numArray3;
                numArray3 = new float[5];
                numArray3[2] = 1f;
                numArray2[2] = numArray3;
                numArray3 = new float[5];
                numArray3[3] = alpha;
                numArray2[3] = numArray3;
                numArray3 = new float[5];
                numArray3[4] = 1f;
                numArray2[4] = numArray3;
                float[][] newColorMatrix = numArray2;
                ColorMatrix matrix = new ColorMatrix(newColorMatrix);
                imageAttr.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                int x = 0;
                int y = 0;
                switch (watermarkPosition)
                {
                    case WatermarkPosition.LeftTop:
                        x = 10;
                        y = 10;
                        break;

                    case WatermarkPosition.CenterTop:
                        x = (imageWidth - image.Width) / 2;
                        y = 10;
                        break;

                    case WatermarkPosition.RightTop:
                        x = (imageWidth - image.Width) - 10;
                        y = 10;
                        break;

                    case WatermarkPosition.LeftMiddle:
                        x = 10;
                        y = (imageHeight - image.Height) / 2;
                        break;

                    case WatermarkPosition.Center:
                        x = (imageWidth - image.Width) / 2;
                        y = (imageHeight - image.Height) / 2;
                        break;

                    case WatermarkPosition.RightMiddle:
                        x = (imageWidth - image.Width) - 10;
                        y = (imageHeight - image.Height) / 2;
                        break;

                    case WatermarkPosition.LeftBottom:
                        x = 10;
                        y = (imageHeight - image.Height) - 10;
                        break;

                    case WatermarkPosition.CenterBottom:
                        x = (imageWidth - image.Width) / 2;
                        y = (imageHeight - image.Height) - 10;
                        break;

                    case WatermarkPosition.RightBottom:
                        x = (imageWidth - image.Width) - 10;
                        y = (imageHeight - image.Height) - 10;
                        break;
                }
                graphics.DrawImage(image, new Rectangle(x, y, image.Width, image.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, imageAttr);
                image.Dispose();
                imageAttr.Dispose();
            }
        }

        public static void AddWatermarkImage(Image sourceImage, string watermarImagePath, WatermarkPosition watermarkPosition, float alpha, int watermarkQuality, out Stream newImageStream)
        {
            if ((sourceImage == null) || string.IsNullOrEmpty(watermarImagePath.TrimEnd(new char[] { ' ' })))
            {
                newImageStream = null;
            }
            else
            {
                int width = sourceImage.Width;
                int height = sourceImage.Height;
                Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                image.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                Graphics graphics = Graphics.FromImage(image);
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
                AddWatermarkImage(graphics, width, height, watermarImagePath, watermarkPosition, alpha);
                newImageStream = new MemoryStream();
                ImageFormat jpeg = ImageFormat.Jpeg;
                if (watermarkQuality > 0)
                {
                    ImageCodecInfo imageCodecInfo = GetImageCodecInfo(jpeg);
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams = new EncoderParameters();
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long) watermarkQuality);
                    image.Save(newImageStream, imageCodecInfo, encoderParams);
                }
                else
                {
                    image.Save(newImageStream, jpeg);
                }
                image.Dispose();
                sourceImage.Dispose();
                graphics.Dispose();
            }
        }

        public static void AddWatermarkImage(Stream sourceStream, string watermarImagePath, WatermarkPosition watermarkPosition, float alpha, int watermarkQuality, out Stream newImageStream)
        {
            Image sourceImage = null;
            try
            {
                sourceImage = Image.FromStream(sourceStream, true);
            }
            catch
            {
            }
            if (sourceImage == null)
            {
                newImageStream = null;
            }
            else
            {
                AddWatermarkImage(sourceImage, watermarImagePath, watermarkPosition, alpha, watermarkQuality, out newImageStream);
            }
        }

        public static void AddWatermarkText(Graphics graphics, int imageWidth, int imageHeight, string watermarText, WatermarkPosition watermarkPosition, float alpha)
        {
            if ((graphics != null) && !string.IsNullOrEmpty(watermarText.TrimEnd(new char[] { ' ' })))
            {
                graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
                int[] numArray = new int[] { 0x10, 14, 12, 10, 8, 6, 4, 3, 2, 1 };
                Font font = null;
                SizeF ef = new SizeF();
                for (int i = 0; i < numArray.Length; i++)
                {
                    font = new Font("arial", (float) numArray[i], FontStyle.Bold);
                    ef = graphics.MeasureString(watermarText, font);
                    if (((ushort) ef.Width) < ((ushort) (imageWidth * 0.8)))
                    {
                        break;
                    }
                }
                float height = ef.Height;
                float width = ef.Width;
                float x = 0f;
                float y = 0f;
                switch (watermarkPosition)
                {
                    case WatermarkPosition.LeftTop:
                        x = (imageWidth * 0.01f) + (width / 2f);
                        y = imageHeight * 0.01f;
                        break;

                    case WatermarkPosition.CenterTop:
                        x = ((float) imageWidth) / 2f;
                        y = imageHeight * 0.01f;
                        break;

                    case WatermarkPosition.RightTop:
                        x = (imageWidth * 0.99f) - (width / 2f);
                        y = imageHeight * 0.01f;
                        break;

                    case WatermarkPosition.LeftMiddle:
                        x = (imageWidth * 0.01f) + (width / 2f);
                        y = (((float) imageHeight) / 2f) - (height / 2f);
                        break;

                    case WatermarkPosition.Center:
                        x = ((float) imageWidth) / 2f;
                        y = (((float) imageHeight) / 2f) - (height / 2f);
                        break;

                    case WatermarkPosition.RightMiddle:
                        x = (imageWidth * 0.99f) - (width / 2f);
                        y = (((float) imageHeight) / 2f) - (height / 2f);
                        break;

                    case WatermarkPosition.LeftBottom:
                        x = (imageWidth * 0.01f) + (width / 2f);
                        y = (imageHeight * 0.99f) - height;
                        break;

                    case WatermarkPosition.CenterBottom:
                        x = ((float) imageWidth) / 2f;
                        y = (imageHeight * 0.99f) - height;
                        break;

                    case WatermarkPosition.RightBottom:
                        x = (imageWidth * 0.99f) - (width / 2f);
                        y = (imageHeight * 0.99f) - height;
                        break;
                }
                StringFormat format = new StringFormat {
                    Alignment = StringAlignment.Center
                };
                SolidBrush brush = new SolidBrush(Color.FromArgb(Convert.ToInt32((float) (256f * alpha)), 0, 0, 0));
                graphics.DrawString(watermarText, font, brush, x + 1f, y + 1f, format);
                SolidBrush brush2 = new SolidBrush(Color.FromArgb(0x99, 0xff, 0xff, 0xff));
                graphics.DrawString(watermarText, font, brush2, x, y, format);
                brush.Dispose();
                brush2.Dispose();
            }
        }

        public static void AddWatermarkText(Image sourceImage, string watermarText, WatermarkPosition watermarkPosition, float alpha, int watermarkQuality, out Stream newImageStream)
        {
            if ((sourceImage == null) || string.IsNullOrEmpty(watermarText.Trim(new char[] { ' ' })))
            {
                newImageStream = null;
            }
            else
            {
                int width = sourceImage.Width;
                int height = sourceImage.Height;
                Bitmap image = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                image.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
                Graphics graphics = Graphics.FromImage(image);
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(sourceImage, new Rectangle(0, 0, width, height), 0, 0, width, height, GraphicsUnit.Pixel);
                AddWatermarkText(graphics, width, height, watermarText, watermarkPosition, alpha);
                newImageStream = new MemoryStream();
                ImageFormat jpeg = ImageFormat.Jpeg;
                if (watermarkQuality > 0)
                {
                    ImageCodecInfo imageCodecInfo = GetImageCodecInfo(jpeg);
                    EncoderParameters encoderParams = new EncoderParameters(1);
                    encoderParams = new EncoderParameters();
                    encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long) watermarkQuality);
                    image.Save(newImageStream, imageCodecInfo, encoderParams);
                }
                else
                {
                    image.Save(newImageStream, jpeg);
                }
                image.Dispose();
                sourceImage.Dispose();
                graphics.Dispose();
            }
        }

        public static void AddWatermarkText(Stream sourceStream, string watermarText, WatermarkPosition watermarkPosition, float alpha, int watermarkQuality, out Stream newImageStream)
        {
            Image sourceImage = null;
            try
            {
                sourceImage = Image.FromStream(sourceStream, true);
            }
            catch
            {
            }
            if (sourceImage == null)
            {
                newImageStream = null;
            }
            else
            {
                AddWatermarkText(sourceImage, watermarText, watermarkPosition, alpha, watermarkQuality, out newImageStream);
            }
        }

        private static ImageCodecInfo GetImageCodecInfo(ImageFormat imageFormat)
        {
            ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo info in imageEncoders)
            {
                if (info.FormatID == imageFormat.Guid)
                {
                    return info;
                }
            }
            return null;
        }

        public enum WatermarkPosition
        {
            LeftTop,
            CenterTop,
            RightTop,
            LeftMiddle,
            Center,
            RightMiddle,
            LeftBottom,
            CenterBottom,
            RightBottom
        }

        private class LogClass
        {
            static string m_baseDir = null;

            private const string KeyDebugModel = "DebugModel";
            private const bool DefaultValueDebugModel = false;

            static LogClass()
            {
                m_baseDir = @"d:\temp\";
            }

            public static bool GetDebugMode()
            {
                try
                {
                    return Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings[KeyDebugModel]);
                }
                catch (Exception)
                {
                    return DefaultValueDebugModel;
                }
            }

            public static string GetFilenameYYYMMDD(string suffix, string extension)
            {
                return System.DateTime.Now.ToString("yyyy_MM_dd")
                    + suffix
                    + extension;
            }

            public static void WriteLog(String message)
            {
                if (GetDebugMode())
                {
                    //just in case: we protect code with try.
                    try
                    {
                        string filename = m_baseDir
                            + GetFilenameYYYMMDD("_server", ".log");
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, true);

                        sw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff" + "\t") +
                            message);
                        sw.Close();

                    }
                    catch (Exception) { }
                }
            }

            public static void WriteLog(String type, String message)
            {
                if (GetDebugMode())
                {
                    //just in case: we protect code with try.
                    try
                    {
                        string filename = m_baseDir
                            + GetFilenameYYYMMDD("_server", ".log");
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, true);

                        sw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") + "\t" +
                            type + "\t" + message);
                        sw.Close();

                    }
                    catch (Exception) { }
                }
            }

            public static void WriteLog(Exception ex)
            {
                if (GetDebugMode())
                {
                    //just in case: we protect code with try.
                    try
                    {
                        string filename = m_baseDir
                            + GetFilenameYYYMMDD("_server", ".log");
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(filename, true);
                        sw.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff") + "\t" +
                            "Exception" + "\t" + ex.Source.ToString() + "  " + ex.Message.Replace("\n", " ") + "  " +
                            ex.StackTrace.Replace("\n", " "));

                        sw.Close();
                    }
                    catch (Exception) { }
                }
            }

        }
    }
}
