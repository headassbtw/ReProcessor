using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using static ReProcessor.PresetExtensions;

namespace ReProcessor.Managers
{
    class MenuCoreManager
    {
        internal static MenuCoreManager Instance;

        internal static Camera MainCamAccess()
        {
            if (Instance._mainCamera != null)
            {
                return Instance._mainCamera;
            }
            else
            {
                Instance._mainCamera = Camera.main;
                return Instance._mainCamera;
            }
        }


        public Camera _mainCamera;
        [Inject]
        public MenuCoreManager()
        {
            _mainCamera = Camera.main;
            string ass2 = _mainCamera == null ? "" : " not";
            Plugin.Log.Critical($"camera.main is{ass2} null");
            
        }

        public void Initialize()
        {
            Plugin.preset = Load(Plugin.PresetName);
            Instance = this;
            _mainCamera.ApplySettings(Plugin.preset.Bloom);
            _mainCamera.ApplySettings(Plugin.preset.ColorBoost);
        }
        public void Dispose()
        {

        }
    }
}
