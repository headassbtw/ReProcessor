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

        private MainCamera _mainCamera;

        public MenuCoreManager(MainCamera mainCamera)
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
            Plugin.Log.Notice("FOV is: " + _mainCamera.camera.fieldOfView.ToString());

            var bloomIntensity = _mainCamera.camera.GetCameraSetting("_bloomBlendFactor");

            Plugin.Log.Notice("Bloom Blend Factor is: " + bloomIntensity);
            Plugin.Log.Notice("Trying to set");
            _mainCamera.camera.SetCameraSetting("_bloomBlendFactor", 0);
            Plugin.Log.Notice("Bloom Blend Factor is: " + _mainCamera.camera.GetCameraSetting("_bloomBlendFactor"));


        }
        public void Dispose()
        {

        }
    }
}
