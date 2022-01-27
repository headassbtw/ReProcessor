using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]

namespace ReProcessor.Configuration
{
    public class PluginConfig
    {
        public static PluginConfig Instance { get; set; }

        public virtual string Preset { get; internal set; } = "Default";
    }
}