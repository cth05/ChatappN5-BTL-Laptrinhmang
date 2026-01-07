using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_app
{
    internal class ImageHelper
    {
        public static byte[] CompressImage(string path, int maxWidth = 400)
        {
            using (var original = Image.FromFile(path))
            {
                int newWidth = maxWidth;
                int newHeight = (int)(original.Height * (newWidth / (double)original.Width));

                using (var bitmap = new Bitmap(original, new Size(newWidth, newHeight)))
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
        }
    }
}
