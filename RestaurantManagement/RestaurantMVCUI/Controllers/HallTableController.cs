using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestaurantEntity;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RestaurantMVCUI.Controllers
{
    public class HallTableController : Controller
    {
        private IConfiguration _configuration;
        public HallTableController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IActionResult>Index()
        {
            #region Getting All hallTables
            IEnumerable<HallTable> hallTables = null;
            using (HttpClient client = new HttpClient())
            {
                string endPoint = _configuration["WebApiBaseUrl"] + "HallTable/GetHallTables";
                using (var response = await client.GetAsync(endPoint))
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {   //dynamic viewbag we can create any variable name in run time
                        var result = await response.Content.ReadAsStringAsync();
                        hallTables = JsonConvert.DeserializeObject<IEnumerable<HallTable>>(result);
                    }
                }
            }
            return View(hallTables);
            #endregion
        }

    }
}
