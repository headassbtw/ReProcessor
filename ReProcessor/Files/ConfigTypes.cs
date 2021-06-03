using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReProcessor.Files
{


    public class Defaults
    {
        public static List<object> Passes = new object[] {
            "Prefilter13",
            "Prefilter3",
            "Downsample13",
            "Downsample4",
            "DownsampleBilinearGamma",
            "UpsampleTent",
            "UpsampleBox",
            "UpsampleTentGamma",
            "UpsampleBoxGamma",
            "Bilinear",
            "BilinearGamma",
            "UpsampleTentAndReinhardToneMapping",
            "UpsampleTentAndACESToneMapping"
        }.ToList();


        public static List<CameraSetting> BloomDefaults
        {
            get => new List<CameraSetting>()
            {
                new CameraSetting(
                    "Blend Factor",
                    "_bloomBlendFactor",
                    0.3f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Radius",
                    "_bloomRadius",
                    5f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Intensity",
                    "_bloomIntensity",
                    1f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Intensity Offset",
                    "_downBloomIntensityOffset",
                    1f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Weight",
                    "_pyramidWeightsParam",
                    1f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Alpha Weights",
                    "_alphaWeights",
                    4f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Pre Filter Pass",
                    "_preFilterPass",
                    "Prefilter13",
                    typeof(PyramidBloomRendererSO.Pass)
                    ),
                new CameraSetting(
                    "Downsample Pass",
                    "_downsamplePass",
                    "Downsample13",
                    typeof(PyramidBloomRendererSO.Pass)
                    ),
                new CameraSetting(
                    "Upsample Pass",
                    "_upsamplePass",
                    "UpsampleTent",
                    typeof(PyramidBloomRendererSO.Pass)
                    ),
                new CameraSetting(
                    "Final Upsample Pass",
                    "_finalUpsamplePass",
                    "UpsampleTent",
                    typeof(PyramidBloomRendererSO.Pass)
                    )
            };
        }

        public static List<CameraSetting> ColorBoostDefaults
        {
            get => new List<CameraSetting>()
                {
                    new CameraSetting(
                    "Base Color Boost",
                    "_baseColorBoost",
                    1f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Base Color Boost Threshold",
                    "_baseColorBoostThreshold",
                    0.0f,
                    typeof(System.Single)
                    )
                };
        }
    }


    public class Preset
    {
        public string Name;
        public List<CameraSetting> Bloom;
        public List<CameraSetting> ColorBoost;
        public List<CameraSetting> User;

        public Preset() { }
        public Preset(string name)
        {
            this.Name = name;
            this.Bloom = new List<CameraSetting>()
            {
                new CameraSetting(
                    "Blend Factor",
                    "_bloomBlendFactor",
                    0.3f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Radius",
                    "_bloomRadius",
                    5f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Intensity",
                    "_bloomIntensity",
                    1f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Intensity Offset",
                    "_downBloomIntensityOffset",
                    1f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Weight",
                    "_pyramidWeightsParam",
                    1f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Alpha Weights",
                    "_alphaWeights",
                    4f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Pre Filter Pass",
                    "_preFilterPass",
                    "Prefilter13",
                    typeof(PyramidBloomRendererSO.Pass)
                    ),
                new CameraSetting(
                    "Downsample Pass",
                    "_downsamplePass",
                    "Downsample13",
                    typeof(PyramidBloomRendererSO.Pass)
                    ),
                new CameraSetting(
                    "Upsample Pass",
                    "_upsamplePass",
                    "UpsampleTent",
                    typeof(PyramidBloomRendererSO.Pass)
                    ),
                new CameraSetting(
                    "Final Upsample Pass",
                    "_finalUpsamplePass",
                    "UpsampleTent",
                    typeof(PyramidBloomRendererSO.Pass)
                    )
            };
            this.ColorBoost = new List<CameraSetting>()
            {
                new CameraSetting(
                    "Base Color Boost",
                    "_baseColorBoost",
                    1f,
                    typeof(System.Single)
                    ),
                new CameraSetting(
                    "Base Color Boost Threshold",
                    "_baseColorBoostThreshold",
                    0.0f,
                    typeof(System.Single)
                    )
            };
            this.User = new List<CameraSetting>();
        }
        public Preset(string name, List<CameraSetting> bloom, List<CameraSetting> colorBoost)
        {
            this.Name = name;
            this.Bloom = bloom;
            this.ColorBoost = colorBoost;
        }
        public Preset(string name, List<CameraSetting> user)
        {
            this.Name = name;
            this.User = user;
        }
    }


    public class CameraSetting
    {
        public string FriendlyName = "";
        [JsonConverter(typeof(PassJsonConverter))]
        public object Value;
        public string PropertyName = "";
        public System.Type ValueType = typeof(System.Single);

        public CameraSetting(string friendlyName, string propertyName, object value, System.Type type)
        {
            this.FriendlyName = friendlyName;
            this.PropertyName = propertyName;

            this.Value = value;
            this.ValueType = type;
        }
    }
}
