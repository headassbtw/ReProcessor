using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ReProcessor.Files
{


    public class Preset
    {
        public string Name;
        public BloomConfig Bloom;

    }

    public class BloomConfig
    {
        public bool Enabled;
        public System.Single BlendFactor;
        public System.Single Radius;
        public System.Single Intensity;
        public System.Single IntensityOffset;
        public System.Single Weight;
        public System.Single AlphaWeight;
    }
}
