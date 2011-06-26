using System;
using System.Collections.Generic;

namespace FacetedWorlds.MyCon.ImageUtilities
{
    public class ImageCache
    {
        private IDictionary<string, ImageCacheCell> _cellBySourceImageUrl =
            new Dictionary<string, ImageCacheCell>();

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

        private ImageCacheCell GetCell(string sourceImageUrl)
        {
            lock (this)
            {
                ImageCacheCell cell;
                if (!_cellBySourceImageUrl.TryGetValue(sourceImageUrl, out cell))
                {
                    cell = new ImageCacheCell(sourceImageUrl);
                    _cellBySourceImageUrl.Add(sourceImageUrl, cell);
                }
                return cell;
            }
        }
    }
}
