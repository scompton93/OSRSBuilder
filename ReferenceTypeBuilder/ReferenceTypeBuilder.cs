using dnlib.DotNet;

namespace ReferenceTypeBuilder
{
    public class ReferenceAssemblyBuilder
    {
        public ReferenceAssemblyBuilder(string assemblyPath, string outPath)
        {
            var assembly = AssemblyDef.Load(assemblyPath);
            //build reference type
            foreach (var module in assembly.Modules)
            {
                foreach (var type in module.Types)
                {
                    foreach (var method in type.Methods)
                    {
                        method.Access = MethodAttributes.Public;
                    }
                    foreach (var field in type.Fields)
                    {
                        field.Access = FieldAttributes.Public;
                    }
                    type.Attributes &= ~TypeAttributes.Sealed;
                    type.Attributes =
                        (type.Attributes & ~TypeAttributes.VisibilityMask) | TypeAttributes.Public;
                }
            }

            assembly.Write(outPath);
        }
    }
}