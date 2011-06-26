using System;
using System.Globalization;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FacetedWorlds.MyCon.Converters
{
    public class CachedImageUrlValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(ImageSource))
                throw new InvalidOperationException("CachedImageUrlValueConverter can only be used on ImageSource properties.");

            if (value == null)
                return value;

            string url = (string)value;
            if (!url.StartsWith("storage:"))
                return new BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));

            url = url.Substring("storage:".Length);
            using (IsolatedStorageFile isoStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isoStream = isoStore.OpenFile(url, FileMode.Open))
                {
                    BitmapImage bmp = new BitmapImage();
                    bmp.SetSource(isoStream);
                    return bmp;
                }
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
