using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
//using RuntimeUnityEditor.BSIPA4;
using UnityEngine.Serialization;
using Zenject;
using ReProcessor.Files;
using static ReProcessor.Config;
using static ReProcessor.PresetExtensions;
/*
namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsView.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\BloomSettingsView.bsml")]
    internal class BloomSettingsView : BSMLAutomaticViewController
    {
        private static Preset tmpPreset;

        internal void Awake()
        {
            tmpPreset = Plugin.preset;
            if (tmpPreset == null)
            {
                tmpPreset = new Preset(Plugin.PresetName);
                tmpPreset.Save();

                tmpPreset = Load(Plugin.PresetName);
                if(tmpPreset == null)
                {
                    Plugin.Log.Notice("something happened and i do not know why, tell headass line 32 sent ya");
                }
            }
            
        }
        [UIValue("max-glow-val")]
        internal float MaxGlowVal
        {
            get => Plugin.Config.MaxAmountIncrease;
            set
            {
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
        [UIAction("revert")]
        internal void Revert()
        {
            Plugin.preset.Bloom = new BloomConfig();
            BlendFactor = Plugin.preset.Bloom.BlendFactor;
            Radius = Plugin.preset.Bloom.Radius;
            Intensity = Plugin.preset.Bloom.Intensity;
            IntensityOffset = Plugin.preset.Bloom.IntensityOffset;
            Weight = Plugin.preset.Bloom.Weight;
            AlphaWeights = Plugin.preset.Bloom.AlphaWeights;
            Plugin.preset.Save();
            Managers.MenuCoreManager.MainCamAccess().ApplyBloomPreset(Plugin.preset);
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
}*/
