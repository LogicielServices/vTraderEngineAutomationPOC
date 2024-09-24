using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using TestProject.utils;

namespace TestProject
{
    [TestFixture]
    public class Tests
    {

        private int CreateOrderID;

        [SetUp]
        public void Before()
        {
            Console.WriteLine("SetUp 1");
            CreateOrderID = HelperFunctions.GenerateRandomNumber();
            try
            {
                FIXAPI_ClientAppNetCore.Program.Setup();
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
            Console.WriteLine("SetUp 2");
        }


        [Test, Order(1)]
        public void CreateOrder()
        {
            Console.WriteLine("CreateOrder 1");

            var message = HelperFunctions.CreateOrderMessage(CreateOrderID, 200, "META");

            HelperFunctions.SendFixMessage(message);

            HelperFunctions.ValidateResponse(CreateOrderID, 200, "META");
        }

        [Test, Order(2)]
        public void ModifyOrder()
        {
            var messageCreate = HelperFunctions.CreateOrderMessage(CreateOrderID, 200, "META");

            HelperFunctions.SendFixMessage(messageCreate);

            HelperFunctions.ValidateResponse(CreateOrderID, 200, "META");

            Console.WriteLine("ModifyOrder");

            Task.Delay(4000);

            var messageModify = HelperFunctions.ModifyOrderMessage(CreateOrderID + 1, CreateOrderID, 100, "META");

            HelperFunctions.SendFixMessage(messageModify);

            HelperFunctions.ValidateResponse(CreateOrderID + 1, 100, "META");
        }

        [Test, Order(3)]
        public void CancelOrder()
        {
            var messageCreate = HelperFunctions.CreateOrderMessage(CreateOrderID, 200, "META");
                
             HelperFunctions.SendFixMessage(messageCreate);

             HelperFunctions.ValidateResponse(CreateOrderID, 200, "META");

            Console.WriteLine("CancelOrder");

            Task.Delay(4000);

            var messageCancel = HelperFunctions.CancelOrderMessage(CreateOrderID + 1, CreateOrderID, 100, "META");

            HelperFunctions.SendFixMessage(messageCancel);

            HelperFunctions.ValidateResponse(CreateOrderID + 1, 100, "META");
        }

        [TearDown]
        public void After()
        {
            Console.WriteLine("TearDown 1");
        }
    }
}