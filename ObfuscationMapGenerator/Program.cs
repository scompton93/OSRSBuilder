using Mono.Cecil;

namespace ObfuscationMapGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var module = ModuleDefinition.ReadModule(@"C:\Users\x\Desktop\osrs-211.dll");

            var obfuscatedTypes = new List<ObfuscatedType>();

            foreach (
                var type in module.Types.Where(t => t.HasCustomAttributes && t.Name == "GameEngine")
            )
            {
                if (type.CustomAttributes == null)
                    continue;
                var obfuscatedType = new ObfuscatedType();
                obfuscatedType.DeobfuscatedName = type.Name;
                foreach (var customAttribute in type.CustomAttributes)
                {
                    if (customAttribute.AttributeType.Name == "ObfuscatedNameAttribute")
                    {
                        CustomAttributeArgument[] constructorArguments = (CustomAttributeArgument[])
                            customAttribute.ConstructorArguments.First().Value;

                        obfuscatedType.ObfuscatedName = (string)
                            constructorArguments[3].Value;
                    }
                    else if (
                        customAttribute.AttributeType.FullName
                        == "net.runelite.mapping.ImplementsAttribute"
                    )
                    {
                        CustomAttributeArgument[] constructorArguments = (CustomAttributeArgument[])
                            customAttribute.ConstructorArguments.First().Value;

                        obfuscatedType.Implements = (string)
                            constructorArguments[3].Value;
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
                            CustomAttributeArgument[] constructorArguments =
                                (CustomAttributeArgument[])
                                    customAttribute.ConstructorArguments.First().Value;

                            obfuscatedMethod.ObfuscatedName = (string)
                                constructorArguments[3].Value;
                        }
                        else if (
                            customAttribute.AttributeType.FullName
                            == "net.runelite.mapping.ExportAttribute"
                        )
                        {
                            CustomAttributeArgument[] constructorArguments =
                                (CustomAttributeArgument[])
                                    customAttribute.ConstructorArguments.First().Value;

                            obfuscatedMethod.Export = (string)
                                constructorArguments[3].Value;
                        }
                        else if (
                            customAttribute.AttributeType.FullName
                            == "net.runelite.mapping.ObfuscatedSignatureAttribute"
                        )
                        {
                            CustomAttributeArgument[] constructorArguments =
                                (CustomAttributeArgument[])
                                    customAttribute.ConstructorArguments.First().Value;

                            for (int i = 2; i < constructorArguments.Length; i++)
                            {
                                obfuscatedMethod.ObfuscatedSignature.Add(
                                    (string)constructorArguments[i].Value
                                );
                            }
                        }
                        obfuscatedType.ObfuscatedMethods.Add(obfuscatedMethod);
                    }
                    obfuscatedTypes.Add(obfuscatedType);
                }
            }
            Console.ReadKey();
        }
    }
}
