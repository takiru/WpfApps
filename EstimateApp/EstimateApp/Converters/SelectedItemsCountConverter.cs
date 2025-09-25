using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Collections.ObjectModel;
using EstimateApp.Models;

namespace EstimateApp.Converters
{
    /// <summary>
    /// 選択された項目数を表示するためのコンバーター
    /// </summary>
    public class SelectedItemsCountConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<EstimateItem> items)
            {
                return items.Count(item => item.IsSelected);
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Boolean値を反転するコンバーター
    /// </summary>
    public class BooleanInverterConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }
            return false;
        }
    }

    /// <summary>
    /// 空文字列を判定するコンバーター
    /// </summary>
    public class IsEmptyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value?.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 数値が0より大きいかを判定するコンバーター
    /// </summary>
    public class IsPositiveNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue > 0;
            }
            if (value is int intValue)
            {
                return intValue > 0;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 日付が今日以降かを判定するコンバーター
    /// </summary>
    public class IsFutureDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateValue)
            {
                return dateValue.Date >= DateTime.Today;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 項目のバリデーション状態を色に変換するコンバーター
    /// </summary>
    public class ValidationStatusToBrushConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length < 4) return System.Windows.Media.Brushes.White;

            var itemCode = values[0]?.ToString();
            var itemName = values[1]?.ToString();
            var unitPrice = values[2];
            var quantity = values[3];

            bool hasError = false;

            // 必須項目チェック
            if (string.IsNullOrWhiteSpace(itemCode) || string.IsNullOrWhiteSpace(itemName))
            {
                hasError = true;
            }

            // 数値チェック
            if (unitPrice is decimal price && price < 0)
            {
                hasError = true;
            }

            if (quantity is int qty && qty <= 0)
            {
                hasError = true;
            }

            if (hasError)
            {
                return System.Windows.Media.Brushes.LightPink;
            }

            return System.Windows.Media.Brushes.White;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}