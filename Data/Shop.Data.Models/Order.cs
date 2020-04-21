namespace Shop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Shop.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {

        public Order()
        {
            this.Items = new HashSet<OrderItem>();
        }

        public DateTime OrderDate { get; set; }

        public string OrderNumber { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

    }
}
