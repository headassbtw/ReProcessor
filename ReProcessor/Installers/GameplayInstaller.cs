using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using ReProcessor.Managers;

namespace ReProcessor.Installers
{
    class GameplayInstaller : Installer
    {
        private static GameplayInstaller Instance;
        public override void InstallBindings()
        {
            Instance = this;
            Plugin.Log.Notice("Binding Gameplay Camera Manager");
            Container.Bind(typeof(IInitializable), typeof(GameplayCoreManager)).To<GameplayCoreManager>().AsSingle();
        }
        public static void UninstallBindings()
        {
            Plugin.Log.Notice("Uninding Gameplay Camera Manager");
            Instance.Container.Unbind(typeof(IInitializable), typeof(GameplayCoreManager));
        }
    }
}
