using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.ViewModels
{
    public class OrderItemsViewModel
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        [Required]
        public int ProductId { get; set; }

        // Tezi propertyta sa na Product i moje da napravim ViewModel za tqh, no kogato samo gi displayvame, e po-dobra praktika direktno da se slojat tuk.
        public string ProductCategory { get; set; }
        public string ProductSize { get; set; }
        public string ProductTitle { get; set; }
        public string ProductArtist { get; set; }
        public string ProductArtId { get; set; }

    }
}
