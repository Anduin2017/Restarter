using System;
using System.ComponentModel.DataAnnotations;
using Restarter.Models;

namespace Restarter.Models.HomeViewModels
{
    public class EditViewModel
    {
        public Server server { get; set; }
        [Display(Name = "选择健康监视器")]
        public int NewHMId { get; set; }

        public int ServerId { get; set; }
    }
}