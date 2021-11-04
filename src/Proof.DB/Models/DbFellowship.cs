using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Poof.DB.Models
{ 
    public sealed class DbFellowship
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; } = "";
    }
}
