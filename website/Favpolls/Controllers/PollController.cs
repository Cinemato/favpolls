using Microsoft.AspNetCore.Mvc;
using Favpolls.Models;
using Favpolls.DataAccess.Repository.IRepository;
using Favpolls.Models.ViewModels;

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
            _unitOfWork.PollOption.AddRange(pollVM.PollOptions);

            var code = GenerateCode(6);
            pollVM.Poll.Code = code;

            _unitOfWork.Save();

            return View("CreateSuccess", pollVM.Poll);
        }

        public IActionResult Join(string code)
        {
            Poll poll = _unitOfWork.Poll.Get(p => p.Code == code);
            List<PollOption> pollOptions = _unitOfWork.PollOption.GetAll(o => o.PollId == poll.Id).ToList();

            var total = 0;
            foreach (var option in pollOptions)
            {
                total += option.VoteCount;
            }

            PollVM pollVM = new PollVM()
            {
                Poll = poll,
                PollOptions = pollOptions,
                TotalVotes = total
            };

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
