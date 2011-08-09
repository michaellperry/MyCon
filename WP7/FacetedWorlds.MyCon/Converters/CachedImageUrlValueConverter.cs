using System;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FacetedWorlds.MyCon.ImageUtilities;

namespace FacetedWorlds.MyCon.Converters
{
    public class CachedImageUrlValueConverter : IValueConverter
    {
        private static IsolatedStorageFile _isoStore = IsolatedStorageFile.GetUserStoreForApplication();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("CachedImageUrlValueConverter can only be used on ImageSource properties.");

            if (value == null)
                return value;

            if (!(value is CachedImage))
                return null;
            CachedImage cachedImage = (CachedImage)value;
            if (cachedImage.Access != null)
                cachedImage.Access();
            string url = cachedImage.ImageUrl;
            if (url == null)
                return null;
            if (!url.StartsWith("storage:"))
                return new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));

            url = url.Substring("storage:".Length);
            using (IsolatedStorageFileStream isoStream = _isoStore.OpenFile(url, FileMode.Open))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.SetSource(isoStream);
                return bmp;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
