using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace FacetedWorlds.MyCon.ImageUtilities
{
    public class ImageCache
    {
        private IsolatedStorageFile _isoStore = IsolatedStorageFile.GetUserStoreForApplication();
        private IDictionary<string, ImageCacheCell> _cellBySourceImageUrl =
            new Dictionary<string, ImageCacheCell>();
        private RequestQueue _requestQueue = new RequestQueue();

        public CachedImage SmallImageUrl(string sourceImageUrl)
        {
            if (String.IsNullOrEmpty(sourceImageUrl))
                return new CachedImage { ImageUrl = ImageCacheCell.DefaultSmallImageUrl };
            return GetCell(sourceImageUrl).SmallImageUrl;
        }

        public CachedImage LargeImageUrl(string sourceImageUrl)
        {
            if (String.IsNullOrEmpty(sourceImageUrl))
                return new CachedImage { ImageUrl = ImageCacheCell.DefaultSmallImageUrl };
            return GetCell(sourceImageUrl).LargeImageUrl;
        }

        public CachedImage OriginalImageUrl(string sourceImageUrl)
        {
            if (String.IsNullOrEmpty(sourceImageUrl))
                return new CachedImage();
            return GetCell(sourceImageUrl).OriginalImageUrl;
        }

        private ImageCacheCell GetCell(string sourceImageUrl)
        {
            ImageCacheCell cell;
            if (!_cellBySourceImageUrl.TryGetValue(sourceImageUrl, out cell))
            {
                cell = new ImageCacheCell(sourceImageUrl, _requestQueue, _isoStore);
                _cellBySourceImageUrl.Add(sourceImageUrl, cell);
            }
            return cell;
        }
    }
}
