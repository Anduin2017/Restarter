using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Restarter.Models;

namespace Restarter.Services
{
    public class RestartTrigger
    {

        private IConfiguration _configuration { get; }
        public RestartTrigger(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> Restart(Server target)
        {
            var startInfo = new ProcessStartInfo
            {
                CreateNoWindow = true,
                FileName = _configuration["PowershellLocation"],
                Arguments = $"{_configuration["RestartScript"]} -target '{target.Name}'",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
            };
            string result = string.Empty;
            using (var process = new Process { StartInfo = startInfo })
            {
                process.Start();
                result = await process.StandardOutput.ReadToEndAsync();
                Console.WriteLine(result);
            }
            return result;
        }
    }
}