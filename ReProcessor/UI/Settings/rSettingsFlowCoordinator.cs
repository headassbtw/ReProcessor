using BeatSaberMarkupLanguage;
using HMUI;
using IPA.Utilities;
using SiraUtil.Tools;
using Zenject;

namespace ReProcessor.UI
{
    internal class rSettingsFlowCoordinator : FlowCoordinator, IInitializable
    {
        private ButtonManager _buttonManager = null!;
        private FlowCoordinator _parentFlowCoordinator = null!;
        private MainFlowCoordinator _mainFlowCoordinator = null!;
        private EffectManager _effectManager = null!;
        private OverallSettingsView _overallSettingsView = null!;
        private BloomSettingsView2 _bloomSettingsView = null!;
        private BaseColorBoostViewController _baseColorBoostView = null!;
        internal static rSettingsFlowCoordinator Instance;
        public void Initialize() { Instance = this; }

        private static readonly FieldAccessor<SelectLevelCategoryViewController, IconSegmentedControl>.Accessor SegmentedControl = FieldAccessor<SelectLevelCategoryViewController, IconSegmentedControl>.GetAccessor("_levelFilterCategoryIconSegmentedControl");
        private static readonly FieldAccessor<SelectLevelCategoryViewController, SelectLevelCategoryViewController.LevelCategoryInfo[]>.Accessor Categories = FieldAccessor<SelectLevelCategoryViewController, SelectLevelCategoryViewController.LevelCategoryInfo[]>.GetAccessor("_levelCategoryInfos");



        [Inject]
        protected void Construct(ButtonManager buttonManager, MainFlowCoordinator mainFlowCoordinator, BloomSettingsView2 bloomSettingsView, EffectManager effectManager, OverallSettingsView overallSettingsView, BaseColorBoostViewController baseColorBoostViewController)
        {
            _buttonManager = buttonManager;
            _mainFlowCoordinator = mainFlowCoordinator;
            _bloomSettingsView = bloomSettingsView;
            _effectManager = effectManager;
            _overallSettingsView = overallSettingsView;
            _baseColorBoostView = baseColorBoostViewController;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("ReProcessor Effects");
                showBackButton = true;
            }
            if (addedToHierarchy)
            {
                ViewController view = _effectManager;
                ProvideInitialViewControllers(view, null, _overallSettingsView);
                
            }
        }

        internal static void SwitchMiddleView(int effectIndex = 0)
        {
            ViewController view;
            switch (effectIndex)
            {
                case 0:
                    Instance.showBackButton = true;
                    Instance.SetTitle("ReProcessor Effects");
                    view = Instance._effectManager;
                    Instance.ReplaceTopViewController(view);
                    Instance.SetRightScreenViewController(Instance._overallSettingsView, ViewController.AnimationType.In);
                    break;
                case 1:
                    Instance.showBackButton = false;
                    Instance.SetTitle("Bloom");
                    view = Instance._bloomSettingsView;
                    Instance.ReplaceTopViewController(view);
                    Instance.SetRightScreenViewController(null, ViewController.AnimationType.Out);
                    break;
                case 2:
                    Instance.showBackButton = false;
                    Instance.SetTitle("Color Boost");
                    view = Instance._baseColorBoostView;
                    Instance.ReplaceTopViewController(view);
                    Instance.SetRightScreenViewController(null, ViewController.AnimationType.Out);
                    break;
            }


            
        }


        internal void ToggleBackButton(bool value)
        {
            showBackButton = value;
        }


        internal void ButtonWasClicked()
        {
            Instance.SetRightScreenViewController(null, ViewController.AnimationType.Out);
            _parentFlowCoordinator = _mainFlowCoordinator.YoungestChildFlowCoordinatorOrSelf();
            _parentFlowCoordinator.PresentFlowCoordinator(this, animationDirection: ViewController.AnimationDirection.Vertical);
        }
        protected void Start()
        {
            _buttonManager.WasClicked += ButtonWasClicked;
        }
        protected void OnDestroy()
        {
            _buttonManager.WasClicked -= ButtonWasClicked;
        }
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            _parentFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}
