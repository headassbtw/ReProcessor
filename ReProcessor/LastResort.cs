using System;
using ReProcessor.Managers;
using UnityEngine;
using Zenject;

namespace ReProcessor
{
    public class LastResort : MonoBehaviour, IInitializable
    {
        public void Initialize(){}

        private CamManager _cam;
        [Inject]
        protected void Construct(CamManager cam)
        {
            _cam = cam;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _cam.Reset();
            }
        }
    }
}