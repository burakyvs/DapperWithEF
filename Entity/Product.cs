using Entity.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    [Table("product")]
    public class Product : IEntity
    {
        [Column("id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
