﻿using IBFramework.Core.IoC;
using IBFramework.Core.Enum;

namespace IBFramework.IoC.Installers
{
    public class IoCInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator container)
        {
            //container.Register<Container>().As<IContainer>().WithLifestyle(RegistrationLifestyleType.Transient);
            //container.Register<ContainerGenerator>().As<IContainerGenerator>().WithLifestyle(RegistrationLifestyleType.Transient);
            //container.Register(typeof(RegistrationResult<,>)).As(typeof(IRegistrationResult<,>)).WithLifestyle(RegistrationLifestyleType.Transient);

            //container.Register<ServiceLocator>().As<IServiceLocator>().WithLifestyle(RegistrationLifestyleType.Singleton);


            container.RegisterTransient<IContainer, Container>();
            container.RegisterTransient<IContainerGenerator, ContainerGenerator>();

            container.RegisterSingleton<IServiceLocator, ServiceLocator>();
        }
    }

    public static class IoCInstallerExtension
    {
        public static IContainerGenerator InstallIoC(this IContainerGenerator containerGenerator)
        {
            new IoCInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}
