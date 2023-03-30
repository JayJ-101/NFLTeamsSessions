using Microsoft.AspNetCore.Mvc;
using NFLTeamsSessions.Models;

namespace NFLTeamsSessions.Controllers
{
    public class FavoritesConotroller:Controller
    {
        [HttpGet]
        public ViewResult Index()
        {
            var session = new NFLSession(HttpContext.Session);
            var model = new TeamListViewModel
            {
                ActiveConf = session.GetActiveConf(),
                ActiveDiv = session.GetActiveDiv(),
                Teams = session.GetMyTeams()
            };
            return View(model);
        }
        [HttpPost]
        public RedirectToActionResult Delete()
        {
            var session = new NFLSession(HttpContext.Session);
            session.RemoveMyTeams();

            TempData["message"] = "Favorite teamss cleard";

            return RedirectToAction("Index", new
            {
                ActiveConf = session.GetActiveConf(),
                ActiveDiv = session.GetActiveDiv()
            });
        }
    }
}
