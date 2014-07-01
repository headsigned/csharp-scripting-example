namespace CSharpScriptingExample
{
    using System;
    using System.IO;
    using System.Linq;
    using CSharpScriptingExample.Interfaces;

    class Program
    {
        private const string ScriptsDirectory = @"..\..\..\Scripts";
        private const string CompiledScriptsAssemblyName = "Scripts.dll";

        static void Main(string[] args)
        {
            // This is exposed to the scripts
            IScriptableComponent component = new DummyComponent();

            var compiledAssemblyPath = Path.Combine(Environment.CurrentDirectory, ScriptsDirectory, CompiledScriptsAssemblyName);
            var scriptFiles = Directory.EnumerateFiles(ScriptsDirectory, "*.cs", SearchOption.AllDirectories).ToArray();

            var scriptAssembly = Helper.CompileAssembly(scriptFiles, compiledAssemblyPath);

            // Find all types that implement the IScript interface in the compiled assembly
            var scriptTypes = Helper.GetTypesImplementingInterface(scriptAssembly, typeof(IScript));

            foreach (var scriptType in scriptTypes)
            {
                // Creates instances of type and pass component to the constructor
                var script = (IScript)Activator.CreateInstance(scriptType);
                script.Run(component);
            }

            Console.ReadKey(true);
        }
    }
}
