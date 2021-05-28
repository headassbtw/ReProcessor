using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using RuntimeUnityEditor.BSIPA4;
using UnityEngine.Serialization;
using Zenject;

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\Views\BloomSettingsView.bsml")]
    class BloomSettingsView : BSMLAutomaticViewController
    {
        void a()
        {
            
        }
        
        [UIValue("Factor Value")]
        public float Enabled
        {
            get => Plugin.Config.Enabled;
            set => Plugin.Config.Enabled = value;
        }
    }
}
