using System;
using System.Linq;
using IPA.Utilities;
using ReProcessor.Configuration;
using SiraUtil.Logging;
using UnityEngine;
using Zenject;

namespace ReProcessor.Managers
{
    public class CamManager : IInitializable, IDisposable
    {
        private SiraLog _log;
        private Camera _mainCam;
        internal PyramidBloomMainEffectSO _mainEffect;
        internal static bool BloomSupported;
        CamManager(SiraLog logger)
        {
            _log = logger;
            _mainCam = Camera.main;
            if (_mainCam == null)
            {
                _log.Critical("aw fuck maincam is null, bruh");
            }
            else
            {
                _log.Notice("maincam exists");
                MainEffectController ctl = _mainCam.gameObject.GetComponent<MainEffectController>();
                var eff = ctl.GetField<MainEffectContainerSO, MainEffectController>("_mainEffectContainer").mainEffect;
                if (eff is PyramidBloomMainEffectSO pyramid)
                {
                    _mainEffect = pyramid;
                    BloomSupported = true;
                }
                else
                {
                    BloomSupported = false;
                    throw new InvalidOperationException("Other bloom engines are not supported");
                }
            }
        }

        public void Reset()
        {
            Preset def = new Preset();
            ApplyAll(def);
        }

        public Preset SaveAll(Preset inn)
        {
            Preset rtn = new Preset();
            rtn.Name = inn.Name;
            foreach (var prop in inn.Props)
            {
                if (prop.Value.ValueType == typeof(int) || prop.Value.ValueType == typeof(Single))
                    rtn.Props[prop.Key].Value =
                        Convert.ToSingle(_mainEffect.GetField<Single, PyramidBloomMainEffectSO>(prop.Value.PropertyName));
            }
            return rtn;
        }
        
        
        public void ApplyAll(Preset preset)
        {
            foreach (var prop in preset.Props)
            {
                if (prop.Value.ValueType == typeof(int) || prop.Value.ValueType == typeof(Single))
                    _mainEffect.SetField(prop.Value.PropertyName, Convert.ToSingle(prop.Value.Value));
                
            }
        }
        
        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}