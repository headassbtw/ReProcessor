using System;
using System.Collections.Generic;
using System.Linq;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.ViewControllers;
//using RuntimeUnityEditor.BSIPA4;
using UnityEngine.Serialization;
using Zenject;
using ReProcessor.Files;
using static ReProcessor.Config;
using static ReProcessor.PresetExtensions;
using BeatSaberMarkupLanguage.Components;

//we'll fix you later oki doki?

namespace ReProcessor.UI
{
    [ViewDefinition("ReProcessor.UI.Views.BaseColorBoostView.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\BaseColorBoostView.bsml")]
    internal class BaseColorBoostViewController : BSMLAutomaticViewController
    {
        internal static BaseColorBoostViewController Instance;

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
                get => setting.ValueType.Equals(valueType.Integer) || setting.ValueType.Equals(valueType.Decimal);
            }
            [UIValue("enum")]
            internal bool IsDropdown
            {
                get => setting.ValueType.Equals(valueType.Enumerator);
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
                    if (setting.ValueType.Equals(valueType.Decimal))
                        return (float)setting.Value;
                    if (setting.ValueType.Equals(valueType.Integer))
                        return (Int32)setting.Value;
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
                if (camSetting.ValueType.Equals(valueType.Decimal))
                {
                    this.SliderValue = (float.Parse(camSetting.Value.ToString()));
                    //this.DropdownValue = "";
                }
                if (camSetting.ValueType.Equals(valueType.Integer))
                {
                    this.SliderValue = (Int32.Parse(camSetting.Value.ToString()));
                    //this.DropdownValue = "";
                }
                if (camSetting.ValueType.Equals(valueType.Enumerator))
                {
                    this.DropdownValue = camSetting.Value.ToString();
                    //this.SliderValue = 0f;
                }
            }
        }
        [UIAction("#post-parse")]
        internal void PostParse()
        {
            Instance = this;
            SettingList.data.Clear();
            //SettingList.data.Add(new EffectListObject(new CameraSetting("Prefilter Pass", "_prefilterPass", "Prefilter13", valueType.enm)));
            //SettingList.data.Add(new EffectListObject(new CameraSetting("Radius", "_bloomRadius", 5f, valueType.num)));
            SettingList.tableView.ReloadData();
            Redo();
        }

        private void Redo()
        {
            SettingList.data.Clear();
            Plugin.Log.Notice($"Color Boost currently has {Plugin.preset.Bloom.Count()} Settings");
            foreach (var setting in Plugin.preset.ColorBoost)
                SettingList.data.Add(new EffectListObject(setting));
            SettingList.tableView.ReloadData();
        }


        [UIAction("revert")]
        internal void Revert()
        {
            Plugin.preset.ColorBoost = Defaults.ColorBoostDefaults;


            Managers.MenuCoreManager.MainCamAccess().ApplySettings(Plugin.preset.ColorBoost);
            Instance.NotifyPropertyChanged();
            Redo();
            Plugin.preset.Save();
            Plugin.preset = Load(Plugin.PresetName);
        }

        [UIAction("cancel-button")]
        internal void Cancel()
        {
            Plugin.preset = Load(Plugin.PresetName);
            Managers.MenuCoreManager.MainCamAccess().ApplySettings(Plugin.preset.ColorBoost);
            rSettingsFlowCoordinator.SwitchMiddleView();
            Instance.NotifyPropertyChanged();
        }
        [UIAction("apply-button")]
        internal void Apply()
        {
            Plugin.preset.ColorBoost.Clear();
            foreach (var setting in settingsList)
            {
                EffectListObject e = (EffectListObject)setting;
                Plugin.preset.ColorBoost.Add(e.setting);
            }
            Plugin.preset.Save();
            Instance.NotifyPropertyChanged();
            SettingList.tableView.ReloadData();
        }
    }
}