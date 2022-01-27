using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using Zenject;

namespace ReProcessor.UI.Views.NoBloomError
{
    [ViewDefinition("ReProcessor.UI.Views.NoBloomError.View.bsml")]
    [HotReload(RelativePathToLayout = @"View.bsml")]
    public class NoBloomController : BSMLAutomaticViewController, IInitializable
    {
        public void Initialize() {}
    }
}