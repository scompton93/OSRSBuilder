using IKVM.Runtime;
using java.awt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixins
{
    public abstract class GameEngineMod : GameEngine
    {


        [MixinMethod(TargetClass = "GameEngine", TargetMethod = "graphicsTick", Replace = true)]
        internal virtual void graphicsTick()
        {
            Console.WriteLine(GameEngine.hasFocus);
            Container container = this.container();
            long num = global::class153.clockNow();
            long num2 = global::GameEngine.graphicsTickTimes[global::class20.field69];
            global::GameEngine.graphicsTickTimes[global::class20.field69] = num;
            global::class20.field69 = (global::class20.field69 + 1 & 31);
            if (0L != num2 && num > num2)
            {
                int num3 = (int)(num - num2);
                int num4 = (num3 >> 1) + 32000;
                int num5 = num3;
                global::GameEngine.fps = ((num5 != -1) ? (num4 / num5) : (-num4));
            }
            if (++global::GameEngine.field129 - 1 > 50)
            {
                global::GameEngine.field129 -= 50;
                this.fullRedraw = true;

                this.canvas.setSize(global::GameEngine.canvasWidth, global::class127.canvasHeight);
                this.canvas.setVisible(true);
                if (container == this.frame)
                {
                    Insets insets = this.frame.getInsets();
                    this.canvas.setLocation(insets.left + this.canvasX, this.canvasY + insets.top);
                }
                else
                {
                    this.canvas.setLocation(this.canvasX, this.canvasY);
                }
            }
            if (this.isCanvasInvalid)
            {
                this.replaceCanvas();
            }
            this.method149();
            this.draw(this.fullRedraw);
            if (this.fullRedraw)
            {
                this.clearBackground();
            }
            this.fullRedraw = false;
        }
    }
}
