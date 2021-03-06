﻿namespace Shop.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Shop.Data.Common.Models;

    public class UserProfile : BaseDeletableModel<string>
    {
        [MaxLength(20)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [MaxLength(200)]
        public string Biography { get; set; }

        public string ProfilePhoto { get; set; }

        public string FacebookLink { get; set; }

        public string InstagramLink { get; set; }

        public string WebsiteLink { get; set; }
    }
}
