using FIXAPI_ClientAppNetCore.Models;
using FIXAPI_ClientAppNetCore.Service;
using FIXAPINet;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace FIXAPI_ClientAppNetCore
{
    public class Program
    {
        public static IConfigurationRoot Configuration;
		public static Dictionary<string, string>  receiveMessage;
        public static String MessageResponseStr;

        public static void Setup()
        {
            try
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                 .AddJsonFile("appsettings.json", optional: true);

                Configuration = builder.Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Setup exception", ex.Message);
            }
        }

        public static void ShutDown()
        {
            try
            {
                var currentProcess = Process.GetCurrentProcess();

                Console.WriteLine($"Closing .NET Host process: {currentProcess.ProcessName} with ID: {currentProcess.Id}");


                foreach (var process in Process.GetProcessesByName("dotnet.exe"))
                {
                    Console.WriteLine($"Killing process: {process.ProcessName} with ID: {process.Id}");
                    process.Kill();  
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during teardown: {ex.Message}");
            }
        }

        static void Main(string[] args)
        {
            try
            {
                var CreateOrderID = "VCTY-TAK2-1049";
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                 .AddJsonFile("appsettings.json", optional: true);

                Configuration = builder.Build();

                var messageP = $"8=FIX.4.2\u00019=220\u000135=D\u000149=FIXAPISEND1\u000156=FIXAPIAUTOMATION\u000134=66\u0001115=VCGS\u000152=20240509-07:56:20.159\u00011=10012\u000111={CreateOrderID}\u000160=20240509-07:56:20.159\u000154=2\u000121=1\u000138=200\u000147=A\u0001109=10012\u000155=META\u000144=11.4700\u00019479=I\u000140=2\u00019303=S\u000159=5\u0001100=EDGX\u0001376={CreateOrderID}\u0001167=CS\u000110=192\u0001";


                InitOutAPI(messageP);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        public static void InitOutAPI(String messageParam)
        {

            ushort _adminPort = 0;
            List<STConnectionInfo> _connectionInfoList = null;


            LoadConfigurations(out _adminPort, out _connectionInfoList);

            //Registering the MsgCallback Events. //

            FIXOutAPIManager.OnMsgCallBack += new OnMsgCallBackDelegate(OnMsgCallBack_EventHandler);

            // Initializing the LSLFIX_OutProcessAPI_NET Library. //            


            int _result = FIXOutAPIManager.Initialize(_adminPort, _connectionInfoList.ToArray());
            if (_result != 0)
                return;

            string SenderCompID = Configuration["SenderCompID"].ToString();
            string TargetCompID = Configuration["TargetCompID"].ToString();
            string ConnectionID = Configuration["ConnectionID:ID"].ToString();

			string transactionTime = DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff");

            var message = Parse(messageParam);
            
            var oORderMesage = GetMessageObj(SenderCompID, TargetCompID, message, transactionTime);

            int a = FIXOutAPIManager.SendMessage(oORderMesage, Convert.ToUInt32(ConnectionID));

            //Thread thread = new Thread(simulateAndSendOrderMsgs);
            //thread.Start();

        }

        public static void InitOutAPI(Dictionary<string, string> messageParam)
        {

            ushort _adminPort = 0;
            List<STConnectionInfo> _connectionInfoList = null;


            LoadConfigurations(out _adminPort, out _connectionInfoList);

            FIXOutAPIManager.OnMsgCallBack += new OnMsgCallBackDelegate(OnMsgCallBack_EventHandler);

            int _result = FIXOutAPIManager.Initialize(_adminPort, _connectionInfoList.ToArray());
            if (_result != 0)
                return;

            string SenderCompID = Configuration["SenderCompID"].ToString();
            string TargetCompID = Configuration["TargetCompID"].ToString();
            string ConnectionID = Configuration["ConnectionID:ID"].ToString();

            string transactionTime = DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff");

            var oORderMesage = GetMessageObj(SenderCompID, TargetCompID, messageParam, transactionTime);

            int a = FIXOutAPIManager.SendMessage(oORderMesage, Convert.ToUInt32(ConnectionID));
        }

        public static bool Compare(Dictionary<string, string> fileMessage, Dictionary<string, string> CallbackMessage)
		{
			List<string> ignoreTags = new List<string>() { "52", "11", "34","60" };
			bool check = true;

			foreach (var keyvalue in CallbackMessage)
			{
				if (!ignoreTags.Contains(keyvalue.Key))
				{
					if (fileMessage.ContainsKey(keyvalue.Key))
					{
						if (keyvalue.Value != fileMessage[keyvalue.Key])
						{
							check = false;
							return check;
						}
					}

				}
			}
			return check;
		}

		public static Dictionary<string, string> Parse(string fixMessage)
        {

            Dictionary<string,string> fixMessageTagValue = new Dictionary<string, string>();
            try
            {

                string[] data = fixMessage.Split('', StringSplitOptions.RemoveEmptyEntries);
                foreach (string pair in data)
                {
                    string[] parts = pair.Split('=');
                    if (parts.Length == 2)
                    {
                        if (parts[0] != "10")
                        {
							fixMessageTagValue.Add(parts[0], parts[1]);
						}
                      
                    }
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return fixMessageTagValue;
        }

        static void simulateAndSendOrderMsgs()
        {
            string SenderCompID = Configuration["SenderCompID"].ToString();
            string TargetCompID = Configuration["TargetCompID"].ToString();
            string ConnectionID = Configuration["ConnectionID:ID"].ToString();
            string BoothID = Configuration["BoothID"].ToString();
            int ClOrdID = 1;

            Message oOrderMsg = new Message();


            // Header Fields
            oOrderMsg.HeaderFields.Add(FIX_MSG_TAGS.TAG_MSG_TYPE, new STField(FIX_MSG_TAGS.TAG_MSG_TYPE, "D", ENDataType.TYPE_STRING));
            oOrderMsg.HeaderFields.Add(FIX_MSG_TAGS.TAG_SENDER_COMP_ID, new STField(FIX_MSG_TAGS.TAG_SENDER_COMP_ID, SenderCompID, ENDataType.TYPE_STRING));
            oOrderMsg.HeaderFields.Add(FIX_MSG_TAGS.TAG_TARGET_COMP_ID, new STField(FIX_MSG_TAGS.TAG_TARGET_COMP_ID, TargetCompID, ENDataType.TYPE_STRING));
            oOrderMsg.HeaderFields.Add(FIX_MSG_TAGS.TAG_ON_BEHALF_OF_COMP_ID, new STField(FIX_MSG_TAGS.TAG_ON_BEHALF_OF_COMP_ID, BoothID, ENDataType.TYPE_STRING));
            oOrderMsg.HeaderFields.Add(FIX_MSG_TAGS.TAG_TARGET_LOCATION_ID, new STField(FIX_MSG_TAGS.TAG_TARGET_LOCATION_ID, "REG", ENDataType.TYPE_STRING));




            // Body Fields

            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_ACCOUNT, new STField(FIX_MSG_TAGS.TAG_ACCOUNT, "9999", ENDataType.TYPE_STRING));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_CLORD_ID, new STField(FIX_MSG_TAGS.TAG_CLORD_ID, ClOrdID.ToString(), ENDataType.TYPE_STRING));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_HANDL_INST, new STField(FIX_MSG_TAGS.TAG_HANDL_INST, "1", ENDataType.TYPE_CHAR));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_ORDER_QTY, new STField(FIX_MSG_TAGS.TAG_ORDER_QTY, "100", ENDataType.TYPE_QTY));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_ORD_TYPE, new STField(FIX_MSG_TAGS.TAG_ORD_TYPE, "2", ENDataType.TYPE_CHAR));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_PRICE, new STField(FIX_MSG_TAGS.TAG_PRICE, "1.000000", ENDataType.TYPE_PRICE));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_RULE_80_A, new STField(FIX_MSG_TAGS.TAG_RULE_80_A, "A", ENDataType.TYPE_CHAR));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_SIDE, new STField(FIX_MSG_TAGS.TAG_SIDE, "1", ENDataType.TYPE_CHAR));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_SYMBOL, new STField(FIX_MSG_TAGS.TAG_SYMBOL, "ZVZZT", ENDataType.TYPE_STRING));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_TIME_IN_FORCE, new STField(FIX_MSG_TAGS.TAG_TIME_IN_FORCE, "0", ENDataType.TYPE_CHAR));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_SETTLMNT_TYPE, new STField(FIX_MSG_TAGS.TAG_SETTLMNT_TYPE, "0", ENDataType.TYPE_CHAR));
            oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_CLIENT_ID, new STField(FIX_MSG_TAGS.TAG_CLIENT_ID, "TEST", ENDataType.TYPE_STRING));



            Thread.Sleep(1000);



            Console.WriteLine(" Starting message simulation...\n\n");



            while (true)
            {

                try
                {
                    string transactionTime = DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff");

                    if (!oOrderMsg.BodyFields.ContainsKey(FIX_MSG_TAGS.TAG_CLORD_ID))
                    {
                        oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_CLORD_ID, new STField(FIX_MSG_TAGS.TAG_CLORD_ID, ClOrdID.ToString(), ENDataType.TYPE_STRING));

                    }
                    else
                    {
                        oOrderMsg.BodyFields[FIX_MSG_TAGS.TAG_CLORD_ID] = new STField(FIX_MSG_TAGS.TAG_CLORD_ID, ClOrdID.ToString(), ENDataType.TYPE_STRING);
                    }





                    if (!oOrderMsg.BodyFields.ContainsKey(FIX_MSG_TAGS.TAG_TRANSACT_TIME))
                    {
                        oOrderMsg.BodyFields.Add(FIX_MSG_TAGS.TAG_TRANSACT_TIME, new STField(FIX_MSG_TAGS.TAG_TRANSACT_TIME, transactionTime, ENDataType.TYPE_UTC_TIMESTAMP));

                    }
                    else
                    {
                        oOrderMsg.BodyFields[FIX_MSG_TAGS.TAG_TRANSACT_TIME] = new STField(FIX_MSG_TAGS.TAG_TRANSACT_TIME, transactionTime, ENDataType.TYPE_UTC_TIMESTAMP);
                    }


                    ClOrdID++;



                    int a = FIXOutAPIManager.SendMessage(oOrderMsg, Convert.ToUInt32(ConnectionID));

                    Console.WriteLine("Message sent");


                    Thread.Sleep(5 * 1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


        }

        private static Message GetMessageObj(string SenderCompID, string TargetCompID, Dictionary<string, string> message,string transactTime)
        {
            List<uint> headerTags = new List<uint>() { 8,9,35, 49, 56,115,128,90,91,34,50,142,57,143,116,144,129,145,43,97,52,122,212,213,347,369,370 };


            Message oOrderMsg = new FIXAPINet.Message();

            foreach (var item in message)
            {
                uint tag = Convert.ToUInt16(item.Key);
                string value = item.Value;
                if (tag == 49)
                {
                    value = SenderCompID;
                }
                else if (tag == 56)
                {
                    value = TargetCompID;
                }

			    if (tag == 60)
				{
					value = transactTime;
				}

				if (headerTags.Contains(tag))
                {
                    if (!oOrderMsg.HeaderFields.ContainsKey(tag))
                    {
                        oOrderMsg.HeaderFields.Add(tag, new STField(tag, value, GetEnDataType(tag)));
                    }
                    else
                    {
                        oOrderMsg.HeaderFields[tag] = new STField(tag, value, GetEnDataType(tag));

                    }
                }
                else
                {
                    if (true)
                    {
                        if (!oOrderMsg.BodyFields.ContainsKey(tag))
                        {
                            oOrderMsg.BodyFields.Add(tag, new STField(tag, value, GetEnDataType(tag)));

                        }
                        else
                        {
                            oOrderMsg.BodyFields[tag] = new STField(tag, value, GetEnDataType(tag));

                        }
                    }

                }

            }
            return oOrderMsg;
        }

		private static ENDataType GetEnDataType(uint tag)
		{
			List<uint> typeCharTags = new List<uint>() { 21, 40, 47, 54, 59, 63 };
			List<uint> typeQtyTags = new List<uint>() { 38 };
			List<uint> typePriceTags = new List<uint>() { 44 };
			List<uint> typeutcTags = new List<uint>() { 60, 52, 122, 370 };
			List<uint> typeIntTags = new List<uint>() { 9, 34, 369 };
			List<uint> typeDataTags = new List<uint>() { 91, 213 };
			List<uint> typebooleanTags = new List<uint>() { 43, 97 };
			List<uint> typeExchangeTags = new List<uint>() { 100 };

			if (typeCharTags.Contains(tag))
			{
				return ENDataType.TYPE_CHAR;
			}
			else if (typeQtyTags.Contains(tag))
			{
				return ENDataType.TYPE_QTY;
			}
			else if (typePriceTags.Contains(tag))
			{
				return ENDataType.TYPE_PRICE;
			}
			else if (typeutcTags.Contains(tag))
			{
				return ENDataType.TYPE_UTC_TIMESTAMP;
			}
			else if (typeIntTags.Contains(tag))
			{
				return ENDataType.TYPE_INT;
			}
			else if (typeDataTags.Contains(tag))
			{
				return ENDataType.TYPE_DATA;
			}
			else if (typebooleanTags.Contains(tag))
			{
				return ENDataType.TYPE_BOOLEAN;
			}
			else if (typeExchangeTags.Contains(tag))
			{
				return ENDataType.TYPE_EXCHANGE;
			}

			return ENDataType.TYPE_STRING;
		}

		static void LoadConfigurations(out ushort adminPort, out List<STConnectionInfo> connectionInfoList)
        {
            adminPort = Convert.ToUInt16(Configuration["adminPort:Port"]);

            connectionInfoList = new List<STConnectionInfo>();

            STIPEndPoint _ipEndPoint = new STIPEndPoint();
            _ipEndPoint.IPEndPoint.Address = System.Net.IPAddress.Parse(Configuration["IPEndPoint:Address"]);
            _ipEndPoint.IPEndPoint.Port = Convert.ToInt32(Configuration["IPEndPoint:Port"]);
            _ipEndPoint.ConnID =  Configuration["ConnectionID:ID"];

            STConnectionInfo _connectionInfo = new STConnectionInfo();

            _connectionInfo.ConnectionID = Convert.ToUInt16(Configuration["ConnectionID:ID"]);
            _connectionInfo.ConnEndPoints.Add(_ipEndPoint); 
            connectionInfoList.Add(_connectionInfo);
        }

		private static string GetMessageStr(Message message)
		{
			string messageStr = "";
            foreach (var item in message.HeaderFields)
            {
                messageStr += item.Key + "=" + item.Value.Value + "\u0001";
            }

            foreach (var item in message.BodyFields)
			{
				messageStr += item.Key + "=" + item.Value.Value + "\u0001";
			}
			return messageStr;
		}

		public static void OnMsgCallBack_EventHandler(Message message)
        {
            MessageResponseStr = GetMessageStr(message);
			receiveMessage = Parse(MessageResponseStr);
            Console.WriteLine("Message Received FIX ------- " + receiveMessage);
            Console.WriteLine("Message Received STR ------- " + MessageResponseStr);

        }
    } 
}
