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
            //_mainCamera.camera.ApplyBloomPreset(Plugin.preset);
        }
        public void Dispose()
        {

        }
    }
}
