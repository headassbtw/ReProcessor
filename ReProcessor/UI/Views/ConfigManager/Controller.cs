using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using ReProcessor.Configuration;
using ReProcessor.Managers;
using ReProcessor.UI.Views.ColorBoostView;
using SiraUtil.Logging;
using Zenject;

namespace ReProcessor.UI.Views.ConfigManager
{
    [ViewDefinition("ReProcessor.UI.Views.ConfigManager.View.bsml")]
    [HotReload(RelativePathToLayout = @"View.bsml")]
    internal class ConfigViewController : BSMLAutomaticViewController
    {
        private PluginConfig _conf;
        private Managers.ConfigManager _cfg;
        private CamManager _cam;
        private ColorBoostController _colorBoostController;
        private string choice;
        private SiraLog _log;

        [Inject]
        protected void Construct(Managers.ConfigManager config, SiraLog log, CamManager cam, ColorBoostController cbCtl, PluginConfig conf)
        {
            _cfg = config;
            _log = log;
            _cam = cam;
            _conf = conf;
            choice = _conf.Preset;
            _colorBoostController = cbCtl;
        }

        [UIComponent("CfgList")]
        public CustomListTableData CfgList = null!;

        [UIAction("cfgSelect")]
        public void cfgSelect(TableView _, int row)
        {
            choice = CfgList.data[row].text;
            _conf.Preset = choice;
            
            _cfg.Set(choice);
            _cam.ApplyAll(_cfg.Presets[choice].Props);
            _log.Notice($"applying {choice}");
            _log.Notice($"BaseColorBoost {_cfg.Presets[choice].Props.BaseColorBoost}");
            _colorBoostController.ReloadProps();

        }

        [UIAction("Save")]
        private void Save()
        {
            _cfg.Presets[choice] = _cam.SaveAll();
            _cfg.Save(choice);
            _conf.Preset = choice;
        }

        [UIAction("#post-parse")]
        public void PostParse()
        {
            
            
            CfgList.tableView.SelectCellWithIdx(0);
            Reload();
        }

        [UIAction("Reload")]
        private void Reload()
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