using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        //Development usage

        // public async Task<IActionResult> Seed()
        // {
        //     _dbContext.Servers.Add(new Server
        //     {
        //         Name = "Example Machine",
        //         Architect = "HyperV",
        //         VCPU = "4",
        //         Memory = "1024MB",
        //         DiskSize = "200GB",
        //         InnerIP = "172.16.1.1",
        //         OutterIP = "202.118.17.65",
        //         Owner = "Anduin",
        //         OS = "Windows Server 2016",
        //         UsageA = "This is an Example",
        //         UsageB = "Ohhhh WTF!@??",
        //         VMArchitect = "HyperV",
        //         InDomainName = "Example.MyDomain.com"
        //     });
        //     await _dbContext.SaveChangesAsync();
        //     return RedirectToAction(nameof(Index));
        // }

        public async Task<IActionResult> Restart(int id)
        {
            var server = await _dbContext.Servers.SingleOrDefaultAsync(t => t.Id == id);
            if (server == null)
            {
                return NotFound();
            }
            var result = await _restartTrigger.Restart(server);
            if (result.Contains("Successfully"))
            {
                _dbContext.AuditLogs.Add(new AuditLog
                {
                    Action = $"成功重启了{server.Name}.",
                    Operator = User.Identity.Name,
                    IPAddress = HttpContext.Connection.RemoteIpAddress.ToString()
                });
                await _dbContext.SaveChangesAsync();
                return Json(new { code = 0, message = "Success!" });
            }
            else
            {
                _dbContext.AuditLogs.Add(new AuditLog
                {
                    Action = $"失败重启了{server.Name}.",
                    Operator = User.Identity.Name,
                    IPAddress = HttpContext.Connection.RemoteIpAddress.ToString()
                });
                await _dbContext.SaveChangesAsync();
                return Json(new { code = -1, message = "Failed!", Reason = result });
            }
        }

        public async Task<IActionResult> Shutdown(int id)
        {
            var server = await _dbContext.Servers.SingleOrDefaultAsync(t => t.Id == id);
            if (server == null)
            {
                return NotFound();
            }
            var result = await _restartTrigger.Shutdown(server);
            if (result.Contains("Successfully"))
            {
                _dbContext.AuditLogs.Add(new AuditLog
                {
                    Action = $"成功关闭了{server.Name}.",
                    Operator = User.Identity.Name,
                    IPAddress = HttpContext.Connection.RemoteIpAddress.ToString()
                });
                await _dbContext.SaveChangesAsync();
                return Json(new { code = 0, message = "Success!" });
            }
            else
            {
                _dbContext.AuditLogs.Add(new AuditLog
                {
                    Action = $"失败的关闭了{server.Name}.",
                    Operator = User.Identity.Name,
                    IPAddress = HttpContext.Connection.RemoteIpAddress.ToString()
                });
                await _dbContext.SaveChangesAsync();
                return Json(new { code = -1, message = "Failed!", Reason = result });
            }
        }

        public async Task<IActionResult> AllServers()
        {
            var servers = await _dbContext
                .Servers
                .AsNoTracking()
                .Include(t => t.Monitor)
                .Take(1000)
                .ToListAsync();

            var model = new AllServersViewModel
            {
                Servers = servers
            };
            return View(model);
        }

        public async Task<IActionResult> Edit(int id) // Server Id
        {
            var server = await _dbContext.Servers.AsNoTracking().SingleOrDefaultAsync(t => t.Id == id);
            var hms = await _dbContext.MonitorTemplates.AsNoTracking().ToListAsync();
            if (server == null)
            {
                return NotFound();
            }
            ViewData["HMID"] = new SelectList(hms, nameof(HealthMonitor.Id), nameof(HealthMonitor.MonitorName));
            var model = new EditViewModel
            {
                server = server,
                ServerId = server.Id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model)
        {
            var server = await _dbContext.Servers.SingleOrDefaultAsync(t => t.Id == model.ServerId);
            if (server == null)
            {
                return NotFound();
            }
            server.MonitorId = model.NewHMId;
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(AllServers));
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
