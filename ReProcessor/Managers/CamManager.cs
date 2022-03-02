using System;
using IPA.Utilities;
using ReProcessor.Configuration;
using SiraUtil.Logging;
using UnityEngine;

namespace ReProcessor.Managers
{
    internal class CamManager
    {
        internal readonly bool BloomSupported;
        public event Action? ResetEvent;
        internal readonly PyramidBloomMainEffectSO? _mainEffect;
        private ConfigManager _cfg;
        internal PyramidBloomEffectProxy proxy { get; private set; }
        private SiraLog _log;
        public CamManager(SiraLog logger, ConfigManager config)
        {
            _log = logger;
            _cfg = config;
            var mainCam = Camera.main;
            if (mainCam == null)
            {
                logger.Critical("aw fuck maincam is null, bruh");
            }
            else
            {
                logger.Notice("maincam exists");
                MainEffectController ctl = mainCam.gameObject.GetComponent<MainEffectController>();
                var eff = ctl.GetField<MainEffectContainerSO, MainEffectController>("_mainEffectContainer").mainEffect;
                if (eff is PyramidBloomMainEffectSO pyramid)
                {
                    _mainEffect = pyramid;
                    BloomSupported = true;
                    proxy = new PyramidBloomEffectProxy(_mainEffect!);
                    
                    
                    ApplyAll(_cfg.Presets[_cfg.Current].Props);
                }
                else
                {
                    BloomSupported = false;
                }
            }
        }

        public void Reset()
        {
            ResetEvent?.Invoke();
            ApplyAll(_cfg.Presets[_cfg.Current].Props);
        }

        public Preset SaveAll(string name)
        {
            if (!BloomSupported)
            {
                throw new InvalidOperationException("Bloom support isn't available, method isn't allowed to be called.");
            }

            var rtn = new Preset(name);
            var cameraSettings = rtn.Props;

            

            cameraSettings.BloomRadius = proxy.BloomRadius;
            cameraSettings.BlendFactor = proxy.BlendFactor;
            cameraSettings.Intensity = proxy.Intensity;
            cameraSettings.IntensityOffset = proxy.IntensityOffset;
            cameraSettings.Weight = proxy.Weight;
            cameraSettings.AlphaWeights = proxy.AlphaWeights;
            cameraSettings.PreFilterPass = proxy.PreFilterPass;
            cameraSettings.DownSamplePass = proxy.DownSamplePass;
            cameraSettings.UpSamplePass = proxy.UpSamplePass;
            cameraSettings.FinalUpSamplePass = proxy.FinalUpSamplePass;
            cameraSettings.BaseColorBoost = proxy.BaseColorBoost;
            cameraSettings.BaseColorBoostThreshold = proxy.BaseColorBoostThreshold;

            return rtn;
        }


        public void ApplyAll(CameraSettings cameraSettings)
        {
            if (!BloomSupported)
            {
                throw new InvalidOperationException("Bloom support isn't available, method isn't allowed to be called.");
            }
            
            proxy.BloomRadius = cameraSettings.BloomRadius;
            proxy.BlendFactor = cameraSettings.BlendFactor;
            proxy.Intensity = cameraSettings.Intensity;
            proxy.IntensityOffset = cameraSettings.IntensityOffset;
            proxy.Weight = cameraSettings.Weight;
            proxy.AlphaWeights = cameraSettings.AlphaWeights;
            proxy.PreFilterPass = cameraSettings.PreFilterPass;
            proxy.DownSamplePass = cameraSettings.DownSamplePass;
            proxy.UpSamplePass = cameraSettings.UpSamplePass;
            proxy.FinalUpSamplePass = cameraSettings.FinalUpSamplePass;
            proxy.BaseColorBoost = cameraSettings.BaseColorBoost;
            proxy.BaseColorBoostThreshold = cameraSettings.BaseColorBoostThreshold;
        }
    }
}