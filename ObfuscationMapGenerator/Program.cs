﻿

using dnlib.DotNet;
using Newtonsoft.Json;


namespace ObfuscationMapGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var module = ModuleDefMD.Load(@"C:\Users\x\Desktop\osrs-211.dll");

            var obfuscatedTypes = new List<ObfuscatedType>();

            foreach (var type in module.GetTypes())
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
                    if(obfuscatedType.DeobfuscatedName != null)
                        obfuscatedType.ObfuscatedMethods.Add(obfuscatedMethod);
                }

                foreach (var field in type.Fields)
                {
                    if (field.CustomAttributes == null)
                        continue;

                    var obfuscatedField = new ObfuscatedField();

                    obfuscatedField.DeobfuscatedName = field.Name;
                    foreach (var customAttribute in field.CustomAttributes)
                    {
                        if (customAttribute.AttributeType.Name == "ObfuscatedNameAttribute")
                        {
                            if (customAttribute.AttributeType.Name == "ObfuscatedNameAttribute")
                            {
                                List<CAArgument> a2 = (List<CAArgument>)customAttribute.ConstructorArguments.Last<CAArgument>().Value;
                                string a3 = a2[3].Value.ToString();
                                obfuscatedField.ObfuscatedName = a3;
                            }
                            else if (customAttribute.AttributeType.FullName == "net.runelite.mapping.ExportAttribute")
                            {
                                List<CAArgument> a2 = (List<CAArgument>)customAttribute.ConstructorArguments.Last<CAArgument>().Value;
                                string a3 = a2[3].Value.ToString();
                                obfuscatedField.Export = a3;
                            }
                            else if (customAttribute.AttributeType.FullName == "net.runelite.mapping.ObfuscatedSignatureAttribute")
                            {
                                List<CAArgument> a2 = (List<CAArgument>)customAttribute.ConstructorArguments.Last<CAArgument>().Value;
                                var test = (List<CAArgument>)customAttribute.ConstructorArguments;
                                for (int i = 2; i < test.Count; i++)
                                {
                                    obfuscatedField.ObfuscatedSignature.Add(a2[i].Value.ToString());
                                }
                            }
                        }
                    }
                    if (obfuscatedType.DeobfuscatedName != null)
                        obfuscatedType.ObfuscatedFields.Add(obfuscatedField);
                }
                if (obfuscatedType.DeobfuscatedName != null)
                    obfuscatedTypes.Add(obfuscatedType);
            }
            var jsonMappings = JsonConvert.SerializeObject(obfuscatedTypes);
            File.WriteAllText("mappings.json", jsonMappings);
            Console.ReadKey();
        }
    }
}
