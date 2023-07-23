using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.Models
{
    public class PollOption
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Option { get; set; } = "";

        [Required]
        public int VoteCount { get; set; } = 0;

        [Required]
        public int? PollId { get; set; }

        [ForeignKey("PollId")]
        public virtual Poll Poll { get; set; }
    }
}