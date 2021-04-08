using AndreyToUsd.Data;
using AndreyToUsd.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AndreyToUsd.Services
{
    public interface IRatesService
    {
        string DefaultCurrencyPairs { get; }
        string Url { get; }

        string GetConvertorName();
        RateToUsd[] Rates { get; }
        Task<RateToUsd> GetRatio(string code);
        Task<FromTo> GetRatioForPair(string from, string to);

        Task<bool> Remove(string code);
    }
    public class RatesService : IRatesService, IDisposable
    {
        private readonly ToUsdContext Context;



        private readonly HttpClient Client;
        private readonly ILogger<RatesService> Log;
        public string Url { get; init; }
        public int MaxReadDelayMsec { get; init; } = 3600 * 1000;

        public string DefaultCurrencyPairs => throw new NotImplementedException();

        static ConcurrentDictionary<string, RateToUsd> Dict =
                     new ConcurrentDictionary<string, RateToUsd>(StringComparer.OrdinalIgnoreCase);
        public RatesService(ToUsdContext context,
                            ILogger<RatesService> logger,
                            IConfiguration config)
        {
            Client = new HttpClient();
            Log = logger;
            Context = context;
            Task.Run(AsyncInit);

        }

        /// <summary>
        /// Async read of 
        /// </summary>
        private async void AsyncInit()
        {
            try
            {
                var arr = await Context.Rates.ToListAsync();
                arr.ForEach(p => Dict.TryAdd(p.Code, p));

            }
            catch (Exception ex)
            {
                Log.LogError(ex.StackTrace);
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public RateToUsd[] GetAll()
        {
            return Dict.Values.ToArray();
        }

        public RateToUsd[] Rates => Dict.Values.ToArray();

        public string GetConvertorName()
        {
            return "Yahoo";
        }

        public async Task<RateToUsd> GetRatio(string code)
        {
            RateToUsd ret = null;
            DateTime dt0 = DateTime.Now;
            double ms = 0;
            if (!Dict.TryGetValue(code, out ret) ||
                (ms = (dt0 - ret.Stored).TotalMilliseconds) < MaxReadDelayMsec)
            {
                ret = await RetrieveFromHttp(code);
            }
            return ret;
        }

        public async Task<FromTo> GetRatioForPair(string from, string to)
        {
            try
            {
                RateToUsd[] arr = await Task.WhenAll<RateToUsd>(
                       GetRatio(from), GetRatio(to));
                var ret = new FromTo()
                {
                    From = arr[0],
                    To = arr[1],

                };

                return ret;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /*
         const ratesExchangeAxios = {
    "Realtime Currency Exchange Rate": {
        "1. From_Currency Code": "USD",
        "2. From_Currency Name": "United States Dollar",
        "3. To_Currency Code": "JPY",
        "4. To_Currency Name": "Japanese Yen",
        "5. Exchange Rate": "110.29100000",
        "6. Last Refreshed": "2021-04-06 08:16:01",
        "7. Time Zone": "UTC",
        "8. Bid Price": "110.29060000",
        "9. Ask Price": "110.29560000"
    }
}

         */

        readonly string[] alphavantage_secret_keys = [
                                               "55Y1508W05UYQN3G",
                                               "3MEYVIGY6HV9QYMI"
                                           ];
        private async Task<RateToUsd> RetrieveFromHttp(string code)
        {
            string url =
                "https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&" +
                "from_currency=USD&to_currency=" + code;

            string url0 = url + "&apikey=" + alphavantage_secret_keys[0];
            string url1 = url + "&apikey=" + alphavantage_secret_keys[1];

            var jsonBody = await HttpGet(url0);
            if(!jsonBody.Contains("1. From_Currency Code"))
            {
                jsonBody = await HttpGet(url1);
            }


            string[] pars1 = new string[] {
                    "\"4. To_Currency Name\":",
                    "\"5. Exchange Rate\":",
                    "\"6. Last Refreshed\":",
                    "\"8. Bid Price\":" +
                    "\"9. Ask Price\":"};

            string[] arrr = jsonBody.Split(pars1, StringSplitOptions.RemoveEmptyEntries);

            Func<string, string> f1 = (str) => 
                str.Trim().Split("\"".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];

            RateToUsd ret = new RateToUsd();
            ret.Code = code;
            ret.Name = f1(arrr[0]);
            ret.Rate = Double.Parse(arrr[1]);
            ret.LastRefreshed = DateTime.Parse(arrr[2]);
            ret.Ask = Double.Parse(arrr[3]);
            ret.Bid = Double.Parse(arrr[4]);
            ret.Stored = DateTime.Now;


            return ret;
          


        }
        private async Task<string> HttpGet(string urlIn)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(urlIn)
            };

            using (var response = await Client.SendAsync(request))
            {

                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        };

    private async Task<bool> Remove(string code)
{

}
    }
}
