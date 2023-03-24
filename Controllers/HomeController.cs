using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NFLTeams.Models;
using NFLTeamsSessions.Models;
using System;
using System.Linq;

namespace NFLTeamsSessions.Controllers
{
    public class HomeController : Controller
    {
        private readonly TeamContext _ctx;

        public HomeController(TeamContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult Index(string activeConf = "all", string activeDiv = "all")
        {
            var session = new NFLSession(HttpContext.Session);
            session.SetActiveConf(activeConf);
            session.SetActiveDiv(activeDiv);

            var data = new TeamListViewModel
            {
                ActiveConf = activeConf,
                ActiveDiv = activeDiv,
                Conferences = _ctx.Conferences.ToList(),
                Divisions = _ctx.Divisions.ToList(),
            };

            IQueryable<Team> query = _ctx.Teams;

            if (activeDiv != "all")
                query = query.Where(
                    t => t.Conference.ConferenceID.ToLower() == activeConf.ToLower());
            if (activeDiv != "all")
                query = query.Where(
                    t => t.Division.DivisionID.ToLower() == activeDiv.ToLower());
            data.Teams = query.ToList();

            return View(data);
        }

        public IActionResult Details(string id)
        {
            var session = new NFLSession(HttpContext.Session);
            var model = new TeamViewModel
            {
                Team = _ctx.Teams
                    .Include(t => t.Conference)
                    .Include(t => t.Division)
                    .FirstOrDefault(t => t.TeamID == id),
                ActiveDiv = session.GetActiveDiv(),
                ActiveConf = session.GetActiveConf()
            };
            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Add(TeamViewModel data)
        {
            data.Team = _ctx.Teams
                .Include(t => t.Conference)
                .Include(t => t.Division)
                .Where(t => t.TeamID == data.Team.TeamID)
                .FirstOrDefault();
            var session = new NFLSession(HttpContext.Session);
            var teams = session.GetMyTeams();
            teams.Add(data.Team);
            session.SetMyTeams(teams);

            TempData["message"] = $"{data.Team.Name} added to your favorites";

            return RedirectToAction("index",
                new
                {
                    ActiveConf = session.GetActiveConf(),
                    ActiveDiv = session.GetActiveDiv()
                });
        }

    }
}
