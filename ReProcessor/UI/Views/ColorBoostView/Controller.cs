using System;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.Tags;
using BeatSaberMarkupLanguage.TypeHandlers;
using BeatSaberMarkupLanguage.ViewControllers;
using ReProcessor.Configuration;
using ReProcessor.Managers;
using Zenject;

namespace ReProcessor.UI.Views.TestView
{
    [ViewDefinition("ReProcessor.UI.Views.ColorBoostView.View.bsml")]
    [HotReload(RelativePathToLayout = @"View.bsml")]
    public class ColorBoostController : BSMLAutomaticViewController, IInitializable
    {
        
        public void Initialize(){}
        private rSettingsFlowCoordinator _settings;
        private CamManager _camManager;
        private ConfigManager _cfgManager;
        [Inject]
        protected void Construct(rSettingsFlowCoordinator settings, CamManager camManager, ConfigManager cfgManager)
        {
            _settings = settings;
            _camManager = camManager;
            _cfgManager = cfgManager;
            
        }

        [UIValue("_bcb")]
        private Single BaseColorBoost
        {
            get => Convert.ToSingle(_cfgManager.TempPreset.ColorBoost["Base Color Boost"].Value);
            set => _cfgManager.TempPreset.ColorBoost["Base Color Boost"].Value = Convert.ToSingle(value);
        }
        [UIValue("_bcbT")]
        private Single BaseColorBoostThreshold
        {
            get => Convert.ToSingle(_cfgManager.TempPreset.ColorBoost["Base Color Boost Threshold"].Value);
            set => _cfgManager.TempPreset.ColorBoost["Base Color Boost Threshold"].Value = Convert.ToSingle(value);
        }
        
        [UIComponent("cb-items")]
        private ScrollableSettingsContainerTag Settings;
        
        [UIAction("Test")]
        void Test()
        {
            NotifyPropertyChanged();
        }

        [UIAction("Apply")]
        void Apply()
        {
            _cfgManager.CurrentPreset = _cfgManager.TempPreset;
            _camManager.ApplyAll(_cfgManager.CurrentPreset);
        }
        
        [UIAction("Back")]
        void GoBack()
        {
            
            _camManager.ApplyAll(_cfgManager.CurrentPreset);
            _cfgManager.TempPreset = _cfgManager.CurrentPreset;
            _settings.RevertMiddleController();
        }
    }
}