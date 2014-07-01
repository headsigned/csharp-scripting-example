namespace CSharpScriptingExample
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Microsoft.CSharp;

    public static class Helper
    {
        /// <summary>
        /// Compiles the list of C# source files into a dll.
        /// </summary>
        /// <param name="sourceFiles">List of files to compile.</param>
        /// <param name="outputAssemblyPath">Path for the new assembly.</param>
        /// <returns></returns>
        public static Assembly CompileAssembly(string[] sourceFiles, string outputAssemblyPath)
        {
            var codeProvider = new CSharpCodeProvider();

            var compilerParameters = new CompilerParameters
            {
                GenerateExecutable = false,     // Make a DLL
                GenerateInMemory = false,       // Explicitly save it to path specified by compilerParameters.OutputAssembly
                IncludeDebugInformation = true, // Enable debugging - generate .pdb
                OutputAssembly = outputAssemblyPath
            };

            // !! This is important: It adds the THIS project as a reference to the compiled dll to expose the public interfaces (as you would add it in the visual studio)
            compilerParameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);

            var result = codeProvider.CompileAssemblyFromFile(compilerParameters, sourceFiles); // Compile

            if (result.Errors.HasErrors) throw new Exception("Assembly compilation failed.");

            return result.CompiledAssembly;
        }

        /// <summary>
        /// Returns all types that implement the specified interface.
        /// </summary>
        /// <param name="assembly">Assembly to search.</param>
        /// <param name="interfaceType">Interface that types must implement.</param>
        /// <returns></returns>
        public static List<Type> GetTypesImplementingInterface(Assembly assembly, Type interfaceType)
        {
            if (!interfaceType.IsInterface) throw new ArgumentException("Not an interface.", "interfaceType");

            return assembly.GetTypes()
                           .Where(t => interfaceType.IsAssignableFrom(t))
                           .ToList();
        }
    }
}
