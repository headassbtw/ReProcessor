using System;
using System.Collections.Generic;

namespace ReProcessor.Configuration
{
    public class CameraSetting
    {
        public string PropertyName;
        public Type ValueType;
        public object Value;

        public CameraSetting(string propertyName, object value,Type valueType)
        {
            PropertyName = propertyName;
            ValueType = valueType;
            Value = value;
        }
    }

    public class Defaults
    {
        public static Dictionary<string,CameraSetting> DefaultBloom()
        {
            Dictionary<string,CameraSetting> rtn = new();
            rtn.Add("Radius",new CameraSetting("_bloomRadius",5f,typeof(System.Single)));
            rtn.Add("Blend Factor",new CameraSetting("_bloomBlendFactor",0.3f,typeof(System.Single)));
            rtn.Add("Intensity",new CameraSetting("_bloomIntensity",1f,typeof(System.Single)));
            rtn.Add("Intensity Offset",new CameraSetting("_downBloomIntensityOffset",1f,typeof(System.Single)));
            rtn.Add("Weight",new CameraSetting("_pyramidWeightsParam",1f,typeof(System.Single)));
            rtn.Add("Alpha Weights",new CameraSetting("_alphaWeights",4f,typeof(System.Single)));
            rtn.Add("Pre Filter Pass",new CameraSetting("_preFilterPass","Prefilter13",typeof(PyramidBloomRendererSO.Pass)));
            rtn.Add("Downsample Pass",new CameraSetting("_downsamplePass","Downsample13",typeof(PyramidBloomRendererSO.Pass)));
            rtn.Add("Upsample Pass",new CameraSetting("_upsamplePass","UpsampleTent",typeof(PyramidBloomRendererSO.Pass)));
            rtn.Add("Final Upsample Pass",new CameraSetting("_finalUpsamplePass","UpsampleTent",typeof(PyramidBloomRendererSO.Pass)));
            return rtn;
        }

        public static Dictionary<string,CameraSetting> ColorBoost()
        {
            Dictionary<string,CameraSetting> rtn = new();
            rtn.Add("Base Color Boost",new CameraSetting("_baseColorBoost",1f,typeof(System.Single)));
            rtn.Add("Base Color Boost Threshold",new CameraSetting("_baseColorBoostThreshold",0.0f,typeof(System.Single)));
            return rtn;
        }
    }
    public class Preset
    {
        public string Name;

        public Dictionary<string,CameraSetting> Bloom;
        public Dictionary<string,CameraSetting> ColorBoost;

        public Preset(string name = "Default")
        {
            Name = name;
            Bloom = Defaults.DefaultBloom();
            ColorBoost = Defaults.ColorBoost();
        }
    }
}