using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using UnityEngine;

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
            Instance = this;
            try
            {
                _mainCamera = mainCamera;
            }
            catch (Exception e)
            {
                Plugin.Log.Critical("ayo fuck " + e.ToString());
            }
        }

        public void Initialize()
        {
            _mainCamera.camera.ApplyBloomPreset(Plugin.preset);
        }
        public void Dispose()
        {

        }
    }
}
