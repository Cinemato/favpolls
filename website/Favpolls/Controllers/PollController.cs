using Microsoft.AspNetCore.Mvc;
using Favpolls.Models;
using Favpolls.DataAccess.Repository.IRepository;
using Favpolls.Models.ViewModels;
using System.Security.Claims;

namespace Favpolls.Controllers
{
    public class PollController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public PollController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PollVM pollVM) {
            pollVM.PollOptions.ForEach(p => p.Poll = pollVM.Poll);
            pollVM.PollSetting.Poll = pollVM.Poll;

            _unitOfWork.PollOption.AddRange(pollVM.PollOptions);
            _unitOfWork.PollSetting.Add(pollVM.PollSetting);

            var code = GenerateCode(6);
            pollVM.Poll.Code = code;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                pollVM.Poll.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            _unitOfWork.Save();

            return View("CreateSuccess", pollVM.Poll);
        }

        public IActionResult Join(string code)
        {
            Poll poll = _unitOfWork.Poll.Get(p => p.Code == code);
            List<PollOption> pollOptions = _unitOfWork.PollOption.GetAll(o => o.PollId == poll.Id).ToList();
            PollSetting pollSetting = _unitOfWork.PollSetting.Get(s => s.PollId == poll.Id);

            var total = 0;
            foreach (var option in pollOptions)
            {
                total += option.VoteCount;
            }

            PollVM pollVM = new PollVM()
            {
                Poll = poll,
                PollOptions = pollOptions,
                TotalVotes = total,
                PollSetting = pollSetting
            };

            if (pollSetting.VoteLimit <= total || pollSetting.Deadline < DateTime.Now)
            {
                pollVM.HasEnded = true;
            }

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                pollVM.CurrentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            return View(pollVM);
        }

        public IActionResult AccountPolls()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                List<PollVM> pollVMs = new List<PollVM>();
                List<Poll> polls = _unitOfWork.Poll.GetAll(p => p.UserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
                foreach (var poll in polls)
                {
                    List<PollOption> pollOptions = _unitOfWork.PollOption.GetAll(o => o.PollId == poll.Id).ToList();

                    var total = 0;
                    PollOption topVote = pollOptions[0];

                    foreach (var option in pollOptions)
                    {
                        total += option.VoteCount;
                        if (option.VoteCount > topVote.VoteCount)
                        {
                            topVote = option;
                        }
                    }

                    PollVM pollVM = new PollVM()
                    {
                        Poll = poll,
                        PollOptions = pollOptions,
                        TotalVotes = total,
                        SelectedOption = topVote
                    };

                    pollVMs.Add(pollVM);
                }

                return View(pollVMs);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitVote(PollVM pollVM)
        {
            Poll poll = _unitOfWork.Poll.Get(p => p.Id == pollVM.Poll.Id);
            pollVM.Poll = poll;

            List<PollOption> pollOptions = _unitOfWork.PollOption.GetAll(o => o.PollId == poll.Id).ToList();       
            pollVM.PollOptions = pollOptions;

            PollOption selectedOption = _unitOfWork.PollOption.Get(o => o.Id == pollVM.SelectedOptionId);
            selectedOption.VoteCount += 1;
            pollVM.SelectedOption = selectedOption;

            _unitOfWork.Save();

            return View(pollVM);
        }

        public static string GenerateCode(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var code = new String(stringChars);
            return code;
        }
    }
}
