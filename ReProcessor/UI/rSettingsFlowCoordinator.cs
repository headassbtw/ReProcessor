using System;
using BeatSaberMarkupLanguage;
using HMUI;
using ReProcessor.UI.Views.TestView;
using SiraUtil.Logging;
using Zenject;

namespace ReProcessor.UI
{
    public class rSettingsFlowCoordinator : FlowCoordinator, IInitializable
    {
        private SiraLog _log;
        private LastResort _spaceResetter;
        private ButtonManager _buttonManager = null!;
        private FlowCoordinator _parentFlowCoordinator = null!;
        private MainViewController _mainController;
        private ColorBoostController _boostController;
        private ConfigViewController _cfgController;
        private MainFlowCoordinator _mainFlowCoordinator = null!;
        [Inject]
        protected void Construct(ButtonManager buttonManager, MainFlowCoordinator mainFlowCoordinator,
            MainViewController mainController, ColorBoostController cbController, ConfigViewController configController,
            SiraLog log, LastResort resetter)
        {
            _buttonManager = buttonManager;
            _mainFlowCoordinator = mainFlowCoordinator;
            _mainController = mainController;
            _cfgController = configController;
            _boostController = cbController;
            _spaceResetter = resetter;
            _log = log;
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
                ViewController view = _mainController;
                ProvideInitialViewControllers(view, null, _cfgController);
            }
            
        }

        internal void RevertMiddleController()
        {
            showBackButton = true;
            ReplaceTopViewController(_mainController);
        }
        internal void SetMiddleController(int controller)
        {
            
            showBackButton = false;
            switch (controller)
            {
                case 0:
                    ReplaceTopViewController(_mainController);
                    break;
                case 1: //bloom
                    ReplaceTopViewController(_mainController);
                break;
                case 2: //color boost
                    ReplaceTopViewController(_boostController);
                break;
            }
        }
        

        protected void Start()
        {
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
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            _spaceResetter.gameObject.SetActive(false);
            _parentFlowCoordinator.DismissFlowCoordinator(this);
        }
        public void Initialize()
        {
            
        }
    }
}