﻿using System;
using System.Collections.Generic;
using Catalog.Interfaces;
using DataTransformation.Interfaces;
using InMemoryStorage.Interfaces;
using Persistency.Implementation;
using Persistency.Interfaces;

namespace Catalog.Implementation
{
    public class PersistableCatalog<T, TVMO, TDTO> : Catalog<T, TVMO, TDTO>, IPersistableCatalog, IManagedCatalog
    {
        public PersistableCatalog(
            IInMemoryCollection<T> collection, 
            IPersistentSource<TDTO> source,
            IFactory<T, TVMO> vmFactory,
            IFactory<T, TDTO> dtoFactory, 
            List<PersistencyOperations> supportedOperations) 
            : base(collection, source, vmFactory, dtoFactory, supportedOperations)
        {
            Manage();
        }

        #region IPersistableCatalog implementation
        public async void Load(bool suppressException = true)
        {
            if (_supportedOperations.Contains(PersistencyOperations.Load))
            {
                List<TDTO> objects = await _source.Load();
                _collection.ReplaceAll(_dtoFactory.CreateDomainObjects(objects), false);
            }
            else
            {
                if (!suppressException)
                {
                    throw new NotSupportedException();
                }
            }
        }

        public async void Save(bool suppressException = true)
        {
            if (_supportedOperations.Contains(PersistencyOperations.Save))
            {
                await _source.Save(_dtoFactory.CreateTransformedObjects(_collection.All));
            }
            else
            {
                if (!suppressException)
                {
                    throw new NotSupportedException();
                }
            }
        } 
        #endregion

        #region IManaged implementation
        /// <summary>
        /// Catalog registers Load/Save methods 
        /// at the PersistencyManager
        /// </summary>
        public virtual void Manage()
        {
            PersistencyManager.Instance.LoadDelegate += Load;
            PersistencyManager.Instance.SaveDelegate += Save;
        }
        #endregion
    }
}