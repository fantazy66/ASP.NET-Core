// ReSharper disable VirtualMemberCallInConstructor
namespace Shop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using Shop.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.IsDeleted = false;
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();

            this.ArtProducts = new HashSet<ArtProduct>();
            this.FavouriteArtProducts = new HashSet<ArtProduct>();
            this.FavouriteArtists = new HashSet<Artist>();
        }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        // Additional info
        public UserProfile UserProfile { get; set; }

        public string UserProfileId { get; set; }

        public DateTime ActivateSubscription { get; set; }

        public DateTime DeactivateSubscription { get; set; }

        public virtual ICollection<ArtProduct> ArtProducts { get; set; }

        public virtual ICollection<ArtProduct> FavouriteArtProducts { get; set; }

        public virtual ICollection<Artist> FavouriteArtists { get; set; }
    }
}
