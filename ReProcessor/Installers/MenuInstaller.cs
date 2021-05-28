using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;
using ReProcessor.Managers;

namespace ReProcessor.Installers
{
    class MenuInstaller : Installer
    {
        public override void InstallBindings()
        {
            Plugin.Log.Notice("Binding Menu Camera Manager");
            Container.Bind(typeof(IInitializable), typeof(MenuCoreManager)).To<MenuCoreManager>().AsSingle();
        }
    }
}
