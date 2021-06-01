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
                Managers.MenuCoreManager.MainCamAccess().SetCameraSetting("_baseColorBoost", (System.Single)value);
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
                Managers.MenuCoreManager.MainCamAccess().SetCameraSetting("_baseColorBoostThreshold", (System.Single)value);
                NotifyPropertyChanged();
            }
        }

        [UIAction("revert")]
        internal void Revert()
        {
            Plugin.preset.ColorBoost = new ColorBoostConfig();
            BaseColorBoostThreshold = Plugin.preset.ColorBoost.BoostThreshold;
            BaseColorBoost = Plugin.preset.ColorBoost.Boost;
            Plugin.preset.Save();
            Managers.MenuCoreManager.MainCamAccess().ApplyBloomPreset(Plugin.preset);
        }

        [UIAction("cancel-button")]
        internal void Cancel()
        {
            Plugin.preset = Load(Plugin.PresetName);
            Managers.MenuCoreManager.MainCamAccess().ApplyBloomPreset(Plugin.preset);
            #region fuckin gamer
            BaseColorBoostThreshold = Plugin.preset.ColorBoost.BoostThreshold;
            BaseColorBoost = Plugin.preset.ColorBoost.Boost;
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