using BeatSaberMarkupLanguage;
using HMUI;
using IPA.Utilities;
using SiraUtil.Tools;
using System.Threading.Tasks;
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
        private TestUserEffect _testUserEffect = null!;
        private int exiting = 0;
        internal static rSettingsFlowCoordinator Instance;
        public void Initialize() { exiting = 0;  Instance = this; }

        private static readonly FieldAccessor<SelectLevelCategoryViewController, IconSegmentedControl>.Accessor SegmentedControl = FieldAccessor<SelectLevelCategoryViewController, IconSegmentedControl>.GetAccessor("_levelFilterCategoryIconSegmentedControl");
        private static readonly FieldAccessor<SelectLevelCategoryViewController, SelectLevelCategoryViewController.LevelCategoryInfo[]>.Accessor Categories = FieldAccessor<SelectLevelCategoryViewController, SelectLevelCategoryViewController.LevelCategoryInfo[]>.GetAccessor("_levelCategoryInfos");



        [Inject]
        protected void Construct(ButtonManager buttonManager, MainFlowCoordinator mainFlowCoordinator, BloomSettingsView bloomSettingsView, EffectManager effectManager, OverallSettingsView overallSettingsView, BaseColorBoostViewController baseColorBoostViewController, TestUserEffect testUserEffect)
        {
            _buttonManager = buttonManager;
            _mainFlowCoordinator = mainFlowCoordinator;
            _bloomSettingsView = bloomSettingsView;
            _effectManager = effectManager;
            _overallSettingsView = overallSettingsView;
            _baseColorBoostView = baseColorBoostViewController;
            _testUserEffect = testUserEffect;
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
                ProvideInitialViewControllers(view, _overallSettingsView);
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
                    Instance.SetLeftScreenViewController(Instance._overallSettingsView, ViewController.AnimationType.In);
                    break;
                case 1:
                    Instance.showBackButton = false;
                    Instance.SetTitle("Bloom");
                    view = Instance._bloomSettingsView;
                    Instance.ReplaceTopViewController(view);
                    Instance.SetLeftScreenViewController(null, ViewController.AnimationType.Out);
                    break;
                case 2:
                    Instance.showBackButton = false;
                    Instance.SetTitle("Color Boost");
                    view = Instance._baseColorBoostView;
                    Instance.ReplaceTopViewController(view);
                    Instance.SetLeftScreenViewController(null, ViewController.AnimationType.Out);
                    break;
                case 3:
                    Instance.showBackButton = false;
                    Instance.SetTitle("User Effects");
                    view = Instance._testUserEffect;
                    Instance.ReplaceTopViewController(view);
                    Instance.SetLeftScreenViewController(null, ViewController.AnimationType.Out);
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
            //Instance.SetLeftScreenViewController(null, ViewController.AnimationType.Out);
            _buttonManager.WasClicked -= ButtonWasClicked;
        }
        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            //Instance.SetLeftScreenViewController(null, ViewController.AnimationType.Out);
            _parentFlowCoordinator.DismissFlowCoordinator(Instance);
        }

        protected override void TransitionDidFinish()
        {

        }
    }
}
