using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}
