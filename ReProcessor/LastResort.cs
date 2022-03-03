using ReProcessor.Managers;
using UnityEngine;
using Zenject;

namespace ReProcessor
{
    internal class LastResort : MonoBehavior
    {
        private CamManager _cam = null!;

        [Inject]
        protected void Construct(CamManager cam)
        {
            _cam = cam;
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _cam.Reset();
            }
        }
    }
}