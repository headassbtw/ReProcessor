using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;

namespace ReProcessor.UI.Views.NoBloomError
{
    [ViewDefinition("ReProcessor.UI.Views.NoBloomError.View.bsml")]
    [HotReload(RelativePathToLayout = @"View.bsml")]
    public class NoBloomController : BSMLAutomaticViewController
    {
    }
}