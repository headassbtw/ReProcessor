using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using SiraUtil.Logging;
using Zenject;

namespace ReProcessor.UI.Views.TestView
{
    [ViewDefinition("ReProcessor.UI.Views.MainView.View.bsml")]
    [HotReload(RelativePathToLayout = @"View.bsml")]
    public class MainViewController : BSMLAutomaticViewController, IInitializable
    {
        public void Initialize(){}
        private rSettingsFlowCoordinator _settings;
        [Inject]
        protected void Construct(rSettingsFlowCoordinator settings)
        {
            _settings = settings;
        }
        
        
        [UIAction("ColorBoost")]
        void ColorBoost()
        {
            _settings.SetMiddleController(2);
        }
        [UIAction("Back")]
        void Back()
        {
            _settings.RevertMiddleController();
        }
    }
}