using Apache.Ignite.Core;
using Apache.Ignite.Core.Client;
using FIXAPI_ClientAppNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIXAPI_ClientAppNetCore.Service
{
    public class IgniteService
    {
        private readonly IgniteClientConfiguration igniteClientConfiguration;
        public IgniteService()
        {
            igniteClientConfiguration = IntializeIgnteServer();
        }
        private IgniteClientConfiguration IntializeIgnteServer()
        {
            return new IgniteClientConfiguration
            {
                Endpoints = new[] { "127.0.0.1" }
            };
        }

        public void InsertFixMessageIntoIgnite(FixMessage message)
        {
            var ignite = Ignition.StartClient(igniteClientConfiguration);

            var cache = ignite.GetOrCreateCache<string, FixMessageCache>("TradeData");

            FixMessageCache cacheMessage = message.ToDto();

            cache.Put(cacheMessage.ID, cacheMessage);
        }
    }
}
