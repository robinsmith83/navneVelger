using NavneVelger.DataContexts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NavneVelger.Models
{
    public class NavnViewModel
    {
        public string Navn { get; set; }
        public string Navn2 { get; set; }
        public Gender Gender { get; set; }
        public List<NavnRangering> NavnRangeringList { get; set; }
        public int Id { get; set; }

        public string StatusMessage { get; set; }
    }
}
