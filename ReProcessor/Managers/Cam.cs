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
            foreach (var prop in inn.Props)
            {
                if (prop.Value.ValueType == typeof(int))
                    inn.Props[prop.Key].Value = _mainEffect.GetField<int, PyramidBloomMainEffectSO>(prop.Value.PropertyName);
            }
            return rtn;
        }
        
        
        public void ApplyAll(Preset preset)
        {
            
            foreach (var prop in preset.Props)
            {
                if (prop.Value.ValueType == typeof(int))
                    _mainEffect.SetField(prop.Value.PropertyName, (int) prop.Value.Value);
                //game says this cast isn't valid?????
                //if (prop.Value.ValueType == typeof(PyramidBloomRendererSO.Pass))
                //    pyramid.SetField(prop.Value.PropertyName, (PyramidBloomRendererSO.Pass) prop.Value.Value);
                
                if (_mainEffect is PyramidBloomMainEffectSO pyramid)
                {
                    

                }
                
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