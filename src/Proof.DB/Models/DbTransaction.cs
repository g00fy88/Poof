using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poof.DB.Models
{
    public sealed class DbTransaction
    {
        [Required]
        public string Id { get; set; }

        public string Title { get; set; } = "";
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string GiveSide { get; set; }
        public string GiveType { get; set; }
        public string TakeSide { get; set; }
        public string TakeType { get; set; }
    }
}
