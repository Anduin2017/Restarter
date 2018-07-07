using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restarter.Data;
using Restarter.Models;
using Restarter.Models.HomeViewModels;
using Restarter.Services;

namespace Restarter.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestartTrigger _restartTrigger;
        private readonly RestarterDbContext _dbContext;
        public HomeController(
            RestartTrigger restartTrigger,
            RestarterDbContext dbContext)
        {
            _restartTrigger = restartTrigger;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Restart(int id)
        {
            var server = await _dbContext.Servers.SingleOrDefaultAsync(t => t.Id == id);
            var result = await _restartTrigger.Restart(server);
            if (result.Contains("Successfully"))
            {
                _dbContext.AuditLogs.Add(new AuditLog
                {
                    Action = $"Successfully Restarted {server.Name}.",
                    Operator = User.Identity.Name,
                });
                await _dbContext.SaveChangesAsync();
                return Json(new { code = 0, message = "Success!" });
            }
            else
            {
                _dbContext.AuditLogs.Add(new AuditLog
                {
                    Action = $"Unsuccessfully Restarted {server.Name}.",
                    Operator = User.Identity.Name,
                });
                await _dbContext.SaveChangesAsync();
                return Json(new { code = -1, message = "Failed!", Reason = result });
            }
        }

        public async Task<IActionResult> Shutdown(int id)
        {
            var server = await _dbContext.Servers.SingleOrDefaultAsync(t => t.Id == id);
            var result = await _restartTrigger.Shutdown(server);
            if (result.Contains("Successfully"))
            {
                _dbContext.AuditLogs.Add(new AuditLog
                {
                    Action = $"Successfully Shutdown {server.Name}.",
                    Operator = User.Identity.Name,
                });
                await _dbContext.SaveChangesAsync();
                return Json(new { code = 0, message = "Success!" });
            }
            else
            {
                _dbContext.AuditLogs.Add(new AuditLog
                {
                    Action = $"Unsuccessfully Shutdown {server.Name}.",
                    Operator = User.Identity.Name,
                });
                await _dbContext.SaveChangesAsync();
                return Json(new { code = -1, message = "Failed!", Reason = result });
            }
        }

        public async Task<IActionResult> AllServers()
        {
            var servers = await _dbContext.Servers.ToListAsync();
            var model = new AllServersViewModel
            {
                Servers = servers
            };
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Audit()
        {
            var logs = await _dbContext
                .AuditLogs
                .OrderByDescending(t => t.EventTime)
                .AsNoTracking()
                .ToListAsync();

            return View(logs);
        }
    }
}
