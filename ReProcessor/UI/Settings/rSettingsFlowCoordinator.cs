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
        private BloomSettingsView _bloomSettingsView = null!;
        private BaseColorBoostViewController _baseColorBoostView = null!;
        internal static rSettingsFlowCoordinator Instance;
        public void Initialize() { Instance = this; }

        private static readonly FieldAccessor<SelectLevelCategoryViewController, IconSegmentedControl>.Accessor SegmentedControl = FieldAccessor<SelectLevelCategoryViewController, IconSegmentedControl>.GetAccessor("_levelFilterCategoryIconSegmentedControl");
        private static readonly FieldAccessor<SelectLevelCategoryViewController, SelectLevelCategoryViewController.LevelCategoryInfo[]>.Accessor Categories = FieldAccessor<SelectLevelCategoryViewController, SelectLevelCategoryViewController.LevelCategoryInfo[]>.GetAccessor("_levelCategoryInfos");



        [Inject]
        protected void Construct(ButtonManager buttonManager, MainFlowCoordinator mainFlowCoordinator, BloomSettingsView bloomSettingsView, EffectManager effectManager, BaseColorBoostViewController baseColorBoostViewController)
        {
            _buttonManager = buttonManager;
            _mainFlowCoordinator = mainFlowCoordinator;
            _bloomSettingsView = bloomSettingsView;
            _effectManager = effectManager;
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
                ProvideInitialViewControllers(view);
                
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
                    break;
                case 1:
                    Instance.showBackButton = false;
                    Instance.SetTitle("Bloom");
                    view = Instance._bloomSettingsView;
                    Instance.ReplaceTopViewController(view);
                    break;
                case 2:
                    Instance.showBackButton = false;
                    Instance.SetTitle("Color Boost");
                    view = Instance._baseColorBoostView;
                    Instance.ReplaceTopViewController(view);
                    break;
            }


            
        }


        internal void ToggleBackButton(bool value)
        {
            showBackButton = value;
        }


        internal void ButtonWasClicked()
        {
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
