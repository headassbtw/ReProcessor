using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
//using RuntimeUnityEditor.BSIPA4;
using UnityEngine.Serialization;
using Zenject;
using ReProcessor.Files;
using static ReProcessor.Config;
using static ReProcessor.PresetExtensions;

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BloomSettingsViewExperimental.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\BloomSettingsViewExperimental.bsml")]
    internal class BloomSettingsView2 : BSMLAutomaticViewController
    {
        internal static BloomSettingsView2 Instance;
        private static Preset tmpPreset;

        internal void Awake()
        {
            tmpPreset = Plugin.preset;
            if (tmpPreset == null)
            {
                tmpPreset = new Preset(Plugin.PresetName);
                tmpPreset.Save();

                tmpPreset = Load(Plugin.PresetName);
                if(tmpPreset == null)
                {
                    Plugin.Log.Notice("something happened and i do not know why, tell headass line 32 sent ya");
                }
            }
            
        }

        [UIComponent("setting-list")]
        internal CustomCellListTableData SettingList;
        [UIValue("settings")]
        public List<object> settingsList = new List<object>();

        public class EffectListObject
        {
            internal CameraSetting setting;
            
            internal static float SliderValue = 0.0f;

            [UIValue("label")] private string Label = "";
            [UIValue("increment")] private float Increment = 0.05f;
            [UIValue("min")] private float Min = Plugin.Config.MinAmountIncrease;
            [UIValue("max")] private float Max = Plugin.Config.MaxAmountIncrease;

            [UIValue("value")]
            private float Value
            {
                get => float.Parse(setting.Value.ToString());
                set
                {
                    Instance.NotifyPropertyChanged();
                    setting.Value = value;
                    Managers.MenuCoreManager.MainCamAccess().SetCameraSetting(setting.PropertyName, setting.Value);
                }
            }

            [UIAction("decrease")]
            private void DecreaseVal()
            {
                Value -= Increment;
                Instance.SettingList.tableView.ReloadData();
            }
            [UIAction("increase")]
            private void IncreaseVal()
            {
                Value += Increment;
                Instance.SettingList.tableView.ReloadData();

            }
            public EffectListObject(CameraSetting camSetting)
            {
                this.setting = camSetting;
                this.Label = setting.FriendlyName;
            }
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            Instance = this;
            Redo();
        }

        private void Redo()
        {
            SettingList.data.Clear();
            foreach (var setting in Plugin.preset.Bloom)
                SettingList.data.Add(new EffectListObject(setting));
            SettingList.tableView.ReloadData();
        }

        [UIAction("revert")]
        internal void Revert()
        {
            Plugin.preset.Bloom = Defaults.BloomDefaults;
            
            Plugin.preset.Save();
            Managers.MenuCoreManager.MainCamAccess().ApplySettings(Plugin.preset.Bloom);
            Instance.NotifyPropertyChanged();
            Redo();
        }

        [UIAction("cancel-button")]
        internal void Cancel()
        {
            Plugin.preset = Load(Plugin.PresetName);
            Managers.MenuCoreManager.MainCamAccess().ApplySettings(Plugin.preset.Bloom);
            rSettingsFlowCoordinator.SwitchMiddleView();
            Instance.NotifyPropertyChanged();
            Redo();
        }
        [UIAction("apply-button")]
        internal void Apply()
        {
            Plugin.preset.Bloom.Clear();
            foreach(var setting in settingsList)
            {
                EffectListObject e = (EffectListObject)setting;
                Plugin.preset.Bloom.Add(e.setting);
            }
            Plugin.preset.Save();
            Instance.NotifyPropertyChanged();
            SettingList.tableView.ReloadData();
        }

    }
}
