using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;


namespace ImageHandlers
{
    public class ImageFileHander
    {
        #region Fields
        private const string IMAGE_FILE_FILTER = "JPEG Image|*.jpg;*.jpeg|PNG Image|*.png|Bitmap Image|*.bmp|" +
                                                 "GIF Image|*.gif|TIFF Image|*.tiff|WMP Image|*.wmp|All Files|*.*";
        #endregion


        #region  Properties & Indexers
        public static double Dpi { get; set; } = 96;
        public static PixelFormat PixelFormat { get; set; } = PixelFormats.Default;
        #endregion


        #region Methods
        public static Image LoadImage()
        {
            var filePath = BrowseFile();
            return string.IsNullOrEmpty(filePath)
                       ? null : new Image { Source = new BitmapImage(new Uri(filePath, UriKind.Absolute)) };
        }

        public static void LoadImage(InkCanvas inkCanvas)
        {
            var image = LoadImage();
            if (image != null)
            {
                inkCanvas.Children.Add(image);
            }
        }

        public static void SaveToImage(InkCanvas inkCanvas)
        {
            SaveToImage(inkCanvas, (int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight);
        }

        public static void SaveToImage(Visual visual, int width, int height)
        {
            Debug.WriteLine($"width: {width}, height: {height}");
            var filePath = ChoosePathToSave();
            if (filePath == null) return;

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                var renderer = new RenderTargetBitmap(width, height, Dpi, Dpi, PixelFormat);
                renderer.Render(visual);
                var encoder = ChooseBitmapEncoder(Path.GetExtension(filePath));
                encoder.Frames.Add(BitmapFrame.Create(renderer));
                encoder.Save(fs);
            }
            SelectFileInExplorer(filePath);
        }
        #endregion


        #region Implementation
        private static string BrowseFile()
        {
            return ShowDialogToGetPath(new OpenFileDialog());
        }

        private static BitmapEncoder ChooseBitmapEncoder(string fileExtension)
        {
            switch (fileExtension.Trim(' ', '.').ToLower())
            {
                case "jpg":
                case "jpeg":
                    return new JpegBitmapEncoder();
                case "png":
                    return new PngBitmapEncoder();
                case "bmp":
                    return new BmpBitmapEncoder();
                case "gif":
                    return new GifBitmapEncoder();
                case "tiff":
                    return new TiffBitmapEncoder();
                case "wmp":
                    return new WmpBitmapEncoder();
                default:
                    throw new NotSupportedException("File extension not supported");
            }
        }

        private static string ChoosePathToSave()
        {
            return ShowDialogToGetPath(new SaveFileDialog());
        }

        private static void SelectFileInExplorer(string filePath)
        {
            var argument = @"/select, " + filePath;
            Process.Start("explorer.exe", argument);
        }

        private static string ShowDialogToGetPath(FileDialog dialog)
        {
            dialog.Filter = IMAGE_FILE_FILTER;
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }
        #endregion
    }
}