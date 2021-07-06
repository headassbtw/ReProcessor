using IPA.Config.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//using ReProcessor.Files;

[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace ReProcessor
{
    public class Config
    {
        public Config() { }
        public event Action<Config> Updated;
        public virtual float MinAmountIncrease { get; set; } = -3;
        public virtual float MaxAmountIncrease { get; set; } = 3;
        
        public virtual void Changed()
        {
            Updated?.Invoke(this);
        }
    }
}
