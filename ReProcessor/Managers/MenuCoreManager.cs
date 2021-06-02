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
            return Instance._mainCamera.camera;
        }


        private MainCamera _mainCamera;

        public MenuCoreManager(MainCamera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public void Initialize()
        {
            Plugin.preset = Load(Plugin.PresetName);
            Instance = this;
            _mainCamera.camera.ApplySettings(Plugin.preset.Bloom);
            _mainCamera.camera.ApplySettings(Plugin.preset.ColorBoost);
        }
        public void Dispose()
        {

        }
    }
}
