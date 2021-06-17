using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReProcessor.UI
{
    internal class GetKeyPress : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Plugin.Log.Notice("space key was pressed");
                rSettingsFlowCoordinator.RevertCurrentSettings();
            }
        }
    }
}
