﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace MobileCRM.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }

        public bool IsInitialized { get; set; }

        public async Task PushModalAsync(Page page)
        {
            if (Navigation != null)
                await Navigation.PushModalAsync(page);
        }

        public async Task PopModalAsync()
        {
            if (Navigation != null)
                await Navigation.PopModalAsync();
        }

        public async Task PushAsync(Page page)
        {
            if (Navigation != null)
                await Navigation.PushAsync(page);
        }

        public async Task PopAsync()
        {
            if (Navigation != null)
                await Navigation.PopAsync();
        }

        bool canLoadMore;
        /// <summary>
        /// Gets or sets the "IsBusy" property
        /// </summary>
        /// <value>The isbusy property.</value>
        public const string CanLoadMorePropertyName = "CanLoadMore";

        public bool CanLoadMore
        {
            get { return canLoadMore; }
            set { SetProperty(ref canLoadMore, value, CanLoadMorePropertyName); }
        }

        bool isBusy;
        /// <summary>
        /// Gets or sets the "IsBusy" property
        /// </summary>
        /// <value>The isbusy property.</value>
        public const string IsBusyPropertyName = "IsBusy";

        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value, IsBusyPropertyName); }
        }

        bool isModelLoaded;
        public const string IsModelLoadedPropertyName = "IsModelLoaded";

        public bool IsModelLoaded
        {
            get { return isModelLoaded; }
            set { SetProperty(ref isModelLoaded, value, IsModelLoadedPropertyName); }
        }

        string title = string.Empty;
        /// <summary>
        /// Gets or sets the "Title" property
        /// </summary>
        /// <value>The title.</value>
        public const string TitlePropertyName = "Title";

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value, TitlePropertyName); }
        }

        string subTitle = string.Empty;
        /// <summary>
        /// Gets or sets the "Subtitle" property
        /// </summary>
        public const string SubtitlePropertyName = "Subtitle";

        public string Subtitle
        {
            get { return subTitle; }
            set { SetProperty(ref subTitle, value, SubtitlePropertyName); }
        }

        string icon = null;
        /// <summary>
        /// Gets or sets the "Icon" of the viewmodel
        /// </summary>
        public const string IconPropertyName = "Icon";

        public string Icon
        {
            get { return icon; }
            set { SetProperty(ref icon, value, IconPropertyName); }
        }

        protected void SetProperty<U>(
            ref U backingStore, U value,
            string propertyName,
            Action onChanged = null,
            Action<U> onChanging = null)
        {
            if (EqualityComparer<U>.Default.Equals(backingStore, value))
                return;

            if (onChanging != null)
                onChanging(value);

            OnPropertyChanging(propertyName);

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }

        #region INotifyPropertyChanging implementation

        public event PropertyChangingEventHandler PropertyChanging;

        #endregion

        public void OnPropertyChanging(string propertyName)
        {
            if (PropertyChanging == null)
                return;

            PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}