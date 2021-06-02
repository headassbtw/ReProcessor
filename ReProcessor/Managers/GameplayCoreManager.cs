using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;
using System.Reflection;
using UnityEngine.Rendering;

namespace ReProcessor.Managers
{
    class GameplayCoreManager : IInitializable, IDisposable
    {

        private MainCamera _mainCamera;

        public GameplayCoreManager(MainCamera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        public void Initialize()
        {
            _mainCamera.camera.ApplySettings(Plugin.preset.Bloom);
            _mainCamera.camera.ApplySettings(Plugin.preset.ColorBoost);
        }
        public void Dispose()
        {

        }
    }
}
