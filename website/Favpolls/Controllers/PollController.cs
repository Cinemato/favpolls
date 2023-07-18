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
             _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
