using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restarter.Data;
using Restarter.Models.HealthMonitorViewModels;
using System.Threading.Tasks;

namespace Restarter.Controllers
{
    public class HealthMonitorController : Controller
    {
        private readonly RestarterDbContext _dbContext;
        public HealthMonitorController(RestarterDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var monitors = await _dbContext.MonitorTemplates.Take(1000).ToListAsync();
            var model = new IndexViewModel()
            {
                HealthMonitors = monitors
            };
            return View(model);
        }
    }
}