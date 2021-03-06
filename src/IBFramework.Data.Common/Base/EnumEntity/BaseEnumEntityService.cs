﻿using IBFramework.Core.Data;
using IBFramework.Core.Data.Base.EnumEntity;
using IBFramework.Core.Data.Domain;
using IBFramework.Data.Common.Base.Entity;
using System;

namespace IBFramework.Data.Common.Base.EnumEntity
{
    public class BaseEnumEntityService<TEntity, TRepo, TEnum> :
        BaseEntityService<TEntity, TRepo>,
        IEnumEntityService<TEntity, TRepo, TEnum>

        where TEntity : class, IEnumEntity<TEnum>
        where TRepo : IEnumEntityRepository<TEntity, TEnum>
        where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        #region Constructor

        public BaseEnumEntityService(TRepo repo) 
            : base(repo)
        {
        }

        #endregion
    }
}
