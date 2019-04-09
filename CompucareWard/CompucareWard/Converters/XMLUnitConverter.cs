using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xamarin.Forms;

namespace CompucareWard.Converters
{
    class XmlUnitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var xml = value.ToString();
            XElement unitType = null;
            XElement unitValue = null;

            if (!string.IsNullOrEmpty(xml))
            {
                var parsedXml = XElement.Parse(xml);
                unitType = parsedXml.Elements("UnitType")?.FirstOrDefault();

                if (unitType == null)
                    return string.Empty;

                if (unitType.Value == "Custom")
                    unitValue = parsedXml.Elements("CustomUnit")?.FirstOrDefault();
                else
                    unitValue = parsedXml.Elements("UnitAbbreviation")?.FirstOrDefault();
            }

            if (unitValue == null)
                return string.Empty;
            else
                return unitValue.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
