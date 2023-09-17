using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.Models
{
    public class PollVote
    {
        [Required]
        public int Id { get; set; }

        public int? PollId { get; set; }

        [ForeignKey("PollId")]
        public virtual Poll? Poll { get; set; }

        [Required]
        public int? PollOptionId { get; set; }

        [ForeignKey("PollOptionId")]
        public virtual PollOption PollOption { get; set; }

        [Required]
        public string UserIP { get; set; } = string.Empty;
    }
}
