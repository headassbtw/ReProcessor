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
        public ColorBoostConfig ColorBoost;

        public Preset(string name, BloomConfig bloom, ColorBoostConfig colorBoost)
        {
            this.Name = name;
            this.Bloom = bloom;
            this.ColorBoost = colorBoost;
        }
    }


    public class BloomConfig
    {
        public bool Enabled;
        public System.Single BlendFactor;
        public System.Single Radius;
        public System.Single Intensity;
        public System.Single IntensityOffset;
        public System.Single Weight;
        public System.Single AlphaWeights;

        public BloomConfig(System.Single blendFactor = 0.3f, System.Single radius = 5f, System.Single intensity = 1f, System.Single intensityOffset = 1f, System.Single weight = 0.01f, System.Single alphaWeights = 4f)
        {
            this.BlendFactor = blendFactor;
            this.Radius = radius;
            this.Intensity = intensity;
            this.IntensityOffset = intensityOffset;
            this.Weight = weight;
            this.AlphaWeights = alphaWeights;
        }
    }
    public class ColorBoostConfig
    {
        public System.Single Boost;
        public System.Single BoostThreshold;

        public ColorBoostConfig(System.Single boost = 0.05f, System.Single boostThreshold = 0.01f)
        {
            this.Boost = boost;
            this.BoostThreshold = boostThreshold;
        }
    }
}
