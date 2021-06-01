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
    [ViewDefinition("ReProcessor.UI.Views.OverallSettings.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\Views\OverallSettings.bsml")]
    internal class OverallSettingsView : BSMLAutomaticViewController
    {
        private Config tempConfig = Plugin.Config;

        

        [UIValue("min-string")]
        internal string MinString
        {
            get => tempConfig.MinAmountIncrease.ToString();
            set
            {
                tempConfig.MinAmountIncrease = float.Parse(value);
                NotifyPropertyChanged();
            }
        }

        [UIValue("max-string")]
        internal string MaxString
        {
            get => tempConfig.MaxAmountIncrease.ToString();
            set
            {
                tempConfig.MaxAmountIncrease = float.Parse(value);
                NotifyPropertyChanged();
            }
        }


        [UIAction("revert")]
        internal void Revert()
        {
            tempConfig = new Config();
            MaxString = tempConfig.MaxAmountIncrease.ToString();
            MinString = tempConfig.MinAmountIncrease.ToString();
            NotifyPropertyChanged();
            Plugin.Config = tempConfig;
        }

        [UIAction("cancel-button")]
        internal void Cancel()
        {
            tempConfig = Plugin.Config;
            MaxString = tempConfig.MaxAmountIncrease.ToString();
            MinString = tempConfig.MinAmountIncrease.ToString();
            NotifyPropertyChanged();
        }
        [UIAction("apply-button")]
        internal void Apply()
        {
            Plugin.Config = tempConfig;
        }
    }
}
