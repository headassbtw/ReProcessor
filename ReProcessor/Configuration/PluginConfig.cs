using System.Runtime.CompilerServices;
using IPA.Config.Stores;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ReProcessor.Configuration
{
    internal class PluginConfig
    {
        public virtual string Preset { get; internal set; } = "Default";
        public virtual bool Introduced { get; internal set; } = false;
    }
}