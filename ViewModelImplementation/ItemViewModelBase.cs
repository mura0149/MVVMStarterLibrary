﻿using System;
using Windows.UI.Xaml;
using DataTransformation.Implementation;
using DataTransformation.Interfaces;
using ViewModel.Interfaces;

namespace ViewModel.Implementation
{
    /// <summary>
    /// Base class for Item ViewModel classes. It is assumed that any Item
    /// ViewModel object will wrap a DTO. The DTO will be strongly typed, 
    /// such that DTO-specific properties can be directly accessed for
    /// implementing the "generic" Item ViewModel properties.
    /// </summary>
    public abstract class ItemViewModelBase<TDO> : 
        TypedTransformedDataWrapper<TDO>, 
        IItemViewModelDescription,
        IItemViewModelImage 
        where TDO : class
    {
        #region Properties (override in model-specific item view model)
        /// <summary>
        /// Override this property to provide a string description of a DTO. 
        /// This description is then displayed for each item in the Master part of the view.
        /// </summary>
        public virtual string Description
        {
            get { throw new NotImplementedException("If you bind to Description in your Item Template (or otherwise), " +
                                                    "you should override the Description property in your ItemViewModel class."); }
        }

        /// <summary>
        /// Override this property to provide an image source for a DTO. 
        /// This image is then displayed for each item in the Master part of the view.
        /// </summary>
        public virtual string ImageSource
        {
            get
            {
                throw new NotImplementedException("If you bind to ImageSource in your Item Template (or otherwise), " +
                                                  "you should override the ImageSource property in your ItemViewModel class.");
            }
        }

        /// <summary>
        /// Override this property to provide a font size for the description. 
        /// </summary>
        public virtual int FontSize
        {
            get { return 18; }
        }

        /// <summary>
        /// Override this property to define the visibility of the image part
        /// </summary>
        public virtual Visibility ImageVisibility
        {
            get { return Visibility.Visible; }
        }

        /// <summary>
        /// Override this property to define the size of the image part
        /// </summary>
        public virtual int ImageSize
        {
            get { return 80; }
        }
        #endregion

        protected ItemViewModelBase(ITransformedData obj) : base(obj)
        {
        }
    }
}