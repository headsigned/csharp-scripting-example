namespace Scripts
{
    using CSharpScriptingExample.Interfaces;

    class MyCustomScript : IScript
    {
        public void Run(IScriptableComponent component)
        {
            component.DoSomething("Hello from MyCustomScript.");
        }
    }
}
