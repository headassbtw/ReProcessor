using ReProcessor.Configuration;
using static ReProcessor.Extensions.Reflection;
namespace ReProcessor.Extensions
{
    public static class Presets
    {

        public static void ApplyProps(this UnityEngine.Camera cam, Preset preset)
        {
            foreach (var prop in preset.Props)
            {
                cam.SetCameraSetting(prop.Value);
            }
        }
    }
}