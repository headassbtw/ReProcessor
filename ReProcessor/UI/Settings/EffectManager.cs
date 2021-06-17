using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
//using ReProcessor.Files;
using static ReProcessor.Config;

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.EffectManager.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\EffectManager.bsml")]
    internal class EffectManager : BSMLAutomaticViewController
    {
        [UIAction("bloom-clicc")]
        internal static void BloomClicc()
        {
            rSettingsFlowCoordinator.SwitchMiddleView(1);
        }
        [UIAction("cb-clicc")]
        internal static void CBClicc()
        {
            rSettingsFlowCoordinator.SwitchMiddleView(2);
        }


        [UIAction("#post-parse")]
        internal void PostParse()
        {
            if(GameObject.Find("ReProcessorSpaceHandler") == null)
            {
                GameObject SpaceHandler = new GameObject("ReProcessorSpaceHandler");
                SpaceHandler.AddComponent<GetKeyPress>();
            }
        }
    }
}
