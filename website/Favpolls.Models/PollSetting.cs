using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.Models
{
    public class PollSetting
    {
        [Required]
        public int Id { get; set; }

        public DateTime? Deadline { get; set; }

        public int? VoteLimit { get; set; }

        [Required]
        public bool HideResults { get; set; }

        [Required]
        public bool HasCaptcha { get; set; }

        [Required]
        public int? PollId { get; set; }

        [ForeignKey("PollId")]
        public virtual Poll Poll { get; set; }
    }
}
