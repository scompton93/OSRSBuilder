using ObfuscationMapGenerator;
using Mono.Cecil;
using System.IO;

namespace MappingApplyer
{
    public class ApplyObfuscatedName
    {
        public ApplyObfuscatedName(string targetAssembly, List<ObfuscatedType> deobfuscatedMappings, string output) {
            var module = ModuleDefinition.ReadModule(targetAssembly);
            foreach(var mapping in deobfuscatedMappings.Where(m=>m.ObfuscatedName != null))
            {
                foreach(var type in module.Types)
                {
                    if (type.Name != mapping.DeobfuscatedName)
                        continue;

                    type.Name = mapping.ObfuscatedName;
                    Console.WriteLine($"Changing {mapping.DeobfuscatedName} to {mapping.ObfuscatedName}");

                    foreach (var method in type.Methods)
                    {
                        var deobMethod = mapping.ObfuscatedMethods.Where(o => o.DeobfuscatedName == method.Name && o.Static == method.IsStatic).FirstOrDefault();
                        if (deobMethod == null || deobMethod.ObfuscatedName == null)
                            continue;
                        method.Name = deobMethod.ObfuscatedName;
                        Console.WriteLine($"Changing method {deobMethod.DeobfuscatedName} to {deobMethod.ObfuscatedName}");
                    }
                    foreach (var field in type.Fields)
                    {
                        var deobMethod = mapping.ObfuscatedFields.Where(o => o.DeobfuscatedName == field.Name).FirstOrDefault();
                        if (deobMethod == null || deobMethod.ObfuscatedName == null)
                            continue;
                        field.Name = deobMethod.ObfuscatedName;
                        Console.WriteLine($"Changing field {deobMethod.DeobfuscatedName} to {deobMethod.ObfuscatedName}");
                    }
                }
            }
            module.Write(output);
        }


    }
}