using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
//using RuntimeUnityEditor.BSIPA4;
using UnityEngine.Serialization;
using Zenject;
//using ReProcessor.Files;
using static ReProcessor.Config;
using static ReProcessor.PresetExtensions;

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\Views\BloomSettingsView.bsml")]
    class BloomSettingsView : BSMLAutomaticViewController
    {
        private static Preset tmpPreset;

        internal void Awake()
        {
            tmpPreset = Plugin.preset;
            if (tmpPreset == null)
            {
                Plugin.Log.Notice("ASS");
                tmpPreset = new Preset("test", new BloomConfig(), new ColorBoostConfig());
                tmpPreset.Save();

                tmpPreset = Load("test");
                if(tmpPreset == null)
                {
                    Plugin.Log.Notice("FUCK YOU THAT'S WHY");
                }
            }
            
        }

        [UIValue("bloom-en")]
        internal bool Enabled
        {
            get => Plugin.preset.Bloom.Enabled;
            set
            {
                Plugin.preset.Bloom.Enabled = value;
                NotifyPropertyChanged();
            }
        }
        [UIValue("blend-factor")]
        internal float BlendFactor
        {
            get => Plugin.preset.Bloom.BlendFactor;
            set
            {
                Plugin.preset.Bloom.BlendFactor = value;
                Managers.MenuCoreManager.MainCamAccess().SetCameraSetting("_bloomBlendFactor", (System.Single)value);
                NotifyPropertyChanged();
            }
        }
        [UIValue("radius")]
        internal float Radius
        {
            get => Plugin.preset.Bloom.Radius;
            set
            {
                Plugin.preset.Bloom.Radius = value;
                Managers.MenuCoreManager.MainCamAccess().SetCameraSetting("_bloomRadius", (System.Single)value);
                NotifyPropertyChanged();
            }
        }
        [UIValue("intensity")]
        internal float Intensity
        {
            get => Plugin.preset.Bloom.Intensity;
            set
            {
                Plugin.preset.Bloom.Intensity = value;
                Managers.MenuCoreManager.MainCamAccess().SetCameraSetting("_bloomIntensity", (System.Single)value);
                NotifyPropertyChanged();
            }
        }
        [UIValue("intensity-offset")]
        internal float IntensityOffset
        {
            get => Plugin.preset.Bloom.IntensityOffset;
            set
            {
                Plugin.preset.Bloom.IntensityOffset = value;
                Managers.MenuCoreManager.MainCamAccess().SetCameraSetting("_downBloomIntensityOffset", (System.Single)value);
                NotifyPropertyChanged();
            }
        }
        [UIValue("weight")]
        internal float Weight
        {
            get => Plugin.preset.Bloom.Weight;
            set
            {
                Plugin.preset.Bloom.Weight = value;
                Managers.MenuCoreManager.MainCamAccess().SetCameraSetting("_pyramidWeightsParam", (System.Single)value);
                NotifyPropertyChanged();
            }
        }
        [UIValue("alpha-weights")]
        internal float AlphaWeights
        {
            get => Plugin.preset.Bloom.AlphaWeights;
            set
            {
                Plugin.preset.Bloom.AlphaWeights = value;
                Managers.MenuCoreManager.MainCamAccess().SetCameraSetting("_alphaWeights", (System.Single)value);
                NotifyPropertyChanged();
            }
        }
        [UIAction("cancel-button")]
        internal void Cancel()
        {
            Plugin.preset = Load(Plugin.PresetName);
            Managers.MenuCoreManager.MainCamAccess().ApplyBloomPreset(Plugin.preset);
            #region fuckin gamer
            BlendFactor = Plugin.preset.Bloom.BlendFactor;
            Radius = Plugin.preset.Bloom.Radius;
            Intensity = Plugin.preset.Bloom.Intensity;
            IntensityOffset = Plugin.preset.Bloom.IntensityOffset;
            Weight = Plugin.preset.Bloom.Weight;
            AlphaWeights = Plugin.preset.Bloom.AlphaWeights;
            #endregion
            rSettingsFlowCoordinator.SwitchMiddleView();
        }
        [UIAction("apply-button")]
        internal void Apply()
        {
            Plugin.preset.Save();
        }

    }
}
