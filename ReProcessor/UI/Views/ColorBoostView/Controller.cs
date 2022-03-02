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
using IPA.Utilities.Async;
using ReProcessor.Configuration;
using ReProcessor.Managers;
using SiraUtil.Logging;
using UnityEngine;
using Zenject;

namespace ReProcessor.UI.Views.ColorBoostView
{
    [ViewDefinition("ReProcessor.UI.Views.ColorBoostView.View.bsml")]
    [HotReload(RelativePathToLayout = @"View.bsml")]
    internal class ColorBoostController : BSMLAutomaticViewController
    {
        private CamManager _camManager = null!;
        private Managers.ConfigManager _cfgManager = null!;
        private SiraLog _log = null!;
        private PluginConfig _conf = null!;

        [Inject]
        protected void Construct(CamManager camManager, Managers.ConfigManager cfgManager, SiraLog log,
            PluginConfig conf)
        {
            _camManager = camManager;
            _cfgManager = cfgManager;
            _log = log;
            _conf = conf;
            
            log.Debug("ColorBoostViewController created");
        }
        [UIValue("passes")] List<object> passes => Enum.GetValues(typeof(PyramidBloomRendererSO.Pass)).Cast<object>().ToList();

         private List<object> fff = new List<object>
            {Enum.GetValues(typeof(PyramidBloomRendererSO.Pass)).Cast<PyramidBloomRendererSO.Pass>()}.ToList();

        [UIParams]
        private readonly BSMLParserParams _parserParams = null!;

        [UIComponent("cb-items")]
        private ScrollableSettingsContainerTag Settings;

        private Transform _Container;

        IEnumerator SingleFrameGoBrrThanksGame()
        {
            yield return new WaitForSeconds(0.5f);
            _parserParams.EmitEvent("flashlight-warning");
        }

        [UIAction("yep-okay")]
        private void Yeah_Okay()
        {
            _conf.Introduced = true;
            _parserParams.EmitEvent("hide-flashlight-warning");
        }

        [UIAction("#post-parse")]
        void PostParse()
        {
            _log.Warn("Enums:");
            foreach (var en in passes)
            {
                _log.Warn(((PyramidBloomRendererSO.Pass) en).ToString());
            }
            
            if (!_conf.Introduced)
                StartCoroutine(SingleFrameGoBrrThanksGame());
        }
        
        [UIValue("bloomradius-val")] private float BloomRadius
        {   get => _camManager.proxy.BloomRadius;
            set => _camManager.proxy.BloomRadius = value;}
        [UIValue("blendfactor-val")] private float BlendFactor
        {   get => _camManager.proxy.BlendFactor;
            set => _camManager.proxy.BlendFactor = value;}
        [UIValue("intensity-val")] private float Intensity
        {   get => _camManager.proxy.Intensity;
            set => _camManager.proxy.Intensity = value;}
        [UIValue("intensityoffset-val")] private float IntensityOffset
        {   get => _camManager.proxy.IntensityOffset;
            set => _camManager.proxy.IntensityOffset = value;}
        [UIValue("weight-val")] private float Weight
        {   get => _camManager.proxy.Weight;
            set => _camManager.proxy.Weight = value;}
        [UIValue("alphaweights-val")] private float AlphaWeights
        {   get => _camManager.proxy.AlphaWeights;
            set => _camManager.proxy.AlphaWeights = value;}
        
        
        
        [UIValue("basecolorboost-val")] private float BaseColorBoost
        {   get => _camManager.proxy.BaseColorBoost;
            set => _camManager.proxy.BaseColorBoost = value;}
        [UIValue("basecolorboostthreshold-val")] private float BaseColorBoostThreshold
        {   get => _camManager.proxy.BaseColorBoostThreshold;
            set => _camManager.proxy.BaseColorBoostThreshold = value;}

        [UIValue("prefilter-val")] private PyramidBloomRendererSO.Pass PreFilterPass
        {   get => _camManager.proxy.PreFilterPass;
            set => _camManager.proxy.PreFilterPass = value;}
        [UIValue("downsample-val")] private PyramidBloomRendererSO.Pass DownSamplePass
        {   get => _camManager.proxy.DownSamplePass;
            set => _camManager.proxy.DownSamplePass = value;}
        [UIValue("upsample-val")] private PyramidBloomRendererSO.Pass UpSamplePass
        {   get => _camManager.proxy.UpSamplePass;
            set => _camManager.proxy.UpSamplePass = value;}
        [UIValue("finalupsample-val")] private PyramidBloomRendererSO.Pass FinalUpSamplePass
        {   get => _camManager.proxy.FinalUpSamplePass;
            set => _camManager.proxy.FinalUpSamplePass = value;}
        
        
        [UIAction("refresh")] async void a(){await ReloadPropsOnMainThread();}
        
        [UIAction("Test")]
        public void ReloadProps()
        {
            
            ReloadPropsOnMainThread();
        }


        async Task ReloadPropsOnMainThread()
        {
            await UnityMainThreadTaskScheduler.Factory.StartNew(() =>
            {
                NotifyPropertyChanged(nameof(BloomRadius));
                NotifyPropertyChanged(nameof(BlendFactor));
                NotifyPropertyChanged(nameof(Intensity));
                NotifyPropertyChanged(nameof(IntensityOffset));
                NotifyPropertyChanged(nameof(Weight));
                NotifyPropertyChanged(nameof(AlphaWeights));
                NotifyPropertyChanged(nameof(PreFilterPass));
                NotifyPropertyChanged(nameof(DownSamplePass));
                NotifyPropertyChanged(nameof(UpSamplePass));
                NotifyPropertyChanged(nameof(FinalUpSamplePass));
                NotifyPropertyChanged(nameof(BaseColorBoost));
                NotifyPropertyChanged(nameof(BaseColorBoostThreshold));
            }).ConfigureAwait(false);
        }

        [UIAction("Apply")]
        void Apply()
        {
           
            //_camManager.ApplyAll(_cfgManager.CurrentPreset);
        }

        [UIAction("Back")]
        void GoBack()
        {
            //_camManager.ApplyAll(_cfgManager.CurrentPreset.Props);
            //_cfgManager.TempPreset = _cfgManager.CurrentPreset;
        }
    }
}