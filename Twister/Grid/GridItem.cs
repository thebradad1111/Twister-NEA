﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Common;
using Common.Grid;
using Twister.Properties;

namespace Twister.Grid
{
    /// <summary>
    /// The non-view version of a grid item
    /// </summary>
    public class GridItem : Image, INotifyPropertyChanged
    {
        protected ImageSource sprite;
        protected Position location;

        public int CurrentWeighting { get; protected set; } = 1;
        //A*
        public int PreviousWeight { get; set; }
        public int NextWeight { get; set; }
        public int SumWeight { get; set; }
        public GridItem ParentItem { get; set; } //Points to previous item

        public virtual ImageSource Sprite
        {
            get => sprite;
        }

        public Position Position
        {
            get => location;
            set
            {
                location = value;                
            }
        }

        protected string absoluteLocation = "";

        /// <summary>
        /// Converts a relative string into a bitmap that is stored in sprite
        /// </summary>
        protected ImageSource SetupSprite()
        {
            BitmapImage src = new BitmapImage();
            src.BeginInit();
            src.UriSource = new Uri(string.IsNullOrEmpty(absoluteLocation) ? $@"{App.AppDir}\Assets\Error.png" : absoluteLocation, UriKind.Absolute);
            src.CacheOption = BitmapCacheOption.OnLoad;
            src.EndInit();
            CachedBitmap cachedSrc = new CachedBitmap(src, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            return cachedSrc;
        }

        /// <summary>
        /// When the item has finished loading set it up
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.Source = SetupSprite();
            this.Height = Constants.GRID_ITEM_WIDTH;
            this.Width = Constants.GRID_ITEM_WIDTH;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}