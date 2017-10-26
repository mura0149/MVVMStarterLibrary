﻿using System.Collections.Generic;
using DataTransformation.Interfaces;
using InMemoryStorage.Implementation;
using InMemoryStorage.Interfaces;
using Persistency.Interfaces;
using WebAPI;

namespace ExtensionsModel.Implementation
{
    /// <summary>
    /// This class injects specific dependencies into the PersistentCollection class:
    /// 1) Data is read from a RESTful web service, supporting Load, Create and Delete
    /// 2) The InMemoryCollection implementation is used.
    /// </summary>
    public abstract class WebAPIPersistableCatalog<T, TDO> : PersistentCollection<T>
        where T : class, IStorable 
        where TDO : ITransformedData
    {
        protected WebAPIPersistableCatalog(
            string url, 
            string apiID, 
            ITransformedDataFactory<T> viewDataFactory, 
            ITransformedDataFactory<T> sourceDataFactory)
            : base(new WebAPISource<T, TDO>(sourceDataFactory, url, apiID), 
                   new InMemoryCollection<T>(),
                   viewDataFactory,
                   new List<PersistencyOperations> { PersistencyOperations.Load, PersistencyOperations.Create, PersistencyOperations.Delete })
        {
        }
    }
}