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
        public event Action<Config>? Updated;
        public virtual float MaxAmountIncrease { get; set; } = 2;
        



        public virtual void Changed()
        {
            Updated?.Invoke(this);
        }
    }
    public class Preset
    {
        public string Name = "preset";
        public BloomConfig Bloom;
        public ColorBoostConfig ColorBoost;

        public Preset(string name, BloomConfig bloomConfig, ColorBoostConfig colorBoostConfig)
        {
            this.Name = name;
            this.Bloom = bloomConfig;
            this.ColorBoost = colorBoostConfig;
        }
        public Preset(string name)
        {
            this.Name = name;
            this.Bloom = new BloomConfig();
            this.ColorBoost = new ColorBoostConfig();
        }
        public Preset()
        {
            this.Name = "preset";
            this.Bloom = new BloomConfig();
            this.ColorBoost = new ColorBoostConfig();
        }
    }
    public class BloomConfig
    {
        public virtual bool Enabled { get; set; } = true;
        public virtual System.Single BlendFactor { get; set; } = 0.3f;
        public virtual System.Single Radius { get; set; } = 5f;
        public virtual System.Single Intensity { get; set; } = 1f;
        public virtual System.Single IntensityOffset { get; set; } = 1f;
        public virtual System.Single Weight { get; set; } = 0.01f;
        public virtual System.Single AlphaWeights { get; set; } = 4f;

        public BloomConfig()
        {
            this.Enabled = true;
            this.BlendFactor = 0.3f;
            this.Radius = 5f;
            this.Intensity = 1f;
            this.IntensityOffset = 1f;
            this.Weight = 0.01f;
            this.AlphaWeights = 4f;
        }
        public BloomConfig(bool enabled = true, System.Single blendFactor = 0.3f, System.Single radius = 5f, System.Single intensity = 1f, System.Single intensityOffset = 1f, System.Single weight = 0.01f, System.Single alphaWeights = 4f)
        {
            this.Enabled = enabled;
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
        public virtual System.Single Boost { get; set; } = 1f;
        public virtual System.Single BoostThreshold { get; set; } = 0.00f;

        public ColorBoostConfig(System.Single boost = 0.05f, System.Single boostThreshold = 0.01f)
        {
            this.Boost = boost;
            this.BoostThreshold = boostThreshold;
        }
    }
}
