using MappingApplyer;
using Newtonsoft.Json;
using ObfuscationMapGenerator;
using ReferenceTypeBuilder;
using CodeWeaving;
using dnlib.DotNet;

namespace Builder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var osrs101Path = @"C:\Users\x\Desktop\osrs-211.dll"; // This has the actual names; example: GameShell, SoundPCMProvider -CometClient

            var deob101Path = @"C:\Users\x\Desktop\deob-211.dll"; // This is the actual deob we will be working with. - CometClient

            //Generate Target Obfuscation Mappings
            ObfuscationMapper.ObfuscationMapGenerator(osrs101Path, "targetMappings.json");
            ObfuscationMapper.ObfuscationMapGenerator(deob101Path, "deobMappings.json");

            var targetMappings = JsonConvert.DeserializeObject<List<ObfuscatedType>>(File.ReadAllText("targetMappings.json"));
            var deobMappings = JsonConvert.DeserializeObject<List<ObfuscatedType>>(File.ReadAllText("deobMappings.json"));

            // Match target and deob and apply mappings

            var applyOb = new ApplyObfuscatedName(deob101Path, deobMappings, "ObfuscatedOSRS.dll");
            var applyDeob = new ApplyDeobfuscatedName("ObfuscatedOSRS.dll", targetMappings, "DeobfuscatedOSRS.dll");

            // Create mixin reference assembly
            var referenceTypeBuilder = new ReferenceAssemblyBuilder("DeobfuscatedNames.dll","ReferenceOSRS.dll");

            // temp hack to set field to public
            {
                var assembly = AssemblyDef.Load("DeobfuscatedNames.dll");
                foreach (var module in assembly.Modules)
                {
                    foreach (var type in module.Types.Where(t=>t.Name == "GameEngine"))
                    {
                        foreach (var field in type.Fields.Where(f=>f.Name == "keyHandler" || f.Name == "gameEngine" || f.Name == "canvas"))
                        {
                            field.Access = FieldAttributes.Public;
                        }
                        type.Attributes &= ~TypeAttributes.Sealed;
                    }
                }

                assembly.Write("DeobfuscatedNamesUnprotected.dll");
            }

            // Apply Mixins
            var codeWeaver = new CodeWeaver("DeobfuscatedNamesUnprotected.dll", "OSRSMixed.dll", @"C:\Users\x\source\repos\a\ObfuscationMapGenerator\Mixins\bin\Debug\Mixins.dll");

            // Apply scripting engine

            // Launcher (maybe class launcher from vjslib)
            Console.ReadKey();
        }
    }
}