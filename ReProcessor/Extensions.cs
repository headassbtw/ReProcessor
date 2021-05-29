using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine;

namespace ReProcessor
{
    static class Extensions
    {
        internal static T GetPrivateField<T>(this object input, string fieldName)
        {
            Type type = input.GetType();
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo f = type.GetField(fieldName, bindingFlags);
            return (T)f.GetValue(input);
        }
        internal static void SetPrivateField(this object input, string fieldName, object value)
        {
            Type type = input.GetType();
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo f = type.GetField(fieldName, bindingFlags);
            f.SetValue(input, value);
        }
        internal static MainEffectController MainEffectController(this Camera cam)
        {
            return cam.gameObject.GetComponent(typeof(MainEffectController)) as MainEffectController;
        }
        internal static MainEffectContainerSO MainEffectContainerSO(this Camera cam)
        {
            return cam.gameObject.GetComponent(typeof(MainEffectController)).GetPrivateField<MainEffectContainerSO>("_mainEffectContainer");
        }
        internal static object GetCameraSetting(this Camera cam, string fieldName)
        {
            return cam.MainEffectContainerSO().mainEffect.GetPrivateField<object>(fieldName);
        }
        internal static void SetCameraSetting(this Camera cam, string fieldName, object value)
        {
            cam.MainEffectContainerSO().mainEffect.SetPrivateField(fieldName, value);
        }
        internal static void ApplyBloomPreset(this Camera cam, Preset preset)
        {
            cam.SetCameraSetting("_bloomBlendFactor", (System.Single)preset.Bloom.BlendFactor);
            cam.SetCameraSetting("_bloomRadius", (System.Single)preset.Bloom.Radius);
            cam.SetCameraSetting("_bloomIntensity", (System.Single)preset.Bloom.Intensity);
            cam.SetCameraSetting("_downBloomIntensityOffset", (System.Single)preset.Bloom.IntensityOffset);
            cam.SetCameraSetting("_pyramidWeightsParam", (System.Single)preset.Bloom.Weight);
            cam.SetCameraSetting("_alphaWeights", (System.Single)preset.Bloom.AlphaWeights);
        }

    }
}
