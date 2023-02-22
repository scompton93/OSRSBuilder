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

        [MixinSettings(TargetClass = "RasterProvider", TargetMethod = "draw0", Replace = true)]
        public void draw0(Graphics A_1, int A_2, int A_3, int A_4, int A_5)
        {
            System.Console.WriteLine("Draw0");
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

        [MixinSettings(TargetClass = "RasterProvider", TargetMethod = "drawFull0", Replace = true)]
        public void drawFull0(Graphics A_1, int A_2, int A_3)
        {
            System.Console.WriteLine("DrawFull0");
            try
            {
                A_1.drawImage(this.image, A_2, A_3, this.component);


            }
            catch (System.Exception x)
            {
                if (ByteCodeHelper.MapException<java.lang.Exception>(x, ByteCodeHelper.MapFlags.Unused) == null)
                {
                    System.Console.WriteLine("throw");
                    throw;
                }
                goto IL_27;
            }
            return;
        IL_27:
            this.component.repaint();
        }
    }
}
