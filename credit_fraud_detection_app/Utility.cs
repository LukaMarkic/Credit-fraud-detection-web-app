
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using credit_fraud_detection_app.Models;
using System.ComponentModel;




namespace credit_fraud_detection_app
{
    static public class Utility
    {
        private static string? isFraud;
        private static string apiResponse = "";

        public static string? RunTransactionCheckTask(TransactionInfo transaction)
        {
            InvokeRequestResponseService(transaction).Wait();
            return isFraud;
        }

        public static string GetApiResponse(TransactionInfo transaction)
        {
            InvokeRequestResponseService(transaction).Wait();
            return apiResponse;
        }
        static async Task InvokeRequestResponseService(TransactionInfo transaction)
        {
            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"distance_from_home", "distance_from_last_transaction", "ratio_to_median_purchase_price", "repeat_retailer", "used_chip", "used_pin_number", "online_order", "fraud"},
                                Values = transaction.GetStringValues()
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };
                const string apiKey = "yILoowqSm/CeGhGv37VlxalFu7EZEUS5N9LwazbSj594RDCV6jq/Wm7ZyyrHNQWrNG8ZwUJ0W+2b+AMC4cCS0Q=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/8f712c085ae242aabbf6d3880779f3a4/services/050df304a4f44bb2a44453fef5992a86/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                 
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                    string fraud = result.Substring(148, 1);
                    string fraudProbability = result.Substring(152, (result.Length-7) - 152);
                    Console.WriteLine("Fraud: {0}", fraud);
                    Console.WriteLine($"Probability: {fraudProbability}");
                    apiResponse = "{\n \"fraud\": "+ fraud +",\n" +
                        $" \"fraudProbability\": "+ fraudProbability + "\n}";
                    isFraud = fraud;
                }
                else
                {
                    Console.WriteLine(string.Format("The request failed with status code: {0}", response.StatusCode));

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }

        
    }
}
