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
    class MenuCoreManager : IInitializable, IDisposable
    {
        internal static MenuCoreManager Instance;

        internal static Camera MainCamAccess()
        {
            if(Instance._mainCamera != null)
                return Instance._mainCamera;
            else
            {
                Instance._mainCamera = Camera.main;
                return Instance._mainCamera;
            }
        }


        private Camera _mainCamera;

        public MenuCoreManager(MainCamera mainCamera)
        {
            _mainCamera = Camera.main;
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
