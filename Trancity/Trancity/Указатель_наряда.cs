namespace Trancity
{
    using Common;
//    using Microsoft.DirectX;
//    using Microsoft.DirectX.Direct3D;
    using SlimDX;
    using SlimDX.Direct3D9;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    public class УказательНаряда : MeshObject, MeshObject.IFromFile, IMatrixObject
    {
        public Matrix matrix;

        public Matrix GetMatrix(int index)
        {
            return matrix;
        }

        public void ОбновитьКартинку(Order наряд)
        {
            const string texture = "Order.PNG";
            var font = new System.Drawing.Font("Verdana", 14f);
            Brush brush = new SolidBrush(Color.Black);
            for (var i = 0; i < _meshTextures.Length; i++)
            {
            	if ((string.IsNullOrEmpty(_meshTextureFilenames[i])) || (_meshTextureFilenames[i].ToLower() != texture.ToLower()))
            		continue;
                Image image = new Bitmap(texture);
                var graphics = Graphics.FromImage(image);
                var s = наряд.маршрут.number;
                var str3 = наряд.номер;
                graphics.DrawString(s, font, brush, 10f - (0.5f * graphics.MeasureString(s, font).Width), 0f);
                graphics.DrawString(str3, font, brush, 10f - (0.5f * graphics.MeasureString(str3, font).Width), 21f);
                Stream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Bmp);
                stream.Seek(0L, SeekOrigin.Begin);
//                _meshTextures[i] = Texture.FromStream(MyDirect3D.device, stream);
				base.LoadTextureFromStream(i, stream);
            }
        }

        public string Filename
        {
            get
            {
                return "order.x";
            }
        }

        public int MatricesCount
        {
            get
            {
                return 1;
            }
        }
    }
}

