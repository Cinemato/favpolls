using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.Models
{
    public class Poll
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Question { get; set; } = "";

        [Required]
        public string Code { get; set; } = "";

        public string? UserId { get; set; } = null;

        [ForeignKey("UserId")]    
        public IdentityUser? User { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
    }
}
