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
        [MixinSettings(TargetClass = "GameEngine", TargetMethod = "paint", Replace = true)]
        public void paint(Graphics g)
        {
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

        [MixinSettings(TargetClass = "GameEngine", TargetMethod = "graphicsTick", Replace = true)]
        internal virtual void graphicsTick()
        {
            //fullRedraw = true;
            //resizeCanvasNextFrame = true;

            Container container = this.container();
            long clockNow = global::class153.clockNow();
            System.Console.WriteLine("clockNow:" + clockNow);
            long graphicsTickTimes = global::GameEngine.graphicsTickTimes[global::class20.field69];
            System.Console.WriteLine("graphicsTickTimes:" + graphicsTickTimes);
            System.Console.WriteLine("field69 before:" + global::class20.field69);


            global::GameEngine.graphicsTickTimes[global::class20.field69] = clockNow;


            global::class20.field69 = (global::class20.field69 + 1 & 31);
            System.Console.WriteLine("field69 after:" + global::class20.field69);


            if (0L != graphicsTickTimes && clockNow > graphicsTickTimes)
            {
                int clockDiff = (int)(clockNow - graphicsTickTimes);
                int num4 = (clockDiff >> 1) + 32000;
                int num5 = clockDiff;
                int fpsCalc = (-num4);
                if (num5 != -1)
                    fpsCalc = num4 / num5;

                System.Console.WriteLine("FPS:" + fpsCalc);

                global::GameEngine.fps = fpsCalc;
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
                System.Console.WriteLine("invalid canvas");
                this.replaceCanvas();
            }
            this.method149();
            this.draw(this.fullRedraw);
            if (this.fullRedraw)
            {
                System.Console.WriteLine("clearbg");
                this.clearBackground();
            }
            this.fullRedraw = false;
        }

        [MixinSettings(TargetClass = "GameEngine", TargetMethod = "run", Replace = true)]
        public virtual void run()
        {
            GameEngine.fiveOrOne = 5;
            this.setFocusCycleRoot(true);
            this.addCanvas();
            this.setUp();
            class177.clock = class161.method857();
            while (0L == GameEngine.stopTimeMs || class153.clockNow() < GameEngine.stopTimeMs)
            {
                InterfaceParent.gameCyclesToDo = class177.clock.wait(
                    GameEngine.cycleDurationMillis,
                    GameEngine.fiveOrOne
                );
                for (int i = 0; i < InterfaceParent.gameCyclesToDo; ++i)
                {
                    this.clientTick();
                }
                this.graphicsTick();
                this.post(this.canvas);
            }

            this.kill();
        }

        [MixinSettings(TargetClass = "GameEngine", TargetMethod = "clientTick", Replace = true)]
        void clientTick()
        {
            long num = class153.clockNow();
            long num2 = GameEngine.clientTickTimes[ReflectionCheck.field172];
            GameEngine.clientTickTimes[ReflectionCheck.field172] = num;
            ReflectionCheck.field172 = (ReflectionCheck.field172 + 1 & 31);
            if (0L == num2 || num > num2) { }
            GameEngine.hasFocus = GameEngine.volatileFocus;
            this.doCycle();
        }

        [MixinSettings(TargetClass = "GameEngine", TargetMethod = "addCanvas", Replace = true)]
        internal void addCanvas()
        {
            System.Console.WriteLine("add vancas");
            Container container = this.container();
            if (this.canvas != null)
            {
                this.canvas.removeFocusListener(this);
                container.remove(this.canvas);
            }
            global::GameEngine.canvasWidth = java.lang.Math.max(container.getWidth(), this.field126);
            global::class127.canvasHeight = java.lang.Math.max(container.getHeight(), this.field123);
            if (this.frame != null)
            {
                Insets insets = this.frame.getInsets();
                global::GameEngine.canvasWidth -= insets.right + insets.left;
                global::class127.canvasHeight -= insets.bottom + insets.top;
            }
            this.canvas = new global::Canvas(this);
            container.setBackground(Color.BLACK);
            container.setLayout(null);
            container.add(this.canvas);
            this.canvas.setSize(global::GameEngine.canvasWidth, global::class127.canvasHeight);
            this.canvas.setVisible(true);
            this.canvas.setBackground(Color.BLACK);
            if (container == this.frame)
            {
                Insets insets = this.frame.getInsets();
                this.canvas.setLocation(this.canvasX + insets.left, insets.top + this.canvasY);
            }
            else
            {
                this.canvas.setLocation(this.canvasX, this.canvasY);
            }
            this.canvas.addFocusListener(this);
            this.canvas.requestFocus();
            this.fullRedraw = true;

            if (global::KeyHandler.rasterProvider != null && global::GameEngine.canvasWidth == global::KeyHandler.rasterProvider.width && global::class127.canvasHeight == global::KeyHandler.rasterProvider.height)
            {
                ((global::RasterProvider)global::KeyHandler.rasterProvider).setComponent(this.canvas);
                global::KeyHandler.rasterProvider.drawFull(0, 0);
            }
            else
            {
                global::KeyHandler.rasterProvider = new global::RasterProvider(global::GameEngine.canvasWidth, global::class127.canvasHeight, this.canvas);
            }
            this.isCanvasInvalid = false;

            this.field130 = global::class153.clockNow();
        }
    }
}
