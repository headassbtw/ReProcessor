using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using UnityEngine.Serialization;
using Zenject;
//using ReProcessor.Files;
using static ReProcessor.Config;

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.EffectManager.bsml")]
    [HotReload(RelativePathToLayout = @"..\UI\Views\EffectManager.bsml")]
    class EffectManager : BSMLAutomaticViewController
    {


        [UIComponent("effect-list")]
        internal CustomCellListTableData EffectList;
        [UIValue("effects")]
        public List<object> effectsList = new List<object>();

        public class EffectListObject
        {
            int effectIndex = 0;

            [UIValue("effect-label")]
            private string Label = "Bloom";

            [UIComponent("bg")]
            private ImageView background;

            [UIAction("effect-onclick")]
            private void EffectClicked()
            {
                switch (effectIndex)
                {
                    case 0:
                        rSettingsFlowCoordinator.SwitchMiddleView(1);
                        break;
                    case 1:
                        rSettingsFlowCoordinator.SwitchMiddleView(2);
                        break;
                }
            }

            public EffectListObject(string name, int index = 0)
            {
                this.Label = name;
                this.effectIndex = index;
            }
            [UIAction("refresh-visuals")]
            public void Refresh(bool selected, bool highlighted)
            {
                var x = new UnityEngine.Color(0, 0, 0, 0.45f);

                if (selected || highlighted)
                    x.a = selected ? 0.9f : 0.6f;

                background.color = x;
            }
        }
        [UIAction("#post-parse")]
        internal void PostParse()
        {
            EffectList.data.Clear();
            EffectList.data.Add(new EffectListObject("Bloom", 0));
            //EffectList.data.Add(new EffectListObject("Color Boost", 1));
            EffectList.tableView.ReloadData();
        }
    }
}
