﻿using System;
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
        public override void InstallBindings()
        {
            Container.Bind(typeof(IInitializable), typeof(GameplayCoreManager)).To<GameplayCoreManager>().AsSingle();
        }
    }
}