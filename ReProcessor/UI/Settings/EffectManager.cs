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
    [HotReload(RelativePathToLayout = @"..\Views\EffectManager.bsml")]
    internal class EffectManager : BSMLAutomaticViewController
    {
        [UIAction("effect-selected")]
        internal static void SpinSelected(TableView sender, EffectListObject row)
        {
            rSettingsFlowCoordinator.SwitchMiddleView(row.effectIndex + 1);
        }

        [UIComponent("effect-list")]
        internal CustomCellListTableData EffectList;
        [UIValue("effects")]
        public List<object> effectsList = new List<object>();

        public class EffectListObject
        {
            internal int effectIndex = 0;

            [UIValue("effect-label")]
            private string Label = "Bloom";

            [UIComponent("bg")]
            private ImageView background = null;

            [UIValue("col")]
            private UnityEngine.Color bGCol;

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
                {
                    x.a = highlighted ? 1.0f : 0.6f;

                    x.r = highlighted ? 0.4f : 0.1f;
                    x.g = highlighted ? 0.4f : 0.1f;
                    x.b = highlighted ? 1.0f : 0.1f;
                }

                bGCol = x;
                background.color = x;
            }
        }
        [UIAction("#post-parse")]
        internal void PostParse()
        {
            EffectList.data.Clear();
            EffectList.data.Add(new EffectListObject("Bloom", 0));
            EffectList.data.Add(new EffectListObject("Color Boost", 1));
            if(Plugin.preset.User.Count > 0)
                EffectList.data.Add(new EffectListObject("User", 2));
            EffectList.tableView.ReloadData();
        }
    }
}
