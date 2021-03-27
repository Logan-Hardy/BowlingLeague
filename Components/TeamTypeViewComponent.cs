using BowlingLeague.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BowlingLeague.Components
{
    public class TeamTypeViewComponent : ViewComponent 
    {
        private BowlingLeagueContext context;
        public TeamTypeViewComponent (BowlingLeagueContext ctx)
        {
            context = ctx;
        }

        public IViewComponentResult Invoke()
        {
            return View(context.Teams
               .Distinct()
               .OrderBy(b => b.TeamName)
            );

            //return View(context.Bowlers
            //    .Select(b => b.BowlerLastName)
            //    .Distinct()
            //    .OrderBy(b => b));
        }

    }
}
