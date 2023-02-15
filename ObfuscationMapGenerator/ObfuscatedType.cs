using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscationMapGenerator
{
    internal class ObfuscatedType
    {
        public ObfuscatedType() { ObfuscatedMethods = new List<ObfuscatedMethod>(); }
        internal string DeobfuscatedName { get; set; }
        internal string ObfuscatedName { get; set; }
        internal string Implements { get; set; }
        internal List<ObfuscatedMethod> ObfuscatedMethods { get; set; }

    }

    internal class ObfuscatedMethod
    {
        public ObfuscatedMethod() { ObfuscatedSignature = new List<String>(); }
        internal string DeobfuscatedName { get; set; }
        internal string ObfuscatedName { get; set; }
        internal List<String> ObfuscatedSignature { get; set; }
        internal string Export { get; set; }
    }
}
