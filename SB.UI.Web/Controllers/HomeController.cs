using SB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SB.UI.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var board = new Board();

            board.TaskList.Add(new Domain.TaskList("List 1"));
            board.TaskList.Add(new Domain.TaskList("List 2"));
            board.TaskList.Add(new Domain.TaskList("List 3"));
            board.TaskList.Add(new Domain.TaskList("List 4"));

            return View(board);
        }
    }
}