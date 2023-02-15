

using dnlib.DotNet;


namespace ObfuscationMapGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var module = ModuleDefMD.Load(@"C:\Users\x\Desktop\osrs-211.dll");

            var obfuscatedTypes = new List<ObfuscatedType>();

            foreach (var type in module.GetTypes().Where(t => t.HasCustomAttributes && t.Name == "GameEngine"))
            {
                if (type.CustomAttributes == null)
                    continue;
                var obfuscatedType = new ObfuscatedType();
                obfuscatedType.DeobfuscatedName = type.Name;
                foreach (var customAttribute in type.CustomAttributes)
                {
                    if (customAttribute.AttributeType.Name == "ObfuscatedNameAttribute")
                    {
                        List<CAArgument> a2 = (List<CAArgument>)customAttribute.ConstructorArguments.Last<CAArgument>().Value;
                        string a3 = a2[3].Value.ToString();
                        obfuscatedType.ObfuscatedName = a3;
                    }
                    else if (customAttribute.AttributeType.FullName == "net.runelite.mapping.ImplementsAttribute")
                    {
                        List<CAArgument> a2 = (List<CAArgument>)customAttribute.ConstructorArguments.Last<CAArgument>().Value;
                        string a3 = a2[3].Value.ToString();
                        obfuscatedType.Implements = a3;
                    }
                }
                foreach (var method in type.Methods)
                {
                    if (method.CustomAttributes == null)
                        continue;
                    var obfuscatedMethod = new ObfuscatedMethod();
                    obfuscatedMethod.DeobfuscatedName = method.Name;
                    foreach (var customAttribute in method.CustomAttributes)
                    {
                        if (customAttribute.AttributeType.Name == "ObfuscatedNameAttribute")
                        {
                            List<CAArgument> a2 = (List<CAArgument>)customAttribute.ConstructorArguments.Last<CAArgument>().Value;
                            string a3 = a2[3].Value.ToString();
                            obfuscatedMethod.ObfuscatedName = a3;
                        }
                        else if (customAttribute.AttributeType.FullName == "net.runelite.mapping.ExportAttribute")
                        {
                            List<CAArgument> a2 = (List<CAArgument>)customAttribute.ConstructorArguments.Last<CAArgument>().Value;
                            string a3 = a2[3].Value.ToString();
                            obfuscatedMethod.Export = a3;
                        }
                        else if (customAttribute.AttributeType.FullName == "net.runelite.mapping.ObfuscatedSignatureAttribute")
                        {
                            List<CAArgument> a2 = (List<CAArgument>)customAttribute.ConstructorArguments.Last<CAArgument>().Value;
                            var test = (List<CAArgument>)customAttribute.ConstructorArguments;
                            for (int i = 2; i < test.Count; i++)
                            {
                                obfuscatedMethod.ObfuscatedSignature.Add(a2[i].Value.ToString());
                            }
                        }
                    }
                    obfuscatedType.ObfuscatedMethods.Add(obfuscatedMethod);
                }
                obfuscatedTypes.Add(obfuscatedType);
            }

            Console.ReadKey();
        }
    }
}
