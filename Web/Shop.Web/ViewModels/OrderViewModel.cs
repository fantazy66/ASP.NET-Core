using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.ViewModels
{
    public class OrderViewModel
    {

        //public OrderViewModel()
        //{
        //    this.Items = new HashSet<OrderItemsViewModel>();
        //}

        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [MinLength(4)]
        public string OrderNumber { get; set; }

        public ICollection<OrderItemsViewModel> Items { get; set; }


    }
}
