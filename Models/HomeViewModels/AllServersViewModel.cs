using System;
using System.Collections.Generic;

namespace Restarter.Models.HomeViewModels
{
    public class AllServersViewModel
    {
        public IEnumerable<Server> Servers { get; set; }
    }
}