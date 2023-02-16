
using dnlib.DotNet;

namespace CodeWeaving
{
    public class CodeWeaver
    {
        public CodeWeaver(string source,string output, string mixinsPath)
        {
            var assembly = AssemblyDef.Load(source);

            var mixinAssembly = AssemblyDef.Load(mixinsPath);

            List<MethodDef> methodsWithAttribute = mixinAssembly.Modules
                .First()
                .GetTypes()
                .SelectMany(t => t.Methods)
                .Where(m => m.HasCustomAttributes)
                .Where(m => m.CustomAttributes.Any(ca => ca.TypeFullName == "Mixins.MixinSettings"))
                .ToList();

            foreach (var mixinMethod in methodsWithAttribute)
            {
                var mixinSettings = mixinMethod.CustomAttributes
                    .Where(a => a.TypeFullName == "Mixins.MixinSettings")
                    .First();
                var targetClass = mixinSettings.Properties
                    .Where(a => a.Name == "TargetClass")
                    .First()
                    .Value.ToString();
                var targetMethod = mixinSettings.Properties
                    .Where(a => a.Name == "TargetMethod")
                    .First()
                    .Value.ToString();

                mixinMethod.DeclaringType = null;
                mixinMethod.Name = mixinMethod.Name + "_Post";
                mixinMethod.CustomAttributes.Clear();
                foreach (var targetModule in assembly.Modules)
                {
                    foreach (var targetType in targetModule.Types)
                    {
                        if (targetType.Name == targetClass)
                        {
                            //Add mixin method to same class
                            targetType.Methods.Add(mixinMethod);

                            //copy body over
                            var sourceMethodToWriteWith = assembly.Modules
                                .First()
                                .Types.Where(t => t.Name == targetClass)
                                .SelectMany(t => t.Methods)
                                .FirstOrDefault(m => m.Name == mixinMethod.Name);

                            var test = assembly.Modules
                                .First()
                                .Types.Where(t => t.Name == targetClass)
                                .SelectMany(t => t.Methods);

                            var targetMethodToOverWrite = assembly.Modules
                                .First()
                                .Types.Where(t => t.Name == targetClass)
                                .SelectMany(t => t.Methods)
                                .FirstOrDefault(m => m.Name == targetMethod);

                            targetMethodToOverWrite.Body = sourceMethodToWriteWith.Body;

                            //targetType.Methods.Remove(sourceMethodToWriteWith);
                        }
                    }
                }
            }
            assembly.Write(output);
        }
    }
}