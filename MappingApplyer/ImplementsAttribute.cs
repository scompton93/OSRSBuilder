using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MappingApplyer
{
    // Token: 0x0200001D RID: 29
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    public sealed class ImplementsAttribute : Attribute
    {
        // Token: 0x060000B6 RID: 182 RVA: 0x00006A1E File Offset: 0x00005A1E
        public ImplementsAttribute(string[] interfaces)
        {
            this.interfaces = (interfaces);
        }

        // Token: 0x17000023 RID: 35
        // (get) Token: 0x060000B7 RID: 183 RVA: 0x00006A32 File Offset: 0x00005A32
        public string[] Interfaces
        {
            get
            {
                return this.interfaces;
            }
        }

        // Token: 0x04000064 RID: 100
        private string[] interfaces;
    }
}
