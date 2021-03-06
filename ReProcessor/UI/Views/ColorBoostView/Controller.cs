using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components.Settings;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Tags;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using ReProcessor.Configuration;
using ReProcessor.Managers;
using SiraUtil.Logging;
using UnityEngine;
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
        private PluginConfig _conf;
        [Inject]
        protected void Construct(rSettingsFlowCoordinator settings, CamManager camManager, ConfigManager cfgManager, SiraLog log, PluginConfig conf)
        {
            _settings = settings;
            _camManager = camManager;
            _cfgManager = cfgManager;
            _log = log;
            _conf = conf;
        }

        [UIValue("d")] private List<object> shit = new object[]{"a","SHI"}.ToList();
        
        [UIParams]
        BSMLParserParams parserParams;
        
        [UIComponent("cb-items")]
        private ScrollableSettingsContainerTag Settings;

        //me
        void YeetChildren(GameObject obj)
        {
            int c = obj.transform.childCount;
            for (int i = 0; i < c; i++)
            {
                
                Destroy(obj.transform.GetChild(0).gameObject);
                Destroy(obj.transform.GetChild(i).gameObject);
            }
        }
        [UIComponent("tmps")] SliderSetting tmp_slider;
        [UIComponent("tmpd")] DropDownListSetting tmp_dropdown;
        
        SliderSetting CreateSlider(string name, Transform parent)
        {
            var sld = UnityEngine.Object.Instantiate(TemplateSlider, parent, false);

            //sld.transform.position -= new Vector3(0, 2000, 0);
            //sld.transform.SetParent(parent);
            sld.gameObject.SetActive(true);
            sld.name = name;
            sld.transform.GetChild(0).GetComponent<CurvedTextMeshPro>().text = name;
            sld.Setup();
            sld.ApplyValue();
            return sld;
        }

        private Transform _Container;
        
        private SliderSetting TemplateSlider;
        private DropDownListSetting TemplateDropdown;

        IEnumerator SingleFrameGoBrrThanksGame()
        {
            yield return new WaitForSeconds(0.5f);
            parserParams.EmitEvent("flashlight-warning");
        }

        [UIAction("yep-okay")]
        private void Yeah_Okay()
        {
            _conf.Introduced = true;
            parserParams.EmitEvent("hide-flashlight-warning");
        }
        
        [UIAction("#post-parse")]
        void PostParse()
        {
            _Container = tmp_slider.transform.parent;
            var s = tmp_slider;
            //var s = Resources.FindObjectsOfTypeAll<SliderSetting>().Last(x => (x.name == "BSMLSliderSetting"));
            TemplateSlider = Instantiate(s, transform, false);
            TemplateSlider.gameObject.SetActive(false);
            s.gameObject.SetActive(false);
            Destroy(s.gameObject);
            
            //var d = Resources.FindObjectsOfTypeAll<DropDownListSetting>().Last(x => (x.name == "BSMLDropdownList"));
            var d = tmp_dropdown;
            TemplateDropdown = UnityEngine.Object.Instantiate(d, transform, false);
            TemplateDropdown.gameObject.SetActive(false);
            d.gameObject.SetActive(false);
            YeetChildren(d.transform.parent.gameObject);
            BeatSaberUI.CreateText(_Container.GetComponent<RectTransform>(), "Select a preset to start", Vector2.right);
            if(!_conf.Introduced)
                SharedCoroutineStarter.instance.StartCoroutine(SingleFrameGoBrrThanksGame());
        }


        private Dictionary<string, BSMLFieldValue> _values = new Dictionary<string, BSMLFieldValue>();


        [UIAction("Test")]
        public void ReloadProps()
        {
            
            NotifyPropertyChanged();
            _log.Notice($"Running on {gameObject.name}");
            var c = gameObject.transform
                .GetChild(0) //BSMLBackground?
                .GetChild(0) //BSMLVerticalLayoutGroup
                .GetChild(1) //BSMLVerticalLayoutGroup
                .GetChild(0) //BSMLScrollableSettingsContainer
                .GetChild(1) //Viewport
                .GetChild(0) //BSMLScrollViewContent
                .GetChild(0) //BSMLScrollViewContentContainer
                ; //lmao
            //var c = _Container;

            YeetChildren(c.gameObject);
            foreach (var prop in _cfgManager.TempPreset.Props)
            {
                if (prop.Value.ValueType == typeof(Single))
                {
                    
                    if (!_values.ContainsKey(prop.Value.PropertyName))
                    {
                        Type type = _camManager._mainEffect.GetType();
                        BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
                        _values.Add(prop.Value.PropertyName,new BSMLFieldValue(_camManager._mainEffect,
                            type.GetField(prop.Value.PropertyName, bindingFlags)));
                    }
                    
                    var sld = CreateSlider(prop.Key,c);
                    sld.associatedValue = _values[prop.Value.PropertyName];
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
        }
    }
}