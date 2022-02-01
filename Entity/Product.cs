using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table(nameof(Product))]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
