﻿using IBFramework.Core.Caching;
using IBFramework.IoC;
using System.IO;

namespace IBFramework.TestUtilities
{
    public static class CacheCleaner
    {
        public static void ResetCache<T>()
        {
            var fileManager = ServiceLocator.Instance.Resolve<ITriggerFileManager>();
            fileManager.TriggerCache<T>();
        }

        public static void ResetAllCache()
        {
            var fileManager = ServiceLocator.Instance.Resolve<ITriggerFileManager>();
            
            foreach (var file in Directory.GetFiles(fileManager.TriggerFileFolder))
            {
                File.Delete(file);
            }
        }
    }
}
