using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Tags;
using BeatSaberMarkupLanguage.TypeHandlers;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using ReProcessor.Configuration;
using ReProcessor.Extensions;
using ReProcessor.Managers;
using SiraUtil.Logging;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace ReProcessor.UI.Views.TestView
{
    [ViewDefinition("ReProcessor.UI.Views.ColorBoostView.View.bsml")]
    [HotReload(RelativePathToLayout = @"View.bsml")]
    public class ColorBoostController : BSMLAutomaticViewController, IInitializable
    {
        
        public void Initialize(){}
        private rSettingsFlowCoordinator _settings;
        private CamManager _camManager;
        private ConfigManager _cfgManager;
        private SiraLog _log;
        [Inject]
        protected void Construct(rSettingsFlowCoordinator settings, CamManager camManager, ConfigManager cfgManager, SiraLog log)
        {
            _settings = settings;
            _camManager = camManager;
            _cfgManager = cfgManager;
            _log = log;
        }

        [UIValue("d")] private List<object> shit = new object[]{"a","SHI"}.ToList();
        
        [UIComponent("cb-items")]
        private ScrollableSettingsContainerTag Settings;

        //me
        void YeetChildren(GameObject obj)
        {
            int c = obj.transform.childCount;
            _log.Notice($"{c} Children of {obj.name}, yeeting...");
            for (int i = 0; i < c; i++)
            {
                
                Destroy(obj.transform.GetChild(0).gameObject);
                Destroy(obj.transform.GetChild(i).gameObject);
            }
        }
        
        SliderSetting CreateSlider(string name, Transform parent)
        {
            var sld = UnityEngine.Object.Instantiate(TemplateSlider, parent, false);
            sld.transform.SetParent(parent);
            sld.name = name;
            sld.transform.GetChild(0).GetComponent<CurvedTextMeshPro>().text = name;
            sld.Setup();
            sld.ApplyValue();
            return sld;
        }

        private SliderSetting TemplateSlider;
        private DropDownListSetting TemplateDropdown;
        
        [UIAction("#post-parse")]
        void PostParse()
        {
            var s = Resources.FindObjectsOfTypeAll<SliderSetting>().Last(x => (x.name == "BSMLSliderSetting"));
            TemplateSlider = UnityEngine.Object.Instantiate(s, transform, false);
            s.gameObject.SetActive(false);
            
            //var d = Resources.FindObjectsOfTypeAll<DropDownListSetting>().Last(x => (x.name == "BSMLDropdownList"));
            //TemplateDropdown = UnityEngine.Object.Instantiate(d, transform, false);
            //d.gameObject.SetActive(false);
        }
        
        
        [UIAction("Test")]
        public void ReloadProps()
        {
            
            NotifyPropertyChanged();

            var c = gameObject.transform
                .GetChild(0) //BSMLVerticalLayoutGroup
                .GetChild(0) //BSMLVerticalLayoutGroup
                .GetChild(1) //BSMLScrollableSettingsContainer
                .GetChild(1) //Viewport
                .GetChild(0) //BSMLScrollViewContent
                .GetChild(0) //BSMLScrollViewContentContainer
                ; //lmao

            YeetChildren(c.gameObject);
            var m = Camera.main.MainEffectContainerSO().mainEffect;
            
            
            
            
            foreach (var prop in _cfgManager.TempPreset.Bloom)
            {
                if (prop.Value.ValueType == typeof(Single))
                {
                    var sld = CreateSlider(prop.Key,c);
                    sld.associatedValue = new BSMLFieldValue(m, m.PrivateField(prop.Value.PropertyName));
                    sld.Value = Convert.ToSingle(sld.associatedValue.GetValue());


                }
            }
            foreach (var prop in _cfgManager.TempPreset.ColorBoost)
            {
                if (prop.Value.ValueType == typeof(Single))
                {
                    var sld = CreateSlider(prop.Key,c);
                    sld.Value = Convert.ToSingle(2);
                    sld.associatedValue = new BSMLFieldValue(m, m.PrivateField(prop.Value.PropertyName));
                    sld.Value = Convert.ToSingle(sld.associatedValue.GetValue());
                    
                }
            }



        }

        [UIAction("Apply")]
        void Apply()
        {
            _cfgManager.CurrentPreset = _cfgManager.TempPreset;
            _camManager.ApplyAll(_cfgManager.CurrentPreset);
        }
        
        [UIAction("Back")]
        void GoBack()
        {
            
            _camManager.ApplyAll(_cfgManager.CurrentPreset);
            _cfgManager.TempPreset = _cfgManager.CurrentPreset;
            _settings.RevertMiddleController();
        }
    }
}