using AndreyCurrenclyShared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AndreyCurrencyBL.Controllers
{
    public interface ICurrencyRatiosController
    {
        Task<ActionResult<CurrencyRatioADO>> ConvertPair(string from, string to);
        ActionResult<string> GetDefault();
        Task<ActionResult<CurrencyRatioADO[]>> GetDelimited(string delim);
    }
}