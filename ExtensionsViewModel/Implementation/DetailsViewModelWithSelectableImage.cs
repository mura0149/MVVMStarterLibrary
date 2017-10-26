﻿using System.Collections.ObjectModel;
using DataTransformation.Interfaces;
using ExtensionsServices.Implementation;
using Images.Interfaces;
using ViewModel.Implementation;

namespace ExtensionsViewModel.Implementation
{
    /// <summary>
    /// Base class for a Details ViewModel class containing a image, 
    /// which can be selected from a set of Image objects. The Image
    /// objects are provided by the Images service. The image is thus 
    /// identified by a numeric key (ImageKey).
    /// </summary>
    public abstract class DetailsViewModelWithSelectableImage<TDO> : DetailsViewModelBase<TDO> 
        where TDO : class
    {
        private string _tag;

        protected DetailsViewModelWithSelectableImage(ITransformedData obj, string tag)
            : base(obj)
        {
            _tag = tag;
        }

        /// <summary>
        /// Gets the Image objects corresponding to the specified tag. The Images
        /// service must be populated with Image objects matching the specified tag.
        /// </summary>
        public ObservableCollection<IImage> ImageCollection
        {
            get { return ServiceProvider.Images.GetObservableImageCollection(_tag); }
        }

        /// <summary>
        /// Tracks the Image object currently selected.
        /// </summary>
        public IImage ImageSelected
        {
            get { return ServiceProvider.Images.Read(ImageKey); }
            set
            {
                if (value != null)
                {
                    ImageKey = value.Key;
                }

                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Specific implementation of the Key property is done in sub-classes,
        /// since the specific source for the key value may vary.
        /// </summary>
        public abstract int ImageKey { get; set; }
    }
}