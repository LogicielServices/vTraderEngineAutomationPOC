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
        public async Task CreateOrder()
        {
            Console.WriteLine("CreateOrder 1");

            var message = HelperFunctions.CreateOrderMessage(CreateOrderID, 200, "META");

            var response = await HelperFunctions.SendFixMessage(message);

            await HelperFunctions.ValidateResponse(CreateOrderID, 200, "META");
        }

        [Test, Order(2)]
        public async Task ModifyOrder()
        {
            await CreateOrder();

            Console.WriteLine("ModifyOrder");

            await Task.Delay(4000);

            var message = HelperFunctions.ModifyOrderMessage(CreateOrderID + 1, CreateOrderID, 100, "META");

            var response = await HelperFunctions.SendFixMessage(message);

            await HelperFunctions.ValidateResponse(CreateOrderID + 1, 100, "META");
        }

        [Test, Order(3)]
        public async Task CancelOrder()
        {
            await CreateOrder();

            Console.WriteLine("CancelOrder");

            await Task.Delay(4000);

            var message = HelperFunctions.CancelOrderMessage(CreateOrderID + 1, CreateOrderID, 100, "META");

            var response = await HelperFunctions.SendFixMessage(message);

            await HelperFunctions.ValidateResponse(CreateOrderID + 1, 100, "META");
        }

        [TearDown]
        public void After()
        {
            Console.WriteLine("TearDown 1");
        }
    }
}