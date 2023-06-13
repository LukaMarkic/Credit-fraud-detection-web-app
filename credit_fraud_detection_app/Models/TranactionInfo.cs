namespace credit_fraud_detection_app.Models
{
    public class TransactionInfo
    {
        string distanceFromHome;
        string distanceFromLastTransaction;
        string ratioToMedianPurchasePrice;
        string repeatRetailer;
        string usedChip;
        string usedPinNumber;
        string onlineOrder;
        string fraud;

        public TransactionInfo(string distanceFromHome, string distanceFromLastTransaction, string ratioToMedianPurchasePrice, string repeatRetailer)
        {
            this.distanceFromHome = distanceFromHome;
            this.distanceFromLastTransaction = distanceFromLastTransaction;
            this.ratioToMedianPurchasePrice = ratioToMedianPurchasePrice;
            this.repeatRetailer = repeatRetailer;
            this.usedChip = "0";
            this.usedPinNumber = "0";
            this.onlineOrder = "1";
            this.fraud = "0";
        }

      
      
        public string[,] GetStringValues()
        {
            return new string[,] { { distanceFromHome, distanceFromLastTransaction, ratioToMedianPurchasePrice, 
                    repeatRetailer, usedChip, usedPinNumber, onlineOrder, fraud} };
        }

       
    }
}
