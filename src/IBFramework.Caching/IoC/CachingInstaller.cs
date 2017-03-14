﻿using IBFramework.Core.Caching;
using IBFramework.Core.Enum;
using IBFramework.Core.IoC;
using IBFramework.Caching;

namespace IBFramework.Caching.IoC
{
    public class CachingInstaller : IContainerInstaller
    {
        public void Install(IContainerGenerator containerGenerator)
        {
            containerGenerator.Register<TriggerFileManager>().As<ITriggerFileManager>().WithLifestyle(RegistrationLifestyleType.Singleton);
            containerGenerator.Register<CacheAccessor>().As<ICacheAccessor>().WithLifestyle(RegistrationLifestyleType.Singleton);

            containerGenerator.Register(typeof(ObjectCache<>)).As(typeof(IObjectCache<>)).WithLifestyle(RegistrationLifestyleType.Transient);
        }
    }

    public static class CachingInstallerExtension
    {
        public static IContainerGenerator InstallCaching(this IContainerGenerator containerGenerator)
        {
            new CachingInstaller().Install(containerGenerator);
            return containerGenerator;
        }
    }
}