using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ThatSneakerShopLaced.Models;

namespace ThatSneakerShopLaced.Api.Models
{
    public class StockMangement : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<Shoe> Shoes { get; set; }

        public StockMangement(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task OnGetAsync()
        {
            var client = _clientFactory.CreateClient("stockmanagement");
            var response = await client.GetAsync("api/StockManagment/Shoes");
            Shoes = JsonConvert.DeserializeObject<IEnumerable<Shoe>>(await response.Content.ReadAsStringAsync());
        }
    }
}
