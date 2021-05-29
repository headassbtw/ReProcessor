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

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BaseColorBoostView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\Views\BaseColorBoostView.bsml")]
    public class BaseColorBoostViewController : BSMLAutomaticViewController
    {


        [UIValue("boost")]
        internal System.Single BaseColorBoost
        {
            get => Plugin.preset.ColorBoost.Boost;
            set
            {
                Plugin.preset.ColorBoost.Boost = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("boost-threshold")]
        internal System.Single BaseColorBoostThreshold
        {
            get => Plugin.preset.ColorBoost.BoostThreshold;
            set
            {
                Plugin.preset.ColorBoost.BoostThreshold = value;
                NotifyPropertyChanged();
            }
        }


        [UIAction("cancel-button")]
        internal void Cancel() => rSettingsFlowCoordinator.SwitchMiddleView();
        [UIAction("apply-button")]
        internal void Apply()
        {
            Plugin.preset.Save();
        }
    }
}