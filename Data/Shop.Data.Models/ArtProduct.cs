namespace Shop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Shop.Data.Common.Models;
    using Shop.Data.Models.Enum;

    public class ArtProduct : BaseDeletableModel<int>
    {
        public ArtProduct()
        {
            this.ImageLinks = new HashSet<ImageOfProduct>();
        }

        public ApplicationUser User { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Artist Artist { get; set; }

        public int ArtistId { get; set; }

        public string Title { get; set; }

        public string Size { get; set; }

        public decimal Price { get; set; }

        public string ArtDescription { get; set; }

        public virtual ICollection<ImageOfProduct> ImageLinks { get; set; }

        public DateTime ArtCreatedDate { get; set; }

        public Status Status { get; set; }

        public bool Framed { get; set; }

        public bool Signature { get; set; }

        public bool CertificateOfAuthenticity { get; set; }

        public Condition Condition { get; set; }

        public string Materials { get; set; }
    }
}
