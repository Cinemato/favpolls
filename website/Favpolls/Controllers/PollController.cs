using Microsoft.AspNetCore.Mvc;
using Favpolls.Models;
using Favpolls.DataAccess.Repository.IRepository;
using Favpolls.Models.ViewModels;
using System.Security.Claims;
using System.Net;
using System.Net.Sockets;
using Microsoft.IdentityModel.Tokens;
using Favpolls.Utility;

namespace Favpolls.Controllers
{
    public class PollController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGoogleReCaptcha _googleReCaptcha;
         

        public PollController(IUnitOfWork unitOfWork, IGoogleReCaptcha googleReCaptcha)
        {
            _unitOfWork = unitOfWork;
            _googleReCaptcha = googleReCaptcha;
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
            pollVM.PollOptions = pollVM.PollOptions.FindAll(o => o.Option != null);
            pollVM.PollOptions.ForEach(p => p.Poll = pollVM.Poll);
            pollVM.PollSetting.Poll = pollVM.Poll;

            _unitOfWork.PollOption.AddRange(pollVM.PollOptions);
            _unitOfWork.PollSetting.Add(pollVM.PollSetting);

            var code = GenerateCode(6);

            while (_unitOfWork.Poll.GetAll(p => p.Code == code).Count() > 0) 
            {
                code = GenerateCode(6);
            }

            pollVM.Poll.Code = code;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                pollVM.Poll.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            pollVM.Poll.CreatedDate = DateTime.Now;

            _unitOfWork.Save();

            return View("CreateSuccess", pollVM.Poll);
        }

        public IActionResult Join(string code)
        {
            Poll poll = _unitOfWork.Poll.Get(p => p.Code == code);

            if (poll == null)
            {
                TempData["error"] = "This poll doesn't exist! Try again.";
                return View("Index");
            }

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

            if (pollSetting.VoteLimit <= total || pollSetting.Deadline <= DateTime.UtcNow)
            {
                pollVM.HasEnded = true;
            }

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                pollVM.CurrentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            string ip = GetUserIP();
            PollVote pollVote = _unitOfWork.PollVote.Get(v => v.PollId == poll.Id && v.UserIP == ip);

            if (pollVote != null)
            {
                PollOption selectedOption = _unitOfWork.PollOption.Get(o => o.Id == pollVote.PollOptionId);
                pollVM.SelectedOption = selectedOption;
            }

            return View(pollVM);
        }

        public IActionResult AccountPolls()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                List<PollVM> pollVMs = new List<PollVM>();
                List<Poll> polls = _unitOfWork.Poll.GetAll(p => p.UserId == this.User.FindFirstValue(ClaimTypes.NameIdentifier)).ToList();
                if (polls.Count() > 0)
                {
                    foreach (var poll in polls)
                    {
                        List<PollOption> pollOptions = _unitOfWork.PollOption.GetAll(o => o.PollId == poll.Id).ToList();
                        PollSetting pollSetting = _unitOfWork.PollSetting.Get(s => s.PollId == poll.Id);

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
                            SelectedOption = topVote,
                            PollSetting = pollSetting
                        };

                        if (pollSetting.VoteLimit <= total || pollSetting.Deadline <= DateTime.Now)
                        {
                            pollVM.HasEnded = true;
                        }

                        pollVMs.Add(pollVM);
                    }

                    pollVMs = pollVMs.OrderByDescending(pv => pv.Poll.CreatedDate).ToList();
                }
                else
                {
                    TempData["empty"] = "You don't have any polls.";
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

            PollSetting pollSetting = _unitOfWork.PollSetting.Get(s => s.PollId == poll.Id);

            if (pollSetting.HasCaptcha)
            {
                var token = Request.Form["g-recaptcha-response"];
                bool result = _googleReCaptcha.ValidateResponse(token);

                if (token[0].IsNullOrEmpty() || !result)
                {
                    TempData["error"] = "You must complete the captcha!";
                    return RedirectToAction("Join", new { code = poll.Code });
                }
            }     

            if (pollVM.SelectedOptionId == -1)
            {
                TempData["error"] = "You must select an option!";
                return RedirectToAction("Join", new { code = poll.Code });
            }

            List<PollOption> pollOptions = _unitOfWork.PollOption.GetAll(o => o.PollId == poll.Id).ToList();       
            pollVM.PollOptions = pollOptions;

            PollOption selectedOption = _unitOfWork.PollOption.Get(o => o.Id == pollVM.SelectedOptionId);
            selectedOption.VoteCount += 1;
            pollVM.SelectedOption = selectedOption;

            string ip = GetUserIP();

            PollVote pollVote = new PollVote
            {
                PollId = poll.Id,
                PollOptionId = selectedOption.Id,
                UserIP = ip
            };

            _unitOfWork.PollVote.Add(pollVote);

            _unitOfWork.Save();

            return View(pollVM);
        }

        public string GenerateCode(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[length];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var code = new string(stringChars);
            return code;
        }

        public string GetUserIP()
        {
            string ip = Response.HttpContext.Connection.RemoteIpAddress.ToString();
            
            if(ip == "::1")
            {
                ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault().ToString();
            }

            return ip;
        }
    }
}
