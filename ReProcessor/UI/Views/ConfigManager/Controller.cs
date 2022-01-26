using System.Collections.Generic;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using ReProcessor.Managers;
using SiraUtil.Logging;
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
        private SiraLog _log;
        private string choice;
        [Inject]
        protected void Construct(ConfigManager config, CamManager cam, SiraLog log)
        {
            _cfg = config;
            _cam = cam;
            _log = log;
        }

        [UIComponent("CfgList")] public CustomListTableData CfgList = new CustomListTableData();


        [UIAction("cfgSelect")]
        public void cfgSelect(TableView _, int row)
        {
            choice = CfgList.data[row].text;
        }

        [UIAction("Save")]
        void Save()
        {
            _cfg.Save(choice);
        }
        
        [UIAction("Apply")]
        void Apply()
        {
            _cfg.Set(choice);
            
            _cam.ApplyAll(_cfg.CurrentPreset);
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