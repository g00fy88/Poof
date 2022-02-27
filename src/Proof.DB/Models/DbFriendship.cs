using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Poof.DB.Models
{ 
    public sealed class DbFriendship
    {
        [Required]
        public string Id { get; set; }

        public string Requester { get; set; }

        public string Friend { get; set; }

        public string Status { get; set; }
    }
}
