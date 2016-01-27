using SB.Domain.Entities;
using SB.Domain.Interfaces; 
using System.Web.Mvc;

namespace SB.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        private IRepository<Board> Repository;

        public HomeController(IRepository<Board> repository)
        {
            this.Repository = repository;

        }

        public ActionResult Index()
        {
            var boards = this.Repository.List();

            return View(boards);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Board board)
        {
            board.Save(this.Repository);

            return RedirectToAction("Index");
        }
    }
}