using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
//using RuntimeUnityEditor.BSIPA4;
using UnityEngine.Serialization;
using Zenject;

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\Views\BloomSettingsView.bsml")]
    class BloomSettingsView : BSMLAutomaticViewController
    {
        private Config tempConfig;
        
        
        [UIValue("bloom-en")]
        public bool Enabled
        {
            get => Plugin.Config.Enabled;
            set => Plugin.Config.Enabled = value;
        }
        [UIValue("blend-factor")]
        float BlendFactor
        {
            get => tempConfig.BloomBlendFactor;
            set
            {
                tempConfig.BloomBlendFactor = value;
                Managers.MenuCoreManager._mainCamera.SetPrivateField("_bloomBlendFactor", value);
                NotifyPropertyChanged();
            }

        }
        [UIAction("apply-button")]
        internal void Apply()
        {
            Plugin.Config = tempConfig;
        }

    }
}
