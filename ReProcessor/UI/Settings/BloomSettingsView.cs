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

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsViewExperimental.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\BloomSettingsViewExperimental.bsml")]
    internal class BloomSettingsView : BloomSettingsView2
    {
        public override List<CameraSetting> GetDefaults()
        {
            return Defaults.BloomDefaults;
        }

        public override List<CameraSetting> GetSettings()
        {
            return Plugin.preset.Bloom;
        }
    }
}
