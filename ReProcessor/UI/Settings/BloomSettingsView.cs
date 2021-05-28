using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
//using RuntimeUnityEditor.BSIPA4;
using UnityEngine.Serialization;
using Zenject;
using ReProcessor.Files;

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\Views\BloomSettingsView.bsml")]
    class BloomSettingsView : BSMLAutomaticViewController
    {
        private BloomConfig tempConfig = Plugin.Config.preset.Bloom;
        
        
        [UIValue("bloom-en")]
        public bool Enabled
        {
            get => Plugin.Config.preset.Bloom.Enabled;
            set => Plugin.Config.preset.Bloom.Enabled = value;
        }
        [UIValue("blend-factor")]
        internal float BlendFactor
        {
            get => tempConfig.BlendFactor;
            set
            {
                tempConfig.BlendFactor = value;
                Managers.MenuCoreManager.MainCamAccess().SetCameraSetting("_bloomBlendFactor", (System.Single)value);
                NotifyPropertyChanged();
            }

        }
        [UIAction("apply-button")]
        internal void Apply()
        {
            var tc = Plugin.Config;
            tc.preset.Bloom = tempConfig;
            Plugin.ApplyConfig(tc);
        }

    }
}
