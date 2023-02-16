using MappingApplyer;
using Newtonsoft.Json;
using ObfuscationMapGenerator;

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

            var applyOb = new ApplyObfuscatedName(deob101Path, deobMappings, "ObfuscatedNames.dll");
            var applyDeob = new ApplyDeobfuscatedName("ObfuscatedNames.dll", targetMappings, "DeobfuscatedNames.dll");
            // Apply Mixins

            // Apply scripting engine

            // Launcher (maybe class launcher from vjslib)
            Console.ReadKey();
        }
    }
}