using System;
using System.Collections.Generic;

namespace FacetedWorlds.MyCon.ImageUtilities
{
    public class ImageCache
    {
        private IDictionary<string, ImageCacheCell> _cellBySourceImageUrl =
            new Dictionary<string, ImageCacheCell>();
        private RequestQueue _requestQueue = new RequestQueue();

        public string SmallImageUrl(string sourceImageUrl)
        {
            if (String.IsNullOrEmpty(sourceImageUrl))
                return ImageCacheCell.DefaultSmallImageUrl;
            return GetCell(sourceImageUrl).SmallImageUrl;
        }

        public string LargeImageUrl(string sourceImageUrl)
        {
            if (String.IsNullOrEmpty(sourceImageUrl))
                return ImageCacheCell.DefaultSmallImageUrl;
            return GetCell(sourceImageUrl).LargeImageUrl;
        }

        public string OriginalImageUrl(string sourceImageUrl)
        {
            if (String.IsNullOrEmpty(sourceImageUrl))
                return null;
            return GetCell(sourceImageUrl).OriginalImageUrl;
        }

        private ImageCacheCell GetCell(string sourceImageUrl)
        {
            ImageCacheCell cell;
            if (!_cellBySourceImageUrl.TryGetValue(sourceImageUrl, out cell))
            {
                cell = new ImageCacheCell(sourceImageUrl, _requestQueue);
                _cellBySourceImageUrl.Add(sourceImageUrl, cell);
            }
            return cell;
        }
    }
}
