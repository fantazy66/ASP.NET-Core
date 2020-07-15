namespace Shop.Data.Models
{
    using System;

    using Shop.Data.Common.Models;
    using Shop.Data.Models.Enum;

    public class Artist : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Nationality { get; set; }

        public string Biography { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DeathDate { get; set; }

        public Style Style { get; set; }
    }
}
