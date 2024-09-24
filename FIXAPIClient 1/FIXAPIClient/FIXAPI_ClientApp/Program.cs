using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FIXAPINet;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace FIXAPI_ClientApp
{
    class Program
    {
        public static IConfigurationRoot Configuration;

        static void Main(string[] args)
        {
            try
            {
                var builder = new ConfigurationBuilder()
                 .SetBasePath(Path.Combine(AppContext.BaseDirectory))
                 .AddJsonFile("appsettings.json", optional: true);

                Configuration = builder.Build();

                InitOutAPI();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        public static void InitOutAPI()
        {

            ushort _adminPort = 0;
            List<STConnectionInfo> _connectionInfoList = null;


            LoadConfigurations(out _adminPort, out _connectionInfoList);

            //Registering the MsgCallback Events. //

            FIXOutAPIManager.OnMsgCallBack += new OnMsgCallBackDelegate(OnMsgCallBack_EventHandler);

            // Initializing the LSLFIX_OutProcessAPI_NET Library. //            


            int _result = FIXOutAPIManager.Initialize(_adminPort, _connectionInfoList);
            Console.WriteLine(_result);
            if (_result != 0)
                return;


            Thread thread = new Thread(simulateAndSendOrderMsgs);
            thread.Start();

        }

        static void simulateAndSendOrderMsgs()
        {
            string SenderCompID = Configuration["SenderCompID"].ToString();
            string TargetCompID = Configuration["TargetCompID"].ToString();
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



                    int a = FIXOutAPIManager.SendMessage(oOrderMsg, 4u);

                    Console.WriteLine("Message sent");


                    Thread.Sleep(5 * 1000);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }


        }

        static void LoadConfigurations(out ushort adminPort, out List<STConnectionInfo> connectionInfoList)
        {
            adminPort = Convert.ToUInt16(Configuration["adminPort:Port"]);

            connectionInfoList = new List<STConnectionInfo>();

            STIPEndPoint _ipEndPoint = new STIPEndPoint();
            _ipEndPoint.IPEndPoint.Address = System.Net.IPAddress.Parse(Configuration["IPEndPoint:Address"]);
            _ipEndPoint.IPEndPoint.Port = Convert.ToInt32(Configuration["IPEndPoint:Port"]);

            STConnectionInfo _connectionInfo = new STConnectionInfo();

            _connectionInfo.ConnectionID = Convert.ToUInt16(Configuration["ConnectionID:ID"]);
            _connectionInfo.ConnEndPoints.Add(_ipEndPoint);
            connectionInfoList.Add(_connectionInfo);
        }


        public static void OnMsgCallBack_EventHandler(Message message)
        {
            try
            {
                Console.WriteLine($"Following Message Received : {JsonConvert.SerializeObject(message)}");
            }
            catch
            {

            }
        }
    }


}





