using System;
using System.ComponentModel.DataAnnotations;

namespace Online_Shopping.ViewModel
{
   public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage ="This field required")]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "This field required")]
        public int Stock { get; set; }

    }
}
