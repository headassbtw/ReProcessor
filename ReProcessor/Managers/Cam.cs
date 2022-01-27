using System;
using System.Linq;
using ReProcessor.Configuration;
using ReProcessor.Extensions;
using SiraUtil.Logging;
using UnityEngine;
using Zenject;

namespace ReProcessor.Managers
{
    public class CamManager : IInitializable, IDisposable
    {
        private SiraLog _log;
        private Camera _mainCam;
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
            }
        }

        public void Reset()
        {
            Preset def = new Preset();
            ApplyAll(def);
        }
        
        public void Apply(CameraSetting setting)
        {
            _mainCam.SetCameraSetting(setting);
        }

        public void ApplyAll(Preset preset)
        {
            _mainCam.ApplyProps(preset);
        }
        
        public void Initialize()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}