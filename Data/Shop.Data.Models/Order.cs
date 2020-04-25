namespace Shop.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Shop.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        public Order()
        {
            this.ArtProducts = new HashSet<ArtProduct>();
        }

        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string DeliveryAddress { get; set; }

        public OrderStatus Status { get; set; }

        public virtual ICollection<ArtProduct> ArtProducts { get; set; }
    }
}
