using System;
using System.Reflection;
using JetBrains.Application;
using JetBrains.ComponentModel;

namespace AgentSmith
{
    /*[ShellComponentImplementation(ProgramConfigurations.ALL)]
    class AssemblyRegistrar : IShellComponent
    {
        private AssemblyResolver _myAssemblyResolver;

        public void Dispose()
        {
            _myAssemblyResolver.Dispose();
        }

        public void Init()
        {
            _myAssemblyResolver = AssemblyResolver.FromAssembly(Assembly.GetExecutingAssembly());
            _myAssemblyResolver.Install(AppDomain.CurrentDomain);             
        }
    }
     * */
}
