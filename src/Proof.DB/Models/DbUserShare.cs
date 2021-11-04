using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Poof.DB.Models
{
    public sealed class DbUserShare
    {
        [Required]
        public string Id { get; set; }

        public ApplicationUser User { get; set; }
        public double Share { get; set; }
    }
}
