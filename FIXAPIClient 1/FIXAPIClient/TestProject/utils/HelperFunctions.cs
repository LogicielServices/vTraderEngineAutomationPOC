using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.utils
{
    internal class HelperFunctions
    {
        public static int GenerateRandomNumber()
        {
            int randomNumber = 0;
            Random random = new Random();
            randomNumber = random.Next(1000, 10000);
            Console.WriteLine(randomNumber);
            return randomNumber;
        }
        public static string GetFormattedCurrentDateTime()
        {
            DateTime now = DateTime.Now;

            string formattedDateTime = now.ToString("yyyyMMdd-HH:mm:ss.fff");

            return formattedDateTime;
        }

        public static string CreateOrderMessage(int orderId, int quantity , string symbol)
        {
            return $"8=FIX.4.2\u00019=220\u000135=D\u000149=FIXAPISEND1\u000156=FIXAPIAUTOMATION\u000134=66\u0001115=VCGS\u000152={GetFormattedCurrentDateTime()}\u00011=10012\u000111={orderId}\u000160={GetFormattedCurrentDateTime()}\u000154=2\u000121=1\u000138={quantity}\u000147=A\u0001109=10012\u000155={symbol}\u000144=11.4700\u000140=2\u00019303=S\u000159=5\u0001100=EDGX\u0001376={orderId}\u0001167=CS\u000110=192\u0001";
        }
        public static string ModifyOrderMessage(int orderId, int previousOrderId,  int quantity, string symbol)
        {
            return $"8=FIX.4.2\u00019=220\u000135=G\u000149=FIXAPISEND1\u000156=FIXAPIAUTOMATION\u000134=66\u0001115=VCGS\u000152={GetFormattedCurrentDateTime()}\u00011=10012\u000111={orderId}\u000160={GetFormattedCurrentDateTime()}\u000154=1\u000121=1\u000138={quantity}\u000147=A\u0001109=10012\u000155={symbol}\u000140=1\u000159=1\u0001100=EDGX\u000141={previousOrderId}\u000110=192\u0001";
        }
        public static string CancelOrderMessage(int orderId, int previousOrderId, int quantity, string symbol)
        {
            return $"8=FIX.4.2\u00019=220\u000135=F\u000149=FIXAPISEND1\u000156=FIXAPIAUTOMATION\u000134=66\u0001115=VCGS\u000152={GetFormattedCurrentDateTime()}\u00011=10012\u000111={orderId}\u000160={GetFormattedCurrentDateTime()}\u000154=1\u000121=1\u000138={quantity}\u000147=A\u0001109=10012\u000155={symbol}\u000140=1\u000159=1\u0001100=EDGX\u000141={previousOrderId}\u000110=192\u0001";
        }

        public static void SendFixMessage(string message)
        {
            try
            {
                FIXAPI_ClientAppNetCore.Program.InitOutAPI(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public static void ValidateResponse(int expectedOrderId, int expectedQuantity, string symbol)
        {
            for (int attempt = 0; attempt < 5; attempt++)
            {
                if (FIXAPI_ClientAppNetCore.Program.MessageResponseStr != null && FIXAPI_ClientAppNetCore.Program.receiveMessage != null)
                {
                    Console.WriteLine("Valid response received.");

                    Console.WriteLine("In Validate - " + FIXAPI_ClientAppNetCore.Program.receiveMessage);
                    Assert.NotNull(FIXAPI_ClientAppNetCore.Program.receiveMessage);
                    Assert.AreEqual("8", FIXAPI_ClientAppNetCore.Program.receiveMessage["35"]);
                    Assert.AreEqual("FIXAPISEND1", FIXAPI_ClientAppNetCore.Program.receiveMessage["49"]);
                    Assert.AreEqual("FIXAPIAUTOMATION", FIXAPI_ClientAppNetCore.Program.receiveMessage["56"]);
                    Assert.AreEqual("10012", FIXAPI_ClientAppNetCore.Program.receiveMessage["1"]);
                    Assert.AreEqual(expectedOrderId.ToString(), FIXAPI_ClientAppNetCore.Program.receiveMessage["11"]);
                    Assert.AreEqual(expectedQuantity.ToString(), FIXAPI_ClientAppNetCore.Program.receiveMessage["38"]);
                    Assert.AreEqual(symbol, FIXAPI_ClientAppNetCore.Program.receiveMessage["55"]);
                    FIXAPI_ClientAppNetCore.Program.MessageResponseStr = null;
                    FIXAPI_ClientAppNetCore.Program.receiveMessage = null;
                    return;
                }

                Console.WriteLine($"Attempt {attempt + 1} failed. Retrying in {2000 / 1000} seconds...");
                Task.Delay(2000);
            }
            Console.WriteLine("Max retries reached. No valid response received.");
        }
    }
}
