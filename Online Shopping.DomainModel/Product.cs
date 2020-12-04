using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Online_Shopping.DomainModel
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
       
    }
}
