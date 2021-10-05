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
using BeatSaberMarkupLanguage.Components;

//we'll fix you later oki doki?

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsViewExperimental.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\BloomSettingsViewExperimental.bsml")]
    internal class BaseColorBoostViewController : BloomSettingsView2
    {
        public override List<CameraSetting> GetDefaults()
        {
            return Defaults.ColorBoostDefaults;
        }

        public override List<CameraSetting> GetSettings()
        {
            return Plugin.preset.ColorBoost;
        }
    }
}