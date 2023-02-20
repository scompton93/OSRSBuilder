using ObfuscationMapGenerator;
using Mono.Cecil;
using System.IO;

namespace MappingApplyer
{
    public class ApplyDeobfuscatedName
    {
        public ApplyDeobfuscatedName(string targetAssembly, List<ObfuscatedType> deobfuscatedMappings, string output) {
            var module = ModuleDefinition.ReadModule(targetAssembly);
            foreach(var mapping in deobfuscatedMappings.Where(m=>m.ObfuscatedName != null))
            {
                foreach(var type in module.Types)
                {
                    if (type.Name != mapping.ObfuscatedName)
                        continue;

                    type.Name = mapping.DeobfuscatedName;
                    Console.WriteLine($"Changing {mapping.ObfuscatedName} to {mapping.DeobfuscatedName}");

                    //Check if has implements tag
                    //var hasAttribute = type.CustomAttributes.Any(a => a.AttributeType.FullName == "IKVM.Attributes.ImplementsAttribute");

                    foreach (var method in type.Methods)
                    {
                        var deobMethod = mapping.ObfuscatedMethods.Where(o => o.ObfuscatedName == method.Name && o.Static == method.IsStatic).FirstOrDefault();

                        if (deobMethod == null || deobMethod.DeobfuscatedName == null)
                            continue;
                        method.Name = deobMethod.DeobfuscatedName;
                        Console.WriteLine($"Changing method {deobMethod.ObfuscatedName} to {deobMethod.DeobfuscatedName}");
                    }
                    foreach (var field in type.Fields)
                    {
                        var deobMethod = mapping.ObfuscatedFields.Where(o => o.ObfuscatedName == field.Name).FirstOrDefault();
                        if (deobMethod == null || deobMethod.DeobfuscatedName == null)
                            continue;
                        field.Name = deobMethod.DeobfuscatedName;
                        Console.WriteLine($"Changing field {deobMethod.ObfuscatedName} to {deobMethod.DeobfuscatedName}");
                    }
                }
            }
            module.Write(output);
        }
    }
}