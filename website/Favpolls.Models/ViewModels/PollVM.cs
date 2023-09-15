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

        public int SelectedOptionId { get; set; } = -1;

        public PollOption SelectedOption { get; set; }

        public string CurrentUserId { get; set; } = string.Empty;

        public PollSetting PollSetting { get; set; }

        public bool HasEnded { get; set; } = false;
    }
}
