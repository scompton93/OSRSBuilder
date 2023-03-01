using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixins
{
    public class MixinField : Attribute
    {
        public string TargetClass { get; set; }
        public string TargetField { get; set; }
        public bool Replace { get; set; }
    }
}