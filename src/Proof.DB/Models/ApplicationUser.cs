using Microsoft.AspNetCore.Identity;
using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poof.DB.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Pseudonym { get; set; } = "";
        public int PseudoNumber { get; set; }
        public byte[] Picture { get; set; } = new byte[0];
        public double Points { get; set; }
        public double BalanceScore { get; set; }
        public double GoodScore { get; set; }
        public string Friends { get; set; } = "";
    }
}
