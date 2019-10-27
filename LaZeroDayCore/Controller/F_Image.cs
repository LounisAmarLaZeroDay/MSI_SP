using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using LaZeroDayCore.Config;

namespace LaZeroDayCore.Controller
{
    public class F_Image
    {
        #region PictureCompany
        public static byte[] PictureCompanyGet()
        {
            String mPath = Path.Combine(C_Variables.Path_.dir_setting, "company.png");
            if (File.Exists(mPath))
            {
                return F_File.FileRead_Bytes(mPath);
            }
            else
            {
                var mImage = F_Image.Image2Bytes(Properties.Resources.DefaultCompany);
                F_File.FileWrite(mImage, mPath);
                return mImage;
            }
        }
        //
        public static string PictureCompanyGetbase64()
        {
            return Convert.ToBase64String(PictureCompanyGet());
        }
        public static void PictureCompanySet(byte[] p_bitmapImage)
        {
            String mPath = Path.Combine(C_Variables.Path_.dir_setting, "company.png");
            F_File.FileWrite(p_bitmapImage, mPath);
        }
        #endregion
        #region Convert
        public static BitmapImage Bytes2BitmapImage(byte[] imageData, int new_width, int new_height)
        {
            if (imageData == null || imageData.Length == 0) return new BitmapImage();
            return Stream2BitmapImage(new MemoryStream(imageData), new_width, new_height);
        }
        public static BitmapImage Stream2BitmapImage(MemoryStream p_stream, int new_width, int new_height)
        {
            var image = new BitmapImage();
            p_stream.Position = 0;
            image.BeginInit();
            image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = null;
            image.StreamSource = p_stream;
            image.DecodePixelWidth = new_width;
            image.DecodePixelHeight = new_height;
            image.EndInit();
            image.Freeze();
            return image;
        }
        public static byte[] BitmapImage2Bytes(BitmapImage bitmapImage)
        {
            bitmapImage = Resize_BitmapImage(bitmapImage,100,100);
            byte[] data;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
        public static byte[] Image2Bytes(Image img)
        {
            ImageConverter converter = new ImageConverter();
            return (byte[])converter.ConvertTo(img, typeof(byte[]));
        }
        #endregion
        #region resize
        public static BitmapImage Resize_BitmapImage(BitmapImage bitmapImage, int new_width, int new_height)
        {
            byte[] imageData;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                imageData = ms.ToArray();
            }
            if (imageData == null || imageData.Length == 0) return new BitmapImage();
            return Stream2BitmapImage(new MemoryStream(imageData), new_width, new_height);
        }
        public static Bitmap Resize_Bitmap(Bitmap image, int new_width, int new_height)
        {
            double scale = Math.Min(new_width / image.Width, new_height / image.Height);
            var bmp = new Bitmap((int)new_width, (int)new_height);
            var graph = Graphics.FromImage(bmp);
            var scaleWidth = (int)(image.Width * scale);
            var scaleHeight = (int)(image.Height * scale);
            var brush = new SolidBrush(System.Drawing.Color.Black);
            graph.FillRectangle(brush, new RectangleF(0, 0, new_width, new_height));
            graph.DrawImage(image, ((int)new_width - scaleWidth) / 2, ((int)new_height - scaleHeight) / 2, scaleWidth, scaleHeight);
            return image;
        }
        #endregion
    }
}
