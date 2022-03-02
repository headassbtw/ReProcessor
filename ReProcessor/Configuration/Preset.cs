using Newtonsoft.Json;

namespace ReProcessor.Configuration
{
    internal interface ICameraSettings
    {
        float BloomRadius { get; set; }
        float BlendFactor { get; set; }
        float Intensity { get; set; }
        float IntensityOffset { get; set; }
        float Weight { get; set; }
        float AlphaWeights { get; set; }
        PyramidBloomRendererSO.Pass PreFilterPass { get; set; }
        PyramidBloomRendererSO.Pass DownSamplePass { get; set; }
        PyramidBloomRendererSO.Pass UpSamplePass { get; set; }
        PyramidBloomRendererSO.Pass FinalUpSamplePass { get; set; }
        float BaseColorBoost { get; set; }
        float BaseColorBoostThreshold { get; set; }
    }

    internal class CameraSettings : ICameraSettings
    {
        public float BloomRadius { get; set; }

        public float BlendFactor { get; set; }

        public float Intensity { get; set; }

        public float IntensityOffset { get; set; }

        public float Weight { get; set; }

        public float AlphaWeights { get; set; }

        public PyramidBloomRendererSO.Pass PreFilterPass { get; set; }

        public PyramidBloomRendererSO.Pass DownSamplePass { get; set; }

        public PyramidBloomRendererSO.Pass UpSamplePass { get; set; }

        public PyramidBloomRendererSO.Pass FinalUpSamplePass { get; set; }

        public float BaseColorBoost { get; set; }

        public float BaseColorBoostThreshold { get; set; }

        public static CameraSettings Default => new()
        {
            BloomRadius = 5f,
            BlendFactor = 0.3f,
            Intensity = 1f,
            IntensityOffset = 1f,
            Weight = 1f,
            AlphaWeights = 4f,
            PreFilterPass = PyramidBloomRendererSO.Pass.Prefilter13,
            DownSamplePass = PyramidBloomRendererSO.Pass.Downsample13,
            UpSamplePass = PyramidBloomRendererSO.Pass.UpsampleTent,
            FinalUpSamplePass = PyramidBloomRendererSO.Pass.UpsampleTent,
            BaseColorBoost = 1f,
            BaseColorBoostThreshold = 0
        };
    }

    internal class Preset
    {
        public readonly string Name;
        public readonly CameraSettings Props;

        public Preset(string name)
        {
            Name = name;
            Props = CameraSettings.Default;
        }
        [JsonConstructor]
        public Preset(string name, CameraSettings props)
        {
            Name = name;
            Props = props;
        }

        public static Preset CreateDefault() => new("Default");
    }
}