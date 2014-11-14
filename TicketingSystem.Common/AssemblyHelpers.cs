namespace TicketingSystem.Common
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class AssemblyHelpers
    {
        public static string GetDirectoryForAssembyl(Assembly assembly)
        {
            var assemblyLocation = assembly.CodeBase;
            var location = new UriBuilder(assemblyLocation);
            var path = Uri.UnescapeDataString(location.Path);
            var directory = Path.GetDirectoryName(path);
            return directory;
        }
    }
}
