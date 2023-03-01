using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixins
{
    public class MixinMethod : Attribute
    {
        public string TargetClass { get; set; }
        public string TargetMethod { get; set; }
        public bool Replace { get; set; }
    }
}