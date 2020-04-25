using Shop.Data.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Models
{
    public class Artist : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public string Nationality { get; set; }

        public string Biography { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime DeathDate { get; set; }
    }
}
