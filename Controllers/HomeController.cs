using BowlingLeague.Models;
using BowlingLeague.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BowlingLeagueContext context { get; set; }

        public HomeController(ILogger<HomeController> logger, BowlingLeagueContext ctx)
        {
            _logger = logger;
            context = ctx;
        }

        
        public IActionResult Index(long? teamid, string teamname, int pageNum = 0)
        {
            int pageSize = 5;
            return View(new IndexViewModel
            {
                Bowlers = context.Bowlers
                    .Where(b => b.TeamId == teamid || teamid == null)
                    .OrderBy(b => b.TeamId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    
                    //These commands below allow us to use SQL to gather data
                    //.FromSqlRaw("SELECT * FROM Bowlers WHERE TeamID = 3")
                    //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamID = {teamid} OR {teamid} IS NULL")
                    .ToList(),

                Teams = context.Teams
                    .Where(b => b.TeamId == teamid || teamid == null)
                    .OrderBy(b => b.TeamId)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)

                    //These commands below allow us to use SQL to gather data
                    //.FromSqlRaw("SELECT * FROM Bowlers WHERE TeamID = 3")
                    //.FromSqlInterpolated($"SELECT * FROM Bowlers WHERE TeamID = {teamid} OR {teamid} IS NULL")
                    .ToList(),

                PageNumberingInfo = new PageNumberingInfo
                {
                    NumItemsPerPage = pageSize,
                    CurrentPage = pageNum,

                    //if no team has been selected, the get the full count.
                    //Otherwise, only count the number from the meal type that has been selected 
                    TotalNumItems = (teamid == null ? context.Bowlers.Count() :
                        context.Bowlers.Where(b => b.TeamId == teamid).Count())
                },

                TeamName = teamname

            });
                
                
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
