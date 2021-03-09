/*
 * Created by SharpDevelop.
 * User: serg
 * Date: 29.12.2012
 * Time: 13:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using SlimDX.Direct3D9;
using System.Drawing;

namespace Common
{
    public class TestTexture
    {
        public Texture texture = null;
        public Size size;

        public TestTexture(string filename)
        {
            try
            {
                texture = Texture.FromFile(MyDirect3D.device, filename);
                using (Image b = Bitmap.FromFile(filename)) size = b.Size;
            }
            catch
            {
                texture = new Texture(MyDirect3D.device, 1, 1, 0, Usage.Dynamic, Format.A8R8G8B8, Pool.Default);
                size = new Size(1, 1);
            }
        }
    }
}