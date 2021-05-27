using System;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
using Zenject;

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsView.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\Views\BloomSettingsView.bsml")]
    class BloomSettingsView : BSMLAutomaticViewController
    {

    }
}
