using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using credit_fraud_detection_app;
using credit_fraud_detection_app.Models;
using System.Globalization;

namespace credit_fraud_detection_app.Pages
{
    public class CheckoutModel : PageModel
    {
        public bool? isFraud;
        public string name;

        public void OnGet()
        {
            isFraud = null;
        }

        public void OnPost()
        {
            
            TransactionCkeckoutCall();

        }

        private void TransactionCkeckoutCall()
        {
            string repeatRetailer;
            float medianPurchase;
            float purchase;
            float purchaseRatio;
            string purchaseRatioStringRepresetantion;
            string distandeFromHome;
            string distanceFromLastTransaction;
            Geolocation homeGeolocation = new Geolocation();
            Geolocation currentGeolocation = new Geolocation();
            Geolocation lastLocationGeolocation = new Geolocation();

            homeGeolocation.SetValuesFromString(Request.Form["home-LatLng"]);
            currentGeolocation.SetValuesFromString(Request.Form["current-LatLng"]);
            lastLocationGeolocation.SetValuesFromString(Request.Form["last-location-LatLng"]);
            name = Request.Form["first-name-input"];

            repeatRetailer = GetCheckBoxValue(Request.Form["repeat-retailer"]);
            purchase = GetFlaotValueOfInput(Request.Form["purchase-value-form"]);
            medianPurchase = GetFlaotValueOfInput(Request.Form["payment-mean"]);
            distandeFromHome = currentGeolocation.CalculateDistanceFrom(homeGeolocation).ToString("0.0000", CultureInfo.InvariantCulture);
            distanceFromLastTransaction = currentGeolocation.CalculateDistanceFrom(lastLocationGeolocation).ToString("0.0000", CultureInfo.InvariantCulture);


            if (medianPurchase != 0.0f)
            {
                purchaseRatio = purchase / medianPurchase;
            }
            else
            {
                purchaseRatio = 1.0f;
            }

            purchaseRatioStringRepresetantion = purchaseRatio.ToString("0.0000", CultureInfo.InvariantCulture);
            Console.WriteLine("Value is: " + purchaseRatioStringRepresetantion);
            Console.WriteLine(Request.Form["home-LatLng"]);
            Console.WriteLine(Request.Form["last-location-LatLng"]);
            Console.WriteLine("Current " + Request.Form["current-LatLng"]);
            Console.WriteLine("Is checked: " + repeatRetailer);
            Console.WriteLine("Distance from home: " + distandeFromHome + " mil");
            Console.WriteLine("Distance from last transaction: " + distanceFromLastTransaction + " mil");

            TransactionInfo transaction = new TransactionInfo(distandeFromHome,distanceFromLastTransaction, purchaseRatioStringRepresetantion, repeatRetailer);

            string? fraud = Utility.RunTransactionCheckTask(transaction);

            SetFraudStauts(fraud);
        }

        private void SetFraudStauts(string? fraud)
        {
            if (fraud == null)
            {
                Console.WriteLine("Error with checking fraud.");
            }
            else if (fraud == "0")
            {
                isFraud = false;
            }
            else
            {
                isFraud = true;
            }
        }

        private float GetFlaotValueOfInput(string value)
        {
            CultureInfo ci = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ci.NumberFormat.CurrencyDecimalSeparator = ".";
            float result;
            try
            {
                result = float.Parse(value, NumberStyles.Any, ci);
                
            }
            catch (Exception e)
            {
                result = 0.0f;
            }

            return result;
        }

        private string GetCheckBoxValue(string value)
        {
            return (value is not null) ? "1" : "0";
        }


    }
}
