using ikvm.@internal;
using sun.misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixins
{
    public abstract class class153mod : class153
    {
        protected class153mod(class142 value) : base(value)
        {
        }

        //[MixinMethod(TargetClass = "class153", TargetMethod = "clockNow", Replace = true)]
        public static long clockNow()
        {
            long currentTimeMillis = java.lang.System.currentTimeMillis();
            if (currentTimeMillis < class286.field2687)
            {
                class286.field2688 += class286.field2687 - currentTimeMillis;
            }
            class286.field2687 = currentTimeMillis;
            return class286.field2688 + currentTimeMillis;
        }
    }
}
