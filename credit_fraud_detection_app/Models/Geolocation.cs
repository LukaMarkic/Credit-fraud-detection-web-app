using System.Globalization;

namespace credit_fraud_detection_app.Models
{
    public class Geolocation
    {
        private float latitude;
        private float longitude;

        public float Latitude { get { return latitude; } set { latitude = value; } }
        public float Longitude { get { return longitude; } set { longitude = value; } }

        public void SetValuesFromString(string value)
        {
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";

            if(value.Length > 0)
            {
                string valueClean = value.Remove(0, 1);
                valueClean = valueClean.Remove(valueClean.Length - 1, 1);
                string[] values = valueClean.Split(", ");

                latitude = float.Parse(values[0], NumberStyles.Any, ci);
                longitude = float.Parse(values[1], NumberStyles.Any, ci);
            }

            
        }

        public float CalculateDistanceFrom(Geolocation geolocation)
        {
            double result;
            float earthRadiusInMiles = 3958.8f;
            double deltaLatitude = degreesIntoRadinas(geolocation.Latitude - this.latitude);
            double deltaLongitude = degreesIntoRadinas(geolocation.Longitude - this.longitude);

            result = Math.Sin(deltaLatitude / 2) * Math.Sin(deltaLatitude / 2) +
                     Math.Cos(degreesIntoRadinas(this.latitude)) * Math.Cos(degreesIntoRadinas(geolocation.Latitude)) *
                     Math.Sin(deltaLongitude / 2) * Math.Sin(deltaLongitude / 2);
            
            result = Math.Atan2(Math.Sqrt(result), Math.Sqrt(1 - result)) * earthRadiusInMiles;
            return (float)result;
        }

        private double degreesIntoRadinas(double degree)
        {
            return degree * Math.PI / 180;
        }

    }
}
