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
    internal class rSettingsFlowCoordinator : FlowCoordinator
    {
        private SiraLog _log;

        private CamManager _cam;
        private ConfigManager _cfg;
        
        private LastResort _spaceResetter;
        private ButtonManager _buttonManager = null!;
        private FlowCoordinator _parentFlowCoordinator = null!;
        private ColorBoostController _boostController;
        private ConfigViewController _cfgController;
        private MainFlowCoordinator _mainFlowCoordinator = null!;
        private NoBloomController _noBloomController;
        private PluginConfig _pluginConfig;
        [Inject]
        protected void Construct(ButtonManager buttonManager, MainFlowCoordinator mainFlowCoordinator,
             ColorBoostController cbController, ConfigViewController configController, NoBloomController nobloom,
            SiraLog log, LastResort resetter, CamManager cam, ConfigManager config, PluginConfig pluginConfig)
        {
            _buttonManager = buttonManager;

            _cam = cam;
            _cfg = config;
            
            _mainFlowCoordinator = mainFlowCoordinator;
            _cfgController = configController;
            _boostController = cbController;
            _noBloomController = nobloom;
            _spaceResetter = resetter;
            _log = log;

            _pluginConfig = pluginConfig;
        }
        
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            
            _log.Debug($"rSettingsFlowCoordinator DidActivate");
            if (firstActivation)
            {
                SetTitle("ReProcessor 2");
                showBackButton = true;
            }

            if (addedToHierarchy)
            {
                if (CamManager.BloomSupported)
                {
                    ProvideInitialViewControllers(_boostController, _cfgController);
                }
                else
                {
                    ProvideInitialViewControllers(_noBloomController);
                }
            }
        }

        
        

        protected void Start()
        {
            _log.Notice($"Loading preset\"{_pluginConfig.Preset}\"");
            _cam.ApplyAll(_cfg.Presets[_pluginConfig.Preset]); //lol, lmao, kek, rofl
            
            
            _buttonManager.WasClicked += ButtonWasClicked;
        }

        protected void OnDestroy()
        {
            _buttonManager.WasClicked -= ButtonWasClicked;
        }

        internal void ButtonWasClicked()
        {
            _parentFlowCoordinator = _mainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            _parentFlowCoordinator.PresentFlowCoordinator(this, animationDirection: ViewController.AnimationDirection.Vertical);
            _spaceResetter.gameObject.SetActive(true);
        }

        public void ShidAndFard()
        {
            _parentFlowCoordinator.DismissFlowCoordinator(this);
        }
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            _spaceResetter.gameObject.SetActive(false);
            ShidAndFard();
        }
        public void Initialize()
        {
            
        }
    }
}