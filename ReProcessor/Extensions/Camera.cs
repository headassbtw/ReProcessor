using ReProcessor.Configuration;
using static ReProcessor.Extensions.Reflection;
namespace ReProcessor.Extensions
{
    public static class Presets
    {

        public static void ApplyBloom(this UnityEngine.Camera cam, Preset preset)
        {
            foreach (var prop in preset.Bloom)
            {
                cam.SetCameraSetting(prop.Value);
            }
        }
        public static void ApplyColorBoost(this UnityEngine.Camera cam, Preset preset)
        {
            foreach (var prop in preset.ColorBoost)
            {
                cam.SetCameraSetting(prop.Value);
            }
        }
        public static void ApplyPreset(this UnityEngine.Camera cam, Preset preset)
        {
            
        }
    }
}