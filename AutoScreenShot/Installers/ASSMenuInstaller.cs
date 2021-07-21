﻿using AutoScreenShot.Models;
using SiraUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace AutoScreenShot.Installers
{
    public class ASSMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            this.Container.BindInterfacesAndSelfTo<FloatingImageController>().FromNewComponentOnNewGameObject().AsCached().NonLazy();
        }
    }
}