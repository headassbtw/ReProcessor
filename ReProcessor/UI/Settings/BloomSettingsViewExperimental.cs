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
            
        }

        [UIComponent("setting-list")]
        internal CustomCellListTableData SettingList;
        [UIValue("settings")]
        public List<object> settingsList = new List<object>();

        public class EffectListObject
        {
            internal CameraSetting setting;
            

            [UIValue("label")] private string Label = "";
            [UIValue("increment")] private float Increment = 0.05f;
            [UIValue("min")] private float Min = Plugin.Config.MinAmountIncrease;
            [UIValue("max")] private float Max = Plugin.Config.MaxAmountIncrease;
            [UIValue("num")]
            internal bool IsNumber
            {
                get => setting.ValueType.Equals(valueType.num);
            }
            [UIValue("enum")]
            internal bool IsDropdown
            {
                get => setting.ValueType.Equals(valueType.enm);
            }
            [UIValue("dropdown-options")] private List<object> passes = Defaults.Passes;

            [UIValue("dropdown-value")]
            private string DropdownValue
            {
                get => setting.Value.ToString();
                set
                {
                    Instance.NotifyPropertyChanged();
                    setting.Value = value;
                    
                    Managers.MenuCoreManager.MainCamAccess().SetCameraSetting(setting);
                }
            }
            [UIValue("slider-value")]
            private float SliderValue
            {
                get
                {
                    if (setting.ValueType.Equals(valueType.num))
                        return (float)setting.Value;
                    else
                        return 0;
                }
                set
                {
                    Instance.NotifyPropertyChanged();
                    setting.Value = value;
                    Managers.MenuCoreManager.MainCamAccess().SetCameraSetting(setting);
                }
            }

            [UIAction("decrease")]
            private void DecreaseVal()
            {
                SliderValue -= Increment;
                Instance.SettingList.tableView.ReloadData();
            }
            [UIAction("increase")]
            private void IncreaseVal()
            {
                SliderValue += Increment;
                Instance.SettingList.tableView.ReloadData();

            }
            public EffectListObject(CameraSetting camSetting)
            {
                Plugin.Log.Notice($"{camSetting.FriendlyName} has a value of {camSetting.Value} (type of {camSetting.Value.GetType().ToString()})");

                this.setting = camSetting;
                this.Label = setting.FriendlyName;
                if (camSetting.ValueType.Equals(valueType.num))
                {
                    this.SliderValue = (float.Parse(camSetting.Value.ToString()));
                    //this.DropdownValue = "";
                }
                if (camSetting.ValueType.Equals(valueType.enm))
                {
                    this.DropdownValue = camSetting.Value.ToString();
                    //this.SliderValue = 0f;
                }
            }
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            
            Plugin.preset = Load(Plugin.PresetName);
            Instance = this;
            SettingList.data.Clear();
            Redo();
        }

        private void Redo()
        {
            SettingList.data.Clear();
            Plugin.Log.Notice($"Bloom currently has {Plugin.preset.Bloom.Count()} Settings");
            foreach (var setting in Plugin.preset.Bloom)
                SettingList.data.Add(new EffectListObject(setting));
            Instance.NotifyPropertyChanged();
            SettingList.tableView.ReloadData();
        }

        [UIAction("revert")]
        internal void Revert()
        {
            Plugin.preset.Bloom = Defaults.BloomDefaults;
            
            
            Managers.MenuCoreManager.MainCamAccess().ApplySettings(Plugin.preset.Bloom);
            Instance.NotifyPropertyChanged();
            Redo();
            Plugin.preset.Save();
            Plugin.preset = Load(Plugin.PresetName);
        }

        [UIAction("cancel-button")]
        internal void Cancel()
        {
            Plugin.preset = Load(Plugin.PresetName);
            Managers.MenuCoreManager.MainCamAccess().ApplySettings(Plugin.preset.Bloom);
            rSettingsFlowCoordinator.SwitchMiddleView();
            Instance.NotifyPropertyChanged();
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
