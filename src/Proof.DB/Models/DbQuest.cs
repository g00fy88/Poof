using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Poof.DB.Models
{
    public sealed class DbQuest
    {
        [Required]
        public string Id { get; set; }

        public ApplicationUser Issuer { get; set; }
        public List<ApplicationUser> Applicants { get; set; } = new List<ApplicationUser>();
        public string Status { get; set; } = "published";
        public double CompletionTime { get; set; }
        public string Scope { get; set; } = "";
        public string Category { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string Note { get; set; } = "";
        public double Reward { get; set; }
        public bool HasEndDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime FinishDate { get; set; }
        public bool HasLocation { get; set; }
        public string Location { get; set; }
    }
}
