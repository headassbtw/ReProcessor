using IPA.Config.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ReProcessor
{
    internal class Config
    {
        public event Action<Config>? Updated;
        public virtual bool Enabled { get; set; } = true;
        public virtual System.Single BloomBlendFactor { get; set; } = 0.3f;



        public virtual void Changed()
        {
            Updated?.Invoke(this);
        }
    }
}
