using System;
using BeatSaberMarkupLanguage;
using HMUI;
using ReProcessor.Configuration;
using ReProcessor.Managers;
using ReProcessor.UI.Views.ColorBoostView;
using ReProcessor.UI.Views.ConfigManager;
using ReProcessor.UI.Views.NoBloomError;
using SiraUtil.Logging;
using Zenject;

namespace ReProcessor.UI
{
    internal class ReSettingsFlowCoordinator : FlowCoordinator, IInitializable, IDisposable
    {
        private SiraLog _log = null!;

        private CamManager _cam = null!;
        private ConfigManager _cfg = null!;

        private LastResort _spaceResetter = null!;
        private ButtonManager _buttonManager = null!;
        private FlowCoordinator _parentFlowCoordinator = null!;
        private LazyInject<ColorBoostController> _boostControllerLazy = null!;
        private LazyInject<ConfigViewController> _cfgControllerLazy = null!;
        private MainFlowCoordinator _mainFlowCoordinator = null!;
        private LazyInject<NoBloomController> _noBloomControllerLazy = null!;
        private PluginConfig _pluginConfig = null!;

        [Inject]
        protected void Construct(ButtonManager buttonManager, MainFlowCoordinator mainFlowCoordinator, LazyInject<ColorBoostController> cbController, LazyInject<ConfigViewController> configController,
            LazyInject<NoBloomController> nobloom, SiraLog log, LastResort resetter, CamManager cam, ConfigManager config, PluginConfig pluginConfig)
        {
            _log = log;
            _buttonManager = buttonManager;

            _cam = cam;
            _cfg = config;

            _mainFlowCoordinator = mainFlowCoordinator;
            _cfgControllerLazy = configController;
            _boostControllerLazy = cbController;
            _noBloomControllerLazy = nobloom;
            _spaceResetter = resetter;
            

            _pluginConfig = pluginConfig;
            
            _log.Notice($"Loading preset\"{_pluginConfig.Preset}\"");
            _cam.ApplyAll(_cfg.Presets[_pluginConfig.Preset].Props); //lol, lmao, kek, rofl

            _buttonManager.WasClicked += ButtonWasClicked;
        }
        
        public void Initialize()
        {
            _log.Notice("rSettingsFlowCoordinator initialized");
        }

        public void Dispose()
        {
            _buttonManager.WasClicked -= ButtonWasClicked;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            _log.Debug("SettingsFlowCoordinator DidActivate");
            if (firstActivation)
            {
                SetTitle("ReProcessor 2");
                showBackButton = true;
                
                if (_cam.BloomSupported)
                {
                    ProvideInitialViewControllers(_boostControllerLazy.Value, _cfgControllerLazy.Value);
                }
                else
                {
                    ProvideInitialViewControllers(_noBloomControllerLazy.Value);
                }
            }
        }

        internal void ButtonWasClicked()
        {
            _parentFlowCoordinator = _mainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            _parentFlowCoordinator.PresentFlowCoordinator(this, animationDirection: ViewController.AnimationDirection.Vertical);
            _spaceResetter.gameObject.SetActive(true);
        }

        protected override void BackButtonWasPressed(ViewController _)
        {
            _spaceResetter.gameObject.SetActive(false);
            _parentFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}