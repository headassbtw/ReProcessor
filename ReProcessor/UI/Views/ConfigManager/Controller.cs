using System;
using System.Collections.Generic;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using ReProcessor.Extensions;
using ReProcessor.Managers;
using SiraUtil.Logging;
using UnityEngine;
using Zenject;

namespace ReProcessor.UI.Views.TestView
{
    [ViewDefinition("ReProcessor.UI.Views.ConfigManager.View.bsml")]
    [HotReload(RelativePathToLayout = @"View.bsml")]
    public class ConfigViewController : BSMLAutomaticViewController, IInitializable
    {
        
        public void Initialize(){}
        private ConfigManager _cfg;
        private CamManager _cam;
        private ColorBoostController _colorBoostController;
        private SiraLog _log;
        private string choice;
        [Inject]
        protected void Construct(ConfigManager config, CamManager cam, SiraLog log, ColorBoostController cbCtl)
        {
            _cfg = config;
            _cam = cam;
            _log = log;
            _colorBoostController = cbCtl;
        }

        [UIComponent("CfgList")] public CustomListTableData CfgList = new CustomListTableData();


        [UIAction("cfgSelect")]
        public void cfgSelect(TableView _, int row)
        {
            choice = CfgList.data[row].text;
            Apply();
        }

        [UIAction("Save")]
        void Save()
        {
            var m = Camera.main.MainEffectContainerSO().mainEffect;
            foreach (var prop in _cfg.TempPreset.Bloom)
            {
                if (prop.Value.ValueType == typeof(Single))
                {
                    var f = m.PrivateField(prop.Value.PropertyName);
                    _cfg.CurrentPreset.Bloom[prop.Key].Value = f.GetValue(m);


                }
            }
            foreach (var prop in _cfg.TempPreset.ColorBoost)
            {
                if (prop.Value.ValueType == typeof(Single))
                {
                    var f = m.PrivateField(prop.Value.PropertyName);
                    _cfg.CurrentPreset.ColorBoost[prop.Key].Value = f.GetValue(m);
                    
                }
            }

            _cfg.Presets[choice] = _cfg.CurrentPreset;
            
            _cfg.Save(choice);
        }
        
        [UIAction("Apply")]
        void Apply()
        {
            _cfg.Set(choice);
            
            _cam.ApplyAll(_cfg.CurrentPreset);
            _colorBoostController.ReloadProps();
        }

        [UIAction("#post-parse")]
        void PostParse()
        {
            _cfg.GetPresets();
        }
        
        [UIAction("Reload")]
        void Reload()
        {
            _cfg.GetPresets();
            CfgList.data.Clear();

            foreach (var item in _cfg.Presets)
            {
                var cfgCell = new CustomListTableData.CustomCellInfo(item.Key);
                CfgList.data.Add(cfgCell);
            }
            CfgList.tableView.ReloadData();
        }
    }
}