using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Poof.DB.Models
{ 
    public sealed class DbMembership
    {
        [Required]
        public string Id { get; set; }

        public ApplicationUser Owner { get; set; }

        public DbFellowship Team { get; set; }

        public double Share { get; set; }

        public string Role { get; set; }
    }
}
