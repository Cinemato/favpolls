using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favpolls.Models.ViewModels
{
    public class PollVM
    {
        public Poll Poll { get; set; }

        public List<PollOption> PollOptions { get; set; }

        public int TotalVotes { get; set; } = 0;
    }
}
