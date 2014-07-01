namespace CSharpScriptingExample
{
    using System;
    using CSharpScriptingExample.Interfaces;

    internal class DummyComponent : IScriptableComponent
    {
        public void DoSomething(string parameter)
        {
            Console.WriteLine("DummyComponent.DoSomething called: {0}", parameter);
        }
    }
}
