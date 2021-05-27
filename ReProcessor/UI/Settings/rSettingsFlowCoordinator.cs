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
        private BloomSettingsView _bloomSettingsView = null!;

        public void Initialize() { }

        private static readonly FieldAccessor<SelectLevelCategoryViewController, IconSegmentedControl>.Accessor SegmentedControl = FieldAccessor<SelectLevelCategoryViewController, IconSegmentedControl>.GetAccessor("_levelFilterCategoryIconSegmentedControl");
        private static readonly FieldAccessor<SelectLevelCategoryViewController, SelectLevelCategoryViewController.LevelCategoryInfo[]>.Accessor Categories = FieldAccessor<SelectLevelCategoryViewController, SelectLevelCategoryViewController.LevelCategoryInfo[]>.GetAccessor("_levelCategoryInfos");



        [Inject]
        protected void Construct(ButtonManager buttonManager, MainFlowCoordinator mainFlowCoordinator, BloomSettingsView bloomSettingsView)
        {
            _buttonManager = buttonManager;
            _mainFlowCoordinator = mainFlowCoordinator;
            _bloomSettingsView = bloomSettingsView;
        }

        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("ReProcessor Settings");
                showBackButton = true;
            }
            if (addedToHierarchy)
            {
                ViewController view = _bloomSettingsView;
                ProvideInitialViewControllers(view);
            }
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
