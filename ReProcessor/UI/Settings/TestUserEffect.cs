using BeatSaberMarkupLanguage.Attributes;
using ReProcessor.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsViewExperimental.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\BloomSettingsViewExperimental.bsml")]
    class TestUserEffect : BloomSettingsView2
    {
        public override List<CameraSetting> GetSettings()
        {
            return Plugin.preset.User;
        }
        public override List<CameraSetting> GetDefaults()
        {
            return new List<CameraSetting>();
        }
    }
}
