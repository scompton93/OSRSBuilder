using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObfuscationMapGenerator
{
    public class ObfuscatedType
    {
        public ObfuscatedType() { ObfuscatedMethods = new List<ObfuscatedMethod>(); ObfuscatedFields = new List<ObfuscatedField>(); }
        public string DeobfuscatedName { get; set; }
        public string ObfuscatedName { get; set; }
        public string Implements { get; set; }

        public List<ObfuscatedMethod> ObfuscatedMethods { get; set; }
        public List<ObfuscatedField> ObfuscatedFields { get; set; }
    }

    public class ObfuscatedMethod
    {
        public ObfuscatedMethod() { ObfuscatedSignature = new List<String>(); }
        public string DeobfuscatedName { get; set; }
        public string ObfuscatedName { get; set; }
        public bool Static { get; set; }
        public List<String> ObfuscatedSignature { get; set; }
        public string Export { get; set; }
    }

    public class ObfuscatedField
    {
        public string DeobfuscatedName { get; set; }
        public string ObfuscatedName { get; set; }
        public List<String> ObfuscatedSignature { get; set; }
        public string Export { get; set; }
    }
}
