using ThatSneakerShopLaced.Areas.Identity.Data;

namespace ThatSneakerShopLaced.Models.ViewModels {
    public class OrderViewModel {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        public decimal OrderTotal { get; set; }
        public virtual Laced_User Customer { get; set; }
    }
}
