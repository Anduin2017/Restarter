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
            var monitors = await _dbContext
                .MonitorTemplates
                .Include(t => t.Servers)
                .AsNoTracking()
                .Take(1000)
                .ToListAsync();

            var model = new IndexViewModel()
            {
                HealthMonitors = monitors
            };
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            var hm = new HealthMonitor
            {
                MonitorName = model.MonitorName,
                RequestPath = model.RequestPath,
                IsPostMethod = model.IsPostMethod,
                Form = model.Form,
                ExpectedStatusCode = model.ExpectedStatusCode,
                ExpectedContent = model.ExpectedContent
            };
            _dbContext.MonitorTemplates.Add(hm);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}