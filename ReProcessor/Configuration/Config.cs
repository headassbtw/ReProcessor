using IPA.Config.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ReProcessor.Files;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ReProcessor
{
    public class Config
    {
        public event Action<Config>? Updated;
        public virtual bool Enabled { get; set; }
        public virtual Preset preset{ get; set;}



        public virtual void Changed()
        {
            Updated?.Invoke(this);
        }
    }
}
