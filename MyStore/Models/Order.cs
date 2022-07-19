using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace MyStore.Models
{
    public class Order
    {
        [BindNever]
        public int OrderId { get; set; }
      
        [BindNever]
        public ICollection<CartLine> Lines { get; set; }
        public bool Shipped { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите имя")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Пожалуйста введите первую строку адреса")]
        public string Line1 { get; set; }   
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        [Required(ErrorMessage ="Пожалуйста введите город")]
        public string City { get; set; }
        public string Zip { get; set; }
        public bool GiftWrap { get; set; }
    }
}
