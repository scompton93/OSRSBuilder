using IKVM.Runtime;
using java.awt;
using java.io;
using sun.misc;
using System.Data.SqlTypes;
using System.Threading;

namespace Mixins
{
    public abstract class GameEngineMod : GameEngine
    {
        [MixinSettings(TargetClass = "GameEngine", TargetMethod = "paint")]
        public void paint(Graphics g)
        {
            System.Console.WriteLine("Paint");
            if (this == GameEngine.gameEngine)
            {
                if (!GameEngine.isKilled)
                {
                    this.fullRedraw = true;
                    if (class153.clockNow() - this.field130 > (long)((ulong)1000))
                    {
                        Rectangle clipBounds = g.getClipBounds();
                        if (
                            clipBounds == null
                            || (
                                clipBounds.width >= GameEngine.canvasWidth
                                && clipBounds.height >= class127.canvasHeight
                            )
                        )
                        {
                            this.isCanvasInvalid = true;
                        }
                    }
                    return;
                }
            }
        }

        [MixinSettings(TargetClass = "GameEngine", TargetMethod = "graphicsTick")]
        internal virtual void graphicsTick()
        {
            System.Console.WriteLine("tick");



            System.Console.WriteLine($"isCanvasInvalid = {isCanvasInvalid}");


            fullRedraw = true;
            resizeCanvasNextFrame = true;

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
                System.Threading.Thread.MemoryBarrier();
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

        [MixinSettings(TargetClass = "GameEngine", TargetMethod = "run")]
        public virtual void run()
        {
            System.Console.WriteLine("Run");
            GameEngine.fiveOrOne = 5;
            this.setFocusCycleRoot(true);
            this.addCanvas();
            this.setUp();
            class177.clock = class161.method857();
            while (0L == GameEngine.stopTimeMs || class153.clockNow() < GameEngine.stopTimeMs)
            {
                System.Console.WriteLine("Inside While");
                InterfaceParent.gameCyclesToDo = class177.clock.wait(
                    GameEngine.cycleDurationMillis,
                    GameEngine.fiveOrOne
                );
                System.Console.WriteLine(InterfaceParent.gameCyclesToDo);
                for (int i = 0; i < InterfaceParent.gameCyclesToDo; ++i)
                {
                    this.clientTick();
                }
                this.graphicsTick();
                this.post(this.canvas);
            }

            this.kill();
        }
        [MixinSettings(TargetClass = "GameEngine", TargetMethod = "clientTick")]
        void clientTick()
        {
            System.Console.WriteLine("clientTick");
            long num = class153.clockNow();
            long num2 = GameEngine.clientTickTimes[ReflectionCheck.field172];
            GameEngine.clientTickTimes[ReflectionCheck.field172] = num;
            ReflectionCheck.field172 = (ReflectionCheck.field172 + 1 & 31);
            if (0L == num2 || num > num2)
            {
            }
            GameEngine.hasFocus = GameEngine.volatileFocus;
            this.doCycle();
        }
    }
}
