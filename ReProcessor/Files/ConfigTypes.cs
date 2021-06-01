using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ReProcessor.Files
{
    public enum valueType
    {
        num,
        enm,
        str
    }


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
                    valueType.num
                    ),
                new CameraSetting(
                    "Radius",
                    "_bloomRadius",
                    5f,
                    valueType.num
                    ),
                new CameraSetting(
                    "Intensity",
                    "_bloomIntensity",
                    1f,
                    valueType.num
                    ),
                new CameraSetting(
                    "Intensity Offset",
                    "_downBloomIntensityOffset",
                    1f,
                    valueType.num
                    ),
                new CameraSetting(
                    "Weight",
                    "_pyramidWeightsParam",
                    1f,
                    valueType.num
                    ),
                new CameraSetting(
                    "Alpha Weights",
                    "_alphaWeights",
                    4f,
                    valueType.num
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
                    valueType.num
                    ),
                new CameraSetting(
                    "Base Color Boost Threshold",
                    "_baseColorBoostThreshold",
                    0.0f,
                    valueType.num
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
                    valueType.num
                    ),
                new CameraSetting(
                    "Radius",
                    "_bloomRadius",
                    5f,
                    valueType.num
                    ),
                new CameraSetting(
                    "Intensity",
                    "_bloomIntensity",
                    1f,
                    valueType.num
                    ),
                new CameraSetting(
                    "Intensity Offset",
                    "_downBloomIntensityOffset",
                    1f,
                    valueType.num
                    ),
                new CameraSetting(
                    "Weight",
                    "_pyramidWeightsParam",
                    1f,
                    valueType.num
                    ),
                new CameraSetting(
                    "Alpha Weights",
                    "_alphaWeights",
                    4f,
                    valueType.num
                    )
            };
            this.ColorBoost = new List<CameraSetting>()
            {
                new CameraSetting(
                    "Base Color Boost",
                    "_baseColorBoost",
                    1f,
                    valueType.num
                    ),
                new CameraSetting(
                    "Base Color Boost Threshold",
                    "_baseColorBoostThreshold",
                    0.0f,
                    valueType.num
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
        public object Value;
        public string PropertyName = "";
        public valueType ValueType = valueType.num;

        public CameraSetting(string friendlyName, string propertyName, object value, valueType type)
        {
            this.FriendlyName = friendlyName;
            this.PropertyName = propertyName;
            this.Value = value;
            this.ValueType = type;
        }
    }
}
