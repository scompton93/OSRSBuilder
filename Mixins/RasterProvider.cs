using IKVM.Runtime;
using java.awt;
using java.awt.image;
using java.io;
using javax.imageio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixins
{
    public abstract class RasterProviderMod : RasterProvider
    {
        protected RasterProviderMod(int value, int a, Component b) : base(value, a, b) { }

        [MixinMethod(TargetClass = "RasterProvider", TargetMethod = "draw0", Replace = true)]
        public void draw0(Graphics A_1, int A_2, int A_3, int A_4, int A_5)
        {

            try
            {
                Shape clip = A_1.getClip();
                A_1.clipRect(A_2, A_3, A_4, A_5);
                A_1.drawImage(this.image, 0, 0, this.component);
                A_1.setClip(clip);
            }
            catch (System.Exception x)
            {
                if (ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.Unused) == null)
                {
                    System.Console.WriteLine("throw");
                    throw;
                }
                goto IL_41;
            }
            return;
        IL_41:
            this.component.repaint();
        }

        [MixinMethod(TargetClass = "RasterProvider", TargetMethod = "drawFull0", Replace = true)]
        public void drawFull0(Graphics A_1, int A_2, int A_3)
        {
            try
            {
                A_1.drawImage(this.image, A_2, A_3, this.component);
                byte[] sfmlpixels = new byte[this.pixels.Length * 4];


                for (int i = 0; i < this.pixels.Length; i++)
                {
                    int pixel = this.pixels[i];
                    byte blue = (byte)(pixel & 0xFF);
                    byte green = (byte)((pixel >> 8) & 0xFF);
                    byte red = (byte)((pixel >> 16) & 0xFF);
                    byte alpha = 0xFF; // set alpha to 255

                    sfmlpixels[i * 4] = red;
                    sfmlpixels[i * 4 + 1] = green;
                    sfmlpixels[i * 4 + 2] = blue;
                    sfmlpixels[i * 4 + 3] = alpha;
                }
                this.SFMLByteArray = sfmlpixels;

            }
            catch (System.Exception x)
            {
                this.component.repaint();
            }
        }
    }
}
