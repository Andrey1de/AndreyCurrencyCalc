﻿using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using AndreyCurrenclyShared.Models;
using AndreyCurrecyBL.http;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System;
using AndreyCurrenclyShared.Text;
using System.Collections.Generic;

namespace AndreyCurrecyBL.Services
{

    public class CurrencyRatiosHTTPConsumer : ICurrencyRatiosHTTPConsumer
    {

        private readonly ILogger<CurrencyRatiosHTTPConsumer> Log;
        private readonly IBaseHttpConsumer Consumer;
        private readonly IConfiguration Config;
        private readonly HttpClient Client;
        private readonly string ServiceUrl;
        public string Name { get; private set; } = "";
        public CurrencyRatiosHTTPConsumer(
            ILogger<CurrencyRatiosHTTPConsumer> log,
            HttpClient client,
            // IBaseHttpConsumer consumer, 
            IConfiguration config)
        {
            Config = config;
            ServiceUrl = config.GetValue<string>("ServuceConverterUrl").Trim();
            if (!ServiceUrl.EndsWith('/'))
            {
                ServiceUrl += '/';
            }
            Client = client;
           // client.BaseAddress = new Uri(ServiceUrl);
            Consumer = new BaseHttpConsumer(Client);
            Log = log;
            Log.LogInformation($"Constructor ServiceURL={ServiceUrl}");

        }

        public async Task<CurrencyRatioADO> ConvertPair(string from, string to)
        {
            string url = $"{ServiceUrl}pair/{from}/{to}";
            CurrencyRatioADO ret = await Consumer.HttpGet<CurrencyRatioADO>(url);
            if (ret != null)
            {
                ret.oldRatio = ret.ratio;
                ret.status = 1;
            }
            return ret;
        }



        public async Task<List<CurrencyRatioADO>> GetDelimited(string delim)
        {
            string url = $"{ServiceUrl}delimited/{delim}";
            CurrencyRatioADO[] retArr = await Consumer.HttpGet<CurrencyRatioADO[]>(url);
            retArr = retArr ?? new CurrencyRatioADO[0];
            List<CurrencyRatioADO> list = new List<CurrencyRatioADO>(retArr);
            list.ForEach(p =>
            {
                p.oldRatio = p.ratio;
                p.status = 1;

            });
            return list;
        }

        public async Task<string> GetConvertorName()
        {
                if (Name.IsZ())
                {
                    string url = $"{ServiceUrl}convertorName";
                    Name = await Consumer.HttpGetRawString(url);
                    
                }
                
    
            return Name;

        }

     
    }
}
